---
title: Replication to Memory-Optimized Table Subscribers
description: Replication to memory-optimized table Subscribers lets you configure snapshot and transactional Subscriber tables for In-Memory OLTP.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Replication to memory-optimized table Subscribers
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  You can configure tables that act as snapshot and transactional replication subscribers, except for peer-to-peer transactional replication, as memory-optimized tables. Memory-optimized tables aren't compatible with other replication configurations. This feature is available starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].  
  
## Two configurations are required  
  
-   **Configure the subscriber database to support replication to memory-optimized tables**  
  
     Set the **\@memory_optimized** property to **true** by using [sp_addsubscription (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) or [sp_changesubscription (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-changesubscription-transact-sql.md).  
  
-   **Configure the article to support replication to memory-optimized tables**  
  
     Set the `@schema_option = 0x40000000000` option for the article by using [sp_addarticle (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) or [sp_changearticle (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md).  
  
#### To configure a memory-optimized table as a subscriber  
  
1.  Create a transactional publication. For more information, see [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md).  
  
2.  Add articles to the publication. For more information, see [Define an Article](../../relational-databases/replication/publish/define-an-article.md).  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the **\@schema_option** parameter of the **sp_addarticle** stored procedure to   
    **0x40000000000**.  
  
3.  In the article properties window, set **Enable Memory optimization** to **true**.  
  
4.  Start the Snapshot Agent job to generate the initial snapshot for this publication. For more information, see [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
5.  Now create a new subscription. In the **New Subscription Wizard** set **Memory Optimized Subscription** to **true**.  

 Memory-optimized tables should now start receiving updates from the publisher.  
  
#### Reconfigure an existing transaction replication  
  
1.  Go to subscription properties in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and set **Memory Optimized Subscription** to **true**. The changes aren't applied until you reinitialize the subscription.  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the new **\@memory_optimized** parameter of the **sp_addsubscription** stored procedure to true.  
  
2.  Go to the article properties  for a publication in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and set **Enable Memory** optimization to true.  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the **\@schema_option** parameter of the **sp_addarticle** stored procedure to   
    **0x40000000000**.  
  
3.  Memory-optimized tables don't support clustered indexes. To have replication handle this limitation by converting the index to a nonclustered index on the destination, set **Convert clustered index to nonclustered for memory optimized article** to true.  
  
     If configuring by using [!INCLUDE[tsql](../../includes/tsql-md.md)] set the **\@schema_option** parameter of the **sp_addarticle** stored procedure to  **0x0000080000000000**.  
  
4.  Regenerate the snapshot.  
  
5.  Reinitialize the Subscription.  
  
## Remarks and restrictions  
 Only one-way transactional replication is supported. Peer-to-peer transactional replication isn't supported.  
  
 You can't publish memory-optimized tables.  
  
 You can't configure replication tables on the distributor as memory-optimized tables.  
  
 You can't include memory-optimized tables in merge replication.  
  
 At the subscriber, you can configure tables involved in transactional replication as memory-optimized tables, but the subscriber tables must meet the requirements of memory-optimized tables. This requirement imposes the following restrictions:  
 
-   Tables replicated to memory-optimized tables on a subscriber are limited to the data types permitted in memory-optimized tables. For more information, see [Supported Data Types for In-Memory OLTP](../../relational-databases/in-memory-oltp/supported-data-types-for-in-memory-oltp.md).  
  
-   Not all Transact-SQL features are supported with memory-optimized tables. See [Transact-SQL Constructs Not Supported by In-Memory OLTP](../../relational-databases/in-memory-oltp/transact-sql-constructs-not-supported-by-in-memory-oltp.md) for details.  
  
##  <a name="Schema"></a> Modifying a schema file  
  
-   If you use the memory-optimized table option `DURABILITY = SCHEMA_AND_DATA`, the table must have a nonclustered primary key index.  
  
-   `ANSI_PADDING` must be `ON`.  
  
  
  
