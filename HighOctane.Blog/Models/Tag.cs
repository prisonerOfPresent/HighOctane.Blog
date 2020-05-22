using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Models
{
    public class Tag
    {
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagID { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
