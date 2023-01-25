---
title: "MSdynamicsnapshotjobs (Transact-SQL)"
description: MSdynamicsnapshotjobs (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSdynamicsnapshotjobs_TSQL"
  - "MSdynamicsnapshotjobs"
helpviewer_keywords:
  - "MSdynamicsnapshotjobs system table"
dev_langs:
  - "TSQL"
ms.assetid: 4f36a325-0e3c-46c4-aeeb-416346cce0bc
---
# MSdynamicsnapshotjobs (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
