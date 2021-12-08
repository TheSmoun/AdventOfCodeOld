package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.removeSingle

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
            setOf('a', 'c', 'd', 'f', 'g') to 3,
            setOf('b', 'c', 'd', 'f') to 4,
            setOf('a', 'b', 'd', 'f', 'g') to 5,
            setOf('a', 'b', 'd', 'e', 'f', 'g') to 6,
            setOf('a', 'c', 'f') to 7,
            setOf('a', 'b', 'c', 'd', 'e', 'f', 'g') to 8,
            setOf('a', 'b', 'c', 'd', 'f', 'g') to 9,
        )
        val numberToSegments = segmentsToNumber.entries.associate { it.value to it.key }

        return input.sumOf { line ->
            val possibilities = mapOf(
                'a' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'b' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'c' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'd' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'e' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'f' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
                'g' to mutableSetOf('a', 'b', 'c', 'd', 'e', 'f', 'g'),
            )

            val hints = line.first.toMutableList()

            val oneChars = numberToSegments[1]!!
            val oneHint = hints.removeSingle { it.length == 2 }.toSet()
            oneHint.forEach { possibilities[it]?.retainAll(oneChars) }
            possibilities.filter { it.key !in oneHint }.forEach { it.value.removeAll(oneChars) }

            val sevenChars = numberToSegments[7]!!
            val sevenHint = hints.removeSingle { it.length == 3 }.toSet()
            sevenHint.forEach { if (it !in oneHint) possibilities[it]?.retainAll(setOf('a')) }
            possibilities.filter { it.key !in sevenHint }.forEach { it.value.removeAll(sevenChars) }

            val fourChars = numberToSegments[4]!!
            val fourHint = hints.removeSingle { it.length == 4 }.toSet()
            fourHint.forEach { if (it !in sevenHint) possibilities[it]?.retainAll(setOf('b', 'd')) }
            possibilities.filter { it.key !in fourHint }.forEach { it.value.removeAll(fourChars) }

            val nineHint = hints.removeSingle { it.length == 6 && fourHint.all { c -> c in it } }.toSet()
            val nineHintMutable = nineHint.toMutableSet()
            nineHintMutable.removeAll(sevenHint)
            nineHintMutable.removeAll(fourHint)
            val g = nineHintMutable.single()
            possibilities[g]?.retainAll(setOf('g'))
            possibilities.filter { it.key != g }.forEach { it.value.remove('g') }

            val eightHint = hints.removeSingle { it.length == 7 }.toSet()
            val eightHintMutable = eightHint.toMutableSet()
            eightHintMutable.removeAll(nineHint)
            val e = eightHintMutable.single()
            possibilities[e]?.retainAll(setOf('e'))
            possibilities.filter { it.key != e }.forEach { it.value.remove('e') }

            val threeHint = hints.removeSingle { it.length == 5 && it.toSet().containsAll(oneHint) }.toSet()
            val threeHintMutable = threeHint.toMutableSet()
            threeHintMutable.removeAll(sevenHint)
            threeHintMutable.remove(g)
            val d = threeHintMutable.single()
            possibilities[d]?.retainAll(setOf('d'))
            possibilities.filter { it.key != d }.forEach { it.value.remove('d') }

            val sixHint = hints.removeSingle { it.length == 6 && !it.toSet().containsAll(oneHint) }.toSet()
            val sixHintMutable = sixHint.toMutableSet()
            sixHintMutable.removeAll { !oneHint.contains(it) }
            val f = sixHintMutable.single()
            possibilities[f]?.retainAll(setOf('f'))
            possibilities.filter { it.key != f }.forEach { it.value.remove('f') }

            val mapping = possibilities.mapValues { it.value.single() }
            line.second.map { s -> segmentsToNumber[s.map { mapping[it]!! }.toSet()]!! }
                .fold(0) { acc, curr -> acc * 10 + curr }.toInt()
        }
    }
}
