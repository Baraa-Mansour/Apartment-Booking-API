using Contracts.Enums;

namespace Contracts.DTOs.user
{
    public class UserFilterParams : PaginationParams
    {
        public UserStatus? Status { get; set; }
        public UserRoles? Role {  get; set; }
        public bool? IsDeleted { get; set; }
    }
}
