namespace Catalyst.Core.Services
{
    using System;

    /// <inheritdoc />
    internal class ServiceContext : IServiceContext
    {
        /// <summary>
        /// The <see cref="IPersonService"/>.
        /// </summary>
        private readonly Lazy<IPersonService> _personService;

        /// <summary>
        /// The <see cref="IAddressService"/>.
        /// </summary>
        private readonly Lazy<IAddressService> _addressService;

        /// <summary>
        /// The <see cref="IExtendedPropertyService"/>.
        /// </summary>
        private readonly Lazy<IExtendedPropertyService> _extendedPropertyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="personService">
        /// The <see cref="IPersonService"/>.
        /// </param>
        /// <param name="addressService">
        /// The <see cref="IAddressService"/>.
        /// </param>
        /// <param name="extendedPropertyService">
        /// The <see cref="IExtendedPropertyService"/>.
        /// </param>
        public ServiceContext(
            Lazy<IPersonService> personService,
            Lazy<IAddressService> addressService,
            Lazy<IExtendedPropertyService> extendedPropertyService)
        {
            _personService = personService;
            _addressService = addressService;
            _extendedPropertyService = extendedPropertyService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="personService">
        /// The <see cref="IPersonService"/>.
        /// </param>
        /// <param name="addressService">
        /// The <see cref="IAddressService"/>.
        /// </param>
        /// <param name="extendedPropertyService">
        /// The <see cref="IExtendedPropertyService"/>.
        /// </param>
        public ServiceContext(
            IPersonService personService = null,
            IAddressService addressService = null,
            IExtendedPropertyService extendedPropertyService = null)
        {
            if (personService != null) _personService = new Lazy<IPersonService>(() => personService);
            if (addressService != null) _addressService = new Lazy<IAddressService>(() => addressService);
            if (extendedPropertyService != null) _extendedPropertyService = new Lazy<IExtendedPropertyService>(() => extendedPropertyService);
        }

        /// <summary>
        /// Gets the <see cref="IPersonService"/>.
        /// </summary>
        public IPersonService Person => _personService.Value;

        /// <summary>
        /// Gets the <see cref="IAddressService"/>.
        /// </summary>
        public IAddressService AddressService => _addressService.Value;

        /// <summary>
        /// Gets the <see cref="IExtendedPropertyService"/>.
        /// </summary>
        public IExtendedPropertyService ExtendedPropertyService => _extendedPropertyService.Value;
    }
}