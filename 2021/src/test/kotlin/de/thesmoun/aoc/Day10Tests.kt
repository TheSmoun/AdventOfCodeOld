package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day10
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day10Tests {

    private val input = """[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]"""

    @Test
    fun part1_shouldFindCorrectNumber() {
        val parsedInput = Day10().parseInput(input.lines())
        val result = Day10().runPart1(parsedInput)
        assertEquals(26397L, result)
    }

    @Test
    fun part2_shouldFindCorrectNumber() {
        val parsedInput = Day10().parseInput(input.lines())
        val result = Day10().runPart2(parsedInput)
        assertEquals(288957L, result)
    }
}
