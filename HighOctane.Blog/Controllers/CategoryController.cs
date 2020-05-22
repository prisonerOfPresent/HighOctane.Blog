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
    public class CategoryController : Controller
    {
        ICategoryRepository _repository;
        IRepository _postRepo;

        public CategoryController( ICategoryRepository repository, IRepository postRepo ) 
        {
            this._repository = repository;
            this._postRepo = postRepo;
        }



        [HttpGet]
        public IActionResult CreateOrEditCategory( string id )
        {
            if (id == null)
                return View();
            else 
            {
                Category category = _repository.GetById(id);
                return View(category);
            }
        }

        public IActionResult ViewCategoryList() 
        {
            IEnumerable<Category> categories = _repository.GetAll();
            return View( categories );
        }


        public IActionResult ViewCategory( string id ) 
        {
            Category category = _repository.GetById( id );
            IEnumerable<Post> postsUnderCategory = _postRepo.GetAllByCategory( category );

            ViewBag.Posts = postsUnderCategory.ToList();

            return View( category );
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrEditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    _repository.Add(category);
                }
                else
                {
                    _repository.Update(category);
                }
                bool changesSaved = await _repository.SaveChangesAsync();
                if (changesSaved)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewData["CategoryResult"] = "Unable to create category.";
                }
            }
            else
            {
                ViewData["ValidationResult"] = "Validation failed.";
                ViewData["ValidationResultColor"] = "red";

            }
            return View();
        }


        public async Task<IActionResult> DeleteCategory( string id ) 
        {
            _repository.Delete(id);
            bool deleted = await _repository.SaveChangesAsync();
            if (deleted)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

    }
}
