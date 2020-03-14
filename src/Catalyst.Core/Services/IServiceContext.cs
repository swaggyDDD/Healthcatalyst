namespace Catalyst.Core.Services
{
    /// <summary>
    /// Represents the Catalyst "People Problem" service context.
    /// </summary>
    public interface IServiceContext
    {
        /// <summary>
        /// Gets the <see cref="IPersonService"/>.
        /// </summary>
        IPersonService Person { get; }

        /// <summary>
        /// Gets the <see cref="IAddressService"/>.
        /// </summary>
        IAddressService AddressService { get; }

        /// <summary>
        /// Gets the <see cref="IExtendedPropertyService"/>.
        /// </summary>
        IExtendedPropertyService ExtendedPropertyService { get; }
    }
}