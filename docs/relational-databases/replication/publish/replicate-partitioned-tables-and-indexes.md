---
description: "Replicate Partitioned Tables and Indexes"
title: "Replicate Partitioned Tables and Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "09/10/2015"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "partitioned indexes [SQL Server], replicating"
  - "partitioned tables [SQL Server], replicating"
  - "replication [SQL Server], partitioned tables"
  - "publishing [SQL Server replication], partitioned tables"
  - "transactional replication, partitioned tables"
ms.assetid: c9fa81b1-6c81-4c11-927b-fab16301a8f5
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replicate Partitioned Tables and Indexes
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Partitioning makes large tables or indexes more manageable because partitioning enables you to manage and access subsets of data quickly and efficiently, and maintain the integrity of a data collection at the same time. For more information, see [Partitioned Tables and Indexes](../../../relational-databases/partitions/partitioned-tables-and-indexes.md). Replication supports partitioning by providing a set of properties that specify how partitioned tables and indexes should be treated.  
  
## Article Properties for Transactional and Merge Replication  
 The following table lists the objects that are used to partition data.  
  
|Object|Created by using|  
|------------|----------------------|  
|Partitioned table or index|CREATE TABLE or CREATE INDEX|  
|Partition function|CREATE PARTITION FUNCTION|  
|Partition scheme|CREATE PARTITION SCHEME|  
  
 Partitioning properties are the article schema options that determine whether partitioning objects should be copied to the Subscriber. These schema options can be set in the following ways:  
  
-   In the **Article Properties** page of the New Publication Wizard or the Publication Properties dialog box. To copy the objects listed in the previous table, specify a value of **true** for the properties **Copy table partitioning schemes** and **Copy index partitioning schemes**. For information about how to access the **Article Properties** page, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
-   By using the *schema_option* parameter of one of the following stored procedures:  
  
    -   [sp_addarticle](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) or [sp_changearticle](../../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md) for transactional replication  
  
    -   [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) or [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md) for merge replication  
  
     To copy the objects listed in the previous table, specify the appropriate schema option values. For information about how to specify schema options, see [Specify Schema Options](../../../relational-databases/replication/publish/specify-schema-options.md).  
  
 Replication copies objects to the Subscriber during the initial synchronization. If the partition scheme uses filegroups other than the PRIMARY filegroup, those filegroups must exist on the Subscriber before the initial synchronization.  
  
 After the Subscriber is initialized, data changes are propagated to the Subscriber and applied to the appropriate partitions. However, changes to the partition scheme are not supported. Transactional and merge replication do not support replicating the following commands: ALTER PARTITION FUNCTION, ALTER PARTITION SCHEME, or the REBUILD WITH PARTITION statement of ALTER INDEX. The changes associated with them will not be automatically replicated to the Subscriber. It is the responsibility of the user to make similar changes manually at the Subscriber.  
  
## Replication Support for Partition Switching  
 One of the key benefits of table partitioning is the ability to quickly and efficiently move subsets of data between partitions. Data is moved by using the SWITCH PARTITION command. By default, when a table is enabled for replication, SWITCH PARTITION operations are blocked for the following reasons:  
  
-   If data is moved into or out of a table that exists at the Publisher but does not exist at the Subscriber, the Publisher and Subscriber could become inconsistent with one another. This problem typically occurs when data is moved into or out of a staging table.  
  
-   If the Subscriber has a different definition for the partitioned table than the Publisher, the Distribution Agent will fail when it tries to apply changes at the Subscriber.  
  
Despite these potential issues, partition switching can be enabled for transactional replication. Before you enable partition switching, make sure that all tables that are involved in partition switching exist at the Publisher and Subscriber, and make sure that the table and partition definitions are the same.  
  
When partitions have the exact same partition scheme at the publishers and subscribers, you can turn on *allow_partition_switch* along with *replication_partition_switch*, which will only replicate the partition switch statement to the subscriber. You can also turn on *allow_partition_switch* without replicating the DDL. This is useful in the case where you want to roll old months out of the partition but keep the replicated partition in place for another year for backup purposes at the subscriber.  
  
If you enable partition switching on SQL Server 2008 R2 through the current version, you might also need split and merge operations in near future. Before executing a split or merge operation on a replicated or CDC enabled table ensure that the partition in question does not have any pending replicated commands. You should also ensure that no DML operations are executed on the partition during the split and merge operations. If there are transactions which the log reader or CDC capture job has not processed, or if DML operations are performed on a partition of a replicated or CDC enabled table while a split or merge operation is executed (involving the same partition), it could lead to a processing error (error 608 - No catalog entry found for partition ID) with log reader agent or CDC capture job. In order to correct the error, it might require a reinitialization of the subscription or disabling CDC on that table or database. 

### Unsupported scenarios

The following scenarios are not supported when using replication with partition switching: 

**Peer-to-peer replication**   
Peer-to-peer replication is not supported with partition switching. 

**Use of variables with partition switching**   

Using variables with partition switching on tables published with transactional replication or Change Data Capture (CDC) is not supported for the `ALTER TABLE ... SWITCH TO ... PARTITION ...` statement.

For example, the following partition switching code will not work with CDC enabled on the database, or with TableA participating in a transactional publication: 

```sql
DECLARE @SomeVariable INT = $PARTITION.pf_test(10);
ALTER TABLE dbo.TableA
SWITCH TO dbo.TableB 
PARTITION @SomeVariable;
```

Instead, switch your partition using the partition function directly, such as the following example: 

```sql
ALTER TABLE NonPartitionedTable 
SWITCH TO PartitionedTable PARTITION $PARTITION.pf_test(10);
```


### Enabling Partition Switching  
 The following properties for transactional publications enable users to control the behavior of partition switching in a replicated environment:  
  
-   `@allow_partition_switch`, when set to `true`, SWITCH PARTITION can be executed against the publication database.  
  
-   `@replicate_partition_switch` determines whether the SWITCH PARTITION DDL statement should be replicated to Subscribers. This option is valid only when `@allow_partition_switch` is set to `true`.  
  
 You can set these properties by using [sp_addpublication](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md) when the publication is created, or by using [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md) after the publication is created. As noted earlier, merge replication does not support partition switching. To execute SWITCH PARTITION on a table that is enabled for merge replication, remove the table from the publication.  
  
## See Also  
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
