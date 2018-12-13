from collections import defaultdict

class Direction:
    def __init__(self, char):
        self.direction = {">": 0, "^": 1, "<": 2, "v": 3}[char]
    
    def get_tuple(self):
        return {0: (1, 0), 1: (0, -1), 2: (-1, 0), 3: (0, 1)}[self.direction]

    def move(self, position):
        t = self.get_tuple()
        return (position[0] + t[0], position[1] + t[1])
    
    def turn_left(self):
        self.direction = (self.direction + 1) % 4
    
    def turn_right(self):
        self.direction = (self.direction + 3) % 4

class Cart:
    def __init__(self, position, direction):
        self.position = position
        self.direction = direction
        self.state = 0
        self.crashed = False
    
    def move(self):
        self.position = self.direction.move(self.position)

    def turn(self, track):
        if track == "/":
            if self.direction.get_tuple()[1] != 0:
                self.direction.turn_right()
            else:
                self.direction.turn_left()
        elif track == "\\":
            if self.direction.get_tuple()[1] != 0:
                self.direction.turn_left()
            else:
                self.direction.turn_right()
        elif track == "+":
            if self.state == 0:
                self.direction.turn_left()
            elif self.state == 2:
                self.direction.turn_right()
            self.state = (self.state + 1) % 3

with open("2018/13/input.txt", "r") as f:
    lines = [line for line in f.readlines()]
    tracks = defaultdict(lambda: "", {(x, y): c for y, l in enumerate(lines) for x, c in enumerate(l) if c in "/\\+"})
    carts = [Cart((x, y), Direction(c)) for y, l in enumerate(lines) for x, c in enumerate(l) if c in "^v<>"]

    while len(carts) > 1:
        carts.sort(key=lambda c: c.position)
        for i, cart in enumerate(carts):
            if cart.crashed:
                continue

            cart.move()
            cart.turn(tracks[cart.position])
            for j, c in enumerate(carts):
                if i != j and cart.position == c.position and not c.crashed:
                    cart.crashed = True
                    c.crashed = True
                    break
        
        carts = [cart for cart in carts if not cart.crashed]
    
    print(carts[0].position)
