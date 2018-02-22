using Microsoft.EntityFrameworkCore;
using QueryExample.Entities;
using QueryExample.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryExample
{
    public class Repository : IRepository
    {
        private readonly QueryExampleContext _context;

        public Repository(QueryExampleContext context)
        {
            _context = context;
        }

        public Task<List<Competition>> GetFriendsPlayedCompetitiionsAsync(int userId)
        {
            return _context.Competitions
                .Include(c => c.Quest)
                .Include(c => c.Quest).ThenInclude(q => q.Author)
                .Include(c => c.Quest).ThenInclude(q => q.QuestTags).ThenInclude(qt => qt.Tag)
                .Where(c => c.Passes.Any(qp => qp.User.UserFriends.Any(uf => uf.FriendId == userId)))
                .OrderByDescending(c => c.Passes
                    .Where(qp => qp.User.UserFriends.Any(uf => uf.FriendId == userId))
                    .Max(qp => qp.CreatedAt))
                .Skip(1)
                .Take(3)
                .ToListAsync();
        }
    }
}
