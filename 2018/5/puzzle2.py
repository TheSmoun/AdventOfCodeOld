with open("2018/5/input.txt", "r") as f:
    line = f.readline().strip()
    alphabeth = "".join([chr(ord("a") + i) for i in range(26)])
    a1 = ["".join(t) for t in zip(alphabeth, alphabeth.upper())]
    a2 = ["".join(t) for t in zip(alphabeth.upper(), alphabeth)]
    length = len(line)
    for c in alphabeth:
        new_line = line.replace(c, "")
        new_line = new_line.replace(c.upper(), "")
        old_line = None
        while old_line != new_line:
            old_line = new_line
            for a in a1:
                new_line = new_line.replace(a, "")
            for a in a2:
                new_line = new_line.replace(a, "")
        if len(new_line) < length:
            length = len(new_line)
    print(length)
