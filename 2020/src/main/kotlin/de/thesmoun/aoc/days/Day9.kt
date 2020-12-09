package de.thesmoun.aoc.days

import kotlin.math.max
import kotlin.math.min

class Day9 : Day<Day9.Input, Long>("Day 9: Encoding Error") {

    override fun parseInput(input: Collection<String>) = Input(25, input.map { it.toLong() })

    override fun runPart1(input: Input): Long {
        for ((i, number) in input.numbers.subList(input.preambleLength, input.numbers.lastIndex).withIndex()) {
            val numbers = input.numbers.subList(i, i + input.preambleLength).toSet()
            val sumExists = numbers.any {
                val diff = number - it
                diff in numbers && diff != it
            }
            if (!sumExists)
                return number
        }
        error("")
    }

    override fun runPart2(input: Input): Long {
        val target = runPart1(input)
        for (i in input.numbers.indices) {
            var min = Long.MAX_VALUE
            var max = Long.MIN_VALUE
            var sum = 0L

            for (number in input.numbers.subList(i, input.numbers.lastIndex)) {
                sum += number
                min = min(min, number)
                max = max(max, number)

                if (sum == target)
                    return min + max
            }
        }

        error("")
    }

    data class Input(val preambleLength: Int, val numbers: List<Long>)
}
