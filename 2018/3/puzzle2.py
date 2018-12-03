import re
from collections import Counter

with open("2018/3/input.txt", "r") as f:
    lines = [line.strip() for line in f.readlines()]
    claims = [[*map(int, re.findall(r"-?\d+", line))] for line in lines]
    get_coords = lambda c: [(i, j) for i in range(c[1], c[1] + c[3]) for j in range(c[2], c[2] + c[4])]
    coords = Counter(c for claim in claims for c in get_coords(claim))
    print(next(claim[0] for claim in claims if all(coords[c] == 1 for c in get_coords(claim))))
