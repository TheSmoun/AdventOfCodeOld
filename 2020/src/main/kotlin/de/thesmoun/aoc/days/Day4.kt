package de.thesmoun.aoc.days

import java.util.*
import kotlin.collections.HashMap

class Day4 : Day<Collection<Map<String, Day4.PassportField>>, Int>("Day 4: Passport Processing") {

    companion object {
        private val requiredFields = listOf("byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid")
    }

    override fun parseInput(input: Collection<String>): Collection<Map<String, PassportField>> {
        val passports = LinkedList<Map<String, PassportField>>()
        var passport = HashMap<String, PassportField>()
        passports.add(passport)

        input.forEach {
            if (it == "") {
                passport = HashMap()
                passports.add(passport)
            } else {
                it.split(' ').forEach { pair ->
                    val split = pair.split(':')
                    when (val key = split[0]) {
                        "byr" -> passport[key] = ByrField(split[1])
                        "iyr" -> passport[key] = IyrField(split[1])
                        "eyr" -> passport[key] = EyrField(split[1])
                        "hgt" -> passport[key] = HgtField(split[1])
                        "hcl" -> passport[key] = HclField(split[1])
                        "ecl" -> passport[key] = EclField(split[1])
                        "pid" -> passport[key] = PidField(split[1])
                        "cid" -> passport[key] = CidField(split[1])
                    }
                }
            }
        }

        return passports
    }

    override fun runPart1(input: Collection<Map<String, PassportField>>) = input.count {
        requiredFields.all { f -> it.containsKey(f) }
    }

    override fun runPart2(input: Collection<Map<String, PassportField>>) = input.count {
        requiredFields.all { f -> it.containsKey(f) } && it.values.all { f -> f.isValid() }
    }

    abstract class PassportField(protected val value: String) {
        abstract fun isValid(): Boolean
    }

    abstract class YearField(value: String, private val range: IntRange) : PassportField(value) {
        override fun isValid(): Boolean {
            val intValue = value.toIntOrNull() ?: return false
            return intValue in range
        }
    }

    abstract class RegexField(value: String, private val regex: Regex) : PassportField(value) {
        override fun isValid() = regex.matches(value)
    }

    class ByrField(value: String) : YearField(value, 1920..2002)

    class IyrField(value: String) : YearField(value, 2010..2020)

    class EyrField(value: String) : YearField(value, 2020..2030)

    class HgtField(value: String) : PassportField(value) {
        override fun isValid(): Boolean {
            when {
                value.endsWith("cm") -> {
                    val intValue = value.substringBefore("cm").toIntOrNull() ?: return false
                    return intValue in 150..193
                }
                value.endsWith("in") -> {
                    val intValue = value.substringBefore("in").toIntOrNull() ?: return false
                    return intValue in 59..76
                }
                else -> {
                    return false
                }
            }
        }
    }

    class HclField(value: String) : RegexField(value, Regex("#[0-9a-f]{6}"))

    class EclField(value: String) : RegexField(value, Regex("(amb|blu|brn|gry|grn|hzl|oth)"))

    class PidField(value: String) : RegexField(value, Regex("[0-9]{9}"))

    class CidField(value: String) : PassportField(value) {
        override fun isValid() = true
    }
}
