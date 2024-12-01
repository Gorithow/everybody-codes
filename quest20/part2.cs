var line = Console.ReadLine();
var lines = new List<string>();

while (line != null && line != "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var start = (0, 0, Direction.Down, Checkpoint.A);

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == 'S')
        {
            start = (i, j, Direction.Down, Checkpoint.A);
        }
    }
}

var seconds = int.MaxValue;

var dict = new Dictionary<(int, int, Direction, Checkpoint), int>();
var initialAltitude = 10000;

dict[start] = initialAltitude;


for (int i = 0; i < seconds; i++)
{
    var newDict = new Dictionary<(int, int, Direction, Checkpoint), int>();

    foreach (var entry in dict)
    {
        var (x, y, dir, c) = entry.Key;
        var value = entry.Value;

        var neighbours = new List<(int, int, Direction, Checkpoint)>()
        {
            (x - 1, y, Direction.Up, c),
            (x + 1, y, Direction.Down, c),
            (x, y - 1, Direction.Left, c),
            (x, y + 1, Direction.Right, c)
        };

        if (dir == Direction.Up)
        {
            neighbours.Remove((x + 1, y, Direction.Down, c));
        }
        else if (dir == Direction.Down)
        {
            neighbours.Remove((x - 1, y, Direction.Up, c));
        }
        else if (dir == Direction.Left)
        {
            neighbours.Remove((x, y + 1, Direction.Right, c));
        }
        else if (dir == Direction.Right)
        {
            neighbours.Remove((x, y - 1, Direction.Left, c));
        }

        foreach (var neighbour in neighbours)
        {
            var (nx, ny, ndir, nc) = neighbour;

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

            if (lines[nx][ny] == 'A' && nc == Checkpoint.A)
            {
                nc = Checkpoint.B;
            }
            else if (lines[nx][ny] == 'B' && nc == Checkpoint.B)
            {
                nc = Checkpoint.C;
            }
            else if (lines[nx][ny] == 'C' && nc == Checkpoint.C)
            {
                nc = Checkpoint.S;
            }
            else if (lines[nx][ny] == 'S' && nc == Checkpoint.S && nVal >= 10000)
            {
                Console.WriteLine(i + 1);
                return;
            }

            if (newDict.ContainsKey((nx, ny, ndir, nc)))
            {
                newDict[(nx, ny, ndir, nc)] = Math.Max(newDict[(nx, ny, ndir, nc)], nVal);
            }
            else
            {
                newDict[(nx, ny, ndir, nc)] = nVal;
            }
        }
    }

    dict = newDict;
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum Checkpoint
{
    A,
    B,
    C,
    S
}