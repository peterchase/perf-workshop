namespace TextData
{
    using System.IO;

    public interface IResource
    {
        Stream Open();
    }
}
