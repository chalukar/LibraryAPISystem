
namespace Library.Domain.Entities
{
    public class Borrower
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        public Borrower(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
