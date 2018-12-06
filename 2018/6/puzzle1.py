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
            closest = min(coords, key=lambda c: dist(i, j, c))
            d = dist(i, j, closest)
            if len([c for c in coords if dist(i, j, c) == d]) == 1:
                dists[i, j] = closest
            else:
                dists[i, j] = -1

    infinites = set(dists[x, max_y] for x in range(min_x, max_x + 1))
    infinites = infinites.union(set(dists[x, min_y] for x in range(min_x, max_x + 1)))
    infinites = infinites.union(set(dists[min_x, y] for y in range(min_y, max_y + 1)))
    infinites = infinites.union(set(dists[max_x, y] for y in range(min_y, max_y + 1)))

    print(next(c[1] for c in Counter(dists.values()).most_common() if c[0] not in infinites))
