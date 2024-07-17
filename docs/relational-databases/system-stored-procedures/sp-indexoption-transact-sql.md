---
title: "sp_indexoption (Transact-SQL)"
description: sp_indexoption sets locking option values for user-defined clustered and nonclustered indexes or tables with no clustered index.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_indexoption"
  - "sp_indexoption_TSQL"
helpviewer_keywords:
  - "sp_indexoption"
dev_langs:
  - "TSQL"
---
# sp_indexoption (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets locking option values for user-defined clustered and nonclustered indexes or tables with no clustered index.

The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] automatically makes choices of page-, row-, or table-level locking. You don't have to set these options manually. `sp_indexoption` is provided for expert users who know with certainty that a particular type of lock is always appropriate.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Instead, use [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_indexoption
    [ @IndexNamePattern = ] N'IndexNamePattern'
    , [ @OptionName = ] 'OptionName'
    , [ @OptionValue = ] 'OptionValue'
[ ; ]
```

## Arguments

#### [ @IndexNamePattern = ] N'*IndexNamePattern*'

The qualified or nonqualified name of a user-defined table or index. *@IndexNamePattern* is **nvarchar(1035)**, with no default. Quotation marks are required only if a qualified index or table name is specified. If a fully qualified table name, including a database name, is provided, the database name must be the name of the current database. If a table name is specified with no index, the specified option value is set for all indexes on that table and the table itself if no clustered index exists.

#### [ @OptionName = ] '*OptionName*'

An index option name. *@OptionName* is **varchar(35)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `AllowRowLocks` | When `TRUE`, row locks are allowed when accessing the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when row locks are used. When `FALSE`, row locks aren't used. The default is `TRUE`. |
| `AllowPageLocks` | When `TRUE`, page locks are allowed when accessing the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when page locks are used. When `FALSE`, page locks aren't used. The default is `TRUE`. |
| `DisAllowRowLocks` | When `TRUE`, row locks aren't used. When `FALSE`, row locks are allowed when accessing the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when row locks are used. |
| `DisAllowPageLocks` | When `TRUE`, page locks aren't used. When `FALSE`, page locks are allowed when accessing the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when page locks are used. |

#### [ @OptionValue = ] '*OptionValue*'

Specifies whether the *@OptionName* setting is enabled (`TRUE`, `ON`, `yes`, or `1`) or disabled (`FALSE`, `OFF`, `no`, or `0`). *@OptionValue* is **varchar(12)**, with no default.

## Return code values

`0` (success) or `> 0` (failure).

## Remarks

XML indexes aren't supported. If an XML index is specified, or a table name is specified with no index name and the table contains an XML index, the statement fails. To set these options, use [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) instead.

To display the current row and page locking properties, use [INDEXPROPERTY](../../t-sql/functions/indexproperty-transact-sql.md) or the [sys.indexes](../system-catalog-views/sys-indexes-transact-sql.md) catalog view.

- Row-level, page-level, and table-level locks are allowed when accessing the index when `AllowRowLocks = TRUE` or `DisAllowRowLocks = FALSE`, and `AllowPageLocks = TRUE` or `DisAllowPageLocks = FALSE`. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] chooses the appropriate lock and can escalate the lock from a row or page lock to a table lock.

Only a table-level lock is allowed when accessing the index when `AllowRowLocks = FALSE` or `DisAllowRowLocks = TRUE` and `AllowPageLocks = FALSE` or `DisAllowPageLocks = TRUE`.

If a table name is specified with no index, the settings are applied to all indexes on that table. When the underlying table has no clustered index (that is, it's a heap) the settings are applied as follows:

- When `AllowRowLocks` or `DisAllowRowLocks` are set to `TRUE` or `FALSE`, the setting is applied to the heap and any associated nonclustered indexes.

- When `AllowPageLocks` option is set to `TRUE` or `DisAllowPageLocks` is set to `FALSE`, the setting is applied to the heap and any associated nonclustered indexes.

- When `AllowPageLocks` option is set `FALSE` or `DisAllowPageLocks` is set to `TRUE`, the setting is fully applied to the nonclustered indexes. That is, all page locks are disallowed on the nonclustered indexes. On the heap, only the shared (S), update (U), and exclusive (X) locks for the page are disallowed. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] can still acquire an intent page lock (IS, IU or IX) for internal purposes.

## Permissions

Requires `ALTER` permission on the table.

## Examples

### A. Set an option on a specific index

The following example disallows page locks on the `IX_Customer_TerritoryID` index on the `Customer` table.

```sql
USE AdventureWorks2022;
GO

EXEC sp_indexoption N'Sales.Customer.IX_Customer_TerritoryID',
    N'disallowpagelocks',
    TRUE;
```

### B. Set an option on all indexes on a table

The following example disallows row locks on all indexes associated with the `Product` table. The `sys.indexes` catalog view is queried before and after executing the `sp_indexoption` procedure to show the results of the statement.

```sql
USE AdventureWorks2022;
GO

--Display the current row and page lock options for all indexes on the table.
SELECT name,
    type_desc,
    allow_row_locks,
    allow_page_locks
FROM sys.indexes
WHERE object_id = OBJECT_ID(N'Production.Product');
GO

-- Set the disallowrowlocks option on the Product table.
EXEC sp_indexoption N'Production.Product',
    N'disallowrowlocks',
    TRUE;
GO

--Verify the row and page lock options for all indexes on the table.
SELECT name,
    type_desc,
    allow_row_locks,
    allow_page_locks
FROM sys.indexes
WHERE object_id = OBJECT_ID(N'Production.Product');
GO
```

### C. Set an option on a table with no clustered index

The following example disallows page locks on a table with no clustered index (a heap). The `sys.indexes` catalog view is queried before and after the `sp_indexoption` procedure is executed to show the results of the statement.

```sql
USE AdventureWorks2022;
GO

--Display the current row and page lock options of the table.
SELECT OBJECT_NAME(object_id) AS [Table],
    type_desc,
    allow_row_locks,
    allow_page_locks
FROM sys.indexes
WHERE OBJECT_NAME(object_id) = N'DatabaseLog';
GO

-- Set the disallowpagelocks option on the table.
EXEC sp_indexoption DatabaseLog,
    N'disallowpagelocks',
    TRUE;
GO

--Verify the row and page lock settings of the table.
SELECT OBJECT_NAME(object_id) AS [Table],
    allow_row_locks,
    allow_page_locks
FROM sys.indexes
WHERE OBJECT_NAME(object_id) = N'DatabaseLog';
GO
```

## Related content

- [INDEXPROPERTY (Transact-SQL)](../../t-sql/functions/indexproperty-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.indexes (Transact-SQL)](../system-catalog-views/sys-indexes-transact-sql.md)
