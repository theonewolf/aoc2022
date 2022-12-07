foreach (string line in System.IO.File.ReadLines(@"./input")) {
    Console.WriteLine(StartMarkerPosition(line, 14));
}

int StartMarkerPosition(string recorded, int length) {
    for (int i = 0; i < recorded.Length - length; i++) {
        if (recorded.Substring(i, length).Distinct().Count() == length) {
            return i + length;
        }
    }

    throw new KeyNotFoundException("Could not find start position.");
}