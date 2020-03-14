namespace Catalyst.Core.Data.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents the database mapping for the <see cref="Person"/>.
    /// </summary>
    internal class PersonDbMapping : EntityTypeConfiguration<Person>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDbMapping"/> class.
        /// </summary>
        public PersonDbMapping()
        {
            this.ToTable("catalystPerson");

            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_catalystPerson_FirstName")));

            Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_catalystPerson_LastName")));

            Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName, 
                    new IndexAnnotation(new IndexAttribute("IX_catalystPerson_Slug") { IsUnique = true }));

            Property(x => x.Birthday).IsRequired();

            HasMany(x => x.Addresses)
                .WithRequired(x => x.Person)
                .Map(x => x.MapKey("PersonId"))
                .WillCascadeOnDelete(true);

            HasMany(x => x.Properties)
                .WithRequired(x => x.Person)
                .Map(x => x.MapKey("PersonId"))
                .WillCascadeOnDelete(true);

            Property(x => x.Watch).IsRequired();

            Property(x => x.UpdateDate).IsRequired();
            Property(x => x.CreateDate).IsRequired();
        }
    }
}