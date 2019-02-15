---
title: "MSdynamicsnapshotjobs (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSdynamicsnapshotjobs_TSQL"
  - "MSdynamicsnapshotjobs"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSdynamicsnapshotjobs system table"
ms.assetid: 4f36a325-0e3c-46c4-aeeb-416346cce0bc
author: stevestein
ms.author: sstein
manager: craigg
---
# MSdynamicsnapshotjobs (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSdynamicsnapshotjobs** table tracks the parameterized row filter information applied to generate a filtered data snapshot. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|The ID for the filtered data snapshot job.|  
|**name**|**sysname**|The name of the filtered data snapshot job.|  
|**pubid**|**uniqueidentifier**|The unique identification number for this publication.|  
|**job_id**|**uniqueidentifier**|The ID of the SQL Server Agent job at the Distributor.|  
|**agent_id**|**int**|The ID of the SQL Server Agent.|  
|**dynamic_filter_login**|**sysname**|The value used for evaluating the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function in parameterized row filters defined for the publication.|  
|**dynamic_filter_hostname**|**sysname**|The value used for evaluating the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function in parameterized row filters defined for the publication.|  
|**dynamic_snapshot_location**|**nvarchar(255)**|The path to the folder where the snapshot files will be read from if a filtered data snapshot is used.|  
|**partition_id**|**int**|The ID of the data partition to which the job belongs.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
