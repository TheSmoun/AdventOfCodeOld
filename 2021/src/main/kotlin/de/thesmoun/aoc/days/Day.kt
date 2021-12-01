package de.thesmoun.aoc.days

import java.io.InputStreamReader
import kotlin.system.measureNanoTime

abstract class AbstractDay(val name: String) {

    abstract fun run()
}

abstract class Day<TInput, TResult>(name: String) : AbstractDay(name) {

    override fun run() {
        val lines = readInputFile()

        println()
        println("    $name")
        println()

        runPart("Part 1", ::runPart1, lines)
        runPart("Part 2", ::runPart2, lines)
        println()
    }

    private fun runPart(name: String, part: (input: TInput) -> TResult, lines: Collection<String>) {
        val (input, ti) = measure { parseInput(lines) }
        val (result, tr) = measure { part(input) }

        println("-> $name: $result")
        println("   Input: ${ti.toTime()}, $name: ${tr.toTime()}, Total: ${(ti + tr).toTime()}")
        println()
    }

    private inline fun <T> measure(block: () -> T): Measurement<T> {
        var result: T
        val time = measureNanoTime {
            result = block()
        }
        return Measurement(result, time)
    }

    private fun readInputFile(): List<String> {
        val dayIndex = javaClass.simpleName.substring(3)
        javaClass.getResourceAsStream("/day$dayIndex.txt").use {
            return InputStreamReader(it).readLines()
        }
    }

    abstract fun parseInput(input: Collection<String>): TInput
    abstract fun runPart1(input: TInput): TResult
    abstract fun runPart2(input: TInput): TResult
}

data class Measurement<T>(val result: T, val time: Long)

fun Long.toTime(): String {
    var t = this
    if (t < 1000)
        return "$t ns"

    t /= 1000
    if (t < 1000)
        return "$t μs"

    t /= 1000
    if (t < 1000)
        return "$t ms"

    val t1 = t / 1000.0
    if (t1 < 180)
        return "${t1.format(3)} s"

    return "${(t1 / 60).format(3)} min"
}

fun Double.format(digits: Int) = "%.${digits}f".format(this)
