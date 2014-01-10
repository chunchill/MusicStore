using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Services;

namespace MusicStore.Web.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly ShoppingService _shoppingService;

        public ShoppingController(IUnitOfWork db,ShoppingService shoppingService)
        {
            _db = db;
            _shoppingService = shoppingService;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
