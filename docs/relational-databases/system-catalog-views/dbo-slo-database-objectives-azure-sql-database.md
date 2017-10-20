---
title: "dbo.slo_database_objectives (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "06/10/2016"
ms.prod: 
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.slo_database_objectives"
  - "dbo.slo_database_objectives_TSQL"
  - "slo_database_objectives_TSQL"
  - "slo_database_objectives"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "slo_database_objectives"
  - "dbo.slo_database_objectives"
ms.assetid: a522569d-8cfc-4643-a170-1cd291e61eee
caps.latest.revision: 10
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# dbo.slo_database_objectives (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

    
> [!IMPORTANT]  
>  **This applies only to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]V11.**  
>   
>  For [!INCLUDE[ssSDSfull](../system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md) (on master) for the operation ALTER DATABASE.   
  
 Returns the assignment status of a Service Level Objective (SLO) in a SQL Database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_name|**sysname**|Name of the database.|  
|current_slo|**sysname**|Current SLO of the database.|  
|target_slo|**sysname**|Target SLO of the database as specified in the SLO change request.|  
|state_desc|**nvarchar**|Status of SLO change request: completed or pending.|  
  
## Permissions  
 This view is available to all user roles with permissions to connect to the virtual **master** database.  
  
## Examples  
  
```  
SELECT   
database_name=database_name.name   
    , current_slo=current_slo.name   
    , target_slo=target_slo.name   
    , state_desc=database_slo.state_desc   
FROM slo_database_objectives AS database_slo  
INNER JOIN slo_service_objectives AS current_slo ON database_slo.current_objective_id = current_slo.objective_id  
INNER JOIN slo_service_objectives AS target_slo ON database_slo.configured_objective_id = target_slo.objective_id  
INNER JOIN sys.databases AS database_name  ON database_slo.database_id = database_name.database_id;  
  
```  
  
## See Also  
 [Managing Premium Databases](http://go.microsoft.com/fwlink/?LinkID=311927)  
[sys.dm_operation_status (Azure SQL Database)](../system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md) 
