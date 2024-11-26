var line = Console.ReadLine();
var lines = new List<string>();

long r = 0L;

while (line != null || line == "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var moves = lines[0].Split(',').Select(x => int.Parse(x)).ToList();
var results = moves.Select(x => new List<string>()).ToList();

for (int i = 2; i < lines.Count; i++)
{
    for (int j = 0; j * 4 < lines[i].Length; j++)
    {
        var result = lines[i].Substring(j * 4, 3);

        if (result != "   ")
        {
            results[j].Add(result);
        }
    }
}

var steps = 202420242024;

var state = moves.Select(x => 0).ToList();

var set = new Dictionary<string, (long, long)>();

for (long i = 1; i <= steps; i++)
{
    for (int j = 0; j < moves.Count; j++)
    {
        state[j] = (state[j] + moves[j]) % results[j].Count;
    }

    var chars = string.Join("", state.Select((x, i) => results[i][x].Substring(0, 1) + results[i][x].Substring(2, 1)));
    
    var points = chars
        .GroupBy(x => x)
        .Where(x => x.Count() >= 2)
        .Select(x => x.Count() - 2)
        .Sum();

    r += points;

    var key = state.Select(x => x.ToString()).Aggregate((x, y) => x + "," + y);

    if (set.ContainsKey(key))
    {
        var dist = i - set[key].Item1;
        var remaining = steps - i;
        var cycles = remaining / dist;
        var stepsInCycles = cycles * dist;
        r += cycles * (r - set[key].Item2);
        i += stepsInCycles;

        set.Clear();
    }
    else
    {
        set[key] = (i, r);
    }
}

Console.WriteLine(r);