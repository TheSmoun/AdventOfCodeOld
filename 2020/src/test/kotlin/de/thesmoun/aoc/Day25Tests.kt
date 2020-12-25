package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day25
import kotlin.test.Test
import kotlin.test.assertEquals

class Day25Tests {

    private val lines = """
        5764801
        17807724
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnPublicKeys() {
        val input = Day25().parseInput(lines)
        assertEquals(5764801L to 17807724L, input)
    }

    @Test
    fun runPart1_shouldReturnEncryptionKey() {
        val result = Day25().testPart1(lines)
        assertEquals(14897079L, result)
    }
}
