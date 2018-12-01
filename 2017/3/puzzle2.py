import math

number = 289326
numbers = {(0, 0): 1}

def get_next(x, y):
    if x == 0 and y == 0: return (1, 0)
    if x > y and y > -x: return (x, y + 1)
    if y > -x and y >= x: return (x - 1, y)
    if y <= -x and x < y: return (x, y - 1)
    if y <= -x and x >= y: return (x + 1, y)

x = 0
y = 0
while numbers[(x, y)] <= number:
    x, y = get_next(x, y)
    numbers[(x, y)] = sum(numbers.get((x + i, y + j), 0) for i in [-1, 0, 1] for j in [0, 1, -1])

print(numbers[(x, y)])
