---
title: "Clean up Merge metadata (Replication SP)"
description: Progrommatically clean up data in the Merge replication tables using replication stored procedures
ms.custom: seo-lt-2019
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "metadata [SQL Server replication]"
  - "sp_mergemetadataretentioncleanup"
ms.assetid: 9b88baea-b7c6-4e5d-88f9-93d6a0ff0368
author: "MashaMSFT"
ms.author: "mathoma"
---
# Clean Up Merge Metadata (Replication Transact-SQL Programming)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Merge replication metadata is cleaned up periodically by the Merge Agent based on the retention setting for the publication. This occurs at the Publisher and Subscriber in the [MSmerge_genhistory](../../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md), [MSmerge_contents](../../../relational-databases/system-tables/msmerge-contents-transact-sql.md), [MSmerge_tombstone](../../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md), [MSmerge_past_partition_mappings](../../../relational-databases/system-tables/msmerge-past-partition-mappings-transact-sql.md), and [MSmerge_current_partition_mappings](../../../relational-databases/system-tables/msmerge-current-partition-mappings.md) system tables. You can also programmatically clean up the data in these tables using replication stored procedures.  
  
### To manually clean up merge metadata  
  
1.  At the Publisher on the publication database, execute [sp_mergemetadataretentioncleanup](../../../relational-databases/system-stored-procedures/sp-mergemetadataretentioncleanup-transact-sql.md).  
  
2.  (Optional) Note the number of rows removed in step 1 from the [MSmerge_genhistory](../../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md), [MSmerge_contents](../../../relational-databases/system-tables/msmerge-contents-transact-sql.md), and [MSmerge_tombstone](../../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md) system tables, returned respectively in the `@num_genhistory_rows`, `@num_contents_rows`, and `@num_tombstone_rows` output parameters.  
  
3.  Repeat steps 1 and 2 at the Subscriber to clean up metadata on the subscription database.  
  
## See Also  
 [Subscription Expiration and Deactivation](../../../relational-databases/replication/subscription-expiration-and-deactivation.md)  
  
  
