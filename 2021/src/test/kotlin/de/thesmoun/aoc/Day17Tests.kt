package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day17
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day17Tests {

    private val input = "target area: x=20..30, y=-10..-5"

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day17().parseInput(input.lines())
        val result = Day17().runPart1(parsedInput)
        assertEquals(45, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day17().parseInput(input.lines())
        val result = Day17().runPart2(parsedInput)
        assertEquals(112, result)
    }
}
