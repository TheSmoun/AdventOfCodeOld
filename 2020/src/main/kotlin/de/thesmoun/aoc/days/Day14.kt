package de.thesmoun.aoc.days

import unsigned.java_1_7.toBinaryString
import kotlin.math.pow

class Day14 : Day<Collection<Day14.Action>, Long>("Day 14: Docking Data") {

    override fun parseInput(input: Collection<String>) = input.map {
        if (it.startsWith("mask = ")) {
            MaskAction(it.replace("mask = ", ""))
        } else {
            val (address, value) = Regex("mem\\[(\\d+)] = (\\d+)").find(it)!!.destructured
            StoreAction(address.toLong(), value.toLong())
        }
    }

    override fun runPart1(input: Collection<Action>) = runPart(input, ComputerV1()).memory.values.sum()

    override fun runPart2(input: Collection<Action>) = runPart(input, ComputerV2()).memory.values.sum()

    private fun runPart(input: Collection<Action>, computer: Computer) = input.fold(computer) { acc, action ->
        action.execute(acc)
        acc
    }

    abstract class Computer(val memory: MutableMap<Long, Long> = mutableMapOf()) {
        abstract fun setMask(mask: String)
        abstract fun storeValue(address: Long, value: Long)
    }

    class ComputerV1(private var mask: MaskV1 = MaskV1(0, 0)) : Computer() {
        override fun setMask(mask: String) {
            this.mask = MaskV1.parse(mask)
        }

        override fun storeValue(address: Long, value: Long) {
            memory[address] = mask.apply(value)
        }
    }

    class ComputerV2(private var mask: MaskV2 = MaskV2(0, listOf())) : Computer() {
        override fun setMask(mask: String) {
            this.mask = MaskV2.parse(mask)
        }

        override fun storeValue(address: Long, value: Long) {
            mask.apply(address).forEach { memory[it] = value }
        }
    }

    class MaskV1(private val set: Long, private val clear: Long) {
        companion object {
            fun parse(mask: String): MaskV1 {
                val setIndices = mask.withIndex().filter { it.value == '1' }.map { 35 - it.index }
                val clearIndices = mask.withIndex().filter { it.value == '0' }.map { 35 - it.index }

                val set = setIndices.fold(0L) { acc, i -> acc or (1L shl i) }

                var clear = (0..35).fold(0L) { acc, i -> acc or (1L shl i) }
                clear = clearIndices.fold(clear) { acc, i -> acc and (1L shl i).inv() }

                return MaskV1(set, clear)
            }
        }

        fun apply(value: Long) = value or set and clear
    }

    class MaskV2(private val set: Long, private val masks: Collection<MaskV1>) {
        companion object {
            fun parse(mask: String): MaskV2 {
                val setIndices = mask.withIndex().filter { it.value == '1' }.map { 35 - it.index }
                val set = setIndices.fold(0L) { acc, i -> acc or (1L shl i) }

                val floatingIndices = mask.withIndex().filter { it.value == 'X' }.map { 35 - it.index }
                val masks = mutableListOf<String>()

                for (i in 0 until (2.0.pow(floatingIndices.size).toLong())) {
                    val binary = i.toBinaryString().padStart(floatingIndices.size, '0')
                    val m = Array(36) { ' ' }
                    var b = 0

                    for ((j, c) in mask.withIndex()) {
                        when (c) {
                            '0' -> m[j] = 'X'
                            '1' -> m[j] = '1'
                            'X' -> m[j] = binary[b++]
                        }
                    }

                    masks.add(m.joinToString(""))
                }

                return MaskV2(set, masks.map { MaskV1.parse(it) })
            }
        }

        fun apply(value: Long): Collection<Long> {
            val baseAddress = value or set
            return masks.map { it.apply(baseAddress) }
        }
    }

    abstract class Action {
        abstract fun execute(computer: Computer)
    }

    class MaskAction(private val mask: String) : Action() {
        override fun execute(computer: Computer) {
            computer.setMask(mask)
        }
    }

    class StoreAction(private val address: Long, private val value: Long) : Action() {
        override fun execute(computer: Computer) {
            computer.storeValue(address, value)
        }
    }
}
