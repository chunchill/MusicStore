using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;

namespace MusicStore.Core.Services
{
    public class OrderService
    {
        private readonly IRepository<AddressBook> _addressBookRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<UserCredit> _userCreditRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, 
            IRepository<AddressBook> addressBookRepository, 
            IRepository<Order> orderRepository,
            IRepository<OrderDetail> orderDetailRepository,
            IRepository<User> userRepository,
            IRepository<Album> albumRepository, 
            IRepository<UserCredit> userCreditRepository)
        {
            _addressBookRepository = addressBookRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _albumRepository = albumRepository;
            _userCreditRepository = userCreditRepository;
        }

        #region AddressBook Actions
        public void CreateAddressBook(AddressBook addressBook)
        {
            using (_unitOfWork)
            {
                if (addressBook == null)
                    throw new InvalidDataException("could not save a null to addressbook");
                if (addressBook.UserId == 0 || !_userRepository.Query(u => u.UserId == addressBook.UserId).Any())
                    throw new InvalidDataException("the addressbook info should contain the user information");
                _addressBookRepository.Add(addressBook);
                _unitOfWork.Commit();
            }

        }

        public void DeleteAddressBooks(int[] addressBookIds)
        {
            for (int i = 0; i < addressBookIds.Length; i++)
            {
                int index = i;
                var addressBookToDeleted = _addressBookRepository.Query(ad => ad.AddressId == addressBookIds[index]).FirstOrDefault();
                if (addressBookToDeleted != null)
                {
                    _addressBookRepository.Delete(addressBookToDeleted);
                }
            }
            _addressBookRepository.Save();
        }

        public void UpdateAddressBook(AddressBook addressBook)
        {
            using (_unitOfWork)
            {
                if (addressBook == null)
                    throw new InvalidDataException("addressBook to be updated could not be null");
                var addressBookToUpdate = _addressBookRepository.Query(ad => ad.AddressId == addressBook.AddressId);
                if (addressBookToUpdate == null)
                    throw new InvalidDataException("addressBook to be updated does not exsit");
                if (addressBook.UserId == 0 || !_userRepository.Query(u => u.UserId == addressBook.UserId).Any())
                    throw new InvalidDataException("the user of the address does not exist");
                _addressBookRepository.Update(addressBook);
                _unitOfWork.Commit();
            }

        }

        public IList<AddressBook> GetAllAddressBooks(Expression<Func<AddressBook, bool>> criteria)
        {
            return _addressBookRepository.Query(criteria).ToList();
        }
        #endregion

        #region OrderDetail Actions
        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            using (_unitOfWork)
            {
                if (orderDetail == null)
                    throw new InvalidDataException("orderDetail to be created could not be null");
                if (orderDetail.AlbumId == 0 || !_albumRepository.Query(a => a.AlbumId == orderDetail.AlbumId).Any())
                    throw new InvalidDataException("please specify an album");
                if (orderDetail.OrderId == 0)
                    throw new InvalidDataException("the detail infomation should belong to an order");
                _orderDetailRepository.Add(orderDetail);
                _unitOfWork.Commit();
            }

        }

        public void DeleteOrderDetails(int[] orderDetailIds)
        {
            for (int i = 0; i < orderDetailIds.Length; i++)
            {
                int index = i;
                var orderDetailToDeleted = _orderDetailRepository.Query(orderDetail => orderDetail.OrderDetailId == orderDetailIds[index]).FirstOrDefault();
                if (orderDetailToDeleted != null)
                {
                    _orderDetailRepository.Delete(orderDetailToDeleted);
                }
            }
            _orderDetailRepository.Save();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            using (_unitOfWork)
            {
                if (orderDetail == null)
                    throw new InvalidDataException("orderDetail to be updated could not be null");
                var orderDetailToUpdate = _orderDetailRepository.Query(art => art.OrderDetailId == orderDetail.OrderDetailId);
                if (orderDetailToUpdate == null)
                    throw new InvalidDataException("orderDetail to be updated does not exsit");
                if (orderDetail.AlbumId == 0 || !_albumRepository.Query(a => a.AlbumId == orderDetail.AlbumId).Any())
                    throw new InvalidDataException("please specify an album");
                if (orderDetail.OrderId == 0)
                    throw new InvalidDataException("the detail infomation should belong to an order");
                _orderDetailRepository.Update(orderDetail);
                _orderDetailRepository.Save();
            }
        }

        public IList<OrderDetail> GetAllOrderDetails(Expression<Func<OrderDetail, bool>> criteria)
        {
            return _orderDetailRepository.Query(criteria).ToList();
        }
        #endregion

        #region Order Actions
        public void CreateOrder(Order order)
        {
            using (_unitOfWork)
            {
                if (!ValidateOrder(order))throw new InvalidDataException("Not validate Order");
                _orderRepository.Add(order);
                order.Status=OrderStatus.Handling;
                _unitOfWork.Commit();
            }
        }

        public bool ValidateOrder(Order order)
        {
            if (order.AddressBook == null) throw new InvalidDataException("The order is missing address book");
            if (!order.IsValidateAccordingToSize()) return false;
            var totalDebetAmout= _userCreditRepository.Query(uc => uc.UserId == order.AddressBook.UserId)
                .Sum(uc => uc.DebtAmount);
            var user = _userRepository.GetById(order.AddressBook.UserId);
            if (totalDebetAmout > user.TotalCredit)
                return false;
            return true;

        }

        public void DeleteOrders(int[] orderIds)
        {
            for (int i = 0; i < orderIds.Length; i++)
            {
                int index = i;
                var orderToDeleted = _orderRepository.Query(o => o.OrderId == orderIds[index]).FirstOrDefault();
                if (orderToDeleted != null)
                {
                    _orderRepository.Delete(orderToDeleted);
                }
            }
            _orderRepository.Save();
        }

        public void UpdateOrder(Order order)
        {
            using (_unitOfWork)
            {
                if (order == null)
                    throw new InvalidDataException("order to be updated could not be null");
                var orderToUpdate = _orderRepository.Query(o => o.OrderId == order.OrderId);
                if (orderToUpdate == null)
                    throw new InvalidDataException("order to be updated does not exsit");
                if (order.AddressId == 0 || !_addressBookRepository.Query(adr => adr.AddressId == order.AddressId).Any())
                    throw new InvalidDataException("please specify an address for the order");
                _orderRepository.Update(order);
                _unitOfWork.Commit();
            }
        }

        public IList<Order> GetAllOrders(Expression<Func<Order, bool>> criteria)
        {
            return _orderRepository.Query(criteria).ToList();
        }
        #endregion
    }
}
