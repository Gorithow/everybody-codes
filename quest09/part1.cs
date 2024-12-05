var r = 0L;

var line = Console.ReadLine();
var lines = new List<int>();

while (line != null)
{
    lines.Add(int.Parse(line));
    line = Console.ReadLine();
}

var divs = new List<int>() { 1, 3, 5, 10 };

divs.Reverse();

for (int i = 0; i < lines.Count; i++)
{
    foreach (var div in divs)
    {
        r += lines[i] / div;
        lines[i] %= div;
    }
}
Console.WriteLine(r);