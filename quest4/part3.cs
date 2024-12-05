var line = Console.ReadLine();
var nails = new List<int>();

while (line != null && line != "")
{
    nails.Add(int.Parse(line));
    line = Console.ReadLine();
}

var r = long.MaxValue;

foreach (var nail in nails)
{
    var sum = nails.Select(n => Math.Abs(n - nail)).Sum();

    r = Math.Min(r, sum);
}

Console.WriteLine(r);