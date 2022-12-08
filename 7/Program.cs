// Key string = path, bool = is directory, int = size
SortedDictionary<string, (bool, int)> fileSystem = new SortedDictionary<string, (bool, int)>();

// Global path to manipulate CWD
string CWD = "";

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    Console.WriteLine(line);
    UpdateFileSystem(line, fileSystem);
}

void UpdateFileSystem(string line, SortedDictionary<string, (bool, int)> fileSystem)
{
    if (line == "") {
        return;
    }

    // command parsing
    if (line[0] == '$') {
        // cd
        Console.Write("Parsing a command: ");
        if (line[2] == 'c') {
            Console.Write("change dir ");
            if (line.EndsWith("..")) {
                Console.Write("up one | ");
                var lastindex = CWD.LastIndexOf('/');
                CWD = CWD.Substring(0, lastindex == 0 ? 1 : lastindex);
            } else {
                Console.Write("into | ");
                if (line[5] != '/') { // relative path
                    CWD += (CWD != "/") ? "/" + line.Substring(5) : line.Substring(5);
                } else { // absolute path
                    CWD = line.Substring(5);
                }
                // TODO: Insert dir if new
                fileSystem.Add(CWD, (true, 0));
            }
            Console.WriteLine(CWD);
        } else if (line[2] == 'l') {
            // nothing to really do
            Console.WriteLine("list dir");
        }
    } else if (line[0] == 'd') {
        // parse dir
        var dir = CWD == "/" ? CWD + line.Substring(4) : CWD + "/" + line.Substring(4);
        Console.WriteLine($"Found directory: {dir}");
        // do nothing?
    } else {
        // parse int or throw!
        var linesplit = line.Split();
        var (ssize, name) = (linesplit[0], linesplit[1]);
        var size = int.Parse(ssize);
        Console.WriteLine($"{name} size {size}");

        // TODO: Insert file
        var file = CWD == "/" ? CWD + name : CWD + "/" + name;
        fileSystem.Add(file, (false, size));
    }
}

SortedDictionary<string, int> dirSizes = new SortedDictionary<string, int>();

foreach (var (path, (isdir, size)) in fileSystem) {
    Console.WriteLine($"{path} | isdir={isdir}, size={size}");

    if (isdir) {
        // add dir to dirSizes
        dirSizes.Add(path, 0);
    } else {
        // add size to all dirs
        var lastindex = path.LastIndexOf('/');
        var dir = path.Substring(0, lastindex == 0 ? 1 : lastindex);
        while (dir != "") {
            dirSizes[dir] += size;
            if (dir == "/") break;

            lastindex = dir.LastIndexOf('/');
            dir = dir.Substring(0, lastindex == 0 ? 1 : lastindex);
        }
    }
}

var totalSize = 0;

foreach (var (path, size) in dirSizes) {
    Console.WriteLine($"{path} | size={size}");
    if (size <= 100000) {
        totalSize += size;
    }
}

Console.WriteLine($"Size sum: {totalSize}");

var diskSize = 70000000;
var free = diskSize - dirSizes["/"];
var targetFree = 30000000 - free;
var potentialDelete = int.MaxValue;
var potentialDeletePath = "";

foreach (var (path, size) in dirSizes) {
    if (size >= targetFree) {
        if (size < potentialDelete) {
            potentialDelete = size;
            potentialDeletePath = path;
        }
    }
}

Console.WriteLine($"Minimal size to delete: {potentialDelete} ({potentialDeletePath})");