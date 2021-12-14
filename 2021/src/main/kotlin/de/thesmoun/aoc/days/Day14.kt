package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day14 : Day<Pair<String, Map<String, String>>, Long>("Day 14: Extended Polymerization") {

    override fun parseInput(input: Collection<String>): Pair<String, Map<String, String>> {
        val split = input.splitAt("")
        val polymer = split[0].first()
        val rulesRegex = Regex("^(\\w+) -> (\\w+)$")
        val rules = split[1].associate {
            val (i, o) = rulesRegex.find(it)!!.destructured
            i to o
        }
        return polymer to rules
    }

    override fun runPart1(input: Pair<String, Map<String, String>>): Long {
        var polymer = input.first
        val rules = input.second
        repeat(10) { i ->
            polymer = polymer.first() + polymer.windowed(2) {
                (rules[it.toString()] ?: "") + it.last()
            }.joinToString("")
        }

        val counts = polymer.groupingBy { it }.eachCount().values
        return (counts.maxOrNull()!! - counts.minOrNull()!!).toLong()
    }

    override fun runPart2(input: Pair<String, Map<String, String>>): Long {
        TODO("Not yet implemented")
    }
}
