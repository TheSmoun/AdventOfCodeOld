package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day4 : Day<Pair<List<Int>, List<Day4.Board>>, Int>("Day 4: Giant Squid") {

    override fun parseInput(input: Collection<String>): Pair<List<Int>, List<Board>> {
        val splitInput = input.splitAt("")
        val numbers = splitInput[0].first().split(',').map { it.toInt() }
        val boards = splitInput.drop(1).map { Board.parse(it) }
        return numbers to boards
    }

    override fun runPart1(input: Pair<List<Int>, List<Board>>): Int {
        input.first.forEach { n ->
            input.second.forEach {
                val winningSum = it.mark(n)
                if (winningSum != null)
                    return winningSum * n
            }
        }
        throw IllegalStateException()
    }

    override fun runPart2(input: Pair<List<Int>, List<Board>>): Int {
        input.first.forEach { n ->
            input.second.forEach {
                val winningSum = it.mark(n)
                if (winningSum != null && input.second.all { b -> b.hasWon })
                    return winningSum * n
            }
        }
        throw IllegalStateException()
    }

    class Board(private val positions: Map<Int, Pair<Int, Int>>,
                private val numbers: Map<Pair<Int, Int>, Int>,
                private val marked: MutableSet<Pair<Int, Int>> = mutableSetOf(),
                var hasWon: Boolean = false) {
        companion object {
            fun parse(input: Collection<String>): Board {
                val positions = mutableMapOf<Int, Pair<Int, Int>>()
                val numbers = mutableMapOf<Pair<Int, Int>, Int>()
                input.forEachIndexed { i, s ->
                    s.trim().replace("  ", " ").split(' ').forEachIndexed { j, n ->
                        positions[n.toInt()] = i to j
                        numbers[i to j] = n.toInt()
                    }
                }
                return Board(positions, numbers)
            }
        }

        fun mark(number: Int): Int? {
            val position = positions[number] ?: return null
            marked.add(position)
            val winningNumbers = checkIsWinner() ?: return null
            val unmarked = numbers.keys.filter { !marked.contains(it) }
            hasWon = true
            return unmarked.sumOf { numbers[it]!! }
        }

        private fun checkIsWinner(): List<Int>? {
            return checkIsWinnerByCols() ?: checkIsWinnerByRows()
        }

        private fun checkIsWinnerByCols(): List<Int>? {
            repeat(5) { i ->
                if ((0 until 5).all { marked.contains(i to it) }) {
                    return (0 until 5).map { numbers[i to it]!! }
                }
            }
            return null
        }

        private fun checkIsWinnerByRows(): List<Int>? {
            repeat(5) { i ->
                if ((0 until 5).all { marked.contains(it to i) }) {
                    return (0 until 5).map { numbers[it to i]!! }
                }
            }
            return null
        }
    }
}
