using NUnit.Framework;

namespace TextData.Tests
{
    [TestFixture]
    public class TextResourceTests
    {
        [Test]
        public void All_ShouldReturnSomeResources()
        {
            Assert.That(TextResource.All, Is.Not.Empty.And.Not.Null);
        }

        [TestCase("HackersDictionary")]
        [TestCase("LotsOfText")]
        [TestCase("Lovecraft")]
        public void Get_ShouldReturnResource_ForValidName(string name)
        {
            Assert.That(TextResource.Get(name), Is.Not.Null);
        }

        [TestCase("HackersDictionary")]
        [TestCase("LotsOfText")]
        [TestCase("Lovecraft")]
        public void Open_ShouldReturnStream_ForValidName(string name)
        {
            var resource = TextResource.Get(name);
            var stream = resource.Open();
            stream.Dispose();
        }
    }
}
