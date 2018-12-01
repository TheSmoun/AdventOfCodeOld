from functools import reduce

with open("2017/1/input.txt", "r") as f:
    numbers = [int(i) for i in list(f.read())]
    s = 0

    prev = numbers[-1]
    for n0 in numbers:
        if n0 == prev:
            s += n0
        prev = n0

    print s
