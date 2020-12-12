package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day12
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertTrue

class Day12Tests {

    private val lines = """
        F10
        N3
        F7
        R90
        F11
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnListOfActions() {
        val input = Day12().parseInput(lines)
        assertEquals(5, input.size)
        assertTrue { input[0].first == "F" && input[0].second == 10 }
    }

    @Test
    fun runPart1_shouldReturnShipsManhattenDistance() {
        val result = Day12().testPart1(lines)
        assertEquals(25, result)
    }

    @Test
    fun runPart2_shouldReturnShipsManhattenDistance() {
        val result = Day12().testPart2(lines)
        assertEquals(286, result)
    }

    @Test
    fun rotate() {
        assertEquals(Day12.Point(1, 0), Day12.Point(0, 1).rotate(-90))
        assertEquals(Day12.Point(1, 0), Day12.Point(0, -1).rotate(90))
        assertEquals(Day12.Point(-1, 1), Day12.Point(1, 1).rotate(90))
    }
}
