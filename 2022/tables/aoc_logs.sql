create sequence aoc_logs_s start with 1;
/
create sequence aoc_logs_batch_s start with 1;
/
create table aoc_logs(
    log_id number default aoc_logs_s.nextval not null,
    batch_id number not null,
    package varchar2(128),
    method varchar2(128),
    message varchar2(4000),
    log_level varchar2(10),
    constraint aoc_debug_pk primary key (log_id)
);
/
create index aoc_logs_batch_id on aoc_logs(batch_id);
/
