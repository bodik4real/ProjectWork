using System.Data.Entity;
using PhotoManager.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoManager.DAL
{
    public class PhotoManagerDbContext : DbContext
    {
        public PhotoManagerDbContext()
            : base("PhotoManagerConnectionString") { }

        public static PhotoManagerDbContext Create()
        {
            return new PhotoManagerDbContext();
        }    

        public DbSet<Album>  Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<AlbumPhoto> AlbumPhotos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().ToTable("Album");

            modelBuilder.Entity<Photo>().ToTable("Photo");

            modelBuilder.Entity<Like>().ToTable("Like");

            modelBuilder.Entity<Comment>().ToTable("Comment");

            modelBuilder.Entity<AlbumPhoto>().ToTable("AlbumPhoto");

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);

            modelBuilder.Entity<IdentityUser>().HasKey<string>(u => u.Id);

            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);

            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}