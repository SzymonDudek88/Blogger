 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

namespace WebAPI.Controllers.V3
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //POLE
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService; //przepisujesz posty
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
           
            var post = await _postService.AddNewPostAsync(newPost, User.FindFirstValue(ClaimTypes.NameIdentifier)); // identify
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
