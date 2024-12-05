var line = Console.ReadLine();
var lines = new List<string>();

while (line != null)
{
    lines.Add(line);
    line = Console.ReadLine();
}

var r = 0;

var words = lines[0].Substring(6).Split(',').ToArray();

foreach (var word in words)
{
    for (int i = 0; i + word.Length - 1 < lines[2].Length; i++)
    {
        if (lines[2].Substring(i, word.Length) == word)
        {
            r++;
        }
    }
}

Console.WriteLine(r);