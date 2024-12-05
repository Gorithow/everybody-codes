var line = Console.ReadLine();

var r = 0;

foreach (var c in line)
{
    if (c == 'B')
    {
        r++;
    }
    else if (c == 'C')
    {
        r += 3;
    }
}

Console.WriteLine(r);