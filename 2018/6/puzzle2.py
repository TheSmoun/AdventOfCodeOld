from collections import Counter

with open("2018/6/input.txt", "r") as f:
    coords = [tuple(map(int, line.split(", "))) for line in f.readlines()]
    min_x = min(coords, key=lambda c: c[0])[0]
    max_x = max(coords, key=lambda c: c[0])[0]
    min_y = min(coords, key=lambda c: c[1])[1]
    max_y = max(coords, key=lambda c: c[1])[1]
    dist = lambda i, j, c: abs(i - c[0]) + abs(j - c[1])
    dists = {}
    for i in range(min_x, max_x + 1):
        for j in range(min_y, max_y + 1):
            dists[i, j] = sum(dist(i, j, c) for c in coords)
    print(len([v for v in dists.values() if v < 10000]))
