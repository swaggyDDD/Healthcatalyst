namespace Catalyst.Core.Data.Context
{
    using System.Data.Entity;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents the Catalyst <see cref="DbContext"/>.
    /// </summary>
    internal interface ICatalystDbContext
    {
        /// <summary>
        /// Gets or sets the addresses <see cref="DbSet"/>.
        /// </summary>
        DbSet<Person> People { get; set; }
    }
}