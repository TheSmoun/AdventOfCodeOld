import re

def dist(a, b):
    return sum(abs(a[i] - b[i]) for i in range(3))

with open("2018/23/input.txt", "r") as f:
    lines = [list(map(int, re.findall(r'-?\d+', line.strip()))) for line in f.readlines()]
    range_2_pos = {r: (x, y, z) for x, y, z, r in lines}

    max_range = max(range_2_pos.keys())
    max_range_pos = range_2_pos[max_range]
    print(len([pos for pos in range_2_pos.values() if dist(max_range_pos, pos) <= max_range]))
