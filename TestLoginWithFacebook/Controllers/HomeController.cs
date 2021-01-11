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
using TestLoginWithFacebook.Data;
using TestLoginWithFacebook.Data.ModelsData;
using TestLoginWithFacebook.Models;
using TestLoginWithFacebook.Services;
using TestLoginWithFacebook.ViewModels;

namespace TestLoginWithFacebook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext applicationDbContext;
        IEmailSender emailSender;
        IAartistRepository aartistRepository;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext, IEmailSender emailSender, IAartistRepository aartistRepository , SignInManager<IdentityUser> signInManager)
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
        //[Authorize]
        //public IActionResult Privacy()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var name = User.Identity.Name;
        //        var n = applicationDbContext.Users.
        //            Where(a => a.UserName == name).FirstOrDefault();
        //        var c = applicationDbContext.UserClaims.Where(a => a.UserId == n.Id).FirstOrDefault();

        //        Debug.WriteLine(c.ClaimType);

        //        //var joined = People.Join(PeopleTypes,
        //        //          PeopleKey => PeopleKey.PersonType,
        //        //          PeopleTypesKey => PeopleTypesKey.TypeID,
        //        //          (Person, PersoneType) => new
        //        //          {
        //        //              Name = Person.Name,
        //        //              TypeID = PersoneType.TypeID
        //        //          });

        //        var aa = applicationDbContext.Users.
        //            Join(applicationDbContext.UserClaims,
        //            uu => uu.Id,
        //            ur => ur.UserId, (ppp, ooo) => new
        //            {
        //                uuuuu = ppp.UserName,
        //                ooff = ooo.ClaimType
        //            }).FirstOrDefault();

        //        Debug.WriteLine(aa.ooff);

        //        var a = from u in applicationDbContext.Users
        //                join cc in applicationDbContext.UserClaims
        //                on u.Id equals cc.UserId
        //                where u.UserName == name
        //                select cc;
        //        var eee = Enums.Roles.Admin;
        //        if (eee.ToString() == aa.ooff)
        //        {
        //            Debug.WriteLine("----------------------");
        //            Debug.WriteLine(eee);
        //            Debug.WriteLine("-----------------------");
        //        }

        //        Debug.WriteLine(a.FirstOrDefault().ClaimType);

        //        ViewData["name"] = n;
        //        return View();

        //    }
        //    return RedirectToAction("Index");
        //}


        public IActionResult SearchArtsit()
        {
            return View();
        }

        public IActionResult SearchArtsitSubmit(SearchArtist searchArtist)
        {

            var result = aartistRepository.SearchArtsit(searchArtist.City, searchArtist.TimeEvent, (Enums.EventType)searchArtist.TypeEvent, (Enums.ArtistType)searchArtist.TypeArtist);
            var cardsArtits = aartistRepository.GetCardsArtistByListId(result);
            //var idcity = applicationDbContext.Citys.Where(c => c.City == searchArtist.City).Select(c => c.Id).FirstOrDefault();

            //var idaritts = applicationDbContext.CictyWorks.Where(c => c.IdCity == idcity).Select(i => i.IdArtis).ToList();

            //var dys = searchArtist.TimeEvent.DayOfWeek.ToString();


            //var lllllll = applicationDbContext.DaysWorks.FromSqlRaw($"select * from DaysWorks where {dys} = 1").Select(a => a.IdArtit).ToList();

            ////var ats =   applicationDbContext.Artists.Where(i => idaritts.Any(b => b == i.Id) && lllllll.Any(b =>b == i.Id)).
            ////    Where(a => (ArtistType)a.ArtistType == searchArtist.TypeArtist &&
            //// (EventType)a.EventType == searchArtist.TypeEvent).ToList();

            //var sest = applicationDbContext.Artists.Where(i => idaritts.Any(b => b == i.Id) && lllllll.Any(b => b == i.Id)).
            //    Where(a => (ArtistType)a.ArtistType == searchArtist.TypeArtist &&
            // (EventType)a.EventType == searchArtist.TypeEvent).
            // Join(applicationDbContext.Users,
            // ar => ar.IdUser,
            // us => us.Id, (art, use) => new
            // {
            //     Id = art.Id,
            //     Fulname = use.UserName,
            //    /// Image = art.ImageProfile,
            //     Price = art.Price,

            // }).ToList();


            ////var sest1 = applicationDbContext.Artists.Join(applicationDbContext.Users,

            ////    ar => ar.IdUser, 
            ////    us => us.Id,

            ////    (a, u) => new
            ////    {
            ////         id= a.Id
            ////    }).Join(applicationDbContext.DaysWorks,
            ////      a =>a.id,
            ////      x=>x.IdArtit,

            ////      (aaa,bbb)  => new{

            ////      }
            ////    );



            //List<Artis> a = (sest.Select(item => new Artis
            //{
            //    FullName = item.Fulname,
            //    Id = item.Id,
            //    // IimagePath = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(item.Image)),
            //    Price = item.Price,

            //})).ToList();

            List<Artis> a1 = (cardsArtits.Select(item => new Artis
            {
                Id = item.Id,
                FullName = item.FullName,
                Description = item.Description,
                Price = item.Price,
                IimagePath = Convert.ToBase64String(item.Image)

            })).ToList();



            return View("ResultArtist", a1);
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
