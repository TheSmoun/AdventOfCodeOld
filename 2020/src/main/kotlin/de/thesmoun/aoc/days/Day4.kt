package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt
import kotlin.collections.HashMap

class Day4 : Day<Collection<Map<String, String>>, Int>("Day 4: Passport Processing") {

    private val requiredFields = mapOf(
            Pair("byr", Regex("19[2-9][0-9]|200[0-2]")),
            Pair("iyr", Regex("201[0-9]|2020")),
            Pair("eyr", Regex("202[0-9]|2030")),
            Pair("hgt", Regex("1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in")),
            Pair("hcl", Regex("#[0-9a-f]{6}")),
            Pair("ecl", Regex("amb|blu|brn|gry|grn|hzl|oth")),
            Pair("pid", Regex("[0-9]{9}"))
    )

    override fun parseInput(input: Collection<String>) = input.splitAt("").map {
        val passport = HashMap<String, String>()
        it.forEach { l ->
            l.split(' ').forEach { pair ->
                val split = pair.split(':')
                passport[split[0]] = split[1]
            }
        }
        passport
    }

    override fun runPart1(input: Collection<Map<String, String>>) = input.count {
        requiredFields.keys.all { f -> it.containsKey(f) }
    }

    override fun runPart2(input: Collection<Map<String, String>>) = input.count {
        requiredFields.all { f -> it.containsKey(f.key) && f.value.matches(it[f.key] ?: error("")) }
    }
}
