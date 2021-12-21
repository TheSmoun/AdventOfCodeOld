package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day21
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day21Tests {
    private val input = """
        Player 1 starting position: 4
        Player 2 starting position: 8
    """.trimIndent()

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day21().parseInput(input.lines())
        val result = Day21().runPart1(parsedInput)
        assertEquals(739785, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day21().parseInput(input.lines())
        val result = Day21().runPart2(parsedInput)
        assertEquals(444356092776315L, result)
    }
}
