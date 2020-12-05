package de.thesmoun.aoc.days

class Day5 : Day<Collection<Int>, Int>("Day 5: Binary Boarding") {

    override fun parseInput(input: Collection<String>) = input.map {
        var row = 0..127
        var col = 0..7
        it.forEach { c ->
            when (c) {
                'F' -> { row = row.lowerHalf() }
                'B' -> { row = row.upperHalf() }
                'L' -> { col = col.lowerHalf() }
                'R' -> { col = col.upperHalf() }
            }
        }
        row.first * 8 + col.first
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

fun IntRange.lowerHalf() = first..(last - 1 - (last - first) / 2)
fun IntRange.upperHalf() = (first + 1 + (last - first) / 2)..last
