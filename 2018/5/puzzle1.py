with open("2018/5/input.txt", "r") as f:
    line = f.readline().strip()
    alphabeth = "".join([chr(ord("a") + i) for i in range(26)])
    a1 = ["".join(t) for t in zip(alphabeth, alphabeth.upper())]
    a2 = ["".join(t) for t in zip(alphabeth.upper(), alphabeth)]
    old_line = None
    while old_line != line:
        old_line = line
        for a in a1:
            line = line.replace(a, "")
        for a in a2:
            line = line.replace(a, "")
    print(len(line))
