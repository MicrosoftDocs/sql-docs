---
title: "Make Schema Changes on Publication Databases"
description: Replication supports a range of schema changes to published objects. Learn about schema changes that are propagated by default to all SQL Server Subscribers.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "replication [SQL Server], schema changes"
  - "snapshot replication [SQL Server], replicating schema changes"
  - "merge replication [SQL Server replication], replicating schema changes"
  - "transactional replication, replicating schema changes"
  - "schemas [SQL Server replication], replicating changes"
  - "publishing [SQL Server replication], schema changes"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Make schema changes on publication databases
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Replication supports a wide range of schema changes to published objects. When you make any of the following schema changes on the appropriate published object at a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publisher, that change is propagated by default to all [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers:  
  
-   ALTER TABLE  
  
-   `ALTER TABLE SET LOCK ESCALATION` shouldn't be used if schema change replication is enabled and a topology includes [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or [!INCLUDE[ssEWnoversion](../../../includes/ssewnoversion-md.md)] Subscribers.

-   ALTER VIEW  
  
-   ALTER PROCEDURE  
  
-   ALTER FUNCTION  
  
-   ALTER TRIGGER  
  
     `ALTER TRIGGER` can be used only for data manipulation language (DML) triggers because data definition language (DDL) triggers can't be replicated.  
  
> [!IMPORTANT]  
>  You must make schema changes to tables by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO). When you make schema changes in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] attempts to drop and re-create the table. You can't drop published objects, so the schema change fails.  
  
 For transactional replication and merge replication, schema changes are propagated incrementally when the Distribution Agent or Merge Agent runs. For snapshot replication, schema changes are propagated when a new snapshot is applied at the Subscriber. In snapshot replication, a new copy of the schema is sent to the Subscriber each time synchronization occurs. Therefore, all schema changes (not just those listed earlier) to previously published objects are automatically propagated with each synchronization.  
  
 For information about adding and removing articles from publications, see [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).  
  
 **To replicate schema changes**  
  
 The schema changes listed earlier are replicated by default. For information about disabling the replication of schema changes, see [Replicate Schema Changes](../../../relational-databases/replication/publish/replicate-schema-changes.md).  
  
## Considerations for schema changes  
 Keep the following considerations in mind when replicating schema changes.  
  
### General considerations  
  
-   Schema changes are subject to any restrictions imposed by [!INCLUDE[tsql](../../../includes/tsql-md.md)]. For example, `ALTER TABLE` doesn't allow you to alter primary key columns.  
  
-   Data type mapping happens only for the initial snapshot. Schema changes don't map to previous versions of data types. For example, if you use `ALTER TABLE ADD datetime2 column` in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], the data type doesn't translate to **nvarchar** for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] Subscribers. In some cases, schema changes are blocked on the Publisher.  
  
-   If you set a publication to allow the propagation of schema changes, it propagates schema changes regardless of how you set the related schema option for an article in the publication. For example, if you select not to replicate foreign key constraints for a table article, but then issue an `ALTER TABLE` command that adds a foreign key to the table at the Publisher, the foreign key is added to the table at the Subscriber. To prevent this behavior, disable the propagation of schema changes before issuing the `ALTER TABLE` command.  
  
-   Make schema changes only at the Publisher, not at Subscribers (including republishing Subscribers). Merge replication prevents schema changes at the Subscriber. Transactional replication doesn't prevent the changes, but the changes can cause replication to fail.  
  
-   Changes propagated to a republishing Subscriber are by default propagated to its Subscribers.  
  
-   If the schema change references objects or constraints existing on the Publisher but not on the Subscriber, the schema change succeeds on the Publisher but fails on the Subscriber.  
  
-   All objects on the Subscriber that you reference when adding a foreign key must have the same name and owner as the corresponding object on the Publisher.  
  
-   Explicit adding, dropping, or altering indexes isn't replicated. You need to run any change involving an explicit index on each replica set individually. Indexes created implicitly for constraints (such as a primary key constraint) are supported.  
  
