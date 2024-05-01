---
title: "sp_stored_procedures (Transact-SQL)"
description: sp_stored_procedures returns a list of stored procedures in the current environment.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_stored_procedures_TSQL"
  - "sp_stored_procedures"
helpviewer_keywords:
  - "sp_stored_procedures"
dev_langs:
  - "TSQL"
---
# sp_stored_procedures (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a list of stored procedures in the current environment.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_stored_procedures
    [ [ @sp_name = ] N'sp_name' ]
    [ , [ @sp_owner = ] N'sp_owner' ]
    [ , [ @sp_qualifier = ] N'sp_qualifier' ]
    [ , [ @fUsePattern = ] fUsePattern ]
[ ; ]
```

## Arguments

#### [ @sp_name = ] N'*sp_name*'

The name of the procedure used to return catalog information. *@sp_name* is **nvarchar(390)**, with a default of `NULL`. Wildcard pattern matching is supported.

#### [ @sp_owner = ] N'*sp_owner*'

The name of the schema to which the procedure belongs. *@sp_owner* is **nvarchar(384)**, with a default of `NULL`. Wildcard pattern matching is supported. If *@sp_owner* isn't specified, the default procedure visibility rules of the underlying database management system (DBMS) apply.

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the current schema contains a procedure with the specified name, that procedure is returned. If a nonqualified stored procedure is specified, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] searches for the procedure in the following order:

- The `sys` schema of the current database.

- The caller's default schema if executed in a batch or in dynamic SQL; or, if the non-qualified procedure name appears inside the body of another procedure definition, the schema containing this other procedure is searched next.

- The `dbo` schema in the current database.

#### [ @sp_qualifier = ] N'*sp_qualifier*'

The name of the procedure qualifier. *@sp_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for tables in the form `<qualifier>.<schema>.<name>`. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], *@sp_qualifier* represents the database name. In some products, it represents the server name of the database environment of the table.

#### [ @fUsePattern = ] *fUsePattern*

Determines whether the underscore (`_`), percent (`%`), or brackets (`[` and `]`) are interpreted as wildcard characters. *@fUsePattern* is **bit**, with a default of `1`.

- `0` = Pattern matching is off.
- `1` = Pattern matching is on.

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `PROCEDURE_QUALIFIER` | **sysname** | Procedure qualifier name. This column can be `NULL`. |
| `PROCEDURE_OWNER` | **sysname** | Procedure owner name. This column always returns a value. |
| `PROCEDURE_NAME` | **nvarchar(134)** | Procedure name. This column always returns a value. |
| `NUM_INPUT_PARAMS` | **int** | Reserved for future use. |
| `NUM_OUTPUT_PARAMS` | **int** | Reserved for future use. |
| `NUM_RESULT_SETS` | **int** | Reserved for future use. |
| `REMARKS` | **varchar(254)** | Description of the procedure. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't return a value for this column. |
| `PROCEDURE_TYPE` | **smallint** | Procedure type. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] always returns 2.0. This value can be one of the following options:<br /><br />0 = `SQL_PT_UNKNOWN`<br />1 = `SQL_PT_PROCEDURE`<br />2 = `SQL_PT_FUNCTION` |

## Remarks

For maximum interoperability, the gateway client should assume only SQL standard pattern matching, namely the percent (`%`) and underscore (`_`) wildcard characters.

The permission information about execute access to a specific stored procedure for the current user isn't necessarily checked; therefore, access isn't guaranteed. Only three-part naming is used. This means that only local stored procedures, not remote stored procedures (which require four-part naming), are returned when they're executed against [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If the server attribute `ACCESSIBLE_SPROC` is Y in the result set for `sp_server_info`, only stored procedures that can be executed by the current user are returned.

`sp_stored_procedures` is equivalent to `SQLProcedures` in ODBC. The results returned are ordered by `PROCEDURE_QUALIFIER`, `PROCEDURE_OWNER`, and `PROCEDURE_NAME`.

## Permissions

Requires `SELECT` permission on the schema.

## Examples

### A. Return all stored procedures in the current database

The following example returns all stored procedures in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_stored_procedures;
```

### B. Return a single stored procedure

The following example returns a result set for the `uspLogError` stored procedure.

```sql
USE AdventureWorks2022;
GO

sp_stored_procedures N'uspLogError',
    N'dbo',
    N'AdventureWorks2022',
    1;
```

## Related content

- [Catalog stored procedures (Transact-SQL)](catalog-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
