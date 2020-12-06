package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day6 : Day<Collection<Collection<String>>, Int>("Day 6: Custom Customs") {

    override fun parseInput(input: Collection<String>) = input.splitAt("")

    override fun runPart1(input: Collection<Collection<String>>) = input.sumBy {
        it.flatMap { s -> s.toList() }.toSet().size
    }

    override fun runPart2(input: Collection<Collection<String>>) = input.sumBy {
        it.map { s -> s.toSet() }.reduce { acc, set -> acc intersect set }.size
    }
}
