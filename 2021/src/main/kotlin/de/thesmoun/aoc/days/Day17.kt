package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.cartesianProduct
import kotlin.math.max

class Day17 : Day<Pair<IntRange, IntRange>, Int>("Day 17: Trick Shot") {

    private val pattern = Regex("^target area: x=(\\d+)\\.\\.(\\d+), y=(-\\d+)\\.\\.(-\\d+)$")

    override fun parseInput(input: Collection<String>) = pattern.find(input.first())!!.destructured.let { (x0, x1, y0, y1) ->
        (x0.toInt()..x1.toInt()) to (y0.toInt()..y1.toInt())
    }

    override fun runPart1(input: Pair<IntRange, IntRange>): Int {
        val (targetX, targetY) = input
        val xVelocities = 1..targetX.last
        val yVelocities = targetY.first..(-targetY.first)
        val startingVectors = xVelocities.cartesianProduct(yVelocities)
        return startingVectors.map { throwProbe(it, input) }.filter { it.first }.maxOf { it.second }
    }

    override fun runPart2(input: Pair<IntRange, IntRange>): Int {
        val (targetX, targetY) = input
        val xVelocities = 1..targetX.last
        val yVelocities = targetY.first..(-targetY.first)
        val startingVectors = xVelocities.cartesianProduct(yVelocities)
        return startingVectors.count { throwProbe(it, input).first }
    }

    private fun throwProbe(startingVelocity: Pair<Int, Int>, target: Pair<IntRange, IntRange>): Pair<Boolean, Int> {
        var (xVel, yVel) = startingVelocity
        val (targetX, targetY) = target
        var x = 0
        var y = 0
        var maxY = 0

        while (x <= targetX.last && y >= targetY.first) {
            if (x >= targetX.first && y <= targetY.last) {
                return true to maxY
            }

            x += xVel
            y += yVel
            maxY = max(y, maxY)

            xVel = max(0, xVel - 1)
            yVel -= 1
        }

        return false to maxY
    }
}
