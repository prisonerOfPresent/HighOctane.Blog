using HighOctane.Blog.Models;
using HighOctane.Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Helpers
{
    public class PostHelper
    {


        public ICategoryRepository CategoryRepository { get; }

        public ITagRepositiry TagRepositiry { get; }

        public PostHelper( ICategoryRepository categoryRepository, ITagRepositiry tagRepositiry ) 
        {
            CategoryRepository = categoryRepository;
            TagRepositiry = tagRepositiry;
        }

        public async Task<Post> GetPostFromViewModel(PostViewModel post) 
        {
            Post actualPost = new Post();
            actualPost.Author = post.Author;
            actualPost.CreatedDate = post.CreatedDate;
            actualPost.Content = post.Content;
            actualPost.Title = post.Title;
            actualPost.Slug = post.Slug;
            actualPost.Id = post.Id;
            actualPost.Excerpt = post.Excerpt;
            actualPost.UpdateTime = post.UpdateTime;
            actualPost.Category = CategoryRepository.GetById(post.CategoryID);
            if (post.Tags != null && post.Tags.Count > 0) 
            {
                actualPost.Tags = await GetTags( post.Tags );
            }
            return actualPost;
        }

        internal PostViewModel GetViewModelFromPost(Post post)
        {
            PostViewModel viewModel = new PostViewModel();
            viewModel.Id = post.Id;
            viewModel.Slug = post.Slug;
            viewModel.CategoryID = post.Category.Id;
            viewModel.CreatedDate = post.CreatedDate;
            viewModel.UpdateTime = post.UpdateTime;
            viewModel.Title = post.Title;
            viewModel.Excerpt = post.Excerpt;
            viewModel.Content = post.Content;
            if (post.Tags != null && post.Tags.Count > 0) 
            {
                List<string> tags = new List<string>();
                foreach (var tag in post.Tags) 
                {
                    tags.Add( tag.Name );
                }
                viewModel.Tags = tags;
            }

            return viewModel;
        }

        private async Task<List<Tag>> GetTags(List<string> tags)
        {
            List<Tag> tagList = new List<Tag>();
            foreach (var tagStr in tags) 
            {
                Tag tag = TagRepositiry.GetByName( tagStr );
                if (tag == null)
                {
                    tag = new Tag();
                    tag.Name = tagStr;
                    TagRepositiry.Add(tag);
                    bool saved = await TagRepositiry.SaveChangesAsync();
                    if (saved)
                        tag = TagRepositiry.GetByName(tagStr);
                }
                tagList.Add(tag);
            }

            return tagList;
        }
    }
}
