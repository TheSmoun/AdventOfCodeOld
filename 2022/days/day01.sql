declare
    l_solution_1 number;
    l_solution_2 number;
begin
    aoc_main.init(1);

    select count(*)
    into l_solution_1
    from aoc_input a
    join aoc_input b
        on (a.line + 1 = b.line)
            and to_number(a.content) < to_number(b.content);

    aoc_main.report_solution(1, l_solution_1);

    commit;
end;
