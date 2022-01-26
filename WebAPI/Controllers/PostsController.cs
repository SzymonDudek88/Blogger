using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //POLE
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        //metoda ktora zwroci listę wszystkich postów
        
        [HttpGet]// informacja że  akcja get odpowiada metodzie Http typu get
        public IActionResult Get() {
            //pobieramy posty
         var posts =    _postService.GetAllPosts();

            return Ok(posts); //200 ok result  


        }


    }
}
