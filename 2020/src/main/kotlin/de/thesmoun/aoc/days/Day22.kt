package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day22 : Day<Pair<MutableList<Int>, MutableList<Int>>, Int>("Day 22: Crab Combat") {

    override fun parseInput(input: Collection<String>): Pair<MutableList<Int>, MutableList<Int>> {
        val (p1, p2) = input.splitAt("")
        return p1.drop(1).map { it.toInt() }.toMutableList() to p2.drop(1).map { it.toInt() }.toMutableList()
    }

    override fun runPart1(input: Pair<MutableList<Int>, MutableList<Int>>): Int {
        val (p1, p2) = input

        while (p1.isNotEmpty() && p2.isNotEmpty()) {
            val c1 = p1.removeFirst()
            val c2 = p2.removeFirst()

            if (c1 > c2) {
                p1.add(c1)
                p1.add(c2)
            } else {
                p2.add(c2)
                p2.add(c1)
            }
        }

        val winningDeck = if (p1.isNotEmpty()) p1 else p2
        return winningDeck.reversed().withIndex().fold(0) { acc, value -> acc + (value.index + 1) * value.value }
    }

    override fun runPart2(input: Pair<MutableList<Int>, MutableList<Int>>): Int {
        val (p1, p2) = input
        return playGame(p1, p2).winningDeck.reversed().withIndex()
                .fold(0) { acc, value -> acc + (value.index + 1) * value.value }
    }

    private fun playGame(deck1: List<Int>, deck2: List<Int>): Result {
        val p1 = deck1.toMutableList()
        val p2 = deck2.toMutableList()

        val knownDecks1 = mutableSetOf<List<Int>>()
        val knownDecks2 = mutableSetOf<List<Int>>()

        while (p1.isNotEmpty() && p2.isNotEmpty()) {
            knownDecks1.add(p1.toList())
            knownDecks2.add(p2.toList())

            val c1 = p1.removeFirst()
            val c2 = p2.removeFirst()

            if (p1.toList() in knownDecks1 || p2.toList() in knownDecks2)
                return Result(1, emptyList())

            val winner = if (c1 <= p1.size && c2 <= p2.size) {
                playGame(p1.take(c1), p2.take(c2)).winner
            } else {
                if (c1 > c2) 1 else 2
            }

            if (winner == 1) {
                p1.add(c1)
                p1.add(c2)
            } else {
                p2.add(c2)
                p2.add(c1)
            }
        }

        return if (p1.isNotEmpty()) Result(1, p1) else Result(2, p2)
    }

    data class Result(val winner: Int, val winningDeck: List<Int>)
}
