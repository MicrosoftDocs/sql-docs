---
title: "sys.fn_hadr_distributed_ag_database_replica (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.fn_hadr_distributed_ag_database_replica"
  - "sys.fn_hadr_distributed_ag_database_replica_TSQL"
  - "fn_hadr_distributed_ag_database_replica"
  - "fn_hadr_distributed_ag_database_replica_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.fn_hadr_distributed_ag_database_replica"
ms.assetid: 0e6202a1-e872-4f53-99d7-c16b6f712efc
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.fn_hadr_distributed_ag_database_replica (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Used to  map a database in a distributed availability group to the database in the local availability group.  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
 [Distributed Availability Groups &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/distributed-availability-groups-always-on-availability-groups.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md)  
  
  