-   Altering or dropping identity columns that replication manages isn't supported. For more information about automatic management of identity columns, see [Replicate Identity Columns](../../../relational-databases/replication/publish/replicate-identity-columns.md).  
  
-   Schema changes that include nondeterministic functions aren't supported because they can result in data at the Publisher and Subscriber being different (referred to as non-convergence). For example, if you issue the following command at the Publisher: `ALTER TABLE SalesOrderDetail ADD OrderDate DATETIME DEFAULT GETDATE()`, the values are different when the command is replicated to the Subscriber and executed. For more information about nondeterministic functions, see [Deterministic and Nondeterministic Functions](../../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
-   Name constraints explicitly. If you don't explicitly name a constraint, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] generates a name for the constraint, and these names are different on the Publisher and each Subscriber. This difference can cause issues during the replication of schema changes. For example, if you drop a column at the Publisher and a dependent constraint is dropped, replication attempts to drop the constraint at the Subscriber. The drop at the Subscriber fails because the name of the constraint is different. If synchronization fails because of a constraint naming issue, manually drop the constraint at the Subscriber and then rerun the Merge Agent.  
  
-   If you publish a table for replication, you can't alter a column in that table to a data type of XML if you already generated a publication snapshot. To alter the column, you must first remove replication.  
  
-   Read uncommitted isn't a supported isolation level when doing DDL on a published table.  
  
-   Don't use **SET CONTEXT_INFO** to modify the context of transactions where schema changes are performed against published objects.  
  
#### Adding columns  
  
-   To add a new column to a table and include that column in an existing publication, execute `ALTER TABLE <Table> ADD <Column>`. By default, the column is then replicated to all Subscribers. The column must allow `NULL` values or include a default constraint. For more information about adding columns, see the "Merge Replication" section in this article.  
  
-   To add a new column to a table and not include that column in an existing publication, disable the replication of schema changes, and then execute `ALTER TABLE <Table> ADD <Column>`.  
  
