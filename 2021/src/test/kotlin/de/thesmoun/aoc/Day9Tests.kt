package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day9
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day9Tests {
    
    private val input = """2199943210
3987894921
9856789892
8767896789
9899965678"""

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day9().parseInput(input.lines())
        val result = Day9().runPart1(parsedInput)
        assertEquals(15, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day9().parseInput(input.lines())
        val result = Day9().runPart2(parsedInput)
        assertEquals(1134, result)
    }
}
