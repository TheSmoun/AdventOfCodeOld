package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day2
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day2Tests {

    private val input = listOf("forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2")

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day2().parseInput(input)
        val result = Day2().runPart1(parsedInput)
        assertEquals(150, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day2().parseInput(input)
        val result = Day2().runPart2(parsedInput)
        assertEquals(900, result)
    }
}
