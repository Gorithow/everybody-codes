var r = 0L;

var line = Console.ReadLine();
var meteors = new List<(int, int)>();

var A = (0, 0);
var B = (0, 1);
var C = (0, 2);

while (line != null && line != "")
{
    var meteor = line.Split(' ').Select(x => int.Parse(x)).ToList();
    meteors.Add((meteor[0], meteor[1]));

    line = Console.ReadLine();
}

var time = 0;
var results = new Dictionary<(int, int), (int, int)>();

while (meteors.Any())
{
    var shot = new HashSet<(int, int)>();

    foreach (var meteor in meteors)
    {
        var p = meteor.Item1;
        var h = meteor.Item2;

        for (int power = 0; power < Math.Min(p, h); power++)
        {
            int xh;

            xh = checkFrom(C.Item2, C.Item1, power, h - time, p - time);

            if (xh != -1)
            {
                updateResults(p, h, xh, power * 3);
            }

            xh = checkFrom(B.Item2, B.Item1, power, h - time, p - time);
            
            if (xh != -1)
            {
                updateResults(p, h, xh, power * 2);
            }

            xh = checkFrom(A.Item2, A.Item1, power, h - time, p - time);

            if (xh != -1)
            {
                updateResults(p, h, xh, power * 1);
            }
        }
    }

    time++;

    meteors = meteors
        .Where(x => x.Item1 - time > 0 && x.Item2 - time >= 0)
        .Where(x => !results.ContainsKey((x.Item1, x.Item2)) || (x.Item2 - time) < results[(x.Item1, x.Item2)].Item1)
        .ToList();
}

r = results.Sum(x => x.Value.Item2);

Console.WriteLine(r);

void updateResults(int p, int h, int xh, int power)
{
    if (results.ContainsKey((p, h)))
    {
        if (results[(p, h)].Item1 > xh)
        {
            results[(p, h)] = (xh, power);
        }
        else if (results[(p, h)].Item1 == xh)
        {
            results[(p, h)] = (xh, Math.Min(results[(p, h)].Item2, power));
        }
    }
    else
    {
        results.Add((p, h), (xh, power));
    }
}

int checkFrom(int h, int p, int power, int mh, int mp)
{
    for (int k = 0; k < power; k++)
    {
        h++;
        p++;
        mh--;
        mp--;

        if (h == mh && p == mp)
        {
            return mh;
        }
    }

    for (int k = 0; k < power; k++)
    {
        p++;
        mh--;
        mp--;

        if (h == mh && p == mp)
        {
            return mh;
        }
    }

    while (h > 0)
    {
        h--;
        p++;
        mh--;
        mp--;

        if (h == mh && p == mp)
        {
            return mh;
        }
    }

    return -1;
}