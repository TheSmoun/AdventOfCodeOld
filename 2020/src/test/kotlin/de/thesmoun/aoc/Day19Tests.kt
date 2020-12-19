package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day19
import kotlin.test.Test
import kotlin.test.assertEquals

class Day19Tests {

    private val lines = """
        0: 1 2
        1: "a"
        2: 1 3 | 3 1
        3: "b"
        
        a
    """.trimIndent().lines()

    @Test
    fun parseRule_shouldReturnRules() {
        val (map, _) = Day19().parseInput(lines)
        val expectedMap = mapOf(
                0 to "1 2",
                1 to "\"a\"",
                2 to "1 3 | 3 1",
                3 to "\"b\""
        )
        assertEquals(expectedMap, map)
    }
}
