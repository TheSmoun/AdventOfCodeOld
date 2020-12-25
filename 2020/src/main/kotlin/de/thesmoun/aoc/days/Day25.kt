package de.thesmoun.aoc.days

class Day25 : Day<Pair<Long, Long>, Any>("Day 25: Combo Breaker") {

    companion object {
        const val SUBJECT_NUMBER = 7
        const val DIVISOR = 20201227
    }

    override fun parseInput(input: Collection<String>) = input.first().toLong() to input.last().toLong()

    override fun runPart1(input: Pair<Long, Long>): Any {
        val (cardPublicKey, doorPublicKey) = input
        val doorLoopSize = getLoopSize(doorPublicKey)
        return getEncryptionKey(doorLoopSize, cardPublicKey)
    }

    override fun runPart2(input: Pair<Long, Long>) = 0L

    private fun getLoopSize(publicKey: Long): Int {
        var loopSize = 0
        var key = 1L
        while (key != publicKey) {
            key = (key * SUBJECT_NUMBER) % DIVISOR
            loopSize++
        }
        return loopSize
    }

    private fun getEncryptionKey(loopSize: Int, publicKey: Long): Long {
        var key = 1L
        repeat(loopSize) {
            key = (key * publicKey) % DIVISOR
        }
        return key
    }
}
