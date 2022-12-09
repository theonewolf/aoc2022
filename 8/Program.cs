List<List<int>> grid = new List<List<int>>();

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var row = new List<int>();

    foreach (var c in line) {
        row.Add((int) Char.GetNumericValue(c));
    }

    grid.Add(row);
}

foreach (var row in grid) {
    foreach (var tree in row) {
        Console.Write(tree);
    }
    Console.WriteLine();
}

int total = 0;

for (int i = 0; i < grid.Count(); i ++) {
    for (int j = 0; j < grid[0].Count(); j++) {
        if (IsVisible(i, j, grid)) {
            total++;
        }
    }
}

bool IsVisible(int x, int y, List<List<int>> grid) {
    int tree = grid[y][x];
    bool blocked = false;

    // Edges always visible
    if (x == 0 || y == 0 || x == grid[0].Count() - 1 || y == grid.Count() - 1) {
        return true;
    }

    // Check top
    for (int i = y - 1; i >= 0; i--) {
        if (grid[i][x] >= tree) {
            blocked = true;
        }
    }

    if (!blocked) {
        return true;
    }

    // Check bottom
    blocked = false;
    for (int i = y + 1; i < grid.Count(); i++) {
        if (grid[i][x] >= tree) {
            blocked = true;
        }
    }

    if (!blocked) {
        return true;
    }

    // Check right
    blocked = false;
    for (int i = x + 1; i < grid[0].Count(); i++) {
        if (grid[y][i] >= tree) {
            blocked = true;
        }
    }

    if (!blocked) {
        return true;
    }

    // Check left
    blocked = false;
    for (int i = x - 1; i >= 0; i--) {
        if (grid[y][i] >= tree) {
            blocked = true;
        }
    }

    if (!blocked) {
        return true;
    }

    Console.WriteLine($"Not visible tree {tree} at: {x},{y}");
    return false;
}

Console.WriteLine($"Total visible trees: {total}");