package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day17
import kotlin.test.Test
import kotlin.test.assertEquals

class Day17Tests {

    private val lines = """
        .#.
        ..#
        ###
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnMapOfActiveCubes() {
        val input = Day17().parseInput(lines)
        val set = setOf(
                Day17.Pos3(1, 0, 0),
                Day17.Pos3(2, 1, 0),
                Day17.Pos3(0, 2, 0),
                Day17.Pos3(1, 2, 0),
                Day17.Pos3(2, 2, 0),
        )
        assertEquals(set, input)
    }

    @Test
    fun runPart1_shouldReturnActiveCubesAfter6CyclesIn3D() {
        val result = Day17().testPart1(lines)
        assertEquals(112, result)
    }

    @Test
    fun runPart1_shouldReturnActiveCubesAfter6CyclesIn4D() {
        val result = Day17().testPart2(lines)
        assertEquals(848, result)
    }
}
