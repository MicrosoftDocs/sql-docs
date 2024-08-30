---
title: "sp_addarticle (Transact-SQL)"
description: Creates an article and adds it to a publication. This stored procedure runs at the Publisher on the publication database.
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addarticle"
  - "sp_addarticle_TSQL"
helpviewer_keywords:
  - "sp_addarticle"
dev_langs:
  - "TSQL"
---
# sp_addarticle (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates an article and adds it to a publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addarticle
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @source_table = ] N'source_table' ]
    [ , [ @destination_table = ] N'destination_table' ]
    [ , [ @vertical_partition = ] N'vertical_partition' ]
    [ , [ @type = ] N'type' ]
    [ , [ @filter = ] N'filter' ]
    [ , [ @sync_object = ] N'sync_object' ]
    [ , [ @ins_cmd = ] N'ins_cmd' ]
    [ , [ @del_cmd = ] N'del_cmd' ]
    [ , [ @upd_cmd = ] N'upd_cmd' ]
    [ , [ @creation_script = ] N'creation_script' ]
    [ , [ @description = ] N'description' ]
    [ , [ @pre_creation_cmd = ] N'pre_creation_cmd' ]
    [ , [ @filter_clause = ] N'filter_clause' ]
    [ , [ @schema_option = ] schema_option ]
    [ , [ @destination_owner = ] N'destination_owner' ]
    [ , [ @status = ] status ]
    [ , [ @source_owner = ] N'source_owner' ]
    [ , [ @sync_object_owner = ] N'sync_object_owner' ]
    [ , [ @filter_owner = ] N'filter_owner' ]
    [ , [ @source_object = ] N'source_object' ]
    [ , [ @artid = ] artid OUTPUT ]
    [ , [ @auto_identity_range = ] N'auto_identity_range' ]
    [ , [ @pub_identity_range = ] pub_identity_range ]
    [ , [ @identity_range = ] identity_range ]
    [ , [ @threshold = ] threshold ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @use_default_datatypes = ] use_default_datatypes ]
    [ , [ @identityrangemanagementoption = ] N'identityrangemanagementoption' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @fire_triggers_on_snapshot = ] N'fire_triggers_on_snapshot' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication that contains the article. The name must be unique in the database. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. The name must be unique within the publication. *@article* is **sysname**, with no default.

#### [ @source_table = ] N'*source_table*'

This parameter has been deprecated; use *@source_object* instead. *@source_table* is **nvarchar(386)**, with a default of `NULL`. *@source_table* isn't supported for Oracle Publishers.

#### [ @destination_table = ] N'*destination_table*'

The name of the destination (subscription) table, if different from *@source_table* or the stored procedure. *@destination_table* is **sysname**, with a default of `NULL`. If set to `NULL`, this means that *@source_table* equals *@destination_table*.

#### [ @vertical_partition = ] N'*vertical_partition*'

Enables and disables column filtering on a table article. *@vertical_partition* is **nchar(5)**, with a default of `false`.

- `false` indicates there's no vertical filtering and publishes all columns.

- `true` clears all columns except the declared primary key, nullable columns with no default, and unique key columns. Columns are added using [sp_articlecolumn](sp-articlecolumn-transact-sql.md).

#### [ @type = ] N'*type*'

The type of article. *@type* is **sysname**, with a default of `NULL`, and can be one of the following values.

