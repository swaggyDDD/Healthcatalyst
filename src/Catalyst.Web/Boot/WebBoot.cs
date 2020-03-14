namespace Catalyst.Web.Boot
{
    using System.Web;

    using Catalyst.Core;
    using Catalyst.Core.Boot;
    using Catalyst.Core.Caching;

    using LightInject;

    /// <summary>
    /// Boot strapping overrides for Catalyst.Web 
    /// </summary>
    internal class WebBoot : CoreBoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebBoot"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public WebBoot(IServiceContainer container)
            : base(container)
        {
        }

        /// <inheritdoc />
        public override void Boot()
        {
            base.Boot();
        }
        
        /// <summary>
        /// Overrides Core Compose.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        protected override void Compose(IServiceContainer container)
        {
            base.Compose(container);

            // Replace the Runtime cache provider
            container.Register<IRuntimeCacheProvider>(factory => new HttpRuntimeCacheProvider(HttpRuntime.Cache));
        }
    }
}