package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day24
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertTrue

class Day24Tests {

    private val lines = """
        sesenwnenenewseeswwswswwnenewsewsw
        neeenesenwnwwswnenewnwwsewnenwseswesw
        seswneswswsenwwnwse
        nwnwneseeswswnenewneswwnewseswneseene
        swweswneswnenwsewnwneneseenw
        eesenwseswswnenwswnwnwsewwnwsene
        sewnenenenesenwsewnenwwwse
        wenwwweseeeweswwwnwwe
        wsweesenenewnwwnwsenewsenwwsesesenwne
        neeswseenwwswnwswswnw
        nenwswwsewswnenenewsenwsenwnesesenew
        enewnwewneswsewnwswenweswnenwsenwsw
        sweneswneswneneenwnewenewwneswswnese
        swwesenesewenwneswnwwneseswwne
        enesenwswwswneneswsenwnewswseenwsese
        wnwnesenesenenwwnenwsewesewsesesew
        nenewswnwewswnenesenwnesewesw
        eneswnwswnwsenenwnwnwwseeswneewsenese
        neswnwewnwnwseenwseesewsenwsweewe
        wseweeenwnesenwwwswnew
    """.trimIndent().lines()

    @Test
    fun parseInput_shouldReturnDirections() {
        val input = Day24().parseInput(lines)
        assertEquals(20, input.size)
    }

    @Test
    fun nwwswee_shouldFlipReferenceTile() {
        val allTiles = mutableMapOf<Day24.Pos, Day24.Tile>()
        val referenceTile = Day24.Tile(Day24.Pos(0, 0), allTiles)
        allTiles[referenceTile.pos] = referenceTile
        val instructions = listOf(
                Day24.Direction.NORTH_WEST,
                Day24.Direction.WEST,
                Day24.Direction.SOUTH_WEST,
                Day24.Direction.EAST,
                Day24.Direction.EAST
        )
        instructions.fold(referenceTile) { tile, direction -> tile.getTile(direction) }.flipColor()
        assertTrue { referenceTile.isBlack }
    }

    @Test
    fun runPart1_shouldReturnAmountOfBlackTiles() {
        val result = Day24().testPart1(lines)
        assertEquals(10, result)
    }
}
