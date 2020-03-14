namespace Catalyst.Core.Data.Initializers
{
    using System.Data.Entity;

    using Catalyst.Core.Data.Context;

    /// <summary>
    /// Always drops the database and recreate without seeding.
    /// </summary>
    /// <remarks>
    /// Used for testing.
    /// </remarks>
    internal class NoSeedDropAlwaysCatalystDbInitializer : DropCreateDatabaseAlways<CatalystDbContext>
    {
    }
}
