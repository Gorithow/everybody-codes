var line = Console.ReadLine();

var blocks = long.Parse(line);

var usedBlocks = 0L;
var width = -1L;

while (blocks > usedBlocks)
{
    width += 2;
    usedBlocks += width;
}

var missingBlocks = usedBlocks - blocks;

var r = missingBlocks * width;

Console.WriteLine(r);