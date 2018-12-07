with open("2018/7/input.txt", "r") as f:
    lines = [line.split(" must be finished before step ") for line in f.readlines()]
    lines = [(line[0][-1], line[-1][0]) for line in lines]
    dependencies = {}
    for a, b in lines:
        array = dependencies.get(a, set())
        array.add(b)
        dependencies[a] = array
    result = ""
    while len(dependencies.items()) > 1:
        n = [k for k in dependencies if all(k not in v for v in dependencies.values())]
        n = sorted(n)[0]
        result += n
        del dependencies[n]
    for k in dependencies:
        result += k
        result += "".join(sorted([i for i in dependencies[k]]))

    print(result)
