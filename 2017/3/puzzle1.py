import math

number = 289326
square = 0
s = 1
while s * s < number:
    s += 2
    square += 1

corners = [s * s + i * (s - 1) for i in range(-3, 1)]
prev = (s - 2) * (s - 2)

for corner in corners:
    if number >= prev and number <= corner:
        print int(square + math.fabs(number - (corner + prev) / 2))
        break
    else:
        prev = corner
