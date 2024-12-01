var line = Console.ReadLine();
var lines = new List<string>();

while (line != null && line != "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var start = (0, 0, Direction.Down);

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == 'S')
        {
            start = (i, j, Direction.Down);
        }
    }
}

var dict = new Dictionary<(int, int, Direction), int>();

var initialAltitude = 384400;
dict[start] = initialAltitude;

var maxSouth = 0;

while (dict.Any())
{
    var newDict = new Dictionary<(int, int, Direction), int>();

    foreach (var entry in dict)
    {
        var (x, y, dir) = entry.Key;
        var value = entry.Value;

        var neighbours = new List<(int, int, Direction)>()
        {
            (x - 1, y, Direction.Up),
            (x + 1, y, Direction.Down),
            (x, y - 1, Direction.Left),
            (x, y + 1, Direction.Right)
        };

        if (dir == Direction.Up)
        {
            neighbours.Remove((x + 1, y, Direction.Down));
        }
        else if (dir == Direction.Down)
        {
            neighbours.Remove((x - 1, y, Direction.Up));
        }
        else if (dir == Direction.Left)
        {
            neighbours.Remove((x, y + 1, Direction.Right));
        }
        else if (dir == Direction.Right)
        {
            neighbours.Remove((x, y - 1, Direction.Left));
        }

        foreach (var neighbour in neighbours)
        {
            var (nx, ny, _) = neighbour;

            if (nx < 0 || ny < 0 || ny >= lines[0].Length)
            {
                continue;
            }

            var accessX = nx % lines.Count;

            if (lines[accessX][ny] == '#')
            {
                continue;
            }

            var valModifier = 0;

            if (lines[accessX][ny] == '+')
            {
                valModifier = 1;
            }
            else if (lines[accessX][ny] == '-')
            {
                valModifier = -2;
            }
            else
            {
                valModifier = -1;
            }

            var nVal = value + valModifier;

            if (nVal >= 0)
            {
                maxSouth = Math.Max(maxSouth, nx);
            }

            if (nVal > 0)
            {
                if (newDict.ContainsKey(neighbour))
                {
                    newDict[neighbour] = Math.Max(newDict[neighbour], nVal);
                }
                else
                {
                    newDict[neighbour] = nVal;
                }

            }
        }
    }

    dict = newDict;

    if (dict.Any())
    {
        var maxVal = dict.Max(x => x.Value);

        foreach (var entry in dict)
        {
            var value = entry.Value;

            if (value < maxVal - 5)
            {
                dict.Remove(entry.Key);
            }
        }

        var maxTempSouth = dict.Max(x => x.Key.Item1);

        foreach (var entry in dict)
        {
            var (x, _, _) = entry.Key;
            var value = entry.Value;

            if (x < maxTempSouth - lines.Count)
            {
                dict.Remove(entry.Key);
            }
        }
    }
}

Console.WriteLine(maxSouth);

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}