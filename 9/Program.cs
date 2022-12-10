Dictionary<(int, int), int> visited_locations = new Dictionary<(int, int), int>();
int H_x = 0, H_y = 0, T_x = 0, T_y = 0;

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var splitline = line.Split();
    var (direction, count) = (splitline[0], splitline[1]);

    (H_x, H_y, T_x, T_y) = Simulate(H_x, H_y, T_x, T_y, direction, int.Parse(count), visited_locations);
}

(int, int, int, int) Simulate(int h_x, int h_y, int t_x, int t_y, string direction, int count, Dictionary<(int, int), int> locations)
{
    for (int i = 0; i < count; i++) {
        (h_x, h_y, t_x, t_y) = Step(h_x, h_y, t_x, t_y, direction);
        if (!locations.ContainsKey((t_x, t_y))) {
            locations.Add((t_x, t_y), 1);
        } else {
            locations[(t_x, t_y)] += 1;
        }
    }

    return (h_x, h_y, t_x, t_y);
}

(int, int, int, int) Step(int h_x, int h_y, int t_x, int t_y, string direction)
{
    Console.WriteLine($"Direction: {direction}");

    switch (direction) {
        case "U":
            h_y += 1;
            break;
        case "D":
            h_y -= 1;
            break;
        case "R":
            h_x += 1;
            break;
        case "L":
            h_x -= 1;
            break;
        default:
            throw new NotImplementedException();
    }

    int d_x = h_x - t_x,
        d_y = h_y - t_y;
    
    int distance = (int) Math.Sqrt(d_x * d_x + d_y * d_y);

    Console.WriteLine($"d_x = {d_x}, d_y = {d_y}, Euclidean distance = {distance}");

    if (distance > 1) {
        // Diagonal move (4 directions)
        if (Math.Abs(d_x) >= 1 && Math.Abs(d_y) >= 1) {
            t_x += Math.Sign(d_x) * 1;
            t_y += Math.Sign(d_y) * 1;
        // Horizontal move (2 directions)
        } else if (Math.Abs(d_x) == 2) {
            t_x += Math.Sign(d_x) * 1;
        // Vertical move (2 directions)
        } else if (Math.Abs(d_y) == 2) {
            t_y += Math.Sign(d_y) * 1;
        } else {
            throw new NotImplementedException();
        }
    }

    return (h_x, h_y, t_x, t_y);
}

foreach (var ((x, y), total) in visited_locations) {
    Console.WriteLine($"(x={x}, y={y}) = {total}");
}

Console.WriteLine($"{visited_locations.Count()}");