package de.thesmoun.aoc.days

class Day10 : Day<List<Int>, Int>("Day 10: Adapter Array") {

    override fun parseInput(input: Collection<String>): List<Int> {
        val list = input.map { it.toInt() }.sorted().toMutableList()
        list.add(0, 0)
        list.add(list.last() + 3)
        return list
    }

    override fun runPart1(input: List<Int>): Int {
        var ones = 0
        var threes = 0
        input.windowed(2, 1).forEach {
            if (it.size == 2) {
                if (it[0] + 1 == it[1])
                    ones++
                else if (it[0] + 3 == it[1])
                    threes++
            }
        }
        return ones * threes
    }

    override fun runPart2(input: List<Int>): Int {
        TODO("Not yet implemented")
    }
}
