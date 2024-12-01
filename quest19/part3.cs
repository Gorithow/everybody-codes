using System.Text;

var line = Console.ReadLine();
var lines = new List<string>();

while (line != null)
{
    lines.Add(line);
    line = Console.ReadLine();
}

var instructions = lines[0];

var map = lines.Skip(2).Select(x => x.ToList()).ToList();

var rounds = 1048576000;

for (int k = 0; k < rounds; k++)
{
    var cN = 0;

    for (int i = 1; i < map.Count - 1; i++)
    {
        for (int j = 1; j < map[i].Count - 1; j++)
        {
            var command = instructions[cN];

            if (command == 'L')
            {
                RotateLeft(i, j);
            }
            else if (command == 'R')
            {
                RotateRight(i, j);
            }

            cN++;
            cN %= instructions.Length;
        }
    }

    if (map.FindIndex(x => x.Contains('>')) == map.FindIndex(x => x.Contains('<')))
    {
        var readMessage = false;
        var message = new StringBuilder();

        var i = map.FindIndex(x => x.Contains('>'));
        
        for (int j = 0; j < map[i].Count; j++)
        {
            if (map[i][j] == '>')
            {
                readMessage = true;
                continue;
            }

            if (map[i][j] == '<')
            {
                readMessage = false;
                continue;
            }

            if (readMessage)
            {
                message.Append(map[i][j]);
            }
        }

        var messageStr = message.ToString();

        if (messageStr.All(x => char.IsDigit(x)) && messageStr.Length > 0)
        {
            Console.WriteLine(messageStr);
            return;
        }
    }
}

void RotateLeft(int i, int j)
{
    var temp = map[i - 1][j];
    map[i - 1][j] = map[i - 1][j + 1];
    map[i - 1][j + 1] = map[i][j + 1];
    map[i][j + 1] = map[i + 1][j + 1];
    map[i + 1][j + 1] = map[i + 1][j];
    map[i + 1][j] = map[i + 1][j - 1];
    map[i + 1][j - 1] = map[i][j - 1];
    map[i][j - 1] = map[i - 1][j - 1];
    map[i - 1][j - 1] = temp;
}

void RotateRight(int i, int j)
{
    var temp = map[i - 1][j];
    map[i - 1][j] = map[i - 1][j - 1];
    map[i - 1][j - 1] = map[i][j - 1];
    map[i][j - 1] = map[i + 1][j - 1];
    map[i + 1][j - 1] = map[i + 1][j];
    map[i + 1][j] = map[i + 1][j + 1];
    map[i + 1][j + 1] = map[i][j + 1];
    map[i][j + 1] = map[i - 1][j + 1];
    map[i - 1][j + 1] = temp;
}