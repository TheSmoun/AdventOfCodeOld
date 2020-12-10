package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day10
import kotlin.test.Test
import kotlin.test.assertEquals

class Day10Tests {

    private val lines = """
        16
        10
        15
        5
        1
        11
        7
        19
        6
        12
        4
    """.trimIndent().lines()

    @Test
    fun parseInput_ShouldReturnSortedList() {
        val input = Day10().parseInput(lines)
        assertEquals(11, input.size)
        assertEquals(1, input[0])
    }

    @Test
    fun runPart1_shouldReturnFactorOfDifferences() {
        val result = Day10().testPart1(lines)
        assertEquals(7 * 5, result)
    }

    @Test
    fun runPart2_shouldReturnNumberOfPossibilities() {
        val result = Day10().testPart2(lines)
        assertEquals(8, result)
    }
}
