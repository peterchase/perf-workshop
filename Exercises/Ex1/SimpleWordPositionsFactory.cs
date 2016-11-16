namespace Exercises.Ex1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TextData;
    using TextData.Extensions;

    public class SimpleWordPositionsFactory : IWordPositionsFactory
    {
        public IWordPositions Create(IReadOnlyCollection<TextResource> resources)
        {
            return new SimpleWordPositions(new WordReader(), resources);
        }

        private class SimpleWordPositions : IWordPositions
        {
            private static readonly StringComparer WordComparer = StringComparer.InvariantCultureIgnoreCase;

            private readonly IWordReader wordReader;
            private readonly IReadOnlyCollection<TextResource> resources;

            public SimpleWordPositions(IWordReader wordReader, IReadOnlyCollection<TextResource> resources)
            {
                this.wordReader = wordReader;
                this.resources = resources;
            }

            public IReadOnlyCollection<string> Words
                => resources.SelectMany(GetWords).Distinct(WordComparer).ToList();

            public IReadOnlyCollection<int> GetPositions(string word)
            {
                return resources
                    .SelectMany(GetWords)
                    .Select((w, i) => new { Word = w, Index = i })
                    .Where(iw => WordComparer.Equals(word, iw.Word))
                    .Select(iw => iw.Index)
                    .ToList();
            }

            private IEnumerable<string> GetWords(TextResource resource)
            {
                using (var stream = resource.Open())
                {
                    return wordReader.Read(stream);
                }
            }
        }
    }
}
