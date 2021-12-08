package de.thesmoun.aoc.days

class Day8 : Day<List<Pair<List<String>, List<String>>>, Int>("Day 8: Seven Segment Search") {

    override fun parseInput(input: Collection<String>) = input.map { s ->
        val parts = s.split('|').map { it.trim() }
        parts[0].split(' ') to parts[1].split(' ')
    }

    override fun runPart1(input: List<Pair<List<String>, List<String>>>): Int {
        val lengths = setOf(2, 3, 4, 7)
        return input.sumOf { it.second.count { s -> s.length in lengths } }
    }

    override fun runPart2(input: List<Pair<List<String>, List<String>>>): Int {
        TODO("Not yet implemented")
    }
}
