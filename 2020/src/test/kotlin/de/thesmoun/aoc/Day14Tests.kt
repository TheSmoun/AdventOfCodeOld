package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day14
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertTrue

class Day14Tests {

    private val lines = """
        mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
        mem[8] = 11
        mem[7] = 101
        mem[8] = 0
    """.trimIndent().lines()

    private val lines2 = """
        mask = 000000000000000000000000000000X1001X
        mem[42] = 100
        mask = 00000000000000000000000000000000X0XX
        mem[26] = 1
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnActions() {
        val input = Day14().parseInput(lines)
        assertEquals(4, input.size)
        assertTrue { input[0] is Day14.MaskAction }
        assertTrue { input[1] is Day14.StoreAction }
    }

    @Test
    fun runPart1_shouldReturnSumOfMemoryValues() {
        val result = Day14().testPart1(lines)
        assertEquals(165, result)
    }

    @Test
    fun runPart2_shouldReturnSumOfMemoryValues() {
        val result = Day14().testPart2(lines2)
        assertEquals(208, result)
    }
}
