package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day6
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day6Tests {

    private val input = "3,4,3,1,2"

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day6().parseInput(input.lines())
        val result = Day6().runPart1(parsedInput)
        assertEquals(5934, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day6().parseInput(input.lines())
        val result = Day6().runPart2(parsedInput)
        assertEquals(26984457539L, result)
    }
}
