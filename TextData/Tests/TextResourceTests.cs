using NUnit.Framework;

namespace TextData.Tests
{
    [TestFixture]
    public class TextResourceTests
    {
        [Test]
        public void All_ShouldReturnSomeResources()
        {
            Assert.That(AssemblyTextResource.All, Is.Not.Empty.And.Not.Null);
        }

        [TestCase("HackersDictionary")]
        [TestCase("LotsOfText")]
        [TestCase("Lovecraft")]
        public void Get_ShouldReturnResource_ForValidName(string name)
        {
            Assert.That(AssemblyTextResource.Get(name), Is.Not.Null);
        }

        [TestCase("HackersDictionary")]
        [TestCase("LotsOfText")]
        [TestCase("Lovecraft")]
        public void Open_ShouldReturnStream_ForValidName(string name)
        {
            var resource = AssemblyTextResource.Get(name);
            var stream = resource.Open();
            stream.Dispose();
        }
    }
}
