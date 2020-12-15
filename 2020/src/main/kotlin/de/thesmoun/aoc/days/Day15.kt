package de.thesmoun.aoc.days

class Day15 : Day<Collection<Int>, Int>("Day 15: Rambunctious Recitation") {

    override fun parseInput(input: Collection<String>) = input.first().split(',').map { it.toInt() }

    override fun runPart1(input: Collection<Int>) = runPart(input, 2020)

    override fun runPart2(input: Collection<Int>) = runPart(input, 30000000)

    private fun runPart(input: Collection<Int>, limit: Int): Int {
        val memory = input.withIndex().associate { it.value to ((it.index + 1) to -1) }.toMutableMap()

        var lastNumberSpoken = input.last()
        for (turn in (input.size + 1)..limit) {
            val lastTurns = memory[lastNumberSpoken]
            lastNumberSpoken = if (lastTurns == null) {
                0
            } else {
                val (t0, t1) = lastTurns
                if (t1 == -1) {
                    turn - t0 - 1
                } else {
                    t0 - t1
                }
            }

            memory[lastNumberSpoken] = turn to (memory[lastNumberSpoken]?.first ?: -1)
        }

        return lastNumberSpoken
    }
}
