---
author: bluefooted
ms.author: pamela
ms.reviewer: dfurman
ms.date: 10/02/2025
ms.service: azure-sql-database
ms.topic: include
---

Some Transact-SQL code examples written for SQL Server need small changes to run in Azure SQL Database or SQL database in Fabric. One category of such code examples involves catalog views whose name prefixes differ depending on the database engine type:

- `server_` - *prefix for SQL Server and Azure SQL Managed Instance*
- `database_` - *prefix for Azure SQL Database, SQL database in Fabric, and SQL Managed Instance*

Azure SQL Database and SQL database in Fabric support only database-scoped event sessions. [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) supports database-scoped event sessions for Azure SQL Database: an **Extended Events** node containing database-scoped sessions appears under each database in [Object Explorer](/ssms/object/object-explorer).

Azure SQL Managed Instance supports both database-scoped sessions and server-scoped sessions. SSMS fully supports server-scoped sessions for SQL Managed Instance: an **Extended Events** node containing all server-scoped sessions appears under the **Management** folder for each managed instance in Object Explorer.

> [!NOTE]  
> Server-scoped event sessions are recommended for Azure SQL Managed Instance.
>
> Database-scoped event sessions aren't displayed in Object Explorer in SSMS for Azure SQL Managed Instance. On a SQL managed instance, database-scoped event sessions can only be queried and managed with Transact-SQL.

For illustration, the following table lists and compares two subsets of catalog views. The subsets have differing name prefixes because they support different database engine types.

| Name in SQL Server and Azure SQL Managed Instance | Name in Azure SQL Database, SQL database in Fabric, and Azure SQL Managed Instance |
| --- | --- |
| `sys.server_event_session_actions`<br />`sys.server_event_session_events`<br />`sys.server_event_session_fields`<br />`sys.server_event_session_targets`<br />`sys.server_event_sessions`<br />| `sys.database_event_session_actions`<br />`sys.database_event_session_events`<br />`sys.database_event_session_fields`<br />`sys.database_event_session_targets`<br />`sys.database_event_sessions` |
