var blocks = 20240000L;

var usedBlocks = 0L;
var width = -1L;
var thickness = 0L;

var priests = long.Parse(Console.ReadLine());
var acolytes = 1111;

while (blocks > usedBlocks)
{
    width += 2;
    thickness = getThickness(thickness);
    usedBlocks += width * thickness;
}

var missingBlocks = usedBlocks - blocks;

var r = missingBlocks * width;

Console.WriteLine(r);

long getThickness(long prevThickness)
{
    if (prevThickness == 0)
    {
        return 1;
    }

    return priests * prevThickness % acolytes;
}
