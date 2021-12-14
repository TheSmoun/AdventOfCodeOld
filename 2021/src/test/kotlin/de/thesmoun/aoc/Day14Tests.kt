package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day14
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day14Tests {

    private val input = """NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C"""

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day14().parseInput(input.lines())
        val result = Day14().runPart1(parsedInput)
        assertEquals(1588L, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day14().parseInput(input.lines())
        val result = Day14().runPart2(parsedInput)
        assertEquals(2188189693529L, result)
    }
}
