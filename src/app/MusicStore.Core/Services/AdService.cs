using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;

namespace MusicStore.Core.Services
{
   public class AdService
   {
      private readonly IRepository<News> _newsRepository;
      private readonly IRepository<NewsType> _newsTypeRepository;
      private readonly IUnitOfWork _unitOfWork;

      public AdService(IUnitOfWork unitOfWork, IRepository<News> newsRepository, IRepository<NewsType> newsTypeRepository)
      {
         _newsRepository = newsRepository;
         _newsTypeRepository = newsTypeRepository;
         _unitOfWork = unitOfWork;
      }

      #region News Actions
      public void CreateNews(News news)
      {
         using (_unitOfWork)
         {
            if (news.NewsTypeId == 0 || !_newsTypeRepository.Query(nt => nt.NewsTypeId == news.NewsTypeId).Any())
               throw new InvalidDataException("the type of the news is not correct!");
            _newsRepository.Add(news);
            _unitOfWork.Commit();
         }
      }

      public void DeleteNews(int[] newsIds)
      {
         for (int i = 0; i < newsIds.Length; i++)
         {
            int index = i;
            var newsToDeleted = _newsRepository.Query(n => n.NewsId == newsIds[index]).FirstOrDefault();
            if (newsToDeleted != null)
            {
               _newsRepository.Delete(newsToDeleted);
            }
         }
         _newsRepository.Save();
      }

      public void UpdateNews(News news)
      {
         using (_unitOfWork)
         {
            if (news == null)
               throw new InvalidDataException("news to be updated could not be null");
            var newsToUpdate = _newsRepository.Query(n => n.NewsId == news.NewsId);
            if (newsToUpdate == null)
               throw new InvalidDataException("news to be updated does not exsit");
            if (!_newsTypeRepository.Query(nt => nt.NewsTypeId == news.NewsTypeId).Any())
               throw new InvalidDataException("news type to be updated could not be null");
            _newsRepository.Update(news);
            _unitOfWork.Commit();
         }
      }


      public IList<News> GetAllNews(Expression<Func<News, bool>> criteria)
      {
         return _newsRepository.Query(criteria).ToList();
      }
      //TODO:
      #endregion

      #region NewsType Actions
      public void CreateNewsType(NewsType newsType)
      {
         _newsTypeRepository.Add(newsType);
         _newsTypeRepository.Save();
      }

      public void DeleteNewsType(int[] newsTypeIds)
      {
         for (int i = 0; i < newsTypeIds.Length; i++)
         {
            int index = i;
            var newsTypeToDeleted = _newsTypeRepository.Query(nt => nt.NewsTypeId == newsTypeIds[index]).FirstOrDefault();
            if (newsTypeToDeleted != null)
            {
               _newsTypeRepository.Delete(newsTypeToDeleted);
            }
         }
         _newsTypeRepository.Save();
      }

      public void UpdateNewsType(NewsType newsType)
      {
         if (newsType == null)
            throw new InvalidDataException("newsType to be updated could not be null");
         var newsTypeToUpdate = _newsTypeRepository.Query(nt => nt.NewsTypeId == newsType.NewsTypeId);
         if (newsTypeToUpdate == null)
            throw new InvalidDataException("newsType to be updated does not exsit");
         _newsTypeRepository.Update(newsType);
         _newsTypeRepository.Save();
      }


      public IList<NewsType> GetAllNewsTypes(Expression<Func<NewsType, bool>> criteria)
      {
         return _newsTypeRepository.Query(criteria).ToList();
      }
      //TODO:
      #endregion
   }
}
