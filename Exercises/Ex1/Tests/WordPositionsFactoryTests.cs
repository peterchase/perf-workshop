namespace Exercises.Ex1.Tests
{
    using System.Collections.Generic;
    using Extensions;
    using NUnit.Framework;
    using TextData;

    [TestFixture]
    public class WordPositionsFactoryTests
    {
        public static IReadOnlyCollection<IWordPositionsFactory> Implementations
            => TestUtil.GetAllImplementations<IWordPositionsFactory>();

        [Test]
        public void Words_ShouldBeNonEmptyAndUnique_WhereTwoResourcesAreSupplied(
            [ValueSource(nameof(Implementations))] IWordPositionsFactory factory)
        {
            var positions = factory.Create("Lovecraft", "HackersDictionary");
            Assert.That(positions.Words, Is.Unique.And.Not.Empty);
        }

        [Test]
        [Combinatorial]
        public void GetPositions_ShouldBeQuiteBig_ForCommonWord(
            [ValueSource(nameof(Implementations))] IWordPositionsFactory factory,
            [Values("the", "AND")] string word)
        {
            var positions = factory.Create("HackersDictionary");
            Assert.That(positions.GetPositions(word), Has.Count.GreaterThan(5).And.All.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void GetPositions_ShouldBeEmpty_ForSillyWord(
            [ValueSource(nameof(Implementations))] IWordPositionsFactory factory)
        {
            var positions = factory.Create("Lovecraft");
            Assert.That(positions.GetPositions("notaword"), Is.Empty);
        }

        [Test]
        public void GetPositions_ShouldBeCorrect_ForSimpleResource(
            [ValueSource(nameof(Implementations))] IWordPositionsFactory factory)
        {
            var positions = factory.Create(new StringResource("the cow is bigger than the rabbit, smaller than the whale"));

            Assert.That(positions.GetPositions("the"), Is.EqualTo(new[] { 0, 5, 9 }));
            Assert.That(positions.GetPositions("than"), Is.EqualTo(new[] { 4, 8 }));
            Assert.That(positions.GetPositions("smaller"), Is.EqualTo(new[] { 7 }));
            Assert.That(positions.GetPositions("bullwarks"), Is.Empty);
        }
    }
}
