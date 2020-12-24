package de.thesmoun.aoc.days

class Day24 : Day<List<List<Day24.Direction>>, Int>("Day 24: Lobby Layout") {

    override fun parseInput(input: Collection<String>) = input.map {
        var last = ""
        val directions = mutableListOf<Direction>()
        for (char in it) {
            when (char) {
                'e' -> {
                    directions.add(when (last) {
                        "s" -> Direction.SOUTH_EAST
                        "n" -> Direction.NORTH_EAST
                        else -> Direction.EAST
                    })
                    last = ""
                }
                'w' -> {
                    directions.add(when (last) {
                        "s" -> Direction.SOUTH_WEST
                        "n" -> Direction.NORTH_WEST
                        else -> Direction.WEST
                    })
                    last = ""
                }
                's' -> last = "s"
                'n' -> last = "n"
                else -> error("")
            }
        }
        directions.toList()
    }

    override fun runPart1(input: List<List<Direction>>) = runPart(input).count { it.isBlack }

    override fun runPart2(input: List<List<Direction>>): Int {
        val allTiles = runPart(input)
        allTiles.filter { it.isBlack }.forEach { it.neighbors().toList() }

        repeat(100) {
            val tilesToFlip = mutableListOf<Tile>()
            for (tile in allTiles.toList()) {
                val blackNeighbors = tile.neighbors().count { it.isBlack }
                if (tile.isBlack) {
                    if (blackNeighbors == 0 || blackNeighbors > 2) {
                        tilesToFlip.add(tile)
                    }
                } else {
                    if (blackNeighbors == 2) {
                        tilesToFlip.add(tile)
                    }
                }
            }

            tilesToFlip.forEach { it.flip() }
        }

        return allTiles.count { it.isBlack }
    }

    private fun runPart(input: List<List<Direction>>): Collection<Tile> {
        val allTiles = mutableMapOf<Pos, Tile>()
        val referenceTile = Tile(Pos(0, 0), allTiles)
        allTiles[referenceTile.pos] = referenceTile
        for (directions in input) {
            directions.fold(referenceTile) { tile, direction -> tile.getTile(direction) }.flip()
        }
        return allTiles.values
    }

    data class Pos(val x: Int, val y: Int) {
        operator fun plus(pos: Pos) = Pos(x + pos.x, y + pos.y)
    }

    enum class Direction(val delta: Pos) {
        EAST(Pos(2, 0)),
        SOUTH_EAST(Pos(1, 1)),
        SOUTH_WEST(Pos(-1, 1)),
        WEST(Pos(-2, 0)),
        NORTH_WEST(Pos(-1, -1)),
        NORTH_EAST(Pos(1, -1));
    }

    class Tile(val pos: Pos, private val allTiles: MutableMap<Pos, Tile>) {

        companion object {
            fun getOrCreateTile(pos: Pos, direction: Direction, allTiles: MutableMap<Pos, Tile>): Tile {
                val targetPos = pos + direction.delta
                return allTiles.getOrPut(targetPos) { Tile(targetPos, allTiles) }
            }
        }

        var isBlack = false

        private val eastTile: Tile by lazy { getOrCreateTile(pos, Direction.EAST, allTiles) }
        private val southEastTile: Tile by lazy { getOrCreateTile(pos, Direction.SOUTH_EAST, allTiles) }
        private val southWestTile: Tile by lazy { getOrCreateTile(pos, Direction.SOUTH_WEST, allTiles) }
        private val westTile: Tile by lazy { getOrCreateTile(pos, Direction.WEST, allTiles) }
        private val northWestTile: Tile by lazy { getOrCreateTile(pos, Direction.NORTH_WEST, allTiles) }
        private val northEastTile: Tile by lazy { getOrCreateTile(pos, Direction.NORTH_EAST, allTiles) }

        fun neighbors() = sequence {
            yield(eastTile)
            yield(southEastTile)
            yield(southWestTile)
            yield(westTile)
            yield(northWestTile)
            yield(northEastTile)
        }

        fun flip() {
            isBlack = !isBlack
        }

        fun getTile(direction: Direction) = when (direction) {
            Direction.EAST -> eastTile
            Direction.SOUTH_EAST -> southEastTile
            Direction.SOUTH_WEST -> southWestTile
            Direction.WEST -> westTile
            Direction.NORTH_WEST -> northWestTile
            Direction.NORTH_EAST -> northEastTile
        }
    }
}
