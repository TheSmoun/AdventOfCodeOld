package de.thesmoun.aoc.util

fun String.splitAt(index: Int): Pair<String, String> {
    if (index < 0) return "" to this
    if (index > lastIndex) return this to ""
    return substring(0, index) to substring(index)
}
