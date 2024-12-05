var line = Console.ReadLine();

var r = 0;

for (int i = 0; i < line.Length; i++)
{
    if (line[i] == 'B')
    {
        r += 1;
    }
    else if (line[i] == 'C')
    {
        r += 3;
    }
    else if (line[i] == 'D')
    {
        r += 5;
    }

    if (i % 3 == 0)
    {
        var creatures = line.Substring(i, 3).Where(x => x != 'x').ToList();

        if (creatures.Count == 3)
        {
            r += 6;
        }
        else if (creatures.Count == 2)
        {
            r += 2;
        }
    }
}

Console.WriteLine(r);