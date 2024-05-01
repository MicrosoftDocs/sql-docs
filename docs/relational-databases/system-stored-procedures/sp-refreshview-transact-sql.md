---
title: "sp_refreshview updates the metadata for the specified non-schema-bound view."
description: "sp_refreshview (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_refreshview"
  - "sp_refreshview_TSQL"
helpviewer_keywords:
  - "sp_refreshview"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_refreshview (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Updates the metadata for the specified non-schema-bound view. Persistent metadata for a view can become outdated because of changes to the underlying objects upon which the view depends.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_refreshview [ @viewname = ] 'viewname'
[ ; ]
```

## Arguments

#### [ @viewname = ] '*viewname*'

The name of the view. *@viewname* is **nvarchar**, with no default. *@viewname* can be a multipart identifier, but can only refer to views in the current database.

## Return code values

`0` (success) or a nonzero number (failure).

## Remarks

If a view isn't created with SCHEMABINDING, `sp_refreshview` should be run when changes are made to the objects underlying the view, which affect the definition of the view. Otherwise, the view might produce unexpected results when it is queried.

## Permissions

Requires ALTER permission on the view and REFERENCES permission on common language runtime (CLR) user-defined types and XML schema collections that are referenced by the view columns.

## Examples

### A. Update the metadata of a view

The following example refreshes the metadata for the view `Sales.vIndividualCustomer`.

```sql
USE AdventureWorks2022;
GO

EXECUTE sp_refreshview N'Sales.vIndividualCustomer';
```

### B. Create a script that updates all views that have dependencies on a changed object

Assume that the table `Person.Person` was changed in a way that would affect the definition of any views that are created on it. The following example creates a script that refreshes the metadata for all views that have a dependency on table `Person.Person`.

```sql
USE AdventureWorks2022;
GO
SELECT DISTINCT 'EXEC sp_refreshview ''' + name + ''''
FROM sys.objects AS so
INNER JOIN sys.sql_expression_dependencies AS sed
    ON so.object_id = sed.referencing_id
WHERE so.type = 'V' AND sed.referenced_id = OBJECT_ID('Person.Person');
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.sql_expression_dependencies (Transact-SQL)](../system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [sp_refreshsqlmodule (Transact-SQL)](sp-refreshsqlmodule-transact-sql.md)
