﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumFinal.Models.Forum;
using ForumFinal.Models.Post;
using ForumFinal.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicianForums.Data;
using MusicianForums.Data.Models;

namespace ForumFinal.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private static UserManager<ApplicationUser> _userManager;
        public PostController(IPost postService, IForum forumService, UserManager<ApplicationUser> userManager )
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies
            };

            return View(model);
        }


        public IActionResult Create(int forumId)
        {
            var forum = _forumService.GetById(forumId);

            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
 
            var post = BuildPostReplies(model, user);
            await _postService.Add(post);
            // TODO: User Rating management here...

            return RedirectToAction("Index", "Post", post.Id);

        }

        private Post BuildPostReplies(NewPostModel model, ApplicationUser user)
        {
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content
            });
          
        }



    }
}