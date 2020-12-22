package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day22
import kotlin.test.Test
import kotlin.test.assertEquals

class Day22Tests {

    private val lines = """
        Player 1:
        9
        2
        6
        3
        1

        Player 2:
        5
        8
        4
        7
        10
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnDecks() {
        val input = Day22().parseInput(lines)
        assertEquals(mutableListOf(9, 2, 6, 3, 1) to mutableListOf(5, 8, 4, 7, 10), input)
    }

    @Test
    fun runPart1_shouldComputeWinningScore() {
        val result = Day22().testPart1(lines)
        assertEquals(306, result)
    }

    @Test
    fun runPart2_shouldComputeRecursiveWinningScore() {
        val result = Day22().testPart2(lines)
        assertEquals(291, result)
    }
}