| Value | Description |
| --- | --- |
| `aggregate schema only` | Aggregate function with schema only. |
| `func schema only` | Function with schema only. |
| `indexed view logbased` | Log-based indexed view article. Not supported for Oracle Publishers. For this type of article, the base table doesn't need to be published separately. |
| `indexed view logbased manualboth` | Log-based indexed view article with manual filter and manual view. This option requires that you specify both *@sync_object* and *@filter* parameters. For this type of article, the base table doesn't need to be published separately. Not supported for Oracle Publishers. |
| `indexed view logbased manualfilter` | Log-based indexed view article with manual filter. This option requires that you specify both *@sync_object* and *@filter* parameters. For this type of article, the base table doesn't need to be published separately. Not supported for Oracle Publishers. |
| `indexed view logbased manualview` | Log-based indexed view article with manual view. This option requires that you specify the *@sync_object* parameter. For this type of article, the base table doesn't need to be published separately. Not supported for Oracle Publishers. |
| `indexed view schema only` | Indexed view with schema only. For this type of article, the base table must also be published. |
| `logbased` (default) | Log-based article. |
| `logbased manualboth` | Log-based article with manual filter and manual view. This option requires that you specify both *@sync_object* and *@filter* parameters. Not supported for Oracle Publishers. |
| `logbased manualfilter` | Log-based article with manual filter. This option requires that you specify both *@sync_object* and *@filter* parameters. Not supported for Oracle Publishers. |
| `logbased manualview` | Log-based article with manual view. This option requires that you specify the *@sync_object* parameter. Not supported for Oracle Publishers. |
| `proc exec` | Replicates the execution of the stored procedure to all Subscribers of the article. Not supported for Oracle Publishers. We recommend that you use the option **serializable proc exec** instead of **proc exec**. For more information, see the section "Types of Stored Procedure Execution" in [Publishing Stored Procedure Execution in Transactional Replication](../replication/transactional/publishing-stored-procedure-execution-in-transactional-replication.md). Not available when change data capture is enabled. |
| `proc schema only` | Procedure with schema only. Not supported for Oracle Publishers. |
| `serializable proc exec` | Replicates the execution of the stored procedure only if it's executed within the context of a serializable transaction. Not supported for Oracle Publishers.<br /><br />The procedure also must be executed inside an explicit transaction for the procedure execution to be replicated. |
| `view schema only` | View with schema only. Not supported for Oracle Publishers. When using this option, you must also publish the base table. |

#### [ @filter = ] N'*filter*'

The stored procedure (created with FOR REPLICATION) used to filter the table horizontally. *@filter* is **nvarchar(386)**, with a default of `NULL`. You must run [sp_articleview](sp-articleview-transact-sql.md) and [sp_articlefilter](sp-articlefilter-transact-sql.md) manually to create the view and filter stored procedure. If not `NULL`, the filter procedure isn't created (assumes the stored procedure is created manually).

#### [ @sync_object = ] N'*sync_object*'

The name of the table or view used for producing the data file used to represent the snapshot for this article. *@sync_object* is **nvarchar(386)**, with a default of `NULL`. If `NULL`, [sp_articleview](sp-articleview-transact-sql.md) is called to automatically create the view used to generate the output file. This occurs after adding any columns with [sp_articlecolumn](sp-articlecolumn-transact-sql.md). If not `NULL`, a view isn't created (assumes the view is manually created).

#### [ @ins_cmd = ] N'*ins_cmd*'

The replication command type used when replicating inserts for this article. *@ins_cmd* is **nvarchar(255)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `NONE` | No action is taken. |
| `CALL sp_MSins_<table>` (default)<br /><br />-or-<br /><br />`CALL custom_stored_procedure_name` | Calls a stored procedure to be executed at the Subscriber. To use this method of replication, use *@schema_option* to specify automatic creation of the stored procedure, or create the specified stored procedure in the destination database of each Subscriber of the article. `<custom_stored_procedure>` is the name of a user-created stored procedure. `sp_MSins_<table>` contains the name of the destination table in place of the *_table* part of the parameter. When *@destination_owner* is specified, it's prepended to the destination table name. For example, for the `ProductCategory` table owned by the `Production` schema at the Subscriber, the parameter would be `CALL sp_MSins_ProductionProductCategory`. For an article in a peer-to-peer replication topology, `<table>` is appended with a GUID value. Specifying `<custom_stored_procedure>` isn't supported for updating subscribers. |
| `SQL` or `NULL` | Replicates an `INSERT` statement. The `INSERT` statement provides values for all columns published in the article. This command is replicated on inserts:<br /><br />`INSERT INTO <table name> VALUES (c1value, c2value, c3value, ..., cnvalue)` |

For more information, see [Transactional Articles - Specify How Changes Are Propagated](../replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).

#### [ @del_cmd = ] N'*del_cmd*'

