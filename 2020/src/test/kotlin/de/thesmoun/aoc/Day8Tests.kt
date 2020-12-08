package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day8
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertTrue

class Day8Tests {

    private val lines = """
        nop +0
        acc +1
        jmp +4
        acc +3
        jmp -3
        acc -99
        acc +1
        jmp -4
        acc +6
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnInstructions() {
        val input = Day8().parseInput(lines)
        assertEquals(9, input.size)
        assertTrue { input[0] is Day8.NoOp }
        assertTrue { input[1] is Day8.Accumulate }
        assertTrue { input[2] is Day8.Jump }
    }

    @Test
    fun runPart1_shouldStopBeforeRunningAnInstructionTwice() {
        val result = Day8().testPart1(lines)
        assertEquals(5, result)
    }

    @Test
    fun runPart2_shouldFixInfiniteLoop() {
        val result = Day8().testPart2(lines)
        assertEquals(8, result)
    }
}
