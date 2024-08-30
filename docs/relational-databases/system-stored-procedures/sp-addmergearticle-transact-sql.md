---
title: "sp_addmergearticle (Transact-SQL)"
description: Adds an article to an existing merge publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addmergearticle"
  - "sp_addmergearticle_TSQL"
helpviewer_keywords:
  - "sp_addmergearticle"
dev_langs:
  - "TSQL"
---
# sp_addmergearticle (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds an article to an existing merge publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addmergearticle
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    , [ @source_object = ] N'source_object'
    [ , [ @type = ] N'type' ]
    [ , [ @description = ] N'description' ]
    [ , [ @column_tracking = ] N'column_tracking' ]
    [ , [ @status = ] N'status' ]
    [ , [ @pre_creation_cmd = ] N'pre_creation_cmd' ]
    [ , [ @creation_script = ] N'creation_script' ]
    [ , [ @schema_option = ] schema_option ]
    [ , [ @subset_filterclause = ] N'subset_filterclause' ]
    [ , [ @article_resolver = ] N'article_resolver' ]
    [ , [ @resolver_info = ] N'resolver_info' ]
    [ , [ @source_owner = ] N'source_owner' ]
    [ , [ @destination_owner = ] N'destination_owner' ]
    [ , [ @vertical_partition = ] N'vertical_partition' ]
    [ , [ @auto_identity_range = ] N'auto_identity_range' ]
    [ , [ @pub_identity_range = ] pub_identity_range ]
    [ , [ @identity_range = ] identity_range ]
    [ , [ @threshold = ] threshold ]
    [ , [ @verify_resolver_signature = ] verify_resolver_signature ]
    [ , [ @destination_object = ] N'destination_object' ]
    [ , [ @allow_interactive_resolver = ] N'allow_interactive_resolver' ]
    [ , [ @fast_multicol_updateproc = ] N'fast_multicol_updateproc' ]
    [ , [ @check_permissions = ] check_permissions ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @published_in_tran_pub = ] N'published_in_tran_pub' ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
    [ , [ @logical_record_level_conflict_detection = ] N'logical_record_level_conflict_detection' ]
    [ , [ @logical_record_level_conflict_resolution = ] N'logical_record_level_conflict_resolution' ]
    [ , [ @partition_options = ] partition_options ]
    [ , [ @processing_order = ] processing_order ]
    [ , [ @subscriber_upload_options = ] subscriber_upload_options ]
    [ , [ @identityrangemanagementoption = ] N'identityrangemanagementoption' ]
    [ , [ @delete_tracking = ] N'delete_tracking' ]
    [ , [ @compensate_for_errors = ] N'compensate_for_errors' ]
    [ , [ @stream_blob_columns = ] N'stream_blob_columns' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication that contains the article. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. The name must be unique within the publication. *@article* is **sysname**, with no default. *@article* must be on the local computer running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and must conform to the rules for identifiers.

#### [ @source_object = ] N'*source_object*'

The database object to be published. *@source_object* is **sysname**, with no default. For more information about the types of objects that can be published using merge replication, see [Publish Data and Database Objects](../replication/publish/publish-data-and-database-objects.md).

#### [ @type = ] N'*type*'

The type of article. *@type* is **sysname**, with a default of `table`, and can be one of the following values.

| Value | Description |
| --- | --- |
| `table` (default) | Table with schema and data. Replication monitors the table to determine data to be replicated. |
| `func schema only` | Function with schema only. |
| `indexed view schema only` | Indexed view with schema only. |
| `proc schema only` | Stored procedure with schema only. |
| `synonym schema only` | Synonym with schema only. |
| `view schema only` | View with schema only. |

#### [ @description = ] N'*description*'

A description of the article. *@description* is **nvarchar(255)**, with a default of `NULL`.

#### [ @column_tracking = ] N'*column_tracking*'

The setting for column-level tracking. *@column_tracking* is **nvarchar(10)**, with a default of `false`. `false` turns on column tracking. `false` turns off column tracking and leaves conflict detection at the row level. If the table is already published in other merge publications, you must use the same column tracking value used by existing articles based on this table. This parameter is specific to table articles only.

> [!NOTE]  
> If row tracking is used for conflict detection (the default), the base table can include a maximum of 1,024 columns, but columns must be filtered from the article so that a maximum of 246 columns is published. If column tracking is used, the base table can include a maximum of 246 columns.

#### [ @status = ] N'*status*'

The status of the article. *@status* is **nvarchar(10)**, with a default of `unsynced`. If `active`, the initial processing script to publish the table is run. If `unsynced`, the initial processing script to publish the table is run at the next time the Snapshot Agent runs.

#### [ @pre_creation_cmd = ] N'*pre_creation_cmd*'

Specifies what the system is to do if the table exists at the subscriber when applying the snapshot. *@pre_creation_cmd* is **nvarchar(10)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `none` | If the table already exists at the Subscriber, no action is taken. |
| `delete` | Issues a delete based on the WHERE clause in the subset filter. |
| `drop` (default) | Drops the table before re-creating it. Required to support [!INCLUDE [ssEW](../../includes/ssew-md.md)] Subscribers. |
| `truncate` | Truncates the destination table. |

#### [ @creation_script = ] N'*creation_script*'

The path and name of an optional article schema script used to create the article in the subscription database. *@creation_script* is **nvarchar(255)**, with a default of `NULL`.

> [!NOTE]  
> Creation scripts aren't run on [!INCLUDE [ssEW](../../includes/ssew-md.md)] Subscribers.

#### [ @schema_option = ] *schema_option*

A bitmap of the schema generation option for the given article. *@schema_option* is **varbinary(8)**, and can be the [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) product of one or more of these values.

| Value | Description |
| --- | --- |
| `0x00` | Disables scripting by the Snapshot Agent and uses the provided schema precreation script defined in *@creation_script*. |
| `0x01` | Generates the object creation (`CREATE TABLE`, `CREATE PROCEDURE`, and so on). This is the default value for stored procedure articles. |
| `0x10` | Generates a corresponding clustered index. Even if this option isn't set, indexes related to primary keys and `UNIQUE` constraints are generated if they are already defined on a published table. |
| `0x20` | Converts user-defined data types (UDT) to base data types at the Subscriber. This option can't be used when there's a CHECK or DEFAULT constraint on a UDT column, if a UDT column is part of the primary key, or if a computed column references a UDT column. |
| `0x40` | Generates corresponding nonclustered indexes. Even if this option isn't set, indexes related to primary keys and `UNIQUE` constraints are generated if they are already defined on a published table. |
| `0x80` | Replicates `PRIMARY KEY` constraints. Any indexes related to the constraint are also replicated, even if options `0x10` and `0x40` aren't enabled. |
| `0x100` | Replicates user triggers on a table article, if defined. |
| `0x200` | Replicates `FOREIGN KEY` constraints. If the referenced table isn't part of a publication, all `FOREIGN KEY` constraints on a published table aren't replicated. |
| `0x400` | Replicates `CHECK` constraints. |
| `0x800` | Replicates defaults. |
| `0x1000` | Replicates column-level collation. |
| `0x2000` | Replicates extended properties associated with the published article source object. |
| `0x4000` | Replicates `UNIQUE` constraints. Any indexes related to the constraint are also replicated, even if options `0x10` and `0x40` aren't enabled. |
| `0x8000` | This option isn't valid for Publishers running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions. |
| `0x10000` | Replicates `CHECK` constraints as `NOT FOR REPLICATION` so that the constraints aren't enforced during synchronization. |
| `0x20000` | Replicates `FOREIGN KEY` constraints as `NOT FOR REPLICATION` so that the constraints aren't enforced during synchronization. |
| `0x40000` | Replicates filegroups associated with a partitioned table or index. |
| `0x80000` | Replicates the partition scheme for a partitioned table. |
| `0x100000` | Replicates the partition scheme for a partitioned index. |
| `0x200000` | Replicates table statistics. |
| `0x400000` | Replicates default Bindings. |
| `0x800000` | Replicates rule Bindings. |
| `0x1000000` | Replicates the full-text index. |
| `0x2000000` | XML schema collections bound to **xml** columns aren't replicated. |
| `0x4000000` | Replicates indexes on **xml** columns. |
| `0x8000000` | Creates any schemas not already present on the subscriber. |
| `0x10000000` | Converts **xml** columns to **ntext** on the Subscriber. |
| `0x20000000` | Converts large object data types (**nvarchar(max)**, **varchar(max)**, and **varbinary(max)**) introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] to data types that are supported on [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. |
| `0x40000000` | Replicates permissions. |
| `0x80000000` | Attempts to drop dependencies to any objects that aren't part of the publication. |
| `0x100000000` | Use this option to replicate the `FILESTREAM` attribute if it's specified on **varbinary(max)** columns. Don't specify this option if you're replicating tables to [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Subscribers. Replicating tables that have FILESTREAM columns to [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Subscribers isn't supported, regardless of how this schema option is set. See related option `0x800000000`. |
| `0x200000000` | Converts date and time data types (**date**, **time**, **datetimeoffset**, and **datetime2**) introduced in [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] to data types that are supported on earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `0x400000000` | Replicates the compression option for data and indexes. For more information, see [Data compression](../data-compression/data-compression.md). |
| `0x800000000` | Set this option to store FILESTREAM data on its own filegroup at the Subscriber. If this option isn't set, FILESTREAM data is stored on the default filegroup. Replication doesn't create filegroups; therefore, if you set this option, you must create the filegroup before you apply the snapshot at the Subscriber. For more information about how to create objects before you apply the snapshot, see [Execute Scripts Before and After the Snapshot Is Applied](../replication/snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied).<br /><br />See related option `0x100000000`. |
| `0x1000000000` | Converts common language runtime (CLR) user-defined types (UDTs) to **varbinary(max)** so that columns of type UDT can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. |
| `0x2000000000` | Converts the **hierarchyid** data type to **varbinary(max)** so that columns of type **hierarchyid** can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. For more information about how to use **hierarchyid** columns in replicated tables, see [hierarchyid data type method reference](../../t-sql/data-types/hierarchyid-data-type-method-reference.md). |
| `0x4000000000` | Replicates any filtered indexes on the table. For more information about filtered indexes, see [Create filtered indexes](../indexes/create-filtered-indexes.md). |
| `0x8000000000` | Converts the **geography** and **geometry** data types to **varbinary(max)** so that columns of these types can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. |
| `0x10000000000` | Replicates indexes on columns of type **geography** and **geometry**. |

If this value is `NULL`, the system autogenerates a valid schema option for the article. The [Default schema option table](#default-schema-option-table) shows the value that is chosen based upon the article type. Also, not all *@schema_option* values are valid for every type of replication and article type. The [Valid schema option table](#valid-schema-option-table) shows the options that can be specified for a given article type.

> [!NOTE]  
> The *@schema_option* parameter only affects replication options for the initial snapshot. Once the initial schema has been generated by the Snapshot Agent and applied at the Subscriber, the replication of publication schema changes to the Subscriber occur based on schema change replication rules and the *@replicate_ddl* parameter setting specified in [sp_addmergepublication](sp-addmergepublication-transact-sql.md). For more information, see [Make Schema Changes on Publication Databases](../replication/publish/make-schema-changes-on-publication-databases.md).

#### [ @subset_filterclause = ] N'*subset_filterclause*'

A WHERE clause specifying the horizontal filtering of a table article without the word WHERE included. *@subset_filterclause* is **nvarchar(1000)**, with a default of an empty string.

> [!IMPORTANT]  
> For performance reasons, we recommended that you don't apply functions to column names in parameterized row filter clauses, such as `LEFT([MyColumn]) = SUSER_SNAME()`. If you use [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) in a filter clause and override the `HOST_NAME` value, you might have to convert data types by using [CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md). For more information about best practices for this case, see the section "Overriding the HOST_NAME() Value" in [Parameterized Filters - Parameterized Row Filters](../replication/merge/parameterized-filters-parameterized-row-filters.md).

#### [ @article_resolver = ] N'*article_resolver*'

The COM-based resolver used to resolve conflicts on the table article or the .NET Framework assembly invoked to execute custom business logic on the table article. *@article_resolver* is **nvarchar(255)**, with a default of `NULL`. Available values for this parameter are listed in [!INCLUDE [msCoName](../../includes/msconame-md.md)] Custom Resolvers. If the value provided isn't one of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] resolvers, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the specified resolver instead of the system-supplied resolver. Use `sp_enumcustomresolvers` to enumerate the list of available custom resolvers. For more information, see [Execute Business Logic During Merge Synchronization](../replication/merge/execute-business-logic-during-merge-synchronization.md) and [Advanced Merge Replication - Conflict Detection and Resolution](../replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).

#### [ @resolver_info = ] N'*resolver_info*'

Used to specify additional information required by a custom resolver. Some of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Resolvers require a column provided as input to the resolver. *@resolver_info* is **nvarchar(517)**, with a default of `NULL`. For more information, see [Advanced Merge Replication Conflict - COM-Based Resolvers](../replication/merge/advanced-merge-replication-conflict-com-based-resolvers.md).

#### [ @source_owner = ] N'*source_owner*'

The name of the owner of the *@source_object*. *@source_owner* is **sysname**, with a default of `NULL`. If `NULL`, the current user is assumed to be the owner.

#### [ @destination_owner = ] N'*destination_owner*'

The owner of the object in the subscription database, if not `dbo`. *@destination_owner* is **sysname**, with a default of `NULL`. If `NULL`, `dbo` is assumed to be the owner.

#### [ @vertical_partition = ] N'*vertical_partition*'

Enables and disables column filtering on a table article. *@vertical_partition* is **nvarchar(5)**, with a default of `false`.

- `false` indicates there's no vertical filtering and publishes all columns.

- `false` clears all columns except the declared primary key and `ROWGUID` columns. Columns are added by using `sp_mergearticlecolumn`.

#### [ @auto_identity_range = ] N'*auto_identity_range*'

Enables and disables automatic identity range handling for this table article on a publication at the time it's created. *@auto_identity_range* is **nvarchar(5)**, with a default of `NULL`. `false` enables automatic identity range handling, while `false` disables it.

> [!NOTE]  
> [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] You should use *@identityrangemanagementoption* for specifying identity range management options. For more information, see [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md).

#### [ @pub_identity_range = ] *pub_identity_range*

Controls the identity range size allocated to a Subscriber with a server subscription when automatic identity range management is used. This identity range is reserved for a republishing Subscriber to allocate to its own Subscribers. *@pub_identity_range* is **bigint**, with a default of `NULL`. You must specify this parameter if *@identityrangemanagementoption* is `auto` or if *@auto_identity_range* is `false`.

#### [ @identity_range = ] *identity_range*

Controls the identity range size allocated both to the Publisher and to the Subscriber when automatic identity range management is used. *@identity_range* is **bigint**, with a default of `NULL`. You must specify this parameter if *@identityrangemanagementoption* is `auto` or if *@auto_identity_range* is `false`.

> [!NOTE]  
> *@identity_range* controls the identity range size at republishing Subscribers using previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### [ @threshold = ] *threshold*

Percentage value that controls when the Merge Agent assigns a new identity range. When the percentage of values specified in *@threshold* is used, the Merge Agent creates a new identity range. *@threshold* is **int**, with a default of `NULL`. You must specify this parameter if *@identityrangemanagementoption* is `auto` or if *@auto_identity_range* is `false`.

#### [ @verify_resolver_signature = ] *verify_resolver_signature*

Specifies if a digital signature is verified before using a resolver in merge replication. *@verify_resolver_signature* is **int**, with a default of `1`.

- `0` specifies that the signature isn't verified.

- `1` specifies that the signature is verified to see if it's from a trusted source.

#### [ @destination_object = ] N'*destination_object*'

The name of the object in the subscription database. *@destination_object* is **sysname**, with a default value of what is in *@source_object*. This parameter can be specified only if the article is a schema-only article, such as stored procedures, views, and UDFs. If the article specified is a table article, the value in *@source_object* overrides the value in *@destination_object*.

#### [ @allow_interactive_resolver = ] N'*allow_interactive_resolver*'

Enables or disables the use of the Interactive Resolver on an article. *@allow_interactive_resolver* is **nvarchar(5)**, with a default of `false`. `false` enables the use of the Interactive Resolver on the article; `false` disables it.

> [!NOTE]  
> Interactive Resolver isn't supported by [!INCLUDE [ssEW](../../includes/ssew-md.md)] Subscribers.

#### [ @fast_multicol_updateproc = ] N'*fast_multicol_updateproc*'

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @check_permissions = ] *check_permissions*

A bitmap of the table-level permissions that are verified when the Merge Agent applies changes to the Publisher. If the Publisher login/user account used by the merge process doesn't have the correct table permissions, the invalid changes are logged as conflicts. *@check_permissions* is **int**, and can be the [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) product of one or more of the following values.

| Value | Description |
| --- | --- |
| `0x00` (default) | Permissions aren't checked. |
| `0x10` | Checks permissions at the Publisher before insert operations made at a Subscriber can be uploaded. |
| `0x20` | Checks permissions at the Publisher before update operations made at a Subscriber can be uploaded. |
| `0x40` | Checks permissions at the Publisher before delete operations made at a Subscriber can be uploaded. |

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Acknowledges that the action taken by this stored procedure might invalidate an existing snapshot. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that adding an article doesn't cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.

- `1` specifies that adding an article might cause the snapshot to be invalid, and if there are existing subscriptions that require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated. *@force_invalidate_snapshot* is set to `1` when adding an article to a publication with an existing snapshot.

#### [ @published_in_tran_pub = ] N'*published_in_tran_pub*'

Indicates that an article in a merge publication is also published in a transactional publication. *@published_in_tran_pub* is **nvarchar(5)**, with a default of `false`. `false` specifies that the article is also published in a transactional publication.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that adding an article doesn't cause the subscription to be reinitialized. If the stored procedure detects that the change requires existing subscriptions to be reinitialized, an error occurs and no changes are made.

- `1` means that changes to the merge article cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur. *@force_reinit_subscription* is set to `1` when *@subset_filterclause* specifies a parameterized row filter.

#### [ @logical_record_level_conflict_detection = ] N'*logical_record_level_conflict_detection*'

Specifies the level of conflict detection for an article that is a member of a logical record. *@logical_record_level_conflict_detection* is **nvarchar(5)**, with a default of `false`.

- `false` specifies that a conflict is detected if changes are made anywhere in the logical record.

- `false` specifies that the default conflict detection is used as specified by *@column_tracking*. For more information, see [Group Changes to Related Rows with Logical Records](../replication/merge/group-changes-to-related-rows-with-logical-records.md).

> [!NOTE]  
> Because logical records aren't supported by [!INCLUDE [ssEW](../../includes/ssew-md.md)] Subscribers, you must specify a value of `false` for *@logical_record_level_conflict_detection* to support these Subscribers.

#### [ @logical_record_level_conflict_resolution = ] N'*logical_record_level_conflict_resolution*'

Specifies the level of conflict resolution for an article that is a member of a logical record. *@logical_record_level_conflict_resolution* is **nvarchar(5)**, with a default of `false`.

- `false` specifies that the entire winning logical record overwrites the losing logical record.

- `false` specifies that winning rows aren't constrained to the logical record.

If *@logical_record_level_conflict_detection* is `false`, then *@logical_record_level_conflict_resolution* must also be set to `false`. For more information, see [Group Changes to Related Rows with Logical Records](../replication/merge/group-changes-to-related-rows-with-logical-records.md).

> [!NOTE]  
> Because logical records aren't supported by [!INCLUDE [ssEW](../../includes/ssew-md.md)] Subscribers, you must specify a value of `false` for *@logical_record_level_conflict_resolution* to support these Subscribers.

#### [ @partition_options = ] *partition_options*

Defines the way in which data in the article is partitioned, which enables performance optimizations when all rows belong in only one partition or in only one subscription. *@partition_options* is **tinyint**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `0` (default) | The filtering for the article either is static or doesn't yield a unique subset of data for each partition, that is, an "overlapping" partition. |
| `1` | The partitions are overlapping, and data manipulation language (DML) updates made at the Subscriber can't change the partition to which a row belongs. |
| `2` | The filtering for the article yields non-overlapping partitions, but multiple Subscribers can receive the same partition. |
| `3` | The filtering for the article yields non-overlapping partitions that are unique for each subscription. |

> [!NOTE]  
> If the source table for an article is already published in another publication, then the value of *@partition_options* must be the same for both articles.

#### [ @processing_order = ] *processing_order*

Indicates the processing order of articles in a merge publication. *@processing_order* is **int**, with a default of `0`. `0` specifies that the article is unordered, and any other value represents the ordinal value of the processing order for this article. Articles are processed in order from lowest to highest value. If two articles have the same value, processing order is determined by the order of the article nickname in the [sysmergearticles](../system-tables/sysmergearticles-transact-sql.md) system table. For more information, see [Specify Merge Replication properties](../replication/merge/specify-merge-replication-properties.md).

#### [ @subscriber_upload_options = ] *subscriber_upload_options*

Defines restrictions on updates made at a Subscriber with a client subscription. For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../replication/merge/optimize-merge-replication-performance-with-download-only-articles.md). *@subscriber_upload_options* is **tinyint**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `0` (default) | No restrictions. Changes made at the Subscriber are uploaded to the Publisher. |
| `1` | Changes are allowed at the Subscriber, but they aren't uploaded to the Publisher. |
| `2` | Changes aren't allowed at the Subscriber. |

