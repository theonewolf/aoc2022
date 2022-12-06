foreach (string line in System.IO.File.ReadLines(@"./input")) {
    Console.WriteLine(StartMarkerPosition(line));
}

int StartMarkerPosition(string recorded) {
    // Sliding window for every 4 characters.
    // If all unique, report position after last unique char in the set of 4

    for (int i = 0; i < recorded.Length - 4; i++) {
        var (a,b,c,d) = (recorded[i], recorded[i+1], recorded[i+2], recorded[i+3]);
        if (a != b && a != c && a != d && b != c && b != d && c != d) {
            return i + 4;
        }
    }

    throw new KeyNotFoundException("Could not find start position.");
}