package de.thesmoun.aoc.days

class Day10 : Day<List<Int>, Long>("Day 10: Adapter Array") {

    override fun parseInput(input: Collection<String>) = input.map { it.toInt() }.sorted()

    override fun runPart1(input: List<Int>): Long {
        var ones = 1L
        var threes = 1L

        input.reduce { acc, i ->
            val diff = i - acc
            if (diff == 1) ones++
            else if (diff == 3) threes++
            i
        }

        return ones * threes
    }

    override fun runPart2(input: List<Int>): Long {
        val map = mutableMapOf(Pair(0, 1L))
        input.forEach { map[it] = (map[it - 3] ?: 0) + (map[it - 2] ?: 0) + (map[it - 1] ?: 0) }
        return map[input.last()]!!
    }
}
