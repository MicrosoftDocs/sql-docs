---
title: "MSmerge_genhistory (Transact-SQL)"
description: MSmerge_genhistory (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_genhistory_TSQL"
  - "MSmerge_genhistory"
helpviewer_keywords:
  - "MSmerge_genhistory system table"
dev_langs:
  - "TSQL"
ms.assetid: 475d08ae-eb8b-49de-afd6-33c96ab8004d
---
# MSmerge_genhistory (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_genhistory** table contains one row for each generation that a Subscriber knows about (within the retention period). It is used to avoid sending common generations during exchanges and to resynchronize Subscribers that are restored from backups. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**guidsrc**|**uniqueidentifier**|The global identifier of the changes identified by generation at the Subscriber.|  
|**pubid**|**uniqueidentifier**|The publication identifier.|  
|**generation**|**bigint**|The generation value.|  
|**art_nick**|**int**|The nickname for the article.|  
|**nicknames**|**varbinary(1001)**|A list of nicknames of other Subscribers that are known to already have this generation. It is used to avoid sending a generation to a Subscriber that has already seen those changes. Nicknames in the nicknames list are maintained in sorted order to make searches more efficient. If there are more nicknames than can fit in this field, they will not benefit from this optimization.|  
|**coldate**|**datetime**|Date when current generation is added to the table.|  
|**genstatus**|**tinyint**|The status of the generation as follows:<br /><br /> **0** = Open.<br /><br /> **1** = Closed.<br /><br /> **2** = Closed and originated at another Subscriber.|  
|**changecount**|**int**|The number of changes reflected in a given generation|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
