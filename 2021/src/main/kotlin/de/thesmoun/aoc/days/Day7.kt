package de.thesmoun.aoc.days

import kotlin.math.abs

class Day7 : Day<Map<Int, Int>, Int>("Day 7: The Treachery of Whales") {

    override fun parseInput(input: Collection<String>) = input.first().split(',').map { it.toInt() }
        .groupingBy { it }.eachCount()

    override fun runPart1(input: Map<Int, Int>) = (input.keys.minOrNull()!!..input.keys.maxOrNull()!!)
        .minOf { i -> input.map { abs(it.key - i) * it.value }.sum() }

    override fun runPart2(input: Map<Int, Int>) = (input.keys.minOrNull()!!..input.keys.maxOrNull()!!)
        .minOf { i -> input.map { (0..abs(it.key - i)).sum() * it.value }.sum() }
}
