var line = Console.ReadLine();
var lines = new List<string>();

while (line != null)
{
    lines.Add(line);
    line = Console.ReadLine();
}

var r = 0L;

for (int i = 0; i < lines.Count; i += 9)
{
    for (int j = 0; j < lines[i].Length; j += 9)
    {
        r += CalculatePoints(i, j);
        Console.WriteLine((i, j));
    }
}

Console.WriteLine(r);

long CalculatePoints(int x, int y)
{
    var answer = new List<char>();

    for (int i = x; i < x + 8; i++)
    {
        for (int j = y; j < y + 8; j++)
        {
            if (lines[i][j] != '.')
            {
                continue;
            }

            for (int t = x; t < x + 8; t++)
            {
                var c = lines[t][j];

                if (c == '.' || c == '*')
                {
                    continue;
                }

                if (lines[i].Substring(y, 8).Contains(c))
                {
                    answer.Add(c);
                    break;
                }
            }
        }
    }

    var points = 0L;

    for (int i = 0; i < answer.Count(); i++)
    {
        points += (answer[i] - 'A' + 1) * (i + 1);
    }

    return points;
}