

using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data
{
    public class LibraryDbContext :DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Borrower> Borrowers => Set<Borrower>();
        public DbSet<LendingRecord> LendingRecords => Set<LendingRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).IsRequired().HasMaxLength(200);
                b.Property(x => x.Author).IsRequired().HasMaxLength(200);
                b.Property(x => x.Isbn).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Borrower>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(200);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<LendingRecord>(l =>
            {
                l.HasKey(x => x.Id);
                l.HasOne(x => x.Book)
                 .WithMany(b => b.LendingRecord)
                 .HasForeignKey(x => x.BookId);

                l.HasOne(x => x.Borrower)
                 .WithMany(b => b.LendingRecord)
                 .HasForeignKey(x => x.BorrowerId);
            });
        }
    }
}
