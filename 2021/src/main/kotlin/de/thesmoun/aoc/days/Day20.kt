package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day20 : Day<Day20.Input, Int>("Day 20: Trench Map") {

    override fun parseInput(input: Collection<String>): Input {
        val parts = input.splitAt("")
        val algorithm = parts[0].first()

        val lines = parts[1]
        val image = mutableSetOf<Pair<Int, Int>>()
        lines.forEachIndexed { j, s ->
            s.forEachIndexed { i, c ->
                if (c == '#')
                    image += i to j
            }
        }

        return Input(algorithm, image, lines.size)
    }

    override fun runPart1(input: Input) = enlargeImage(input, 2)

    override fun runPart2(input: Input) = enlargeImage(input, 50)

    private fun enlargeImage(input: Input, times: Int): Int {
        var min = 0
        var max = input.size - 1

        var edge = false
        var image = input.image.toSet()
        repeat(times) {
            val newImage = mutableSetOf<Pair<Int, Int>>()
            val range = (min - 1)..(max + 1)
            range.forEach { j ->
                range.forEach { i ->
                    val index = neighborhood(i, j).map { pair ->
                        val (x, y) = pair
                        when {
                            (pair in image) -> 1
                            ((x !in (min..max) || y !in (min..max)) && edge) -> 1
                            else -> 0
                        }
                    }.fold(0) { acc, idx -> acc * 2 + idx }

                    if (input.algorithm[index] == '#')
                        newImage += i to j
                }
            }

            min--
            max++
            image = newImage

            edge = when {
                input.algorithm.first() == '.' -> false
                input.algorithm.last() == '.' -> !edge
                input.algorithm.last() == '#' -> false
                else -> error("should not happen")
            }
        }

        return image.size
    }

    private fun neighborhood(i: Int, j: Int) = ((j - 1)..(j + 1)).flatMap { y ->
        ((i - 1)..(i + 1)).map { x -> x to y }
    }

    class Input(val algorithm: String, val image: Set<Pair<Int, Int>>, val size: Int)
}
