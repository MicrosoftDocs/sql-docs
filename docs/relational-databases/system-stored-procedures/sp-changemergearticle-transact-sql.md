---
title: "sp_changemergearticle (Transact-SQL)"
description: "Changes the properties of a merge article. This stored procedure is executed at the Publisher on the publication database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changemergearticle_TSQL"
  - "sp_changemergearticle"
helpviewer_keywords:
  - "sp_changemergearticle"
dev_langs:
  - "TSQL"
---
# sp_changemergearticle (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the properties of a merge article. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changemergearticle
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @property = ] N'property' ]
    [ , [ @value = ] N'value' ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication in which the article exists. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article to change. *@article* is **sysname**, with no default.

#### [ @property = ] N'*property*'

The property to change for the given article and publication. *@property* is **sysname**, and can be one of the values listed in the following table.

#### [ @value = ] N'*value*'

The new value for the specified property. *@value* is **nvarchar(2000)**, and can be one of the values listed in the following table.

This table describes the properties of articles and the values for those properties.

| Property | Values | Description |
| --- | --- | --- |
| `allow_interactive_resolver` | `true` | Enables the use of an interactive resolver for the article. |
| | `false` | Disables the use of an interactive resolver for the article. |
| `article_resolver` | | Custom resolver for the article. Applies only to a table article. |
| `check_permissions` (bitmap) | `0x00` | Table-level permissions aren't checked. |
| | `0x10` | Table-level permissions are checked at the Publisher before INSERT statements made at the Subscriber are applied at the Publisher. |
| | `0x20` | Table-level permissions are checked at the Publisher before UPDATE statements made at the Subscriber are applied at the Publisher. |
| | `0x40` | Table-level permissions are checked at the Publisher before DELETE statements at the Subscriber are applied at the Publisher. |
| `column_tracking` | `true` | Turns on column level tracking. Applies only to a table article.<br /><br />**Note:** Column level tracking can't be used when publishing tables with more than 246 columns. |
| | `false` | Turns off column level tracking and leaves conflict detection at the row level. Applies only to a table article. |
| `compensate_for_errors` | `true` | Compensating actions are performed when errors occur during synchronization. For more information, see [sp_addmergearticle](sp-addmergearticle-transact-sql.md). |
| | `false` | Compensating actions aren't performed, which is the default behavior. For more information, see [sp_addmergearticle](sp-addmergearticle-transact-sql.md).<br /><br />**Important:** Although data in the affected rows might appear to be out of convergence, as soon as you address any errors, changes can be applied, and data converges. If the source table for an article is already published in another publication, then the value of `compensate_for_errors` must be the same for both articles. |
| `creation_script` | | Path and name of an optional article schema script used to create the article in the subscription database. |
| `delete_tracking` | `true` | DELETE statements are replicated, which is the default behavior. |
| | `false` | DELETE statements aren't replicated.<br /><br />**Important:** Setting `delete_tracking` to `false` results in non-convergence, and deleted rows need to be manually removed. |
| `description` | | Descriptive entry for the article. |
| `destination_owner` | | Name of the owner of the object in the subscription database, if not **dbo**. |
| `identity_range` | | **bigint** that specifies the range size to use when assigning new identity values if the article has `identityrangemanagementoption` set to `auto` or `auto_identity_range` set to `true`. Applies to a table article only. For more information, see the "Merge Replication" section of [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md). |
| `identityrangemanagementoption` | `manual` | Disables automatic identity range management. Marks identity columns using NOT FOR REPLICATION to enable manual identity range handling. For more information, see [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md). |
| | `none` | Disables all identity range management. |
| `logical_record_level_conflict_detection` | `true` | A conflict is detected if changes are made anywhere in the logical record. Requires that `logical_record_level_conflict_resolution` is set to `true`. |
| | `false` | Default conflict detection is used as specified by `column_tracking`. |
| `logical_record_level_conflict_resolution` | `true` | Entire winning logical record overwrites the losing logical record. |
| | `false` | Winning rows aren't constrained to the logical record. |
| `partition_options` | `0` | The filtering for the article either is static or doesn't yield a unique subset of data for each partition, that is, an "overlapping" partition. |
| | `1` | The partitions are overlapping, and DML updates made at the Subscriber can't change the partition to which a row belongs. |
| | `2` | The filtering for the article yields non-overlapping partitions, but multiple Subscribers can receive the same partition. |
| | `3` | The filtering for the article yields non-overlapping partitions that are unique for each subscription.<br /><br />Note: If you specify a value of `3` for `partition_options`, there can be only a single subscription for each partition of data in that article. If a second subscription is created, in which the filtering criterion of the new subscription resolves to the same partition as the existing subscription, the existing subscription is dropped. |
| `pre_creation_command` | `none` | If the table already exists at the Subscriber, no action is taken. |
| | `delete` | Issues a delete based on the WHERE clause in the subset filter. |
| | `drop` | Drops the table before re-creating it. |
| | `truncate` | Truncates the destination table. |
| `processing_order` | | **int** that indicates the processing order of articles in a merge publication. |
| `pub_identity_range` | | **bigint** that specifies the range size allocated to a Subscriber with a server subscription if the article has `identityrangemanagementoption` set to `auto` or `auto_identity_range` set to `true`. This identity range is reserved for a republishing Subscriber to allocate to its own Subscribers. Applies to a table article only. For more information, see the "Merge Replication" section of [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md). |
| `published_in_tran_pub` | `true` | Article is also published in a transactional publication. |
| | `false` | Article isn't also published in a transactional publication. |
| `resolver_info` | | Is used to specify additional information required by a custom resolver. Some of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Resolvers require a column provided as input to the resolver. `resolver_info` is **nvarchar(255)**, with a default of `NULL`. For more information, see [Advanced Merge Replication Conflict - COM-Based Resolvers](../replication/merge/advanced-merge-replication-conflict-com-based-resolvers.md). |
| `schema_option` (bitmap) | | For more information, see the Remarks section later in this article. |
| | `0x00` | Disables scripting by the Snapshot Agent and uses the script provided in `creation_script`. |
| | `0x01` | Generates the object creation script (CREATE TABLE, CREATE PROCEDURE, and so on). |
| | `0x10` | Generates a corresponding clustered index. |
| | `0x20` | Converts user-defined data types to base data types at the Subscriber. This option can't be used when there's a CHECK or DEFAULT constraint on a user-defined type (UDT) column, if a UDT column is part of the primary key, or if a computed column references a UDT column. |
| | `0x40` | Generates corresponding nonclustered indexes. |
| | `0x80` | Includes declared referential integrity on the primary keys. |
| | `0x100` | Replicates user triggers on a table article, if defined. |
| | `0x200` | Replicates FOREIGN KEY constraints. If the referenced table isn't part of a publication, all FOREIGN KEY constraints on a published table aren't replicated. |
| | `0x400` | Replicates CHECK constraints. |
| | `0x800` | Replicates defaults. |
| | `0x1000` | Replicates column-level collation. |
| | `0x2000` | Replicates extended properties associated with the published article source object. |
| | `0x4000` | Replicates unique keys if defined on a table article. |
| | `0x8000` | Generates ALTER TABLE statements when scripting constraints. |
| | `0x10000` | Replicates CHECK constraints as NOT FOR REPLICATION so that the constraints aren't enforced during synchronization. |
| | `0x20000` | Replicates FOREIGN KEY constraints as NOT FOR REPLICATION so that the constraints aren't enforced during synchronization. |
| | `0x40000` | Replicates filegroups associated with a partitioned table or index. |
| | `0x80000` | Replicates the partition scheme for a partitioned table. |
| | `0x100000` | Replicates the partition scheme for a partitioned index. |
| | `0x200000` | Replicates table statistics. |
| | `0x400000` | Replicates default Bindings |
| | `0x800000` | Replicates rule Bindings |
| | `0x1000000` | Replicates the full-text index |
| | `0x2000000` | XML schema collections bound to **xml** columns aren't replicated. |
| | `0x4000000` | Replicates indexes on **xml** columns. |
| | `0x8000000` | Create any schemas not already present on the subscriber. |
| | `0x10000000` | Converts **xml** columns to **ntext** on the Subscriber. |
| | `0x20000000` | Converts large object data types (**nvarchar(max)**, **varchar(max)**, and **varbinary(max)**) that were introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] to data types that are supported on [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. |
| | `0x40000000` | Replicate permissions. |
| | `0x80000000` | Attempt to drop dependencies to any objects that aren't part of the publication. |
| | `0x100000000` | Use this option to replicate the FILESTREAM attribute, if it's specified on **varbinary(max)** columns. Don't specify this option if you're replicating tables to [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Subscribers. Replicating tables that have FILESTREAM columns to [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Subscribers isn't supported, regardless of how this schema option is set. See related option `0x800000000`. |
| | `0x200000000` | Converts date and time data types (**date**, **time**, **datetimeoffset**, and **datetime2**) that are introduced in [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] to data types that are supported on earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| | `0x400000000` | Replicates the compression option for data and indexes. For more information, see [Data compression](../data-compression/data-compression.md). |
| | `0x800000000` | Set this option to store FILESTREAM data on its own filegroup at the Subscriber. If this option isn't set, FILESTREAM data is stored on the default filegroup. Replication doesn't create filegroups; therefore, if you set this option, you must create the filegroup before you apply the snapshot at the Subscriber. For more information about how to create objects before you apply the snapshot, see [Execute Scripts Before and After the Snapshot Is Applied](../replication/snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied).<br /><br />See related option `0x100000000`. |
| | `0x1000000000` | Converts common language runtime (CLR) user-defined types (UDTs) to **varbinary(max)** so that columns of type UDT can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. |
| | `0x2000000000` | Converts the **hierarchyid** data type to **varbinary(max)** so that columns of type **hierarchyid** can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. For more information about how to use **hierarchyid** columns in replicated tables, see [hierarchyid data type method reference](../../t-sql/data-types/hierarchyid-data-type-method-reference.md). |
| | `0x4000000000` | Replicates any filtered indexes on the table. For more information about filtered indexes, see [Create filtered indexes](../indexes/create-filtered-indexes.md). |
| | `0x8000000000` | Converts the **geography** and **geometry** data types to **varbinary(max)** so that columns of these types can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. |
| | `0x10000000000` | Replicates indexes on columns of type **geography** and **geometry**. |
| | `NULL` | System autogenerates a valid schema option for the article. |
| `status` | `active` | Initial processing script to publish the table is run. |
| | `unsynced` | The initial processing script to publish the table is run the next time the Snapshot Agent runs. |
| `stream_blob_columns` | `true` | A data stream optimization is used when replicating binary large object columns. However, certain merge replication functionalities, such as logical records, can still prevent the stream optimization from being used. `stream_blob_columns` is set to true when FILESTREAM is enabled. This allows replication of FILESTREAM data to perform optimally and reduce memory utilization. To force FILESTREAM table articles to not use blob streaming, set `stream_blob_columns` to false.<br /><br />**Important:** Enabling this memory optimization might hurt the performance of the Merge Agent during synchronization. This option should only be used when replicating columns that contain megabytes of data. |
| | `false` | The optimization isn't used when replicating binary large object columns. |
| `subscriber_upload_options` | `0` | No restrictions on updates made at a Subscriber with a client subscription; changes are uploaded to the Publisher. Changing this property might require that existing Subscribers be reinitialized. |
| | `1` | Changes are allowed at a Subscriber with a client subscription, but they aren't uploaded to the Publisher. |
| | `2` | Changes aren't allowed at a Subscriber with a client subscription. |
| `subset_filterclause` | | WHERE clause specifying the horizontal filtering. Applies only to a table article.<br /><br />**Important:** For performance reasons, we recommended that you don't apply functions to column names in parameterized row filter clauses, such as `LEFT([MyColumn]) = SUSER_SNAME()`. If you use [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) in a filter clause and override the HOST_NAME value, you might have to convert data types by using [CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md). For more information about best practices for this case, see the section "Overriding the HOST_NAME() Value" in [Parameterized Filters - Parameterized Row Filters](../replication/merge/parameterized-filters-parameterized-row-filters.md). |
| `threshold` | | Percentage value used for Subscribers running [!INCLUDE [ssEW](../../includes/ssew-md.md)] or earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. `threshold` controls when the Merge Agent assigns a new identity range. When the percentage of values specified in threshold is used, the Merge Agent creates a new identity range. Used when `identityrangemanagementoption` is set to `auto` or `auto_identity_range` is set to `true`. Applies to a table article only. For more information, see the "Merge Replication" section of [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md). |
| `verify_resolver_signature` | `1` | Digital signature on a custom resolver is verified to determine if it's from a trusted source. |
| | `0` | Digital signature on a custom resolver isn't verified to determine if it's from a trusted source. |
| `NULL` (default) | | Returns the list of supported values for *@property*. |

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Acknowledges that the action taken by this stored procedure might invalidate an existing snapshot. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.

- `1` means that changes to the merge article might cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.

See the Remarks section for the properties that, when changed, require the generation of a new snapshot.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the subscription to be reinitialized. If the stored procedure detects that the change would require existing subscriptions to be reinitialized, an error occurs and no changes are made.

- `1` means that changes to the merge article cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.

See the [Remarks](#remarks) section for the properties that, when changed, require that all existing subscriptions be reinitialized.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changemergearticle` is used in merge replication.

Because `sp_changemergearticle` is used to change article properties that were initially specified by using [sp_addmergearticle](sp-addmergearticle-transact-sql.md), refer to [sp_addmergearticle](sp-addmergearticle-transact-sql.md) for more information about these properties.

Changing the following properties requires that a new snapshot is generated, and you must specify a value of `1` for the *@force_invalidate_snapshot* parameter:

- `check_permissions`
- `column_tracking`
- `destination_owner`
- `pre_creation_command`
- `schema_options`
- `subset_filterclause`

Changing the following properties requires that existing subscriptions is reinitialized, and you must specify a value of `1` for the *@force_reinit_subscription* parameter:

- `check_permissions`
- `column_tracking`
- `destination_owner`
- `pre_creation_command`
- `identityrangemanagementoption`
- `subscriber_upload_options`
- `subset_filterclause`
- `creation_script`
- `schema_option`
- `logical_record_level_conflict_detection`
- `logical_record_level_conflict_resolution`

When you specify a value of `3` for `partition_options`, metadata is cleaned up whenever the Merge Agent runs and the partitioned snapshot expires more quickly. When using this option, you should consider enabling subscriber requested partitioned snapshot. For more information, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).

When setting the `column_tracking` property, if the table is already published in other merge publications, the column tracking must be the same as the value being used by existing articles based on this table. This parameter is specific to table articles only.

If multiple publications publish articles based on the same underlying table, changing the `delete_tracking` property or the `compensate_for_errors` property for one article causes the same change to be made to the other articles that are based on the same table.

If the Publisher login/user account used by the merge process doesn't have the correct table permissions, the invalid changes are logged as conflicts.

When you change the value of `schema_option`, the system doesn't perform a bitwise update. This means that when you set `schema_option` using `sp_changemergearticle`, existing bit settings might be turned off. To retain the existing settings, you should perform [& (Bitwise AND)](../../t-sql/language-elements/bitwise-and-transact-sql.md) between the value that you're setting and the current value of `schema_option`, which can be determined by executing [sp_helpmergearticle](sp-helpmergearticle-transact-sql.md).

> [!CAUTION]  
> When you many (perhaps hundreds) of articles in a publication and you execute `sp_changemergearticle` for one of the articles, it might take a long time to finish execution.

## Valid schema option table

The following table describes the allowed `schema_option` values, depending on article type.

| Article type | Schema option values |
| --- | --- |
| `func schema only` | `0x01` and `0x2000` |
| `indexed view schema only` | `0x01`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x1000000`, and `0x200000` |
| `proc schema only` | `0x01` and `0x2000` |
| `table` | All options. |
| `view schema only` | `0x01`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x1000000`, and `0x200000` |

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-changemergearticle-tr_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_changemergearticle`.

## Related content

- [View and Modify Article Properties](../replication/publish/view-and-modify-article-properties.md)
- [Change Publication and Article Properties](../replication/publish/change-publication-and-article-properties.md)
- [sp_addmergearticle (Transact-SQL)](sp-addmergearticle-transact-sql.md)
- [sp_dropmergearticle (Transact-SQL)](sp-dropmergearticle-transact-sql.md)
- [sp_helpmergearticle (Transact-SQL)](sp-helpmergearticle-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
