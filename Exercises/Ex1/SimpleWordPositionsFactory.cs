namespace Exercises.Ex1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using TextData;
    using TextData.Extensions;

    public class SimpleWordPositionsFactory : IWordPositionsFactory
    {
        public IWordPositions Create(IReadOnlyCollection<IResource> resources)
        {
            return new SimpleWordPositions(new WordReader(), resources);
        }

        private class SimpleWordPositions : IWordPositions
        {
            private static readonly StringComparer WordComparer = StringComparer.InvariantCultureIgnoreCase;

            private readonly IWordReader wordReader;
            private readonly IReadOnlyCollection<IResource> resources;

            public SimpleWordPositions(IWordReader wordReader, IReadOnlyCollection<IResource> resources)
            {
                this.wordReader = wordReader;
                this.resources = resources;
            }

            public IReadOnlyCollection<string> Words
                => resources.SelectMany(ReadWords).Distinct(WordComparer).ToList();

            public IReadOnlyCollection<int> GetPositions(string word)
            {
                return resources
                    .SelectMany(ReadWords)
                    .Select((w, i) => new { Word = w, Index = i })
                    .Where(wi => WordComparer.Equals(word, wi.Word))
                    .Select(wi => wi.Index)
                    .ToList();
            }

            private IEnumerable<string> ReadWords(IResource resource)
            {
                using (Stream stream = resource.Open())
                {
                    foreach (string word in wordReader.Read(stream))
                    {
                        yield return word;
                    }
                }
            }
        }
    }
}
