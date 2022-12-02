declare
    l_solution_1 number;
    l_solution_2 number;
begin
    aoc_main.init(2);

    select sum(selected_score + winning_score) score
    into l_solution_1
    from (
        select case second
                when 'X' then 1
                when 'Y' then 2
                when 'Z' then 3
            end selected_score,
            case
                when first = 'A' and second = 'X' then 3
                when first = 'B' and second = 'Y' then 3
                when first = 'C' and second = 'Z' then 3
                when first = 'A' and second = 'Y' then 6
                when first = 'B' and second = 'Z' then 6
                when first = 'C' and second = 'X' then 6
                else 0
            end winning_score
        from (
            select substr(content, 1, 1) first,
                substr(content, 3, 1) second
            from aoc_input
        )
    );

    aoc_main.report_solution(1, l_solution_1);

    select sum(selected_score + winning_score) score
    into l_solution_2
    from (
        select case
                when first = 'A' and second = 'X' then 3
                when first = 'A' and second = 'Y' then 1
                when first = 'A' and second = 'Z' then 2
                when first = 'B' and second = 'X' then 1
                when first = 'B' and second = 'Y' then 2
                when first = 'B' and second = 'Z' then 3
                when first = 'C' and second = 'X' then 2
                when first = 'C' and second = 'Y' then 3
                when first = 'C' and second = 'Z' then 1
                else 0
            end selected_score,
            case second
                when 'X' then 0
                when 'Y' then 3
                when 'Z' then 6
            end winning_score
        from (
            select substr(content, 1, 1) first,
                substr(content, 3, 1) second
            from aoc_input
        )
    );

    aoc_main.report_solution(2, l_solution_2);

    commit;
end;
