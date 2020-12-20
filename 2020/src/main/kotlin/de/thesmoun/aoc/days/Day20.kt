package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt
import kotlin.math.roundToInt
import kotlin.math.sqrt

class Day20 : Day<Collection<Day20.Tile>, Long>("Day 20: Jurassic Jigsaw") {

    override fun parseInput(input: Collection<String>) = input.splitAt("").map { Tile.parse(it) }

    override fun runPart1(input: Collection<Tile>) =
            input.filter { it.isCornerTile(input) }.fold(1L) { acc, tile -> acc * tile.id }

    override fun runPart2(input: Collection<Tile>): Long {
        val possibleTiles = input.toMutableList()
        val stack = mutableListOf(input.first())

        while (stack.isNotEmpty()) {
            val tile = stack.removeFirst()
            val borders = tile.getBorders().toList()
            possibleTiles.remove(tile)

            for (possibleTile in possibleTiles) {
                val possibleBorders = possibleTile.getBorders()
                for (possibleBorder in possibleBorders) {
                    var border = borders.find { it.matches(possibleBorder) }
                    if (border != null) {
                        if (tile.neighbors[border.id] == null) {
                            possibleTile.rotate((border.id + 4 - possibleBorder.oppositeId()) % 4)
                            if (border.id % 2 == 0) {
                                possibleTile.flipHorizontal()
                            } else {
                                possibleTile.flipVertical()
                            }
                            tile.neighbors[border.id] = possibleTile.id
                            possibleTile.neighbors[border.oppositeId()] = tile.id
                            stack.add(possibleTile)
                        }
                    }

                    border = borders.find { it.matches(possibleBorder.reversed()) }
                    if (border != null) {
                        if (tile.neighbors[border.id] == null) {
                            possibleTile.rotate((border.id + 4 - possibleBorder.oppositeId()) % 4)
                            tile.neighbors[border.id] = possibleTile.id
                            possibleTile.neighbors[border.oppositeId()] = tile.id
                            stack.add(possibleTile)
                        }
                    }
                }
            }
        }

        val image = Tile.from(input.associateBy { it.id })
        return image.pixels.size.toLong() - image.countAllSeaMonsters() * SeaMonster.pixels().count()
    }

    data class Pos(val x: Int, val y: Int) {
        operator fun plus(pos: Pos) = Pos(x + pos.x, y + pos.y)
    }

    data class Border(val pixels: Set<Int>, val id: Int) {
        fun reversed() = Border(pixels.map { Tile.TILE_SIZE - it - 1 }.toSet(), id)

        fun matches(border: Border) = pixels == border.pixels

        fun matches(tile: Tile) = tile.getBorders().any { matches(it) || matches(it.reversed()) }

        fun oppositeId() = when (id) {
            0 -> 2
            1 -> 3
            2 -> 0
            3 -> 1
            else -> error("")
        }
    }

    data class Neighbors(var top: Int? = null, var right: Int? = null,
                         var bottom: Int? = null, var left: Int? = null) {
        operator fun get(i: Int) = when (i) {
            0 -> top
            1 -> right
            2 -> bottom
            3 -> left
            else -> error("")
        }

        operator fun set(i: Int, id: Int) {
            when (i) {
                0 -> top = id
                1 -> right = id
                2 -> bottom = id
                3 -> left = id
            }
        }
    }

    data class Tile(val id: Int, var pixels: Set<Pos>, val size: Int, val neighbors: Neighbors = Neighbors()) {
        companion object {
            const val TILE_SIZE = 10

            fun from(tiles: Map<Int, Tile>): Tile {
                tiles.values.forEach { it.removeBorder() }
                val imageSize = sqrt(tiles.size.toDouble()).roundToInt()
                val image = Array(imageSize) { Array(imageSize) { Tile(-1, setOf(), 0) } }
                var rowStart = tiles.values.find { it.neighbors.left == null && it.neighbors.top == null }

                for (y in 0 until imageSize) {
                    var tile = rowStart
                    for (x in 0 until imageSize) {
                        image[y][x] = tile!!
                        tile = tiles[tile.neighbors.right]
                    }

                    rowStart = tiles[rowStart!!.neighbors.bottom]
                }

                for (y in 0 until imageSize) {
                    for (x in 0 until imageSize) {
                        image[y][x].translate(Pos(x * 8 - 1, y * 8 - 1))
                    }
                }

                return Tile(0, tiles.values.flatMap { it.pixels }.toSet(), imageSize * 8)
            }

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

                return Tile(id.toInt(), pixels, 10)
            }
        }

        fun getBorders() = sequence {
            yield(Border(pixels.filter { it.y == 0 }.map { it.x }.toSet(), 0))
            yield(Border(pixels.filter { it.x == size - 1 }.map { it.y }.toSet(), 1))
            yield(Border(pixels.filter { it.y == size - 1 }.map { it.x }.toSet(), 2).reversed())
            yield(Border(pixels.filter { it.x == 0 }.map { it.y }.toSet(), 3).reversed())
        }

        fun isCornerTile(tiles: Collection<Tile>): Boolean {
            val possibleTiles = tiles.filter { id != it.id }
            return getBorders().count { possibleTiles.any { t -> it.matches(t) } } == 2
        }

        fun rotate(times: Int) = repeat(times) {
            pixels = pixels.map { px -> Pos(size - px.y - 1, px.x) }.toSet()
        }

        fun flipHorizontal() {
            pixels = pixels.map { Pos(size - it.x - 1, it.y) }.toSet()
        }

        fun flipVertical() {
            pixels = pixels.map { Pos(it.x, size - it.y - 1) }.toSet()
        }

        fun removeBorder() {
            pixels = pixels.filter { it.x != 0 && it.x != size - 1 && it.y != 0 && it.y != size - 1 }.toSet()
        }

        fun translate(offset: Pos) {
            pixels = pixels.map { offset + it }.toSet()
        }

        operator fun get(x: Int, y: Int) = Pos(x, y) in pixels

        fun countAllSeaMonsters(): Int {
            repeat(4) {
                repeat(2) {
                    countSeaMonsters().let { if (it > 0) return it }
                    flipVertical()
                    countSeaMonsters().let { if (it > 0) return it }
                    flipHorizontal()
                }
                rotate(1)
            }
            return 0
        }

        private fun countSeaMonsters() = pixels.count { isSeaMonster(it) }

        private fun isSeaMonster(pos: Pos): Boolean {
            return SeaMonster.pixels().map { pos + it }.all { it in pixels }
        }
    }

    class SeaMonster {
        companion object {
            fun pixels() = sequenceOf(
                    Pos(0, 0),
                    Pos(1, 1),
                    Pos(4, 1),
                    Pos(5, 0),
                    Pos(6, 0),
                    Pos(7, 1),
                    Pos(10, 1),
                    Pos(11, 0),
                    Pos(12, 0),
                    Pos(13, 1),
                    Pos(16, 1),
                    Pos(17, 0),
                    Pos(18, 0),
                    Pos(18, -1),
                    Pos(19, 0),
            )
        }
    }
}
