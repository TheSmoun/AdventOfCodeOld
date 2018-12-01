from itertools import cycle

with open("2018/1/input.txt", "r") as f:
    numbers = [int(line) for line in f.readlines()]
    seen_frequencies = set([0])
    prev = 0
    for n in cycle(numbers):
        prev += n
        if prev in seen_frequencies:
            print prev
            break
        else:
            seen_frequencies.add(prev)
