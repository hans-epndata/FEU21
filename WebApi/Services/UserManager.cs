using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Data;
using WebApi.Models.Entities;

namespace WebApi.Services
{
    public class UserManager
    {
        private readonly DataContext _context;

        public UserManager(DataContext context)
        {
            _context = context;
        }

        public async Task<UserProfileEntity> CreateUserAsync(UserRequest req)
        {
            var userEntity = await CheckIfUserExists(req.Email);
            if (userEntity == null)
            {
                try
                {
                    // skapar en ny användare i Users
                    userEntity = new UserEntity { Id = Guid.NewGuid() };
                    userEntity.CreatePassword(req.Password);
                    _context.Add(userEntity);
                    await _context.SaveChangesAsync();

                    // skapar en ny användarprofil i UserProfiles
                    var userProfileEntity = new UserProfileEntity
                    {
                        UserId = userEntity.Id,
                        FirstName = req.FirstName,
                        LastName = req.LastName,
                        Email = req.Email
                    };
                    _context.Add(userProfileEntity);
                    await _context.SaveChangesAsync();

                    return userProfileEntity;
                }
                catch { }
            }

            return null;

        }

        private async Task<UserEntity> CheckIfUserExists(string email) 
        {
            // kolla om det redan finns en användare med samma e-postadress inlagd
            var userProfileEntity = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Email == email);
            if (userProfileEntity != null)
            {
                var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userProfileEntity.UserId);
                if (userEntity != null)
                    return userEntity;
            }

            return null;
        }
    }
}
