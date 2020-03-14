namespace Catalyst.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents the database mapping for the <see cref="Address"/>.
    /// </summary>
    internal class AddressDbMapping : EntityTypeConfiguration<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDbMapping"/> class.
        /// </summary>
        public AddressDbMapping()
        {
            this.ToTable("catalystAddress");

            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.Name).IsOptional().HasMaxLength(255);
            Property(x => x.Address1).IsOptional().HasMaxLength(500);
            Property(x => x.Address2).IsOptional().HasMaxLength(500);
            Property(x => x.Region).IsOptional().HasMaxLength(255);
            Property(x => x.Locality).IsOptional().HasMaxLength(255);
            Property(x => x.CountryCode).IsOptional().HasMaxLength(3);
            Property(x => x.PostalCode).IsOptional().HasMaxLength(50);
            Property(x => x.UpdateDate).IsRequired();
            Property(x => x.CreateDate).IsRequired();
        }
    }
}