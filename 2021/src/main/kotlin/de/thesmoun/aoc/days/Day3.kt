package de.thesmoun.aoc.days

class Day3 : Day<List<String>, Int>("Day 3: Binary Diagnostic") {

    override fun parseInput(input: Collection<String>) = input.toList()

    override fun runPart1(input: List<String>): Int {
        var gamma = ""
        var epsilon = ""
        sortBitwise(input).forEach { s ->
            if (s.count { it == '1' } > s.length / 2) {
                gamma += '1'
                epsilon += '0'
            } else {
                gamma += '0'
                epsilon += '1'
            }
        }

        return Integer.parseInt(gamma, 2) * Integer.parseInt(epsilon, 2)
    }

    override fun runPart2(input: List<String>): Int {
        var oxygen = input.toList()
        var scrubber = input.toList()

        var i = 0
        while (oxygen.count() > 1) {
            val threshold = oxygen.count() / 2 + (if (oxygen.count() % 2 == 0) 0 else 1)
            oxygen = if (sortBitwise(oxygen)[i].count { it == '1' } >= threshold) {
                oxygen.filter { it[i] == '1' }
            } else {
                oxygen.filter { it[i] == '0' }
            }
            i++
        }

        i = 0
        while (scrubber.count() > 1) {
            val threshold = scrubber.count() / 2 + (if (scrubber.count() % 2 == 0) 0 else 1)
            scrubber = if (sortBitwise(scrubber)[i].count { it == '1' } < threshold) {
                scrubber.filter { it[i] == '1' }
            } else {
                scrubber.filter { it[i] == '0' }
            }
            i++
        }

        return Integer.parseInt(oxygen.first(), 2) * Integer.parseInt(scrubber.first(), 2)
    }

    private fun sortBitwise(input: List<String>): List<String> {
        return (0 until input[0].length).map { i -> input.joinToString("") { it[i].toString() } }
    }
}
