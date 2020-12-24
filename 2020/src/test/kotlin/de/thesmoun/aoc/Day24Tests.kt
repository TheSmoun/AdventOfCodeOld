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
                Day24.Direction.NW,
                Day24.Direction.W,
                Day24.Direction.SW,
                Day24.Direction.E,
                Day24.Direction.E
        )
        instructions.fold(referenceTile) { tile, direction -> tile.getTile(direction) }.flip()
        assertTrue { referenceTile.isBlack }
    }

    @Test
    fun runPart1_shouldReturnAmountOfBlackTiles() {
        val result = Day24().testPart1(lines)
        assertEquals(10, result)
    }

    @Test
    fun runPart2_shouldReturnBlackTilesAfter100Days() {
        val result = Day24().testPart2(lines)
        assertEquals(2208, result)
    }
}
