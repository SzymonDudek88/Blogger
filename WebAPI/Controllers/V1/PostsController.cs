 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //POLE
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService; //przepisujesz posty
        }

        [SwaggerOperation(Summary = "Retrieves sort fields")] 
        [HttpGet("[action]")]// informacja że  akcja get odpowiada metodzie Http typu get
        public  IActionResult  GetSortFields( )
        {

            return Ok (SortingHelper.GetSortFields().Select (x => x.Key)) ;
        }

        //metoda ktora zwroci listę wszystkich postów 
        [SwaggerOperation(Summary = "Retrieves paged posts")]
        [HttpGet]// informacja że  akcja get odpowiada metodzie Http typu get                                                                   "" poniewaz ddomyslnie puste ma byc inaczje by wywalalo wyjatek kiedy nic by nie filtrowano
        public async Task < IActionResult> Get([FromQuery]PaginationFilter paginationFilter, [FromQuery]SortingFilter sortingFilter, [FromQuery] string filterBy = "") {
            // parametr fromquery  oznacza ze wartosc parametru zostanie pobrana z ciągu zapytania 

            //ponizej validacja sortowania danych przeslanych przez klienta
            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            //te dane teraz trzeba wyslac do odpowiedniego miejsca repozytorium
            //zaczynamy od domeny i Ipostrepository
            // potem do klasy postRepository w Infrastructure
            //a potem klase seriwsu - w Application
            // najpierw interfejs potem , potem interface post service potem Post service 
            var posts = await _postService.GetAllPostsAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
                                                            validSortingFilter.SortField,validSortingFilter.Ascending,
                                                            filterBy);


            //lista wszystkich postow:
            var totalRecords = await _postService.GetAllPostsCountAsync(filterBy);

            //to co dalej robimy: dodamy kolejna klase opakowujaca odpowiedz ktora rozszerzy response
            // o kolejne parametry ktore maja sens tylko dla listy elementow

            // po prostu opakowujemy :   no przeciez proste, 
            //            return Ok(new PagedResponse<IEnumerable<PostDto>> (posts,validPaginationFilter.PageNumber, validPaginationFilter.PageSize)); //200 ok result 
            ///to wyzej nei wie mczemu zastapione nizej to po co to pisalismy w tym odcinku        
            return Ok(PaginationHelper.CreatePageResponse(posts, validPaginationFilter, totalRecords)); 
        
        }

        [SwaggerOperation(Summary = "Retrieves posts cointaning searched text")]
        [HttpGet("Search/{TextToFind}")]// informacja że  akcja get odpowiada metodzie Http typu get
        public async Task<IActionResult> GetPostByTitleContent(string TextToFind)
        {
            //pobieramy posty
            var posts = await _postService.GetPostByTitleContentAsync(TextToFind); 

            return Ok(posts) ; //200 ok result 
        }

        [SwaggerOperation(Summary = "Retrieves all posts")]
        [EnableQuery]
        [HttpGet("[action]")]
        // zapytanie bedzie wykonane na bazie danych linq - sql - jezeli bylo by ienumerable to pobralby nie potrzebnie
        // cala ilosc np 1000 postow i dopiero wybral 5
        public IQueryable<PostDto> GetAll()
        {
            return _postService.GetAllPosts();
        }


        [SwaggerOperation(Summary = "Retrieves post by Id")]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id) {
            var post = await _postService.GetPostIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(new Response <PostDto> (post));
        }

        [SwaggerOperation(Summary = "Creating new post")]
        [HttpPost] // to oznacza ze akcja http odpowiada ponizszej akcji 
        // jeeli pojawi sie zadanie http typu post pod adres ponizej to wywola sie metoda z klasy post controller
        public async Task<IActionResult> Create(CreatePostDto newPost)
        {
            var post = await _postService.AddNewPostAsync(newPost);
            return Created($"api/posts/{post.Id}", new Response<PostDto> (post));


        }
        [SwaggerOperation(Summary = "Updating existing post")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostDto updatePost)
        {
          await  _postService.UpdatePostAsync(updatePost);

            return NoContent();
        }
        [SwaggerOperation(Summary = "Delete post")]
        [HttpDelete("{id}")] // tu byl blad nie moze byc spacji  w "" 
        public async Task<IActionResult> Delete(int id)
        {
           await _postService.DeletePostAsync(id);

            return NoContent();
        }
    }
}
