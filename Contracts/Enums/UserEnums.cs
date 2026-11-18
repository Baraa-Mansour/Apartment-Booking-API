namespace Contracts.Enums
{
    public enum UserRoles
    {
        Admin,
        Owner,
        Tenant
    }

    public enum UserStatus
    {
        Pending,
        Approved, 
        Rejected, 
        Suspended
    }

    public enum UserLanguage
    {
        Ar,
        En
    }

    public enum UserThemes
    {
        Light,
        Dark
    }
}
