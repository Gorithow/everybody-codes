var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var palmsCount = lines.SelectMany(x => x).Count(x => x == 'P');


for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '.' && (i == 0 || i == lines.Count - 1 || j == 0 || j == lines[i].Length - 1))
        {
            Console.WriteLine(CalculateTime((i, j)));
            return;
        }
    }
}

int CalculateTime((int, int) start)
{
    var visited = new HashSet<(int, int)>();
    var queue = new Queue<(int, int)>();

    queue.Enqueue(start);

    var time = 0;

    var count = palmsCount;

    while (queue.Count > 0)
    {
        var newQueue = new Queue<(int, int)>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visited.Add(current);

            if (lines[current.Item1][current.Item2] == 'P')
            {
                count--;
            }

            if (count == 0)
            {
                return time;
            }

            var neighbours = new List<(int, int)>
            {
                (current.Item1 - 1, current.Item2),
                (current.Item1 + 1, current.Item2),
                (current.Item1, current.Item2 - 1),
                (current.Item1, current.Item2 + 1)
            };

            foreach (var neighbour in neighbours)
            {
                if (neighbour.Item1 >= 0 && neighbour.Item1 < lines.Count &&
                    neighbour.Item2 >= 0 && neighbour.Item2 < lines[0].Length &&
                    lines[neighbour.Item1][neighbour.Item2] != '#' &&
                    !visited.Contains(neighbour))
                {
                    newQueue.Enqueue(neighbour);
                }
            }
        }

        queue = newQueue;
        time++;
    }

    return int.MaxValue;
}