with open("2018/7/input.txt", "r") as f:
    lines = [line.split(" must be finished before step ") for line in f.readlines()]
    lines = [(line[0][-1], line[-1][0]) for line in lines]
    dependencies = {}
    for a, b in lines:
        array = dependencies.get(a, set())
        array.add(b)
        dependencies[a] = array
    time = 0
    workers = [list(t) for t in zip([0 for _ in range(5)], [None for _ in range(5)])]
    todo = []
    while len(dependencies.items()) > 0:
        n = [k for k in dependencies if all(k not in v for v in dependencies.values())]
        n = [k for k in n if k not in map(lambda w: w[1], workers)]
        n = sorted(n)
        n.reverse()
        for i in range(len(workers)):
            worker = workers[i]
            worker[0] = max(worker[0] - 1, 0)
            if worker[0] == 0:
                if worker[1] is not None and worker[1] in dependencies:
                    if len(dependencies.items()) == 1:
                        for item in dependencies[worker[1]]:
                            todo.append(item)
                    del dependencies[worker[1]]
                if n:
                    task = n.pop()
                    worker[0] = 60 + ord(task) - ord("A")
                    worker[1] = task
                else:
                    worker[1] = None
        time += 1
    todo.sort()
    todo.reverse()
    while len(todo) > 0:
        for i in range(len(workers)):
            worker = workers[i]
            worker[0] = max(worker[0] - 1, 0)
            if worker[0] == 0:
                if todo:
                    task = todo.pop()
                    worker[0] = 60 + ord(task) - ord("A")
                    worker[1] = task
                else:
                    worker[1] = None
        time += 1
    time += max(map(lambda w: w[0], workers))
    print(time)
