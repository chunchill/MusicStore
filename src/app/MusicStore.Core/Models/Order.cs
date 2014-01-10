using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Core.Models
{
    public enum OrderStatus
    {
        DeliveringGoods,
        Done,
        Handling,
        WaitingForPayment
    }

    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public AddressBook AddressBook { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public OrderStatus Status { get; set; }

        public decimal? Total
        {
            get { return OrderDetails.Sum(detail => detail.UnitPrice * detail.Quantity); }


        }
        public bool IsValidateAccordingToSize()
        {
            return true;
        }
    }
}
