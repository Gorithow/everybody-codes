var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

(int, int) e = (0, 0);

var dists = new Dictionary<(int, int), int>();
var queue = new PriorityQueue<(int, int), int>();

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Count(); j++)
    {
        if (lines[i][j] == 'S')
        {
            dists.Add((i, j), 0);
            queue.Enqueue((i, j), 0);
        }
        else if (lines[i][j] == 'E')
        {
            e = (i, j);
        }
    }
}

var visited = new HashSet<(int, int)>();

while (queue.Count > 0)
{
    var pos = queue.Dequeue();

    if (visited.Contains(pos))
    {
        continue;
    }

    visited.Add(pos);

    if (pos == e)
    {
        Console.WriteLine(dists[pos]);
        break;
    }

    var neighbours = new List<(int, int)>()
    {
        (pos.Item1 - 1, pos.Item2),
        (pos.Item1 + 1, pos.Item2),
        (pos.Item1, pos.Item2 - 1),
        (pos.Item1, pos.Item2 + 1)
    };

    int h = Parse(lines[pos.Item1][pos.Item2].ToString());

    foreach (var n in neighbours)
    {
        if (n.Item1 < 0 || n.Item1 >= lines.Count || n.Item2 < 0 || n.Item2 >= lines[0].Count())
        {
            continue;
        }

        if (TryParse(lines[n.Item1][n.Item2].ToString(), out int nh))
        {
            var diff = Math.Min(Math.Abs(h - nh), 10 - Math.Abs(h - nh));
            var nDist = dists[pos] + diff + 1;

            if (!dists.ContainsKey(n) || nDist < dists[n])
            {
                dists[n] = nDist;
                queue.Enqueue(n, dists[n]);
            }
        }
    }
}

int Parse(string s)
{
    if (s == "S" || s == "E")
    {
        return 0;
    }

    return int.Parse(s);
}

bool TryParse(string s, out int i)
{
    if (s == "E")
    {
        i = 0;
        return true;
    }

    return int.TryParse(s, out i);
}