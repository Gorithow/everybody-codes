using System.Text;

var line = Console.ReadLine();
var lines = new List<string>();

while (line != null && line != "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var track = new List<char>();

var start = (0, 0);
var prev = (0, 0);
var next = (0, 1);

while (next != start)
{
    track.Add(lines[next.Item1][next.Item2]);

    var neighbours = new List<(int, int)>()
    {
        (next.Item1 - 1, next.Item2),
        (next.Item1 + 1, next.Item2),
        (next.Item1, next.Item2 - 1),
        (next.Item1, next.Item2 + 1)
    };

    var nextNeighbour = neighbours
        .Where(x => x != prev)
        .Where(x => x.Item1 >= 0 && x.Item1 < lines.Count)
        .Where(x => x.Item2 >= 0 && x.Item2 < lines[0].Length)
        .FirstOrDefault(x => lines[x.Item1][x.Item2] != ' ');

    prev = next;
    next = nextNeighbour;
}

track.Add(lines[start.Item1][start.Item2]);

var trackLength = track.Count;
var loops = 2024;

line = Console.ReadLine();

var rivalCommands = line
    .Split(':')[1]
    .Split(',')
    .Aggregate((a, b) => a + b);

var rivalEssence = checkPoints(rivalCommands);

var r = checkCommands(new StringBuilder(), 5, 3, 3);

Console.WriteLine(r);

long checkPoints(string commands)
{
    var power = 10;
    long essence = 0;

    for (int i = 0; i < trackLength * loops; i++)
    {
        var c = commands[i % commands.Length];
        var t = track[i % track.Count];

        if (t == '+')
        {
            power++;
        }
        else if (t == '-')
        {
            power--;
        }
        else if (c == '+')
        {
            power++;
        }
        else if (c == '-')
        {
            power--;
        }

        essence += power;
    }

    return essence;

}

long checkCommands(StringBuilder commandsBuilder, int plusLeft, int minusLeft, int equalLeft)
{
    if (plusLeft == 0 && minusLeft == 0 && equalLeft == 0)
    {
        var commands = commandsBuilder.ToString();

        if (commands == rivalCommands)
        {
            return 0;
        }

        var essence = checkPoints(commands);

        return essence > rivalEssence ? 1 : 0;
    }

    var winning = 0L;

    if (plusLeft > 0)
    {
        commandsBuilder.Append('+');
        winning += checkCommands(commandsBuilder, plusLeft - 1, minusLeft, equalLeft);
        commandsBuilder.Remove(commandsBuilder.Length - 1, 1);
    }

    if (minusLeft > 0)
    {
        commandsBuilder.Append('-');
        winning += checkCommands(commandsBuilder, plusLeft, minusLeft - 1, equalLeft);
        commandsBuilder.Remove(commandsBuilder.Length - 1, 1);
    }

    if (equalLeft > 0)
    {
        commandsBuilder.Append('=');
        winning += checkCommands(commandsBuilder, plusLeft, minusLeft, equalLeft - 1);
        commandsBuilder.Remove(commandsBuilder.Length - 1, 1);
    }

    return winning;
}