Changing *@subscriber_upload_options* requires the subscription to be reinitialized by calling [sp_reinitmergepullsubscription](sp-reinitmergepullsubscription-transact-sql.md).

> [!NOTE]  
> If the source table for an article is already published in another publication, the value of *@subscriber_upload_options* must be the same for both articles.

#### [ @identityrangemanagementoption = ] N'*identityrangemanagementoption*'

Specifies how identity range management is handled for the article. *@identityrangemanagementoption* is **nvarchar(10)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `none` | Disables identity range management. |
| `manual` | Marks the identity column using NOT FOR REPLICATION to enable manual identity range handling. |
| `auto` | Specifies automatic management of identity ranges. |
| `NULL` (default) | Defaults to `none` when the value of *@auto_identity_range* isn't `true`. |

For backward compatibility, when the value of *@identityrangemanagementoption* is `NULL`, the value of *@auto_identity_range* is checked. However, when the value of *@identityrangemanagementoption* isn't `NULL`, then the value of *@auto_identity_range* is ignored. For more information, see [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md).

#### [ @delete_tracking = ] N'*delete_tracking*'

Indicates whether deletes are replicated. *@delete_tracking* is **nvarchar(5)**, with a default of `true`. `false` indicates that deletes aren't replicated, and `true` indicates that deletes are replicated, which is the usual behavior for merge replication. When *@delete_tracking* is set to `false`, rows deleted at the Subscriber must be manually removed at the Publisher, and rows deleted at the Publisher must be manually removed at the Subscriber.

