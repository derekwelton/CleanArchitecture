﻿namespace Application.Common.Identity
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}