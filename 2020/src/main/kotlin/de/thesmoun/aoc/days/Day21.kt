package de.thesmoun.aoc.days

class Day21 : Day<Collection<Day21.Food>, Any>("Day 21: Allergen Assessment") {

    override fun parseInput(input: Collection<String>) = input.map { Food.parse(it) }

    override fun runPart1(input: Collection<Food>): Any {
        val map = mutableMapOf<String, String>()
        val allergens = input.flatMap { it.allergens }.toMutableSet()

        while (allergens.isNotEmpty()) {
            val allergensToRemove = mutableListOf<String>()
            for (allergen in allergens) {
                val possibleIngredients = input.filter { allergen in it.allergens }
                        .map { it.ingredients }
                        .reduce { acc, set -> (acc intersect set).toMutableSet() }
                if (possibleIngredients.size == 1) {
                    val ingredient = possibleIngredients.first()
                    map[allergen] = ingredient
                    input.forEach {
                        it.ingredients.remove(ingredient)
                        it.allergens.remove(allergen)
                    }
                    allergensToRemove.add(allergen)
                }
            }
            allergensToRemove.forEach { allergens.remove(it) }
        }

        return input.flatMap { it.ingredients }.size
    }

    override fun runPart2(input: Collection<Food>): Any {
        TODO("Not yet implemented")
    }

    data class Food(val ingredients: MutableSet<String>, val allergens: MutableSet<String>) {
        companion object {
            fun parse(line: String): Food {
                val (ingredients, allergens) = line.split(" (contains ")
                return Food(ingredients.split(' ').toMutableSet(),
                        allergens.dropLast(1).split(", ").toMutableSet())
            }
        }
    }
}
