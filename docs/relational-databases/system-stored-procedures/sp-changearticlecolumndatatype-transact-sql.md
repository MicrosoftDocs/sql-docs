---
title: "sp_changearticlecolumndatatype (Transact-SQL)"
description: sp_changearticlecolumndatatype changes the article column data type mapping for an Oracle publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changearticlecolumndatatype"
  - "sp_changearticlecolumndatatype_TSQL"
helpviewer_keywords:
  - "sp_changearticlecolumndatatype"
dev_langs:
  - "TSQL"
---
# sp_changearticlecolumndatatype (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the article column data type mapping for an Oracle publication. This stored procedure is executed at the Distributor on any database.

> [!NOTE]  
> The data type mappings between supported Publisher types are provided by default. Use `sp_changearticlecolumndatatype` only when overriding these default settings.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changearticlecolumndatatype
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    , [ @column = ] N'column'
    [ , [ @mapping_id = ] mapping_id ]
    [ , [ @type = ] N'type' ]
    [ , [ @length = ] length ]
    [ , [ @precision = ] precision ]
    [ , [ @scale = ] scale ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the Oracle publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with no default.

#### [ @column = ] N'*column*'

The name of the column for which to change the data type mapping. *@column* is **sysname**, with no default.

#### [ @mapping_id = ] *mapping_id*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @type = ] N'*type*'

The name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type in the destination column. *@type* is **sysname**, with a default of `NULL`.

#### [ @length = ] *length*

The length of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type in the destination column. *@length* is **bigint**, with a default of `NULL`.

#### [ @precision = ] *precision*

The precision of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type in the destination column. *@precision* is **bigint**, with a default of `NULL`.

#### [ @scale = ] *scale*

The scale of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type in the destination column. *@scale* is **bigint**, with a default of `NULL`.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changearticlecolumndatatype` is used to override the default data type mappings between supported Publisher types (Oracle and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]). To view these default data type mappings, execute [sp_getdefaultdatatypemapping](sp-getdefaultdatatypemapping-transact-sql.md).

`sp_changearticlecolumndatatype` is only supported for Oracle Publishers. Executing this stored procedure against a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publication results in an error.

`sp_changearticlecolumndatatype` must be executed for each article column mapping that must be changed.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_changearticlecolumndatatype`.

## Related content

- [Change Publication and Article Properties](../replication/publish/change-publication-and-article-properties.md)
- [Data Type Mapping for Oracle Publishers](../replication/non-sql/data-type-mapping-for-oracle-publishers.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
