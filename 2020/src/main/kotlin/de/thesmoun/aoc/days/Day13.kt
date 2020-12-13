package de.thesmoun.aoc.days

class Day13 : Day<Pair<Long, List<Int>>, Long>("Day 13: Shuttle Search") {

    override fun parseInput(input: Collection<String>) =
        input.first().toLong() to input.last().split(',').map { if (it == "x") -1 else it.toInt() }

    override fun runPart1(input: Pair<Long, List<Int>>): Long {
        val (timestamp, busIds) = input
        val busId = busIds.filter { it > 0 }.minByOrNull { it - (timestamp % it) } ?: error("")
        return (busId * (timestamp / busId + 1) - timestamp) * busId
    }

    override fun runPart2(input: Pair<Long, List<Int>>): Long {
        val buses = input.second.mapIndexed { i, busId -> i to busId }.filter { it.second > 0 }

        var step = buses.first().second.toLong()
        var timestamp = 0L
        for ((i, busId) in buses.drop(1)) {
            while (true) {
                if ((timestamp + i) % busId == 0L) {
                    step *= busId
                    break
                }
                timestamp += step
            }
        }

        return timestamp
    }
}
