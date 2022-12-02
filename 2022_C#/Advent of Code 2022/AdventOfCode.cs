using Advent_of_Code_2022.Days;

var days = typeof(DayBase).Assembly.GetTypes()
    .Where(t => !t.IsAbstract && typeof(DayBase).IsAssignableFrom(t))
    .OrderBy(t => int.Parse(t.Name[3..]))
    .Select(t => Activator.CreateInstance(t) as DayBase);

days.Last()?.Run();
