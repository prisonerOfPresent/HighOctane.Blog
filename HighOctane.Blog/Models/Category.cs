using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Models
{
    public class Category
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided.")]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(AllowEmptyStrings =false,ErrorMessage ="Description must be provided.")]
        public string Description { get; set; }
    }
}
