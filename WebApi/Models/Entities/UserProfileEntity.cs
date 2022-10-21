using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class UserProfileEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
