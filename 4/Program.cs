
int contains = 0;
foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var split = line.Split(",");
    var (elf1, elf2) = (ParseElf(split[0]), ParseElf(split[1]));

    Console.WriteLine($"elf1={elf1}, elf2={elf2}");

    if (Contains(elf1, elf2)) {
        contains++;
    }
}

bool Contains((int, int) e1, (int, int) e2) {
    return (e1.Item1 <= e2.Item1 && e1.Item2 >= e2.Item2) ||
           (e2.Item1 <= e1.Item1 && e2.Item2 >= e1.Item2);
}

(int, int) ParseElf(string elf) {
    var elf_split = elf.Split("-");
    return (int.Parse(elf_split[0]), int.Parse(elf_split[1]));
}

Console.WriteLine(contains);