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
        TODO()
    }

    private fun runPart(map: Map<Int, String>, messages: Collection<String>): Int {
        val possibilities = Rule.parse(map[0] ?: error(""), map).possibilities()
        return messages.count { it in possibilities }
    }

    abstract class Rule {
        companion object {
            fun parse(body: String, map: Map<Int, String>): Rule {
                return when {
                    body.contains(" | ") -> {
                        val left = parse(body.substringBefore(" | "), map)
                        val right = parse(body.substringAfter(" | "), map)
                        Branch(left, right)
                    }
                    body.contains(' ') -> {
                        val left = parse(body.substringBefore(' '), map)
                        val right = parse(body.substringAfter(' '), map)
                        Composite(left, right)
                    }
                    body.contains('"') -> {
                        Literal(body.replace("\"", ""))
                    }
                    else -> {
                        Proxy(parse(map[body.toInt()] ?: error(""), map))
                    }
                }
            }
        }

        abstract fun possibilities(): Set<String>
    }

    class Branch(private val left: Rule, private val right: Rule) : Rule() {
        override fun possibilities(): Set<String> {
            val set = left.possibilities().toMutableSet()
            set.addAll(right.possibilities())
            return set
        }
    }

    class Composite(private val left: Rule, private val right: Rule) : Rule() {
        override fun possibilities(): Set<String> {
            val set = mutableSetOf<String>()
            for (l in left.possibilities()) {
                for (r in right.possibilities()) {
                    set.add(l + r)
                }
            }
            return set
        }
    }

    class Proxy(private val rule: Rule) : Rule() {
        override fun possibilities() = rule.possibilities()
    }

    class Literal(private val char: String) : Rule() {
        override fun possibilities() = setOf(char)
    }
}
