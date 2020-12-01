package de.thesmoun.aoc

import de.thesmoun.aoc.days.AbstractDay
import org.reflections.Reflections
import java.lang.reflect.Modifier

fun main() {
    val reflections = Reflections("de.thesmoun.aoc.days")
    val dayClasses = reflections.getSubTypesOf(AbstractDay::class.java)
            .filter { !Modifier.isAbstract(it.modifiers) }
            .sortedBy { it.simpleName.substring(3).toInt() }

    dayClasses.last().getConstructor().newInstance().run()
}
