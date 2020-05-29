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
            Post actualPost = new Post
            {
                Author = post.Author,
                CreatedDate = post.CreatedDate,
                Content = post.Content,
                Title = post.Title,
                Slug = post.Slug,
                Id = post.Id,
                Excerpt = post.Excerpt,
                UpdateTime = post.UpdateTime,
                Category = CategoryRepository.GetById(post.CategoryID)
            };
            if (post.Tags != null && post.Tags.Count > 0) 
            {
                actualPost.Tags = await GetTags( post.Tags );
            }
            return actualPost;
        }

        internal PostViewModel GetViewModelFromPost(Post post)
        {
            PostViewModel viewModel = new PostViewModel
            {
                Id = post.Id,
                Slug = post.Slug,
                CategoryID = post.Category.Id,
                CreatedDate = post.CreatedDate,
                UpdateTime = post.UpdateTime,
                Title = post.Title,
                Excerpt = post.Excerpt,
                Content = post.Content
            };
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

            List<string> actualTags = tags[0].Split(",").ToList();
            if (actualTags != null && actualTags.Count > 0)
            {
                List<Tag> tagList = new List<Tag>();
                foreach (var tagStr in actualTags)
                {
                    Tag tag = TagRepositiry.GetByName(tagStr);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagStr
                        };
                        TagRepositiry.Add(tag);
                        bool saved = await TagRepositiry.SaveChangesAsync();
                        if (saved)
                            tag = TagRepositiry.GetByName(tagStr);
                    }
                    tagList.Add(tag);
                }

                return tagList;
            }
            return null;
        }
    }
}
