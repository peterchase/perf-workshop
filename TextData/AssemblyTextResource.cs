﻿namespace TextData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents some text built into the assembly.
    /// </summary>
    public class AssemblyTextResource : IResource
    {
        private static readonly Lazy<IReadOnlyDictionary<string, AssemblyTextResource>> LazyAll = new Lazy<IReadOnlyDictionary<string, AssemblyTextResource>>(FindAll);
        private static readonly Assembly Assembly = typeof(AssemblyTextResource).Assembly;
        private static readonly Regex NameRegex = new Regex(@".*\.(?<name>\w+)\.\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly string path;

        private AssemblyTextResource(string path)
        {
            this.path = path;
        }

        public static IReadOnlyDictionary<string, AssemblyTextResource> All => LazyAll.Value;

        public static AssemblyTextResource Get(string name)
        {
            return LazyAll.Value[name];
        }

        /// <summary>
        /// Opens a new stream on the resource.
        /// </summary>
        /// <returns>Never null. Throws if resource not found.</returns>
        public Stream Open()
        {
            Stream stream = Assembly.GetManifestResourceStream(path);
            if (stream == null)
            {
                throw new FileNotFoundException(path);
            }

            return stream;
        }

        private static IReadOnlyDictionary<string, AssemblyTextResource> FindAll()
        {
            return Assembly.GetManifestResourceNames().ToDictionary(ExtractName, n => new AssemblyTextResource(n));
        }

        private static string ExtractName(string path)
        {
            return NameRegex.Match(path).Groups["name"].Value;
        }
    }
}
