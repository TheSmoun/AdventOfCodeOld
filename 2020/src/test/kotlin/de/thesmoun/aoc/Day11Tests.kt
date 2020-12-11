package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day11
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertTrue

class Day11Tests {

    private val lines = """
        L.LL.LL.LL
        LLLLLLL.LL
        L.L.L..L..
        LLLL.LL.LL
        L.LL.LL.LL
        L.LLLLL.LL
        ..L.L.....
        LLLLLLLLLL
        L.LLLLLL.L
        L.LLLLL.LL
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnMapOfSeats() {
        val input = Day11().parseInput(lines)
        assertTrue { input.seats.containsKey(Day11.Pos(0, 0)) }
        assertTrue { input.seats.containsKey(Day11.Pos(1, 1)) }
    }

    @Test
    fun runPart1_shouldReturnNumberOfOccupiedSeats() {
        val result = Day11().testPart1(lines)
        assertEquals(37, result)
    }

    @Test
    fun runPart2_shouldReturnNumberOfOccupiedSeats() {
        val result = Day11().testPart2(lines)
        assertEquals(26, result)
    }
}
