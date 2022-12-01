create or replace package aoc_main as

    e_invalid_day exception;
    pragma exception_init(e_invalid_day, -46001);

    e_invalid_part exception;
    pragma exception_init(e_invalid_part, -46002);

    type t_input is record(
        line number,
        content varchar2(4000)
    );

    type t_input_table is table of t_input index by binary_integer;

    function init(p_day in number,
                  p_test in number default null)
    return t_input_table;

    procedure init(p_day in number,
                   p_test in number default null);

    procedure report_solution(p_part in number,
                              p_solution in number);

    procedure report_solution(p_part in number,
                              p_solution in varchar);

end aoc_main;
