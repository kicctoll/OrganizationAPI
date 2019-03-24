using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context) { }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
