package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day21
import kotlin.test.Test
import kotlin.test.assertEquals

class Day21Tests {

    private val lines = """
        mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
        trh fvjkl sbzzf mxmxvkd (contains dairy)
        sqjhc fvjkl (contains soy)
        sqjhc mxmxvkd sbzzf (contains fish)
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnFoods() {
        val input = Day21().parseInput(lines)
        assertEquals(4, input.size)
        assertEquals(Day21.Food(mutableSetOf("mxmxvkd", "kfcds", "sqjhc", "nhms"), mutableSetOf("dairy", "fish")), input.first())
    }

    @Test
    fun runPart1_shouldDoStuff() {
        val result = Day21().testPart1(lines)
        assertEquals(5, result)
    }
}
