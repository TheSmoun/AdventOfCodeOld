package de.thesmoun.aoc.days

class Day5 : Day<Collection<Int>, Int>("Day 5: Binary Boarding") {

    override fun parseInput(input: Collection<String>) = input.map {
        it.replace(Regex("[FL]"), "0").replace(Regex("[BR]"), "1").toInt(2)
    }.sorted()

    override fun runPart1(input: Collection<Int>) = input.last()

    override fun runPart2(input: Collection<Int>) = (input.first()..input.last()).first { it !in input }
}
