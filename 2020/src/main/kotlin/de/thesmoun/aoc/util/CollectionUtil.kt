package de.thesmoun.aoc.util

import java.util.*

fun <T> Collection<T>.splitAt(item: T): Collection<Collection<T>> {
    val result = LinkedList<Collection<T>>()
    var current = LinkedList<T>()
    result.add(current)

    forEach {
        if (it == item) {
            current = LinkedList()
            result.add(current)
        } else {
            current.add(it)
        }
    }

    if (current.isEmpty()) {
        result.removeLast()
    }

    return result
}
