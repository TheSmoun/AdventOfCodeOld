package de.thesmoun.aoc.days

class Day21 : Day<Collection<Day21.Food>, Any>("Day 21: Allergen Assessment") {

    override fun parseInput(input: Collection<String>) = input.map { Food.parse(it) }

    override fun runPart1(input: Collection<Food>): Any {
        runPart(input)
        return input.flatMap { it.ingredients }.size
    }

    override fun runPart2(input: Collection<Food>): Any {
        val map = runPart(input).toSortedMap()
        return map.values.joinToString(",")
    }

    private fun runPart(input: Collection<Food>): Map<String, String> {
        val map = mutableMapOf<String, String>()
        val allergens = input.flatMap { it.allergens }.toMutableSet()

        while (allergens.isNotEmpty()) {
            for (allergen in allergens.toList()) {
                val possibleIngredients = input.filter { allergen in it.allergens }
                        .map { it.ingredients.toSet() }.reduce { acc, set -> (acc intersect set) }
                if (possibleIngredients.size == 1) {
                    val ingredient = possibleIngredients.first()
                    map[allergen] = ingredient
                    input.forEach {
                        it.ingredients.remove(ingredient)
                        it.allergens.remove(allergen)
                    }
                    allergens.remove(allergen)
                }
            }
        }

        return map
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
