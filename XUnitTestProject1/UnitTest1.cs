using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using TestLoginWithFacebook;
using TestLoginWithFacebook.Data;
using TestLoginWithFacebook.Services;
using Xunit;

namespace XUnitTestProject1
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(@"Server=tcp:serverdb65.database.windows.net,1433;Initial Catalog=MyDbLearn;Persist Security Info=False;User ID=yakov;Password=Kobi656565;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"),
                    ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }



    public class UnitTest1 : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public UnitTest1(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestDb()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {

                AartistRepository aartistRepository = new AartistRepository(context);
                var result = aartistRepository.SearchCity("tel");

                Assert.NotNull(context);
            }
        }
        [Fact]
        public void TestDb1()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {

                AartistRepository aartistRepository = new AartistRepository(context);
                var result = aartistRepository.AddArtist(new TestLoginWithFacebook.Data.ModelsData.Artist
                {
                    ArtistType = TestLoginWithFacebook.Data.ModelsData.Enums.ArtistType.DJ,
                    EventType = TestLoginWithFacebook.Data.ModelsData.Enums.EventType.AbBigEvent,
                    IdUser = "daba07b1-989b-4252-a5e2-a78f14621c43",
                    Price = 2850,


                });

                Assert.NotNull(context);
            }
        }

        [Fact]
        public void TestSearch()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {

                AartistRepository aartistRepository = new AartistRepository(context);

                var result = aartistRepository.SearchArtsit("tel aviv",
                     DateTime.Now,
                     TestLoginWithFacebook.Data.ModelsData.Enums.EventType.AbBigEvent,
                     TestLoginWithFacebook.Data.ModelsData.Enums.ArtistType.Singer);
                var result1 =  aartistRepository.GetCardsArtistByListId(result);
            }
        }
        [Fact]
        public void TestEditA()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {

                AartistRepository aartistRepository = new AartistRepository(context);
                var a = aartistRepository.GetArtitById(8);
                a.Price = 2012;
                aartistRepository.EditArtist(a);
                var a1 = aartistRepository.GetArtitById(8);
                Debug.WriteLine(a1);

            }
        }
        [Fact]
        public void TestEditDayOfWork()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {

                AartistRepository aartistRepository = new AartistRepository(context);
                var a = aartistRepository.GetByIdArtist(5);
                a.Friday = true;
                aartistRepository.EditDaysWork(a);
                var a1 = aartistRepository.GetByIdArtist(5);
                Assert.Equal<TestLoginWithFacebook.Data.ModelsData.DaysWork>(a, a1);

            }
        }
    }
}
         
