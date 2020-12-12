package de.thesmoun.aoc.days

import kotlin.math.*

class Day12 : Day<Collection<Pair<String, Int>>, Int>("Day 12: Rain Risk") {

    override fun parseInput(input: Collection<String>) = input.map {
        val (action, value) = Regex("([NESWLRF])(\\d+)").find(it)!!.destructured
        action to value.toInt()
    }

    override fun runPart1(input: Collection<Pair<String, Int>>): Int {
        val ship = Ship()

        input.forEach {
            val (action, value) = it
            when (action) {
                "N" -> ship.moveBy(Direction.NORTH.delta * value)
                "E" -> ship.moveBy(Direction.EAST.delta * value)
                "S" -> ship.moveBy(Direction.SOUTH.delta * value)
                "W" -> ship.moveBy(Direction.WEST.delta * value)
                "L" -> ship.rotateLeft(value)
                "R" -> ship.rotateRight(value)
                "F" -> ship.moveBy(ship.direction.delta * value)
            }
        }

        return ship.getDistance()
    }

    override fun runPart2(input: Collection<Pair<String, Int>>): Int {
        val ship = Ship()
        var waypoint = Point(10, 1)

        input.forEach {
            val (action, value) = it
            when (action) {
                "N" -> waypoint += Direction.NORTH.delta * value
                "E" -> waypoint += Direction.EAST.delta * value
                "S" -> waypoint += Direction.SOUTH.delta * value
                "W" -> waypoint += Direction.WEST.delta * value
                "L" -> waypoint = waypoint.rotate(value)
                "R" -> waypoint = waypoint.rotate(-value)
                "F" -> ship.moveBy(waypoint * value)
            }
        }

        return ship.getDistance()
    }

    class Ship(var direction: Direction = Direction.EAST, var pos: Point = Point(0, 0)) {
        fun moveBy(delta: Point) {
            pos += delta
        }

        fun rotateLeft(degrees: Int) {
            direction = Direction.fromDegrees(direction.degrees - degrees)
        }

        fun rotateRight(degrees: Int) {
            direction = Direction.fromDegrees(direction.degrees + degrees)
        }

        fun getDistance() = abs(pos.x) + abs(pos.y)
    }

    data class Point(val x: Int, val y: Int) {
        operator fun plus(other: Point) = Point(x + other.x, y + other.y)
        operator fun minus(other: Point) = Point(x - other.x, y - other.y)
        operator fun times(factor: Int) = Point(x * factor, y * factor)

        fun rotate(degrees: Int): Point {
            val rad = degrees * PI / 180.0
            val cos = cos(rad)
            val sin = sin(rad)
            return Point((cos * x - sin * y).roundToInt(), (sin * x + cos * y).roundToInt())
        }
    }

    enum class Direction(val delta: Point, val degrees: Int) {
        NORTH(Point(0, 1), 0),
        EAST(Point(1, 0), 90),
        SOUTH(Point(0, -1), 180),
        WEST(Point(-1, 0), 270);

        companion object {
            fun fromDegrees(degrees: Int): Direction {
                var deg = degrees
                if (deg < 0) {
                    deg += 360
                }
                deg %= 360

                return when (deg) {
                    0 -> NORTH
                    90 -> EAST
                    180 -> SOUTH
                    270 -> WEST
                    else -> error("")
                }
            }
        }
    }
}
