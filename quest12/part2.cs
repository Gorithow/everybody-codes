var r = 0L;

var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var Ah = 19;
var Bh = 18;
var Ch = 17;
var Ap = 1;
var Bp = 2;
var Cp = 3;

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Count(); j++)
    {
        if (lines[i][j] == 'A')
        {
            Ah = i;
        }
        else if (lines[i][j] == 'B')
        {
            Bh = i;
        }
        else if (lines[i][j] == 'C')
        {
            Ch = i;
        }
    }
}

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == 'T')
        {
            for (int p = 0; p < lines[i].Length; p++)
            {
                if (checkFrom(Ch, 1, p, i, j))
                {
                    r += Cp * p;
                    break;
                }
                else if (checkFrom(Bh, 1, p, i, j))
                {
                    r += Bp * p;
                    break;
                }

                else if (checkFrom(Ah, 1, p, i, j))
                {
                    r += Ap * p;
                    break;
                }
            }
        }
        else if (lines[i][j] == 'H')
        {
            for (int p = 0; p < lines[i].Length; p++)
            {
                if (checkFrom(Ch, 1, p, i, j))
                {
                    r += Cp * p * 2;
                    break;
                }
                else if (checkFrom(Bh, 1, p, i, j))
                {
                    r += Bp * p * 2;
                    break;
                }

                else if (checkFrom(Ah, 1, p, i, j))
                {
                    r += Ap * p * 2;
                    break;
                }
            }
        }
    }
}

Console.WriteLine(r);

bool checkFrom(int a, int b, int power, int i, int j)
{
    for (int k = 0; k < power; k++)
    {
        a--;
        b++;
    }

    if (a == i && b == j)
    {
        return true;
    }

    for (int k = 0; k < power; k++)
    {
        b++;

        if (a == i && b == j)
        {
            return true;
        }
    }

    while (b < lines[i].Length && a < lines.Count)
    {
        a++;
        b++;

        if (a == i && b == j)
        {
            return true;
        }
    }

    return false;
}