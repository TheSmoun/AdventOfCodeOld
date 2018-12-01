import csv
from itertools import permutations
from functools import partial

with open("2017/2/input.csv", "r") as csvfile:
    reader = csv.reader(csvfile, delimiter="\t")
    rows = [[int(i) for i in row] for row in reader]
    f = partial(filter, lambda t: t[0] % t[1] == 0)
    m = partial(map, lambda t: t[0] / t[1])
    r = partial(reduce, lambda p, c: p + sum(c))
    print(r([m(f(permutations(row, 2))) for row in rows], 0))
