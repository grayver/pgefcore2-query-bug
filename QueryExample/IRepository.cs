using QueryExample.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueryExample
{
    public interface IRepository
    {
        Task<List<Competition>> GetFriendsPlayedCompetitiionsAsync(int userId);
    }
}
