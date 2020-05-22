using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        public List<string> Tags { get; set; }

        public string CategoryID { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Excerpt { get; set; }
    }
}
