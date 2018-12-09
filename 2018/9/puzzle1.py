from collections import deque, defaultdict

players = 431
marbles = 70950

game = deque([0])
scores = defaultdict(int)

for m in range(1, marbles + 1):
    if m % 23 == 0:
        game.rotate(7)
        scores[m % players] += m + game.pop()
        game.rotate(-1)
    else:
        game.rotate(-1)
        game.append(m)

print(max(scores.values()))
