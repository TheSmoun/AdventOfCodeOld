from collections import defaultdict, Counter

with open("2018/18/input.txt", "r") as f:
    m = defaultdict(lambda: "", {(x, y): c for y, line in enumerate(f.readlines()) for x, c in enumerate(line.strip())})
    minute = 0
    while minute < 10:
        r = defaultdict(lambda: "")
        for y in range(50):
            for x in range(50):
                counter = Counter()
                for dy in range(-1, 2):
                    for dx in range(-1, 2):
                        if dy == 0 and dx == 0:
                            continue
                        x1, y1 = x + dx, y + dy
                        if 0 <= x1 < 50 and 0 <= y1 < 50:
                            counter[m[(x1, y1)]] += 1
                if m[(x, y)] == ".":
                    r[(x, y)] = "|" if counter["|"] >= 3 else "."
                elif m[(x, y)] == "|":
                    r[(x, y)] = "#" if counter["#"] >= 3 else "|"
                elif m[(x, y)] == "#":
                    r[(x, y)] = "#" if (counter["#"] >= 1 and counter["|"] >= 1) else "."
        m = r
        minute += 1

    counter = Counter(m.values())
    print(counter["#"] * counter["|"])
