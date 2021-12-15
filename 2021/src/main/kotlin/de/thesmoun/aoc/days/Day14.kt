package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.eachLongCount
import de.thesmoun.aoc.util.splitAt

class Day14 : Day<Pair<String, Map<String, Char>>, Long>("Day 14: Extended Polymerization") {

    override fun parseInput(input: Collection<String>): Pair<String, Map<String, Char>> {
        val split = input.splitAt("")
        val polymer = split[0].first()
        val rulesRegex = Regex("^(\\w+) -> (\\w+)$")
        val rules = split[1].associate {
            val (i, o) = rulesRegex.find(it)!!.destructured
            i to o[0]
        }
        return polymer to rules
    }

    override fun runPart1(input: Pair<String, Map<String, Char>>): Long {
        var polymer = input.first
        val rules = input.second
        repeat(10) {
            polymer = polymer.first() + polymer.windowed(2) {
                (rules[it.toString()] ?: ' ').toString() + it.last()
            }.joinToString("")
        }

        val counts = polymer.groupingBy { it }.eachCount().values
        return (counts.maxOrNull()!! - counts.minOrNull()!!).toLong()
    }

    override fun runPart2(input: Pair<String, Map<String, Char>>): Long {
        val rules = input.second
        val chars = input.first.groupingBy { it }.eachLongCount().toMutableMap().withDefault { 0L }
        val pairs = input.first.windowed(2) { it.first().toString() + it.last().toString() }.groupingBy { it }
            .eachLongCount()

        (0 until 40).fold(pairs) { p, _ ->
            rules.entries.filter { it.key in p }.fold(p.toMutableMap()) { np, rule ->
                val count = p[rule.key]!!
                val left = "${rule.key[0]}${rule.value}"
                val right = "${rule.value}${rule.key[1]}"

                chars[rule.value] = (chars[rule.value] ?: 0) + count
                np[left] = (np[left] ?: 0) + count
                np[right] = (np[right] ?: 0) + count
                np[rule.key] = (np[rule.key] ?: 0) - count
                np
            }.filter { it.value > 0 }
        }

        val counts = chars.values.sortedDescending()
        return counts.first() - counts.last()
    }
}
