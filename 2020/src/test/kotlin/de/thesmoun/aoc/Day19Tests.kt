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
    fun parseRule_shouldReturnRule() {
        val (map, _) = Day19().parseInput(lines)
        val rule = Day19.Rule.parse(map[0] ?: error(""), map)
        assertEquals(setOf("aab", "aba"), rule.possibilities())
    }
}
