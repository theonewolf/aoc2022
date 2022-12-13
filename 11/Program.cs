/*
Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3
*/

List<Monkey> monkeys = new List<Monkey>();
Monkey monkey = new Monkey();
monkey.Items = new List<ulong>();
var divisors = new List<ulong>();

bool DEBUG = false;

foreach (string line in System.IO.File.ReadLines(@"./sample_input")) {
    var trimmedline = line.Trim();

    if (trimmedline == "") {
        continue;
    }

    var splitline = line.Split(':');
    if (trimmedline.StartsWith("Monkey")) {
        monkey = new Monkey();
        monkey.Items = new List<ulong>();
    } else if (trimmedline.StartsWith("Starting")) {
        var valstrings = splitline[1].Split(',');

        foreach (var val in valstrings) {
            monkey.Items.Add(ulong.Parse(val));
        }
    } else if (trimmedline.StartsWith("Operation")) {
        monkey.Opcode = splitline[1][11];
        if (splitline[1].Substring(13) == "old") {
            monkey.Operand = -1;
        } else {
            monkey.Operand = long.Parse(splitline[1].Substring(13));
            divisors.Add((ulong) monkey.Operand);
        }
    } else if (trimmedline.StartsWith("Test")) {
        monkey.Test = ulong.Parse(splitline[1].Substring(14));
        divisors.Add(ulong.Parse(splitline[1].Substring(14)));
    } else if (trimmedline.StartsWith("If true")) {
        monkey.TrueTarget = int.Parse(splitline[1].Substring(17));
    } else if (trimmedline.StartsWith("If false")) {
        monkey.FalseTarget = int.Parse(splitline[1].Substring(17));
        monkeys.Add(monkey);
    } else {
        throw new InvalidOperationException();
    }
}

List<long> inspections = new List<long>();
foreach (var m in monkeys) {
    inspections.Add(0);
}

/*
Monkey 0:
  Monkey inspects an item with a worry level of 79.
    Worry level is multiplied by 19 to 1501.
    Monkey gets bored with item. Worry level is divided by 3 to 500.
    Current worry level is not divisible by 23.
    Item with worry level 500 is thrown to monkey 3.
  Monkey inspects an item with a worry level of 98.
    Worry level is multiplied by 19 to 1862.
    Monkey gets bored with item. Worry level is divided by 3 to 620.
    Current worry level is not divisible by 23.
    Item with worry level 620 is thrown to monkey 3.
*/
void RunSimulationStep(List<Monkey> themonkeys, ulong divisor) {
    for (int i = 0; i < themonkeys.Count; i++) {
        if (DEBUG) Console.WriteLine($"Monkey {i}:");
        var themonkey = themonkeys[i];
        for (int j = 0; j < themonkey.Items.Count; j++) {
            var item = themonkey.Items[j];
            var operand = themonkey.Operand == -1 ? item : (ulong) themonkey.Operand;
            if (DEBUG) Console.WriteLine($" . Monkey inspects an item with a worry level of {item}.");
            switch (themonkey.Opcode) {
                case '+':
                    item += operand;
                    if (DEBUG) Console.WriteLine($" .   Worry level is increases by {operand} to {item}.");
                    if (divisor == 1) {
                        foreach (var testDivisor in divisors) {
                            while (item % testDivisor == 0) {
                                if ((item / testDivisor) % testDivisor == 0) {
                                    Console.WriteLine("Removing divisor...");
                                    item /= testDivisor;
                                } else {
                                    break;
                                }
                            }
                        }
                    } else {
                        Console.WriteLine($"Dividing by {divisor}");
                        item /= divisor;
                    }
                    if (DEBUG) Console.WriteLine($" .   Monkey gets bored with item. Worry level is divided by 3 to {item}.");
                    break;
                case '*':
                    item *= operand;
                    if (DEBUG) Console.WriteLine($" .   Worry level is multiplied by {operand} to {item}.");
                    if (divisor == 1) {
                        foreach (var testDivisor in divisors) {
                            while (item % testDivisor == 0) {
                                if ((item / testDivisor) % testDivisor == 0) {
                                    Console.WriteLine("Removing divisor...");
                                    item /= testDivisor;
                                } else {
                                    break;
                                }
                            }
                        }
                    } else {
                        Console.WriteLine($"Dividing by {divisor}");
                        item /= divisor;
                    }
                    if (DEBUG) Console.WriteLine($" .   Monkey gets bored with item. Worry level is divided by 3 to {item}.");
                    break;
                default:
                    throw new InvalidDataException();
            }

            themonkey.Items[j] = item;

            // List ops will work and we don't have to replace the monkeys even though they are immutable
            // The lists are references and can be modified
            if (themonkey.Items[j] % themonkey.Test == 0) {
                var targetMonkey = themonkeys[themonkey.TrueTarget];
                targetMonkey.Items.Add(item);
                if (DEBUG) Console.WriteLine($" .   Current worry level is divisible by {themonkey.Test}.");
                if (DEBUG) Console.WriteLine($" .   Item with worry level {item} is thrown to monkey {themonkey.TrueTarget}.");
            } else {
                var targetMonkey = themonkeys[themonkey.FalseTarget];
                targetMonkey.Items.Add(item);
                if (DEBUG) Console.WriteLine($" .   Current worry level is not divisible by {themonkey.Test}.");
                if (DEBUG) Console.WriteLine($" .   Item with worry level {item} is thrown to monkey {themonkey.FalseTarget}.");
            }

            // Completed an inspection.
            inspections[i]++;
        }

        // Finally remove all items from the current monkey that were thrown to other monkeys
        themonkey.Items.Clear();
    }
}

// Run 20 rounds or 10,000 for part 2
// Optimization: keep it as a list of prime factors?
foreach (var round in Enumerable.Range(0, 10000)) {
    RunSimulationStep(monkeys, 1);
    if (round % 100 == 0) {
        Console.WriteLine($"Round -- {round} ({((decimal) round / 10000) * 100} percent complete)");
    }
}

for (int i = 0; i < inspections.Count; i++) {
    Console.WriteLine($"Monkey {i} inspected items {inspections[i]} times.");
}

inspections.Sort();
inspections.Reverse();
var topTwo = inspections.GetRange(0,2);

Console.WriteLine($"Top two inspectors had {topTwo[0]} and {topTwo[1]} inspections for {topTwo[0] * topTwo[1]} monkey business.");

struct Monkey {
    public List<ulong> Items;
    public char Opcode;
    public long Operand;
    public ulong Test;
    public int TrueTarget;
    public int FalseTarget;
}