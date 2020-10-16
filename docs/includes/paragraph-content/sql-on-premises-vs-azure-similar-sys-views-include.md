
<!--
### Code examples for Azure cloud differ slightly from on-premises
  Or.....
### Code examples can differ for Azure SQL Database
-->

Some Transact-SQL code examples written for SQL Server on-premises need small changes to run on Azure SQL Database service in the cloud. One category of such code examples involves system views whose name prefixes differ slightly between the two database systems:

- **server\_** &nbsp; - &nbsp; _prefix for on-premises_
- **database\_** &nbsp; - &nbsp; _prefix for Azure SQL Database_

For illustration, the following table lists and compares two subsets of the system views. For brevity, the subsets are restricted to view names that also contain the string `_event`. The subsets have differing name prefixes because they come from two different database systems.

| Name from on-premises 2017 | Name from cloud service |
| :------------------------- | :---------------------- |
| server_event_notifications<br />server_event_session_actions<br />server_event_session_events<br />server_event_session_fields<br />server_event_session_targets<br />server_event_sessions<br />server_events<br />server_trigger_events | database_event_session_actions<br />database_event_session_events<br />database_event_session_fields<br />database_event_session_targets<br />database_event_sessions |
| &nbsp; | &nbsp; |

The two lists in the preceding table are accurate as of June 2019. But the table contents here may become outdated, because they will not be maintained here. For accurate lists, run the following T-SQL SELECT statement. Run the SELECT twice, once on each database system.

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

<!--
The creation of this docs/includes/ file was prompted by Issue 2211 (https://github.com/MicrosoftDocs/sql-docs/issues/2211).
Initial PR was PR 10427 (https://github.com/MicrosoftDocs/sql-docs-pr/pull/10427).
The complaint was that a specific T-SQL code block failed on Azure SQL Database.

GeneMi  ,  2019/05/28
-->
