package de.thesmoun.aoc.days

class Day17 : Day<Set<Day17.Pos3>, Int>("Day 17: Conway Cubes") {

    override fun parseInput(input: Collection<String>): Set<Pos3> {
        val set = mutableSetOf<Pos3>()
        for ((y, line) in input.withIndex()) {
            for ((x, char) in line.withIndex()) {
                if (char == '#')
                    set.add(Pos3(x, y, 0))
            }
        }

        return set
    }

    override fun runPart1(input: Set<Pos3>) = runPart(input)

    override fun runPart2(input: Set<Pos3>) = runPart(input.map { Pos4(it) }.toSet())

    private fun <TPos : Pos<TPos>> runPart(input: Set<TPos>): Int {
        var current = input

        for (i in 0..5) {
            val next = mutableSetOf<TPos>()
            val positions = current.flatMap { it.getNeighbors(true) }.toSet()
            for (pos in positions) {
                val activeNeighbors = pos.getNeighbors(false).count { it in current }
                if (pos in current && activeNeighbors in 2..3) {
                    next.add(pos)
                } else if (pos !in current && activeNeighbors == 3) {
                    next.add(pos)
                }
            }

            current = next
        }

        return current.size
    }

    interface Pos<TPos> where TPos : Pos<TPos> {
        fun getNeighbors(self: Boolean): Sequence<TPos>
    }

    data class Pos3(val x: Int, val y: Int, val z: Int) : Pos<Pos3> {
        override fun getNeighbors(self: Boolean) = sequence {
            for (dz in -1..1) {
                for (dy in -1..1) {
                    for (dx in -1..1) {
                        if (self || dx != 0 || dy != 0 || dz != 0) {
                            yield(Pos3(x + dx, y + dy, z + dz))
                        }
                    }
                }
            }
        }
    }

    data class Pos4(val x: Int, val y: Int, val z: Int, val w: Int) : Pos<Pos4> {
        constructor(pos: Pos3) : this(pos.x, pos.y, pos.z, 0)

        override fun getNeighbors(self: Boolean) = sequence {
            for (dw in -1..1) {
                for (dz in -1..1) {
                    for (dy in -1..1) {
                        for (dx in -1..1) {
                            if (self || dx != 0 || dy != 0 || dz != 0 || dw != 0) {
                                yield(Pos4(x + dx, y + dy, z + dz, w + dw))
                            }
                        }
                    }
                }
            }
        }
    }
}
