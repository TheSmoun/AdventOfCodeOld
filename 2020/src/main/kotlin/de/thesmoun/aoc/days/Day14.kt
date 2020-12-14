package de.thesmoun.aoc.days

class Day14 : Day<Collection<Day14.Action>, Long>("Day 14: Docking Data") {

    override fun parseInput(input: Collection<String>) = input.map {
        if (it.startsWith("mask = ")) {
            MaskAction(Mask.parse(it.replace("mask = ", "")))
        } else {
            val (address, value) = Regex("mem\\[(\\d+)] = (\\d+)").find(it)!!.destructured
            StoreAction(address.toInt(), value.toLong())
        }
    }

    override fun runPart1(input: Collection<Action>) = input.fold(Computer()) { computer, action ->
        action.execute(computer)
        computer
    }.memory.values.sum()

    override fun runPart2(input: Collection<Action>): Long {
        TODO("Not yet implemented")
    }

    class Computer(val memory: MutableMap<Int, Long> = mutableMapOf(), var mask: Mask = Mask(0, 0)) {
        fun storeValue(address: Int, value: Long) {
            memory[address] = mask.apply(value)
        }
    }

    class Mask(private val set: Long, private val clear: Long) {
        companion object {
            fun parse(mask: String): Mask {
                val setIndices = mask.withIndex().filter { it.value == '1' }.map { 35 - it.index }
                val clearIndices = mask.withIndex().filter { it.value == '0' }.map { 35 - it.index }

                val set = setIndices.fold(0L) { acc, i -> acc or (1L shl i) }

                var clear = (0..35).fold(0L) { acc, i -> acc or (1L shl i) }
                clear = clearIndices.fold(clear) { acc, i -> acc and (1L shl i).inv() }

                return Mask(set, clear)
            }
        }

        fun apply(value: Long) = value or set and clear
    }

    abstract class Action {
        abstract fun execute(computer: Computer)
    }

    class MaskAction(private val mask: Mask) : Action() {
        override fun execute(computer: Computer) {
            computer.mask = mask
        }
    }

    class StoreAction(private val address: Int, private val value: Long) : Action() {
        override fun execute(computer: Computer) {
            computer.storeValue(address, value)
        }
    }
}
