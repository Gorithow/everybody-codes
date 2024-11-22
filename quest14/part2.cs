var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var segments = new HashSet<(long x,long y, long z)>();

foreach (var l in lines)
{
    var commands = l.Split(',');

    var p = (x: 0L, y: 0L, z: 0L);

    foreach (var command in commands)
    {
        var direction = command[0];
        var distance = long.Parse(command.Substring(1));

        switch (direction)
        {
            case 'U':
                for (int i = 0; i < distance; i++)
                {
                    p = (p.x, p.y + 1, p.z);
                    segments.Add(p);
                }
                break;
            case 'D':
                for (int i = 0; i < distance; i++)
                {
                    p = (p.x, p.y - 1, p.z);
                    segments.Add(p);
                }
                break;
            case 'R':
                for (int i = 0; i < distance; i++)
                {
                    p = (p.x + 1, p.y, p.z);
                    segments.Add(p);
                }
                break;
            case 'L':
                for (int i = 0; i < distance; i++)
                {
                    p = (p.x - 1, p.y, p.z);
                    segments.Add(p);
                }
                break;
            case 'F':
                for (int i = 0; i < distance; i++)
                {
                    p = (p.x, p.y, p.z + 1);
                    segments.Add(p);
                }
                break;
            case 'B':
                for (int i = 0; i < distance; i++)
                {
                    p = (p.x, p.y, p.z - 1);
                    segments.Add(p);
                }
                break;
            default:
                break;
        }
    }
}

Console.WriteLine(segments.Count);