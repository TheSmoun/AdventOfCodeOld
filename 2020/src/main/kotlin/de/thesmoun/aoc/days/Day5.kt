package de.thesmoun.aoc.days

class Day5 : Day<Collection<Int>, Int>("Day 5: Binary Boarding") {

    override fun parseInput(input: Collection<String>) = input.map {
        it.replace(Regex("[FL]"), "0").replace(Regex("[BR]"), "1").toInt(2)
    }

    override fun runPart1(input: Collection<Int>) = input.maxOrNull() ?: error("")

    override fun runPart2(input: Collection<Int>): Int {
        val sortedIds = input.sorted()
        for (i in sortedIds.first()..sortedIds.last()) {
            if (!sortedIds.contains(i))
                return i
        }
        error("")
    }
}
