List<List<int>> elves = new List<List<int>>();

List<int> current_elf = new List<int>();
foreach (string line in System.IO.File.ReadLines(@"./input1")) {
    if (line != "") {
        current_elf.Add(int.Parse(line));
    } else {
        elves.Add(current_elf);
        current_elf = new List<int>();
    }
}

int max = elves.Max(x => x.Sum());
Console.WriteLine(max);