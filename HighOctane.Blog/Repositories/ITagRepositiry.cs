using HighOctane.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Repositories
{
    public interface ITagRepositiry
    {
        public void Add( Tag entity);
        public void Update( Tag entity);
        public void Delete(string id);

        public Tag GetById(string id);

        public Tag GetByName( string name );

        public IEnumerable<Tag> GetAll();

        public Task<bool> SaveChangesAsync();
    }
}
