using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookingArtistMvcCore.Data.ModelsData;
using BookingArtistMvcCore.Services;
using BookingArtistMvcCore.ViewModels;
using System.Drawing.Imaging;
using System.Drawing;
using BookingArtistMvcCore.ViewModels.Validations;
using Microsoft.AspNetCore.Http;
using BookingArtistMvcCore.Helpers;

namespace BookingArtistMvcCore.Controllers
{
    public class ArtistController : Controller
    {
        // IEmailSender emailSender;
        IAartistRepository aartistRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        public ArtistController(IAartistRepository aartistRepository, IEmailSender emailSender, SignInManager<IdentityUser> signInManager)
        {
            this.aartistRepository = aartistRepository;

            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var a = aartistRepository.GetArtitByUserNmae(User.Identity.Name);
            if (a != null)
            {

                var p = aartistRepository.GetProfileArtistByIdAtris(a.Id);
                if (p != null)
                {

                    var pv = new ViewModels.ProfileArtist()
                    {
                        FullName = p.FullName,
                        Description = p.About,
                        Image = $"getfile?filename={p.ImageProfile}"
                    };
                    return View(pv);
                }
                return View();
            }
            return RedirectToAction("ArtistCard");
        }

        public async Task<IActionResult> SaveProfile(ViewModels.ProfileArtist profileArtist)
        {

            var a = aartistRepository.GetArtitByUserNmae(User.Identity.Name);
            var filenName = "";
            if (profileArtist.FileImg != null)
                filenName = await UploadFile(profileArtist.FileImg);

            var p = aartistRepository.GetProfileArtistByIdAtris(a.Id);
            if (p == null)
            {


                aartistRepository.AddProfileArtist(
                    new Data.ModelsData.ProfileArtist
                    {

                        ImageProfile = filenName,

                        About = profileArtist.Description,
                        IdArtit = a.Id,
                        FullName = profileArtist.FullName
                    });
            }
            else
            {
                p.ImageProfile = filenName;
                p.About = profileArtist.Description;
                p.FullName = profileArtist.FullName;
                aartistRepository.EditProfileArtist(p);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult JoiningArtist()
        {
            return View();
        }
        [Authorize]
        public IActionResult SubmitJoinig()
        {
            aartistRepository.AddUserClaims("Artist", "2", aartistRepository.GetIdUserByUsurName(User.Identity.Name));
            _signInManager.SignOutAsync();

            return RedirectToAction("ArtistCard", "Artist");
        }

        [Authorize]
        public IActionResult ArtistCard()
        {
            if (User.HasClaim("Artist", "2"))
            {

            }
            else
            {
                return View("JoiningArtist");
            }
            var artist = aartistRepository.GetArtitByUserNmae(User.Identity.Name);

            if (artist != null)
            {
                var citysWork = aartistRepository.GetCictyWorksByArtist(artist.Id);
                List<Citys> citys = aartistRepository.GetCitysByIds(citysWork.Select(i => i.IdCity).ToList());

                var dayWork = aartistRepository.GetByIdArtist(artist.Id);

                ArtistSetings artistSetings = new ArtistSetings
                {
                    Price = artist.Price,
                    TypeArtist = (ArtistType)artist.ArtistType,
                    TypeEvent = (EventType)artist.EventType,
                    DaysWork = dayWork,
                    Citys = JsonConvert.SerializeObject(citys)
                };

                return View(artistSetings);
            }
            return View();

        }


        [HttpPost]
        public JsonResult SearchCity(string citySearch)
        {
            var citys = aartistRepository.SearchCity(citySearch);
            return Json(citys);
        }

        [Authorize]
        public IActionResult Orders()
        {
            var artist = aartistRepository.GetArtitByUserNmae(User.Identity.Name);
            if (artist != null)
            {

                var ors = aartistRepository.GetOrdersByArtsist(artist.Id);
                List<OrderForArtist> orders = ors.Select(o => new OrderForArtist
                {

                    City = aartistRepository.GetCityById(o.IdCity),
                    Price = o.Price,
                    IfPaid = o.IfPaid,
                    IfApprovedOrder = o.IfApprovedOrder,
                    DateTimeEvent = o.DateEvent,
                    OrderDate = o.OrderDate,


                }).ToList();

                return View(orders);
            }
            else
            {
                return RedirectToAction("ArtistCard");
            }
        }

        [Authorize]
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
        public IActionResult CreatePost()
        {
            return PartialView("_CreatePost");
        }
        [Authorize]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateNewPost(PostNew postNew)
        {
            if (ModelState.IsValid)
            {

                var idUser = aartistRepository.GetIdUserByUsurName(User.Identity.Name);

                var id = aartistRepository.GetIdArtistByIdUser(idUser);




                var fieleName = await UploadFile(postNew.ImageFile);
                aartistRepository.AddPost(new Data.ModelsData.Post
                {

                    idArtist = id,
                    Description = postNew.Description,
                    Title = postNew.Title,
                    //Image = ResizeImage(fileBytes),
                    Image = fieleName,
                    UploadTime = DateTime.Now

                });
            }
            return RedirectToAction("MyPost");


        }



        public async Task<JsonResult> CreateNewPostJson([FromForm] PostNew postNew)
        {
            if (ModelState.IsValid)
            {




                var idUser = aartistRepository.GetIdUserByUsurName(User.Identity.Name);

                var id = aartistRepository.GetIdArtistByIdUser(idUser);

                var fileName = await UploadFile(postNew.ImageFile);



                aartistRepository.AddPost(new Data.ModelsData.Post
                {

                    idArtist = id,
                    Description = postNew.Description,
                    Title = postNew.Title,
                    Image = fileName,
                    UploadTime = DateTime.Now

                });
            }
            return Json("ok");


        }
        [Authorize]
        public IActionResult MyPost()
        {

            var artist = aartistRepository.GetArtitByUserNmae(User.Identity.Name);
            if (artist != null)
            {


                var post = aartistRepository.GetPostByIdArtist(artist.Id);

                var profile = aartistRepository.GetProfileArtistByIdAtris(artist.Id);
                List<ViewModels.Post> posts = post.Select(item => new ViewModels.Post
                {
                    Description = item.Description,
                    Id = item.Id,
                    Title = item.Title,
                    Image = $"getfile?filename={item.Image}",
                    ImageProfile = $"getfile?filename={profile.ImageProfile}",
                    NameProfile = profile.FullName,
                    UploadTime = item.UploadTime,
                    IsVideo = FileHelper.IsVideo(item.Image)    

                }).ToList();
                return View(posts);
            }
            else
            {
                return RedirectToAction("ArtistCard");

            }
        }

        [Authorize]
        public IActionResult DeletePost(int id)
        {
            var idUser = aartistRepository.GetIdUserByUsurName(User.Identity.Name);
            var idArtist = aartistRepository.GetIdArtistByIdUser(idUser);
            aartistRepository.DeletePost(id, idArtist);
            return RedirectToAction("MyPost");
        }

        byte[] ResizeImage(byte[] btyesImage)
        {
            var jpegQuality = 50;
            Image image;
            using (var inputStream = new MemoryStream(btyesImage))
            {
                image = Image.FromStream(inputStream);
                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                  .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);
                Byte[] outputBytes;
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, jpegEncoder, encoderParameters);
                    return outputBytes = outputStream.ToArray();
                }
            }
        }

        private static async Task<string> UploadFile(IFormFile file)
        {
            var fileName = file.FileName;
            var filePath = Path.Combine(@"C:\filesBoking\", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return file.FileName;
        }

        public async Task<IActionResult> GetFile(string fileName)
        {
            var filePath = Path.Combine(@"C:\filesBoking\", fileName);
            // ודא שהקובץ קיים וניתן לגישה
            return PhysicalFile(filePath, FileHelper.GetMimeType(filePath), fileName);

        }
    }
}
