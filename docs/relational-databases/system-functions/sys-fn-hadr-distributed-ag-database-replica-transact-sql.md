---
title: "sys.fn_hadr_distributed_ag_database_replica (Transact-SQL)"
description: "sys.fn_hadr_distributed_ag_database_replica (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "06/14/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fn_hadr_distributed_ag_database_replica"
  - "sys.fn_hadr_distributed_ag_database_replica_TSQL"
  - "fn_hadr_distributed_ag_database_replica"
  - "fn_hadr_distributed_ag_database_replica_TSQL"
helpviewer_keywords:
  - "sys.fn_hadr_distributed_ag_database_replica"
dev_langs:
  - "TSQL"
---
# sys.fn_hadr_distributed_ag_database_replica (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Used to  map a database in a distributed availability group to the database in the local availability group.  
   
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_hadr_distributed_ag_database_replica( lag_Id, database_id )  
```  
  
## Arguments  
 '*lag_Id*'  
 Is the identifier of the distributed availability group. *lag_Id* is type **uniqueidentifier**.  
  
 '*database_id*'  
 Is the identifier of the database in a distributed availability group. *database_id* is type **uniqueidentifier**.  
  
## Tables Returned  
 Returns the following information.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_database_id**|**uniqueidentifier**|ID of the database in the local availability group.|  
  
## Examples  
  
### Using sys.fn_hadr_distributed_ag_database_replica  
 The following example passes in the database ID in a distributed availability group. It returns a table with the database ID associated with the local availability group.  
  
```  
DECLARE @lagId uniqueidentifier = '4A03D1A8-4AE6-B153-E7E9-ED22A546008D'  
DECLARE @databaseId uniqueidentifier = '3FFA882A-C4C3-5B9E-A203-8F44BD9659F7'  
  
SELECT * FROM sys.fn_hadr_distributed_ag_database_replica(@lagId, @databaseId)  
GO  
```  
  
## See Also  
 [Always On Availability Groups Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/always-on-availability-groups-functions-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Distributed Availability Groups &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/distributed-availability-groups.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md)  
  
