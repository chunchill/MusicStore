using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;
using NUnit.Framework;

namespace MusicStore.IntegrateTest.TDD
{
    public class MockTest
    {
        public readonly IRepository<Album> MockAlbumRepository;
        public readonly IRepository<Artist> MockArtistRepository;
        public readonly IRepository<Genre> MockGenreRepository;

        public MockTest()
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
            MockAlbumRepository = mockAlbumRepository.Object;
            MockArtistRepository = mockArtistRepository.Object;
            MockGenreRepository = mockGenreRepository.Object;


        }

        [Test]
        public void CheckTheTotalCount()
        {
            var geners = MockGenreRepository.Query(item => item.GenreId != 0);
            Assert.AreEqual(3, geners.Count());
            var artists = MockArtistRepository.Query(item => item.ArtistId != 0);
            Assert.AreEqual(2, artists.Count());
            var albums = MockAlbumRepository.Query(item => item.AlbumId != 0);
            Assert.AreEqual(2, albums.Count());
        }

        /// <summary>
        /// Can we return a Album By Id?
        /// </summary>
        [Test]
        public void CanReturnAlbumById()
        {
            // Try finding a product by id
            var testAlbum = this.MockAlbumRepository.GetById(2);
            Assert.IsNotNull(testAlbum); // Test if null
            Assert.AreEqual("You're Beautiful", testAlbum.Name); // Verify it is the right album
        }

        [Test]
        public void CreateAlbumWithoutArtistOrGener()
        {
            var aNewAlbum = new Album { GenreId = 0, ArtistId = 0 };
            var addedItem = MockAlbumRepository.Add(aNewAlbum);
            Assert.IsNotNull(addedItem);
        }

        [Test]
        public void CreateAlbumWithoutName()
        {
            var aNewAlbum = new Album { GenreId = 1, ArtistId = 1 };
            var addedItem = MockAlbumRepository.Add(aNewAlbum);
            Assert.IsNotNull(addedItem);
        }

        [Test]
        public void CreateAlbum()
        {
            var countBeforeAdding = MockAlbumRepository.Query(item => item.AlbumId != 0).Count();
            var aNewAlbum = new Album { GenreId = 1, ArtistId = 1, Name = "Just for Testing" };
            var addedItem = MockAlbumRepository.Add(aNewAlbum);
            var countAfterAdding = MockAlbumRepository.Query(item => item.AlbumId != 0).Count();
            Assert.IsNotNull(addedItem);
            Assert.AreEqual("Just for Testing", aNewAlbum.Name);
            Assert.AreEqual(countAfterAdding, countBeforeAdding + 1);

        }






    }
}
