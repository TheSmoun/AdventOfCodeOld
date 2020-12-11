package de.thesmoun.aoc.days

import kotlin.math.max

class Day11 : Day<Day11.Seats, Int>("Day 11: Seating System") {

    override fun parseInput(input: Collection<String>): Seats {
        val map = mutableMapOf<Pos, Boolean>()
        var cols = 0
        var rows = 0
        for ((y, line) in input.withIndex()) {
            for ((x, char) in line.withIndex()) {
                if (char == 'L') {
                    map[Pos(x, y)] = false
                }
                rows = max(rows, y)
            }
            cols = max(cols, y)
        }
        return Seats(cols + 1, rows + 1, map)
    }

    override fun runPart1(input: Seats) = run(input, 4) { map, pos ->  map.getAdjacentSeats(pos) }

    override fun runPart2(input: Seats) = run(input, 5) { map, pos -> map.getVisibleSeats(pos, input) }

    private fun run(input: Seats, threshold: Int, getAdjacentSeats: (map: Map<Pos, Boolean>, pos: Pos) -> Sequence<Boolean>): Int {
        var current = input.seats
        val next = mutableMapOf<Pos, Boolean>()

        while (current != next) {
            if (next.isNotEmpty()) {
                current = next.toMap()
            }

            for ((key, value) in current) {
                val adjacentSeats = getAdjacentSeats(current, key).toList()
                if (!value) {
                    next[key] = adjacentSeats.none { it }
                } else {
                    next[key] = adjacentSeats.count { it } < threshold
                }
            }
        }

        return current.count { it.value }
    }

    data class Seats(val cols: Int, val rows: Int, val seats: Map<Pos, Boolean>)

    data class Pos(val x: Int, val y: Int) {
        operator fun plus(delta: Pos) = Pos(x + delta.x, y + delta.y)
    }

    private fun Map<Pos, Boolean>.getVisibleSeats(pos: Pos, input: Seats) = sequence {
        val xBounds = 0 until input.rows
        val yBounds = 0 until input.cols
        getNextSeatInDirection(pos, Pos(-1, -1), xBounds, yBounds)?.let { yield(it) }
        getNextSeatInDirection(pos, Pos(0, -1), xBounds, yBounds)?.let { yield(it) }
        getNextSeatInDirection(pos, Pos(1, -1), xBounds, yBounds)?.let { yield(it) }
        getNextSeatInDirection(pos, Pos(-1, 0), xBounds, yBounds)?.let { yield(it) }
        getNextSeatInDirection(pos, Pos(1, 0), xBounds, yBounds)?.let { yield(it) }
        getNextSeatInDirection(pos, Pos(-1, 1), xBounds, yBounds)?.let { yield(it) }
        getNextSeatInDirection(pos, Pos(0, 1), xBounds, yBounds)?.let { yield(it) }
        getNextSeatInDirection(pos, Pos(1, 1), xBounds, yBounds)?.let { yield(it) }
    }

    private fun Map<Pos, Boolean>.getNextSeatInDirection(pos: Pos, delta: Pos, xBounds: IntRange, yBounds: IntRange): Boolean? {
        return generateSequence(pos + delta) { it + delta }.takeWhile { it.x in xBounds && it.y in yBounds }.map { get(it) }.firstOrNull { it != null }
    }

    private fun Map<Pos, Boolean>.getAdjacentSeats(pos: Pos) = sequence {
        get(Pos(pos.x - 1, pos.y - 1))?.let { yield(it) }
        get(Pos(pos.x, pos.y - 1))?.let { yield(it) }
        get(Pos(pos.x + 1, pos.y - 1))?.let { yield(it) }
        get(Pos(pos.x - 1, pos.y))?.let { yield(it) }
        get(Pos(pos.x + 1, pos.y))?.let { yield(it) }
        get(Pos(pos.x - 1, pos.y + 1))?.let { yield(it) }
        get(Pos(pos.x, pos.y + 1))?.let { yield(it) }
        get(Pos(pos.x + 1, pos.y + 1))?.let { yield(it) }
    }
}