> [!IMPORTANT]  
> Setting *@delete_tracking* to `false` results in non-convergence. If the source table for an article is already published in another publication, then the value of *@delete_tracking* must be the same for both articles.

> [!NOTE]  
> *@delete_tracking* options can't be set using the **New Publication Wizard** or the **Publication Properties** dialog box.

#### [ @compensate_for_errors = ] N'*compensate_for_errors*'

Indicates if compensating actions are taken when errors are encountered during synchronization. *@compensate_for_errors* is **nvarchar(5)**, with a default of `false`. When set to `true`, changes that can't be applied at a Subscriber or Publisher during synchronization always lead to compensating actions to undo the change; however, one incorrectly configured Subscriber that generates an error can cause changes at other Subscribers and Publishers to be undone. `false` disables these compensating actions, however, the errors are still logged as with compensation and subsequent merges continues to attempt to apply the changes until successful.

> [!IMPORTANT]  
> Although data in the affected rows might appear to be out of convergence, as soon as you address any errors, changes can be applied and data will converge. If the source table for an article is already published in another publication, then the value of *@compensate_for_errors* must be the same for both articles.

#### [ @stream_blob_columns = ] N'*stream_blob_columns*'

Specifies that a data stream optimization be used when replicating binary large object columns. *@stream_blob_columns* is **nvarchar(5)**, with a default of `false`. `true` means that the optimization will be attempted. *@stream_blob_columns* is set to true when FILESTREAM is enabled. This allows replication of FILESTREAM data to perform optimally and reduce memory utilization. To force FILESTREAM table articles to not use blob streaming, use `sp_changemergearticle` to set *@stream_blob_columns* to false.

