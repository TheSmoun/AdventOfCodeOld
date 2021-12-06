package de.thesmoun.aoc.days

class Day6 : Day<Map<Int, Long>, Long>("Day 6: Lanternfish") {

    override fun parseInput(input: Collection<String>) = input.first().split(',').map { it.toInt() }
        .groupingBy { it }.eachCount().entries.associate { it.key to it.value.toLong() }

    override fun runPart1(input: Map<Int, Long>) = run(input, 80)

    override fun runPart2(input: Map<Int, Long>) = run(input, 256)

    private fun run(input: Map<Int, Long>, days: Int): Long {
        var population = input.toMutableMap()
        repeat(days) { _ ->
            val newPopulation = population.entries.associate { (it.key - 1) to it.value }.toMutableMap()
            val newFish = newPopulation.remove(-1) ?: 0
            if (newFish > 0) {
                newPopulation[8] = newFish
                newPopulation[6] = (newPopulation[6] ?: 0) + newFish
            }

            population = newPopulation
        }

        return population.values.sum()
    }
}
