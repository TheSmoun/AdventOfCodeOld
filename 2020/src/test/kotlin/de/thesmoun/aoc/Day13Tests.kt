package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day13
import kotlin.test.Test
import kotlin.test.assertEquals

class Day13Tests {

    private val lines = """
        939
        7,13,x,x,59,x,31,19
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnTimestampAndBusIds() {
        val (timestamp, busIds) = Day13().parseInput(lines)
        assertEquals(939, timestamp)
        assertEquals(listOf(7, 13, -1, -1, 59, -1, 31, 19), busIds)
    }

    @Test
    fun runPart1_shouldFindTheNextBusAfterArrival() {
        val result = Day13().testPart1(lines)
        assertEquals(295, result)
    }

    @Test
    fun runPart2_shouldFindFirstTimestampOfSuccessiveBusDepartures() {
        val result = Day13().testPart2(lines)
        assertEquals(1068781, result)
    }
}
