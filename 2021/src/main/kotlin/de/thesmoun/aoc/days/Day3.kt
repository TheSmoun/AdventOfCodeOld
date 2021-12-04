package de.thesmoun.aoc.days

class Day3 : Day<Collection<String>, Int>("Day 3: Binary Diagnostic") {

    override fun parseInput(input: Collection<String>): Collection<String> {
        val length = input.first().length
        return (0 until length).map { i -> input.joinToString("") { it[i].toString() } }
    }

    override fun runPart1(input: Collection<String>): Int {
        var gamma = ""
        var epsilon = ""
        input.forEach { s ->
            if (s.count { it == '1' } > s.count { it == '0' }) {
                gamma += '1'
                epsilon += '0'
            } else {
                gamma += '0'
                epsilon += '1'
            }
        }
        return Integer.parseInt(gamma, 2) * Integer.parseInt(epsilon, 2)
    }

    override fun runPart2(input: Collection<String>): Int {
        TODO("Not yet implemented")
    }
}
