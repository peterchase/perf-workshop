namespace TextData
{
    using System.Collections.Generic;
    using System.IO;

    public interface IWordReader
    {
        IEnumerable<string> Read(TextReader reader);
    }
}