---
title: "sys.sql_modules (Transact-SQL)"
description: sys.sql_modules returns a row for each object that is a SQL language-defined module in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, srdjanmatin
ms.date: 06/29/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.sql_modules_TSQL"
  - "sql_modules"
  - "sql_modules_TSQL"
  - "sys.sql_modules"
helpviewer_keywords:
  - "sys.sql_modules catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.sql_modules (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns a row for each object that is a SQL language-defined module in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], including natively compiled scalar user-defined function. Objects of type `P`, `RF`, `V`, `TR`, `FN`, `IF`, `TF`, and `R` have an associated SQL module. Stand-alone defaults, objects of type `D`, also have a SQL module definition in this view. For a description of these types, see the `type` column in the [sys.objects](sys-objects-transact-sql.md) catalog view.

For more information, see [Scalar User-Defined Functions for In-Memory OLTP](../in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object of the containing object. Is unique within a database. |
| `definition` | **nvarchar(max)** | SQL text that defines this module. This value can also be obtained using the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) built-in function.<br /><br />`NULL` = Encrypted. |
| `uses_ansi_nulls` | **bit** | Module was created with `SET ANSI_NULLS ON`.<br /><br />Always `0` for rules and defaults. |
| `uses_quoted_identifier` | **bit** | Module was created with `SET QUOTED_IDENTIFIER ON`. |
| `is_schema_bound` | **bit** | Module was created with `SCHEMABINDING` option.<br /><br />Always contains a value of `1` for natively compiled stored procedures. |
| `uses_database_collation` | **bit** | `1` = Schema-bound module definition depends on the default-collation of the database for correct evaluation; otherwise, `0`. Such a dependency prevents changing the database's default collation. |
| `is_recompiled` | **bit** | Procedure was created `WITH RECOMPILE` option. |
| `null_on_null_input` | **bit** | Module was declared to produce a `NULL` output on any `NULL` input. |
| `execute_as_principal_id` | **Int** | ID of the `EXECUTE AS` database principal.<br /><br />`NULL` by default or if `EXECUTE AS CALLER`.<br />ID of the specified principal if `EXECUTE AS SELF` or `EXECUTE AS <principal>`.<br />`-2` = `EXECUTE AS OWNER`. |
| `uses_native_compilation` | **bit** | `0` = not natively compiled<br />`1` = is natively compiled<br /><br />The default value is `0`.<br /><br /> **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions. |
| `is_inlineable` | **bit** | Indicates whether the module can be inlined or not. Inlineability is based on the conditions specified in the [requirements](../user-defined-functions/scalar-udf-inlining.md#requirements).<br /><br />`0` = can't be inlined<br />`1` = can be inlined.<br /><br />For scalar user-defined functions (UDFs), the value is `1` if the UDF can be inlined, and `0` otherwise. It always contains a value of `1` for inline table-valued functions (TVFs), and `0` for all other module types.<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. |
| `inline_type` | **bit** | Indicates whether inlining is turned on for the module currently. <br />0 = inlining is turned off<br />1 = inlining is turned on.<br />For scalar user-defined functions (UDFs), the value is `1` if inlining is turned on (explicitly or implicitly). The value is always `1` for inline table-valued functions (TVFs), and `0` for other module types.<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. |
| `inline_eligibility_mask` | **tinyint** | Indicates which inlining technique is applicable for the module currently. <br />0 = no eligible inlining technique <br />1 = scalar UDF inlining <br />2 = inlining via expression block <br />3 = inlining via expression block or scalar UDF inlining. <br /> <br />For scalar user-defined functions (UDFs), a value of `1` or greater is only possible if inlining is turned on (explicitly or implicitly). The value is always `1` for inline table-valued functions (TVFs), and `0` for other module types.<br /><br />**Applies to**: [!INCLUDE [applies-to-version/fabricse-fabricdw](../../includes/fabric-se-dw.md)] only. |

## Remarks

The SQL expression for a `DEFAULT` constraint, object of type `D`, is found in the [sys.default_constraints](sys-default-constraints-transact-sql.md) catalog view. The SQL expression for a `CHECK` constraint, object of type `C`, is found in the [sys.check_constraints](sys-check-constraints-transact-sql.md) catalog view.

This information is also described in [sys.dm_db_uncontained_entities](../system-dynamic-management-views/sys-dm-db-uncontained-entities-transact-sql.md).

Renaming a stored procedure, function, view, or trigger doesn't change the name of the corresponding object in the definition column of the `sys.sql_modules` catalog view or the definition returned by the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) built-in function. For this reason, we recommend that you don't use `sp_rename` to rename these object types. Instead, drop and recreate the object with its new name. Learn more in [sp_rename](../system-stored-procedures/sp-rename-transact-sql.md).

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

The following example returns the `object_id`, schema name, object name, object type, and definition of each module in the current database.

```sql
SELECT sm.object_id,
       ss.[name] AS [schema],
       o.[name] AS object_name,
       o.[type],
       o.[type_desc],
       sm.[definition]
FROM sys.sql_modules AS sm
     INNER JOIN sys.objects AS o
         ON sm.object_id = o.object_id
     INNER JOIN sys.schemas AS ss
         ON o.schema_id = ss.schema_id
ORDER BY o.[type], ss.[name], o.[name];
```

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)