The replication command type used when replicating deletes for this article. *@del_cmd* is **nvarchar(255)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `NONE` | No action is taken. |
| `CALLsp_MSdel_<table>` (default)<br /><br />-or-<br /><br />`CALL <custom_stored_procedure_name>` | Calls a stored procedure to be executed at the Subscriber. To use this method of replication, use *@schema_option* to specify automatic creation of the stored procedure, or create the specified stored procedure in the destination database of each Subscriber of the article. `<custom_stored_procedure>` is the name of a user-created stored procedure. `sp_MSdel_<table>` contains the name of the destination table in place of the *_table* part of the parameter. When *@destination_owner* is specified, it's prepended to the destination table name. For example, for the `ProductCategory` table owned by the `Production` schema at the Subscriber, the parameter would be `CALL sp_MSdel_ProductionProductCategory`. For an article in a peer-to-peer replication topology, `<table>` is appended with a GUID value. Specifying `<custom_stored_procedure>` isn't supported for updating subscribers. |
| `XCALL sp_MSdel_<table>`<br /><br />-or-<br /><br />`XCALL <custom_stored_procedure_name>` | Calls a stored procedure taking `XCALL` style parameters. To use this method of replication, use *@schema_option* to specify automatic creation of the stored procedure, or create the specified stored procedure in the destination database of each Subscriber of the article. Specifying a user-created stored procedure isn't allowed for updating subscribers. |
| `SQL` or `NULL` | Replicates a `DELETE` statement. The `DELETE` statement is provided all primary key column values. This command is replicated on deletes:<br /><br />`DELETE FROM <table name> WHERE pkc1 = pkc1value AND pkc2 = pkc2value AND pkcn = pkcnvalue` |

For more information, see [Transactional Articles - Specify How Changes Are Propagated](../replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).

#### [ @upd_cmd = ] N'*upd_cmd*'

The replication command type used when replicating updates for this article. *@upd_cmd* is **nvarchar(255)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `NONE` | No action is taken. |
| `CALL sp_MSupd_<table>`<br /><br />-or-<br /><br />`CALL <custom_stored_procedure_name>` | Calls a stored procedure to be executed at the Subscriber. To use this method of replication, use *@schema_option* to specify automatic creation of the stored procedure, or create the specified stored procedure in the destination database of each Subscriber of the article. |
| `MCALL sp_MSupd_<table>`<br /><br />-or-<br /><br />`MCALL <custom_stored_procedure_name>` | Calls a stored procedure taking `MCALL` style parameters. To use this method of replication, use *@schema_option* to specify automatic creation of the stored procedure, or create the specified stored procedure in the destination database of each Subscriber of the article. `<custom_stored_procedure>` is the name of a user-created stored procedure. `sp_MSupd_<table>` contains the name of the destination table in place of the `<table>` part of the parameter. When *@destination_owner* is specified, it's prepended to the destination table name. For example, for the `ProductCategory` table owned by the `Production` schema at the Subscriber, the parameter would be `MCALL sp_MSupd_ProductionProductCategory`. For an article in a peer-to-peer replication topology, `<table>` is appended with a GUID value. Specifying a user-created stored procedure isn't allowed for updating subscribers. |
| `SCALL sp_MSupd_<table>` (default)<br /><br />-or-<br /><br />`SCALL <custom_stored_procedure_name>` | Calls a stored procedure taking `SCALL` style parameters. To use this method of replication, use *@schema_option* to specify automatic creation of the stored procedure, or create the specified stored procedure in the destination database of each Subscriber of the article. `<custom_stored_procedure>` is the name of a user-created stored procedure. `sp_MSupd_<table>` contains the name of the destination table in place of the *_table* part of the parameter. When *@destination_owner* is specified, it's prepended to the destination table name. For example, for the `ProductCategory` table owned by the `Production` schema at the Subscriber, the parameter would be `SCALL sp_MSupd_ProductionProductCategory`. For an article in a peer-to-peer replication topology, *_table* is appended with a GUID value. Specifying a user-created stored procedure isn't allowed for updating subscribers. |
| `XCALL sp_MSupd_<table>`<br /><br />-or-<br /><br />`XCALL <custom_stored_procedure_name>` | Calls a stored procedure taking `XCALL` style parameters. To use this method of replication, use *@schema_option* to specify automatic creation of the stored procedure, or create the specified stored procedure in the destination database of each Subscriber of the article. Specifying a user-created stored procedure isn't allowed for updating subscribers. |
| `SQL` or `NULL` | Replicates an `UPDATE` statement. The `UPDATE` statement is provided on all column values and the primary key column values. This command is replicated on updates:<br /><br />`UPDATE <table name> SET c1 = c1value, SET c2 = c2value, SET cn = cnvalue WHERE pkc1 = pkc1value AND pkc2 = pkc2value AND pkcn = pkcnvalue` |

