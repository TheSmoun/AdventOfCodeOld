declare
    l_solution_1 number;
    l_solution_2 number;
begin
    aoc_main.init(1);

    select max(content)
    into l_solution_1
    from (
        select root, sum(content) content
        from (
            select connect_by_root line root, to_number(content) content
            from aoc_input
            start with line = 1 or content is null
            connect by prior line + 1 = line and content is not null
        )
        group by root
    );

    aoc_main.report_solution(1, l_solution_1);

    select sum(content)
    into l_solution_2
    from (
        select content
        from (
            select content
            from (
                select root, sum(content) content
                from (
                    select connect_by_root line root, to_number(content) content
                    from aoc_input
                    start with line = 1 or content is null
                    connect by prior line + 1 = line and content is not null
                )
                group by root
            )
            order by content desc
        )
        where rownum <= 3
    );

    aoc_main.report_solution(2, l_solution_2);

    commit;
end;
