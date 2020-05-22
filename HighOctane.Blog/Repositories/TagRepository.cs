using HighOctane.Blog.Data;
using HighOctane.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Repositories
{
    public class TagRepository : ITagRepositiry
    {


        AppDbContext ctx;

        public TagRepository( AppDbContext ctx ) 
        {
            this.ctx = ctx;
        }

        public void Add( Tag entity )
        {
            ctx.Tags.Add( entity );
        }

        public void Delete( string id )
        {
            Tag tag = GetById(id);
            ctx.Tags.Remove(tag);
        }

        public IEnumerable<Tag> GetAll()
        {
            return ctx.Tags.AsEnumerable<Tag>();
        }

        public Tag GetById( string id )
        {
            return ctx.Tags.FirstOrDefault( tag => string.Equals(tag.Id,id) ) ;
        }

        public Tag GetByName(string name)
        {
            return ctx.Tags.FirstOrDefault( tag => string.Equals(tag.Name, name) );
        }

        public async Task<bool> SaveChangesAsync()
        {
            int changesSaved = await ctx.SaveChangesAsync();
            if (changesSaved > 0)
                return true;
            return false;
        }

        public void Update(Tag entity)
        {
            ctx.Tags.Update(entity);
        }
    }
}
