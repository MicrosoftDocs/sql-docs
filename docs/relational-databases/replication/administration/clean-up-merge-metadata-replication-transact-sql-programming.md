---
title: Clean up Merge Metadata (Replication SP)
description: Merge metadata cleanup in SQL Server replication doesn't have to wait for the Merge Agent. See how to run cleanup manually with stored procedures.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "metadata [SQL Server replication]"
  - "sp_mergemetadataretentioncleanup"
dev_langs:
  - "TSQL"
---
# Clean up merge metadata (Replication Transact-SQL Programming)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The Merge Agent periodically cleans up merge replication metadata based on the retention setting for the publication. This process occurs at the Publisher and Subscriber in the [MSmerge_genhistory](../../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md), [MSmerge_contents](../../../relational-databases/system-tables/msmerge-contents-transact-sql.md), [MSmerge_tombstone](../../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md), [MSmerge_past_partition_mappings](../../../relational-databases/system-tables/msmerge-past-partition-mappings-transact-sql.md), and [MSmerge_current_partition_mappings](../../../relational-databases/system-tables/msmerge-current-partition-mappings.md) system tables. You can also clean up the data in these tables programmatically by using replication stored procedures.  
  
### To manually clean up merge metadata  
  
1.  At the Publisher on the publication database, execute [sp_mergemetadataretentioncleanup](../../../relational-databases/system-stored-procedures/sp-mergemetadataretentioncleanup-transact-sql.md).  
  
1.  (Optional) Note the number of rows removed in step 1 from the [MSmerge_genhistory](../../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md), [MSmerge_contents](../../../relational-databases/system-tables/msmerge-contents-transact-sql.md), and [MSmerge_tombstone](../../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md) system tables, returned respectively in the `@num_genhistory_rows`, `@num_contents_rows`, and `@num_tombstone_rows` output parameters.  
  
1.  Repeat steps 1 and 2 at the Subscriber to clean up metadata on the subscription database.  
  
## Related content

- [Subscription Expiration and Deactivation](../subscription-expiration-and-deactivation.md)
