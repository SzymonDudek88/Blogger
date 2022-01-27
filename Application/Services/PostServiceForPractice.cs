//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Application.Dto;
//using Application.DTO;
//using Application.Interfaces;
//using Domain.Interfaces;

//namespace Application.Services
//{// W TEJ KLASIE RZUTUJESZ NA PostDTO pobierajac z postRepository typu IPostRepository ktore pracuje na POST
//    class PostServiceForPractice : IPostService
//    {
//       private readonly IPostRepository _postDtos; //ipostrepository pracyja na Postach
//                                                   /// a ja chce pracowac na PostDto

//        public PostServiceForPractice(IPostRepository postRepository ) //to jest przekazane przy wywolywaniu
//            //tej tutaj klasy
//        {
//            _postDtos = postRepository;
//        }

//        public PostDto AddNewPost(CreatePostDto newPost)
//        {
//            throw new NotImplementedException();
//        }

//        //zeby przypisac musisz stworzyc kolekcje - ale z czego ?
//        // jakiego ona ma byc typu? nvm  ale chyba tu tworze jakis post
//        public IEnumerable<PostDto> GetAllPosts()
//        {
//            var posts = _postDtos.GetAll();

//            return  from a in posts
//                             select new PostDto 
//                             {
//                                 Id = a.Id, 
//                                 Title = a.Title, 
//                                 Content = a.Content 
//                             };
        
//        }

//        public PostDto GetPostId(int id)
//        {
//            // co chce zrobic, chce zyskac Id jakiegos posta
//            var cos = _postDtos.GetById(id);
//            // chce przeciez zwrocic Post dt bo taki interface zaimplementowalem
//            return new PostDto() {
//                Id = cos.Id,
//                Title = cos.Title,
//                Content = cos.Content

//            };
//            // wrzucam id wg inta ok... zwracam post dto
//             // tu sa przechowane PostDto

//         }
//    }
//}
