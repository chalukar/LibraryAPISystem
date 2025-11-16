using Xunit;

namespace Library.Tests.IntegrationTests
{
    // Shared database fixture for integration tests, with parallelization disabled
    [CollectionDefinition("Database collection", DisableParallelization = true)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}


