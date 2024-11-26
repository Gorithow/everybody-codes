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

var steps = 100;

var state = moves.Select(x => 0).ToList();

for (int i = 1; i <= steps; i++)
{
    for (int j = 0; j < moves.Count; j++)
    {
        state[j] = (state[j] + moves[j]) % results[j].Count;
    }
}

var chars = string.Join(" ", state.Select((x, i) => results[i][x]));

Console.WriteLine(chars);