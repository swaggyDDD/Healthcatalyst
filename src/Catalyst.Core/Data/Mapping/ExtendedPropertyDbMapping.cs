namespace Catalyst.Core.Data.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents the database mapping for the <see cref="ExtendedProperty"/>.
    /// </summary>
    internal class ExtendedPropertyDbMapping : EntityTypeConfiguration<ExtendedProperty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedPropertyDbMapping"/> class.
        /// </summary>
        public ExtendedPropertyDbMapping()
        {
            this.ToTable("catalystExtProperty");

            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.ConverterAlias)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName, 
                    new IndexAnnotation(new IndexAttribute("IX_catalystExtProperty_ConverterAlias")));

            Property(x => x.Value).IsOptional();
        }
    }
}