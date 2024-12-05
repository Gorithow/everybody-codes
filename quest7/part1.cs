var line = Console.ReadLine();

var chariots = new Dictionary<string, int>();
var trackLength = 10;

while (line != null && line != "")
{
    var power = 10;
    var essence = 0;

    var segments = line.Split(':').ToList();

    var id = segments[0];
    var commands = segments[1].Split(',').ToList();

    for (int i = 0; i < trackLength; i++)
    {
        var c = commands[i % commands.Count];

        if (c == "+")
        {
            power++;
        }
        else if (c == "-")
        {
            power--;
        }

        essence += power;
    }

    chariots[id] = essence;

    line = Console.ReadLine();
}

var result = string.Join("", chariots.OrderByDescending(x => x.Value).Select(x => x.Key));

Console.WriteLine(result);