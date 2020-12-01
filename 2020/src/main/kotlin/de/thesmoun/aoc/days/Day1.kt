package de.thesmoun.aoc.days

class Day1 : Day<Collection<Int>, Int>("Day 1") {

    private val target = 2020

    override fun parseInput(input: Collection<String>) = input.map { it.toInt() }

    override fun runPart1(input: Collection<Int>): Int {
        val set = input.toHashSet()
        set.forEach {
            val diff = target - it
            if (set.contains(diff))
                return it * diff
        }

        throw IllegalArgumentException()
    }

    override fun runPart2(input: Collection<Int>): Int {
        val array = input.toIntArray()
        for (i in array.indices) {
            val n0 = array[i]
            for (j in (i + 1)..array.lastIndex) {
                val n1 = array[j]
                for (k in (j + 1)..array.lastIndex) {
                    val n2 = array[k]
                    if (n0 + n1 + n2 == target) {
                        return n0 * n1 * n2
                    }
                }
            }
        }

        throw IllegalArgumentException()
    }
}
