namespace Catalyst.Core.Data.Initializers
{
    using System.Data.Entity;

    using Catalyst.Core.Data.Context;
    using Catalyst.Core.Data.Migrations.Install;

    /// <summary>
    /// Overrides the default DropCreate initializer.
    /// </summary>
    /// <remarks>
    /// This will not seed any data!
    /// </remarks>
    internal class DropAlwaysCatalystDbInitializer : DropCreateDatabaseAlways<CatalystDbContext>
    {
        /// <inheritdoc />
        protected override void Seed(CatalystDbContext context)
        {
            base.Seed(context);

            foreach (var person in SeedDataHelper.GetDefaultPeople())
            {
                context.People.Add(person);
            }
            context.SaveChanges();
        }
    }
}