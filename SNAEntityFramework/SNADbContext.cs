using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SNAEntityFramework.Entities;

namespace SNAEntityFramework
{
    public class SNADbContext: DbContext
    {
        public SNADbContext()
        {

        }

        public SNADbContext(DbContextOptions<SNADbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dataset>().HasAlternateKey(p => p.Name);
            modelBuilder.Entity<Link>().HasKey(p => new { p.DatasetId, p.User1Id, p.User2Id });

            modelBuilder.Entity<Dataset>().HasData(
                new Dataset() { Id = 1, Name = "Sample", Description = "Sample dataset", LinksCount = 48, UsersCount = 18, AvgFriendsCount = 5.3333 }
                );

            modelBuilder.Entity<Link>().HasData(
                new Link() { User1Id = 0, User2Id = 1, DatasetId = 1 },
                new Link() { User1Id = 0, User2Id = 2, DatasetId = 1 },
                new Link() { User1Id = 0, User2Id = 3, DatasetId = 1 },
                new Link() { User1Id = 0, User2Id = 4, DatasetId = 1 },
                new Link() { User1Id = 0, User2Id = 5, DatasetId = 1 },
                new Link() { User1Id = 0, User2Id = 6, DatasetId = 1 },
                new Link() { User1Id = 0, User2Id = 12, DatasetId = 1 },
                new Link() { User1Id = 1, User2Id = 2, DatasetId = 1 },
                new Link() { User1Id = 1, User2Id = 3, DatasetId = 1 },
                new Link() { User1Id = 1, User2Id = 4, DatasetId = 1 },
                new Link() { User1Id = 1, User2Id = 5, DatasetId = 1 },
                new Link() { User1Id = 2, User2Id = 3, DatasetId = 1 },
                new Link() { User1Id = 2, User2Id = 4, DatasetId = 1 },
                new Link() { User1Id = 2, User2Id = 5, DatasetId = 1 },
                new Link() { User1Id = 3, User2Id = 4, DatasetId = 1 },
                new Link() { User1Id = 3, User2Id = 5, DatasetId = 1 },
                new Link() { User1Id = 4, User2Id = 5, DatasetId = 1 },
                new Link() { User1Id = 6, User2Id = 7, DatasetId = 1 },
                new Link() { User1Id = 6, User2Id = 8, DatasetId = 1 },
                new Link() { User1Id = 6, User2Id = 9, DatasetId = 1 },
                new Link() { User1Id = 6, User2Id = 10, DatasetId = 1 },
                new Link() { User1Id = 6, User2Id = 11, DatasetId = 1 },
                new Link() { User1Id = 6, User2Id = 12, DatasetId = 1 },
                new Link() { User1Id = 7, User2Id = 8, DatasetId = 1 },
                new Link() { User1Id = 7, User2Id = 9, DatasetId = 1 },
                new Link() { User1Id = 7, User2Id = 10, DatasetId = 1 },
                new Link() { User1Id = 7, User2Id = 11, DatasetId = 1 },
                new Link() { User1Id = 8, User2Id = 9, DatasetId = 1 },
                new Link() { User1Id = 8, User2Id = 10, DatasetId = 1 },
                new Link() { User1Id = 8, User2Id = 11, DatasetId = 1 },
                new Link() { User1Id = 9, User2Id = 10, DatasetId = 1 },
                new Link() { User1Id = 9, User2Id = 11, DatasetId = 1 },
                new Link() { User1Id = 10, User2Id = 11, DatasetId = 1 },
                new Link() { User1Id = 12, User2Id = 13, DatasetId = 1 },
                new Link() { User1Id = 12, User2Id = 14, DatasetId = 1 },
                new Link() { User1Id = 12, User2Id = 15, DatasetId = 1 },
                new Link() { User1Id = 12, User2Id = 16, DatasetId = 1 },
                new Link() { User1Id = 12, User2Id = 17, DatasetId = 1 },
                new Link() { User1Id = 13, User2Id = 14, DatasetId = 1 },
                new Link() { User1Id = 13, User2Id = 15, DatasetId = 1 },
                new Link() { User1Id = 13, User2Id = 16, DatasetId = 1 },
                new Link() { User1Id = 13, User2Id = 17, DatasetId = 1 },
                new Link() { User1Id = 14, User2Id = 15, DatasetId = 1 },
                new Link() { User1Id = 14, User2Id = 16, DatasetId = 1 },
                new Link() { User1Id = 14, User2Id = 17, DatasetId = 1 },
                new Link() { User1Id = 15, User2Id = 16, DatasetId = 1 },
                new Link() { User1Id = 15, User2Id = 17, DatasetId = 1 },
                new Link() { User1Id = 16, User2Id = 17, DatasetId = 1 }
                );
        }

        //entities
        public DbSet<Dataset> Datasets { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
