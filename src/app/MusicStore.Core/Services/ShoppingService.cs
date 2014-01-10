using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;

namespace MusicStore.Core.Services
{
    public class ShoppingService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Album> _albumRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService UnderlyingOrderService { get; set; }
        public InventoryService UnderlyingInventoryService { get; set; }

        public ShoppingService(IUnitOfWork unitOfWork, IRepository<Album> albumRepository, IRepository<Cart> cartRepository)
        {
            _unitOfWork = unitOfWork;
            _albumRepository = albumRepository;
            _cartRepository = cartRepository;
        }

        #region Cart Actions

        public void CompleteCheckout(Cart cart)
        {
            if (cart == null) throw new ArgumentNullException("cart");
            EnsureProductsInStock(cart);
            GenerateOrder(cart);
        }

        private void GenerateOrder(Cart cart)
        {

        }

        private void EnsureProductsInStock(Cart cart)
          {

          }

        public void CreateCart(Cart cart)
        {
            using (_unitOfWork)
            {
                if (cart == null)
                    throw new InvalidDataException("the cart could be null");
                if (cart.AlbumId == 0 || !_albumRepository.Query(a => a.AlbumId == cart.AlbumId).Any())
                    throw new InvalidDataException("please pick up an album to the cart");
                _cartRepository.Add(cart);
                _unitOfWork.Commit();
            }

        }

        public void DeleteCarts(int[] cartIds)
        {
            for (int i = 0; i < cartIds.Length; i++)
            {
                int index = i;
                var cartToDeleted = _cartRepository.Query(cart => cart.RecordId == cartIds[index]).FirstOrDefault();
                if (cartToDeleted != null)
                {
                    _cartRepository.Delete(cartToDeleted);
                }
            }
            _unitOfWork.Commit();
        }

        public void UpdateCart(Cart cart)
        {
            using (_unitOfWork)
            {
                if (cart == null)
                    throw new InvalidDataException("cart to be updated could not be null");
                var cartToUpdate = _cartRepository.Query(art => art.CartId == cart.CartId);
                if (cartToUpdate == null)
                    throw new InvalidDataException("cart to be updated does not exsit");
                _cartRepository.Update(cart);
                if (cart.AlbumId == 0 || !_albumRepository.Query(a => a.AlbumId == cart.AlbumId).Any())
                    throw new InvalidDataException("please pick up an album to the cart");
                _unitOfWork.Commit();
            }

        }

        public IList<Cart> GetAllCarts(Expression<Func<Cart, bool>> criteria)
        {
            return _cartRepository.Query(criteria).ToList();
        }
        #endregion
    }
}
