from collections import defaultdict, Counter

def run(m, mins = 1000000000):
    seen = {}
    max_mins = 1000000000

    for minute in range(mins):
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
        state = str(m)
        if state in seen:
            s = seen[state]
            max_mins = (max_mins - s - 1) % (minute - s)
            return run(m, max_mins)
        seen[state] = minute

    counter = Counter(m.values())
    return counter["#"] * counter["|"]

with open("2018/18/input.txt", "r") as f:
    m = defaultdict(lambda: "", {(x, y): c for y, line in enumerate(f.readlines()) for x, c in enumerate(line.strip())})
    print(run(m))
