using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FominProject.Models;
using FominProject.DB;

namespace FominProject.Controllers
{
    public class PagesController : Controller
    {
        private readonly IRepository _repository;

        public PagesController(DataBaseContext context)
        {
            _repository = context;
        }

        public IActionResult PagesList()
        {
            return View(_repository.GetPublishedPagesList());
        }

        public IActionResult Page(int id)
        {
            return View(_repository.GetPageById(id));
        }
    }
}
