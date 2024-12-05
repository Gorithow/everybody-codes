var line = Console.ReadLine();
var lines = new List<List<char>>();

while (line != null && line != "")
{
    lines.Add(line.ToList());
    line = Console.ReadLine();
}

var r = 0L;

var found = true;

while (found)
{
    found = false;

    for (int i = 0; i < lines.Count - 2; i += 6)
    {
        for (int j = 0; j < lines[i].Count() - 2; j += 6)
        {
            Solve(i, j);
        }
    }

    for (int i = 0; i < lines.Count - 2; i += 6)
    {
        for (int j = 0; j < lines[i].Count() - 2; j += 6)
        {
            Guess(i, j);
        }
    }
}

for (int i = 0; i < lines.Count - 2; i += 6)
{
    for (int j = 0; j < lines[i].Count() - 2; j += 6)
    {
        r += CalculatePoints(i, j);
    }
}

Console.WriteLine(r);

void Solve(int x, int y)
{
    for (int i = x + 2; i < x + 6; i++)
    {
        for (int j = y + 2; j < y + 6; j++)
        {
            if (lines[i][j] != '.')
            {
                continue;
            }

            var vert = lines.Skip(x)
                .Take(2).Select(l => l[j])
                .Union(lines.Skip(x + 6)
                    .Take(2).Select(l => l[j]))
                .Where(c => c != '?');

            var hor = lines[i].Skip(y).Take(2)
                .Union(lines[i].Skip(y + 6).Take(2))
                .Where(c => c != '?');

            var intersect = vert.Intersect(hor).FirstOrDefault();

            if (intersect != default(char))
            {
                found = true;
                lines[i][j] = intersect;
            }
        }
    }
}

void Guess(int x, int y)
{
    var board = new List<List<char>>();

    for (int i = 0; i < 8; i++)
    {
        board.Add(lines[x + i].Skip(y).Take(8).ToList());
    }

    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            if (board[i][j] == '?')
            {
                if (i >= 2 && i <= 5)
                {
                    var hor = board[i];
                    var toFillIndexes = board[i]
                        .Select((x, index) => new { x, index })
                        .Where(x => x.x == '.')
                        .Select(x => x.index)
                        .ToList();

                    if (toFillIndexes.Count == 1)
                    {
                        var toFillIndex = toFillIndexes.First();

                        var options = board
                            .Select(x => x[toFillIndex])
                            .Where(x => x != '.' && x != '?')
                            .GroupBy(x => x)
                            .Where(x => x.Count() == 1)
                            .Select(x => x.Key).ToList();

                        if (options.Count == 1)
                        {
                            found = true;
                            lines[x + i][y + j] = options.First();
                            lines[x + i][y + toFillIndex] = options.First();
                        }
                    }
                }

                if (j >= 2 && j <= 5)
                {
                    var vert = board.Select(x => x[j]).ToList();

                    var toFillIndexes = board.Select(x => x[j])
                        .Select((x, index) => new { x, index })
                        .Where(x => x.x == '.')
                        .Select(x => x.index)
                        .ToList();

                    if (toFillIndexes.Count == 1)
                    {
                        var toFillIndex = toFillIndexes.First();

                        var options = board[toFillIndex]
                            .Where(x => x != '.' && x != '?')
                            .GroupBy(x => x)
                            .Where(x => x.Count() == 1)
                            .Select(x => x.Key).ToList();

                        if (options.Count == 1)
                        {
                            found = true;
                            lines[x + i][y + j] = options.First();
                            lines[x + toFillIndex][y + j] = options.First();
                        }
                    }
                }
            }
        }
    }

}

long CalculatePoints(int x, int y)
{
    var points = 0L;

    for (int i = x + 2; i < x + 6; i++)
    {
        for (int j = y + 2; j < y + 6; j++)
        {
            if (lines[i][j] == '.')
            {
                return 0;
            }

            var relativeX = i - 1 - x;
            var relativeY = j - 1 - y;

            points += (lines[i][j] - 'A' + 1) * (relativeY + (relativeX - 1) * 4);
        }
    }

    return points;
}