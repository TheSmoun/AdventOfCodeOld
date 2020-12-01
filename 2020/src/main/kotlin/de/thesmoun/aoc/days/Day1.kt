package de.thesmoun.aoc.days

class Day1 : Day<Collection<Int>, Int>("Day 1") {

    private val target = 2020

    override fun parseInput(input: Collection<String>) = input.map { it.toInt() }.toSet()

    override fun runPart1(input: Collection<Int>): Int {
        input.forEach {
            val diff = target - it
            if (input.contains(diff))
                return it * diff
        }

        throw IllegalArgumentException()
    }

    override fun runPart2(input: Collection<Int>): Int {
        input.forEach { n0 ->
            input.forEach { n1 ->
                input.forEach { n2 ->
                    if (n0 + n1 + n2 == target)
                        return n0 * n1 * n2
                }
            }
        }

        throw IllegalArgumentException()
    }
}
