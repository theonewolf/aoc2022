Dictionary<(int, int), int> visited_locations1 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations2 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations3 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations4 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations5 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations6 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations7 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations8 = new Dictionary<(int, int), int>();
Dictionary<(int, int), int> visited_locations9 = new Dictionary<(int, int), int>();
int H1_x = 0, H1_y = 0, T1_x = 0, T1_y = 0;
int H2_x = 0, H2_y = 0, T2_x = 0, T2_y = 0;
int H3_x = 0, H3_y = 0, T3_x = 0, T3_y = 0;
int H4_x = 0, H4_y = 0, T4_x = 0, T4_y = 0;
int H5_x = 0, H5_y = 0, T5_x = 0, T5_y = 0;
int H6_x = 0, H6_y = 0, T6_x = 0, T6_y = 0;
int H7_x = 0, H7_y = 0, T7_x = 0, T7_y = 0;
int H8_x = 0, H8_y = 0, T8_x = 0, T8_y = 0;
int H9_x = 0, H9_y = 0, T9_x = 0, T9_y = 0;

foreach (string line in System.IO.File.ReadLines(@"./input")) {
    var splitline = line.Split();
    var (direction, count) = (splitline[0], int.Parse(splitline[1]));

    string subdirection;
    int subcount;
    for (int i = 0; i < count; i++) {
        (H1_x, H1_y, T1_x, T1_y, subdirection, subcount) = Simulate(H1_x, H1_y, T1_x, T1_y, direction, count, visited_locations1);
        (H2_x, H2_y, T2_x, T2_y, subdirection, subcount) = Simulate(H2_x, H2_y, T2_x, T2_y, subdirection, subcount, visited_locations2);
        (H3_x, H3_y, T3_x, T3_y, subdirection, subcount) = Simulate(H3_x, H3_y, T3_x, T3_y, subdirection, subcount, visited_locations3);
        (H4_x, H4_y, T4_x, T4_y, subdirection, subcount) = Simulate(H4_x, H4_y, T4_x, T4_y, subdirection, subcount, visited_locations4);
        (H5_x, H5_y, T5_x, T5_y, subdirection, subcount) = Simulate(H5_x, H5_y, T5_x, T5_y, subdirection, subcount, visited_locations5);
        (H6_x, H6_y, T6_x, T6_y, subdirection, subcount) = Simulate(H6_x, H6_y, T6_x, T6_y, subdirection, subcount, visited_locations6);
        (H7_x, H7_y, T7_x, T7_y, subdirection, subcount) = Simulate(H7_x, H7_y, T7_x, T7_y, subdirection, subcount, visited_locations7);
        (H8_x, H8_y, T8_x, T8_y, subdirection, subcount) = Simulate(H8_x, H8_y, T8_x, T8_y, subdirection, subcount, visited_locations8);
        (H9_x, H9_y, T9_x, T9_y, subdirection, subcount) = Simulate(H9_x, H9_y, T9_x, T9_y, subdirection, subcount, visited_locations9);
    }
}

(int, int, int, int, string, int) Simulate(int h_x, int h_y, int t_x, int t_y, string direction, int count, Dictionary<(int, int), int> locations)
{
    string subdirection = "";
    int subcount = 0;

    (h_x, h_y, t_x, t_y, subdirection, subcount) = Step(h_x, h_y, t_x, t_y, direction);
    if (!locations.ContainsKey((t_x, t_y))) {
        locations.Add((t_x, t_y), 1);
    } else {
        locations[(t_x, t_y)] += 1;
    }

    return (h_x, h_y, t_x, t_y, subdirection, subcount);
}

(int, int, int, int, string, int) Step(int h_x, int h_y, int t_x, int t_y, string direction)
{
    Console.WriteLine($"Direction: {direction}");
    string newdirection = "";
    int newcount = 0;

    switch (direction) {
        case "":
            return (h_x, h_y, t_x, t_y, newdirection, newcount);
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
        case "NW":
            h_x -= 1;
            h_y += 1;
            break;
        case "NE":
            h_x += 1;
            h_y += 1;
            break;
        case "SE":
            h_x += 1;
            h_y -= 1;
            break;
        case "SW":
            h_x -= 1;
            h_y -= 1;
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

            if (Math.Sign(d_x) == -1 &&
                Math.Sign(d_y) == -1) {
                    newdirection = "SW";
                    newcount = 1;
                } else if (Math.Sign(d_x) == -1 &&
                           Math.Sign(d_y) == 1) {
                            newdirection = "NW";
                } else if (Math.Sign(d_x) == 1 &&
                           Math.Sign(d_y) == 1) {
                            newdirection = "NE";
                } else if (Math.Sign(d_x) == 1 &&
                           Math.Sign(d_y) == -1) {
                            newdirection = "SE";
                } else {
                    throw new InvalidOperationException();
                }

        // Horizontal move (2 directions)
        } else if (Math.Abs(d_x) == 2) {
            t_x += Math.Sign(d_x) * 1;
            if (Math.Sign(d_x) == -1) {
                newdirection = "L";
                newcount = 1;
            } else if (Math.Sign(d_x) == 1) {
                newdirection = "R";
                newcount = 1;
            } else {
                throw new InvalidOperationException();
            }

        // Vertical move (2 directions)
        } else if (Math.Abs(d_y) == 2) {
            t_y += Math.Sign(d_y) * 1;
            if (Math.Sign(d_y) == -1) {
                newdirection = "D";
                newcount = 1;
            } else if (Math.Sign(d_y) == 1) {
                newdirection = "U";
                newcount = 1;
            } else {
                throw new InvalidOperationException();
            }
        } else {
            throw new NotImplementedException();
        }
    }

    return (h_x, h_y, t_x, t_y, newdirection, newcount);
}

foreach (var ((x, y), total) in visited_locations9) {
    Console.WriteLine($"(x={x}, y={y}) = {total}");
}

Console.WriteLine($"{visited_locations1.Count()}");
Console.WriteLine($"{visited_locations9.Count()}");