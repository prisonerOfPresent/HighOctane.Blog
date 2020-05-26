using HighOctane.Blog.Helpers;
using HighOctane.Blog.Models;
using HighOctane.Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Controllers
{
    public class PostController : Controller
    {
        IRepository repo { get; set; } 

        ICategoryRepository CategoryRepository { get; set; }

        ITagRepositiry TagRepositiry { get; set; }

        PostHelper postHelper;

        public PostController( IRepository repository, 
            ICategoryRepository categoryRepository,
            ITagRepositiry tagRepositiry,
            PostHelper helper ) 
        {
            this.repo = repository;
            this.CategoryRepository = categoryRepository;
            this.TagRepositiry = tagRepositiry;
            postHelper = helper;
        }

        public IActionResult ViewPost( string id ) 
        {
            Post post = repo.GetBySlug(id);
            return View( post );
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateOrEditPost( int? id ) 
        {
            ViewBag.Categories = CategoryRepository.GetAll().ToList();

            if (id is null)
            {
                IEnumerable<Tag> tags = TagRepositiry.GetAll().ToList();
                ViewBag.AvailableTags = tags;
                return View();
            }
            else
            {
                var post = repo.GetById((int)id);
                PostViewModel postViewModel = postHelper.GetViewModelFromPost(post);
                IEnumerable<Tag> tags = TagRepositiry.GetAll().ToList();
                ViewBag.Tags = postViewModel.Tags;
                ViewBag.AvailableTags = tags;
                return View(postViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditPost( PostViewModel post ) 
        {
            if (ModelState.IsValid)
            {
                if (post.Id == 0)
                {
                    post.Author = User.Identity.Name;
                    post.CreatedDate = DateTime.Now;

                    Post actualPost = await postHelper.GetPostFromViewModel(post);

                    repo.Add(actualPost);
                }
                else 
                {
                    if (post.CreatedDate == null)
                        post.CreatedDate = DateTime.Now;
                    post.UpdateTime = DateTime.Now;
                    post.Author = User.Identity.Name;
                    Post actualPost = await postHelper.GetPostFromViewModel(post);
                    repo.Update(actualPost);
                }
                bool changesSaved = await repo.SaveChangesAsync();
                if (changesSaved)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewData["PostResult"] = "Unable to create post.";
                }
            }
            else 
            {
                ViewData["ValidationResult"] = "Validation failed.";
                ViewData["ValidationResultColor"] = "red";

            }
            return View();
        }


        public async Task<IActionResult> DeletePost( int id ) 
        {
            repo.Delete( id );
            await repo.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
