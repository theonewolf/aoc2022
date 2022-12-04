
int contains1 = 0;
int contains2 = 0;
foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var split = line.Split(",");
    var (elf1, elf2) = (ParseElf(split[0]), ParseElf(split[1]));

    if (Contains1(elf1, elf2)) {
        contains1++;
    }

    if (Contains2(elf1, elf2)) {
        contains2++;
    }
}

bool Contains1((int, int) e1, (int, int) e2) {
    return (e1.Item1 <= e2.Item1 && e1.Item2 >= e2.Item2) ||
           (e2.Item1 <= e1.Item1 && e2.Item2 >= e1.Item2);
}

bool Contains2((int, int) e1, (int, int) e2) {
    bool ret = false;

    // Start of 1 inside 2
    ret |= (e1.Item1 >= e2.Item1 && e1.Item1 <= e2.Item2);

    // Start of 2 inside 1
    ret |= (e2.Item1 >= e1.Item1 && e2.Item1 <= e1.Item2);

    // End of 1 inside 2
    ret |= (e1.Item2 >= e2.Item1 && e1.Item2 <= e2.Item2);

    // End of 2 inside 1
    ret |= (e2.Item2 >= e1.Item1 && e2.Item2 <= e1.Item2);    

    return ret;
}

(int, int) ParseElf(string elf) {
    var elf_split = elf.Split("-");
    return (int.Parse(elf_split[0]), int.Parse(elf_split[1]));
}

Console.WriteLine(contains1);
Console.WriteLine(contains2);