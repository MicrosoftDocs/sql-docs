
<!--
### Code examples for Azure cloud differ slightly from on-premises
  Or.....
### Code examples can differ for Azure SQL Database
-->

Some Transact-SQL code examples written for SQL Server on-premises need small changes to run in the cloud. One category of such code examples involves system views whose name prefixes differ slightly between the two database systems:

- **server\_** &nbsp; - &nbsp; _prefix for SQL Server and SQL Managed Instance_
- **database\_** &nbsp; - &nbsp; _prefix for Azure SQL Database and SQL Managed Instance_

[!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] can only execute database-scoped sessions. [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] supports both database-scoped sessions and more capable server-scoped sessions.

For illustration, the following table lists and compares two subsets of the system views. For brevity, the subsets are restricted to view names that also contain the string `_event`. The subsets have differing name prefixes because they come from two different database systems.

| Name from SQL Server | Name from cloud service |
| :------------------------- | :---------------------- |
| server_event_notifications<br />server_event_session_actions<br />server_event_session_events<br />server_event_session_fields<br />server_event_session_targets<br />server_event_sessions<br />server_events<br />server_trigger_events | database_event_session_actions<br />database_event_session_events<br />database_event_session_fields<br />database_event_session_targets<br />database_event_sessions |

The two lists in the preceding table are accurate as of March 2022. But the table contents here may become outdated, because they will not be maintained here. For accurate lists, refer to the following T-SQL SELECT statement.

```sql
SELECT name
    FROM sys.all_objects
    WHERE
        (name LIKE 'database\_%' { ESCAPE '\' } OR
         name LIKE 'server\_%' { ESCAPE '\' })
        AND name LIKE '%\_event%' { ESCAPE '\' }
        AND type = 'V'
    ORDER BY name;
```

