---
title: sys.sp_refreshview (Transact-SQL)
description: sp_refreshview updates the metadata for the specified non-schema-bound view.
author: vanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 10/13/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sp_refreshview"
  - "sp_refreshview_TSQL"
helpviewer_keywords:
  - "sp_refreshview"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.sp_refreshview (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

The `sys.sp_refreshview` system stored procedure updates the metadata for the specified non-schema-bound view. Persistent metadata for a view can become outdated because of changes to the underlying objects upon which the view depends.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_refreshview [ @viewname = ] N'viewname'
[ ; ]
```

## Arguments

#### [ @viewname = ] N'*viewname*'

The name of the view. *@viewname* is **nvarchar**, with no default. *@viewname* can be a multipart identifier, but can only refer to views in the current database.

## Return code values

`0` (success) or a nonzero number (failure).

## Remarks

If you don't create a view with `SCHEMABINDING`, run `sp_refreshview` when you make changes to the objects underlying the view that affect the definition of the view. Otherwise, the view might return unexpected results when you query it.

## Permissions

Requires `ALTER` permission on the view, and `REFERENCES` permission on common language runtime (CLR) user-defined types and XML schema collections that the view columns reference.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Update the metadata of a view

The following example refreshes the metadata for the view `Sales.vIndividualCustomer`.

```sql
USE AdventureWorks2025;
GO

EXECUTE sp_refreshview N'Sales.vIndividualCustomer';
```

### B. Create a script that updates all views that have dependencies on a changed object

Assume that the table `Person.Person` was changed in a way that affects the definition of any views on it. The following example creates a script that refreshes the metadata for all views that depend on the table `Person.Person`.

```sql
USE AdventureWorks2025;
GO

SELECT DISTINCT 'EXECUTE sp_refreshview ''' + name + ''''
FROM sys.objects AS so
     INNER JOIN sys.sql_expression_dependencies AS sed
         ON so.object_id = sed.referencing_id
WHERE so.type = 'V'
      AND sed.referenced_id = OBJECT_ID('Person.Person');
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.sql_expression_dependencies (Transact-SQL)](../system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [sys.sp_refreshsqlmodule (Transact-SQL)](sp-refreshsqlmodule-transact-sql.md)
