var line = Console.ReadLine();
var lines = new List<string>();

while (line != null && line != "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var track = new List<char>();

for (int i = 1; i < lines[0].Count(); i++)
{
    track.Add(lines[0][i]);
}
for (int i = 1; i < lines.Count(); i++)
{
    track.Add(lines[i][lines[i].Count() - 1]);
}
for (int i = lines[0].Count() - 2; i >= 0; i--)
{
    track.Add(lines[lines.Count() - 1][i]);
}
for (int i = lines.Count() - 2; i >= 0; i--)
{
    track.Add(lines[i][0]);
}

var chariots = new Dictionary<string, int>();

var trackLength = track.Count;
var loops = 10;

line = Console.ReadLine();

while (line != null && line != "")
{
    var power = 10;
    var essence = 0;

    var segments = line.Split(':').ToList();

    var id = segments[0];
    var commands = segments[1].Split(',').ToList();

    for (int i = 0; i < trackLength * loops; i++)
    {
        var c = commands[i % commands.Count];
        var t = track[i % track.Count];

        if (t == '+')
        {
            power++;
        }
        else if (t == '-')
        {
            power--;
        }
        else if (c == "+")
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
