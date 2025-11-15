
namespace Library.Domain.Entities
{
    public class Borrower
    {
        private Borrower() { }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        public ICollection<LendingRecord> LendingRecord { get; private set; } = new List<LendingRecord>();

        public Borrower(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            Name = name;
            Email = email;
        }
    }
}
