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

    if (i % 2 == 0 && line[i] != 'x' && line[i + 1] != 'x')
    {
        r += 2;
    }
}

Console.WriteLine(r);