using System.Reflection;

namespace WordPuzzleSolver.Common.Core.Reflection
{
    public static class AssemblyScanner
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var path = AppContext.BaseDirectory;
            var directory = new DirectoryInfo(path);

            if (!directory.Exists) throw new InvalidOperationException($"FATAL error: directory at path '{path}' does not exist");

            var assemblyList = directory
                .GetFiles("WordPuzzleSolver.*.dll")
                .Select(file => Assembly.Load(AssemblyName.GetAssemblyName(file.FullName).ToString())).ToList();

            return assemblyList;
        }

        public static IEnumerable<(Assembly Assembly, string ResourceName)> GetResourceDetailsFromAssemblies(Predicate<string> resourceFilter)
        {
            var containerAssemblies = GetAssemblies()
                .Where(x => x.GetManifestResourceNames()
                .Any(y => resourceFilter(y)));

            foreach (var containerAssembly in containerAssemblies)
            {
                var resourceName = containerAssembly.GetManifestResourceNames().FirstOrDefault(x => resourceFilter(x));
                if (!string.IsNullOrWhiteSpace(resourceName))
                    yield return (containerAssembly, resourceName);
            }
        }

        public static string? GetResourceFromAssemblies(Predicate<string> resourceFilter)
        {
            var containerAssemblies = GetAssemblies()
                .Where(x => x.GetManifestResourceNames()
                .Any(y => resourceFilter(y)));

            var containerAssembly = containerAssemblies.FirstOrDefault();
            if (containerAssembly == null) return null;

            var resourceName = containerAssembly.GetManifestResourceNames().FirstOrDefault(x => resourceFilter(x));
            if (resourceName == null) return null;

            var resourceStream = containerAssembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null) return null;

            using var streamReader = new StreamReader(resourceStream);
            return streamReader.ReadToEnd();
        }
    }
}
