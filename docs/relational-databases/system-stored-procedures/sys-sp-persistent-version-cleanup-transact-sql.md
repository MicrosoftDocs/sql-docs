---
description: "The sys.sp_persistent_version_cleanup system stored procedure manually starts persistent version store (PVS) cleanup process, a key element of accelerated database recovery (ADR)."
title: "sys.sp_persistent_version_cleanup (Transact-SQL)"
ms.custom: ""
ms.date: 07/12/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_persistent_version_cleanup"
  - "sys.sp_persistent_version_cleanup"
  - "sys.sp_persistent_version_cleanup_TSQL"
  - "sp_persistent_version_cleanup_TSQL"
dev_langs: 
  - "TSQL"
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---

# sys.sp_persistent_version_cleanup (Transact-SQL)
[!INCLUDE [SQL Server 2019, ASDB, ASDBMI ](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

Manually starts persistent version store (PVS) cleanup process, a key element of accelerated database recovery (ADR). This cleaner rolls back uncommitted data in the PVS from aborted transactions. 

It is not typically necessary to start the PVS cleanup process manually using `sys.sp_persistent_version_cleanup`. However in some scenarios, in a known period of rest/recovery after busy OLTP activity, you may want to initiate the PVS cleanup process manually.

For more information on ADR on Azure SQL, see [Accelerated Database Recovery in Azure SQL](/azure/azure-sql/accelerated-database-recovery).

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 

## Syntax  
  
```syntaxsql
EXEC sys.sp_persistent_version_cleanup [database_name] [, scan_all_pages] [, clean_option];
```

## Arguments

#### [database_name]

Optional. The name of the database to clean up. If not provided, uses the current database context. 

#### [scan_all_pages]

Optional. Default is 0. When 1, forces cleanup of all database pages even if not versioned. 

#### [clean_option]

Optional. Possible options determine whether or not to reclaim off-row PVS page. This reference is not commonly needed and the default value `0` is recommended.

| Value | Description |
|:--|:--|
|0 | Default, no option specified |
|1 | off-row version store without checking individual PVS page contents |
|2 | off-row version store with each PVS page visited |
|3 | in-row version store only |
|4 | internal use only |
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  

## Permissions

Requires the **ALTER DATABASE** permission to execute.

## Remarks

The `sys.sp_persistent_version_cleanup` stored procedure is synchronous, meaning that it will not complete until all version information is cleaned up from the current PVS. 

In SQL Server 2019, the PVS cleanup process only executes for one database at a time. In Azure SQL Database and Azure SQL Managed Instance, and beginning with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], the PVS cleanup process can execute in parallel against multiple databases in the same instance.

If the PVS cleanup process is already running against the desired database, this stored procedure will be blocked and wait for completion before starting another PVS cleanup process. Active, long-running transactions in any database where ADR is enabled can also block cleanup of the PVS. You can monitor the version cleaner task by looking for its process with the following sample query: 

```sql
SELECT * FROM sys.dm_exec_requests
WHERE command LIKE '%PERSISTED_VERSION_CLEANER%';
```

### Limitations

Database Mirroring cannot be set for a database where ADR is enabled or there are still versions in the persisted version store (PVS). If ADR is disabled, run `sys.sp_persistent_version_cleanup` to clean up previous versions still in the PVS.

## Example

To activate the PVS cleanup process manually between workloads or during maintenance windows, use the following sample script:

```sql
EXEC sys.sp_persistent_version_cleanup [database_name]; 
```

For example:

```sql
EXEC sys.sp_persistent_version_cleanup [WideWorldImporters];
```

Or, to assume the current database context:

```sql
USE [WideWorldImporters];
GO
EXEC sys.sp_persistent_version_cleanup;
```

## Next steps

- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
- [Troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshoot.md)
- [Manage accelerated database recovery](../accelerated-database-recovery-management.md)
- [Accelerated Database Recovery in Azure SQL](/azure/azure-sql/accelerated-database-recovery)
