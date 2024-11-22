var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var commands = lines[0].Split(',');

var maxH = 0L;
var h = 0L;

foreach (var command in commands)
{
    var direction = command[0];
    var distance = long.Parse(command.Substring(1));

    switch (direction)
    {
        case 'U':
            h += distance;
            break;
        case 'D':
            h -= distance;
            break;
        default:
            break;
    }

    if (h > maxH)
    {
        maxH = h;
    }
}

Console.WriteLine(maxH);