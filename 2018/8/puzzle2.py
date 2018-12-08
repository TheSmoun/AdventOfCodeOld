def solve(numbers):
    children_size, metadata_size = numbers[:2]
    del numbers[:2]

    children = []
    for _ in range(children_size):
        child, numbers = solve(numbers)
        children.append(child)
    
    if children_size > 0:
        return (sum(children[n - 1] for n in numbers[:metadata_size] if n > 0 and n <= len(children)), numbers[metadata_size:])
    else:
        return (sum(numbers[:metadata_size]), numbers[metadata_size:])

with open("2018/8/input.txt", "r") as f:
    print(solve([int(i) for i in f.readline().split()])[0])
