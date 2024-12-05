var line = Console.ReadLine();
var nails = new List<int>();

while (line != null && line != "")
{
    nails.Add(int.Parse(line));
    line = Console.ReadLine();
}

var min = nails.Min();

var r = nails.Select(n => n - min).Sum();

Console.WriteLine(r);