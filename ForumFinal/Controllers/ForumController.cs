using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumFinal.Models.Forum;
using Microsoft.AspNetCore.Mvc;
using MusicianForums.Data;
using MusicianForums.Data.Models;

namespace ForumFinal.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        /// <summary>
        /// Returns View with list of all Forums.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var forums = _forumService
                .GetAll()
                .Select(forum => new ForumListingModel { 
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description
                });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }


        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);


        }

    }
}