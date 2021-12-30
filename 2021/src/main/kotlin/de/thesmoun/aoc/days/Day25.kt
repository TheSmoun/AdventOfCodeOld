package de.thesmoun.aoc.days

class Day25 : Day<Day25.Input, Int>("Day 25: Sea Cucumber") {

    override fun parseInput(input: Collection<String>): Input {
        val height = input.size
        val width = input.first().length
        val cucumbers = mutableMapOf<Pair<Int, Int>, Boolean>()

        input.forEachIndexed { y, s ->
            s.forEachIndexed { x, c ->
                if (c == '>' || c == 'v') {
                    cucumbers[x to y] = c == '>'
                }
            }
        }

        return Input(width, height, cucumbers)
    }

    override fun runPart1(input: Input): Int {
        var steps = 0
        var moved = true

        while (moved) {
            moved = input.stepHorizontal()
            moved = input.stepVertical() || moved
            steps++
        }

        return steps
    }

    override fun runPart2(input: Input) = 0

    class Input(private val width: Int, private val height: Int, private var cucumbers: Map<Pair<Int, Int>, Boolean>) {

        fun stepHorizontal() = step(1 to 0, true)

        fun stepVertical() = step(0 to 1, false)

        private fun step(delta: Pair<Int, Int>, dir: Boolean): Boolean {
            val (dx, dy) = delta
            var moved = false
            cucumbers = cucumbers.entries.associate {
                val newLoc = Pair((it.key.first + dx).mod(width), (it.key.second + dy).mod(height))
                if (it.value == dir && newLoc !in cucumbers) {
                    moved = true
                    newLoc to it.value
                } else {
                    it.key to it.value
                }
            }

            return moved
        }
    }
}
