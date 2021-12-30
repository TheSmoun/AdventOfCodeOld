package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day25
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day25Tests {

    private val input = """
        v...>>.vv>
        .vv>>.vv..
        >>.>v>...v
        >>v>>.>.v.
        v>v.vv.v..
        >.>>..v...
        .vv..>.>v.
        v.v..>>v.v
        ....v..v.>
    """.trimIndent()

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day25().parseInput(input.lines())
        val result = Day25().runPart1(parsedInput)
        assertEquals(58, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day25().parseInput(input.lines())
        val result = Day25().runPart2(parsedInput)
        assertEquals(0, result)
    }
}
