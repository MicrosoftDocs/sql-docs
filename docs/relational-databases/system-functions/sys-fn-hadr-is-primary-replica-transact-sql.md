---
title: "sys.fn_hadr_is_primary_replica (Transact-SQL)"
description: "sys.fn_hadr_is_primary_replica (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fn_hadr_is_primary_replica"
  - "fn_hadr_is_primary_replica_TSQL"
  - "fn_hadr_is_primary_replica"
  - "sys.fn_hadr_is_primary_replica_TSQL"
helpviewer_keywords:
  - "fn_hadr_is_primary_replica"
  - "sys.fn_hadr_is_primary_replica"
dev_langs:
  - "TSQL"
---
# sys.fn_hadr_is_primary_replica (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Used to determine if the current replica is the primary replica.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sys.fn_hadr_is_primary_replica ( 'dbname' )  
```  
  
## Arguments  
 '*dbname*'  
 Is the name of the database. *dbname* is type sysname.  
  
## Returns  
 Returns data type **bit**: 1 if the database on the current instance is the primary replica, otherwise 0.  
 Returns `NULL` if the database doesn't exist, or isn't part of an availability group.
  
## Remarks  
 Use this function to conveniently determine whether the local instance is hosting the primary replica of the specified availability database. Sample code could be similar to the following.  
  
```sql
If sys.fn_hadr_is_primary_replica ( @dbname ) <> 1   
BEGIN  
-- If this is not the primary replica, exit (probably without error).  
END  
-- If this is the primary replica, continue to do the backup.  
```  
  
## Examples  
  
### A. Using sys.fn_hadr_is_primary_replica  
 The following example returns 1 if the specified database on the local instance is the primary replica.  
  
```sql
SELECT sys.fn_hadr_is_primary_replica ('TestDB');  
GO  
```    
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Related content

- [Always On Availability Groups Functions (Transact-SQL)](always-on-availability-groups-functions-transact-sql.md)
- [sys.dm_hadr_database_replica_states (Transact-SQL)](../system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [CREATE AVAILABILITY GROUP (Transact-SQL)](../../t-sql/statements/create-availability-group-transact-sql.md)
- [ALTER AVAILABILITY GROUP (Transact-SQL)](../../t-sql/statements/alter-availability-group-transact-sql.md)
- [Always On Availability Groups Catalog Views (Transact-SQL)](../system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)
