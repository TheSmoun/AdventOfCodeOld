package de.thesmoun.aoc

import de.thesmoun.aoc.days.Day16
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class Day16Tests {

    @Test
    fun part1_test1() {
        val parsedInput = Day16().parseInput(listOf("D2FE28"))
        val result = Day16().runPart1(parsedInput)
        assertEquals(6, result)
    }

    @Test
    fun part1_test2() {
        val parsedInput = Day16().parseInput(listOf("38006F45291200"))
        val result = Day16().runPart1(parsedInput)
        assertEquals(9, result)
    }

    @Test
    fun part1_test3() {
        val parsedInput = Day16().parseInput(listOf("EE00D40C823060"))
        val result = Day16().runPart1(parsedInput)
        assertEquals(14, result)
    }

    @Test
    fun part1_test4() {
        val parsedInput = Day16().parseInput(listOf("8A004A801A8002F478"))
        val result = Day16().runPart1(parsedInput)
        assertEquals(16, result)
    }

    @Test
    fun part1_test5() {
        val parsedInput = Day16().parseInput(listOf("620080001611562C8802118E34"))
        val result = Day16().runPart1(parsedInput)
        assertEquals(12, result)
    }

    @Test
    fun part1_test6() {
        val parsedInput = Day16().parseInput(listOf("C0015000016115A2E0802F182340"))
        val result = Day16().runPart1(parsedInput)
        assertEquals(23, result)
    }

    @Test
    fun part1_test7() {
        val parsedInput = Day16().parseInput(listOf("A0016C880162017C3686B18A3D4780"))
        val result = Day16().runPart1(parsedInput)
        assertEquals(31, result)
    }

    @Test
    fun part2_test1() {
        val parsedInput = Day16().parseInput(listOf("C200B40A82"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(3, result)
    }

    @Test
    fun part2_test2() {
        val parsedInput = Day16().parseInput(listOf("04005AC33890"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(54, result)
    }

    @Test
    fun part2_test3() {
        val parsedInput = Day16().parseInput(listOf("880086C3E88112"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(7, result)
    }

    @Test
    fun part2_test4() {
        val parsedInput = Day16().parseInput(listOf("CE00C43D881120"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(9, result)
    }

    @Test
    fun part2_test5() {
        val parsedInput = Day16().parseInput(listOf("D8005AC2A8F0"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(1, result)
    }

    @Test
    fun part2_test6() {
        val parsedInput = Day16().parseInput(listOf("F600BC2D8F"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(0, result)
    }

    @Test
    fun part2_test7() {
        val parsedInput = Day16().parseInput(listOf("9C005AC2F8F0"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(0, result)
    }

    @Test
    fun part2_test8() {
        val parsedInput = Day16().parseInput(listOf("9C0141080250320F1802104A08"))
        val result = Day16().runPart2(parsedInput)
        assertEquals(1, result)
    }
}
