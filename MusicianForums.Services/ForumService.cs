using ForumFinal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicianForums.Data;
using MusicianForums.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicianForums.Services
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;
        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all Forums with their associated posts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Forum> GetAll()
        {

            return _context.Forums
                .Include(forum => forum.Posts);
        }

        public IEnumerable<IdentityUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a single Forum corresponding to the Id passed 
        /// in as a parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(forum => forum.Id == id)
                .Include(forum => forum.Posts).ThenInclude(post => post.User)
                .Include(forum => forum.Posts).ThenInclude(post => post.Replies)
                .ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
