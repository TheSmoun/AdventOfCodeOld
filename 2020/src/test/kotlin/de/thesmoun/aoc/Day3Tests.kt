package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day3
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertTrue

class Day3Tests {

    private val lines = """
        ..##.......
        #...#...#..
        .#....#..#.
        ..#.#...#.#
        .#...##..#.
        ..#.##.....
        .#.#.#....#
        .#........#
        #.##...#...
        #...##....#
        .#..#...#.#
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnTreePoints() {
        val input = Day3().parseInput(lines)
        assertTrue { input.trees.contains(Day3.Point(2, 0)) }
    }

    @Test
    fun runPart1_shouldReturnCorrectNumberOfTrees() {
        val result = Day3().testPart1(lines)
        assertEquals(7, result)
    }

    @Test
    fun runPart2_shouldReturnCorrectNumberOfTreesForMultipleSlopes() {
        val result = Day3().testPart2(lines)
        assertEquals(336, result)
    }
}
