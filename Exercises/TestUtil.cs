namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class TestUtil
    {
        public static IReadOnlyCollection<T> GetAllImplementations<T>()
            => typeof(T).Assembly.GetExportedTypes()
            .Where(t => t.IsClass && typeof(T).IsAssignableFrom(t))
            .Select(Activator.CreateInstance)
            .Cast<T>()
            .ToList();
    }
}
