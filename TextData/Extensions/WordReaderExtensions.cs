namespace TextData.Extensions
{
    using System.Collections.Generic;
    using System.IO;

    public static class WordReaderExtensions
    {
        public static IEnumerable<string> Read(this IWordReader wordReader, Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                foreach (string word in wordReader.Read(streamReader))
                {
                    yield return word;
                }
            }
        }
    }
}
