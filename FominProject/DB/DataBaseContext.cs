using FominProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FominProject.DB
{
    public class DataBaseContext : DbContext, IRepository
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        private DbSet<Page> Pages { get; set; }
        private DbSet<Author> Authors { get; set; }

        public void AddPage(Page page)
        {
            this.Pages.Add(page);
            this.SaveChanges();
        }

        public void DeletePage(Page page)
        {
            this.Pages.Remove(page);
            this.SaveChanges();
        }

        public List<Page> GetAllPages()
        {
            return this.Pages.ToList();
        }

        public List<Page> GetPublishedPagesList() {
            return this.Pages.Where(x => x.IsPublished).ToList();
        }

        public Page GetPageById(int pageId)
        {
            return this.Pages.Include(x => x.Author).SingleOrDefault(x => x.Id == pageId);
        }

        public List<Page> GetPagesByPageTitleContainsClause(string pageTitle)
        {
            if (pageTitle == string.Empty)
            {
                return this.Pages.ToList();
            }
            else
            {
                return this.Pages.Where(x => x.PageTitle.ToLower().Contains(pageTitle.ToLower())).ToList();
            }
        }

        public void UpdatePage(Page page)
        {
            this.Pages.Update(page);
            this.SaveChanges();
        }

        public bool PageExists(int pageId)
        {
            return this.Pages.Any(e => e.Id == pageId);
        }

        public List<Author> GetAllAuthors()
        {
            return this.Authors.ToList();
        }

        public Author GetAuthorById(int authorId)
        {
            return this.Authors.SingleOrDefault(x => x.Id == authorId);
        }

        public void UpdateAuthor(Author author)
        {
            this.Authors.Update(author);
            this.SaveChanges();
              
        }

        public void DeleteAuthor(Author author)
        {
            this.Authors.Remove(author);
            this.SaveChanges();
        }

        public void AddAuthor(Author author)
        {
            this.Authors.Add(author);
            this.SaveChanges();
        }

        public bool AuthorExists(int authorId)
        {
            return this.Pages.Any(e => e.Id == authorId);
        }
    }
}
