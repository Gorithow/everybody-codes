using System.Text;

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

lines = lines.Skip(2).ToList();

var found = new HashSet<(int, int)>();

for (int k = 0; k < lines.Count; k++)
{
    var l = lines[k];

    foreach (var word in words)
    {
        for (int i = 0; i < l.Length; i++)
        {
            var str = new StringBuilder();

            for (int j = 0; j < word.Length; j++)
            {
                str.Append(l[(i + j) % l.Length]);
            }

            if (str.ToString() == word)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (found.Contains((k, (i + j) % l.Length)))
                    {
                        continue;
                    }

                    found.Add((k, (i + j) % l.Length));

                    r++;
                }
            }
        }
    }
}

for (int k = 0; k < lines[0].Length; k++)
{
    var str = new StringBuilder();

    for (int i = 0; i < lines.Count; i++)
    {
        str.Append(lines[i][k]);
    }

    foreach (var word in words)
    {
        for (int j = 0; j + word.Length - 1 < str.Length; j++)
        {
            if (str.ToString().Substring(j, word.Length) == word)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (found.Contains((j + i, k)))
                    {
                        continue;
                    }

                    found.Add((j + i, k));

                    r++;
                }
            }
        }
    }
}

Console.WriteLine(r);