using FominProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FominProject.DB
{
    public interface IRepository
    {
        List<Page> GetAllPages();
        List<Page> GetPagesByPageTitleContainsClause(string pageTitle);
        List<Page> GetPublishedPagesList();
        Page GetPageById(int pageId);
        void UpdatePage(Page page);
        void DeletePage(Page page);
        void AddPage(Page page);
        bool PageExists(int pageId);

        List<Author> GetAllAuthors();
        Author GetAuthorById(int authorId);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        void AddAuthor(Author author);
        bool AuthorExists(int authorId);
    }
}
