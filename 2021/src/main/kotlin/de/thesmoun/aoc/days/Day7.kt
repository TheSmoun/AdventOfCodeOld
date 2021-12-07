package de.thesmoun.aoc.days

import kotlin.math.abs

class Day7 : Day<Map<Int, Int>, Int>("Day 7: The Treachery of Whales") {

    override fun parseInput(input: Collection<String>) = input.first().split(',').map { it.toInt() }
        .groupingBy { it }.eachCount()

    override fun runPart1(input: Map<Int, Int>) = run(input) { it }

    override fun runPart2(input: Map<Int, Int>) = run(input) { it * (it + 1) / 2 }

    private fun run(input: Map<Int, Int>, cost: (Int) -> Int) = (input.keys.minOrNull()!!..input.keys.maxOrNull()!!)
        .minOf { i -> input.map { cost(abs(it.key - i)) * it.value }.sum() }
}
