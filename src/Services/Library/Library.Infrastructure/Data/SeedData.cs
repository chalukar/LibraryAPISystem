using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(LibraryDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book("C# 12 and .NET 8 - Modern Cross-Platform Development Fundamentals", "Mark J. Price", "9781837635870", 821, 8),
                    new Book("Head First C#: A Learner's Guide to Real-World Programming", "Andrew Stellman, Jennifer Greene", "9781491976708", 1010, 10),
                    new Book("C# 11 in a Nutshell : The Definitive Reference", "Joseph Albahari", "9781098121952", 1056, 6)
                );
            }

            if (!context.Borrowers.Any())
            {
                context.Borrowers.AddRange(
                    new Borrower("Chaluka", "chaluka@example.com"),
                    new Borrower("Rathnayaka", "Rathnayaka@example.com")
                );
            }

            await context.SaveChangesAsync();

        }
    }
}
