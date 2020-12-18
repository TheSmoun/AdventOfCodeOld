package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day18
import de.thesmoun.aoc.util.splitAt
import kotlin.test.Test
import kotlin.test.assertEquals

class Day18Tests {

    @Test
    fun expressionParsingAndEvaluation1() {
        testExpression1("1 + 2 * 3 + 4 * 5 + 6", 71)
        testExpression1("1 + (2 * 3) + (4 * (5 + 6))", 51)
        testExpression1("2 * 3 + (4 * 5)", 26)
        testExpression1("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)
        testExpression1("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)
        testExpression1("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)
    }

    @Test
    fun expressionParsingAndEvaluation2() {
        testExpression2("1 + 2 * 3 + 4 * 5 + 6", 231)
        testExpression2("1 + (2 * 3) + (4 * (5 + 6))", 51)
        testExpression2("2 * 3 + (4 * 5)", 46)
        testExpression2("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)
        testExpression2("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)
        testExpression2("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)
    }

    @Test
    fun string_splitAt_shouldProperlySplitAString() {
        assertEquals("012" to "345", "012345".splitAt(3))
    }

    @Test
    fun encapsulatePlus() {
        assertEquals("(1+2)", Day18.Expression.encapsulatePlus(0, "1+2"))
        assertEquals("(1+2)+3", Day18.Expression.encapsulatePlus(0, "1+2+3"))
        assertEquals("(1+(2+3))", Day18.Expression.encapsulatePlus(0, "1+(2+3)"))
        assertEquals("1+((2+3))", Day18.Expression.encapsulatePlus(1, "1+(2+3)"))
        assertEquals("((1+2)+3)", Day18.Expression.encapsulatePlus(1, "(1+2)+3"))
        assertEquals("(1+((2+3)))", Day18.Expression.encapsulatePlus(1, "(1+(2+3))"))
    }

    @Test
    fun preprocess() {
        assertEquals("(1+2)", Day18.Expression.preprocess("1+2"))
        assertEquals("((1+2)+3)", Day18.Expression.preprocess("1+2+3"))
    }

    private fun testExpression1(input: String, expectedResult: Long) {
        val expression = Day18.Expression.parse1(input)
        val result = expression.evaluate()
        assertEquals(expectedResult, result)
    }

    private fun testExpression2(input: String, expectedResult: Long) {
        val expression = Day18.Expression.parse2(input)
        val result = expression.evaluate()
        assertEquals(expectedResult, result)
    }
}
