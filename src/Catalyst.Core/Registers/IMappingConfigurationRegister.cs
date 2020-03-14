namespace Catalyst.Core.Registers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a register of Model Type Mapping Configurations.
    /// </summary>
    public interface IMappingConfigurationRegister : ITypeRegister
    {
        /// <summary>
        /// Returns instantiations of resolved instance types.
        /// </summary>
        /// <returns>
        /// The collection of instantiated configuration types .
        /// </returns>
        IEnumerable<dynamic> GetInstantiations();
    }
}