---
title: "Clean Up Merge Metadata (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "metadata [SQL Server replication]"
  - "sp_mergemetadataretentioncleanup"
ms.assetid: 9b88baea-b7c6-4e5d-88f9-93d6a0ff0368
caps.latest.revision: 32
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Clean Up Merge Metadata (Replication Transact-SQL Programming)
  Merge replication metadata is cleaned up periodically by the Merge Agent based on the retention setting for the publication. This occurs at the Publisher and Subscriber in the [MSmerge_genhistory](../Topic/MSmerge_genhistory%20\(Transact-SQL\).md), [MSmerge_contents](../Topic/MSmerge_contents%20\(Transact-SQL\).md), [MSmerge_tombstone](../Topic/MSmerge_tombstone%20\(Transact-SQL\).md), [MSmerge_past_partition_mappings](../Topic/MSmerge_past_partition_mappings%20\(Transact-SQL\).md), and [MSmerge_current_partition_mappings](../Topic/MSmerge_current_partition_mappings.md) system tables. You can also programmatically clean up the data in these tables using replication stored procedures.  
  
### To manually clean up merge metadata  
  
1.  At the Publisher on the publication database, execute [sp_mergemetadataretentioncleanup](../Topic/sp_mergemetadataretentioncleanup%20\(Transact-SQL\).md).  
  
2.  (Optional) Note the number of rows removed in step 1 from the [MSmerge_genhistory](../Topic/MSmerge_genhistory%20\(Transact-SQL\).md), [MSmerge_contents](../Topic/MSmerge_contents%20\(Transact-SQL\).md), and [MSmerge_tombstone](../Topic/MSmerge_tombstone%20\(Transact-SQL\).md) system tables, returned respectively in the **@num_genhistory_rows**, **@num_contents_rows**, and **@num_tombstone_rows** output parameters.  
  
3.  Repeat steps 1 and 2 at the Subscriber to clean up metadata on the subscription database.  
  
## See Also  
 [Subscription Expiration and Deactivation](../../../2014/relational-databases/replication/subscription-expiration-and-deactivation.md)  
  
  