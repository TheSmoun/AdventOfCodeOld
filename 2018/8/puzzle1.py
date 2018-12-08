def solve(numbers):
    children_size, metadata_size = numbers[:2]
    del numbers[:2]

    metadata_sum = 0
    for _ in range(children_size):
        s, numbers = solve(numbers)
        metadata_sum += s
    
    metadata_sum += sum(numbers[:metadata_size])
    return (metadata_sum, numbers[metadata_size:])

with open("2018/8/input.txt", "r") as f:
    print(solve([int(i) for i in f.readline().split()])[0])
