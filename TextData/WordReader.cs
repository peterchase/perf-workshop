namespace TextData
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Extracts the human-readable words from a given stream. Whitespace and punctuation
    /// delimits words. Dashes and apostrophes do not delimit words when they are within
    /// a word, so "don't" and "ice-cream" are one word each.
    /// </summary>
    public class WordReader : IWordReader
    {
        private readonly Encoding encoding;

        public WordReader(Encoding encoding = null)
        {
            this.encoding = encoding ?? Encoding.UTF8;
        }

        public IEnumerable<string> Read(TextReader reader)
        {
            StringBuilder builder = null;
            int ci;
            char? pending = null;
            while ((ci = reader.Read()) != -1)
            {
                char c = (char)ci;
                if (char.IsLetterOrDigit(c))
                {
                    builder = builder ?? new StringBuilder();

                    if (pending.HasValue)
                    {
                        builder.Append(pending);
                    }

                    builder.Append(c);
                    pending = null;
                }
                else if (builder != null)
                {
                    if (c == '\'' || c == '-')
                    {
                        pending = c;
                    }
                    else
                    {
                        yield return builder.ToString();
                        pending = null;
                        builder = null;
                    }
                }
                else
                {
                    pending = null;
                }
            }

            if (builder != null)
            {
                yield return builder.ToString();
            }
        }
    }
}
