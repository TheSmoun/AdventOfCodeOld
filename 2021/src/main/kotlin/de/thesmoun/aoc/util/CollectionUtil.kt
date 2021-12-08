package de.thesmoun.aoc.util

import java.util.*

fun <T> Collection<T>.splitAt(item: T): List<Collection<T>> {
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

fun <T> Collection<T>.permutations(withSelf: Boolean = false): Collection<Pair<T, T>> {
    val result = LinkedList<Pair<T, T>>()

    forEachIndexed { i, ti ->
        forEachIndexed { j, tj ->
            if (withSelf || i != j) {
                result.add(ti to tj)
            }
        }
    }

    return result
}

fun <T> MutableCollection<T>.removeSingle(predicate: (T) -> Boolean): T {
    val value = single(predicate)
    remove(value)
    return value
}
