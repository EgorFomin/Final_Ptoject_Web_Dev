using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FominProject.Models
{
    public class Page
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string PageTitle { get; set; }

        [Required]
        public string PageBody { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Extra Feature #1

        [Required]
        public bool IsPublished { get; set; }     // Extra Feature #2

        [Required]
        public string PageStyle { get; set; }     // Extra Feature #4

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }        // Extra Feature #5

        public string GetPageBodyWithStyle()
        {
            return this.PageStyle + "\n" + this.PageBody;
        }
    }
}
