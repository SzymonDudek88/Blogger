 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using Infrastructute.Identity;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;
using Application.Services;
using Application.Validators;
using WebAPI.Attributes;

namespace WebAPI.Controllers.V1
{
   // atrybut autorize umozliwia ograniczenie dostepu na podstawie rol - deklaratywny do kontrolera lub akcji
   // sprawdza uwierzytelnienie i dostepnosc do zasobów 

    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize] // okreslanie roli ktora pozwala na uzycie danej zasobu wg jakiejs danej roli
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
        [AllowAnonymous]
        [HttpGet("[action]")]// informacja że  akcja get odpowiada metodzie Http typu get
        public  IActionResult  GetSortFields( )
        {

            return Ok (SortingHelper.GetSortFields().Select (x => x.Key)) ;
        }

        //metoda ktora zwroci listę wszystkich postów 
        [SwaggerOperation(Summary = "Retrieves paged posts")]
        [AllowAnonymous]
        [HttpGet]// informacja że  akcja get odpowiada metodzie Http typu get                                                                   "" poniewaz ddomyslnie puste ma byc inaczje by wywalalo wyjatek kiedy nic by nie filtrowano
        public async Task < IActionResult> Get([FromQuery]PaginationFilter paginationFilter, [FromQuery]SortingFilter sortingFilter, [FromQuery] string filterBy = "") {
            // parametr fromquery  oznacza ze wartosc parametru zostanie pobrana z ciągu zapytania 

            //ponizej validacja sortowania danych przeslanych przez klienta
            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            
            var posts = await _postService.GetAllPostsAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
                                                            validSortingFilter.SortField,validSortingFilter.Ascending,
                                                            filterBy);


            //lista wszystkich postow:
            var totalRecords = await _postService.GetAllPostsCountAsync(filterBy);
      
            return Ok(PaginationHelper.CreatePageResponse(posts, validPaginationFilter, totalRecords)); 
        
        }

        [SwaggerOperation(Summary = "Retrieves posts cointaning searched text")]
        [AllowAnonymous]
        [HttpGet("Search/{TextToFind}")]// informacja że  akcja get odpowiada metodzie Http typu get
        public async Task<IActionResult> GetPostByTitleContent(string TextToFind)
        {
            //pobieramy posty
            var posts = await _postService.GetPostByTitleContentAsync(TextToFind); 

            return Ok(posts) ; //200 ok result 
        }

        [SwaggerOperation(Summary = "Retrieves all posts")]
        [EnableQuery]
        [Authorize(Roles = UserRoles.AdminOrSuperUser)] // UWAGA1!!!! nadrzedny jest zawsze górny atrybut z calego kontrolera(tam w naglowku pod wersja), bo nadał on dostep wszedzie jako dostepny dla USER
       // i admin nie mial dostepu jako user bo nie byl userem a tylko adminem! - kasujesz z naglowka role na puste authorize
        [HttpGet("[action]")]
        // zapytanie bedzie wykonane na bazie danych linq - sql - jezeli bylo by ienumerable to pobralby nie potrzebnie
        // cala ilosc np 1000 postow i dopiero wybral 5
        public IQueryable<PostDto> GetAll()
        {
            return _postService.GetAllPosts();
        }


        [SwaggerOperation(Summary = "Retrieves post by Id")]
        [AllowAnonymous]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id) {
            var post = await _postService.GetPostIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(new Response <PostDto> (post));
        }

        [ValidateFilter] // ValidateFilterAttribute - ale nie dajesz attribute - taka konwencja 
        [SwaggerOperation(Summary = "Creating new post")]
        [Authorize(Roles = UserRoles.UserOrSuperUser)]
        [HttpPost] // to oznacza ze akcja http odpowiada ponizszej akcji 
        // jeeli pojawi sie zadanie http typu post pod adres ponizej to wywola sie metoda z klasy post controller
        public async Task<IActionResult> Create(CreatePostDto newPost)
        {
            // first create  validator object :
            var validator = new CreatePostDtoValidator();
 

            var tempString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var post = await _postService.AddNewPostAsync(newPost , tempString);
            return Created($"api/posts/{post.Id}", new Response<PostDto>(post));


        }
        [SwaggerOperation(Summary = "Updating existing post")]
        [Authorize(Roles = UserRoles.UserOrSuperUser)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostDto updatePost)
        {
            // spr czy uzytkownik jest autorem posta UserOwnsPostAsync
            var userOwnsPost = await _postService.UserOwnsPostAsync(updatePost.Id, User.FindFirstValue(ClaimTypes.NameIdentifier)); // spr czy jest wlascicielem posta
            if (!userOwnsPost)
            {
                return BadRequest(new Response( false, "you dont own this post"));// przerobiono po zmianie klasy response
            }


          await  _postService.UpdatePostAsync(updatePost);

            return NoContent();
        }
        [SwaggerOperation(Summary = "Delete post")]
        [Authorize(Roles = UserRoles.AdminOrUserOrSuperUser)] // bo sa podane po przecinku - recznie wpisane bylo by "Admin,User"
        [HttpDelete("{id}")] // tu byl blad nie moze byc spacji  w "" 
        public async Task<IActionResult> Delete(int id)
        {
            // spr czy uzytkownik jest autorem posta 
            var userOwnsPost = await _postService.UserOwnsPostAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier)); // spr czy jest wlascicielem posta
            // jeszcze sprawdzamy czy uzytkownik jest przypisany jako admin
            var userIsAdmin = User.FindFirstValue(ClaimTypes.Role).Contains(UserRoles.Admin);
            var userIsSuperUser = User.FindFirstValue(ClaimTypes.Role).Contains(UserRoles.SuperUser); // spr czy jest superuser
            // jezeli nie jest to bad reques
            if (!userOwnsPost && !userIsAdmin && !userIsSuperUser)
            {
                return BadRequest(new Response(false, "you dont own this post") ); // przerobiono po zmianie klasy response
            }

            await _postService.DeletePostAsync(id);

            return NoContent();
        }
    }
}
