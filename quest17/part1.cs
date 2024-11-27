var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var stars = new List<(int, int)>();

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '*')
        {
            stars.Add((i, j));
        }
    }
}

var edges = new List<Edge>();

for (int i = 0; i < stars.Count; i++)
{
    for (int j = i + 1; j < stars.Count; j++)
    {
        int dist = GetDist(stars[i], stars[j]);
        edges.Add(new Edge(stars[i], stars[j], dist));
    }
}

Console.WriteLine(PrimDijkstra(stars, edges) + stars.Count);

int PrimDijkstra(List<(int, int)> stars, List<Edge> edges)
{
    var connected = new HashSet<(int, int)>();
    var priorityQueue = new PriorityQueue<Edge, int>();
    var edgesByNode = new Dictionary<(int, int), List<Edge>>();

    foreach (var edge in edges)
    {
        if (!edgesByNode.ContainsKey(edge.Start))
        {
            edgesByNode[edge.Start] = new List<Edge>();
        }

        if (!edgesByNode.ContainsKey(edge.End))
        {
            edgesByNode[edge.End] = new List<Edge>();
        }

        edgesByNode[edge.Start].Add(edge);
        edgesByNode[edge.End].Add(edge);
    }

    var connectionCost = 0;

    var start = stars[0];
    connected.Add(start);

    foreach (var edge in edges.Where(e => e.Start == start || e.End == start))
    {
        priorityQueue.Enqueue(edge, edge.Distance);
    }

    while (connected.Count < stars.Count)
    {
        if (priorityQueue.Count == 0)
        {
            break;
        }

        var edge = priorityQueue.Dequeue();

        if (connected.Contains(edge.Start) && connected.Contains(edge.End))
        {
            continue;
        }

        connectionCost += edge.Distance;

        var newNode = connected.Contains(edge.Start) ? edge.End : edge.Start;
        connected.Add(newNode);

        foreach (var newEdge in edgesByNode[newNode])
        {
            if (connected.Contains(newEdge.Start) && connected.Contains(newEdge.End))
            {
                continue;
            }

            priorityQueue.Enqueue(newEdge, newEdge.Distance);
        }
    }

    return connectionCost;
}

int GetDist((int, int) a, (int, int) b)
{
    return Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
}
    
class Edge
{
    public (int, int) Start { get; }
    public (int, int) End { get; }
    public int Distance { get; }

    public Edge((int, int) start, (int, int) end, int distance)
    {
        Start = start;
        End = end;
        Distance = distance;
    }
}
