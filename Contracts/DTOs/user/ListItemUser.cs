using Contracts.Enums;

namespace Contracts.DTOs.user
{
    public class ListItemUser
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public UserRoles Role {  get; set; }
        public UserStatus Status { get; set; }
        public string ProfilePhotoUrls { get; set; } = null!;
    }
}
