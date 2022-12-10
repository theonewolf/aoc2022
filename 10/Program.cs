int register_x = 1;
int cycle_count = 1;
int signal_strength = 0;

int target_cycle = 20;

List<List<char>> crt_screen = new List<List<char>>();

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var splitline = line.Split();
    var (instruction, value) = (splitline[0], 0);

    if (instruction == "addx") {
        value = int.Parse(splitline[1]);
    }

    Execute(instruction, value);
}

void ClearScreen() {
    crt_screen.Clear();

    foreach (var _ in Enumerable.Range(0, 6) {
            var row = new List<char>();
            crt_screen.Add(new List<char>());

            foreach (var __ in Enumerable.Range(0, 40)) {
                row.Add('.');
            }
    }
}

void PrintScreen() {
    foreach (var row in crt_screen) {
        foreach (var col in row) {
            Console.Write(col);
        }
        Console.WriteLine();
    }
}

void IncrementCounter() {
    if (cycle_count == target_cycle) {
        signal_strength += cycle_count * register_x;
        target_cycle += 40;
        Console.WriteLine($"Signal Strength: {signal_strength}, Cycle Count: {cycle_count}, Register x: {register_x}");

    }

    cycle_count++;
}

void Execute(string instruction, int arg) {
    switch(instruction) {
        case "noop":
            IncrementCounter();
            break;

        case "addx":
            IncrementCounter();
            IncrementCounter();
            register_x += arg;
            break;

        default:
           throw new NotImplementedException();
    }
}

Console.WriteLine($"FINAL | Signal Strength: {signal_strength}, Cycle Count: {cycle_count}, Register x: {register_x}");

ClearScreen();
PrintScreen();