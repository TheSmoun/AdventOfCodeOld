package de.thesmoun.aoc.days

class Day5 : Day<Collection<Day5.Line>, Int>("Day 5: Hydrothermal Venture") {

    override fun parseInput(input: Collection<String>) = input.map { Line.parse(it) }

    override fun runPart1(input: Collection<Line>) = input.flatMap { it.getHorizontalAndVerticalPoints() }
        .groupingBy { it }.eachCount().values.count { it >= 2 }

    override fun runPart2(input: Collection<Line>) = input.flatMap { it.getAllPoints() }
        .groupingBy { it }.eachCount().values.count { it >= 2 }

    data class Line(val start: Point, val end: Point) {
        companion object {
            private val regex = Regex("^(\\d+),(\\d+) -> (\\d+),(\\d+)$")

            fun parse(input: String): Line {
                val (x1, y1, x2, y2) = regex.find(input)!!.destructured
                return Line(Point(x1.toInt(), y1.toInt()), Point(x2.toInt(), y2.toInt()))
            }
        }

        fun getAllPoints(): Collection<Point> {
            val horAndVertPoints = getHorizontalAndVerticalPoints()
            if (horAndVertPoints.isNotEmpty())
                return horAndVertPoints

            return getDiagonalPoints()
        }

        fun getHorizontalAndVerticalPoints(): Collection<Point> {
            if (start.x == end.x)
                return getRange(start.y, end.y).map { Point(start.x, it) }

            if (start.y == end.y)
                return getRange(start.x, end.x).map { Point(it, start.y) }

            return emptyList()
        }

        private fun getRange(start: Int, end: Int): Collection<Int> {
            if (start == end)
                return listOf(start)
            if (start < end)
                return (start..end).toList()
            return (start downTo end).toList()
        }

        private fun getDiagonalPoints(): Collection<Point> {
            val diff = end.y - start.y
            if (diff > 0)
                return (0..diff).map { Point(if (start.x < end.x) (start.x + it) else (start.x - it), start.y + it) }
            return (diff..0).map { Point(if (start.x < end.x) (start.x - it) else (start.x + it), start.y + it) }
        }
    }

    data class Point(val x: Int, val y: Int)
}
