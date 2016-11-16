namespace TextData
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class WordReader
    {
        private readonly Encoding encoding;

        public WordReader(Encoding encoding = null)
        {
            this.encoding = encoding ?? Encoding.UTF8;
        }

        public IEnumerable<string> Read(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                foreach (string word in Read(reader))
                {
                    yield return word;
                }
            }
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
