package de.thesmoun.aoc.days

import java.util.*

class Day10 : Day<Collection<String>, Long>("Day 10: Syntax Scoring") {

    private val close2Open = mapOf(']' to '[', ')' to '(', '>' to '<', '}' to '{')
    private val open2Close = close2Open.entries.associate { it.value to it.key }
    private val open = close2Open.values.toSet()
    private val errorScore = mapOf(')' to 3, ']' to 57, '}' to 1197, '>' to 25137)
    private val completionScore = mapOf(')' to 1, ']' to 2, '}' to 3, '>' to 4)

    override fun parseInput(input: Collection<String>) = input

    override fun runPart1(input: Collection<String>) = input.sumOf { s ->
        val stack = LinkedList<Char>()
        val invalidChar = s.firstOrNull { c ->
            if (c in open) {
                stack.addFirst(c)
                false
            } else if (stack.first == close2Open[c]) {
                stack.removeFirst()
                false
            } else {
                true
            }
        }

        (errorScore[invalidChar] ?: 0).toLong()
    }

    override fun runPart2(input: Collection<String>): Long {
        val stacks = input.map { s ->
            val stack = LinkedList<Char>()
            s.forEach { c ->
                if (c in open) {
                    stack.addFirst(c)
                } else if (stack.first == close2Open[c]) {
                    stack.removeFirst()
                } else {
                    return@map null
                }
            }
            stack
        }.filterNotNull().map { s -> s.map { c -> open2Close[c]!! } }

        val scores = stacks.map { s -> s.fold(0L) { acc, c -> acc * 5 + completionScore[c]!! } }.sorted()
        return scores[scores.size / 2]
    }
}
