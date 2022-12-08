---
title: "MSpublicationthresholds (Transact-SQL)"
description: MSpublicationthresholds (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "mspublicationthresholds"
  - "mspublicationthresholds_TSQL"
helpviewer_keywords:
  - "MSpublicationthresholds system table"
dev_langs:
  - "TSQL"
ms.assetid: 9da3879f-b1f4-4ab4-abd4-a9a8ac395eba
---
# MSpublicationthresholds (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSpublicationthresholds** table is used to track replication performance metrics for a publication, with one row for each threshold value being monitored. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publication_id**|**int**|Identifies the publication for which a threshold has been set.|  
|**metric_id**|**int**|Identifies a replication performance metric being monitored as defined in the [MSreplmonthresholdmetrics](../../relational-databases/system-tables/msreplmonthresholdmetrics-transact-sql.md) system table.|  
|**value**|**sql_variant**|The threshold value of the metric being monitored.|  
|**shouldalert**|**bit**|A value of **1** indicates that an alert should be generated when the metric exceeds the defined threshold.|  
|**isenabled**|**bit**|A value of **1** indicates that monitoring is enabled for this replication performance metric.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
