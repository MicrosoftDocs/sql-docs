---
title: "System dynamic management views (Transact-SQL)"
description: "Dynamic management views and functions return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance."
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/23/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "database scoped dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server], about dynamic management objects"
  - "server state information [SQL Server]"
  - "dynamic management functions [SQL Server]"
  - "metadata [SQL Server], dynamic management objects"
  - "dynamic management views [SQL Server]"
  - "DMVs [SQL Server]"
  - "functions [SQL Server], dynamic management"
  - "server scoped dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# System dynamic management views

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Dynamic management views (DMVs) and dynamic management functions (DMFs) return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance.

> [!IMPORTANT]  
> Dynamic management views and functions return internal, implementation-specific state data. Their schemas and the data they return may change in future releases of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, dynamic management views and functions in future releases may not be compatible with the dynamic management views and functions in this release. For example, in future releases of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Microsoft may augment the definition of any dynamic management view by adding columns to the end of the column list. We recommend against using the syntax `SELECT * FROM dynamic_management_view_name` in production code because the number of columns returned might change and break your application.

There are two types of dynamic management views and functions:

- Server-scoped dynamic management views and functions. These require VIEW SERVER STATE permission on the server. For SQL Server 2022 and later, VIEW SERVER PERFORMANCE STATE is required, or VIEW SERVER SECURITY STATE is required for a few DMVs that are security related.

- Database-scoped dynamic management views and functions. These require VIEW DATABASE STATE permission on the database. For SQL Server 2022 and later, VIEW DATABASE PERFORMANCE STATE is required, or VIEW DATABASE SECURITY STATE is required for a few DMVs that are security related.

## Query dynamic management views

Dynamic management views can be referenced in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements by using two-part, three-part, or four-part names. Dynamic management functions on the other hand can be referenced in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements by using either two-part or three-part names. Dynamic management views and functions can't be referenced in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements by using one-part names.

All dynamic management views and functions exist in the sys schema and follow this naming convention dm_*. When you use a dynamic management view or function, you must prefix the name of the view or function by using the sys schema. For example, to query the dm_os_wait_stats dynamic management view, run the following query:

```sql
SELECT wait_type, wait_time_ms
FROM sys.dm_os_wait_stats;
```

### Required permissions

To query a dynamic management view or function requires SELECT permission on object and VIEW SERVER STATE or VIEW DATABASE STATE permission. This lets you selectively restrict access of a user or login to dynamic management views and functions. To do this, first create the user in `master` and then deny the user SELECT permission on the dynamic management views or functions that you don't want them to access. After this, the user can't select from these dynamic management views or functions, regardless of database context of the user.

> [!NOTE]  
> Because DENY takes precedence, if a user has been granted VIEW SERVER STATE permissions but denied VIEW DATABASE STATE permission, the user can see server-level information, but not database-level information.

## In this section

Dynamic management views and functions have been organized into the following categories.

:::row:::
    :::column:::
        [Always On Availability Groups Dynamic Management Views and Functions (Transact-SQL)](always-on-availability-groups-dynamic-management-views-functions.md)

        [Change Data Capture related dynamic management views (Transact-SQL)](change-data-capture-sys-dm-cdc-errors.md)

        [Change Tracking related dynamic management views](change-tracking-sys-dm-tran-commit-table.md)

        [Common Language Runtime related dynamic management views (Transact-SQL)](common-language-runtime-related-dynamic-management-views-transact-sql.md)

        [Database Mirroring related dynamic management views (Transact-SQL)](database-mirroring-sys-dm-db-mirroring-auto-page-repair.md)

        [Database related dynamic management views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)

        [Execution related dynamic management views and functions (Transact-SQL)](execution-related-dynamic-management-views-and-functions-transact-sql.md)

        [Extended Events Dynamic Management Views](extended-events-dynamic-management-views.md)

        [Filestream and FileTable related dynamic management views (Transact-SQL)](filestream-and-filetable-dynamic-management-views-transact-sql.md)

        [Full-Text Search and Semantic Search Dynamic Management Views and Functions (Transact-SQL)](full-text-and-semantic-search-dynamic-management-views-functions.md)

        [Geo-Replication Dynamic Management Views and Functions (Azure SQL Database)](geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)

        [Index related dynamic management views and functions (Transact-SQL)](index-related-dynamic-management-views-and-functions-transact-sql.md)

        [I O related dynamic management views and functions (Transact-SQL)](i-o-related-dynamic-management-views-and-functions-transact-sql.md)

        [PolyBase Dynamic Management Views](../../relational-databases/polybase/polybase-troubleshooting.md)
    :::column-end:::
    :::column:::
        [Memory-Optimized Table related dynamic management views (Transact-SQL)](memory-optimized-table-dynamic-management-views-transact-sql.md)

        [Object related dynamic management views and functions (Transact-SQL)](object-related-dynamic-management-views-and-functions-transact-sql.md)

        [Query Notifications related dynamic management views (Transact-SQL)](query-notifications-sys-dm-qn-subscriptions.md)

        [Replication related dynamic management views (Transact-SQL)](replication-related-dynamic-management-views-transact-sql.md)

        [Resource Governor related dynamic management views (Transact-SQL)](resource-governor-related-dynamic-management-views-transact-sql.md)

        [Security-related dynamic management views and functions (Transact-SQL)](security-related-dynamic-management-views-and-functions-transact-sql.md)

        [Server-related dynamic management views and functions (Transact-SQL)](server-related-dynamic-management-views-and-functions-transact-sql.md)

        [Service Broker related dynamic management views (Transact-SQL)](service-broker-related-dynamic-management-views-transact-sql.md)

        [Spatial Data related dynamic management views and functions (Transact-SQL)](./spatial-data-sys-dm-db-objects-disabled-on-compatibility-level-change.md)

        [Azure Synapse Analytics and Parallel Data Warehouse related dynamic management views (Transact-SQL)](sql-and-parallel-data-warehouse-dynamic-management-views.md)

        [SQL Server Operating System related dynamic management views (Transact-SQL)](sql-server-operating-system-related-dynamic-management-views-transact-sql.md)

        [Stretch Database related dynamic management views (Transact-SQL)](stretch-database-related-dynamic-management-views-transact-sql.md)

        [Transaction related dynamic management views and functions (Transact-SQL)](transaction-related-dynamic-management-views-and-functions-transact-sql.md)
    :::column-end:::
:::row-end:::

## Next steps

- [GRANT Server Permissions (Transact-SQL)](../../t-sql/statements/grant-server-permissions-transact-sql.md)
- [GRANT Database Permissions (Transact-SQL)](../../t-sql/statements/grant-database-permissions-transact-sql.md)
- [System Views (Transact-SQL)](../../t-sql/language-reference.md)
