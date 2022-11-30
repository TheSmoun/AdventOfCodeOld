alter session set container=XEPDB1;
begin
    dbms_network_acl_admin.append_host_ace(
        host => 'files',
        lower_port => 80,
        upper_port => 80,
        ace => xs$ace_type(
            privilege_list => xs$name_list('http'),
            principal_name => 'AOC_2022',
            principal_type => xs_acl.ptype_db
        )
    );
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
exit;
