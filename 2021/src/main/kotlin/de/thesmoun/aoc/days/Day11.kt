package de.thesmoun.aoc.days

class Day11 : Day<Map<Pair<Int, Int>, Int>, Int>("Day 11: Dumbo Octopus") {

    override fun parseInput(input: Collection<String>): Map<Pair<Int, Int>, Int> {
        val map = mutableMapOf<Pair<Int, Int>, Int>()
        input.forEachIndexed { j, s ->
            s.forEachIndexed { i, c ->
                map[i to j] = c.digitToInt()
            }
        }
        return map
    }

    override fun runPart1(input: Map<Pair<Int, Int>, Int>): Int {
        val map = input.toMutableMap()
        return (0 until 100).sumOf { step(map) }
    }

    override fun runPart2(input: Map<Pair<Int, Int>, Int>): Int {
        val map = input.toMutableMap()
        var steps = 0
        var lastFlashes = 0

        while (lastFlashes != map.size) {
            lastFlashes = step(map)
            steps++
        }

        return steps
    }

    private fun step(map: MutableMap<Pair<Int, Int>, Int>): Int {
        val queue = mutableListOf<Pair<Int, Int>>()
        val flashPositions = mutableListOf<Pair<Int, Int>>()

        map.keys.forEach {
            map[it] = (map[it] ?: 0) + 1
            if (map[it] == 10) {
                queue += it
                flashPositions += it
            }
        }

        while (queue.isNotEmpty()) {
            val flashPos = queue.removeFirst()
            val positions = flashPos.neighbors()
            positions.filter { it in map }.forEach {
                map[it] = (map[it] ?: 0) + 1
                if (map[it] == 10) {
                    queue += it
                    flashPositions += it
                }
            }
        }

        flashPositions.forEach {
            map[it] = 0
        }

        return flashPositions.size
    }

    private fun Pair<Int, Int>.neighbors() = ((first - 1)..(first + 1)).flatMap { x ->
        ((second - 1)..(second + 1)).map { y -> x to y }
    }
}
