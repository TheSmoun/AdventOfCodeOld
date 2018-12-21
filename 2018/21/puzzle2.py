r5 = 0
ur5 = -1
seen = set()

while True:
    r3 = r5 | 65536
    r5 = 7586220
    while True:
        r5 = (((r5 + (r3 & 255)) & 16777215) * 65899) & 16777215
        if r3 < 256:
            if r5 not in seen:
                seen.add(r5)
                ur5 = r5
                break
            else:
                print(ur5)
                exit()
        else:
            r3 //= 256
