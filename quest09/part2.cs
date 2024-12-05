var r = 0L;

var line = Console.ReadLine();
var lines = new List<int>();

while (line != null)
{
    lines.Add(int.Parse(line));
    line = Console.ReadLine();
}

var beetles = new List<int>() { 1, 3, 5, 10, 15, 16, 20, 24, 25, 30 };

foreach (var amount in lines)
{
    r += calculateBeetles(amount);
}

Console.WriteLine(r);

int calculateBeetles(int amount)
{
    var dp = new int[amount + 1];

    for (int i = 1; i <= amount; i++)
    {
        dp[i] = int.MaxValue - 1;
    }

    dp[0] = 0;

    for (int i = 1; i <= amount; i++)
    {
        foreach (var beetle in beetles)
        {
            if (i >= beetle && dp[i - beetle] + 1 < dp[i])
            {
                dp[i] = dp[i - beetle] + 1;
            }
        }
    }

    return dp[amount];
}
