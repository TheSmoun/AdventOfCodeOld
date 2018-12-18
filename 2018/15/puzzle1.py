from enum import Enum, auto
from typing import NamedTuple
from collections import deque

class Point(NamedTuple("Point", [("y", int), ("x", int)])):
    def __add__(self, other):
        return type(self)(self.y + other.y, self.x + other.x)
    
    @property
    def neighbors(self):
        points = [Point(0, 1), Point(0, -1), Point(1, 0), Point(-1, 0)]
        return [self + p for p in points]

class Entity:
    def __init__(self, id, team, position, power):
        self.id = id
        self.team = team
        self.position = position
        self.power = power
        self.hp = 200
        self.alive = True

    def make_turn(self, map):
        targets = [e for e in map.entities if e.team != self.team and e.alive]
        if not targets:
            return True

        occupied = set(e.position for e in map.entities if e.alive and e.id != self.id)
        in_range = set(p for t in targets for p in t.position.neighbors if not map[p] and p not in occupied)
        if not self.position in in_range:
            self.move(in_range, map)
        
        opponents = [t for t in targets if t.position in self.position.neighbors]
        if opponents:
            target = min(opponents, key=lambda e: (e.hp, e.position))
            target.hp -= self.power
            if target.hp <= 0:
                target.alive = False

    def move(self, targets, map):
        visiting = deque([(self.position, 0)])
        meta = {self.position: (0, None)}
        seen = set()
        occupied = {e.position for e in map.entities if e.alive}

        while visiting:
            pos, dist = visiting.popleft()
            for neighbor in pos.neighbors:
                if map[neighbor] or neighbor in occupied:
                    continue
                if neighbor not in meta or meta[neighbor] > (dist + 1, pos):
                    meta[neighbor] = (dist + 1, pos)
                if neighbor in seen:
                    continue
                if not any(neighbor == v[0] for v in visiting):
                    visiting.append((neighbor, dist + 1))
            seen.add(pos)

        try:
            _, closest = min((dist, pos) for pos, (dist, parent) in meta.items() if pos in targets)
        except ValueError:
            return
        
        while meta[closest][0] > 1:
            closest = meta[closest][1]
        
        self.position = closest

class Map(dict):
    def __init__(self, lines, power):
        super().__init__()
        self.entities = []

        id_count = 1
        for y, line in enumerate(lines):
            for x, c in enumerate(line):
                self[Point(y, x)] = c == "#"
                if c in "EG":
                    self.entities.append(Entity(id_count, c, Point(y, x), {"E": power, "G": 3}[c]))
                    id_count += 1
    
    def start_fight(self):
        rounds = 0
        while not self.play_round():
            rounds += 1
        return rounds * sum(e.hp for e in self.entities if e.alive)
    
    def play_round(self):
        for entity in sorted(self.entities, key=lambda e: e.position):
            if entity.alive and entity.make_turn(self):
                return True
        return False

with open("2018/15/input.txt", "r") as f:
    print(Map([line.strip() for line in f.readlines()], 3).start_fight())
