using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingArtistMvcCore.Data.ModelsData;
using Microsoft.AspNetCore.Identity;
using static BookingArtistMvcCore.Data.ModelsData.Enums;

namespace BookingArtistMvcCore.Services
{
    public interface IAartistRepository
    {

        public string UserName { get; set; }
        public List<Citys> SearchCity(string citySearch);


        /////////////////--Add--////////////////////////
        public int AddArtist(Artist artist);
        public void AddDaysWork(DaysWork daysWork);
        public void AddCictyWorks(List<Citys> citys, int IdArtis);
        public void Add(ProfileArtist profileArtist);
        public void AddUserClaims(string claimType, string claimValue, string idUser);
        public void AddProfileArtist(ProfileArtist profileArtist);
        public void AddPost(Post post);
        public void AddClient(Client client);
        public void AddOrder(Orders orders);

        //////////////////--Edit---//////////////////////
        public void EditCictyWorks(List<Citys> citys, int IdArtis);
        public void EditCictyWorksVerson1(List<CictyWorks> Works);
        public void EditDaysWork(DaysWork daysWork);
        public int EditArtist(Artist artist);
        public void EditProfileArtist(ProfileArtist profileArtist);
        public void EditPost(Post post);
        ///////////////////--delete--////////////////////
        public void DeleteCityWork(int id);
        public void DeleteAllCityWorkByIdArtist(int id);
        public void DeleteProfileArtist(ProfileArtist profileArtist);
        public void DeletePost(int id, int idArtist);
        /////////////////---Get---/////////////////
        public string GetIdUserByUsurName(string UserName);
        public int GetIdArtistByIdUser(string idUser);
        public int GetIdArtistByUserNmae(string UserName);
        public Artist GetArtitById(int id);
        public Artist GetArtitByIdUser(string idUser);
        public Artist GetArtitByUserNmae(string userName);
        public DaysWork GetByIdArtist(int id);
        public List<CictyWorks> GetCictyWorksByArtist(int id);
        public List<Citys> GetCitysByIds(List<int> ids);
        public List<int> SearchArtsit(string city, DateTime TimeEvent, EventType eventType, ArtistType artistType);
        public List<ArtistCard> GetCardsArtistByListId(List<int> ids);
        public ArtistCard GetCardArtitById(int id);
        public List<object> GetArtsitCardsByIdis(List<int> idArtsits);
        public bool IfArtisCreated(string idUser);
        public ProfileArtist GetProfileArtistByIdAtris(int idAtris);
        public Post GetPostById(int id, int idArtist);
        public List<Post> GetPostByIdArtist(int idArtist);
        public List<Post> GetAllPost();

        public bool IfClientExists(string idUser);
        public Client GetClient(string idUser);
        public IdentityUser GetIdentityUser(string idUser);
        public IdentityUser GetIdentityUserByUsurName(string userNmae);

        public List<Orders> GetOrdersByClent(int id);
        public List<Orders> GetOrdersByArtsist(int id);
        public string GetCityById(int id);
    }
}
