﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HighOctane.Blog.Models
{
    public class Post
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual List<Tag> Tags { get; set; }

        public virtual Category Category { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Excerpt { get; set; }

        public string GetSlug( string text ) 
        {
          
            return Regex
                .Replace(text, "/^[a - z0 - 9] +(?:-[a - z0 - 9] +)* $/","-", RegexOptions.IgnoreCase);
        }

    }
}
