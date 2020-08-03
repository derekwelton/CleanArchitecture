using System;

namespace Application.Common.Identity
{
    public interface IApplicationRole
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}