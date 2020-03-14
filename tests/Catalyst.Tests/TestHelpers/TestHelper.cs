namespace Catalyst.Tests.TestHelpers
{
    using System;
    using System.IO;

    using LightInject;

    /// <summary>
    /// Common helper properties and methods useful to testing
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Gets the current assembly directory.
        /// </summary>
        /// <value>The assembly directory.</value>
        public static string CurrentAssemblyDirectory
        {
            get
            {
                var codeBase = typeof(TestHelper).Assembly.CodeBase;
                var uri = new Uri(codeBase);
                var path = uri.LocalPath;
                return Path.GetDirectoryName(path);
            }
        }

        public static string TestAppDataDirectory
        {
            get
            {
                var assembly = CurrentAssemblyDirectory;
                var baseDir = assembly.Substring(0, assembly.LastIndexOf("\\bin", StringComparison.Ordinal));

                return $"{baseDir}\\App_Data\\";
            }
        }

        /// <summary>
        /// Maps the given <paramref name="relativePath"/> making it rooted on <see cref="CurrentAssemblyDirectory"/>. <paramref name="relativePath"/> must start with <code>~/</code>
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>The mapped path for use in the test</returns>
        public static string MapPathForTest(string relativePath)
        {
            if (!relativePath.StartsWith("~/")) throw new ArgumentException("relativePath must start with '~/'", nameof(relativePath));

            return relativePath.Replace("~/", CurrentAssemblyDirectory + "/");
        }


        public static IServiceContainer GetEmptyServiceContainer()
        {
            var container = new ServiceContainer();
            container.EnableAnnotatedConstructorInjection();
            container.EnableAnnotatedPropertyInjection();

            return container;
        }
    }
}