> [!IMPORTANT]  
> Enabling this memory optimization might reduce the performance of the Merge Agent during synchronization. This option should only be used when replicating columns that contain megabytes of data.

> [!NOTE]  
> Certain merge replication functionalities, such as logical records, can still prevent the stream optimization from being used when replicating binary large objects even with *@stream_blob_columns* set to `true`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addmergearticle` is used in merge replication.

When you publish objects, their definitions are copied to Subscribers. If you're publishing a database object that depends on one or more other objects, you must publish all referenced objects. For example, if you publish a view that depends on a table, you must publish the table also.

If you specify a value of `3` for *@partition_options*:

- There can be only a single subscription for each partition of data in that article. If a second subscription is created in which the filtering criterion of the new subscription resolves to the same partition as the existing subscription, the existing subscription is dropped.

- Metadata is cleaned-up whenever the Merge Agent runs and the partitioned snapshot expires more quickly. When using this option, you should consider enabling subscriber requested partitioned snapshot. For more information, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).

If you add an article with a static horizontal filter, using *@subset_filterclause*, to an existing publication with articles that have parameterized filters, subscriptions must be reinitialized.

When you specify *@processing_order*, we recommend leaving gaps between the article order values, which makes it easier to set new values in the future. For example, if you have three articles, `Article1`, `Article2`, and `Article3`, set *@processing_order* to `10`, `20`, and `30`, rather than `1`, `2`, and `3`. For more information, see [Specify Merge Replication properties](../replication/merge/specify-merge-replication-properties.md).

### Default schema option table

This table describes the default value that the stored procedure sets, if a `NULL` value is specified for *@schema_option*, which depends on article type.

| Article type | Schema option value |
| --- | --- |
| `func schema only` | `0x01` |
| `indexed view schema only` | `0x01` |
| `proc schema only` | `0x01` |
| `table` | `0x0C034FD1` - [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later compatible publications with a native mode snapshot.<br /><br />`0x08034FF1` - [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later compatible publications with a character mode snapshot. |
| `view schema only` | `0x01` |

> [!NOTE]  
> If the publication supports earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the default schema option for `table` is `0x30034FF1`.

### Valid schema option table

The following table describes the allowed values *@schema_option* depending on article type.

| Article type | Schema option values |
| --- | --- |
| `func schema only` | `0x01` and `0x2000` |
| `indexed view schema only` | `0x01`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x1000000`, and `0x200000` |
| `proc schema only` | `0x01` and `0x2000` |
| `table` | All options. |
| `view schema only` | `0x01`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x1000000`, and `0x200000` |

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addmergearticle-trans_1.sql":::

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## Related content

- [Define an Article](../replication/publish/define-an-article.md)
- [Publish Data and Database Objects](../replication/publish/publish-data-and-database-objects.md)
- [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md)
- [sp_changemergearticle (Transact-SQL)](sp-changemergearticle-transact-sql.md)
- [sp_dropmergearticle (Transact-SQL)](sp-dropmergearticle-transact-sql.md)
- [sp_helpmergearticle (Transact-SQL)](sp-helpmergearticle-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
