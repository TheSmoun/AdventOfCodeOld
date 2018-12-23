depth = 9171
target = (7, 721)
erosion = {}

def get_geological_index(pos):
    if pos == (0, 0) or pos == target:
        return 0
    if pos[1] == 0:
        return pos[0] * 16807
    if pos[0] == 0:
        return pos[1] * 48271
    return get_erosion_level((pos[0] - 1, pos[1])) * get_erosion_level((pos[0], pos[1] - 1))

def get_erosion_level(pos):
    if pos in erosion:
        return erosion[pos]
    
    e = (get_geological_index(pos) + depth) % 20183
    erosion[pos] = e
    return e

print(sum(get_erosion_level((x, y)) % 3 for x in range(target[0] + 1) for y in range(target[1] + 1)))
