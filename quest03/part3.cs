var line = Console.ReadLine();
var lines = new List<List<char>>();

var r = 0L;

while (line != null && line != "")
{
    lines.Add(line.Prepend('.').Append('.').ToList());
    line = Console.ReadLine();
}

lines.Insert(0, Enumerable.Repeat('.', lines[0].Count).ToList());
lines.Add(Enumerable.Repeat('.', lines[0].Count).ToList());

var toMine = lines.Sum(l => l.Count(c => c == '#'));

while (toMine > 0)
{
    var newLines = lines.Select(l => l.Select(x => x).ToList()).ToList();

    r += toMine;

    for (int i = 0; i < lines.Count; i++)
    {
        for (int j = 0; j < lines[i].Count; j++)
        {
            if (lines[i][j] == '#')
            {
                var neighbours = new List<(int, int)>()
                {
                    (i - 1, j),
                    (i + 1, j),
                    (i, j - 1),
                    (i, j + 1),
                    (i - 1, j - 1),
                    (i - 1, j + 1),
                    (i + 1, j - 1),
                    (i + 1, j + 1)
                };

                neighbours = neighbours
                    .Where(n => n.Item1 >= 0 && n.Item1 < lines.Count && n.Item2 >= 0 && n.Item2 < lines[0].Count)
                    .ToList();

                if (neighbours.Any(n => lines[n.Item1][n.Item2] == '.'))
                {
                    newLines[i][j] = '.';
                    toMine--;
                }
            }
        }
    }

    lines = newLines;
}

Console.WriteLine(r);