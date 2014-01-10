using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;

namespace MusicStore.Core.Services
{
   public class InventoryService
   {
      private readonly IRepository<Album> _albumRepository;
      private readonly IRepository<Genre> _genreRepository;
      private readonly IRepository<Artist> _artistRepository;
      private readonly IUnitOfWork _unitOfWork;

      public InventoryService(IUnitOfWork unitOfWork, IRepository<Album> albumRepository, 
          IRepository<Genre> genreRepository, IRepository<Artist> artistRepository)
      {
         _unitOfWork = unitOfWork;
         _albumRepository = albumRepository;
         _genreRepository = genreRepository;
         _artistRepository = artistRepository;
      }

      #region Artist Actions
      public void CreateArtist(Artist artist)
      {
         _artistRepository.Add(artist);
         _artistRepository.Save();
      }

      public void DeleteArtists(int[] artistIds)
      {
         for (int i = 0; i < artistIds.Length; i++)
         {
            int index = i;
            var artistToDeleted = _artistRepository.Query(artist => artist.ArtistId == artistIds[index]).FirstOrDefault();
            if (artistToDeleted != null)
            {
               _artistRepository.Delete(artistToDeleted);
            }
         }
         _artistRepository.Save();
      }

      public void UpdateArtist(Artist artist)
      {
         if (artist == null)
            throw new InvalidDataException("artist to be updated could not be null");
         var artistToUpdate = _artistRepository.Query(art => art.ArtistId == artist.ArtistId);
         if (artistToUpdate == null)
            throw new InvalidDataException("artist to be updated does not exsit");
         _artistRepository.Update(artist);
         _artistRepository.Save();
      }

      public IList<Artist> GetAllArtists(Expression<Func<Artist, bool>> criteria)
      {
         return _artistRepository.Query(criteria).ToList();
      }
      #endregion

      #region Genre Actions
      public void CreateGenre(Genre genre)
      {
         _genreRepository.Add(genre);
         _genreRepository.Save();
      }

      public void DeleteGenre(int[] genreIds)
      {
         for (int i = 0; i < genreIds.Length; i++)
         {
            int index = i;
            var genreToDeleted = _genreRepository.Query(g => g.GenreId == genreIds[index]).FirstOrDefault();
            if (genreToDeleted != null)
            {
               _genreRepository.Delete(genreToDeleted);
            }
         }
         _genreRepository.Save();
      }

      public void UpdateGenre(Genre genre)
      {
         if (genre == null)
            throw new InvalidDataException("genre to be updated could not be null");
         var genreToUpdate = _genreRepository.Query(g => g.GenreId == genre.GenreId);
         if (genreToUpdate == null)
            throw new InvalidDataException("genre to be updated does not exsit");
         _genreRepository.Update(genre);
         _genreRepository.Save();
      }


      public IList<Genre> GetAllGenres(Expression<Func<Genre, bool>> criteria)
      {
         return _genreRepository.Query(criteria).ToList();
      }
      //TODO:
      #endregion

      #region Album Actions
      public void CreateAlbum(Album album)
      {
         using (_unitOfWork)
         {
            if (album == null)
               throw new InvalidDataException("Album to be updated could not be null");
            if (album.GenreId == 0 || !_genreRepository.Query(g => g.GenreId == album.GenreId).Any())
               throw new InvalidDataException("Please specify a correct genre for the album");
            if (album.ArtistId == 0 || !_albumRepository.Query(a => a.ArtistId == album.ArtistId).Any())
               throw new InvalidDataException("Please sepcify a correct artist for the album");
            _albumRepository.Add(album);
            _unitOfWork.Commit();
         }
      }

      public void DeleteAlbums(int[] albumIds)
      {
         for (int i = 0; i < albumIds.Length; i++)
         {
            int index = i;
            var albumToDeleted = _albumRepository.Query(a => a.AlbumId == albumIds[index]).FirstOrDefault();
            if (albumToDeleted != null)
            {
               _albumRepository.Delete(albumToDeleted);
            }
         }
         _albumRepository.Save();
      }

      public void UpdateAlbum(Album album)
      {
         using (_unitOfWork)
         {
            if (album == null)
               throw new InvalidDataException("Album to be updated could not be null");
            var albumToUpdate = _albumRepository.Query(a => a.AlbumId == album.AlbumId);
            if (album.GenreId == 0 || !_genreRepository.Query(g => g.GenreId == album.GenreId).Any())
               throw new InvalidDataException("Please specify a correct genre for the album");
            if (album.ArtistId == 0 || !_albumRepository.Query(a => a.ArtistId == album.ArtistId).Any())
               throw new InvalidDataException("Please sepcify a correct artist for the album");
            if (albumToUpdate == null)
               throw new InvalidDataException("Album to be updated does not exsit");
            _albumRepository.Update(album);
            _unitOfWork.Commit();
         }
        
      }

      public IList<Album> GetAllAlbums(Expression<Func<Album, bool>> criteria)
      {
         return _albumRepository.Query(criteria).ToList();
      }

      #endregion

   }
}
