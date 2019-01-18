---
title: "sys.fn_hadr_is_primary_replica (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.fn_hadr_is_primary_replica"
  - "fn_hadr_is_primary_replica_TSQL"
  - "fn_hadr_is_primary_replica"
  - "sys.fn_hadr_is_primary_replica_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_hadr_is_primary_replica"
  - "sys.fn_hadr_is_primary_replica"
ms.assetid: c9b1969f-be1d-4dfb-a33d-551f380b9e27
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.fn_hadr_is_primary_replica (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

  Used to determine if the current replica is the primary replica.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_hadr_is_primary_replica ( 'dbname' )  
```  
  
## Arguments  
 '*dbname*'  
 Is the name of the database. *dbname* is type sysname.  
  
## Returns  
 Returns 1 if the database on the current instance is the primary replica. Otherwise returns 0.  
  
## Remarks  
 Use this function to conveniently determine whether the local instance is hosting the primary replica of the specified availability database. Sample code could be similar to the following.  
  
```  
If sys.fn_hadr_is_primary_replica ( @dbname ) <> 1   
BEGIN  
-- If this is not the primary replica, exit (probably without error).  
END  
-- If this is the primary replica, continue to do the backup.  
  
```  
  
## Examples  
  
### A. Using sys.fn_hadr_is_primary_replica  
 The following example returns 1 if the specified database on the local instance is the primary replica.  
  
```  
SELECT sys.fn_hadr_is_primary_replica ('TestDB');  
GO  
```  
  
## See Also  
 [AlwaysOn Availability Groups Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/always-on-availability-groups-functions-transact-sql.md)   
 [AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md)   
 [AlwaysOn Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)  
  
  
