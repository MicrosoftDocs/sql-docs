
<!--
### Code examples for Azure cloud differ slightly from on-premises
-->

Some Transact-SQL code examples written for SQL Server on-premises need small changes to run on Azure SQL Database in the cloud. One catagory of such code examples involves system views whose names differ slightly between the two database systems:

- **server\_** &nbsp; - &nbsp; _prefix for on-premises_
- **database\_** &nbsp; - &nbsp; _prefix for cloud_

The following table lists and compares all the system views that start with either of the two earlier prefixes, as SELECT-ed from the two database systems.

| Name on-premises 2017 | Name in cloud |
| :-------------------- | :------------ |
| server_assembly_modules<br /><br />server_audit_specification_details<br />server_audit_specifications<br />server_audits<br /><br />server_event_notifications<br />server_event_session_actions<br />server_event_session_events<br />server_event_session_fields<br />server_event_session_targets<br />server_event_sessions<br />server_events<br /><br />server_file_audits<br />server_permissions<br /><br />server_principal_credentials<br />server_principals<br /><br />server_role_members<br />server_sql_modules<br /><br />server_trigger_events<br />server_triggers | database_audit_specification_details<br />database_audit_specifications<br /><br />database_automatic_tuning_mode<br />database_automatic_tuning_options<br /><br />database_connection_stats_ex<br />database_credentials<br /><br />database_event_session_actions<br />database_event_session_events<br />database_event_session_fields<br />database_event_session_targets<br />database_event_sessions<br /><br />database_files<br /><br />database_firewall_rules<br />database_firewall_rules_table<br /><br />database_permissions<br />database_principals<br />database_query_store_options<br />database_resource_governor_workload_groups<br />database_role_members<br /><br />database_scoped_configurations<br />database_scoped_credentials<br /><br />database_service_objectives |
| &nbsp; | &nbsp; |

The two lists in the preceding table are accurate as of June 2019. But the table contents here may become outdated, because its content will not be maintained here. For accurate lists, run the following T-SQL SELECT statement. Run the SELECT twice, once on each database system. The WHERE clause must be slightly different for each run, as shown by the `--WHERE` comment.

```sql
SELECT *
  FROM sys.all_objects
  WHERE name LIKE 'server\_%' { ESCAPE '\' }
  --WHERE name LIKE 'database\_%' { ESCAPE '\' }
  ORDER BY name;
```

<!--
The creation of this docs/includes/ file was prompted by Issue 2211 (https://github.com/MicrosoftDocs/sql-docs/issues/2211).
The complaint was that a specific T-SQL code block failed on Azure SQL Database.

GeneMi  ,  2019/05/28
-->
