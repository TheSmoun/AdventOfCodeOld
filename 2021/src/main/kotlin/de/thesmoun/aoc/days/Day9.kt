package de.thesmoun.aoc.days

class Day9 : Day<Map<Pair<Int, Int>, Int>, Int>("Day 9: Smoke Basin") {

    override fun parseInput(input: Collection<String>): Map<Pair<Int, Int>, Int> {
        val map = mutableMapOf<Pair<Int, Int>, Int>()
        input.forEachIndexed { i, s ->
            s.forEachIndexed { j, c ->
                map[i to j] = c.digitToInt()
            }
        }
        return map
    }

    override fun runPart1(input: Map<Pair<Int, Int>, Int>) = input.filter { entry ->
        neighbors(entry.key).filter { it in input }.all { input[it]!! > entry.value }
    }.entries.sumOf { it.value + 1 }

    override fun runPart2(input: Map<Pair<Int, Int>, Int>): Int {
        val allPositions = input.filter { it.value < 9 }.keys.toMutableSet()
        val basinSizes = mutableListOf<Int>()

        while (allPositions.isNotEmpty()) {
            val position = allPositions.first()
            allPositions.remove(position)
            val positions = mutableSetOf(position)
            var grew = true

            while (grew) {
                val newPositions = mutableSetOf<Pair<Int, Int>>()
                positions.forEach { pos ->
                    neighbors(pos).forEach {
                        if (allPositions.remove(it)) {
                            newPositions.add(it)
                        }
                    }
                }
                if (newPositions.isNotEmpty()) {
                    positions.addAll(newPositions)
                } else {
                    grew = false
                }
            }

            basinSizes.add(positions.size)
        }

        basinSizes.sortDescending()
        return basinSizes.take(3).reduce { acc, i -> acc * i }
    }

    private fun neighbors(pos: Pair<Int, Int>): Set<Pair<Int, Int>> {
        return setOf(
            (pos.first - 1) to pos.second,
            (pos.first + 1) to pos.second,
            pos.first to (pos.second - 1),
            pos.first to (pos.second + 1)
        )
    }
}
