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

var readMessage = false;

var message = new StringBuilder();

for (int i = 0; i < map.Count; i++)
{
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
}

Console.WriteLine(message.ToString());

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