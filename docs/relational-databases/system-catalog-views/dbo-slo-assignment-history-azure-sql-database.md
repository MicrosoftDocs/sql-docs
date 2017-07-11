---
title: "dbo.slo_assignment_history (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.slo_assignment_history"
  - "slo_assignment_history"
  - "slo_assignment_history_TSQL"
  - "dbo.slo_assignment_history_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dbo.slo_assignment_history"
  - "slo_assignment_history"
ms.assetid: 048a6fb5-2fc2-4d12-a436-4c53ecd413f3
caps.latest.revision: 9
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# dbo.slo_assignment_history (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

    
> [!IMPORTANT]  
>  **This applies only to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]V11.**  
>   
>  This feature is in a preview state. Do not take a dependency on the specific implementation of this feature because the feature might be changed or removed in a future release.  
  
 Returns the history of database SLO assignments in the server, including the following:  
  
-   The history of database SLO assignments in the server.  
  
-   Start and end time of each database SLO change request.  
  
-   Any SLO assignment errors which are recorded in the error_code and error_desc columns.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_name|**sysname**|Name of the database.|  
|database_id|**int**|ID of the database.|  
|create_date|**datetimeoffset(7)**|Date of database creation.|  
|service_objective_name|**sysname**|Name of the Service Level Objective (SLO).|  
|service_objective_id|**uniqueidentifier**|ID of the SLO.|  
|operation_id|**uniqueidentifier**|ID of the operation.|  
|operation_start_time|**datetimeoffset(7)**|Start time of the database SLO change request.|  
|operation_end_time|**datetimeoffset(7)**|End time of the database SLO change request.|  
|error_code|**int**|Error code of the database SLO change request.|  
|error_desc|**nvarchar**|Description of the error in the database SLO change request.|  
  
## Permissions  
 This view is available to all user roles with permissions to connect to the virtual **master** database.  
  
## Examples  
 The following example returns the history of database SLO assignments for a specified database.  
  
```  
SELECT *  
FROM dbo.slo_assignment_history   
WHERE database_name = '<DB NAME>’   
ORDER BY operation_start_time DESC;  
  
```  
  
## See Also  
 [Managing Premium Databases](http://go.microsoft.com/fwlink/?LinkID=311927)  
  
  
