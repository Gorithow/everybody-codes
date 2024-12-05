var line = Console.ReadLine();
var lines = new List<string>();

while (line != null && line != "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var tree = new Dictionary<string, List<string>>();

foreach (var l in lines)
{
    var split = l.Split(":");
    var a = split[0];
    var b = split[1];

    tree[a] = b.Split(",").ToList();
}

var startWith = "RR";

var queue = new Queue<(string, string)>();

queue.Enqueue((startWith, ""));

while (queue.Any())
{
    var currentQueue = queue;
    queue = new Queue<(string, string)>();

    var apples = new List<string>();

    while (currentQueue.Any())
    {
        var current = currentQueue.Dequeue();
        var currentName = current.Item1;

        if (tree.ContainsKey(currentName))
        {
            foreach (var n in tree[currentName])
            {
                queue.Enqueue((n, current.Item2 + currentName));
            }
        }
        else if (currentName == "@")
        {
            apples.Add(current.Item2 + currentName);
        }
    }

    if (apples.Count == 1)
    {
        Console.WriteLine(apples[0]);
        return;
    }
}