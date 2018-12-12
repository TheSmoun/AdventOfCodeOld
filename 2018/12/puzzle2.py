from collections import deque

with open("2018/12/input.txt", "r") as f:
    lines = [line.strip() for line in f.readlines() if len(line.strip()) > 0]
    state = lines[0].split(" ")[-1]
    length = len(state)
    rules = {i:o for (i, o) in map(lambda l: l.split(" => "), lines[1:])}
    
    diff = (len(state) - length) // 2
    start = sum(i - diff for i, c in enumerate(state) if c == "#")
    prev = start
    diffs = []
    for _ in range(100):
        if not state.startswith("....") or not state.endswith("...."):
            state = "...." + state + "...."
        state = "".join(rules[state[i - 2: i + 3]] for i in range(2, len(state) - 2))
        diff = (len(state) - length) // 2
        s = sum(i - diff for i, c in enumerate(state) if c == "#")
        diffs.append(s - prev)
        prev = s
    
    print((50000000000 - 100) * diffs[-1] + sum(diffs) + start)
