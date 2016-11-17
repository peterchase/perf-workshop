namespace Exercises.Ex1
{
    using System.Collections.Generic;
    using TextData;

    public interface IWordPositionsFactory
    {
        IWordPositions Create(IReadOnlyCollection<IResource> resources);
    }
}
