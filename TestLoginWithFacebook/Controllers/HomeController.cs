using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BookingArtistMvcCore.Data;
using BookingArtistMvcCore.Data.ModelsData;
using BookingArtistMvcCore.Models;
using BookingArtistMvcCore.Services;
using BookingArtistMvcCore.ViewModels;
using System.Text.Encodings.Web;

namespace BookingArtistMvcCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext applicationDbContext;
        IEmailSender emailSender;
        IAartistRepository aartistRepository;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext, IEmailSender emailSender, IAartistRepository aartistRepository, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            this.applicationDbContext = applicationDbContext;
            this.emailSender = emailSender;
            this.aartistRepository = aartistRepository;
            _signInManager = signInManager;
        }

        public IActionResult TestSendMeil()
        {
            return View();
        }

        public IActionResult SendEmail(string email, string messeg)
        {
            emailSender.SendEmailAsync(email, messeg, messeg);
            return View("TestSendMeil");
        }
        public IActionResult Index()
        {
            return RedirectToAction("SearchArtsit");
        }
       


        public IActionResult SearchArtsit()
        {
            return View();
        }

        public IActionResult SearchArtsitSubmit(SearchArtist searchArtist)
        {

            var result = aartistRepository.SearchArtsit(searchArtist.City, searchArtist.TimeEvent, (Enums.EventType)searchArtist.TypeEvent, (Enums.ArtistType)searchArtist.TypeArtist);
            var cardsArtits = aartistRepository.GetCardsArtistByListId(result);
            

            List<Artis> a1 = (cardsArtits.Select(item => new Artis
            {
                Id = item.Id,
                FullName = item.FullName,
                Description = item.Description,
                Price = item.Price,
                IimagePath = Convert.ToBase64String(item.Image)

            })).ToList();

            ViewData["DateSerach"] = searchArtist.TimeEvent;
            ViewData["DateSerachAll"] = searchArtist;

            return View("ResultArtist", a1);
        }

        public IActionResult ViewArtist(int id, DateTime TiemSerch, string city)
        {

            var a = aartistRepository.GetCardArtitById(id);
            var artis = new ArtistExtened
            {
                ArtistType = (ArtistType)a.ArtistType,
                Id = id,
                Description = a.Description,
                FullName = a.FullName,
                Price = a.Price,
                IimagePath = Convert.ToBase64String(a.Image)

            };
            ViewData["DateSerach1"] = TiemSerch;
            ViewData["city"] = city;

            return View(artis);
        }
        [Authorize]
        public IActionResult OrderNow(int id, DateTime TiemSerch, string city)
        {
            var p = aartistRepository.GetProfileArtistByIdAtris(id);
            var a = aartistRepository.GetArtitById(id);
           
            var user = aartistRepository.GetIdentityUserByUsurName(User.Identity.Name);

            var client = aartistRepository.GetClient(user.Id);
            Order order = new Order
            {
                DateTimeEvent = TiemSerch,
                City = city,
                NameArtist = p.FullName,
                ArtistType = (ArtistType)a.ArtistType,
                EventType = (EventType)a.EventType,
                Price = a.Price,
                NameClient = client.FullName,
                PhoneClient = user.PhoneNumber,
                IdArtist = id

            };
         
            return View(order);
        }

        public IActionResult OrderNowSubmit(Order order)
        {
            
            var user = aartistRepository.GetIdentityUserByUsurName(User.Identity.Name);

             var client = aartistRepository.GetClient(user.Id);

            aartistRepository.AddOrder(new Orders
            {
                IdAtris = order.IdArtist,
                IdClient = client.Id,
                IdCity = aartistRepository.SearchCity(order.City).FirstOrDefault().Id,
                IfApprovedOrder = false,
                IfPaid = false,
                Price = order.Price,
                DateEvent = order.DateTimeEvent,
                OrderDate = DateTime.Now,
               
            }) ;

           

            emailSender.SendEmailAsync(user.Email, " הזמנת אומן לאיורע בתאריך" + order.DateTimeEvent.ToString(),
                "<h1>הזמנה באתר booking artist</h1>"+
                "<hr/>"+
                $"<h4>עיר: {order.City}</h4>"+
                $"<h4>שם אומן {order.NameArtist}</h4>" +
                "<h4></h4>" +
                $"<a href='http://bookingtestsite.azurewebsites.net/home/Orders'>youer order</a>");
            return RedirectToAction("Client");
        }

        [HttpPost]
        public JsonResult SearchCity(string citySearch)
        {
            var citys = aartistRepository.SearchCity(citySearch);
            return Json(citys);
        }


        public IActionResult Testsub(ArtistSetings artistSetings)
        {

            var idUser = aartistRepository.GetIdUserByUsurName(User.Identity.Name);

            //creat artist
            if (aartistRepository.IfArtisCreated(idUser) == false)
            {
                var citys = JsonConvert.DeserializeObject<List<Citys>>(artistSetings.Citys);

                var newIdArtit = aartistRepository.AddArtist(new Artist
                {
                    IdUser = idUser,
                    ArtistType = (Enums.ArtistType)artistSetings.TypeArtist,

                    EventType = (Enums.EventType)artistSetings.TypeEvent,
                    Price = artistSetings.Price,
                });

                artistSetings.DaysWork.IdArtit = newIdArtit;
                aartistRepository.AddDaysWork(artistSetings.DaysWork);
                aartistRepository.AddCictyWorks(citys, newIdArtit);

            }
            //edit artisi
            else
            {

                var artist = aartistRepository.GetArtitByIdUser(idUser);


                artist.ArtistType = (Enums.ArtistType)artistSetings.TypeArtist;

                artist.EventType = (Enums.EventType)artistSetings.TypeEvent;
                artist.Price = artistSetings.Price;

                aartistRepository.EditArtist(artist);


                artistSetings.DaysWork.IdArtit = artist.Id;
                var dayWork = aartistRepository.GetByIdArtist(artist.Id);



                dayWork.Friday = artistSetings.DaysWork.Friday;
                dayWork.Monday = artistSetings.DaysWork.Monday;
                dayWork.Sunday = artistSetings.DaysWork.Sunday;
                dayWork.Thursday = artistSetings.DaysWork.Thursday;
                dayWork.Tuesday = artistSetings.DaysWork.Tuesday;
                dayWork.Wednesday = artistSetings.DaysWork.Wednesday;


                aartistRepository.EditDaysWork(dayWork);


                var citys = JsonConvert.DeserializeObject<List<Citys>>(artistSetings.Citys);

                aartistRepository.DeleteAllCityWorkByIdArtist(artist.Id);
                aartistRepository.AddCictyWorks(citys, artist.Id);





            }
            return View("ArtistCard", artistSetings);

        }

        [Authorize]
        public IActionResult JoiningClient()
        {
            return View();
        }
        [Authorize]
        public IActionResult JoiningClientJoining(string FullName)
        {
            var idUser = aartistRepository.GetIdUserByUsurName(User.Identity.Name);
            aartistRepository.AddClient(new Data.ModelsData.Client
            {
                IdUser = idUser,
                FullName = FullName
            });
            return RedirectToAction("Client");
        }
        [Authorize]
        public IActionResult Client()
        {
           
            var idUser = aartistRepository.GetIdUserByUsurName(User.Identity.Name);
            if (aartistRepository.IfClientExists(idUser) == false)
            {
                return RedirectToAction("JoiningClient");
            }
            else
            {
                var client =  aartistRepository.GetClient(idUser);
                return View(client);
            }
        }

        [Authorize]
        public IActionResult Orders()
        {
            var idUser = aartistRepository.GetIdUserByUsurName(User.Identity.Name);
            var client = aartistRepository.GetClient(idUser);
            var orders=  aartistRepository.GetOrdersByClent(client.Id);
            List<Order> orders1 = orders.Select(o => new Order
            {
                City = aartistRepository.GetCityById(o.IdCity),
                Price = o.Price,
               DateTimeEvent = o.DateEvent,
               

            }).ToList();
            return View(orders1);
        }

        public IActionResult GetPostsByIdArtist(int id)
        {
           
            var post =  aartistRepository.GetPostByIdArtist(id);
            var profile = aartistRepository.GetProfileArtistByIdAtris(id);

            List<ViewModels.Post> posts = post.Select(item => new ViewModels.Post
            {
                Description = item.Description,
                Id = item.Id,
                Title = item.Title,
                Image = Convert.ToBase64String(item.Image),
                ImageProfile = Convert.ToBase64String(profile.ImageProfile),
                NameProfile = profile.FullName,
                UploadTime = item.UploadTime

            }).ToList();
            return PartialView("_Posts" , posts);
        }

        public IActionResult GetAllPosts()
        {

            var post = aartistRepository.GetAllPost() ;
           // var profile = aartistRepository.GetProfileArtistByIdAtris(id);

            List<ViewModels.Post> posts = post.Select(item => new ViewModels.Post
            {
                Description = item.Description,
                Id = item.Id,
                Title = item.Title,
                Image = Convert.ToBase64String(item.Image),
                //ImageProfile = Convert.ToBase64String(profile.ImageProfile),
                //NameProfile = profile.FullName
                UploadTime = item.UploadTime

            }).ToList();
            return PartialView("_Posts", posts);
        }
        public async Task<IActionResult> GetAllPostsAsinc()
        {

            var post = await aartistRepository.GetAllPostPerfectAsinc();
            // var profile = aartistRepository.GetProfileArtistByIdAtris(id);

            List<ViewModels.Post> posts = post.Select(item => new ViewModels.Post
            {
                Description = item.Description,
                Id = item.Id,
                Title = item.Title,
                Image = Convert.ToBase64String(item.Image),
                ImageProfile = Convert.ToBase64String(item.ImageProfile),
                NameProfile = item.FullName,
                UploadTime = item.UploadTime


            }).ToList();
            return  PartialView("_Posts", posts);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
