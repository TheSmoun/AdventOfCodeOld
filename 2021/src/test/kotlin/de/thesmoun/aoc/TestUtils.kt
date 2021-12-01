package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day

fun <TInput, TResult> Day<TInput, TResult>.testPart1(lines: Collection<String>) = testPart(lines, ::runPart1)

fun <TInput, TResult> Day<TInput, TResult>.testPart2(lines: Collection<String>) = testPart(lines, ::runPart2)

private fun <TInput, TResult> Day<TInput, TResult>.testPart(lines: Collection<String>, runPart: (input: TInput) -> TResult): TResult {
    val input = parseInput(lines)
    return runPart(input)
}
