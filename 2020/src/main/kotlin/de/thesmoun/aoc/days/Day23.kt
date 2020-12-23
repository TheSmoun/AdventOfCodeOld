package de.thesmoun.aoc.days

class Day23 : Day<List<Day23.Cup>, Any>("Day 23: Crab Cups") {

    override fun parseInput(input: Collection<String>) = input.single().map { Cup(it.toString().toInt()) }

    override fun runPart1(input: List<Cup>) = simulateGame(input, 100).order()

    override fun runPart2(input: List<Cup>): Any {
        val cups = input.toMutableList()
        val max = input.maxByOrNull { it.value }!!.value
        cups.addAll(((max + 1)..1000000).map { Cup(it) })
        return simulateGame(cups, 10000000).getCupsNextTo1()
    }

    private fun simulateGame(cups: List<Cup>, times: Int): Game {
        val game = Game(cups)
        repeat(times) {
            game.simulateRound()
        }
        return game
    }

    class Game(cups: List<Cup>) {

        private var current: Cup
        private val map: Map<Int, Cup>
        private val max = cups.maxByOrNull { it.value }!!.value

        init {
            for (i in cups.indices) {
                cups[i].next = cups[(i + 1) % cups.size]
            }
            current = cups.first()
            map = cups.associateBy { it.value }
        }

        fun simulateRound() {
            val firstPickedCup = pickCups()
            val destination = selectDestination(listOf(firstPickedCup.value,
                    firstPickedCup.next.value, firstPickedCup.next.next.value))
            insertCups(destination, firstPickedCup)
            current = current.next
        }

        fun order(): String {
            var result = ""
            val one = map[1] ?: error("")
            var cup = one.next
            while (cup != one) {
                result += cup.value
                cup = cup.next
            }
            return result
        }

        fun getCupsNextTo1(): Long {
            val one = map[1] ?: error("")
            return 1L * one.next.value * one.next.next.value
        }

        private fun pickCups(): Cup {
            val firstPickedCup = current.next
            current.next = current.next.next.next.next
            return firstPickedCup
        }

        private fun selectDestination(pickedValues: List<Int>): Cup {
            var destinationValue = if (current.value == 1) max else current.value - 1
            while (destinationValue in pickedValues) {
                --destinationValue
                if (destinationValue < 1)
                    destinationValue = max
            }
            return map[destinationValue] ?: error("")
        }

        private fun insertCups(destination: Cup, firstPickedCup: Cup) {
            firstPickedCup.next.next.next = destination.next
            destination.next = firstPickedCup
        }

        override fun toString(): String {
            var result = "(${current.value})"
            var cup = current.next
            while (cup != current) {
                result += " ${cup.value} "
                cup = cup.next
            }
            return result
        }
    }

    data class Cup(val value: Int) {
        lateinit var next: Cup
    }
}
