---
title: "Make Schema Changes on Publication Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], schema changes"
  - "snapshot replication [SQL Server], replicating schema changes"
  - "merge replication [SQL Server replication], replicating schema changes"
  - "transactional replication, replicating schema changes"
  - "schemas [SQL Server replication], replicating changes"
  - "publishing [SQL Server replication], schema changes"
ms.assetid: 926c88d7-a844-402f-bcb9-db49e5013b69
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Make Schema Changes on Publication Databases
  Replication supports a wide range of schema changes to published objects. When you make any of the following schema changes on the appropriate published object at a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publisher, that change is propagated by default to all [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers:  
  
-   ALTER TABLE  
  
-   ALTER TABLE SET LOCK ESCALATION should not be used if schema change replication is enabled and a topology includes [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or [!INCLUDE[ssEWnoversion](../../../includes/ssewnoversion-md.md)] Subscribers.ALTER VIEW  
  
-   ALTER PROCEDURE  
  
-   ALTER FUNCTION  
  
-   ALTER TRIGGER  
  
     ALTER TRIGGER can be used only for data manipulation language [DML] triggers because data definition language [DDL] triggers cannot be replicated.  
  
> [!IMPORTANT]  
>  Schema changes to tables must be made by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO). When schema changes are made in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] attempts to drop and re-create the table. You cannot drop published objects, therefore the schema change fails.  
  
 For transactional replication and merge replication, schema changes are propagated incrementally when the Distribution Agent or Merge Agent runs. For snapshot replication, schema changes are propagated when a new snapshot is applied at the Subscriber. In snapshot replication, a new copy of the schema is sent to the Subscriber each time synchronization occurs. Therefore, all schema changes (not just those listed above) to previously published objects are automatically propagated with each synchronization.  
  
 For information about adding and removing articles from publications, see [Add Articles to and Drop Articles from Existing Publications](add-articles-to-and-drop-articles-from-existing-publications.md).  
  
 **To replicate schema changes**  
  
 The schema changes listed above are replicated by default. For information about disabling the replication of schema changes, see [Replicate Schema Changes](replicate-schema-changes.md).  
  
## Considerations for Schema Changes  
 Keep the following considerations in mind when replicating schema changes.  
  
### General Considerations  
  
-   Schema changes are subject to any restrictions imposed by [!INCLUDE[tsql](../../../includes/tsql-md.md)]. For example, ALTER TABLE does not allow you to ALTER primary key columns.  
  
-   Data type mapping is performed only for the initial snapshot. Schema changes are not mapped to previous versions of data types. For example, if the statement `ALTER TABLE ADD datetime2 column` is used in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], the data type is not translated to `nvarchar` for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] Subscribers. In some cases, schema changes are blocked on the Publisher.  
  
-   If a publication is set to allow the propagation of schema changes, schema changes are propagated regardless of how the related schema option is set for an article in the publication. For example, if you select not to replicate foreign key constraints for a table article, but then issue an ALTER TABLE command that adds a foreign key to the table at the Publisher, the foreign key is added to the table at the Subscriber. To prevent this, disable the propagation of schema changes before issuing the ALTER TABLE command.  
  
-   Schema changes should be made only at the Publisher, not at Subscribers (including republishing Subscribers). Merge replication prevents schema changes at the Subscriber. Transactional replication does not prevent the changes, but the changes can cause replication to fail.  
  
-   Changes propagated to a republishing Subscriber are by default propagated to its Subscribers.  
  
-   If the schema change references objects or constraints existing on the Publisher but not on the Subscriber, the schema change will succeed on the Publisher but will fail on the Subscriber.  
  
-   All objects on the Subscriber that are referenced when adding a foreign key must have the same name and owner as the corresponding object on the Publisher.  
  
-   Explicitly adding, dropping, or altering indexes is not supported. Indexes created implicitly for constraints (such as a primary key constraint) are supported.  
  
-   Altering or dropping identity columns that are managed by replication is not supported. For more information about automatic management of identity columns, see [Replicate Identity Columns](replicate-identity-columns.md).  
  
-   Schema changes that include nondeterministic functions are not supported because they can result in data at the Publisher and Subscriber being different (referred to as non-convergence). For example, if you issue the following command at the Publisher: `ALTER TABLE SalesOrderDetail ADD OrderDate DATETIME DEFAULT GETDATE()`, the values are different when the command is replicated to the Subscriber and executed. For more information about nondeterministic functions, see [Deterministic and Nondeterministic Functions](../../user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
-   It is recommended that constraints be explicitly named. If a constraint is not explicitly named, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] generates a name for the constraint, and these names will be different on the Publisher and each Subscriber. This can cause issues during the replication of schema changes. For example, if you drop a column at the Publisher and a dependent constraint is dropped, replication will attempt to drop the constraint at the Subscriber. The drop at the Subscriber will fail because the name of the constraint is different. If synchronization fails because of a constraint naming issue, manually drop the constraint at the Subscriber and then rerun the Merge Agent.  
  
-   If a table is published for replication, it is not possible to alter a column in that table to a data type of XML if a publication snapshot has already been generated To alter the column, you must first remove replication.  
  
-   Read uncommitted is not a supported isolation level when doing DDL on a published table.  
  
-   `SET CONTEXT_INFO` should not be used to modify the context of transactions where schema changes are performed against published objects.  
  
#### Adding Columns  
  
-   To add a new column to a table and include that column in an existing publication, execute ALTER TABLE \<Table> ADD \<Column>. By default, the column is then replicated to all Subscribers. The column must allow NULL values or include a default constraint. For more information about adding columns, see the "Merge Replication" section in this topic.  
  
-   To add a new column to a table and not include that column in an existing publication, disable the replication of schema changes, and then execute ALTER TABLE \<Table> ADD \<Column>.  
  
