using Microsoft.EntityFrameworkCore;
using QueryExample.Entities;

namespace QueryExample.EntityFramework
{
    public class QueryExampleContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestTag> QuestTags { get; set; }
        public DbSet<QuestPass> QuestPasses { get; set; }
        public DbSet<Competition> Competitions { get; set; }

        public QueryExampleContext(DbContextOptions<QueryExampleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserFriend>()
                .ToTable("UserFriends");

            modelBuilder.Entity<UserFriend>()
                .HasKey(uf => new { uf.UserId, uf.FriendId });

            modelBuilder.Entity<UserFriend>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFriends)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<UserFriend>()
                .HasOne(uf => uf.Friend)
                .WithMany()
                .HasForeignKey(uf => uf.FriendId);

            modelBuilder.Entity<Quest>()
                .ToTable("Quests");

            modelBuilder.Entity<Tag>()
                .ToTable("Tags");

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Value)
                .IsUnique();

            modelBuilder.Entity<QuestTag>()
                .ToTable("QuestTags");

            modelBuilder.Entity<QuestTag>()
                .HasKey(qt => new { qt.QuestId, qt.TagId });

            modelBuilder.Entity<QuestPass>()
                .ToTable("QuestPasses");

            modelBuilder.Entity<Competition>()
                .ToTable("Competitions");

            modelBuilder.Entity<Competition>()
                .HasMany(c => c.Passes)
                .WithOne(qp => qp.Competition)
                .HasForeignKey(qp => qp.CompetitionId);
        }
    }
}
