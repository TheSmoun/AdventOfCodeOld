from collections import Counter

with open("2018/2/input.txt", "r") as f:
    lines = [line.strip() for line in f.readlines()]
    count2 = 0
    count3 = 0
    for counts in map(lambda l: Counter(l).items(), lines):
        if len([count for char, count in counts if count == 2]) > 0:
            count2 += 1
        if len([count for char, count in counts if count == 3]) > 0:
            count3 += 1
    
    print(count2 * count3)
