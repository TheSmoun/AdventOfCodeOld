import math
from itertools import product

def get_next(x, y):
    if x == 0 and y == 0: return (1, 0)
    if x > y and y > -x: return (x, y + 1)
    if y > -x and y >= x: return (x - 1, y)
    if y <= -x and x < y: return (x, y - 1)
    if y <= -x and x >= y: return (x + 1, y)

number = 289326
numbers = {(0, 0): 1}
o = [-1, 0, 1]
x = 0
y = 0

while numbers[(x, y)] <= number:
    x, y = get_next(x, y)
    numbers[(x, y)] = sum(numbers.get((x + t[0], y + t[1]), 0) for t in product(o, o))

print(numbers[(x, y)])
