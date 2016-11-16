namespace TextData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class WordReaderTests
    {
        [Test]
        public void Read_ShouldReturnSomeWords_ForValidTextResourceStream()
        {
            var reader = new WordReader();
            var resource = TextResource.Get("HackersDictionary");
            using (var stream = resource.Open())
            {
                bool hasWords = false;
                foreach (string word in reader.Read(stream))
                {
                    Console.WriteLine(word);
                    hasWords = true;
                }

                Assert.That(hasWords, Is.True);
            }
        }

        [TestCase("Don't panic", "Don't", "panic")]
        [TestCase("My flugel-spurgler wins", "My", "flugel-spurgler", "wins")]
        public void Read_ShouldIncludeApostrophesAndDashes_WhenTheyArePartOfWord(string input, params string[] expected)
        {
            var wordReader = new WordReader();
            using (var reader = new StringReader(input))
            {
                var result = wordReader.Read(reader).ToArray();
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestCase("'Don't panic'", "Don't", "panic")]
        [TestCase("Say: 'Don't panic'", "Say", "Don't", "panic")]
        [TestCase("My dog- wins", "My", "dog", "wins")]
        public void Read_ShouldExcludeApostrophesAndDashes_WhenTheyAreNotPartOfWord(string input, params string[] expected)
        {
            var wordReader = new WordReader();
            using (var reader = new StringReader(input))
            {
                var result = wordReader.Read(reader).ToArray();
                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}
