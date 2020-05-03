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
        public PostController( IRepository repository ) 
        {
            this.repo = repository;
        }

        public IActionResult ViewPost( int id ) 
        {
            Post post = repo.GetById(id);
            return View( post );
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateOrEditPost( int? id ) 
        {
            if (id is null)
                return View();
            else 
            {
                var post = repo.GetById((int)id);
                return View( post );
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditPost( Post post ) 
        {
            if (ModelState.IsValid)
            {
                if (post.Id == 0)
                {
                    post.Author = User.Identity.Name;
                    post.CreatedDate = DateTime.Now;
                    repo.Add(post);
                }
                else 
                {
                    if (post.CreatedDate == null)
                        post.CreatedDate = DateTime.Now;
                    post.UpdateTime = DateTime.Now;
                    post.Author = User.Identity.Name;
                    repo.Update(post);
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
