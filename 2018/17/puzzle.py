from typing import NamedTuple
import re

class Point(NamedTuple("Point", [("x", int), ("y", int)])):
    def __add__(self, other):
        return type(self)(self.x + other.x, self.y + other.y)
    
    @property
    def down(self):
        return self + Point(0, 1)

    @property
    def up(self):
        return self + Point(0, -1)

    @property
    def left(self):
        return self + Point(-1, 0)
    
    @property
    def right(self):
        return self + Point(1, 0)

spring = Point(500, 0)
left = Point(-1, 0)
right = Point(1, 0)

def fall(p, max_y, clay, flowing):
    r = Point(p.x, p.y)
    while r.y < max_y:
        d = r.down
        if d not in clay:
            flowing.add(d)
            r = d
        elif d in clay:
            return r
    return None

def spread_side(p, offset, clay, still, s):
    pos = p
    while pos not in clay:
        s.add(pos)
        d = pos.down
        if d not in clay and d not in still:
            return pos
        pos = pos + offset
    return None

def spread(p, clay, flowing, still):
    s = set()
    l = spread_side(p, left, clay, still, s)
    r = spread_side(p, right, clay, still, s)
    if not l and not r:
        still.update(s)
    else:
        flowing.update(s)
    return l, r

with open("2018/17/input.txt", "r") as f:
    clay = set()
    for line in f.readlines():
        n = [int(i) for i in re.findall(r'-?\d+', line)]
        if line[0] == "x":
            for y in range(n[1], n[2] + 1):
                clay.add(Point(n[0], y))
        elif line[0] == "y":
            for x in range(n[1], n[2] + 1):
                clay.add(Point(x, n[0]))
    
    min_y, max_y = min(p.y for p in clay), max(p.y for p in clay)
    flowing, still, to_fall, to_spread = set(), set(), set(), set()
    to_fall.add(spring)

    while to_fall or to_spread:
        while to_fall:
            res = fall(to_fall.pop(), max_y, clay, flowing)
            if res:
                to_spread.add(res)
        
        while to_spread:
            p = to_spread.pop()
            l, r = spread(p, clay, flowing, still)
            if not l and not r:
                to_spread.add(p.up)
            else:
                if l:
                    to_fall.add(l)
                if r:
                    to_fall.add(r)

    print(len([p for p in (flowing | still) if p.y >= min_y]))
    print(len([p for p in still if p.y >= min_y]))
