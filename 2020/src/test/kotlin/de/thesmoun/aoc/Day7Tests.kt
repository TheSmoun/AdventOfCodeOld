package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day7
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertTrue

class Day7Tests {

    private val lines = """
        light red bags contain 1 bright white bag, 2 muted yellow bags.
        dark orange bags contain 3 bright white bags, 4 muted yellow bags.
        bright white bags contain 1 shiny gold bag.
        muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
        shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
        dark olive bags contain 3 faded blue bags, 4 dotted black bags.
        vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
        faded blue bags contain no other bags.
        dotted black bags contain no other bags.
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnRules() {
        val input = Day7().parseInput(lines)
        assertEquals(9, input.size)
        assertTrue { input.containsKey("light red") }
    }

    @Test
    fun runPart1_shouldCountPathsToShinyGold() {
        val result = Day7().testPart1(lines)
        assertEquals(4, result)
    }

    @Test
    fun runPart2_shouldCountRequiredBags() {
        val result = Day7().testPart2(lines)
        assertEquals(32, result)
    }
}
