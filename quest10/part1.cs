var line = Console.ReadLine();
var lines = new List<string>();

while (line != null && line != "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var answer = new List<char>();

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines.Count; j++)
    {
        if (lines[i][j] != '.')
        {
            continue;
        }

        for (int t = 0; t < lines[i].Count(); t++)
        {
            var c = lines[t][j];

            if (c == '.' || c == '*')
            {
                continue;
            }

            if (lines[i].Contains(c))
            {
                answer.Add(c);
                break;
            }
        }
    }
}

var s = new string(answer.ToArray());
Console.WriteLine(s);