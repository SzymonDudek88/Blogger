using Application.Dto;
using Application.Dto.Cosmos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V2
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //POLE
        private readonly ICosmosPostService _postService;

        public PostsController(ICosmosPostService postService)
        {
            _postService = postService; //przepisujesz posty
        }


        //metoda ktora zwroci listę wszystkich postów
        [SwaggerOperation(Summary = "Retrieves all posts")]
        [HttpGet]// informacja że  akcja get odpowiada metodzie Http typu get
        public async Task<IActionResult> Get() {
            //pobieramy posty
            var posts = await _postService.GetAllPostsAsync( );

            return Ok(posts);
        }

      

        [SwaggerOperation(Summary = "Retrieves post by Id")]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(string id) { 
            var post = await  _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [SwaggerOperation(Summary = "Creating new post")]
        [HttpPost] // to oznacza ze akcja http odpowiada ponizszej akcji 
        // jeeli pojawi sie zadanie http typu post pod adres ponizej to wywola sie metoda z klasy post controller
        public async Task<IActionResult> Create(CreateCosmosPostDto newPost)
        {
            var post = await _postService.AddNewPostAsync(newPost);
            return Created($"api/posts/{post.Id}", post);


        }
        [SwaggerOperation(Summary = "Updating existing post")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCosmosPostDto updatePost)
        {
           await _postService.UpdatePostAsync(updatePost);

            return NoContent();
        }
        [SwaggerOperation(Summary = "Delete post")]
        [HttpDelete("{id}")] // tu byl blad nie moze byc spacji  w "" 
        public async Task<IActionResult> Delete(string id)
        {
          await  _postService.DeletePostAsync(id);

            return NoContent();
        }
    }
}
