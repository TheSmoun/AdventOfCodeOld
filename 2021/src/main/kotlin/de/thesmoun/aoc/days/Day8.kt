package de.thesmoun.aoc.days

class Day8 : Day<List<Pair<List<String>, List<String>>>, Int>("Day 8: Seven Segment Search") {

    override fun parseInput(input: Collection<String>) = input.map { s ->
        val parts = s.split('|').map { it.trim() }
        parts[0].split(' ') to parts[1].split(' ')
    }

    override fun runPart1(input: List<Pair<List<String>, List<String>>>): Int {
        val lengths = setOf(2, 3, 4, 7)
        return input.sumOf { it.second.count { s -> s.length in lengths } }
    }

    override fun runPart2(input: List<Pair<List<String>, List<String>>>): Int {
        val segmentsToNumber = mapOf(
            setOf('a', 'b', 'c', 'e', 'f', 'g') to 0,
            setOf('c', 'f') to 1,
            setOf('a', 'c', 'd', 'e', 'g') to 2,
            setOf('a', 'c', 'd', 'f') to 4,
            setOf('a', 'b', 'd', 'f', 'g') to 5,
            setOf('a', 'b', 'd', 'e', 'f', 'g') to 6,
            setOf('a', 'c', 'f') to 7,
            setOf('a', 'b', 'c', 'd', 'e', 'f', 'g') to 8,
            setOf('a', 'b', 'c', 'd', 'f', 'g') to 9,
        )

        input.map { line ->
            val possibilities = mutableMapOf(
                'a' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'b' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'c' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'd' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'e' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'f' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'g' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
            )

            val hints = line.first.toMutableList()
            while (hints.isNotEmpty()) {
                for (hint in hints) {
                    when (hint.length) {
                        2 -> hint.forEach { c -> possibilities[c]?.removeAll(setOf('a', 'b', 'd', 'e', 'g')) }
                        3 -> hint.forEach { c -> possibilities[c]?.removeAll(setOf('b', 'd', 'e', 'g')) }
                        4 -> hint.forEach { c -> possibilities[c]?.removeAll(setOf('b', 'e', 'g')) }
                    }

                    if (hint.all { c -> possibilities[c]!!.count() == 1 }) {
                        hints.remove(hint)
                    }
                }
            }
        }

        TODO("Not yet implemented")
    }
}
