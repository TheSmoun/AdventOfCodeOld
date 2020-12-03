package de.thesmoun.aoc.days

class Day3 : Day<Day3.Map, Long>("Day 3: Toboggan Trajectory") {

    override fun parseInput(input: Collection<String>): Map {
        val set = HashSet<Point>()
        for ((y, line) in input.withIndex()) {
            for ((x, c) in line.withIndex()) {
                if (c == '#') {
                    set.add(Point(x, y))
                }
            }
        }
        return Map(input.first().length, input.size, set)
    }

    override fun runPart1(input: Map) = getTreesForSlope(input, 3, 1)

    override fun runPart2(input: Map): Long {
        return getTreesForSlope(input, 1, 1) *
                getTreesForSlope(input, 3, 1) *
                getTreesForSlope(input, 5, 1) *
                getTreesForSlope(input, 7, 1) *
                getTreesForSlope(input, 1, 2)
    }

    private fun getTreesForSlope(map: Map, right: Int, down: Int): Long {
        var count = 0L
        var x = 0
        for (y in 0 until map.h step down) {
            if (map.trees.contains(Point(x, y))) {
                count++
            }
            x = (x + right) % map.w
        }
        return count
    }

    data class Map(val w: Int, val h: Int, val trees: Set<Point>)
    data class Point(val x: Int, val y: Int)
}
