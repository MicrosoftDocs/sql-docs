---
title: "sp_fulltext_pendingchanges (Transact-SQL)"
description: Returns unprocessed changes, such as pending inserts, updates, and deletes, for a specified table that is using change tracking.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_pendingchanges_TSQL"
  - "sp_fulltext_pendingchanges"
helpviewer_keywords:
  - "sp_fulltext_pendingchanges"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_fulltext_pendingchanges (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns unprocessed changes, such as pending inserts, updates, and deletes, for a specified table that is using change tracking.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_pendingchanges table_id
[ ; ]
```

## Arguments

#### *table_id*

ID of the table. If the table isn't full-text indexed, or change tracking isn't enabled on the table, an error is returned.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| **Key** | <sup>1</sup> | The full-text key value from the specified table. |
| **DocId** | **bigint** | An internal document identifier (DocId) column that corresponds to the key value. |
| **Status** | **int** | 0 = Row will be removed from the full-text index.<br /><br />1 = Row will be full-text indexed.<br /><br />2 = Row is up-to-date.<br /><br />-1 = Row is in a transitional (batched, but not committed) state or an error state. |
| **DocState** | **tinyint** | A raw dump of the internal document identifier (DocId) map status column. |

<sup>1</sup> The data type for Key is same as the data type of the full-text key column in the base table.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Remarks

If there are no changes to process, an empty rowset is returned.

Full-Text Search queries don't return rows with a `Status` value of `0`. This is because the row has been deleted from base table and is waiting to be deleted from the full-text index.

To find out how many changes are pending for a particular table, use the `TableFullTextPendingChanges` property of the `OBJECTPROPERTYEX` function.

## Related content

- [Full-Text Search and Semantic Search stored procedures (Transact-SQL)](full-text-search-and-semantic-search-stored-procedures-transact-sql.md)
- [OBJECTPROPERTYEX (Transact-SQL)](../../t-sql/functions/objectpropertyex-transact-sql.md)
