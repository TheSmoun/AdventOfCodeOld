create or replace package body aoc_log as

    g_batch_id number;

    procedure log(p_package in aoc_logs.package%type,
                  p_method in aoc_logs.method%type,
                  p_message in aoc_logs.message%type,
                  p_log_level in aoc_logs.log_level%type)
    as
    begin
        insert into aoc_logs(
            batch_id,
            package,
            method,
            message,
            log_level
        ) values (
            g_batch_id,
            p_package,
            p_method,
            p_message,
            p_log_level
        );
    end;

    procedure init
    as
    begin
        g_batch_id := aoc_logs_batch_s.nextval;
    end;

    procedure debug(p_message in aoc_logs.message%type)
    as
    begin
        debug(
            p_package => null,
            p_method => null,
            p_message => p_message
        );
    end debug;

    procedure debug(p_package in aoc_logs.package%type,
                    p_method in aoc_logs.method%type,
                    p_message in aoc_logs.message%type)
    as
    begin
        log(
            p_package => p_package,
            p_method => p_method,
            p_message => p_message,
            p_log_level => c_log_level_debug
        );
    end debug;

    procedure information(p_message in aoc_logs.message%type)
    as
    begin
        information(
            p_package => null,
            p_method => null,
            p_message => p_message
        );
    end information;

    procedure information(p_package in aoc_logs.package%type,
                          p_method in aoc_logs.method%type,
                          p_message in aoc_logs.message%type)
    as
    begin
        log(
            p_package => p_package,
            p_method => p_method,
            p_message => p_message,
            p_log_level => c_log_level_info
        );
    end information;

    procedure error(p_message in aoc_logs.message%type)
    as
    begin
        error(
            p_package => null,
            p_method => null,
            p_message => p_message
        );
    end error;

    procedure error(p_package in aoc_logs.package%type,
                    p_method in aoc_logs.method%type,
                    p_message in aoc_logs.message%type)
    as
    begin
        log(
            p_package => p_package,
            p_method => p_method,
            p_message => p_message,
            p_log_level => c_log_level_error
        );
    end error;

end aoc_log;
