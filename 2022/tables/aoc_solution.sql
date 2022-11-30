create table aoc_solution(
    day number not null,
    part number not null,
    solution varchar2(4000),
    constraint aoc_solution_pk primary key (day, part)
);
/
