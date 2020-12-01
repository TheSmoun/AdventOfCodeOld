package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day1
import kotlin.test.Test
import kotlin.test.assertEquals

class Day1Tests {

    private val input = setOf(1721, 979, 366, 299, 675, 1456)

    @Test
    fun part1_shouldFindCorrectNumber() {
        val result = Day1().runPart1(input)
        assertEquals(514579, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val result = Day1().runPart2(input)
        assertEquals(241861950, result)
    }
}
