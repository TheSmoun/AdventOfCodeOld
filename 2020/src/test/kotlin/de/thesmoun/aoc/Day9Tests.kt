package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day9
import kotlin.test.Test
import kotlin.test.assertEquals

class Day9Tests {

    private val lines = """
        35
        20
        15
        25
        47
        40
        62
        55
        65
        95
        102
        117
        150
        182
        127
        219
        299
        277
        309
        576
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnIntsAndPreamble() {
        val input = Day9().parseInput(lines)
        assertEquals(25, input.preambleLength)
        assertEquals(35, input.numbers[0])
    }

    @Test
    fun runPart1_shouldFindFirstNumberWithNoSum() {
        val day = Day9()
        var input = day.parseInput(lines)
        input = Day9.Input(5, input.numbers)
        val result = day.runPart1(input)
        assertEquals(127, result)
    }

    @Test
    fun runPart2_shouldFindEncryptionWeakness() {
        val day = Day9()
        var input = day.parseInput(lines)
        input = Day9.Input(5, input.numbers)
        val result = day.runPart2(input)
        assertEquals(62, result)
    }
}
