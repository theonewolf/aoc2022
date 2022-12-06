using System.Collections;

// Parser: fixed length 3 chars
// Till we hit a line with a number in the second char position
// Then it's the number of columns
// Each column is a LIFO queue

List<Stack<char>> stacks = new List<Stack<char>>();
bool parsingColumns = true;
bool parsingInstructions = false;

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    Console.WriteLine(line);

    if (!parsingInstructions && line == "") {
        Console.WriteLine("Found separator for instructions.");
        parsingInstructions = true;
        continue;
    }

    if (parsingInstructions) {
        ParseInstruction(line, stacks);
        continue;
    }

    if (parsingColumns && line[1] == '1') {
        Console.WriteLine("Found list of columns.");
        parsingColumns = false;
        for (var i = 0; i < stacks.Count; i++) {
            stacks[i] = ReverseStack(stacks[i]);
        }
    }

    if (parsingColumns) {
        ParseColumns(line, stacks);
    }
}

Stack<char> ReverseStack(Stack<char> stack) {
    Stack<char> ret = new Stack<char>();

    // Assuming forach goes in Pop order.
    foreach (var s in stack) {
        ret.Push(s);
    }

    return ret;
}

void ParseInstruction(string line, List<Stack<char>> stacks)
{
    // move n from a to b
    var splitline = line.Split();
    var (n, a, b) = (splitline[1], splitline[3], splitline[5]);

    foreach (var _ in Enumerable.Range(0, int.Parse(n))) {
        stacks[int.Parse(b) - 1].Push(stacks[int.Parse(a) - 1].Pop());
    }
}

void ParseColumns(string line, List<Stack<char>> stacks)
{
    // Initialize stacks
    if (stacks.Count == 0) {
        foreach (var _ in Enumerable.Range(0, (int) Math.Ceiling((double) line.Length / (double) 4))) {
            stacks.Add(new Stack<char>());
        }
    }

    for (var i = 0; i < line.Length - 1; i += 4) {
        if (line[i + 1] != ' ') {
            stacks[i / 4].Push(line[i + 1]);
        }
    }
}

foreach (var stack in stacks) {
    Console.Write(stack.Pop());
}

Console.WriteLine();