-   To include an existing column in an existing publication, use [sp_articlecolumn (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md), [sp_mergearticlecolumn (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-mergearticlecolumn-transact-sql.md), or the **Publication Properties - \<Publication>** dialog box.  
  
     For more information, see [Define and Modify a Column Filter](../../../relational-databases/replication/publish/define-and-modify-a-column-filter.md). This action requires subscriptions to be reinitialized.  
  
-   Adding an identity column to a published table isn't supported, because it can result in non-convergence when the column is replicated to the Subscriber. The values in the identity column at the Publisher depend on the order in which the rows for the affected table are physically stored. The rows might be stored differently at the Subscriber; therefore the value for the identity column can be different for the same rows.  
  
#### Dropping columns  
  
-   To drop a column from an existing publication and drop the column from the table at the Publisher, execute `ALTER TABLE <Table> DROP <Column>`. By default, the column is then dropped from the table at all Subscribers.  
  
-   To drop a column from an existing publication but retain the column in the table at the Publisher, use [sp_articlecolumn (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md), [sp_mergearticlecolumn (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-mergearticlecolumn-transact-sql.md), or the **Publication Properties - \<Publication>** dialog box.  
  
     For more information, see [Define and Modify a Column Filter](../../../relational-databases/replication/publish/define-and-modify-a-column-filter.md). This action requires generating a new snapshot.  
  
-   You can't use the column to drop in the filter clauses of any article of any publication in the database.  
  
-   When dropping a column from a published article, consider any constraints, indexes, or properties of the column that could affect the database. For example:  
  
    -   You can't drop columns used in a primary key from articles in transactional publications, because replication uses them.  
  
    -   You can't drop the `rowguid` column from articles in merge publications or the `mstran_repl_version` column from articles in transactional publications that support updating subscriptions, because replication uses them.  
  
    -   Index changes aren't propagated to Subscribers. If you drop a column at the Publisher and a dependent index is dropped, the index drop isn't replicated. You should drop the index at the Subscriber before dropping the column at the Publisher, so that the column drop succeeds when it is replicated from the Publisher to the Subscriber. If synchronization fails because of an index at the Subscriber, manually drop the index and then rerun the Merge Agent.  
  
    -   Name constraints explicitly so you can drop them. For more information, see the "General Considerations" section earlier in this article.  
  
### Transactional replication  
  
-   Schema changes are propagated to Subscribers running previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], but the DDL statement should only include syntax supported by the version at the Subscriber.  
  
     If the Subscriber republishes data, the only supported schema changes are adding and dropping a column. Make these changes on the Publisher by using [sp_repladdcolumn (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-repladdcolumn-transact-sql.md) and [sp_repldropcolumn (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-repldropcolumn-transact-sql.md) rather than `ALTER TABLE` DDL syntax.  
  
-   Schema changes aren't replicated to non-SQL Server Subscribers.  
  
-   Schema changes aren't propagated from non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publishers.  
  
-   You can't alter indexed views that are replicated as tables. You can alter indexed views that are replicated as indexed views, but altering them causes them to become regular views rather than indexed views.  
  
-   If the publication supports immediate updating or queued updating subscriptions, quiesce the system before making schema changes: stop all activity on the published table at the Publisher and Subscribers, and propagate pending data changes to all nodes. After the schema changes propagate to all nodes, activity can resume on the published tables.  
  
-   If the publication is in a peer-to-peer topology, quiesce the system before making schema changes. For more information, see [Quiesce a Replication Topology (Replication Transact-SQL Programming)](../../../relational-databases/replication/administration/quiesce-a-replication-topology-replication-transact-sql-programming.md).  
  
-   Adding a timestamp column to a table and mapping the timestamp to `binary(8)` causes the article to be reinitialized for all active subscriptions.  
  
### Merge Replication  
  
-   How merge replication handles schema changes depends on the publication compatibility level, and whether the snapshot is set to native mode (default) or character mode:  
  
    -   To replicate schema changes, set the publication compatibility level to at least 90RTM. If Subscribers use previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or the compatibility level is less than 90RTM, use [sp_repladdcolumn &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-repladdcolumn-transact-sql.md) and [sp_repldropcolumn &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-repldropcolumn-transact-sql.md) to add and drop columns. However, these procedures are deprecated.  
  
    -   If you try to add to an existing article a column with a data type that was introduced in [!INCLUDE[sql2008-md](../../../includes/sql2008-md.md)], [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has the following behavior:  
  
        ||100RTM, native snapshot|100RTM, character snapshot|All other compatibility levels|  
        |-|-----------------------------|--------------------------------|------------------------------------|  
        |**hierarchyid**|Allow change|Block change|Block change|  
        |**geography** and **geometry**|Allow change|Allow change*|Block change|  
        |**filestream**|Allow change|Block change|Block change|  
        |**date**, **time**, **datetime2**, and **datetimeoffset**|Allow change|Allow change*|Block change|  
  
         *SQL Server Compact Subscribers convert these data types at the Subscriber.  
  
-   If an error occurs when applying a schema change (such as an error resulting from adding a foreign key that references a table not available at the Subscriber), synchronization fails and the subscription must be reinitialized.  
  
-   If a schema change is made on a column involved in a join filter or parameterized filter, you must reinitialize all subscriptions and regenerate the snapshot.  
  
-   Merge replication provides stored procedures to skip schema changes during troubleshooting. For more information, see [sp_markpendingschemachange &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-markpendingschemachange-transact-sql.md) and [sp_enumeratependingschemachanges &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-enumeratependingschemachanges-transact-sql.md).  
  
## Related content

- [ALTER TABLE (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md)
- [ALTER VIEW (Transact-SQL)](../../../t-sql/statements/alter-view-transact-sql.md)
- [ALTER PROCEDURE (Transact-SQL)](../../../t-sql/statements/alter-procedure-transact-sql.md)
- [ALTER FUNCTION (Transact-SQL)](../../../t-sql/statements/alter-function-transact-sql.md)
- [ALTER TRIGGER (Transact-SQL)](../../../t-sql/statements/alter-trigger-transact-sql.md)
- [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)
- [Regenerate Custom Transactional Procedures to Reflect Schema Changes](../../../relational-databases/replication/transactional/transactional-articles-regenerate-to-reflect-schema-changes.md)
