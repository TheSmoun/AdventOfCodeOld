package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day20 : Day<Collection<Day20.Tile>, Long>("Day 20: Jurassic Jigsaw") {

    override fun parseInput(input: Collection<String>) = input.splitAt("").map { Tile.parse(it) }

    override fun runPart1(input: Collection<Tile>) =
            input.filter { it.isCornerTile(input) }.fold(1L) { acc, tile -> acc * tile.id }

    override fun runPart2(input: Collection<Tile>): Long {
        TODO("Not yet implemented")
    }

    data class Pos(val x: Int, val y: Int)

    data class Border(val pixels: Set<Int>) {
        fun reversed() = Border(pixels.map { Tile.TILE_SIZE - it - 1 }.toSet())

        fun matches(border: Border) = pixels == border.pixels || pixels == border.reversed().pixels

        fun matches(tile: Tile) = tile.getBorders().any { matches(it) }
    }

    data class Tile(val id: Int, val pixels: Set<Pos>) {
        companion object {
            const val TILE_SIZE = 10

            fun parse(lines: Collection<String>): Tile {
                val (id) = Regex("^Tile (\\d+):$").find(lines.first())!!.destructured
                val data = lines.drop(1)

                val pixels = mutableSetOf<Pos>()
                for ((y, line) in data.withIndex()) {
                    for ((x, char) in line.withIndex()) {
                        if (char == '#')
                            pixels.add(Pos(x, y))
                    }
                }

                return Tile(id.toInt(), pixels)
            }
        }

        fun getBorders() = sequence {
            yield(Border(pixels.filter { it.y == 0 }.map { it.x }.toSet()))
            yield(Border(pixels.filter { it.x == TILE_SIZE - 1 }.map { it.y }.toSet()))
            yield(Border(pixels.filter { it.y == TILE_SIZE - 1 }.map { it.x }.toSet()))
            yield(Border(pixels.filter { it.x == 0 }.map { it.y }.toSet()))
        }

        fun isCornerTile(tiles: Collection<Tile>): Boolean {
            val possibleTiles = tiles.filter { id != it.id }
            return getBorders().count { possibleTiles.any { t -> it.matches(t) } } == 2
        }
    }
}
