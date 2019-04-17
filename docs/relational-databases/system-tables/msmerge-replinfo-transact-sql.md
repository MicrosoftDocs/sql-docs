---
title: "MSmerge_replinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_replinfo_TSQL"
  - "MSmerge_replinfo"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_replinfo system table"
ms.assetid: b0924094-c0cc-49c1-869a-65be0d0465a0
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_replinfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_replinfo** table contains one row for each subscription. This table tracks information about subscriptions. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**repid**|**uniqueidentifier**|The unique ID for the replica.|  
|**use_interactive_resolver**|**bit**|Specifies whether the interactive resolver is used during reconciliation.<br /><br /> **0** = Do not use the interactive resolver.<br /><br /> **1** = use the interactive resolver.|  
|**validation_level**|**int**|Type of validation to perform on the subscription. The validation level specified can be one of these values:<br /><br /> **0** = No validation.<br /><br /> **1** = Rowcount-only validation.<br /><br /> **2** = Rowcount and checksum validation.<br /><br /> **3** = Rowcount and binary checksum validation.|  
|**resync_gen**|**bigint**|The generation number that is used for resynchronization of the subscription. A value of **-1** indicates that the subscription is not marked for resynchronization.|  
|**login_name**|**sysname**|The name of the user who created the subscription.|  
|**hostname**|**sysname**|The value that is used by the parameterized row filter when generating the partition for the subscription.|  
|**merge_jobid**|**binary(16)**|The merge job ID for this subscription.|  
|**sync_info**|**int**|Internal-use only.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
