package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day19 : Day<Pair<Map<Int, String>, Collection<String>>, Int>("Day 19: Monster Messages") {

    override fun parseInput(input: Collection<String>): Pair<Map<Int, String>, Collection<String>> {
        val (rules, messages) = input.splitAt("")
        return rules.associate {
            val (id, body) = Regex("^(\\d+): (.*)$").find(it)!!.destructured
            id.toInt() to body
        } to messages
    }

    override fun runPart1(input: Pair<Map<Int, String>, Collection<String>>) = runPart(input.first, input.second)

    override fun runPart2(input: Pair<Map<Int, String>, Collection<String>>): Int {
        val map = input.first.toMutableMap()
        map[8] = "42 | 42 8"
        map[11] = "42 31 | 42 11 31"
        return runPart(map, input.second)
    }

    private fun runPart(map: Map<Int, String>, messages: Collection<String>): Int {
        val rules = mutableMapOf<Int, Rule>()
        rules.putAll(map.mapValues { Rule.parse(it.value, rules) })
        return messages.filter { rules[0]!!.findMatches(it).filter { m -> m.isBlank() }.any() }.size
    }

    abstract class Rule {
        companion object {
            fun parse(rule: String, rules: Map<Int, Rule>): Rule {
                return when {
                    rule.contains('"') -> Literal(rule[1])
                    rule.contains('|') -> {
                        val (left, right) = rule.split(" | ")
                        Branch(parse(left, rules), parse(right, rules))
                    }
                    else -> Composite(rule.split(' ').map { Proxy(it.toInt(), rules) })
                }
            }
        }

        abstract fun findMatches(message: String): Sequence<String>
    }

    class Branch(private val left: Rule, private val right: Rule): Rule() {
        override fun findMatches(message: String) = sequence {
            yieldAll(left.findMatches(message))
            yieldAll(right.findMatches(message))
        }
    }

    class Composite(private val rules: List<Rule>) : Rule() {
        override fun findMatches(message: String) = findMatches(message, 0)

        private fun findMatches(message: String, rule: Int): Sequence<String> {
            if (rule >= rules.size)
                return sequenceOf(message)
            return rules[rule].findMatches(message).flatMap { findMatches(it, rule + 1) }
        }
    }

    class Proxy(private val rule: Int, private val rules: Map<Int, Rule>) : Rule() {
        override fun findMatches(message: String) = (rules[rule] ?: error("")).findMatches(message)
    }

    class Literal(private val char: Char) : Rule() {
        override fun findMatches(message: String): Sequence<String> {
            return when {
                message.isEmpty() -> emptySequence()
                message.first() == char -> sequenceOf(message.drop(1))
                else -> emptySequence()
            }
        }
    }
}
