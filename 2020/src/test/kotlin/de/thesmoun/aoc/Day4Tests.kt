package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day4
import kotlin.test.*

class Day4Tests {

    private val lines = """
        ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
        byr:1937 iyr:2017 cid:147 hgt:183cm

        iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
        hcl:#cfa07d byr:1929

        hcl:#ae17e1 iyr:2013
        eyr:2024
        ecl:brn pid:760753108 byr:1931
        hgt:179cm

        hcl:#cfa07d eyr:2025 pid:166559648
        iyr:2011 ecl:brn hgt:59in
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnBatches() {
        val input = Day4().parseInput(lines).toTypedArray()
        assertEquals(4, input.size)
        assertEquals(8, input[0].size)
    }

    @Test
    fun runPart1_shouldCountValidPassports() {
        val result = Day4().testPart1(lines)
        assertEquals(2, result)
    }

    @Test
    fun runPart2_shouldCountValidFields() {
        val result = Day4().testPart2(lines)
        assertEquals(2, result)
    }
}
