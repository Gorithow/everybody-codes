var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var segments = new HashSet<(long x,long y, long z)>();
var leafs = new HashSet<(long x,long y, long z)>();

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

    leafs.Add(p);
}

var bestMurkiness = long.MaxValue;

for (int i = 1; i <= segments.Max(x => x.y); i++)
{
    if (!segments.Contains((0, i, 0)))
    {
        continue;
    }

    var newMurkiness = 0L;
    var visitedSegments = new HashSet<(long x,long y, long z)>();

    var q = new Queue<(long x,long y, long z)>();
    q.Enqueue((0, i, 0));

    var leafsToVisit = new HashSet<(long x,long y, long z)>(leafs);
    var d = 0;

    while (leafsToVisit.Any())
    {
        var nQ = new Queue<(long x,long y, long z)>();

        while (q.Any())
        {
            var current = q.Dequeue();
            
            if (visitedSegments.Contains(current))
            {
                continue;
            }

            visitedSegments.Add(current);

            if (leafsToVisit.Contains(current))
            {
                leafsToVisit.Remove(current);
                newMurkiness += d;
            }

            visitedSegments.Add(current);

            var neighbors = new List<(long x,long y, long z)>()
            {
                (current.x + 1, current.y, current.z),
                (current.x - 1, current.y, current.z),
                (current.x, current.y + 1, current.z),
                (current.x, current.y - 1, current.z),
                (current.x, current.y, current.z + 1),
                (current.x, current.y, current.z - 1)
            };

            foreach (var neighbor in neighbors)
            {
                if (segments.Contains(neighbor) && !visitedSegments.Contains(neighbor))
                {
                    nQ.Enqueue(neighbor);
                }
            }
        }

        q = nQ;
        d++;
    }

    if (newMurkiness < bestMurkiness)
    {
        bestMurkiness = newMurkiness;
    }
}

Console.WriteLine(bestMurkiness);