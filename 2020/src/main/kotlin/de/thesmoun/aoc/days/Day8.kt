package de.thesmoun.aoc.days

class Day8 : Day<List<Day8.Instruction>, Int>("Day 8: Handheld Halting") {

    override fun parseInput(input: Collection<String>) = input.map {
        val split = it.split(' ')
        when (split[0]) {
            "acc" -> Accumulate(split[1].toInt())
            "jmp" -> Jump(split[1].toInt())
            "nop" -> NoOp(split[1].toInt())
            else -> error("")
        }
    }

    override fun runPart1(input: List<Instruction>) = runInstructions(input).accumulator

    override fun runPart2(input: List<Instruction>) = sequence {
        for (i in input.indices) {
            val program = input.toMutableList()
            if (program[i] is Jump) {
                program[i] = NoOp(program[i].value)
                yield(program)
            } else if (program[i] is NoOp) {
                program[i] = Jump(program[i].value)
                yield(program)
            }
        }
    }.map { runInstructions(it) }.first { !it.infiniteLoop }.accumulator

    private fun runInstructions(instructions: List<Instruction>): Result {
        val state = State(0, 0)
        val seenIps = HashSet<Int>()
        val infiniteLoop: Boolean

        while (true) {
            if (state.ip in seenIps) {
                infiniteLoop = true
                break
            } else if (state.ip == instructions.size) {
                infiniteLoop = false
                break
            }

            seenIps.add(state.ip)
            instructions[state.ip].execute(state)
        }

        return Result(state.accumulator, infiniteLoop)
    }

    data class State(var accumulator: Int, var ip: Int)
    data class Result(val accumulator: Int, val infiniteLoop: Boolean)

    abstract inner class Instruction(val value: Int) {
        abstract fun execute(state: State)
    }

    inner class Accumulate(value: Int) : Instruction(value) {
        override fun execute(state: State) {
            state.accumulator += value
            state.ip++
        }
    }

    inner class Jump(value: Int) : Instruction(value) {
        override fun execute(state: State) {
            state.ip += value
        }
    }

    inner class NoOp(value: Int) : Instruction(value) {
        override fun execute(state: State) {
            state.ip++
        }
    }
}
