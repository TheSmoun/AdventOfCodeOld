from collections import defaultdict

serial_number = 5177
grid = defaultdict(int)
for x in range(1, 301):
    for y in range(1, 301):
        p = ((((x + 10) * y + serial_number) * (x + 10) // 100) % 10) - 5
        grid[(x, y)] = p

for x in range(1, 301):
    for y in range(1, 301):
        if x == 1 and y == 1:
            grid[(1, 1)] = grid[(1, 1)]
        elif y == 1:
            grid[(x, 1)] = grid[(x - 1, 1)] + grid[(x, 1)]
        elif x == 1:
            grid[(1, y)] = grid[(1, y - 1)] + grid[(1, y)]
        else:
            grid[(x, y)] = grid[(x, y)] + grid[(x - 1, y)] + grid[(x, y - 1)] - grid[(x - 1, y - 1)]

s = 0
x_s = None
y_s = None
s_s = None
for x in range(1, 301):
    for y in range(1, 301):
        for size in range(1, 301 - max(x, y)):
            sp = grid[(x - 1, y - 1)] + grid[(x + size - 1, y + size - 1)] - grid[(x + size - 1, y - 1)] - grid[(x - 1, y + size - 1)]
            if sp > s:
                x_s = x
                y_s = y
                s_s = size
                s = sp

print((x_s, y_s, s_s))
