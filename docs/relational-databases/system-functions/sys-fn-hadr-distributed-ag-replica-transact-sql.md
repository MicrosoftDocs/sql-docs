---
description: "sys.fn_hadr_distributed_ag_replica (Transact-SQL)"
title: "sys.fn_hadr_distributed_ag_replica (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.fn_hadr_distributed_ag_replica"
  - "sys.fn_hadr_distributed_ag_replica_TSQL"
  - "fn_hadr_distributed_ag_replica"
  - "fn_hadr_distributed_ag_replica_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.fn_hadr_distributed_ag_replica"
ms.assetid: a1e5f9cb-c350-4bb4-a04f-7394f6f25d62
author: MikeRayMSFT
ms.author: mikeray
---
# sys.fn_hadr_distributed_ag_replica (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Used to  map a replica in a distributed availability group to the local availability group.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_hadr_distributed_ag_replica( lag_Id, replica_id )  
```  
  
## Arguments  
 '*lag_Id*'  
 Is the identifier of the distributed availability group. *lag_Id* is type **uniqueidentifier**.  
  
 '*replica_id*'  
 Is the identifier of a replica in the distributed availability group. *replica_id* is type **uniqueidentifier**.  
  
## Tables Returned  
 Returns the following information.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_id**|**uniqueidentifier**|Unique identifier (GUID) of the local availability group.|  
  
## Examples  
  
### Using sys.fn_hadr_distributed_ag_replica  
 The following example returns a table with the local availability group identifier  that is associated with the specified distributed availability group and replica.  
  
```  
DECLARE @lagId uniqueidentifier = '4A03D1A8-4AE6-B153-E7E9-ED22A546008D'  
DECLARE @replicaId uniqueidentifier = 'D5517513-04A8-FD82-14C6-E684EC913935'  
  
SELECT * FROM sys.fn_hadr_distributed_ag_replica(@lagId, @replicaId)  
GO  
```  
  
## See Also  
 [Always On Availability Groups Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/always-on-availability-groups-functions-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Distributed Availability Groups &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/distributed-availability-groups.md)  
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md)  
  
