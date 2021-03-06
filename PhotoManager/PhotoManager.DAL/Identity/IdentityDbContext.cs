﻿using Microsoft.AspNet.Identity.EntityFramework;
using PhotoManager.DAL.Entities;

namespace PhotoManager.DAL.Identity
{
    public class IdentityDbContext : IdentityDbContext<User>
    {
        public IdentityDbContext()
            : base("PhotoManagerConnectionString", throwIfV1Schema: false)
        {
        }
        public static IdentityDbContext Create()
        {
            return new IdentityDbContext();
        }
    }
}
