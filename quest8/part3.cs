var blocks = 202400000L;

var usedBlocks = 0L;
var fullBuild = 0L;
var width = -1L;
var thickness = 0L;

var priests = long.Parse(Console.ReadLine());
var acolytes = 10;

var thicknesses = new List<long>();

while (blocks > usedBlocks)
{
    width += 2;

    thickness = getThickness(thickness);
    thicknesses.Add(thickness);

    fullBuild += width * thickness;
    usedBlocks = calculateUsedBlocks();
}

var missingBlocks = usedBlocks - blocks;

Console.WriteLine(missingBlocks);

long getThickness(long prevThickness)
{
    if (prevThickness == 0)
    {
        return 1;
    }

    return (priests * prevThickness % acolytes) + acolytes;
}

long calculateUsedBlocks()
{
    var height = 0L;
    var prevHeight = 1L;
    var toRemove = 0L;

    for (var i = thicknesses.Count - 1; i >= 0; i--)
    {
        height += thicknesses[i];

        var leftForColumn = priests * width * height % acolytes;

        leftForColumn = Math.Min(leftForColumn, prevHeight - 1);
        prevHeight = height;

        if (i == 0)
        {
            toRemove += leftForColumn;
        }
        else
        {
            toRemove += leftForColumn * 2;
        }
    }

    return fullBuild - toRemove;
}