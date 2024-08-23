---
title: sp_refreshsqlmodule (Transact-SQL)
description: sp_refreshsqlmodule updates the metadata for the specified non-schema-bound object.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_refreshsqlmodule_TSQL"
  - "sp_refreshsqlmodule"
helpviewer_keywords:
  - "metadata [SQL Server], stored procedures"
  - "metadata [SQL Server], triggers"
  - "metadata [SQL Server], views"
  - "triggers [SQL Server], refreshing metadata"
  - "views [SQL Server], refreshing metadata"
  - "sp_refreshsqlmodule"
  - "metadata [SQL Server], functions"
  - "stored procedures [SQL Server], refreshing metadata"
  - "user-defined functions [SQL Server], refreshing metadata"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest"
---
# sp_refreshsqlmodule (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-dedicated-poolonly](../../includes/applies-to-version/sql-asdb-asdbmi-asa-dedicated-poolonly.md)]

Updates the metadata for the specified non-schema-bound stored procedure, user-defined function, view, DML trigger, database-level DDL trigger, or server-level DDL trigger in the current database. Persistent metadata for these objects, such as data types of parameters, can become outdated because of changes to their underlying objects. For example, you might see an error like `The definition for user-defined data type 'typename' has changed`. Refreshing the metadata for the module that uses the type specified in the error might resolve the problem.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sp_refreshsqlmodule
    [ @name = ] N'name'
    [ , [ @namespace = ] { OBJECT | DATABASE_DDL_TRIGGER | SERVER_DDL_TRIGGER } ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the stored procedure, user-defined function, view, DML trigger, database-level DDL trigger, or server-level DDL trigger. *@name* is **nvarchar(776)**, with no default. *@name* can't be a common language runtime (CLR) stored procedure or a CLR function. *@name* can't be schema-bound. *@name* can be a multi-part identifier, but can only refer to objects in the current database.

#### [ @namespace = ] N'*namespace*'

The class of the specified module. *@namespace* is **nvarchar(20)**, with a default of `OBJECT`. When *@name* is a DDL trigger, *@namespace* is required. Valid inputs are `DATABASE_DDL_TRIGGER` and `SERVER_DDL_TRIGGER`.

## Return code values

`0` (success) or a nonzero number (failure).

## Remarks

`sp_refreshsqlmodule` should be run when changes are made to the objects underlying the module that affect its definition. Otherwise, the module might produce unexpected results when queried or invoked. To refresh a view, you can use either `sp_refreshsqlmodule` or `sp_refreshview` with the same results.

`sp_refreshsqlmodule` doesn't affect any permissions, extended properties, or `SET` options that are associated with the object.

To refresh a server-level DDL trigger, execute this stored procedure from the context of any database.

> [!NOTE]  
> Any signatures that are associated with the object are dropped when you run `sp_refreshsqlmodule`.

## Permissions

Requires `ALTER` permission on the module and `REFERENCES` permission on any CLR user-defined types and XML schema collections that are referenced by the object. Requires `ALTER ANY DATABASE DDL TRIGGER` permission in the current database when the specified module is a database-level DDL trigger. Requires `CONTROL SERVER` permission when the specified module is a server-level DDL trigger.

Additionally, for modules that are defined with the `EXECUTE AS` clause, `IMPERSONATE` permission is required on the specified principal. Generally, refreshing an object doesn't change its `EXECUTE AS` principal, unless the module was defined with `EXECUTE AS USER` and the user name of the principal now resolves to a different user than the user at the time the module was created.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Refresh a user-defined function

The following example refreshes a user-defined function. The example creates an alias data type, `mytype`, and a user-defined function, `to_upper`, that uses `mytype`. Then, `mytype` is renamed to `myoldtype`, and a new `mytype` is created that's a different definition. The `dbo.to_upper` function is refreshed so that it references the new implementation of `mytype`, instead of the old one.

In the first step, create an alias type.

```sql
USE AdventureWorks2022;
GO

IF EXISTS (SELECT 'mytype' FROM sys.types WHERE name = 'mytype')
DROP TYPE mytype;
GO

CREATE TYPE mytype FROM NVARCHAR(5);
GO

IF OBJECT_ID('dbo.to_upper', 'FN') IS NOT NULL
DROP FUNCTION dbo.to_upper;
GO

CREATE FUNCTION dbo.to_upper (@a mytype)
RETURNS mytype
    WITH ENCRYPTION
AS
BEGIN
    RETURN UPPER(@a);
END;
GO

SELECT dbo.to_upper('abcde');
GO
```

Next, increase the length of the alias type.

```sql
sp_rename 'mytype', 'myoldtype', 'userdatatype';
GO

CREATE TYPE mytype FROM NVARCHAR(10);
GO
```

The function parameter still uses the old type, and fails because of truncation.

```sql
SELECT name, TYPE_NAME(user_type_id)
FROM sys.parameters
WHERE object_id = OBJECT_ID('dbo.to_upper');
GO

SELECT dbo.to_upper('abcdefgh'); -- Fails because of truncation
GO
```

Refresh the function to bind to the renamed type.

```sql
EXEC sys.sp_refreshsqlmodule 'dbo.to_upper';
```

The function parameters are now bound to the correct type and the statement works correctly.

```sql
SELECT name, TYPE_NAME(user_type_id)
FROM sys.parameters
WHERE object_id = OBJECT_ID('dbo.to_upper');
GO

SELECT dbo.to_upper('abcdefgh');
GO
```

### B. Refresh a database-level DDL trigger

The following example refreshes a database-level DDL trigger.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_refreshsqlmodule
    @name = 'ddlDatabaseTriggerLog',
    @namespace = 'DATABASE_DDL_TRIGGER';
GO
```

### C. Refresh a server-level DDL trigger

The following example refreshes a server-level DDL trigger.

```sql
USE master;
GO

EXEC sys.sp_refreshsqlmodule
    @name = 'ddl_trig_database',
    @namespace = 'SERVER_DDL_TRIGGER';
GO
```

## Related content

- [sp_refreshview (Transact-SQL)](sp-refreshview-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
