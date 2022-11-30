create sequence aoc_debug_s start with 1;
/
create table aoc_debug(
    log_id number default aoc_debug_s.nextval not null,
    batch_id number not null,
    package varchar2(128) not null,
    method varchar2(128) not null,
    message varchar2(4000),
    log_level number,
    constraint aoc_debug_pk primary key (log_id)
);
/
create index aoc_debug_batch_id on aoc_debug(batch_id);
/
