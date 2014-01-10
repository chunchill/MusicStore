using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Core.Models;

namespace MusicStore.Core.Factory
{
    public class OrderFactory
    {
        public static Order CreateOrder(User user)
        {
            var order = new Order
                            {
                                UserId = user.UserId, OrderDetails = new Collection<OrderDetail>()
                            };
            return order;
        }
    }
}
