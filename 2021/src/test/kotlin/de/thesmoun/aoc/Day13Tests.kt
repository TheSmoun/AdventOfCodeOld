package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day13
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day13Tests {

    private val input = """6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5"""

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day13().parseInput(input.lines())
        val result = Day13().runPart1(parsedInput)
        assertEquals("17", result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day13().parseInput(input.lines())
        val result = Day13().runPart2(parsedInput)
        assertEquals("", result)
    }
}
