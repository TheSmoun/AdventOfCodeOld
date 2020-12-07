package de.thesmoun.aoc.days

class Day7 : Day<Map<String, List<Day7.Rule>>, Int>("Day 7: Handy Haversacks") {

    private val ruleRegex = Regex("(\\w+ \\w+) bags contain (.*)")
    private val itemRegex = Regex("(\\d+) (\\w+ \\w+) bags?")
    private val target = "shiny gold"

    override fun parseInput(input: Collection<String>) = input.associate {
        val (key, items) = ruleRegex.find(it)!!.destructured
        key to itemRegex.findAll(items).map { match ->
            val (count, item) = match.destructured
            Rule(count.toInt(), item)
        }.toList()
    }

    override fun runPart1(input: Map<String, List<Rule>>) = input.keys.count {
        traverse(input, it).any { rule -> rule.color == target }
    }

    override fun runPart2(input: Map<String, List<Rule>>) = traverse(input, target).sumOf { it.amount }

    private fun traverse(rules: Map<String, List<Rule>>, target: String) = sequence {
        val deque = ArrayDeque(rules[target].orEmpty())
        while (true) {
            val rule = deque.removeFirstOrNull() ?: break
            yield(rule)
            rules[rule.color]?.forEach { (count, color) -> deque.add(Rule(rule.amount * count, color)) }
        }
    }

    data class Rule(val amount: Int, val color: String)
}
