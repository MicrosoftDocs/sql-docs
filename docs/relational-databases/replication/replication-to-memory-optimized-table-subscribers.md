---
description: "Replication to Memory-Optimized Table Subscribers"
title: "Replication to Memory-Optimized Table Subscribers | Microsoft Docs"
ms.custom: ""
ms.date: "11/21/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
ms.assetid: 1a8e6bc7-433e-471d-b646-092dc80a2d1a
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication to Memory-Optimized Table Subscribers
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Tables acting as snapshot and transactional replication subscribers, excluding Peer-to-peer transactional replication, can be configured as memory-optimized tables. Other replication configurations are not compatible with memory-optimized tables. This feature is available beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].  
  
## Two configurations are required  
  
-   **Configure the subscriber database to support replication to memory-optimized tables**  
  
     Set the **\@memory_optimized** property  to **true**, by using [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) or [sp_changesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubscription-transact-sql.md).  
  
-   **Configure the article to support replication to memory-optimized tables**  
  
     Set the `@schema_option = 0x40000000000` option for the article by using [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) or [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md).  
  
#### To configure a memory-optimized table as a subscriber  
  
1.  Create a transactional publication. For more information, see [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md).  
  
2.  Add articles to the publication. For more information, see [Define an Article](../../relational-databases/replication/publish/define-an-article.md).  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the **\@schema_option** parameter of the **sp_addarticle** stored procedure to   
    **0x40000000000**.  
  
3.  In the article properties window set **Enable Memory optimization** to **true**.  
  
4.  Start the Snapshot Agent job to generate the initial snapshot for this publication. For more information, see [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
5.  Now create a new subscription. In the **New Subscription Wizard** set **Memory Optimized Subscription** to **true**.  

 Memory-optimized tables should now start receiving updates from the publisher.  
  
#### Reconfigure an existing transaction replication  
  
1.  Go to subscription properties in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and set **Memory Optimized Subscription** to **true**. The changes are not applied until the subscription is reinitialized.  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the new **\@memory_optimized** parameter of the **sp_addsubscription** stored procedure to true.  
  
2.  Go to the article properties  for a publication in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and set **Enable Memory** optimization to true.  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the **\@schema_option** parameter of the **sp_addarticle** stored procedure to   
    **0x40000000000**.  
  
3.  Memory optimized tables do not support clustered indexes. To have replication handle this by converting it to nonclustered index on the destination, set **Convert clustered index to nonclustered for memory optimized article** to true.  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the **\@schema_option** parameter of the **sp_addarticle** stored procedure to  **0x0000080000000000**.  
  
4.  Regenerate the snapshot.  
  
5.  Reinitialize the Subscription.  
  
## Remarks and Restrictions  
 Only one-way transactional replication is supported. Peer-to-peer transactional replication is not supported.  
  
 Memory-optimized tables cannot be published.  
  
 Replication tables on the distributor cannot be configured as memory-optimized tables.  
  
 Merge replication cannot include memory-optimized tables.  
  
 At the subscriber, tables involved in transactional replication can be configured as memory optimized tables, but the subscriber tables must meet the requirements of memory-optimized tables. This requires the following restrictions.  
 
-   Tables replicated to memory-optimized tables on a subscriber are limited to the data types permitted in memory-optimized tables. For more information, see [Supported Data Types for In-Memory OLTP](../../relational-databases/in-memory-oltp/supported-data-types-for-in-memory-oltp.md).  
  
-   Not all Transact-SQL features are supported with memory-optimized tables. See [Transact-SQL Constructs Not Supported by In-Memory OLTP](../../relational-databases/in-memory-oltp/transact-sql-constructs-not-supported-by-in-memory-oltp.md) for details.  
  
##  <a name="Schema"></a> Modifying a schema file  
  
-   If using the memory-optimized table option `DURABILITY = SCHEMA_AND_DATA` the table must have a nonclustered primary key index.  
  
-   ANSI_PADDING must be ON.  
  
  
  
