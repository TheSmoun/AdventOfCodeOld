package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day6 : Day<Collection<Collection<String>>, Int>("Day 6: Custom Customs") {

    override fun parseInput(input: Collection<String>) = input.splitAt("")

    override fun runPart1(input: Collection<Collection<String>>) = input.map {
        it.flatMap { s -> s.toList() }.toSet().size
    }.sum()

    override fun runPart2(input: Collection<Collection<String>>) = input.map {
        it.map { s -> s.toSet() }.reduce { acc, set -> acc intersect set }.size
    }.sum()
}
