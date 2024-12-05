var line = Console.ReadLine();

line = line.Replace(" ", "");
var clappers = line.Select(c => new List<int>()).ToList();

while (line != null && line != "")
{
    line = line.Replace(" ", "");

    for (int i = 0; i < line.Length; i++)
    {
        var c = int.Parse(line[i].ToString());
        clappers[i].Add(c);
    }

    line = Console.ReadLine();
}

var rounds = 10;

for (int t = 0; t < rounds; t++)
{
    var i = t % clappers.Count;
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
            if (posY == clappers[posX].Count - 1)
            {
                dir = Dir.Up;
            }
            else
            {
                posY++;
            }
        }
        else if (dir == Dir.Up)
        {
            if (posY == 0)
            {
                dir = Dir.Down;
            }
            else
            {
                posY--;
            }
        }

        claps--;
    }

    if (dir == Dir.Down)
    {
        clappers[posX].Insert(posY, clapper);
    }
    else if (dir == Dir.Up)
    {
        var x = (posX - 1) < 0 ? clappers.Count - 1 : posX - 1;
        clappers[x].Insert(posY + 1, clapper);
    }
}

foreach (var clapper in clappers)
{
    Console.Write(clapper[0]);
}

Console.WriteLine();

enum Dir
{
    Up,
    Down
}