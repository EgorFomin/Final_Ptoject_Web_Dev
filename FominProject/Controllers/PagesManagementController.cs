using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FominProject.DB;
using FominProject.Models;

namespace FominProject.Controllers
{
    public class PagesManagementController : Controller
    {
        private readonly IRepository _repository;

        public PagesManagementController(DataBaseContext context)
        {
            _repository = context;
        }

        // GET: Pages
        public async Task<IActionResult> Index()
        {
            return View(_repository.GetAllPages());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string pageTitle)
        {
            return View(_repository.GetPagesByPageTitleContainsClause(pageTitle));
        }

        // GET: Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _repository.GetPageById((int)id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Pages/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_repository.GetAllAuthors(), "Id", "FirstName");
            return View();
        }

        // POST: Pages/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,PageTitle,PageBody,IsPublished,PageStyle,AuthorId")] Page page)
        {
            if (ModelState.IsValid)
            {
                _repository.AddPage(page);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_repository.GetAllAuthors(), "Id", "FirstName", page.AuthorId);
            return View(page);
        }

        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _repository.GetPageById((int)id);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_repository.GetAllAuthors(), "Id", "FirstName", page.AuthorId);
            return View(page);
        }

        // POST: Pages/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PageTitle,PageBody,CreatedDate,IsPublished,PageStyle,AuthorId")] Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdatePage(page);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_repository.GetAllAuthors(), "Id", "FirstName", page.AuthorId);
            return View(page);
        }

        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _repository.GetPageById((int)id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = _repository.GetPageById(id);
            _repository.DeletePage(page);
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _repository.PageExists(id);
        }
    }
}
