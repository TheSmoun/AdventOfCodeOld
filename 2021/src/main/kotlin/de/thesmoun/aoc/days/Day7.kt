package de.thesmoun.aoc.days

import kotlin.math.abs
import kotlin.math.min

class Day7 : Day<Map<Int, Int>, Int>("Day 7: The Treachery of Whales") {

    override fun parseInput(input: Collection<String>) = input.first().split(',').map { it.toInt() }
        .groupingBy { it }.eachCount()

    override fun runPart1(input: Map<Int, Int>): Int {
        val min = input.keys.minOrNull()!!
        val max = input.keys.maxOrNull()!!

        var minFuel = Int.MAX_VALUE
        (min..max).forEach { i ->
            val fuel = input.map { abs(it.key - i) * it.value }.sum()
            minFuel = min(minFuel, fuel)
        }

        return minFuel
    }

    override fun runPart2(input: Map<Int, Int>): Int {
        val min = input.keys.minOrNull()!!
        val max = input.keys.maxOrNull()!!

        var minFuel = Int.MAX_VALUE
        (min..max).forEach { i ->
            val fuel = input.map { (0..abs(it.key - i)).sum() * it.value }.sum()
            minFuel = min(minFuel, fuel)
        }

        return minFuel
    }
}
