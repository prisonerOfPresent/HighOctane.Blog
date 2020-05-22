using HighOctane.Blog.Data;
using HighOctane.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Repositories
{
    public class PostRepository : IRepository
    {
        AppDbContext ctx { get; set; }


        public PostRepository( AppDbContext ctx ) 
        {
            this.ctx = ctx;
        }


        public void Add(Post entity)
        {
            ctx.Posts.Add( entity );
        }

        public void Delete(int id)
        {
            Post post = GetById(id);
            ctx.Posts.Remove(post);
        }

        public IEnumerable<Post> GetAll()
        {
            return ctx.Posts.AsEnumerable<Post>();
        }

        public Post GetById(int id)
        {
            return ctx.Posts.FirstOrDefault(post => post.Id == id);
        }

        public Post GetBySlug( string slug )
        {
            return ctx.Posts.FirstOrDefault( post => string.Equals( post.Slug, slug ) );
        }

        public void Update(Post entity)
        {
            ctx.Posts.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            int changesSaved = await ctx.SaveChangesAsync();
            if (changesSaved > 0)
                return true;

            return false;
        }

        public IEnumerable<Post> GetAllByCategory(Category category)
        {
            var posts = from post in ctx.Posts
                        where post.Category.Id == category.Id
                        select post;
            return posts.AsEnumerable();
        }

        public IEnumerable<Post> GetAllByTag(Tag tag)
        {
            var posts = from post in ctx.Posts
                        where post.Tags.Contains(tag)
                        select post;
            return posts;
        }
    }
}
