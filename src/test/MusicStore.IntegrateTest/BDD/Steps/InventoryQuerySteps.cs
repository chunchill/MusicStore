using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;
using MusicStore.Core.Services;
using Should;
using TechTalk.SpecFlow;

namespace MusicStore.IntegrateTest.BDD.Steps
{
    [Binding]
    public class InventoryQuerySteps
    {
        public readonly IRepository<Album> MockAlbumRepository;
        public readonly IRepository<Artist> MockArtistRepository;
        public readonly IRepository<Genre> MockGenreRepository;
        public readonly InventoryService MockInventoryService;
        public readonly IUnitOfWork MockUnitOfWork;

        public Genre SelectedGerne { get; set; }
        public Artist SelectedArtist { get; set; }
        public Album Result { get; set; }


        private void SetUp()
        {

        }

        public InventoryQuerySteps()
        {
            // create some mock Genre to play with
            List<Genre> genres = new List<Genre>
                {
                    new Genre {GenreId = 1,Name = "Pop",Description = "Pop Country"},
                    new Genre {GenreId = 1,Name = "Jazz",Description = "Ellison inherited and made full use of blues,jazz and black folklores"},
                    new Genre {GenreId = 1,Name = "Electronic",Description = "Electronic Music"},
                };
            // create some mock Genre to play with
            IList<Artist> artists = new List<Artist>
                {
                    new Artist {ArtistId = 1,Name = "Michael Jackson"},
                    new Artist {ArtistId = 2,Name = "Jame Blunt"},
                };
            // create some albums Genre to play with
            IList<Album> albums = new List<Album>
                {
                    new Album(){AlbumId = 1,ArtistId = 1,GenreId = 1,Name = "Beat It"},
                    new Album(){AlbumId = 2,ArtistId = 2,GenreId = 1,Name = "You're Beautiful"},
                };

            // Mock the Genre Repository using Moq
            var mockGenreRepository = new Mock<IRepository<Genre>>();
            // Mock the Artist Repository using Moq
            var mockArtistRepository = new Mock<IRepository<Artist>>();
            // Mock the Album Repository using Moq
            var mockAlbumRepository = new Mock<IRepository<Album>>();
            mockGenreRepository
                .Setup(g => g.Query(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns((Expression<Func<Genre, bool>> predicate) => genres.Where(predicate.Compile()));
            mockArtistRepository
                .Setup(a => a.Query(It.IsAny<Expression<Func<Artist, bool>>>()))
                .Returns((Expression<Func<Artist, bool>> predicate) => artists.Where(predicate.Compile()));
            mockAlbumRepository
                .Setup(a => a.Query(It.IsAny<Expression<Func<Album, bool>>>()))
                .Returns((Expression<Func<Album, bool>> predicate) => albums.Where(predicate.Compile()));
            // return a product by Id
            mockAlbumRepository
                .Setup(al => al.GetById(It.IsAny<int>())).
                Returns((int i) => albums.Single(x => x.AlbumId == i));
            mockAlbumRepository.
                Setup(mr => mr.Add(It.IsAny<Album>())).
                Returns(
                (Album target) =>
                {
                    if (artists.All(item => item.ArtistId != target.ArtistId))
                        throw new InvalidDataException("the artist is not in the system");
                    if (genres.All(item => item.GenreId != target.GenreId))
                        throw new InvalidDataException("the gener is not in the system");
                    if (string.IsNullOrEmpty(target.Name))
                        throw new InvalidDataException("Name is required");
                    target.AlbumId = albums.Count + 1;
                    albums.Add(target);
                    return target;
                });
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            MockAlbumRepository = mockAlbumRepository.Object;
            MockArtistRepository = mockArtistRepository.Object;
            MockGenreRepository = mockGenreRepository.Object;
            MockUnitOfWork = mockUnitOfWork.Object;
            MockInventoryService=new InventoryService(MockUnitOfWork,MockAlbumRepository,MockGenreRepository,MockArtistRepository);
        }
        

        [Given(@"I have entered '(.*)' as the genre's name")]
        public void GivenIHaveEnteredAsTheGenreSName(string p0)
        {
            SelectedGerne= MockGenreRepository.Query(g => g.Name == p0).FirstOrDefault();
        }

        [Given(@"I have entered '(.*)' as the artist's name")]
        public void GivenIHaveEnteredAsTheArtistSName(string p0)
        {
            SelectedArtist = MockArtistRepository.Query(ar => ar.Name == p0).FirstOrDefault();
        }

        [When(@"I excute getAllAlbums")]
        public void WhenIExcuteGetAllAlbums()
        {
            Result= MockInventoryService
                .GetAllAlbums(album => album.GenreId == SelectedGerne.GenreId 
                              && album.ArtistId == SelectedArtist.ArtistId)
                .FirstOrDefault();
        }

        [Then(@"the result should be '(.*)'")]
        public void ThenTheResultShouldBe(string p0)
        {
            Result.Name.ShouldEqual(p0);
        }
    }
}
