with open("2017/1/input.txt", "r") as f:
    numbers = [int(i) for i in list(f.read())]
    length = len(numbers)
    middle = length / 2
    s = 0

    for i, n0 in enumerate(numbers):
        n1 = numbers[(i + middle) % length]
        if n0 == n1:
            s += n0

    print(s)
