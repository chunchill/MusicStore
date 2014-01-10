using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Core.Factory;
using MusicStore.Core.Models;
using NUnit.Framework;

namespace MusicStore.IntegrateTest.TDD
{
    [TestFixture]
    public class Test1
    {
        [Test]
        public void CanCreateOrder()
        {
            var order= OrderFactory.CreateOrder(new User());
            Assert.IsNotNull(order);
        }
    }
}
