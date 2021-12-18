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

fun <T, K> Grouping<T, K>.eachLongCount(): Map<K, Long> =
    eachCount().entries.associate { it.key to it.value.toLong() }

fun <T, S> Iterable<T>.cartesianProduct(other: Iterable<S>): List<Pair<T, S>> {
    return cartesianProduct(other) { first, second -> first to second }
}

fun <T, S, V> Iterable<T>.cartesianProduct(other: Iterable<S>, transformer: (first: T, second: S) -> V): List<V> {
    return flatMap { first -> other.map { second -> transformer(first, second) } }
}
