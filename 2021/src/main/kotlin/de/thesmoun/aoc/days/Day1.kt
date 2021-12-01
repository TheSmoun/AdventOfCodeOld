package de.thesmoun.aoc.days

class Day1 : Day<Collection<Int>, Int>("Day 1: Sonar Sweep") {

    override fun parseInput(input: Collection<String>) = input.map { it.toInt() }

    override fun runPart1(input: Collection<Int>): Int = input.windowed(2).count { it[0] < it[1] }

    override fun runPart2(input: Collection<Int>): Int = input.windowed(3)
        .windowed(2).count { it[0].sum() < it[1].sum() }
}
