package de.thesmoun.aoc.days

class Day24 : Day<List<List<Day24.Direction>>, Int>("Day 24: Lobby Layout") {

    override fun parseInput(input: Collection<String>) = input.map {
        Regex("(se|sw|nw|ne|e|w)").findAll(it).map { d -> Direction.valueOf(d.value.toUpperCase()) }.toList()
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
        E(Pos(2, 0)),
        SE(Pos(1, 1)),
        SW(Pos(-1, 1)),
        W(Pos(-2, 0)),
        NW(Pos(-1, -1)),
        NE(Pos(1, -1));
    }

    class Tile(val pos: Pos, private val allTiles: MutableMap<Pos, Tile>) {

        companion object {
            fun getOrCreateTile(pos: Pos, direction: Direction, allTiles: MutableMap<Pos, Tile>): Tile {
                val targetPos = pos + direction.delta
                return allTiles.getOrPut(targetPos) { Tile(targetPos, allTiles) }
            }
        }

        var isBlack = false

        private val eastTile: Tile by lazy { getOrCreateTile(pos, Direction.E, allTiles) }
        private val southEastTile: Tile by lazy { getOrCreateTile(pos, Direction.SE, allTiles) }
        private val southWestTile: Tile by lazy { getOrCreateTile(pos, Direction.SW, allTiles) }
        private val westTile: Tile by lazy { getOrCreateTile(pos, Direction.W, allTiles) }
        private val northWestTile: Tile by lazy { getOrCreateTile(pos, Direction.NW, allTiles) }
        private val northEastTile: Tile by lazy { getOrCreateTile(pos, Direction.NE, allTiles) }

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
            Direction.E -> eastTile
            Direction.SE -> southEastTile
            Direction.SW -> southWestTile
            Direction.W -> westTile
            Direction.NW -> northWestTile
            Direction.NE -> northEastTile
        }
    }
}
