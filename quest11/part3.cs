var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var dict = new Dictionary<string, List<string>>();

foreach (var l in lines)
{
    var ll = l.Split(':').ToList();
    var key = ll[0];
    var value = ll[1].Split(',').ToList();

    dict.Add(key, value);
}

var days = 20;

var keys = dict.Keys.ToList();

var min = long.MaxValue;
var max = 0L;

foreach (var k in keys)
{
    var termites = new Dictionary<string, long> { { k, 1 } };

    for (long i = 0; i < days; i++)
    {
        var newTermites = new Dictionary<string, long> { };

        foreach (var (k2, v) in termites)
        {
            if (dict.ContainsKey(k2))
            {
                foreach (var t in dict[k2])
                {
                    if (newTermites.ContainsKey(t))
                    {
                        newTermites[t] += v;
                    }
                    else
                    {
                        newTermites.Add(t, v);
                    }
                }
            }
        }

        termites = newTermites;
    }

    min = Math.Min(min, termites.Sum(t => t.Value));
    max = Math.Max(max, termites.Sum(t => t.Value));
}

Console.WriteLine(max - min);