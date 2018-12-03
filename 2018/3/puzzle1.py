import re
from collections import Counter

with open("2018/3/input.txt", "r") as f:
    lines = [line.strip() for line in f.readlines()]
    claims = [[*map(int, re.findall(r"-?\d+", line))] for line in lines]
    coords = Counter(co for c in claims for co in [(i, j) for i in range(c[1], c[1] + c[3]) for j in range(c[2], c[2] + c[4])])
    print(sum(1 for v in coords.values() if v > 1))
