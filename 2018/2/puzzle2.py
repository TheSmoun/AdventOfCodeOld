from itertools import permutations

with open("2018/2/input.txt", "r") as f:
    lines = [line.strip() for line in f.readlines()]
    for w1,w2 in permutations(lines, 2):
        if len([c1 for c1,c2 in zip(w1, w2) if c1 != c2]) == 1:
            print("".join([c1 for c1,c2 in zip(w1, w2) if c1 == c2]))
            break
