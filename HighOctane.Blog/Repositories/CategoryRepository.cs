using HighOctane.Blog.Data;
using HighOctane.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        AppDbContext Context { get; set; }


        public CategoryRepository(AppDbContext dbContext) 
        {
            this.Context = dbContext;
        }


        public void Add(Category entity)
        {
            this.Context.Categories.Add( entity );
        }

        public void Delete(string id)
        {
            Category category = GetById( id );
            this.Context.Categories.Remove( category );
        }

        public IEnumerable<Category> GetAll()
        {
            return this.Context.Categories.AsEnumerable<Category>();
        }

        public Category GetById(string id)
        {
            return this.Context.Categories.FirstOrDefault( category => string.Equals( category.Id, id ) );
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                int changesSaved = await Context.SaveChangesAsync();
                if (changesSaved > 0)
                    return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public void Update(Category entity)
        {
            Context.Categories.Update( entity );
        }
    }
}
