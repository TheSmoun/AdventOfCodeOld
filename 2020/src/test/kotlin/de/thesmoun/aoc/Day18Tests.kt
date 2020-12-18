package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day18
import kotlin.test.Test
import kotlin.test.assertEquals

class Day18Tests {

    @Test
    fun expressionParsingAndEvaluation() {
        testExpression("1 + 2 * 3 + 4 * 5 + 6", 71)
        testExpression("1 + (2 * 3) + (4 * (5 + 6))", 51)
        testExpression("2 * 3 + (4 * 5)", 26)
        testExpression("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)
        testExpression("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)
        testExpression("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)
    }

    private fun testExpression(input: String, expectedResult: Long) {
        val expression = Day18.Expression.parse(input)
        val result = expression.evaluate()
        assertEquals(expectedResult, result)
    }
}
