package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day23
import kotlin.test.Test
import kotlin.test.assertEquals

class Day23Tests {

    private val lines = """
        389125467
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnListOfInts() {
        val input = Day23().parseInput(lines)
        assertEquals(listOf(3, 8, 9, 1, 2, 5, 4, 6, 7), input.map { it.value })
    }

    @Test
    fun game_shouldSimulateCorrectly() {
        val cups = Day23().parseInput(lines)
        val game = Day23.Game(cups)
        repeat(10) {
            game.simulateRound()
        }
        assertEquals("92658374", game.order())
    }

    @Test
    fun runPart1_shouldSimulate100Rounds() {
        val result = Day23().testPart1(lines)
        assertEquals("67384529", result)
    }

    @Test
    fun runPart2_shouldSimulate10000000Rounds() {
        val result = Day23().testPart2(lines)
        assertEquals(149245887792L, result)
    }
}
