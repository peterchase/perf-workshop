namespace TextData
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// A resource whose content is directly supplied as a string, on construction.
    /// </summary>
    public class StringResource : IResource
    {
        private readonly Encoding encoding;
        private readonly string textString;

        public StringResource(string textString, Encoding encoding = null)
        {
            this.textString = textString;
            this.encoding = encoding ?? Encoding.UTF8;
        }

        public Stream Open()
        {
            return new MemoryStream(encoding.GetBytes(textString));
        }
    }
}
