---
title: "MSpublisher_databases (Transact-SQL)"
description: MSpublisher_databases (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSpublisher_databases"
  - "MSpublisher_databases_TSQL"
helpviewer_keywords:
  - "MSpublisher_databases system table"
dev_langs:
  - "TSQL"
ms.assetid: 59b0166e-a64c-46b8-befc-c222fa1ccce2
---
# MSpublisher_databases (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSpublisher_databases** table contains one row for each Publisher/Publisher database pair serviced by the local Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_id**|**smallint**|The ID of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**id**|**int**|The ID of the row.|  
|**publisher_engine_edition**|**int**|The edition of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher, which can be one of the following:<br /><br /> **10** = Personal Edition<br /><br /> **11** = Desktop Engine (MSDE)<br /><br /> **20** = Standard<br /><br /> **21** = Workgroup<br /><br /> **30** = Enterprise (Evaluation)<br /><br /> **31** = Developer<br /><br /> **40** = Express (Express cannot be a publisher. This value is present for completeness.)|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
