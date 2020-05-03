using HighOctane.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Repositories
{
    public interface IRepository
    {
        public void Add( Post entity );
        public void Update(Post entity );
        public void Delete( int id );

        public Post GetById( int id );

        public IEnumerable<Post> GetAll();

        public Task<bool> SaveChangesAsync();

    }
}
