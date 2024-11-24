
var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

lines[0] = lines[0].Replace('.', 'S');
var s = (0, lines[0].IndexOf('S'));

var letters = lines.Where(x => x.Any(c => char.IsLetter(c))).SelectMany(x => x.Where(c => char.IsLetter(c))).ToHashSet();
var lettersWithPoints = lines.SelectMany((x, i) => x.Select((c, j) => (c, (i, j))).Where(x => char.IsLetter(x.c))).ToList();

var lettersRoads = new Dictionary<(int, int), Dictionary<char, List<(int, int, int)>>>();

foreach (var letter in lettersWithPoints)
{
    var (c, (x, y)) = letter;

    var roads = new Dictionary<char, List<(int, int, int)>>();

    var queue = new Queue<(int, int)>();
    var visited = new HashSet<(int, int)>();
    var innerDists = new Dictionary<(int, int), int>();

    queue.Enqueue((x, y));
    innerDists[(x, y)] = 0;

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();

        if (visited.Contains(current))
        {
            continue;
        }

        visited.Add(current);

        var (i, j) = current;

        var neighbours = new List<(int, int)>()
                {
                    (i + 1, j),
                    (i - 1, j),
                    (i, j + 1),
                    (i, j - 1)
                };

        foreach (var n in neighbours)
        {
            if (n.Item1 < 0 || n.Item1 >= lines.Count || n.Item2 < 0 || n.Item2 >= lines[0].Length)
            {
                continue;
            }

            if (lines[n.Item1][n.Item2] == '#' || lines[n.Item1][n.Item2] == '~')
            {
                continue;
            }

            if (!innerDists.ContainsKey(n) || innerDists[n] > innerDists[current] + 1)
            {
                innerDists[n] = innerDists[current] + 1;
                queue.Enqueue(n);
            }

            if (char.IsLetter(lines[n.Item1][n.Item2]))
            {
                if (!roads.ContainsKey(lines[n.Item1][n.Item2]))
                {
                    roads[lines[n.Item1][n.Item2]] = new List<(int, int, int)>();
                }

                roads[lines[n.Item1][n.Item2]].Add((n.Item1, n.Item2, innerDists[n]));
            }
        }
    }

    lettersRoads[(x, y)] = roads;
}

var priorityQueue = new PriorityQueue<State, int>();

var initialState = new State(s.Item1, s.Item2, new HashSet<char> { 'S' });

priorityQueue.Enqueue(initialState, 0);

var dists = new Dictionary<State, int>();
dists[initialState] = 0;

var maxCount = 0;

var results = new List<(int, int, int)>();

while (priorityQueue.Count > 0)
{
    var current = priorityQueue.Dequeue();

    var (i, j, set) = current;

    if (set.Count == letters.Count)
    {
        results.Add((i, j, dists[current]));
        continue;
    }

    if (set.Count < maxCount - 1)
    {
        continue;
    }

    if (set.Count > maxCount)
    {
        maxCount = set.Count;
    }

    if (!lettersRoads.ContainsKey((i, j)))
    {
        continue;
    }

    foreach (var (letter, roads) in lettersRoads[(i, j)])
    {
        if (set.Contains(letter))
        {
            continue;
        }

        foreach (var road in roads)
        {
            var (x, y, dist) = road;

            var newSet = new HashSet<char>(set)
                    {
                        letter
                    };

            var newState = new State(x, y, newSet);

            if (!dists.ContainsKey(newState) || dists[newState] > dists[current] + dist)
            {
                dists[newState] = dists[current] + dist;
                priorityQueue.Enqueue(newState, dists[newState]);
            }
        }
    }
}

var bestResult = int.MaxValue;

foreach (var result in results)
{
    var toS = lettersRoads[(result.Item1, result.Item2)]['S'][0].Item3;

    if (result.Item3 + toS < bestResult)
    {
        bestResult = result.Item3 + toS;
    }
}

Console.WriteLine(bestResult);

public class State
{
    public int X { get; }
    public int Y { get; }
    public HashSet<char> Set { get; }

    public State(int x, int y, HashSet<char> set)
    {
        X = x;
        Y = y;
        Set = new HashSet<char>(set);
    }

    public override bool Equals(object? obj)
    {
        if (obj is State other)
        {
            return X == other.X && Y == other.Y && Set.SetEquals(other.Set);
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + X.GetHashCode();
        hash = hash * 31 + Y.GetHashCode();
        foreach (var c in Set)
        {
            hash = hash * 31 + c.GetHashCode();
        }
        return hash;
    }

    public void Deconstruct(out int x, out int y, out HashSet<char> set)
    {
        x = X;
        y = Y;
        set = Set;
    }
}
