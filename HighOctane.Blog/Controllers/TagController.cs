using HighOctane.Blog.Models;
using HighOctane.Blog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Controllers
{
    public class TagController : Controller
    {

        IRepository _postRepository;
        ITagRepositiry _tagRepository;

        public TagController( IRepository repository, ITagRepositiry tagRepositiry ) 
        {
            _postRepository = repository;
            _tagRepository = tagRepositiry;
        }

        [HttpGet]
        public IActionResult CreateOrEditTag( string id ) 
        {
            if (id == null)
                return View();
            else 
            {
                Tag tag = _tagRepository.GetById(id);
                return View(tag);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditTag( Tag tag ) 
        {
            if (ModelState.IsValid) 
            {
                _tagRepository.Add(tag);
                bool changesSaved = await _tagRepository.SaveChangesAsync();
                if (changesSaved)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public IActionResult ViewTags() 
        {
            IEnumerable<Tag> tags = _tagRepository.GetAll().ToList();
            return View( tags );
        }

        public IActionResult ViewTag( string id ) 
        {
            Tag tag = _tagRepository.GetById(id);
            if (tag != null)
            {
                IEnumerable<Post> posts = _postRepository.GetAllByTag( tag ).ToList();
                ViewBag.Posts = posts;
                return View(tag);
            }
            else 
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> DeleteTag( string id )
        {
            _tagRepository.Delete(id);
            bool deleted = await _tagRepository.SaveChangesAsync();
            if (deleted)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

    }
}
