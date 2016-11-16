namespace Exercises.Ex1.Tests
{
    using System.Collections.Generic;
    using Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class WordPositionsFactoryTests
    {
        public static IReadOnlyCollection<IWordPositionsFactory> Implementations
            => TestUtil.GetAllImplementations<IWordPositionsFactory>();

        [Test]
        public void Words_ShouldBeNonEmptyAndUnique_WhereTwoResourcesAreSupplied([ValueSource(nameof(Implementations))] IWordPositionsFactory factory)
        {
            var positions = factory.Create("Lovecraft", "HackersDictionary");
            Assert.That(positions.Words, Is.Unique.And.Not.Empty);
        }

        [Test, Combinatorial]
        public void GetPositions_ShouldBeQuiteBig_ForCommonWord(
            [ValueSource(nameof(Implementations))] IWordPositionsFactory factory,
            [Values("the", "AND")] string word)
        {
            var positions = factory.Create("HackersDictionary");
            Assert.That(positions.GetPositions(word), Has.Count.GreaterThan(5).And.All.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void GetPositions_ShouldBeEmpty_ForSillyWord([ValueSource(nameof(Implementations))] IWordPositionsFactory factory)
        {
            var positions = factory.Create("Lovecraft");
            Assert.That(positions.GetPositions("notaword"), Is.Empty);
        }
    }
}
