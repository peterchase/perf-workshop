namespace Exercises.Ex1.Extensions
{
    using System.Linq;
    using TextData;

    internal static class WordPositionsFactoryExtensions
    {
        public static IWordPositions Create(this IWordPositionsFactory factory, params string[] resourceNames)
        {
            return factory.Create(resourceNames.Select(AssemblyTextResource.Get).ToList());
        }

        public static IWordPositions Create(this IWordPositionsFactory factory, params IResource[] resources)
        {
            return factory.Create(resources);
        }
    }
}
