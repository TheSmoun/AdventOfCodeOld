package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day5
import kotlin.test.Test
import kotlin.test.assertEquals

class Day5Tests {

    private val lines = """
        FBFBBFFRLR
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnBoardingPasses() {
        val input = Day5().parseInput(lines).toTypedArray()
        assertEquals(1, input.size)
        assertEquals(357, input[0])
    }
}
