from typing import NamedTuple

class Point(NamedTuple("Point", [("x", int), ("y", int)])):
    def __add__(self, other):
        return type(self)(self.x + other.x, self.y + other.y)
    
    def move(self, c):
        directions = {"N": Point(0, -1), "E": Point(1, 0), "S": Point(0, 1), "W": Point(-1, 0)}
        return self + directions[c]

with open("2018/20/input.txt", "r") as f:
    line = f.readline().strip()[1:-1]
    pos = Point(0, 0)
    distance = 0
    m = {pos: distance}
    branches = []
    
    for c in line:
        if c == "(":
            branches.append((pos, distance))
        elif c == ")":
            pos, distance = branches.pop()
        elif c == "|":
            pos, distance = branches[-1]
        else:
            pos = pos.move(c)
            distance += 1
            if pos not in m or distance < m[pos]:
                m[pos] = distance
    
    print(max(m.values()))
    print(len([v for v in m.values() if v >= 1000]))
