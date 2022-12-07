---
description: "Active Geo-Replication - sp_wait_for_database_copy_sync"
title: "sp_wait_for_database_copy_sync"
titleSuffix: Azure SQL Database
ms.date: "03/03/2017"
ms.service: sql-database
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "sp_wait_for_database_copy_sync_TSQL"
  - "sp_wait_for_database_copy_sync"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_wait_for_database_copy_sync"
ms.assetid: 7068da7f-cb74-47f2-b064-eb076a0d3885
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom: seo-dt-2019
---

# Active Geo-Replication - sp_wait_for_database_copy_sync

[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

This procedure is scoped to an [!INCLUDE[ssGeoDR](../../includes/ssgeodr-md.md)] relationship between a primary and secondary. Calling the `sys.sp_wait_for_database_copy_sync` causes the application to wait until all committed transactions are replicated and acknowledged by the active secondary database. Run `sys.sp_wait_for_database_copy_sync` on only the primary database.

## Syntax  
  
```sql
sp_wait_for_database_copy_sync [ @target_server = ] 'server_name'   
     , [ @target_database = ] 'database_name'  
```  
  
## Arguments

[ @target_server = ] 'server_name'  
 The name of the SQL Database server that hosts the active secondary database. server_name is sysname, with no default.  
  
[ @target_database = ] 'database_name'  
 The name of the active secondary database. database_name is sysname, with no default.  
  
## Return Code Values

Returns 0 for success or an error number for failure.  
  
The most likely error conditions are as follows:  
  
- The server name or database name is missing.  
  
- The link cannot be found to the specified server name or database.  
  
- Interlink connectivity has been lost, and `sys.sp_wait_for_database_copy_sync` will return after the connection timeout.  

## Permissions

Any user in the primary database can call this system stored procedure. The login must be a user in both the primary and active secondary databases.  
  
## Remarks

All transactions committed before a **sp_wait_for_database_copy_sync** call are sent to the active secondary database.  
  
## Examples

The following example invokes **sp_wait_for_database_copy_sync** to ensure that all transactions are committed to the primary database, db0, get sent to its active secondary database on the target server ubfyu5ssyt.  
  
```sql
USE db0;  
GO  
EXEC sys.sp_wait_for_database_copy_sync @target_server = N'ubfyu5ssyt1', @target_database = N'db0';  
GO  
```  
  
## See Also

- [sys.dm_continuous_copy_status &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-continuous-copy-status-azure-sql-database.md)   
- [Geo-Replication Dynamic Management Views (DMVs) and Functions &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)   
- [sys.dm_geo_replication_link_status](../system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md)