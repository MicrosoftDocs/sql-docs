---
description: "sys.sp_persistent_version_cleanup (Transact-SQL)"
title: "sys.sp_persistent_version_cleanup (Transact-SQL)"
ms.custom: ""
ms.date: "01/21/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_persistent_version_cleanup"
  - "sys.sp_persistent_version_cleanup"
  - "sys.sp_persistent_version_cleanup_TSQL"
  - "sp_persistent_version_cleanup_TSQL"
dev_langs: 
  - "TSQL"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---

# sys.sp_persistent_version_cleanup (Transact-SQL)
[!INCLUDE [SQL Server 2019, ASDB, ASDBMI ](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

Manually starts persistent version store (PVS) cleanup process, a key element of accelerated database recovery (ADR). This cleaner rolls back uncommitted data in the PVS from aborted transactions. 

It is not typically necessary to start the PVS cleanup process manually using `sys.sp_persistent_version_cleanup`. However in some scenarios, in a known period of rest/recovery after busy OLTP activity, you may want to initiate the PVS cleanup process manually.

Currently the version cleaner is a single threaded background task.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 

## Syntax  
  
```syntaxsql
EXEC sys.sp_persistent_version_cleanup [database_name] [, scan_all_pages] [, clean_option];
```

## Arguments

#### [database_name]

Optional. The name of the database to cleanup. If not provided, uses the current database context. 

#### [scan_all_pages]

Optional. Default is 0. When 1, forces cleanup of all database pages even if not versioned. 

#### [clean_option]

Optional. Possible options determine whether or not to reclaim off-row PVS page:

0 - Default, no option specified
1 - off-row version store without checking individual PVS page contents
2 - off-row version store with each PVS page visited
3 - in-row version store only
4 - internal use only
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  

## Permissions

Requires the **ALTER DATABASE** permission to execute.

## Remarks

The `sys.sp_persistent_version_cleanup` stored procedure is synchronous, meaning that it will not complete until all version information is cleaned up from the current PVS. 

Database Mirroring cannot be set for a database where ADR is enabled or there are still versions in the persisted version store (PVS). If ADR is disabled, run `sys.sp_persistent_version_cleanup` to clean up previous versions still in the PVS.

## Example

To activate the PVS cleanup process manually between workloads or during maintenance windows, use:

```sql
EXEC sys.sp_persistent_version_cleanup [database_name]; 
```

For example,

```sql
EXEC sys.sp_persistent_version_cleanup [WideWorldImporters];
```

## See also

- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
- [Troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshooting.md)
- [Manage accelerated database recovery](../accelerated-database-recovery-management.md)