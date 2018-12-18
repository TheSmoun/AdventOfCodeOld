import re

def addr(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] + result[b]
    return result

def addi(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] + b
    return result

def mulr(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] * result[b]
    return result

def muli(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] * b
    return result

def banr(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] & result[b]
    return result

def bani(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] & b
    return result

def borr(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] | result[b]
    return result

def bori(registers, a, b, c):
    result = registers[:]
    result[c] = result[a] | b
    return result

def setr(registers, a, b, c):
    result = registers[:]
    result[c] = result[a]
    return result

def seti(registers, a, b, c):
    result = registers[:]
    result[c] = a
    return result

def gtir(registers, a, b, c):
    result = registers[:]
    result[c] = 1 if a > result[b] else 0
    return result

def gtri(registers, a, b, c):
    result = registers[:]
    result[c] = 1 if result[a] > b else 0
    return result

def gtrr(registers, a, b, c):
    result = registers[:]
    result[c] = 1 if result[a] > result[b] else 0
    return result

def eqir(registers, a, b, c):
    result = registers[:]
    result[c] = 1 if a == result[b] else 0
    return result

def eqri(registers, a, b, c):
    result = registers[:]
    result[c] = 1 if result[a] == b else 0
    return result

def eqrr(registers, a, b, c):
    result = registers[:]
    result[c] = 1 if result[a] == result[b] else 0
    return result

operations = [addr, addi, mulr, muli, banr, bani, borr, bori, setr, seti, gtir, gtri, gtrr, eqir, eqri, eqrr]

def get_operations(registers, instruction, expected_result):
    result = set()
    for op in operations:
        if op(registers, *instruction[1:]) == expected_result:
            result.add(op)
    return result

with open("2018/16/input.txt", "r") as f:
    lines = [line.strip() for line in f.readlines()]
    for i, line in enumerate(lines):
        if not line and not lines[i + 1] and not lines[i + 2]:
            break
    
    monitor = [line for line in lines[:i] if line]
    input = [[int(i) for i in re.findall(r'-?\d+', line)] for line in lines[i + 3:]]
    tests = []

    it = iter(monitor)
    for line in it:
        before = [int(i) for i in re.findall(r'-?\d+', line)]
        instruction = [int(i) for i in re.findall(r'-?\d+', next(it))]
        expected_result = [int(i) for i in re.findall(r'-?\d+', next(it))]
        tests.append((before[:], instruction[:], expected_result[:]))

    o = {opcode: set(operations) for opcode in range(16)}
    for test in tests:
        opcode = test[1][0]
        o[opcode].intersection_update(get_operations(*test))
    
    while True:
        unique_ops = {}
        for opcode, ops in o.items():
            if len(ops) == 1:
                unique_ops[opcode] = ops
        for u_opcode, u_ops in unique_ops.items():
            for opcode, ops in o.items():
                if opcode != u_opcode:
                    ops.difference_update(u_ops)
        if len(unique_ops) == len(o):
            break
    
    registers = [0 for _ in range(4)]
    for opcode, a, b, c in input:
        registers = next(iter(o[opcode]))(registers, a, b, c)

    print(registers[0])
