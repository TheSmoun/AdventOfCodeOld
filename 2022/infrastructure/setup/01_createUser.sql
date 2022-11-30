alter session set container=XEPDB1;
create tablespace aoc_2022
datafile '/opt/oracle/oradata/XE/XEPDB1/AOC_2022.dbf'
size 10M reuse autoextend on maxsize unlimited;
create user AOC_2022 identified by aoc
default tablespace aoc_2022
account unlock;
alter user aoc_2022 quota unlimited on aoc_2022;
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
exit;
