var line = Console.ReadLine();
var lines = new List<string>();

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var dict = new Dictionary<char, List<char>>();

foreach (var l in lines)
{
    var ll = l.Split(':').ToList();
    var key = ll[0][0];
    var value = ll[1].Split(',').Select(x => x[0]).ToList();

    dict.Add(key, value);
}

var days = 4;

var termites = new List<char>() { 'A' };

for (int i = 0; i < days; i++)
{
    var newTermites = new List<char>();

    foreach (var t in termites)
    {
        if (dict.ContainsKey(t))
        {
            newTermites.AddRange(dict[t]);
        }
    }

    termites = newTermites;
}

Console.WriteLine(termites.Count);