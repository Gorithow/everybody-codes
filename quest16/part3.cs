var line = Console.ReadLine();
var lines = new List<string>();

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

var steps = 256;
var state = moves.Select(x => 0).ToList();

var states = new Dictionary<string, (long min, long max)>
{
    { string.Join(",", state), (0, 0) }
};

for (int i = 1; i <= steps; i++)
{
    var newStates = new Dictionary<string, (long min, long max)>();

    foreach (var s in states)
    {
        for (int k = -1; k < 2; k++)
        {
            var stateInts = s.Key.Split(',').Select(x => int.Parse(x)).ToList();

            for (int j = 0; j < moves.Count; j++)
            {
                stateInts[j] = (stateInts[j] + moves[j] + k) % results[j].Count;
            }

            var chars = string.Join("", stateInts.Select((x, i) => results[i][x].Substring(0, 1) + results[i][x].Substring(2, 1)));

            var points = chars
                .GroupBy(x => x)
                .Where(x => x.Count() >= 2)
                .Select(x => x.Count() - 2)
                .Sum();

            var stateStr = string.Join(",", stateInts);

            if (newStates.ContainsKey(stateStr))
            {
                newStates[stateStr] = (
                    Math.Min(newStates[stateStr].min, s.Value.min + points),
                    Math.Max(newStates[stateStr].max, s.Value.max + points));
            }
            else
            {
                newStates.Add(stateStr, (s.Value.min + points, s.Value.max + points));
            }
        }
    }

    states = newStates;
}

var min = states.Values.Min(x => x.min);
var max = states.Values.Max(x => x.max);

Console.WriteLine(max + " " + min);