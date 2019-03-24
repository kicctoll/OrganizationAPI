using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserByEmailAsync(string email);
    }
}
