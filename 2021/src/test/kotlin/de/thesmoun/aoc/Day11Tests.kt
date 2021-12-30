package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day11
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day11Tests {
    
    private val input = """
        5483143223
        2745854711
        5264556173
        6141336146
        6357385478
        4167524645
        2176841721
        6882881134
        4846848554
        5283751526
    """.trimIndent()

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day11().parseInput(input.lines())
        val result = Day11().runPart1(parsedInput)
        assertEquals(1656, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day11().parseInput(input.lines())
        val result = Day11().runPart2(parsedInput)
        assertEquals(195, result)
    }
}
