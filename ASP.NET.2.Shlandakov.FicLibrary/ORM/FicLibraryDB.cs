using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ORM
{
    public partial class FicLibraryDB : DbContext
    {
        public FicLibraryDB()
            : base("name=FicLibraryDB")
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<CommentRelation> CommentRelation { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Text> Text { get; set; }
        public virtual DbSet<TextDescription> TextDescription { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelBuilder.Build(modelBuilder);
        }
    }
}
