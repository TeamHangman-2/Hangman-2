namespace Hangman.WordGeneration
{
    /// <summary>
    /// Generates word and returns it within method GetWord()
    /// </summary>
    public interface IWordGenerator
    {
        Word GetWord();
    }
}
