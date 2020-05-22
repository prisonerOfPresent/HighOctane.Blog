using HighOctane.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Repositories
{
    public interface ICategoryRepository
    {
        public void Add(Category entity);
        public void Update(Category entity);
        public void Delete(string id);

        public Category GetById(string id);

        public IEnumerable<Category> GetAll();

        public Task<bool> SaveChangesAsync();
    }
}
