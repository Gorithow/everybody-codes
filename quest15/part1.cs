var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var s = (0, lines[0].IndexOf('.'));

var queue = new Queue<(int, int)>();
queue.Enqueue(s);

var visited = new HashSet<(int, int)>();
var dists = new Dictionary<(int, int), int>();
dists.Add(s, 0);

while (queue.Any())
{
    var current = queue.Dequeue();

    if (visited.Contains(current))
    {
        continue;
    }

    visited.Add(current);

    var (x, y) = current;

    var neighbours = new List<(int, int)>()
    {
        (x + 1, y),
        (x - 1, y),
        (x, y + 1),
        (x, y - 1)
    };

    foreach (var n in neighbours)
    {
        if (n.Item1 < 0 || n.Item1 >= lines.Count || n.Item2 < 0 || n.Item2 >= lines[0].Length)
        {
            continue;
        }

        if (lines[n.Item1][n.Item2] == '#')
        {
            continue;
        }

        queue.Enqueue(n);

        if (!dists.ContainsKey(n))
        {
            dists.Add(n, dists[current] + 1);
        }
        if (lines[n.Item1][n.Item2] == 'H')
        {
            Console.WriteLine(dists[n] * 2);
            return;
        }
    }
}
