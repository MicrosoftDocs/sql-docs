---
title: "MSdynamicsnapshotviews (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSdynamicsnapshotviews_TSQL"
  - "MSdynamicsnapshotviews"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSdynamicsnapshotviews system table"
ms.assetid: 4fc1822a-5d6e-4034-a2e2-363210232d3b
author: stevestein
ms.author: sstein
manager: craigg
---
# MSdynamicsnapshotviews (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSdynamicsnapshotviews** table tracks all the temporary filtered data snapshot views created by the snapshot agent, and is used by the system for cleaning up views in the case of an abnormal shutdown of SQL Server Agent or the Snapshot Agent. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**dynamic_snapshot_view_name**|**sysname**|The name of the temporary filtered data snapshot view.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
