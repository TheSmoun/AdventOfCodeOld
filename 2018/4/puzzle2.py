from datetime import datetime
from collections import Counter, defaultdict

with open("2018/4/input.txt", "r") as f:
    lines = [line.strip() for line in sorted(f.readlines())]
    guards = defaultdict(Counter)
    for time, state in [line.split("] ") for line in lines]:
        time = datetime.strptime(time, "[%Y-%m-%d %H:%M")
        if "begins shift" in state:
            id = int(state.split("#")[-1].split()[0])
        elif "falls asleep" in state:
            start = time
        elif "wakes up" in state:
            minutes = int((time - start).total_seconds() // 60)
            guards[id].update(Counter((start.minute + m) % 60 for m in range(minutes)))
    (_, minute), id = max((c.most_common()[0][::-1], id) for id, c in guards.items())
    print(id * minute)
