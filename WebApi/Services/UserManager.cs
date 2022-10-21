using WebApi.Models.Data;

namespace WebApi.Services
{
    public class UserManager
    {
        private readonly DataContext _context;

        public UserManager(DataContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync()
    }
}
