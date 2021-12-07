package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day7
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day7Tests {

    private val input = "16,1,2,0,4,2,7,1,2,14"

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day7().parseInput(input.lines())
        val result = Day7().runPart1(parsedInput)
        assertEquals(37, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day7().parseInput(input.lines())
        val result = Day7().runPart2(parsedInput)
        assertEquals(168, result)
    }
}
