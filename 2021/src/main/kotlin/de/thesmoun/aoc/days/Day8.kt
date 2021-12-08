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
        val overlappingToNumber = mapOf(
            14 to 0,
            10 to 2,
            13 to 3,
            11 to 5,
            12 to 6,
            15 to 9,
        )

        val lengthToNumber = mapOf(
            2 to 1,
            4 to 4,
            3 to 7,
            7 to 8,
        )

        return input.sumOf { line ->
            val oneTwoFourEightChars = line.first.filter { it.length in lengthToNumber }.map { it.toSet() }
            line.second.map { s -> lengthToNumber[s.length] ?: overlappingToNumber[oneTwoFourEightChars.sumOf { l -> s.count { c -> c in l } }]!! }
                .fold(0) { acc, curr -> acc * 10 + curr }.toInt()
        }
    }
}