> [!NOTE]  
> The syntax for `CALL`, `MCALL`, `SCALL`, and `XCALL` vary the amount of data propagated to the subscriber. The `CALL` syntax passes all values for all inserted and deleted columns. The `SCALL` syntax passes values only for affected columns. The `XCALL` syntax passes values for all columns, whether changed or not, including the previous value of the column. For more information, see [Transactional Articles - Specify How Changes Are Propagated](../replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).

#### [ @creation_script = ] N'*creation_script*'

The path and name of an optional article schema script used to create the article in the subscription database. *@creation_script* is **nvarchar(255)**, with a default of `NULL`.

#### [ @description = ] N'*description*'

A descriptive entry for the article. *@description* is **nvarchar(255)**, with a default of `NULL`.

#### [ @pre_creation_cmd = ] N'*pre_creation_cmd*'

Specifies what the system should do if it detects an existing object of the same name at the subscriber when applying the snapshot for this article. *@pre_creation_cmd* is **nvarchar(10)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `none` | Doesn't use a command. |
| `delete` | Deletes data from the destination table before applying the snapshot. When the article is horizontally filtered, only data in columns specified by the filter clause is deleted. Not supported for Oracle Publishers when a horizontal filter is defined. |
| `drop` (default) | Drops the destination table. |
| `truncate` | Truncates the destination table. Isn't valid for ODBC or OLE DB Subscribers. |

#### [ @filter_clause = ] N'*filter_clause*'

A restriction (WHERE) clause that defines a horizontal filter. When entering the restriction clause, omit the keyword WHERE. *@filter_clause* is **nvarchar(max)**, with a default of `NULL`. For more information, see [Filter Published Data](../replication/publish/filter-published-data.md).

#### [ @schema_option = ] *schema_option*

A bitmask of the schema generation option for the given article. *@schema_option* is **varbinary(8)**, with a default of `NULL`, and can be the [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) product of one or more of these values:

