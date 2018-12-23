from typing import NamedTuple
from heapq import heappush, heappop

class Point(NamedTuple("Point", [("x", int), ("y", int)])):
    def __add__(self, other):
        return type(self)(self.x + other.x, self.y + other.y)
    
    @property
    def neighbors(self):
        points = [Point(0, 1), Point(0, -1), Point(1, 0), Point(-1, 0)]
        return [self + p for p in points]

depth = 9171
target = Point(7, 721)
neither, torch, climb = 0, 1, 2
erosion = {}

def get_geological_index(pos):
    if pos == Point(0, 0) or pos == target:
        return 0
    if pos.y == 0:
        return pos[0] * 16807
    if pos.x == 0:
        return pos[1] * 48271
    return get_erosion_level(Point(pos.x - 1, pos.y)) * get_erosion_level(Point(pos.x, pos.y - 1))

def get_erosion_level(pos):
    if pos in erosion:
        return erosion[pos]
    
    e = (get_geological_index(pos) + depth) % 20183
    erosion[pos] = e
    return e

queue = [(0, Point(0, 0), torch)]
visited = {(Point(0, 0), torch): 0}

def visit_next(time, pos, equipment):
    for neighbor in pos.neighbors:
        if neighbor.x < 0 or neighbor.y < 0:
            continue
        if get_erosion_level(neighbor) % 3 == equipment:
            continue
        if (neighbor, equipment) in visited and visited[(neighbor, equipment)] <= time:
            continue
        visited[(neighbor, equipment)] = time
        heappush(queue, (time, neighbor, equipment))

while True:
    time, pos, equipment = heappop(queue)
    if pos == target and equipment == torch:
        break
    
    time += 1
    visit_next(time, pos, equipment)

    time += 7
    equipment = 3 - equipment - get_erosion_level(pos) % 3
    visit_next(time, pos, equipment)

print(time)
