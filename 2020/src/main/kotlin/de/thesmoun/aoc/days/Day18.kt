package de.thesmoun.aoc.days

class Day18 : Day<Collection<String>, Long>("Day 18: Operation Order") {

    override fun parseInput(input: Collection<String>) = input

    override fun runPart1(input: Collection<String>) = input.sumOf { Expression.parse(it).evaluate() }

    override fun runPart2(input: Collection<String>): Long {
        TODO("Not yet implemented")
    }

    abstract class Expression {
        companion object {
            fun parse(s: String) = parseInternal(s.replace(" ", "")).first

            private fun parseInternal(s: String): Pair<Expression, String> {
                var input = s
                var expression: Expression? = null
                var operation = ' '

                while (input.isNotEmpty()) {
                    val char = input.first()
                    if (char.isDigit()) {
                        expression = buildExpression(expression, operation) { Number(char.toString().toLong()) }
                        operation = ' '
                    } else if (char == '+' || char == '*') {
                        operation = char
                    } else if (char == '(') {
                        val (inner, newInput) = parseInternal(input.substring(1))
                        expression = buildExpression(expression, operation) { inner }
                        operation = ' '
                        input = newInput
                    } else if (char == ')') {
                        return expression!! to input
                    }
                    input = input.substring(1)
                }

                return expression!! to input
            }

            private fun buildExpression(expression: Expression?, operation: Char, supplier: () -> Expression): Expression {
                return if (expression == null) {
                    supplier()
                } else {
                    when (operation) {
                        '+' -> Addition(expression, supplier())
                        '*' -> Multiplication(expression, supplier())
                        else -> error("")
                    }
                }
            }
        }

        abstract fun evaluate(): Long
    }

    class Multiplication(private val left: Expression, private val right: Expression) : Expression() {
        override fun evaluate() = left.evaluate() * right.evaluate()
        override fun toString() = "($left * $right)"
    }

    class Addition(private val left: Expression, private val right: Expression) : Expression() {
        override fun evaluate() = left.evaluate() + right.evaluate()
        override fun toString() = "($left + $right)"
    }

    class Number(private val value: Long) : Expression() {
        override fun evaluate() = value
        override fun toString() = value.toString()
    }
}
