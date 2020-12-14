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
}
