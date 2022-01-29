 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers.V1
{
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
        //metoda ktora zwroci listę wszystkich postów
        [SwaggerOperation(Summary = "Retrieves all posts")]
        [HttpGet]// informacja że  akcja get odpowiada metodzie Http typu get
        public IActionResult Get() {
            //pobieramy posty
            var posts = _postService.GetAllPosts();

            return Ok(posts); //200 ok result 
        }

        [SwaggerOperation(Summary = "Retrieves posts cointaning searched text")]
        [HttpGet("Search/{TextToFind}")]// informacja że  akcja get odpowiada metodzie Http typu get
        public IActionResult GetPostByTitleContent(string TextToFind)
        {
            //pobieramy posty
            var posts = _postService.GetPostByTitleContent(TextToFind); 

            return Ok(posts) ; //200 ok result 
        }

        [SwaggerOperation(Summary = "Retrieves post by Id")]
        [HttpGet("{id}")]

        public IActionResult Get(int id) {
            var post = _postService.GetPostId(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [SwaggerOperation(Summary = "Creating new post")]
        [HttpPost] // to oznacza ze akcja http odpowiada ponizszej akcji 
        // jeeli pojawi sie zadanie http typu post pod adres ponizej to wywola sie metoda z klasy post controller
        public IActionResult Create(CreatePostDto newPost)
        {
            var post = _postService.AddNewPost(newPost);
            return Created($"api/posts/{post.Id}", post);


        }
        [SwaggerOperation(Summary = "Updating existing post")]
        [HttpPut]
        public IActionResult Update(UpdatePostDto updatePost)
        {
            _postService.UpdatePost(updatePost);

            return NoContent();
        }
        [SwaggerOperation(Summary = "Delete post")]
        [HttpDelete("{id}")] // tu byl blad nie moze byc spacji  w "" 
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);

            return NoContent();
        }
    }
}
