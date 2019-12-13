using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FominProject.DB;
using FominProject.Models;

namespace FominProject.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IRepository _repository;

        public AuthorsController(DataBaseContext context)
        {
            _repository = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(_repository.GetAllAuthors());
        }

        // GET:  Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _repository.GetAuthorById((int)id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                _repository.AddAuthor(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _repository.GetAuthorById((int)id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateAuthor(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _repository.GetAuthorById((int)id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = _repository.GetAuthorById(id);
            _repository.DeleteAuthor(author);
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _repository.AuthorExists(id);
        }
    }
}
