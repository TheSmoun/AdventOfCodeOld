package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day2
import kotlin.test.Test
import kotlin.test.assertEquals

class Day2Tests {

    private val lines = """
        1-3 a: abcde
        1-3 b: cdefg
        2-9 c: ccccccccc
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnPasswordPolicies() {
        val input = Day2().parseInput(lines).toTypedArray()
        assertEquals(3, input.size)
        assertEquals(Day2.PasswordPolicy(1, 3, 'a', "abcde"), input[0])
        assertEquals(Day2.PasswordPolicy(1, 3, 'b', "cdefg"), input[1])
        assertEquals(Day2.PasswordPolicy(2, 9, 'c', "ccccccccc"), input[2])
    }

    @Test
    fun runPart1_shouldCheckForValidPasswords() {
        val result = Day2().testPart1(lines)
        assertEquals(2, result)
    }

    @Test
    fun runPart2_shouldCheckForValidPasswords() {
        val result = Day2().testPart2(lines)
        assertEquals(1, result)
    }
}
