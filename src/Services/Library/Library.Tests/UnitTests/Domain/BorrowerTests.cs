using FluentAssertions;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.UnitTests.Domain
{
    public class BorrowerTests
    {
        [Fact]
        public void Borrower_ShouldSetProperties()
        {
            var borrower = new Borrower("Chaluka", "Chaluka@mail.com");

            borrower.Name.Should().Be("Chaluka");
            borrower.Email.Should().Be("Chaluka@mail.com");
        }

        [Fact]
        public void Borrower_ShouldThrow_WhenNameMissing()
        {
            Action a = () => new Borrower("", "Chal@mail.com");
            a.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Borrower_ShouldThrow_WhenEmailMissing()
        {
            Action a = () => new Borrower("Chaluka", "");
            a.Should().Throw<ArgumentException>();
        }
    }
}
