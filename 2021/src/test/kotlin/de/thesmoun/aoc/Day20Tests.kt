package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day20
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day20Tests {

    private val input = """
        ..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

        #..#.
        #....
        ##..#
        ..#..
        ..###
    """.trimIndent()

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day20().parseInput(input.lines())
        val result = Day20().runPart1(parsedInput)
        assertEquals(35, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day20().parseInput(input.lines())
        val result = Day20().runPart2(parsedInput)
        assertEquals(3351, result)
    }
}
