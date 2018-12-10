import re

with open("2018/10/input.txt", "r") as f:
    lines = [[int(i) for i in re.findall(r'-?\d+', l)] for l in f.readlines()]
    range_x = max(l[0] for l in lines) - min(l[0] for l in lines)
    range_y = max(l[1] for l in lines) - min(l[1] for l in lines)
    s = 1
    while True:
        r_x = max(x + s * dx for (x, _, dx, _) in lines) - min(x + s * dx for (x, _, dx, _) in lines)
        r_y = max(y + s * dy for (_, y, _, dy) in lines) - min(y + s * dy for (_, y, _, dy) in lines)
        if r_x + r_y < range_x + range_y:
            range_x = r_x
            range_y = r_y
            s += 1
        else:
            break
    s -= 1
    min_x = min(x + s * dx for (x, _, dx, _) in lines)
    min_y = min(y + s * dy for (_, y, _, dy) in lines)
    output = [[" "] * (range_x + 1) for _ in range(range_y + 1)]
    for (x, y, dx, dy) in lines:
        output[(y + s * dy) - min_y][(x + s * dx) - min_x] = "#"
    for o in output:
        print("".join(o))
    print(s)
