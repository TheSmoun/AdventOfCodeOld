package de.thesmoun.aoc.days

class Day2 : Day<Collection<Pair<Day2.PasswordPolicy, String>>, Int>("Day 2: Password Philosophy") {

    override fun parseInput(input: Collection<String>): Collection<Pair<PasswordPolicy, String>> = input.map {
        val parts = it.split(' ', ':')
        val rangeParts = parts[0].split('-')
        Pair(PasswordPolicy(rangeParts[0].toInt(), rangeParts[1].toInt(), parts[1][0]), parts[3])
    }

    override fun runPart1(input: Collection<Pair<PasswordPolicy, String>>) = input.count {
        val policy = it.first
        it.second.count { c -> c == policy.char } in policy.min..policy.max
    }

    override fun runPart2(input: Collection<Pair<PasswordPolicy, String>>) = input.count {
        val policy = it.first
        val password = it.second
        (password[policy.min - 1] == policy.char) xor (password[policy.max - 1] == policy.char)
    }

    data class PasswordPolicy(val min: Int, val max: Int, val char: Char)
}
