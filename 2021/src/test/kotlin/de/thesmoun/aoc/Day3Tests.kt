package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day3
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day3Tests {

    private val input = listOf("00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000",
        "11001", "00010", "01010")

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day3().parseInput(input)
        val result = Day3().runPart1(parsedInput)
        assertEquals(198, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day3().parseInput(input)
        val result = Day3().runPart2(parsedInput)
        assertEquals(900, result)
    }
}
