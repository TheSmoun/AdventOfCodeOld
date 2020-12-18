package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day18 : Day<Collection<String>, Long>("Day 18: Operation Order") {

    override fun parseInput(input: Collection<String>) = input

    override fun runPart1(input: Collection<String>) = input.sumOf { Expression.parse1(it).evaluate() }

    override fun runPart2(input: Collection<String>) = input.sumOf { Expression.parse2(it).evaluate() }

    abstract class Expression {
        companion object {
            fun parse1(s: String) = parseInternal(s.replace(" ", "")).first
            fun parse2(s: String) = parseInternal(preprocess(s.replace(" ", ""))).first

            fun preprocess(s: String): String {
                return (0 until s.count { it == '+' }).fold(s) { acc, i ->  encapsulatePlus(i, acc) }
            }

            fun encapsulatePlus(plusIndex: Int, s: String): String {
                val index = s.withIndex().filter { it.value == '+' }[plusIndex].index
                var expression = s

                var level = 0
                for (i in (index - 1) downTo 0) {
                    val char = expression[i]
                    if (char == ')') {
                        level++
                    } else if (char == '(') {
                        level--
                        if (level <= 0) {
                            val (p0, p1) = expression.splitAt(i)
                            expression = "$p0($p1"
                            break
                        }
                    } else if (char.isDigit() && level == 0) {
                        val (p0, p1) = expression.splitAt(i)
                        expression = "$p0($p1"
                        break
                    }
                }

                expression = "(".repeat(level) + expression

                level = 0
                for (i in (index + 2)..expression.lastIndex) {
                    val char = expression[i]
                    if (char == '(') {
                        level++
                    } else if (char == ')') {
                        level--
                        if (level <= 0) {
                            val (p0, p1) = expression.splitAt(i + 1)
                            expression = "$p0)$p1"
                            break
                        }
                    } else if (char.isDigit() && level == 0) {
                        val (p0, p1) = expression.splitAt(i + 1)
                        expression = "$p0)$p1"
                        break
                    }
                }

                expression += ")".repeat(level)

                return expression
            }

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
