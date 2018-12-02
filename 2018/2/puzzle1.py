from collections import Counter

with open("2018/2/input.txt", "r") as f:
    lines = [line.strip() for line in f.readlines()]
    count2 = 0
    count3 = 0
    for line in lines:
        has2 = False
        has3 = False
        for char, count in Counter(line).items():
            if count == 2 and not has2:
                count2 += 1
                has2 = True
            if count == 3 and not has3:
                count3 += 1
                has3 = True
    
    print(count2 * count3)
