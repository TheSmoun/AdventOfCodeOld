create or replace package aoc_log as

    c_log_level_debug constant aoc_logs.log_level%type := 'DEBUG';
    c_log_level_info constant aoc_logs.log_level%type := 'INFO';
    c_log_level_error constant aoc_logs.log_level%type := 'ERROR';

    procedure init;

    procedure debug(p_message in aoc_logs.message%type);

    procedure debug(p_package in aoc_logs.package%type,
                    p_method in aoc_logs.method%type,
                    p_message in aoc_logs.message%type);

    procedure information(p_message in aoc_logs.message%type);

    procedure information(p_package in aoc_logs.package%type,
                          p_method in aoc_logs.method%type,
                          p_message in aoc_logs.message%type);

    procedure error(p_message in aoc_logs.message%type);

    procedure error(p_package in aoc_logs.package%type,
                    p_method in aoc_logs.method%type,
                    p_message in aoc_logs.message%type);

end aoc_log;