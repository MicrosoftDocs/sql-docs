---
title: "sys.sp_flush_CT_internal_table_on_demand (Transact-SQL)"
description: "Manually clean the side table (change_tracking_objectid) for a table in a database for which change tracking is enabled"
author: JetterMcTedder
ms.author: bspendolini
ms.reviewer: randolphwest
ms.date: 11/28/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_flush_CT_internal_table_on_demand "
  - "sp_flush_CT_internal_table_on_demand_TSQL"
  - "sys.sp_flush_CT_internal_table_on_demand"
  - "sys.sp_flush_CT_internal_table_on_demand_TSQL"
helpviewer_keywords:
  - "sys.sp_flush_CT_internal_table_on_demand"
  - "sp_flush_CT_internal_table_on_demand"
dev_langs:
  - "TSQL"
---
# sys.sp_flush_CT_internal_table_on_demand (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure allows you to manually clean the side table (`change_tracking_objectid`) for a table in a database for which change tracking is enabled. If the *TableToClean* parameter is not passed, then this process cleans all side tables for all tables in the database where change tracking is enabled.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_flush_CT_internal_table_on_demand  [@TableToClean = ] 'TableToClean'
[ , [@DeletedRowCount = ] DeletedRowCount OUTPUT ]
```

## Arguments

#### [ *@TableToClean* = ] '*TableToClean*'

The change tracking-enabled table to be manually cleaned up. The backlogs are left for the automatic cleanup by change tracking. Can be null to clean up all side tables.

## Return code values

`0` (success) or `1` (failure)

## Example

```sql
DECLARE @DeletedRowCount BIGINT;

EXEC sys.sp_flush_CT_internal_table_on_demand '[Sales].[Orders]',
    @DeletedRowCount = @DeletedRowCount OUTPUT;

PRINT CONCAT('Number of rows deleted: ', @DeletedRowCount);
GO
```

The results will look similar to the following output:

```output
Started executing query at Line 1
Cleanup Watermark = 17
Internal Change Tracking table name : change_tracking_1541580530
Total rows deleted: 0.
Number of rows deleted: 0
Total execution time: 00:00:02.949
```

## Remarks

This procedure must be run in a database that has change tracking enabled.

When you run the stored procedure, one of the following scenarios happens:

- If the table doesn't exist or if change tracking isn't enabled, appropriate error messages will be thrown.

- This stored procedure will call another internal stored procedure that cleans up contents from the change tracking side table that's based on the invalid cleanup version by using the `sys.change_tracking_tables` dynamic management view. When it's running, it will show the information of total rows deleted (for every 5000 rows).

This stored procedure is available in the following products:

- [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Service Pack 1 and later versions
- [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] Service Pack 3 and later versions
- [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] Service Pack 4 and later versions
- Azure SQL Database and Azure SQL Managed Instance

## Permissions

 Only a member of the **sysadmin** server role or **db_owner** database role can execute this procedure.

## See also

- [About Change Tracking (Transact-SQL)](../../relational-databases/track-changes/about-change-tracking-sql-server.md)
- [Change Tracking Cleanup and Troubleshooting (Transact-SQL)](../../relational-databases/track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md)
- [Change Tracking Functions (Transact-SQL)](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)
- [Change Tracking System Tables (Transact-SQL)](../../relational-databases/system-tables/change-tracking-tables-transact-sql.md)
- [Change Tracking Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)
