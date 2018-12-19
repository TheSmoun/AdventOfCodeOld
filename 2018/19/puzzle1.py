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

ops = {
    "addr": addr, "addi": addi, "mulr": mulr, "muli": muli,
    "banr": banr, "bani": bani, "borr": borr, "bori": bori,
    "setr": setr, "seti": seti, "gtir": gtir, "gtri": gtri,
    "gtrr": gtrr, "eqir": eqir, "eqri": eqri, "eqrr": eqrr
}

with open("2018/19/input.txt", "r") as f:
    lines = [line.strip().split(" ") for line in f.readlines()]
    ip_reg = int(lines[0][-1])
    instructions = [[i, int(a), int(b), int(c)] for i, a, b, c in lines[1:]]
    
    registers = [0 for _ in range(6)]
    while 0 <= registers[ip_reg] < len(instructions):
        ip = registers[ip_reg]
        instruction = instructions[ip]
        registers = ops[instruction[0]](registers, *instruction[1:])
        registers[ip_reg] += 1
    
    print(registers[0])
