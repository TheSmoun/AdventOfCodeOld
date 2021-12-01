package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day1
import kotlin.test.Test
import kotlin.test.assertEquals

class Day1Tests {

    private val input = listOf(199, 200, 208, 210, 200, 207, 240, 269, 260, 263)

    @Test
    fun part1_shouldFindCorrectNumber() {
        val result = Day1().runPart1(input)
        assertEquals(7, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val result = Day1().runPart2(input)
        assertEquals(5, result)
    }
}
