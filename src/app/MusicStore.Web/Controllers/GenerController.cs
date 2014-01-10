using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Core.Interfaces;
using MusicStore.Core.Services;

namespace MusicStore.Web.Controllers
{
    public class GenerController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly InventoryService _inventoryService;

        public GenerController(IUnitOfWork db, InventoryService service)
        {
            _db = db;
            _inventoryService = service;
        }

        public ActionResult List()
        {
            var allGener = _inventoryService.GetAllGenres(g => g.GenreId != 0);
            return Json(allGener);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
