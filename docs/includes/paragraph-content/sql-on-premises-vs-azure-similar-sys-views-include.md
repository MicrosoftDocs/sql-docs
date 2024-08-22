---
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/22/2023
ms.service: azure-sql-database
ms.topic: include
---
Some Transact-SQL code examples written for SQL Server need small changes to run in Azure. One category of such code examples involves catalog views whose name prefixes differ depending on the database engine type:

- `server_` - *prefix for SQL Server and Azure SQL Managed Instance*
- `database_` - *prefix for Azure SQL Database and SQL Managed Instance*

Azure SQL Database supports only database-scoped event sessions. [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md) (SSMS) fully supports database-scoped event sessions for Azure SQL Database: an **Extended Events** node containing database-scoped sessions appears under each database in [Object Explorer](../../ssms/object/object-explorer.md).

Azure SQL Managed Instance supports both database-scoped sessions and server-scoped sessions. SSMS fully supports server-scoped sessions for SQL Managed Instance: an **Extended Events** node containing all server-scoped sessions appears under the **Management** folder for each managed instance in Object Explorer.

> [!NOTE]  
> Server-scoped sessions are recommended for managed instances. Database-scoped sessions aren't displayed in Object Explorer in SSMS for Azure SQL Managed Instance. Database-scoped sessions can only be queried and managed with Transact-SQL when using a managed instance.

For illustration, the following table lists and compares two subsets of catalog views. For brevity, the subsets are restricted to view names that also contain the string `_event`. The subsets have differing name prefixes because they support different database engine types.

| Name in SQL Server and Azure SQL Managed Instance | Name in Azure SQL Database and Azure SQL Managed Instance |
| :--- | :--- |
| server_event_notifications<br />server_event_session_actions<br />server_event_session_events<br />server_event_session_fields<br />server_event_session_targets<br />server_event_sessions<br />server_events<br />server_trigger_events | database_event_session_actions<br />database_event_session_events<br />database_event_session_fields<br />database_event_session_targets<br />database_event_sessions |

The two lists in the preceding table were accurate as of March 2022. For an up-to-date list, run the following Transact-SQL `SELECT` statement:

```sql
SELECT name
    FROM sys.all_objects
    WHERE
        (name LIKE 'database[_]%' OR
         name LIKE 'server[_]%' )
        AND name LIKE '%[_]event%'
        AND type = 'V'
        AND SCHEMA_NAME(schema_id) = 'sys'
    ORDER BY name;
```
