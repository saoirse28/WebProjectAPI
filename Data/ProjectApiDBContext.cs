using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebProjectAPI.Data.Entities;
using WebProjectAPI.Interface;

namespace WebProjectAPI.Data
{
    public class ProjectApiDBContext : DbContext
    {
        public ProjectApiDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Reaction> Reaction { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<PhotoReactions> PhotoReactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryPhotos>()
            .HasKey(t => new { t.CategoryId, t.PhotosId });

            //One(Category)toMany(Photo)
            modelBuilder.Entity<CategoryPhotos>()
                .HasOne(pt => pt.Category)
                .WithMany(p => p.CategoryPhotos)
                .HasForeignKey(pt => pt.CategoryId);

            //One(Photo)toMany(Category)
            modelBuilder.Entity<CategoryPhotos>()
                .HasOne(pt => pt.Photos)
                .WithMany(t => t.CategoryPhotos)
                .HasForeignKey(pt => pt.PhotosId);

            //One(User)withMany(Photo)
            modelBuilder.Entity<Photo>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.Photos)
                .HasForeignKey(pt => pt.UserId);

            //One(Photo)WithMany(Reaction)
            modelBuilder.Entity<PhotoReactions>()
                .HasOne(pt => pt.Photos)
                .WithMany(t => t.PhotoReactions)
                .HasForeignKey(pt => pt.PhotoId);

            //One(User)withMany(PhotoReaction)
            modelBuilder.Entity<PhotoReactions>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.PhotoReactions)
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            //One(User)withMany(Comment)
            modelBuilder.Entity<Comment>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            //One(User)withMany(CommentReactions)
            modelBuilder.Entity<CommentReactions>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.CommentReactions)
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

        }

        public DbSet<WebProjectAPI.Data.Entities.CommentReactions> CommentReactions { get; set; }

        
    }

}
