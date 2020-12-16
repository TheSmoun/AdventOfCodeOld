package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day16
import kotlin.test.Test
import kotlin.test.assertEquals

class Day16Tests {

    private val lines = """
        class: 1-3 or 5-7
        row: 6-11 or 33-44
        seat: 13-40 or 45-50

        your ticket:
        7,1,14

        nearby tickets:
        7,3,47
        40,4,50
        55,2,20
        38,6,12
    """.trimIndent().lines()

    private val lines2 = """
        class: 0-1 or 4-19
        row: 0-5 or 8-19
        seat: 0-13 or 16-19

        your ticket:
        11,12,13

        nearby tickets:
        3,9,18
        15,1,5
        5,14,9
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnFieldsTicketAndTickets() {
        val input = Day16().parseInput(lines)
        val expectedFields = listOf(
                Day16.Field("class", Day16.FieldRange(1..3, 5..7)),
                Day16.Field("row", Day16.FieldRange(6..11, 33..44)),
                Day16.Field("seat", Day16.FieldRange(13..40, 45..50)),
        )
        val expectedTicket = Day16.Ticket(listOf(7, 1, 14))
        val expectedTickets = listOf(
                Day16.Ticket(listOf(7, 3, 47)),
                Day16.Ticket(listOf(40, 4, 50)),
                Day16.Ticket(listOf(55, 2, 20)),
                Day16.Ticket(listOf(38, 6, 12)),
        )
        val expectedInput = Day16.Input(expectedFields, expectedTicket, expectedTickets)
        assertEquals(expectedInput, input)
    }

    @Test
    fun runPart1_shouldReturnSumOfInvalidValues() {
        val result = Day16().testPart1(lines)
        assertEquals(71, result)
    }

    @Test
    fun runPart2_shouldReturnMultipliedDepartureValues() {
        Day16().testPart2(lines2)
    }
}
