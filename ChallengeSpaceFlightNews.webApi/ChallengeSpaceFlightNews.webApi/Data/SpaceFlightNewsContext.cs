using ChallengeSpaceFlightNews.webApi.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Data
{
    public class SpaceFlightNewsContext : DbContext
    {
        public SpaceFlightNewsContext(DbContextOptions<SpaceFlightNewsContext> options) : base(options)
        {
            var sqlServerOptionsExtension =
                options.FindExtension<SqlServerOptionsExtension>();
            StringConnection = sqlServerOptionsExtension.ConnectionString;
        }

        public string StringConnection { get; }

        // Define Database tables

        public DbSet<Launch> Launches { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Ignore<Notification>();

            #region Launches

            modelBuilder.Entity<Launch>().HasKey(x => x.Id);

            // Provider
            modelBuilder.Entity<Launch>().Property(x => x.Provider).HasMaxLength(100);
            modelBuilder.Entity<Launch>().Property(x => x.Provider).HasColumnType("varchar(100)");
            modelBuilder.Entity<Launch>().Property(x => x.Provider).IsRequired();

            // Foreign Key ArticleId
            modelBuilder.Entity<Launch>()
                        .HasOne<Article>(o => o.Article)
                        .WithMany(c => c.Launches)
                        .HasForeignKey(s => s.ArticleId);

            #endregion

            #region Events

            modelBuilder.Entity<Event>().HasKey(x => x.Id);

            // Provider
            modelBuilder.Entity<Event>().Property(x => x.Provider).HasMaxLength(100);
            modelBuilder.Entity<Event>().Property(x => x.Provider).HasColumnType("varchar(100)");
            modelBuilder.Entity<Event>().Property(x => x.Provider).IsRequired();

            // Foreign Key ArticleId
            modelBuilder.Entity<Event>()
                        .HasOne<Article>(o => o.Article)
                        .WithMany(c => c.Events)
                        .HasForeignKey(s => s.ArticleId);

            #endregion

            #region Articles

            modelBuilder.Entity<Article>().HasKey(x => x.Id);

            // Title
            modelBuilder.Entity<Article>().Property(x => x.Title).HasMaxLength(200);
            modelBuilder.Entity<Article>().Property(x => x.Title).HasColumnType("varchar(200)");
            modelBuilder.Entity<Article>().Property(x => x.Title).IsRequired();

            // Url
            modelBuilder.Entity<Article>().Property(x => x.Url).HasMaxLength(200);
            modelBuilder.Entity<Article>().Property(x => x.Url).HasColumnType("varchar(200)");
            modelBuilder.Entity<Article>().Property(x => x.Url).IsRequired();

            // ImageUrl
            modelBuilder.Entity<Article>().Property(x => x.ImageUrl).HasMaxLength(300);
            modelBuilder.Entity<Article>().Property(x => x.ImageUrl).HasColumnType("varchar(300)");
            modelBuilder.Entity<Article>().Property(x => x.ImageUrl).IsRequired();

            // NewsSites
            modelBuilder.Entity<Article>().Property(x => x.NewsSite).HasMaxLength(50);
            modelBuilder.Entity<Article>().Property(x => x.NewsSite).HasColumnType("varchar(50)");
            modelBuilder.Entity<Article>().Property(x => x.NewsSite).IsRequired();

            // Summary
            modelBuilder.Entity<Article>().Property(x => x.Summary).HasMaxLength(400);
            modelBuilder.Entity<Article>().Property(x => x.Summary).HasColumnType("varchar(400)");
            modelBuilder.Entity<Article>().Property(x => x.Summary).IsRequired();

            // PublishedAt
            modelBuilder.Entity<Article>().Property(x => x.PublishedAt).HasDefaultValueSql("getdate()");

            // UpdatedAt
            modelBuilder.Entity<Article>().Property(x => x.UpdatedAt).HasColumnType("datetime2");

            // Featured
            modelBuilder.Entity<Article>().Property(x => x.Featured).HasColumnType("bit");
            modelBuilder.Entity<Article>().Property(x => x.Featured).IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
