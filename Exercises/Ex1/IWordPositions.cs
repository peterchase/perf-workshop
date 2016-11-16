namespace Exercises.Ex1
{
    using System.Collections.Generic;

    public interface IWordPositions
    {
        /// <summary>
        /// Gets all words that appear at least once.
        /// </summary>
        IReadOnlyCollection<string> Words { get; }

        /// <summary>
        /// Return all the positions, in terms of word count, of the given word. Words
        /// are considered case-insensitive.
        /// </summary>
        /// <param name="word">Not null or empty.</param>
        /// <returns>Never null. Empty if word does not appear at all.</returns>
        IReadOnlyCollection<int> GetPositions(string word);
    }
}
