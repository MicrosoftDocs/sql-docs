---
title: "sp_helparticle (Transact-SQL)"
description: sp_helparticle displays information about an article.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helparticle_TSQL"
  - "sp_helparticle"
helpviewer_keywords:
  - "sp_helparticle"
dev_langs:
  - "TSQL"
---
# sp_helparticle (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Displays information about an article. This stored procedure is executed at the Publisher on the publication database. For Oracle Publishers, this stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helparticle
    [ @publication = ] N'publication'
    [ , [ @article = ] N'article' ]
    [ , [ @returnfilter = ] returnfilter ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @found = ] found OUTPUT ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of an article in the publication. *@article* is **sysname**, with a default of `%`. If *@article* isn't supplied, information on all articles for the specified publication is returned.

#### [ @returnfilter = ] *returnfilter*

Specifies whether the filter clause should be returned. *@returnfilter* is **bit**, with a default of `1`, which returns the filter clause.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be specified when requesting information on an article published by a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

#### [ @found = ] *found* OUTPUT

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `article id` | **int** | ID of the article. |
| `article name` | **sysname** | Name of the article. |
| `base object` | **nvarchar(257)** | Name of the underlying table represented by the article or stored procedure. |
| `destination object` | **sysname** | Name of the destination (subscription) table. |
| `synchronization object` | **nvarchar(257)** | Name of the view that defines the published article. |
| `type` | **smallint** | The type of article:<br /><br />`1` = Log-based.<br />`3` = Log-based with manual filter.<br />`5` = Log-based with manual view.<br />`7` = Log-based with manual filter and manual view.<br />`8` = Stored procedure execution.<br />`24` = Serializable stored procedure execution.<br />`32` = Stored procedure (schema only).<br />`64` = View (schema only).<br />`96` = Aggregate function (schema only).<br />`128` = Function (schema only).<br />`257` = Log-based indexed view.<br />`259` = Log-based indexed view with manual filter.<br />`261` = Log-based indexed view with manual view.<br />`263` = Log-based indexed view with manual filter and manual view.<br />`320` = Indexed view (schema only).<br />|
| `status` | **tinyint** | Can be the [&amp; (Bitwise AND)](../../t-sql/language-elements/bitwise-and-transact-sql.md) result of one or more or these article properties:<br /><br />`0x00` = [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]<br />`0x01` = Article is active.<br />`0x08` = Include the column name in insert statements.<br />`0x16` = Use parameterized statements.<br />`0x32` = Use parameterized statements and include the column name in insert statements. |
| `filter` | **nvarchar(257)** | Stored procedure used to horizontally filter the table. This stored procedure must be created using the `FOR REPLICATION` clause. |
| `description` | **nvarchar(255)** | Descriptive entry for the article. |
| `insert_command` | **nvarchar(255)** | The replication command type used when replicating inserts with table articles. <sup>1</sup> |
| `update_command` | **nvarchar(255)** | The replication command type used when replicating updates with table articles. <sup>1</sup> |
| `delete_command` | **nvarchar(255)** | The replication command type used when replicating deletes with table articles. <sup>1</sup> |
| `creation script path` | **nvarchar(255)** | Path and name of an article schema script used to create target tables. |
| `vertical partition` | **bit** | Is whether vertical partitioning is enabled for the article; where a value of `1` means that vertical partitioning is enabled. |
| `pre_creation_cmd` | **tinyint** | Precreation command for `DROP TABLE`, `DELETE TABLE`, or `TRUNCATE TABLE`. |
| `filter_clause` | **ntext** | WHERE clause specifying the horizontal filtering. |
| `schema_option` | **binary(8)** | Bitmap of the schema generation option for the given article. For a complete list of `schema_option` values, see [sp_addarticle](sp-addarticle-transact-sql.md). |
| `dest_owner` | **sysname** | Name of the owner of the destination object. |
| `source_owner` | **sysname** | Owner of the source object. |
| `unqua_source_object` | **sysname** | Name of the source object, without the owner name. |
| `sync_object_owner` | **sysname** | Owner of the view that defines the published article. |
| `unqualified_sync_object` | **sysname** | Name of the view that defines the published article, without the owner name. |
| `filter_owner` | **sysname** | Owner of the filter. |
| `unqua_filter` | **sysname** | Name of the filter, without the owner name. |
| `auto_identity_range` | **int** | Flag indicating if automatic identity range handling was turned on at the publication at the time it was created. `1` means that automatic identity range is enabled; `0` means it's disabled. |
| `publisher_identity_range` | **int** | Range size of the identity range at the Publisher if the article has `identityrangemanagementoption` set to `auto` or `auto_identity_range` set to `true`. |
| `identity_range` | **bigint** | Range size of the identity range at the Subscriber if the article has `identityrangemanagementoption` set to `auto` or `auto_identity_range` set to `true`. |
| `threshold` | **bigint** | Percentage value indicating when the Distribution Agent assigns a new identity range. |
| `identityrangemanagementoption` | **int** | Indicates the identity range management handled for the article. |
| `fire_triggers_on_snapshot` | **bit** | Is if replicated user triggers are executed when the initial snapshot is applied.<br /><br />`1` = user triggers are executed.<br />`0` = user triggers aren't executed. |

<sup>1</sup> For more information, see [Transactional Articles - Specify How Changes Are Propagated](../replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helparticle` is used in snapshot replication and transactional replication.

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the publication access list for the current publication can execute `sp_helparticle`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-helparticle-transact-_1.sql":::

## Related content

- [View and Modify Article Properties](../replication/publish/view-and-modify-article-properties.md)
- [sp_addarticle (Transact-SQL)](sp-addarticle-transact-sql.md)
- [sp_articlecolumn (Transact-SQL)](sp-articlecolumn-transact-sql.md)
- [sp_changearticle (Transact-SQL)](sp-changearticle-transact-sql.md)
- [sp_droparticle (Transact-SQL)](sp-droparticle-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
