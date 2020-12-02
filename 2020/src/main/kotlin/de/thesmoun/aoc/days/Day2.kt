package de.thesmoun.aoc.days

class Day2 : Day<Collection<Day2.PasswordPolicy>, Int>("Day 2: Password Philosophy") {

    override fun parseInput(input: Collection<String>) = input.map {
        val (min, max, char, password) = Regex("(\\d+)-(\\d+) (.): (\\w+)").find(it)!!.destructured
        PasswordPolicy(min.toInt(), max.toInt(), char[0], password)
    }

    override fun runPart1(input: Collection<PasswordPolicy>) = input.count { it.matchesCharCount() }

    override fun runPart2(input: Collection<PasswordPolicy>) = input.count { it.matchesCharPosition() }

    data class PasswordPolicy(val min: Int, val max: Int, val char: Char, val password: String) {
        fun matchesCharCount() = password.count { it == char } in min..max
        fun matchesCharPosition() = (password[min - 1] == char) xor (password[max - 1] == char)
    }
}
