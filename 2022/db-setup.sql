create tablespace aoc_2022
datafile '/opt/oracle/oradata/<todo>/<todo>/AOC_2022.dbf'
size 10M reuse autoextend on maxsize unlimited;
/

create user aoc_2022 identified by aoc
default tablespace aoc_2022
account unlock;
/

alter user aoc_2022 quota unlimited on aoc_2022;
/

grant create database link to aoc_2022;
grant create dimension to aoc_2022;
grant create indextype to aoc_2022;
grant create job to aoc_2022;
grant create materialized view to aoc_2022;
grant create operator to aoc_2022;
grant create procedure to aoc_2022;
grant create sequence to aoc_2022;
grant create session to aoc_2022;
grant create synonym to aoc_2022;
grant create table to aoc_2022;
grant create trigger to aoc_2022;
grant create type to aoc_2022;
grant create view to aoc_2022;
/

create sequence aoc_2022.results_s start with 1;
/

create table aoc_2022.results(
    id number default aoc_2022.results_s.nextval not null,
    day number not null,
    time number not null,
    result clob
    constraint aoc_2022.results_pk primary key (id)
);
/

create index aoc_2022.results_day_idx on aoc_2022.results(day);
/

begin
    dbms_network_acl_admin.append_host_ace(
        host => '10.10.0.3',
        lower_port => 80,
        upper_port => 80,
        ace => xs$ace_type(
            privilege_list => xs$name_list('http'),
            principal_name => 'AOC_2022',
            principal_type => xs_acl.ptype_db
        )
    );
end;
/
