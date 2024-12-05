var line = Console.ReadLine();
var lines = new List<string>();

while (line != null)
{
    lines.Add(line);
    line = Console.ReadLine();
}

var r = 0;

var words = lines[0].Substring(6).Split(',').ToArray();

words = words.Union(words.Select(w => new string(w.Reverse().ToArray()))).Distinct().ToArray();

foreach (var l in lines.Skip(2))
{
    var found = new HashSet<int>();

    foreach (var word in words)
    {
        for (int i = 0; i + word.Length - 1 < l.Length; i++)
        {
            if (l.Substring(i, word.Length) == word)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (found.Contains(i + j))
                    {
                        continue;
                    }

                    found.Add(i + j);
                    r++;
                }
            }
        }
    }
}

Console.WriteLine(r);