var line = Console.ReadLine();

var clappers = line.Split(' ').Select(c => new List<int>()).ToList();

while (line != null && line != "")
{
    var clappersInLine = line.Split(' ').Select(c => int.Parse(c)).ToList();
    for (int i = 0; i < clappersInLine.Count; i++)
    {
        clappers[i].Add(clappersInLine[i]);
    }

    line = Console.ReadLine();
}

var shoutNumbers = new Dictionary<string, int>();
var lookingForShoutNumber = 2024;

var round = 0;
var found = false;

while (!found)
{
    var i = round % clappers.Count;
    round++;
    var clapper = clappers[i][0];

    var posX = (i + 1) % clappers.Count;
    var posY = 0;
    var dir = Dir.Down;
    var claps = clapper - 1;

    clappers[i].RemoveAt(0);

    while (claps > 0)
    {
        if (dir == Dir.Down)
        {
            if (claps > clappers[posX].Count - 1)
            {
                posY = clappers[posX].Count - 1;
                dir = Dir.Up;
                claps -= clappers[posX].Count;
            }
            else
            {
                posY = claps;
                claps = 0;
            }
        }
        else if (dir == Dir.Up)
        {
            if (claps > clappers[posX].Count - 1)
            {
                posY = 0;
                dir = Dir.Down;
                claps -= clappers[posX].Count;
            }
            else
            {
                posY = clappers[posX].Count - 1 - claps;
                claps = 0;
            }
        }
    }

    if (dir == Dir.Down)
    {
        clappers[posX].Insert(posY, clapper);
    }
    else if (dir == Dir.Up)
    {
        clappers[posX].Insert(posY + 1, clapper);
    }

    var shout = clappers.Select(x => x[0].ToString()).Aggregate((x, y) => x + y);

    if (!shoutNumbers.ContainsKey(shout))
    {
        shoutNumbers.Add(shout, 1);
    }
    else
    {
        shoutNumbers[shout]++;

        if (shoutNumbers[shout] == lookingForShoutNumber)
        {
            found = true;
        }
    }
}

var shoutNumber = shoutNumbers.First(x => x.Value == lookingForShoutNumber).Key;
var r = long.Parse(shoutNumber) * round;

Console.WriteLine(r);

enum Dir
{
    Up,
    Down
}