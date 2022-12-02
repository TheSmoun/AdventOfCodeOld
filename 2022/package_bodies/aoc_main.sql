create or replace package body aoc_main as

    g_day number;

    procedure load_input(p_day in number,
                         p_test in number)
    as
        l_url varchar2(256);
        l_request utl_http.req;
        l_response utl_http.resp;
        l_line number := 1;
        l_content varchar2(4000);
    begin
        l_url := 'http://files/day' || to_char(p_day, 'fm00') || nvl2(p_test, '_test' || to_char(p_test, 'fm00'), '') || '.txt';
        l_request := utl_http.begin_request(l_url);
        l_response := utl_http.get_response(l_request);

        loop
            utl_http.read_line(l_response, l_content, true);
            insert into aoc_input(line, content) values (l_line, l_content);
            l_line := l_line + 1;
        end loop;
    exception
        when utl_http.end_of_body then
            utl_http.end_response(l_response);
    end load_input;

    function init(p_day in number,
                  p_test in number default null)
    return t_input_table as
        l_input t_input_table;
    begin
        if p_day is null or p_day not between 1 and 25 then
            raise e_invalid_day;
        end if;

        g_day := p_day;
        aoc_log.init;

        delete from aoc_input
        where line is not null;

        load_input(p_day, p_test);

        select line,
            content
        bulk collect into l_input
        from aoc_input
        order by line;

        return l_input;
    end init;

    procedure init(p_day in number,
                   p_test in number default null)
    as
        l_input t_input_table;
    begin
        l_input := aoc_main.init(p_day, p_test);
    end init;

    procedure report_solution(p_part in number,
                              p_solution in number)
    as
    begin
        report_solution(p_part, to_char(p_solution));
    end report_solution;

    procedure report_solution(p_part in number,
                              p_solution in varchar)
    as
    begin
        if p_part is null or p_part not in (1, 2) then
            raise e_invalid_day;
        end if;

        merge into aoc_solution e
        using (
            select g_day day,
                p_part part,
                p_solution solution
            from dual
        ) n
        on (
            e.day = n.day
                and e.part = n.part
        )
        when matched then
            update set
                e.solution = n.solution
        when not matched then
            insert (
                day,
                part,
                solution
            ) values (
                n.day,
                n.part,
                n.solution
            );
    end report_solution;

end aoc_main;
/
