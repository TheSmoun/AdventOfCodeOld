using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day14 : DayBase<Dictionary<string, Day14.Reaction>, long>
    {
        public override string Name => "Day 14: Space Stoichiometry";

        public override Dictionary<string, Reaction> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(l => new Reaction(l)).ToDictionary(r => r.Product.Name);
        }

        public override long RunPart1(Dictionary<string, Reaction> input)
        {
            var chemicals = new DefaultDictionary<string, long> {{"ORE", long.MaxValue}};
            Consume("FUEL", 1, chemicals, input);
            return long.MaxValue - chemicals["ORE"];
        }

        public override long RunPart2(Dictionary<string, Reaction> input)
        {
            const long ores = 1_000_000_000_000L;
            var chemicals = new DefaultDictionary<string, long> {{"ORE", ores}};
            Produce("FUEL", 1, chemicals, input);
            var consumed = ores - chemicals["ORE"];
            while (Produce("FUEL", Math.Max(1, chemicals["ORE"] / consumed), chemicals, input));
            return chemicals["FUEL"];
        }

        private static bool Consume(string chemical, long amount, DefaultDictionary<string, long> chemicals, Dictionary<string, Reaction> reactions)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            var available = chemicals[chemical];
            if (available < amount && !Produce(chemical, amount - available, chemicals, reactions))
                return false;

            chemicals[chemical] -= amount;
            return true;
        }

        private static bool Produce(string chemical, long amount, DefaultDictionary<string, long> chemicals, Dictionary<string, Reaction> reactions)
        {
            if (chemical == "ORE")
                return false;

            var reaction = reactions[chemical];
            var count = (long) Math.Ceiling((double) amount / reaction.Product.Amount);
            if (reaction.Ingredients.Any(ingredient => !Consume(ingredient.Name, count * ingredient.Amount, chemicals, reactions)))
                return false;

            chemicals[chemical] += count * reaction.Product.Amount;
            return true;
        }

        public struct Reaction
        {
            public Component Product;
            public Component[] Ingredients;

            public Reaction(string line)
            {
                var formula = line.Split(" => ");
                Product = ParseSide(formula[1]).Single();
                Ingredients = ParseSide(formula[0]).ToArray();
            }

            private static IEnumerable<Component> ParseSide(string side)
            {
                return side.Split(", ").Select(e =>
                {
                    var parts = e.Split(" ");
                    return new Component {Name = parts[1], Amount = int.Parse(parts[0])};
                });
            }
        }

        public struct Component
        {
            public string Name;
            public int Amount;
        }
    }
}
