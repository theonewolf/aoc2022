Dictionary<char, int> value = new Dictionary<char, int> {
{'a', 1},
{'b', 2},
{'c', 3},
{'d', 4},
{'e', 5},
{'f', 6},
{'g', 7},
{'h', 8},
{'i', 9},
{'j', 10},
{'k', 11},
{'l', 12},
{'m', 13},
{'n', 14},
{'o', 15},
{'p', 16},
{'q', 17},
{'r', 18},
{'s', 19},
{'t', 20},
{'u', 21},
{'v', 22},
{'w', 23},
{'x', 24},
{'y', 25},
{'z', 26},
{'A', 27},
{'B', 28},
{'C', 29},
{'D', 30},
{'E', 31},
{'F', 32},
{'G', 33},
{'H', 34},
{'I', 35},
{'J', 36},
{'K', 37},
{'L', 38},
{'M', 39},
{'N', 40},
{'O', 41},
{'P', 42},
{'Q', 43},
{'R', 44},
{'S', 45},
{'T', 46},
{'U', 47},
{'V', 48},
{'W', 49},
{'X', 50},
{'Y', 51},
{'Z', 52}
};

List<int> scores = new List<int>();

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var compartment1 = line.Substring(0, (int) line.Length / 2);
    var compartment2 = line.Substring((int) line.Length / 2, (int) line.Length / 2);
    scores.Add(ScoreCompartments(compartment1, compartment2));
}

int ScoreCompartments(string compartment1, string compartment2) {
    foreach (char c in compartment1) {
        if (compartment2.Contains(c)) { 
            return value[c];
        }
    }
    throw new Exception("Impossible, the compartments are perfectly sorted!");
}

Console.WriteLine(scores.Sum());


List<List<string>> groups = new List<List<string>>();
List<string> group = new List<string>();
groups.Add(group);
int counter = 0;

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    group.Add(line);
    counter++;

    if (counter == 3) {
        group = new List<string>();
        groups.Add(group);
        counter = 0;
    }
}

List<int> groupScores = new List<int>();
foreach (List<string> g in groups) {
    if (g.Count != 0)
    {
        groupScores.Add(ScoreGroup(g));
    }
}

int ScoreGroup(List<string> group) {
    foreach (char c in group[0]) {
        if (group[1].Contains(c) && group[2].Contains(c)) { 
            return value[c];
        }
    }
    throw new Exception("Impossible, the group shared nothing!");
}

Console.WriteLine(groupScores.Sum());