> [!NOTE]  
> If this value is `NULL`, the system auto-generates a valid schema option for the article depending on other article properties. The [Default schema options](#default-schema-options) shows the value that will be chosen based upon the combination of the article type and the replication type.

| Value | Description |
| --- | --- |
| `0x00` | Disables scripting by the Snapshot Agent and uses *@creation_script*. |
| `0x01` | Generates the object creation script (CREATE TABLE, CREATE PROCEDURE, and so on). This value is the default for stored procedure articles. |
| `0x02` | Generates the stored procedures that propagate changes for the article, if defined. |
| `0x04` | Identity columns are scripted using the IDENTITY property. |
| `0x08` | Replicate **timestamp** columns. If not set, **timestamp** columns are replicated as **binary**. |
| `0x10` | Generates a corresponding clustered index. Even if this option isn't set, indexes related to primary keys and unique constraints are generated if they are already defined on a published table. |
| `0x20` <sup>1</sup> | Converts user-defined data types (UDT) to base data types at the Subscriber. This option can't be used when there's a CHECK or DEFAULT constraint on a UDT column, if a UDT column is part of the primary key, or if a computed column references a UDT column. |
| `0x40` | Generates corresponding nonclustered indexes. Even if this option isn't set, indexes related to primary keys and unique constraints are generated if they are already defined on a published table. |
| `0x80` | Replicates primary key constraints. Any indexes related to the constraint are also replicated, even if options `0x10` and `0x40` aren't enabled. |
| `0x100` <sup>1</sup> | Replicates user triggers on a table article, if defined. |
| `0x200` <sup>1</sup> | Replicates foreign key constraints. If the referenced table isn't part of a publication, all foreign key constraints on a published table aren't replicated. |
| `0x400` <sup>1</sup> | Replicates check constraints. |
| `0x800` <sup>1</sup> | Replicates defaults. |
| `0x1000` | Replicates column-level collation.<br /><br />**Note:** This option should be set for Oracle Publishers to enable case-sensitive comparisons. |
| `0x2000` <sup>1</sup> | Replicates extended properties associated with the published article source object. |
| `0x4000` | Replicates UNIQUE constraints. Any indexes related to the constraint are also replicated, even if options `0x10` and `0x40` aren't enabled. |
| `0x8000` | This option isn't valid for [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Publishers. |
| `0x10000` | Replicates CHECK constraints as NOT FOR REPLICATION so that the constraints aren't enforced during synchronization. |
| `0x20000` | Replicates FOREIGN KEY constraints as NOT FOR REPLICATION so that the constraints aren't enforced during synchronization. |
| `0x40000` | Replicates filegroups associated with a partitioned table or index. |
| `0x80000` | Replicates the partition scheme for a partitioned table. |
| `0x100000` | Replicates the partition scheme for a partitioned index. |
| `0x200000` | Replicates table statistics. |
| `0x400000` | Default bindings. |
| `0x800000` | Rule bindings. |
| `0x1000000` | Full-text index. |
| `0x2000000` | XML schema collections bound to **xml** columns aren't replicated. |
| `0x4000000` | Replicates indexes on **xml** columns. |
| `0x8000000` | Create any schemas not already present on the subscriber. |
| `0x10000000` | Converts **xml** columns to **ntext** on the Subscriber. |
| `0x20000000` | Converts large object data types (**nvarchar(max)**, **varchar(max)**, and **varbinary(max)**) introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] to data types that are supported on [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. |
| `0x40000000` | Replicate permissions. |
| `0x80000000` | Attempt to drop dependencies to any objects that aren't part of the publication. |
| `0x100000000` | Use this option to replicate the FILESTREAM attribute if it's specified on **varbinary(max)** columns. Don't specify this option if you're replicating tables to [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Subscribers. Replicating tables that have FILESTREAM columns to [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Subscribers isn't supported, regardless of how this schema option is set.<br /><br />See related option **0x800000000**. |
| `0x200000000` | Converts date and time data types (**date**, **time**, **datetimeoffset**, and **datetime2**) introduced in [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] to data types that are supported on earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `0x400000000` | Replicates the compression option for data and indexes. For more information, see [Data compression](../data-compression/data-compression.md). |
| `0x800000000` | Set this option to store FILESTREAM data on its own filegroup at the Subscriber. If this option isn't set, FILESTREAM data is stored on the default filegroup. Replication doesn't create filegroups; therefore, if you set this option, you must create the filegroup before you apply the snapshot at the Subscriber. For more information about how to create objects before you apply the snapshot, see [Execute Scripts Before and After the Snapshot Is Applied](../replication/snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied).<br /><br />See related option **0x100000000**. |
| `0x1000000000` | Converts common language runtime (CLR) user-defined types (UDTs) that are larger than 8000 bytes to **varbinary(max)** so that columns of type UDT can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. |
| `0x2000000000` | Converts the **hierarchyid** data type to **varbinary(max)** so that columns of type **hierarchyid** can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. For more information about how to use **hierarchyid** columns in replicated tables, see [hierarchyid](../../t-sql/data-types/hierarchyid-data-type-method-reference.md). |
| `0x4000000000` | Replicates any filtered indexes on the table. For more information about filtered indexes, see [Create filtered indexes](../indexes/create-filtered-indexes.md). |
| `0x8000000000` | Converts the **geography** and **geometry** data types to **varbinary(max)** so that columns of these types can be replicated to Subscribers that are running [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. |
| `0x10000000000` | Replicates indexes on columns of type **geography** and **geometry**. |
| `0x20000000000` | Replicates the SPARSE attribute for columns. For more information about this attribute, see [Use sparse columns](../tables/use-sparse-columns.md). |
| `0x40000000000` | Enable scripting by the snapshot agent to create memory-optimized table on the subscriber. |
| `0x80000000000` | Converts clustered index to nonclustered index for memory-optimized articles. |
| `0x400000000000` | Replicates any nonclustered columnstore indexes on the table(s) |
| `0x800000000000` | Replicates any filtered nonclustered columnstore indexes on the table(s). |
| `NULL` | Replication automatically sets *@schema_option* to a default value, the value of which depends on other article properties. The "Default Schema Options" table in the Remarks section shows the default schema options based on article type and replication type.<br /><br />The default for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publications is `0x050D3`. |

<sup>1</sup> Not supported for Oracle Publishers.

Not all *@schema_option* values are valid for every type of replication and article type. The **Valid Schema Options** table in the Remarks section shows the valid schema options that can be chosen based upon the combination of the article type and the replication type.

#### [ @destination_owner = ] N'*destination_owner*'

The name of the owner of the destination object. *@destination_owner* is **sysname**, with a default of `NULL`. When *@destination_owner* isn't specified, the owner is specified automatically based on the following rules:

| Condition | Destination object owner |
| --- | --- |
| Publication uses native-mode bulk copy to generate the initial snapshot, which only supports [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. | Defaults to the value of *@source_owner*. |
| Published from a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. | Defaults to the owner of the destination database. |
| Publication uses character-mode bulk copy to generate the initial snapshot, which supports non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. | Not assigned. |

To support non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, *@destination_owner* must be `NULL`.

#### [ @status = ] *status*

Specifies if the article is active and additional options for how changes are propagated. *@status* is **tinyint**, and can be the [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) product of one or more of these values.

| Value | Description |
| --- | --- |
| `1` | Article is active. |
| `8` | Includes the column name in `INSERT` statements. |
| `16` (default) | Uses parameterized statements. |
| `24` | Includes the column name in `INSERT` statements and uses parameterized statements. |
| `64` | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |

For example, an active article using parameterized statements would have a value of `17` in this column. A value of `0` means that the article is inactive and no additional properties are defined.

#### [ @source_owner = ] N'*source_owner*'

The owner of the source object. *@source_owner* is **sysname**, with a default of `NULL`. *@source_owner* must be specified for Oracle Publishers.

#### [ @sync_object_owner = ] N'*sync_object_owner*'

The owner of the view that defines the published article. *@sync_object_owner* is **sysname**, with a default of `NULL`.

#### [ @filter_owner = ] N'*filter_owner*'

The owner of the filter. *@filter_owner* is **sysname**, with a default of `NULL`.

#### [ @source_object = ] N'*source_object*'

The database object to be published. *@source_object* is **sysname**, with a default of `NULL`. If *@source_table* is `NULL`, *@source_object* can't be `NULL`. *@source_object* should be used instead of *@source_table*. For more information about the types of objects that can be published using snapshot or transactional replication, see [Publish Data and Database Objects](../replication/publish/publish-data-and-database-objects.md).

#### [ @artid = ] *artid* OUTPUT

The article ID of the new article. *@artid* is an OUTPUT parameter of type **int**.

#### [ @auto_identity_range = ] N'*auto_identity_range*'

Enables and disables automatic identity range handling on a publication at the time it's created. *@auto_identity_range* is **nvarchar(5)**, and can be one of the following values:

| Value | Description |
| --- | --- |
| `true` | Enables automatic identity range handling |
| `false` | Disables automatic identity range handling |
| `NULL` (default) | Identity range handling is set by *@identityrangemanagementoption*. |

> [!NOTE]  
> *@auto_identity_range* is deprecated, and is provided for backward compatibility only. You should use *@identityrangemanagementoption* for specifying identity range management options. For more information, see [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md).

#### [ @pub_identity_range = ] *pub_identity_range*

Controls the range size at the Publisher if the article has *@identityrangemanagementoption* set to **auto** or *@auto_identity_range* set to **true**. *@pub_identity_range* is **bigint**, with a default of `NULL`. *@pub_identity_range* isn't supported for Oracle Publishers.

#### [ @identity_range = ] *identity_range*

Controls the range size at the Subscriber if the article has *@identityrangemanagementoption* set to **auto** or *@auto_identity_range* set to **true**. *@identity_range* is **bigint**, with a default of `NULL`. Used when *@auto_identity_range* is set to **true**. *@identity_range* isn't supported for Oracle Publishers.

#### [ @threshold = ] *threshold*

The percentage value that controls when the Distribution Agent assigns a new identity range. When the percentage of values specified in *@threshold* is used, the Distribution Agent creates a new identity range. *@threshold* is **int**, with a default of `NULL`. Used when *@identityrangemanagementoption* is set to **auto** or *@auto_identity_range* is set to **true**. *@threshold* isn't supported for Oracle Publishers.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Acknowledges that the action taken by this stored procedure can invalidate an existing snapshot. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that adding an article doesn't cause the snapshot to be invalid. If the stored procedure detects that the change requires a new snapshot, an error occurs and no changes are made.

- `1` specifies that adding an article can cause the snapshot to be invalid, and if subscriptions exist that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot to be generated.

#### [ @use_default_datatypes = ] *use_default_datatypes*

Whether the default column data type mappings are used when publishing an article from an Oracle Publisher. *@use_default_datatypes* is **bit**, with a default of `1`.

- `1` = the default article column mappings are used. The default data type mappings can be displayed by executing [sp_getdefaultdatatypemapping](sp-getdefaultdatatypemapping-transact-sql.md).

- `0` = custom article column mappings are defined, and therefore [sp_articleview](sp-articleview-transact-sql.md) isn't called by `sp_addarticle`.

When *@use_default_datatypes* is set to `0`, you must execute [sp_changearticlecolumndatatype](sp-changearticlecolumndatatype-transact-sql.md) once for each column mapping being changed from the default. After all custom column mappings have been defined, you must execute [sp_articleview](sp-articleview-transact-sql.md).

> [!NOTE]  
> This parameter should only be used for Oracle Publishers. Setting *@use_default_datatypes* to `0` for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher generates an error.

#### [ @identityrangemanagementoption = ] N'*identityrangemanagementoption*'

Specifies how identity range management is handled for the article. *@identityrangemanagementoption* is **nvarchar(10)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `none` | Replication does no explicit identity range management. This option is recommended only for backward compatibility with earlier versions of SQL Server. Not allowed for peer replication. |
| `manual` | Marks the identity column using NOT FOR REPLICATION to enable manual identity range handling. |
| `auto` | Specifies automatic management of identity ranges. |
| `NULL` (default) | Defaults to `none` when the value of *@auto_identity_range* isn't `true`. Defaults to `manual` in a peer-to-peer topology default (*@auto_identity_range* is ignored). |

For backward compatibility, when the value of *@identityrangemanagementoption* is `NULL`, the value of *@auto_identity_range* is checked. However, when the value of *@identityrangemanagementoption* isn't `NULL`, then the value of *@auto_identity_range* is ignored.

For more information, see [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md).

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when adding an article to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

#### [ @fire_triggers_on_snapshot = ] N'*fire_triggers_on_snapshot*'

If replicated user triggers are executed when the initial snapshot is applied. *@fire_triggers_on_snapshot* is **nvarchar(5)**, with a default of `false`. `true` means that user triggers on a replicated table are executed when the snapshot is applied. In order for triggers to be replicated, the bitmask value of *@schema_option* must include the value `0x100`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addarticle` is used in snapshot replication or transactional replication.

By default, replication doesn't publish any columns in the source table when the column data type isn't supported by replication. If you need to publish such a column, you must execute [sp_articlecolumn](sp-articlecolumn-transact-sql.md) to add the column.

When adding an article to a publication that supports peer-to-peer transactional replication, the following restrictions apply:

- Parameterized statements must be specified for all logbased articles. You must include `16` in the *@status* value.

- Name and owner of the destination table must match the source table.

- The article can't be filtered horizontally or vertically.

- Automatic identity range management isn't supported. You must specify a value of manual for *@identityrangemanagementoption*.

- If a **timestamp** column exists in the table, you must include 0x08 in *@schema_option* to replicate the column as **timestamp**.

- A value of `SQL` can't be specified for *@ins_cmd*, *@upd_cmd*, and *@del_cmd*.

For more information, see [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md).

When you publish objects, their definitions are copied to Subscribers. If you're publishing a database object that depends on one or more other objects, you must publish all referenced objects. For example, if you publish a view that depends on a table, you must publish the table also.

If *@vertical_partition* is set to **true**, `sp_addarticle` defers the creation of the view until [sp_articleview](sp-articleview-transact-sql.md) is called (after the last [sp_articlecolumn](sp-articlecolumn-transact-sql.md) is added).

If the publication allows updating subscriptions and the published table doesn't have a **uniqueidentifier** column, `sp_addarticle` adds a **uniqueidentifier** column to the table automatically.

When replicating to a subscriber that isn't an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (heterogeneous replication), only [!INCLUDE [tsql](../../includes/tsql-md.md)] statements are supported for `INSERT`, `UPDATE`, and `DELETE` commands.

When the log reader agent is running, adding an article to a peer-to-peer publication can cause a deadlock between the log reader agent and the process that adds the article. To avoid this issue, before adding an article to a peer-to-peer publication use the Replication Monitor to stop the log reader agent on the node where you're adding the article. Restart the log reader agent after adding the article.

When setting *@del_cmd* to `NONE` or *@ins_cmd* to `NONE`, the propagation of `UPDATE` commands might also be affected by not sending those commands when a bounded update occurs. (A bounded update is type of `UPDATE` statement from the publisher that replicates as a `DELETE`/`INSERT` pair on the subscriber.)

## Default schema options

This table describes the default value set by replication if *@schema_options* isn't specified by the user, where this value depends on the replication type (shown across the top) and the article type (shown down the first column).

| Article type | Transactional replication | Snapshot replication |
| --- | --- | --- |
| **aggregate schema only** | `0x01` | `0x01` |
| **func schema only** | `0x01` | `0x01` |
| **indexed view schema only** | `0x01` | `0x01` |
| **indexed view logbased** | `0x30F3` | `0x3071` |
| **indexed view logbase manualboth** | `0x30F3` | `0x3071` |
| **indexed view logbased manualfilter** | `0x30F3` | `0x3071` |
| **indexed view logbased manualview** | `0x30F3` | `0x3071` |
| **logbased** | `0x30F3` | `0x3071` |
| **logbased manualfilter** | `0x30F3` | `0x3071` |
| **logbased manualview** | `0x30F3` | `0x3071` |
| **proc exec** | `0x01` | `0x01` |
| **proc schema only** | `0x01` | `0x01` |
| **serializable proc exec** | `0x01` | `0x01` |
| **view schema only** | `0x01` | `0x01` |

> [!NOTE]  
> If a publication is enabled for queued updating, a *@schema_option* value of `0x80` is added to the default value shown in the table. The default *@schema_option* for a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publication is `0x050D3`.

## Valid schema options

This table describes the allowable values of *@schema_option* based upon the replication type (shown across the top) and the article type (shown down the first column).

| Article type | Transactional replication | Snapshot replication |
| --- | --- | --- |
| **logbased** | All options | All options but `0x02` |
| **logbased manualfilter** | All options | All options but `0x02` |
| **logbased manualview** | All options | All options but `0x02` |
| **indexed view logbased** | All options | All options but `0x02` |
| **indexed view logbased manualfilter** | All options | All options but `0x02` |
| **indexed view logbased manualview** | All options | All options but `0x02` |
| **indexed view logbase manualboth** | All options | All options but `0x02` |
| **proc exec** | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` |
| **serializable proc exec** | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` |
| **proc schema only** | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` |
| **view schema only** | `0x01`, `0x010`, `0x020`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x100000`, `0x200000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x40000000`, and `0x80000000` | `0x01`, `0x010`, `0x020`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x100000`, `0x200000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x40000000`, and `0x80000000` |
| **func schema only** | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` | `0x01`, `0x20`, `0x2000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x10000000`, `0x20000000`, `0x40000000`, and `0x80000000` |
| **indexed view schema only** | `0x01`, `0x010`, `0x020`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x100000`, `0x200000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x40000000`, and `0x80000000` | `0x01`, `0x010`, `0x020`, `0x040`, `0x0100`, `0x2000`, `0x40000`, `0x100000`, `0x200000`, `0x400000`, `0x800000`, `0x2000000`, `0x8000000`, `0x40000000`, and `0x80000000` |

> [!NOTE]  
> For queued updating publications, the *@schema_option* values of `0x8000` and `0x80` must be enabled. The supported *@schema_option* values for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publications are: `0x01`, `0x02`, `0x10`, `0x40`, `0x80`, `0x1000`, `0x4000` and `0X8000`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addarticle-transact-sql_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addarticle`.

## Related content

- [Define an Article](../replication/publish/define-an-article.md)
- [sp_articlecolumn (Transact-SQL)](sp-articlecolumn-transact-sql.md)
- [sp_articlefilter (Transact-SQL)](sp-articlefilter-transact-sql.md)
- [sp_articleview (Transact-SQL)](sp-articleview-transact-sql.md)
- [sp_changearticle (Transact-SQL)](sp-changearticle-transact-sql.md)
- [sp_droparticle (Transact-SQL)](sp-droparticle-transact-sql.md)
- [sp_helparticle (Transact-SQL)](sp-helparticle-transact-sql.md)
- [sp_helparticlecolumns (Transact-SQL)](sp-helparticlecolumns-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [Publish Data and Database Objects](../replication/publish/publish-data-and-database-objects.md)
