package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day6
import kotlin.test.Test
import kotlin.test.assertEquals

class Day6Tests {

    private val lines = """
        abc

        a
        b
        c

        ab
        ac

        a
        a
        a
        a

        b
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnGroupAnswers() {
        val input = Day6().parseInput(lines).toTypedArray()
        assertEquals(5, input.size)
    }

    @Test
    fun runPart1_shouldComputeTheSumOfYesCounts() {
        val result = Day6().testPart1(lines)
        assertEquals(11, result)
    }

    @Test
    fun runPart2_shouldComputeAllYesCounts() {
        val result = Day6().testPart2(lines)
        assertEquals(6, result)
    }
}
