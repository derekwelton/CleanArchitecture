using System;

namespace Application.Common.Identity
{
    public interface IApplicationUser
    {
        Guid Id { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        String PasswordHash { get; set; }
        string NormalizedUserName { get; set; }
        string AuthenticationType { get; set; }
        bool IsAuthenticated { get; set; }
        string Name { get; set; }
    }
}