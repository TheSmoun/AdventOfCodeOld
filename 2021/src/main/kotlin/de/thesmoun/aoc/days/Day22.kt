package de.thesmoun.aoc.days

class Day22 : Day<Collection<Day22.Instruction>, Long>("Day 22: Reactor Reboot") {

    private val regex = Regex("^(on|off) x=([-]?\\d+)\\.\\.([-]?\\d+),y=([-]?\\d+)\\.\\.([-]?\\d+),z=([-]?\\d+)\\.\\.([-]?\\d+)$")

    override fun parseInput(input: Collection<String>) = input.map {
        val (flag, x1, x2, y1, y2, z1, z2) = regex.find(it)!!.destructured
        Instruction(flag == "on", x1.toInt()..x2.toInt(), y1.toInt()..y2.toInt(), z1.toInt()..z2.toInt())
    }

    override fun runPart1(input: Collection<Instruction>): Long {
        val points = (0..100).map { (0..100).map { (0..100).map { false }.toBooleanArray() } }
        val range = -50..50
        input.forEach {
            it.x.coerce(range).forEach { x ->
                val px = points[x + 50]
                it.y.coerce(range).forEach { y ->
                    val py = px[y + 50]
                    it.z.coerce(range).forEach { z ->
                        py[z + 50] = it.flag
                    }
                }
            }
        }

        return points.sumOf { p -> p.sumOf { it.count { f -> f } } }.toLong()
    }

    override fun runPart2(input: Collection<Instruction>): Long {
        TODO("Not yet implemented")
    }

    class Instruction(val flag: Boolean, val x: IntRange, val y: IntRange, val z: IntRange) {
        operator fun contains(point: Triple<Int, Int, Int>): Boolean {
            val (px, py, pz) = point
            return px in x && py in y && pz in z
        }
    }

    private fun IntRange.coerce(range: IntRange) = when {
        range.first > last || range.last < first -> IntRange.EMPTY
        else -> kotlin.math.max(range.first, first)..kotlin.math.min(range.last, last)
    }
}
