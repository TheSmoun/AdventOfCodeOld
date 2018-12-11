from collections import defaultdict

serial_number = 5177
grid = defaultdict(int)
for x in range(1, 301):
    for y in range(1, 301):
        p = ((((x + 10) * y + serial_number) * (x + 10) // 100) % 10) - 5
        grid[(x, y)] = p
s = 0
x_s = None
y_s = None
for x in range(1, 301 - 2):
    for y in range(1, 301 - 2):
        sp = sum(grid[(x + i, y + j)] for i in range(3) for j in range(3))
        if sp > s:
            x_s = x
            y_s = y
            s = sp
print((x_s, y_s))
