---
description: "sys.sp_flush_CT_internal_table_on_demand  (Transact-SQL)"
title: "sys.sp_flush_CT_internal_table_on_demand  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/21/2022"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_flush_CT_internal_table_on_demand "
  - "sp_flush_CT_internal_table_on_demand_TSQL"
  - "sys.sp_flush_CT_internal_table_on_demand"
  - "sys.sp_flush_CT_internal_table_on_demand_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_flush_CT_internal_table_on_demand"
  - "sp_flush_CT_internal_table_on_demand"
ms.assetid: 
author: JetterMcTedder
ms.author: bspendolini
---
# sys.sp_flush_CT_internal_table_on_demand (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This stored procedure allows you to manually clean up the side table (change_tracking_objectid) for a table in a database which change tracking is enabled. If the tablename parameter is not passed, then this process will clean up all side tables for all tables in the database where change tracking is enabled.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
```
sys.sp_flush_CT_internal_table_on_demand  [@TableToClean = ] 'TableToClean'
[ , [@DeletedRowCount = ] DeletedRowCount OUTPUT ]
```

## Arguments
[@TableToClean = ] TableToClean is the Change Tracking enabled table to be manually cleaned up. The backlogs are left for the automatic cleanup by change tracking. Can be null to clean up all side tables.


## Return Code Values  
 **0** (success) or **1** (failure)

## Example
```
DECLARE @DeletedRowCount bigint;

exec sys.sp_flush_CT_internal_table_on_demand [sales.orders], @DeletedRowCount = @DeletedRowCount OUTPUT;

print concat('Number of rows deleted: ', @DeletedRowCount);
GO

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

If the table doesn't exist or if change tracking isn't enabled, appropriate error messages will be thrown.

This stored procedure will call another internal stored procedure that cleans up contents from the change tracking side table that's based on the invalid cleanup version by using the sys.change_tracking_tables dynamic management view. When it's running, it will show the information of total rows deleted (for every 5000 rows).

This stored procedure is available in Azure SQL and the following service packs for SQL Server:

SQL Server 2016 Service Pack 1

Service Pack 3 for SQL Server 2014

Service Pack 4 for SQL Server 2012

## Permissions  
 Only a member of the sysadmin server role or db_owner database role can execute this procedure.  
  
## See Also  
 [Change Tracking Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)  
 [About Change Tracking &#40;Transact-SQL&#41;](../../relational-databases/track-changes/about-change-tracking-sql-server.md)  
 [Change Tracking Cleanup &#40;Transact-SQL&#41;](../../relational-databases/track-changes/cleanup-and-troubleshooting-change-tracking-sql-server.md)  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)  
  
