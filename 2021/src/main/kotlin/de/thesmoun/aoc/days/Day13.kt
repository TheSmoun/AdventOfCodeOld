package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day13 : Day<Pair<Set<Pair<Int, Int>>, Collection<Day13.Fold>>, String>("Day 13: Transparent Origami") {

    override fun parseInput(input: Collection<String>): Pair<Set<Pair<Int, Int>>, Collection<Fold>> {
        val locationRegex = Regex("^(\\d+),(\\d+)$")
        val instructionRegex = Regex("^fold along ([xy])=(\\d+)$")
        val split = input.splitAt("")
        val locations = split[0].map {
            val (x, y) = locationRegex.find(it)!!.destructured
            x.toInt() to y.toInt()
        }.toSet()
        val instructions = split[1].map {
            val (axis, location) = instructionRegex.find(it)!!.destructured
            when (axis) {
                "x" -> XFold(location.toInt())
                "y" -> YFold(location.toInt())
                else -> throw IllegalArgumentException()
            }
        }
        return locations to instructions
    }

    override fun runPart1(input: Pair<Set<Pair<Int, Int>>, Collection<Fold>>): String {
        return input.second.first().fold(input.first).size.toString()
    }

    override fun runPart2(input: Pair<Set<Pair<Int, Int>>, Collection<Fold>>): String {
        val locations = input.second.fold(input.first) { acc, i -> i.fold(acc) }
        val maxX = locations.maxOf { it.first }
        val maxY = locations.maxOf { it.second }

        repeat(maxY + 1) { y ->
            repeat(maxX + 1) { x ->
                print(if (x to y in locations) '#' else ' ')
            }
            println()
        }
        return ""
    }

    abstract class Fold(val location: Int) {
        abstract fun fold(locations: Set<Pair<Int, Int>>): Set<Pair<Int, Int>>
    }

    class XFold(location: Int) : Fold(location) {
        override fun fold(locations: Set<Pair<Int, Int>>) = locations.map {
            if (it.first <= location)
                it
            else
                (location - (it.first - location)) to it.second
        }.toMutableSet()
    }

    class YFold(location: Int) : Fold(location) {
        override fun fold(locations: Set<Pair<Int, Int>>) = locations.map {
            if (it.second <= location)
                it
            else
                it.first to (location - (it.second - location))
        }.toSet()
    }
}
