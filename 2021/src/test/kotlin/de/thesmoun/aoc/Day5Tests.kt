package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day5
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day5Tests {
    
    private val input = """0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2"""

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day5().parseInput(input.lines())
        val result = Day5().runPart1(parsedInput)
        assertEquals(5, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day5().parseInput(input.lines())
        val result = Day5().runPart2(parsedInput)
        assertEquals(12, result)
    }
}