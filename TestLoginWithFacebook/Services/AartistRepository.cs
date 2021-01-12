using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingArtistMvcCore.Data;
using BookingArtistMvcCore.Data.ModelsData;
using static BookingArtistMvcCore.Data.ModelsData.Enums;

namespace BookingArtistMvcCore.Services
{
    public class AartistRepository : IAartistRepository
    {
        public string UserName { get; set; }
        ApplicationDbContext applicationDbContext;
        public AartistRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public int AddArtist(Artist artist)
        {

            // artist.IdUser = GetIdUserByUsurName(UserName);
            if (artist.IdUser != "")
            {
                try
                {
                    var art = applicationDbContext.Artists.Add(artist).Entity;
                    applicationDbContext.SaveChanges();
                    return artist.Id;
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return 0;
        }

        public void AddCictyWorks(List<Citys> citys, int IdArtis)
        {
            foreach (var city in citys)
            {
                applicationDbContext.CictyWorks.Add(new CictyWorks
                {
                    IdArtis = IdArtis,
                    IdCity = city.Id
                });
            }
            applicationDbContext.SaveChanges();
        }

        public void AddDaysWork(DaysWork daysWork)
        {
            applicationDbContext.DaysWorks.Add(daysWork);
            applicationDbContext.SaveChanges();
        }

        public int EditArtist(Artist artist)
        {
            var a = applicationDbContext.Artists.Where(a => a.Id == artist.Id).FirstOrDefault();
            a = artist;
            applicationDbContext.SaveChanges();
            return a.Id;
        }

        public void EditCictyWorks(List<Citys> citys, int IdArtis)
        {
            List<CictyWorks> newCitys = (citys.Select(item => new CictyWorks
            {
                IdArtis = IdArtis,
                IdCity = item.Id
            })).ToList();

            var citysDb = applicationDbContext.CictyWorks.Where(a => a.IdArtis == IdArtis).ToList();
            citysDb = newCitys;
            applicationDbContext.SaveChanges();

        }
        public void EditCictyWorksVerson1(List<CictyWorks> Works)
        {
            var citysDb = applicationDbContext.CictyWorks.Where(a => a.IdArtis == Works[0].IdArtis).ToList();
            citysDb = Works;
            applicationDbContext.SaveChanges();
        }

        public void EditDaysWork(DaysWork daysWork)
        {
            var d = applicationDbContext.DaysWorks.Where(a => a.IdArtit == daysWork.IdArtit).FirstOrDefault();
            d = daysWork;
            applicationDbContext.SaveChanges();
        }

        public List<object> GetArtsitCardsByIdis(List<int> idArtsits)
        {
            throw new NotImplementedException();
        }

        public Artist GetArtitById(int id)
        {
            return applicationDbContext.Artists.Where(a => a.Id == id).FirstOrDefault();
        }

        public DaysWork GetByIdArtist(int id)
        {
            return applicationDbContext.DaysWorks.Where(d => d.IdArtit == id).FirstOrDefault();
        }

        public List<CictyWorks> GetCictyWorksByArtist(int id)
        {
            return applicationDbContext.CictyWorks.Where(c => c.IdArtis == id).ToList();
        }

        public string GetIdUserByUsurName(string UserName)
        {
            var n = applicationDbContext.Users.
                    Where(a => a.UserName == UserName).FirstOrDefault();

            // var c = applicationDbContext.UserClaims.Where(a => a.UserId == n.Id).FirstOrDefault();
            return n.Id;
        }

        public List<int> SearchArtsit(string city, DateTime timeEvent, Enums.EventType eventType, Enums.ArtistType artistType)
        {
            int idCity = applicationDbContext.Citys.Where(c => c.City == city).Select(c => c.Id).FirstOrDefault();
            var idArtitsResultOfCitys = applicationDbContext.CictyWorks.Where(c => c.IdCity == idCity).Select(i => i.IdArtis).ToList();
            string daySelect = timeEvent.DayOfWeek.ToString();
            var idArtitsResultOfDay = applicationDbContext.DaysWorks.FromSqlRaw($"select * from DaysWorks where {daySelect} = 1").Select(a => a.IdArtit).ToList();


            return applicationDbContext.Artists.Where(i => idArtitsResultOfCitys.Any(b => b == i.Id) && idArtitsResultOfDay.Any(b => b == i.Id)).
                Where(a => (ArtistType)a.ArtistType == artistType &&
             (EventType)a.EventType == eventType).Select(a => a.Id).ToList();
        }

        public List<Citys> SearchCity(string citySearch)
        {
            return applicationDbContext.Citys.Where(c => c.City.ToLower().Contains(citySearch.ToLower())).
                   OrderBy(c => c.City).ToList();
        }

        public int GetIdArtistByIdUser(string idUser)
        {
            try
            {

                int id = applicationDbContext.Artists.Where(a => a.IdUser == idUser).Select(a => a.Id).FirstOrDefault();
                return id > 0 ? id : 0;
            }
            catch (Exception ex)
            {

                var dataEx = new Exception("not defaund", ex);
                throw (dataEx);
            }
        }

        public Artist GetArtitByIdUser(string idUser)
        {
            return applicationDbContext.Artists.Where(a => a.IdUser == idUser).FirstOrDefault();

        }

        public Artist GetArtitByUserNmae(string userName)
        {


            return GetArtitByIdUser(GetIdUserByUsurName(userName));

        }

        public List<Citys> GetCitysByIds(List<int> ids)
        {
            return (ids.Select(item => applicationDbContext.Citys.Where(a => a.Id == item).FirstOrDefault())).ToList();
        }

        public bool IfArtisCreated(string idUser)
        {
            return GetIdArtistByIdUser(idUser) > 0 ? true : false;

        }

        public void DeleteCityWork(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllCityWorkByIdArtist(int id)
        {
            var cw = GetCictyWorksByArtist(id);
            applicationDbContext.CictyWorks.RemoveRange(cw);
            applicationDbContext.SaveChanges();

        }

        public void AddUserClaims(string claimType, string claimValue, string idUser)
        {
            applicationDbContext.UserClaims.Add(new Microsoft.AspNetCore.Identity.IdentityUserClaim<string>
            {
                ClaimType = claimType,
                ClaimValue = claimValue,
                UserId = idUser
            });

            applicationDbContext.SaveChanges();

        }

        public void Add(ProfileArtist profileArtist)
        {
            applicationDbContext.ProfileArtists.Add(profileArtist);
            applicationDbContext.SaveChanges();

        }

        public void DeleteProfileArtist(ProfileArtist profileArtist)
        {
            applicationDbContext.ProfileArtists.Remove(profileArtist);
            applicationDbContext.SaveChanges();

        }

        public ProfileArtist GetProfileArtistByIdAtris(int idAtris)
        {
            return applicationDbContext.ProfileArtists.Where(a => a.IdArtit == idAtris).FirstOrDefault();
        }

        public void EditProfileArtist(ProfileArtist profileArtist)
        {
            var p = applicationDbContext.ProfileArtists.Where(a => a.IdArtit == profileArtist.IdArtit).FirstOrDefault();
            p = profileArtist;
            applicationDbContext.SaveChanges();
        }

        public int GetIdArtistByUserNmae(string UserName)
        {
          
            throw new NotImplementedException();
        }

        public void AddProfileArtist(ProfileArtist profileArtist)
        {
            applicationDbContext.ProfileArtists.Add(profileArtist);
            applicationDbContext.SaveChanges();

        }

        public List<ArtistCard> GetCardsArtistByListId(List<int> ids)
        {
           List<ArtistCard> cardsArtist =  applicationDbContext.Artists.Where(a => ids.Any(i=>i == a.Id)).Join(applicationDbContext.ProfileArtists,
                a => a.Id,
                p => p.IdArtit,
                (aa, pp) => new ArtistCard
                {
                    Id = aa.Id,
                    Price = aa.Price,
                    Image = pp.ImageProfile,
                    FullName = pp.FullName,
                    Description = pp.About,

                }
                ).ToList();
            return cardsArtist;
        }

        public void AddPost(Post post)
        {
            applicationDbContext.Posts.Add(post);
            applicationDbContext.SaveChanges();
        }

        public void EditPost(Post post)
        {
            var p = applicationDbContext.Posts.Where(p => p.Id == post.Id).FirstOrDefault();
            p = post;
            applicationDbContext.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var p = GetPostById(id);
            applicationDbContext.Posts.Remove(p);
        }

        public Post GetPostById(int id)
        {
            return applicationDbContext.Posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Post> GetPostByIdArtist(int idArtist)
        {
            return applicationDbContext.Posts.Where(p => p.idArtist == idArtist).ToList();
        }

        public List<Post> GetAllPost()
        {
            return applicationDbContext.Posts.ToList();

        }
    }
}
