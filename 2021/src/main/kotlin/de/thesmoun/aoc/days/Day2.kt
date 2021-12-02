package de.thesmoun.aoc.days

class Day2 : Day<Collection<Day2.Instruction>, Int>("Day 2: Dive!") {

    private val forwardRegex = Regex("^forward (\\d+)$")
    private val downRegex = Regex("^down (\\d+)$")
    private val upRegex = Regex("^up (\\d+)$")

    override fun parseInput(input: Collection<String>) = input.map {
        if (forwardRegex.matches(it))
            ForwardInstruction(it.substring(8).toInt())
        else if (downRegex.matches(it))
            DownInstruction(it.substring(5).toInt())
        else if (upRegex.matches(it))
            UpInstruction(it.substring(3).toInt())
        else
            throw IllegalArgumentException(it)
    }

    override fun runPart1(input: Collection<Instruction>) = input.fold(Location()) { l, i -> i.applyTo(l) }.result1

    override fun runPart2(input: Collection<Instruction>) = input.fold(Location()) { l, i -> i.applyTo(l) }.result2

    data class Location(var horizontal: Int = 0, var depth1: Int = 0, var depth2: Int = 0, var aim: Int = 0) {
        val result1: Int get() = horizontal * depth1
        val result2: Int get() = horizontal * depth2
    }

    abstract class Instruction(val value: Int) {
        abstract fun applyTo(location: Location): Location
    }

    class ForwardInstruction(value: Int) : Instruction(value) {
        override fun applyTo(location: Location): Location {
            location.horizontal += value
            location.depth2 += location.aim * value
            return location
        }
    }

    class DownInstruction(value: Int) : Instruction(value) {
        override fun applyTo(location: Location): Location {
            location.depth1 += value
            location.aim += value
            return location
        }
    }

    class UpInstruction(value: Int) : Instruction(value) {
        override fun applyTo(location: Location): Location {
            location.depth1 -= value
            location.aim -= value
            return location
        }
    }
}