-   To include an existing column in an existing publication, use [sp_articlecolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql), [sp_mergearticlecolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-mergearticlecolumn-transact-sql), or the **Publication Properties - \<Publication>** dialog box.  
  
     For more information, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md). This will require subscriptions to be reinitialized.  
  
-   Adding an identity column to a published table is not supported, because it can result in non-convergence when the column is replicated to the Subscriber. The values in the identity column at the Publisher depend on the order in which the rows for the affected table are physically stored. The rows might be stored differently at the Subscriber; therefore the value for the identity column can be different for the same rows.  
  
#### Dropping Columns  
  
-   To drop a column from an existing publication and drop the column from the table at the Publisher, execute ALTER TABLE \<Table> DROP \<Column>. By default, the column is then dropped from the table at all Subscribers.  
  
-   To drop a column from an existing publication but retain the column in the table at the Publisher, use [sp_articlecolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql), [sp_mergearticlecolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-mergearticlecolumn-transact-sql), or the **Publication Properties - \<Publication>** dialog box.  
  
     For more information, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md). This will require a new snapshot to be generated.  
  
-   The column to be dropped cannot be used in the filter clauses of any article of any publication in the database.  
  
-   When dropping a column from a published article, take into consideration any constraints, indexes, or properties of the column that could affect the database. For example:  
  
    -   You cannot drop columns used in a primary key from articles in transactional publications, because they are used by replication.  
  
    -   You cannot drop the rowguid column from articles in merge publications or the mstran_repl_version column from articles in transactional publications that support updating subscriptions, because they are used by replication.  
  
    -   Index changes are not propagated to Subscribers: if you drop a column at the Publisher and a dependent index is dropped, the index drop is not replicated. You should drop the index at the Subscriber before dropping the column at the Publisher, so that the column drop succeeds when it is replicated from the Publisher to the Subscriber. If synchronization fails because of an index at the Subscriber, manually drop the index and then rerun the Merge Agent.  
  
    -   Constraints should be explicitly named to allow for dropping. For more information, see the "General Considerations" section earlier in this topic.  
  
### Transactional Replication  
  
-   Schema changes are propagated to Subscribers running previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], but the DDL statement should only include syntax supported by the version at the Subscriber.  
  
     If the Subscriber republishes data, the only supported schema changes are adding and dropping a column. These changes should be made on the Publisher using [sp_repladdcolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-repladdcolumn-transact-sql) and [sp_repldropcolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-repldropcolumn-transact-sql) rather than ALTER TABLE DDL syntax.  
  
-   Schema changes are not replicated to non-SQL Server Subscribers.  
  
-   Schema changes are not propagated from non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publishers.  
  
-   You cannot alter indexed views that are replicated as tables. Indexed views that are replicated as indexed views can be altered, but altering them will cause them to become regular views, rather than indexed views.  
  
-   If the publication supports immediate updating or queued updating subscriptions, the system must be quiesced before making schema changes: all activity on the published table must be stopped at the Publisher and Subscribers, and pending data changes must be propagated to all nodes. After the schema changes have propagated to all nodes, activity can resume on the published tables.  
  
-   If the publication is in a peer-to-peer topology, the system must be quiesced before making schema changes. For more information, see [Quiesce a Replication Topology &#40;Replication Transact-SQL Programming&#41;](../administration/quiesce-a-replication-topology-replication-transact-sql-programming.md).  
  
-   Adding a timestamp column to a table and mapping the timestamp to binary(8) causes the article to be reinitialized for all active subscriptions.  
  
### Merge Replication  
  
-   How merge replication handles schema changes is determined by the publication compatibility level, and whether the snapshot is set to native mode (default) or character mode:  
  
    -   To replicate schema changes, the compatibility level of the publication must be at least 90RTM. If Subscribers are running previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or the compatibility level is less than 90RTM you can use [sp_repladdcolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-repladdcolumn-transact-sql) and [sp_repldropcolumn &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-repldropcolumn-transact-sql) to add and drop columns. However, these procedures are deprecated.  
  
    -   If you try to add to an existing article a column with a data type that was introduced in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has the following behavior:  
  
        ||100RTM, native snapshot|100RTM, character snapshot|All other compatibility levels|  
        |-|-----------------------------|--------------------------------|------------------------------------|  
        |`hierarchyid`|Allow change|Block change|Block change|  
        |`geography` and `geometry`|Allow change|Allow change<sup>1</sup>|Block change|  
        |`filestream`|Allow change|Block change|Block change|  
        |`date`, `time`, `datetime2`, and `datetimeoffset`|Allow change|Allow change<sup>1</sup>|Block change|  
  
         <sup>1</sup> SQL Server Compact Subscribers convert these data types at the Subscriber.  
  
-   If an error occurs when applying a schema change (such as an error resulting from adding a foreign key that references a table not available at the Subscriber), synchronization fails and the subscription must be reinitialized.  
  
-   If a schema change is made on a column involved in a join filter or parameterized filter, you must reinitialize all subscriptions and regenerate the snapshot.  
  
-   Merge replication provides stored procedures to skip schema changes during troubleshooting. For more information, see [sp_markpendingschemachange &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-markpendingschemachange-transact-sql) and [sp_enumeratependingschemachanges &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-enumeratependingschemachanges-transact-sql).  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql)   
 [ALTER VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-view-transact-sql)   
 [ALTER PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-procedure-transact-sql)   
 [ALTER FUNCTION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-function-transact-sql)   
 [ALTER TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-trigger-transact-sql)   
 [Publish Data and Database Objects](publish-data-and-database-objects.md)   
 [Regenerate Custom Transactional Procedures to Reflect Schema Changes](../transactional/transactional-articles-regenerate-to-reflect-schema-changes.md)  
  
  
