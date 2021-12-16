package de.thesmoun.aoc.days

import java.math.BigInteger

class Day16 : Day<Day16.Packet, Long>("Day 16: Packet Decoder") {

    override fun parseInput(input: Collection<String>): Packet {
        val hex = input.first()
        val binary = String.format("%${hex.length * 4}s", BigInteger(hex, 16).toString(2)).replace(' ', '0')
        val (packet, _) = Packet.parse(binary)
        return packet
    }

    override fun runPart1(input: Packet) = input.withAllSubPackets().sumOf { it.version }.toLong()

    override fun runPart2(input: Packet) = input.evaluate()

    abstract class Packet(val version: Int) {
        companion object {
            fun parse(input: String): Pair<Packet, String> {
                var binary = input

                val version = binary.take(3).toInt(2)
                binary = binary.drop(3)

                val typeId = binary.take(3).toInt(2)
                binary = binary.drop(3)

                return when (typeId) {
                    4 -> Value.parse(version, binary)
                    else -> Operator.parse(version, typeId, binary)
                }
            }
        }

        abstract fun withAllSubPackets(): Collection<Packet>
        abstract fun evaluate(): Long
    }

    class Value(version: Int, private val value: Long) : Packet(version) {
        companion object {
            fun parse(version: Int, input: String): Pair<Value, String> {
                var binary = input
                val allData = mutableListOf<Long>()

                do {
                    val groupHeader = binary.take(1).toInt(2)
                    binary = binary.drop(1)

                    val data = binary.take(4).toLong(2)
                    binary = binary.drop(4)

                    allData.add(data)
                } while (groupHeader == 1)

                var value = 0L
                allData.forEach {
                    value = (value shl 4) or it
                }

                return Value(version, value) to binary
            }
        }

        override fun withAllSubPackets() = listOf(this)

        override fun evaluate() = value
    }

    abstract class Operator(version: Int, protected val packets: Collection<Packet>) : Packet(version) {
        companion object {
            fun parse(version: Int, typeId: Int, input: String): Pair<Operator, String> {
                var binary = input

                val bit = binary.take(1).toInt(2)
                binary = binary.drop(1)

                val packets = mutableListOf<Packet>()
                if (bit == 0) {
                    val lengthOfSubPackets = binary.take(15).toInt(2)
                    binary = binary.drop(15)

                    var subPackets = binary.take(lengthOfSubPackets)
                    binary = binary.drop(lengthOfSubPackets)

                    do {
                        val (packet, b) = parse(subPackets)
                        subPackets = b
                        packets += packet
                    } while (subPackets.any { it == '1' })
                } else {
                    val numberOfSubPackets = binary.take(11).toInt(2)
                    binary = binary.drop(11)

                    repeat(numberOfSubPackets) {
                        val (packet, b) = parse(binary)
                        binary = b
                        packets += packet
                    }
                }

                val packet = when (typeId) {
                    0 -> Sum(version, packets)
                    1 -> Product(version, packets)
                    2 -> Minimum(version, packets)
                    3 -> Maximum(version, packets)
                    5 -> GreaterThan(version, packets)
                    6 -> LessThan(version, packets)
                    7 -> EqualTo(version, packets)
                    else -> throw IllegalArgumentException("invalid packet $typeId")
                }

                return packet to binary
            }
        }

        override fun withAllSubPackets(): Collection<Packet> {
            val allPackets = mutableListOf<Packet>(this)
            allPackets.addAll(packets.flatMap { it.withAllSubPackets() })
            return allPackets
        }
    }

    class Sum(version: Int, packets: Collection<Packet>) : Operator(version, packets) {
        override fun evaluate() = packets.sumOf { it.evaluate() }
    }

    class Product(version: Int, packets: Collection<Packet>) : Operator(version, packets) {
        override fun evaluate() = packets.fold(1L) { acc, packet -> acc * packet.evaluate() }
    }

    class Minimum(version: Int, packets: Collection<Packet>) : Operator(version, packets) {
        override fun evaluate() = packets.minOf { it.evaluate() }
    }

    class Maximum(version: Int, packets: Collection<Packet>) : Operator(version, packets) {
        override fun evaluate() = packets.maxOf { it.evaluate() }
    }

    class GreaterThan(version: Int, packets: Collection<Packet>) : Operator(version, packets) {
        override fun evaluate() = if (packets.first().evaluate() > packets.last().evaluate()) 1L else 0L
    }

    class LessThan(version: Int, packets: Collection<Packet>) : Operator(version, packets) {
        override fun evaluate() = if (packets.first().evaluate() < packets.last().evaluate()) 1L else 0L
    }

    class EqualTo(version: Int, packets: Collection<Packet>) : Operator(version, packets) {
        override fun evaluate() = if (packets.first().evaluate() == packets.last().evaluate()) 1L else 0L
    }
}
