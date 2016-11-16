namespace Exercises.Ex1
{
    using System.Collections.Generic;
    using TextData;

    public interface IWordPositionsFactory
    {
        IWordPositions Create(IEnumerable<TextResource> resources);
    }
}
