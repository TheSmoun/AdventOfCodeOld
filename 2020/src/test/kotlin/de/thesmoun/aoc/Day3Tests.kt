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
        assertTrue {
            input.trees.contains(Day3.Point(2, 0))
        }
    }

    @Test
    fun runPart1_shouldReturnCorrectNumberOfTrees() {
        val day = Day3()
        val input = day.parseInput(lines)
        val result = day.runPart1(input)
        assertEquals(7, result)
    }

    @Test
    fun runPart2_shouldReturnCorrectNumberOfTreesForMultipleSlopes() {
        val day = Day3()
        val input = day.parseInput(lines)
        val result = day.runPart2(input)
        assertEquals(336, result)
    }
}
