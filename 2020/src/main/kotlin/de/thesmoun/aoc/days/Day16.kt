package de.thesmoun.aoc.days

import de.thesmoun.aoc.util.splitAt

class Day16 : Day<Day16.Input, Long>("Day 16: Ticket Translation") {

    override fun parseInput(input: Collection<String>): Input {
        val (fieldsInput, ticketInput, ticketsInput) = input.splitAt("")
        val fields = fieldsInput.map {
            val (key, ll, lu, ul, uu) = Regex("^(.*): (\\d+)-(\\d+) or (\\d+)-(\\d+)$").find(it)!!.destructured
            Field(key, FieldRange(ll.toInt()..lu.toInt(), ul.toInt()..uu.toInt()))
        }
        val ticket = Ticket(ticketInput.last().split(',').map { it.toInt() })
        val tickets = ticketsInput.drop(1).map { Ticket(it.split(',').map { i -> i.toInt() }) }
        return Input(fields, ticket, tickets)
    }

    override fun runPart1(input: Input) = input.tickets.flatMap { it.getInvalidValues(input.fields) }.sum().toLong()

    override fun runPart2(input: Input): Long {
        val validTickets = input.tickets.filter { it.isValid(input.fields) }
        val indices = input.ticket.values.size
        val fieldIndices = mutableMapOf<Int, Int>()

        while (fieldIndices.size < indices) {
            for (i in 0 until indices) {
                if (i in fieldIndices.values)
                    continue

                val values = validTickets.map { it.values[i] }

                val possibleFields = mutableListOf<Int>()
                for ((j, field) in input.fields.withIndex()) {
                    if (j in fieldIndices.keys)
                        continue

                    if (values.all { it in field.range })
                        possibleFields.add(j)
                }

                val possibleField = possibleFields.singleOrNull()
                if (possibleField != null)
                    fieldIndices[possibleField] = i
            }
        }

        return (0..5).map { input.ticket.values[fieldIndices[it]!!] }.fold(1L) { acc, i -> acc * i }
    }

    data class Input(val fields: Collection<Field>, val ticket: Ticket, val tickets: Collection<Ticket>)

    data class Field(val key: String, val range: FieldRange)

    data class FieldRange(val lower: IntRange, val upper: IntRange) {
        operator fun contains(value: Int) = value in lower || value in upper
    }

    data class Ticket(val values: List<Int>) {
        fun isValid(fields: Collection<Field>) = getInvalidValues(fields).none()
        fun getInvalidValues(fields: Collection<Field>) = values.filter { fields.all { f -> it !in f.range } }
    }
}
