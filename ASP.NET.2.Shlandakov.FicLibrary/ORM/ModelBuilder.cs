using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    internal static class ModelBuilder
    {
        public static void Build(DbModelBuilder mb)
        {
            CommentBuilder(mb);
            CommentRelationBuilder(mb);
            ProfileBuilder(mb);
            RoleBuilder(mb);
            TextBuilder(mb);
            TextDescriptionBuilder(mb);
            UserBuilder(mb);
        }

        private static void CommentBuilder(DbModelBuilder modelBuilder)
        {

        }

        private static void CommentRelationBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentRelation>()
                .Property(e => e.RelationName)
                .IsUnicode(false);
        }

        private static void ProfileBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .Property(e => e.Mobile)
                .IsUnicode(false);
        }
        private static void RoleBuilder(DbModelBuilder modelBuilder)
        {

        }

        private static void TextBuilder(DbModelBuilder modelBuilder)
        {

        }

        private static void TextDescriptionBuilder(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TextDescription>()
                .HasMany(e => e.Rating)
                .WithRequired(e => e.TextDescription)
                .HasForeignKey(e => e.TextDescId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TextDescription>()
                .HasMany(e => e.Text)
                .WithRequired(e => e.TextDescription)
                .HasForeignKey(e => e.TitleId)
                .WillCascadeOnDelete(false);
        }
        private static void UserBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comment)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CommentatorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TextDescription)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rating)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }

    }
}
