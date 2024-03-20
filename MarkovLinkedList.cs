
using MyLinkedListLibrary;
public class MarkovEntry
{
    private SortedList<string, int> nextWords;
    private string root;
    private int count;
    private Random rng;

    public MarkovEntry(string root)
    {
        this.root = root;
        count = 0;
        nextWords = new SortedList<string, int>();
        rng = new Random();
    }

    public void Add(string str)
    {
        if (nextWords.ContainsKey(str))
        {
            nextWords[str]++;
        }
        else
        {
            nextWords.Add(str, 1);
        }
        count++;
    }

    public string RandomWord()
    {
        int index = rng.Next(nextWords.Count);
        return nextWords.Keys.ElementAt(index);
    }

    public string WeightedRandomWord()
    {
        List<string> options = new List<string>();

        foreach (KeyValuePair<string, int> pair in nextWords)
        {
            for (int x = 0; x < pair.Value; x++)
            {
                options.Add(pair.Key);
            }
        }

        int index = rng.Next(options.Count);

        return options[index];
    }

    public override string ToString()
    {
        string output = $"'{root}' ({count}) : ";

        foreach (KeyValuePair<string, int> pair in nextWords)
        {
            output += $"'{pair.Key}' {pair.Value}, ";
        }

        return output;
    }


}