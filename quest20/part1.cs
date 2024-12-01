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

var seconds = 100;
var initialAltitude = 10000;

var dict = new Dictionary<(int, int, Direction), int>();

dict[start] = initialAltitude;

for (int i = 0; i < seconds; i++)
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
            var (nx, ny, ndir) = neighbour;

            if (nx < 0 || nx >= lines.Count || ny < 0 || ny >= lines[nx].Length)
            {
                continue;
            }

            if (lines[nx][ny] == '#')
            {
                continue;
            }

            var valModifier = 0;

            if (lines[nx][ny] == '+')
            {
                valModifier = 1;
            }
            else if (lines[nx][ny] == '-')
            {
                valModifier = -2;
            }
            else
            {
                valModifier = -1;
            }

            var nVal = value + valModifier;

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

    dict = newDict;
}

var max = dict.Values.Max();

Console.WriteLine(max);

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}