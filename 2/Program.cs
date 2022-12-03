List<int> scores = new List<int>();
foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var row = line.Split();
    scores.Add(Score(row[0], row[1]));
}

// A Rock B Paper C Scissors
// X Rock Y Paper Z Scissors

// A beats C
// B beats A
// C beats B

int Score(string other, string me) {
    Dictionary<string, string> decryptor = new Dictionary<string, string> {{"X","A"},{"Y","B"},{"Z","C"}};
    Dictionary<string, int> score = new Dictionary<string, int> {{"A", 1}, {"B", 2}, {"C", 3}};

    me = decryptor[me];

    // draw
    if (other == me) {
        return 3 + score[me];
    }
    
    // lose
    if ((other == "A" && me == "C") ||
        (other == "B" && me == "A") ||
        (other == "C" && me == "B")) {
        return score[me];
    }

    // win
    if ((me == "A" && other == "C") ||
        (me == "B" && other == "A") ||
        (me == "C" && other == "B")) {
        return 6 + score[me];
    }

    Console.WriteLine($"other={other}, me={me}");
    throw new Exception("Woah, unaccounted for game!");
}

// A Rock, B Paper, C Scissors
// X lose, Y draw, Z win

// A,X - C
// B,X - A
// C,X - B

// A,Y - A
// B,Y - B
// C,Y - C

// A,Z - B
// B,Z - C
// C,Z - A

int Score2(string other, string me) {
    Dictionary<(string, string), string> decryptor = new Dictionary<(string,string), string>
        {
            {("A","X"),"C"},
            {("B","X"),"A"},
            {("C","X"),"B"},

            {("A","Y"),"A"},
            {("B","Y"),"B"},
            {("C","Y"),"C"},

            {("A","Z"),"B"},
            {("B","Z"),"C"},
            {("C","Z"),"A"},


};
    Dictionary<string, int> score = new Dictionary<string, int> {{"A", 1}, {"B", 2}, {"C", 3}};

    me = decryptor[(other, me)];

    // draw
    if (other == me) {
        return 3 + score[me];
    }
    
    // lose
    if ((other == "A" && me == "C") ||
        (other == "B" && me == "A") ||
        (other == "C" && me == "B")) {
        return score[me];
    }

    // win
    if ((me == "A" && other == "C") ||
        (me == "B" && other == "A") ||
        (me == "C" && other == "B")) {
        return 6 + score[me];
    }

    Console.WriteLine($"other={other}, me={me}");
    throw new Exception("Woah, unaccounted for game!");
}

Console.WriteLine(scores.Sum());

List<int> scores2 = new List<int>();
foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var row = line.Split();
    scores2.Add(Score2(row[0], row[1]));
}

Console.WriteLine(scores2.Sum());