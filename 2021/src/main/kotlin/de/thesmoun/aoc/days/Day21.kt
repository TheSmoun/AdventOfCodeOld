package de.thesmoun.aoc.days

class Day21 : Day<Collection<Int>, Long>("Day 21: Dirac Dice") {

    private val regex = Regex("(\\d+)$")

    override fun parseInput(input: Collection<String>) = input.map {
        val (space) = regex.find(it)!!.destructured
        space.toInt()
    }

    override fun runPart1(input: Collection<Int>): Long {
        val players = input.map { Part1Player(it) }
        var currentPlayerIdx = 0
        var currentPlayer = players.first()
        val die = DeterministicDie()

        while (players.none { it.score >= 1000 }) {
            currentPlayer.makeTurn(die)
            currentPlayerIdx = (currentPlayerIdx + 1) % input.size
            currentPlayer = players[currentPlayerIdx]
        }

        return (players.single { it.score < 1000 }.score * die.rolls).toLong()
    }

    override fun runPart2(input: Collection<Int>): Long {
        TODO("Not yet implemented")
    }

    class Part1Player(var space: Int, var score: Int = 0) {
        fun makeTurn(die: DeterministicDie) {
            val amount = (0..2).sumOf { die.roll() }
            var newSpace = (space + amount) % 10
            newSpace = if (newSpace == 0) 10 else newSpace
            space = newSpace
            score += newSpace
        }
    }

    class DeterministicDie(var rolls: Int = 0) {
        fun roll() = rolls++ % 100 + 1
    }
}
