package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day15
import kotlin.test.Test
import kotlin.test.assertEquals

class Day15Tests {

    private val lines = """
        0,3,6
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnsInts() {
        val input = Day15().parseInput(lines)
        assertEquals(listOf(0, 3, 6), input)
    }

    @Test
    fun runPart1_shouldReturn2020Number() {
        assertEquals(436, Day15().testPart1(listOf("0,3,6")))
    }
}
