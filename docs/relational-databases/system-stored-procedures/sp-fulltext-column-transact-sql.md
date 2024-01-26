---
title: "sp_fulltext_column (Transact-SQL)"
description: Specifies whether or not a particular column of a table participates in full-text indexing.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_column_TSQL"
  - "sp_fulltext_column"
helpviewer_keywords:
  - "sp_fulltext_column"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_fulltext_column (Transact-SQL)

[!INCLUDE [sql-asa](../../includes/applies-to-version/sql-asa.md)]

Specifies whether or not a particular column of a table participates in full-text indexing.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_column
    [ @tabname = ] N'tabname'
    , [ @colname = ] N'colname'
    , [ @action = ] 'action'
    [ , [ @language = ] language ]
    [ , [ @type_colname = ] N'type_colname' ]
[ ; ]
```

## Arguments

#### [ @tabname = ] N'*tabname*'

A one-part or two-part table name. The table must exist in the current database. The table must have a full-text index. *@tabname* is **nvarchar(517)**, with no default.

#### [ @colname = ] N'*colname*'

The name of a column in *@tabname*. The column must be either a character, **varbinary(max)**, or **image**, and can't be a computed column. *@colname* is **sysname**, with no default.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can create full-text indexes of text data stored in columns that are of **varbinary(max)** or **image** data type. Images and pictures aren't indexed.

#### [ @action = ] '*action*'

The action to be performed. *@action* is **varchar(20)**, with no default, and can be one of the following values.

| Value | Description |
| --- | --- |
| **add** | Adds *@colname* of *@tabname* to the table's inactive full-text index. This action enables the column for full-text indexing. |
| **drop** | Removes *@colname* of *@tabname* from the table's inactive full-text index. |

#### [ @language = ] *language*

The language of the data stored in the column. *@language* is **int**, with a default of `NULL`. For a list of languages included in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [sys.fulltext_languages (Transact-SQL)](../system-catalog-views/sys-fulltext-languages-transact-sql.md).

> [!NOTE]  
> Use `Neutral` when a column contains data in multiple languages or in an unsupported language. The default is specified by the server configuration option [default full-text language](../../database-engine/configure-windows/configure-the-default-full-text-language-server-configuration-option.md).

#### [ @type_colname = ] N'*type_colname*'

The name of a column in *@tabname* that holds the document type of *@colname*. This column must be **char**, **nchar**, **varchar**, or **nvarchar**. It's only used when the data type of *@colname* is of type **varbinary(max)** or **image**. *@type_colname* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

If the full-text index is active, any ongoing population is stopped. Furthermore, if a table with an active full-text index has change tracking enabled, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ensures that the index is current. For example, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stops any current population on the table, drops the existing index, and starts a new population.

If change tracking is on and columns need to be added or dropped from the full-text index while preserving the index, the table should be deactivated, and the required columns should be added or dropped. These actions freeze the index. The table can be activated later when starting a population is practical.

## Permissions

User must be a member of the **db_ddladmin** fixed database role, or a member of the **db_owner** fixed database role, or the owner of the table.

## Examples

The following example adds the `DocumentSummary` column from the `Document` table to the full-text index of the table.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_column 'Production.Document', DocumentSummary, 'add';
GO
```

The following example assumes you created a full-text index on a table named `spanishTbl`. To add the `spanishCol` column to the full-text index, execute the following stored procedure:

```sql
EXEC sp_fulltext_column 'spanishTbl', 'spanishCol', 'add', 0xC0A;
GO
```

When you run this query:

```sql
SELECT *
FROM spanishTbl
WHERE CONTAINS (spanishCol, 'formsof(inflectional, trabajar)');
```

The result set would include rows with different forms of `trabajar` (to work), such as `trabajo`, `trabajamos`, and `trabajan`.

> [!NOTE]  
> All columns listed in a single full-text query function clause must use the same language.

## Related content

- [OBJECTPROPERTY (Transact-SQL)](../../t-sql/functions/objectproperty-transact-sql.md)
- [sp_help_fulltext_columns (Transact-SQL)](sp-help-fulltext-columns-transact-sql.md)
- [sp_help_fulltext_columns_cursor (Transact-SQL)](sp-help-fulltext-columns-cursor-transact-sql.md)
- [sp_help_fulltext_tables (Transact-SQL)](sp-help-fulltext-tables-transact-sql.md)
- [sp_help_fulltext_tables_cursor (Transact-SQL)](sp-help-fulltext-tables-cursor-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Full-Text Search and Semantic Search stored procedures (Transact-SQL)](full-text-search-and-semantic-search-stored-procedures-transact-sql.md)
