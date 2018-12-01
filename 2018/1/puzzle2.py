from itertools import accumulate, cycle

with open("2018/1/input.txt", "r") as f:
    numbers = [int(line) for line in f.readlines()]
    seen = set([0])
    print(next(n for n in accumulate(cycle(numbers)) if n in seen or seen.add(n)))
