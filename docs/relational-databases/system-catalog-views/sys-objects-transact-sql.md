---
title: "sys.objects (Transact-SQL)"
description: Contains a row for each user-defined, schema-scoped object that is created within a database, including natively compiled scalar user-defined functions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/12/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.objects_TSQL"
  - "objects"
  - "sys.objects"
  - "objects_TSQL"
helpviewer_keywords:
  - "sys.objects catalog view"
  - "table-valued parameters, sys.objects catalog view"
  - "user-defined table types [SQL Server]"
  - "table types [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sys.objects (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Contains a row for each user-defined, schema-scoped object that is created within a database, including natively compiled scalar user-defined functions.

For more information, see [Scalar User-Defined Functions for In-Memory OLTP](../in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md).

> [!NOTE]  
> `sys.objects` doesn't show DDL triggers, because they aren't schema-scoped. All triggers, both DML and DDL, are found in [sys.triggers](sys-triggers-transact-sql.md). `sys.triggers` supports a mixture of name-scoping rules for the various kinds of triggers.

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Object name. |
| `object_id` | **int** | Object identification number. Is unique within a database. |
| `principal_id` | **int** | ID of the individual owner, if different from the schema owner. By default, schema-contained objects are owned by the schema owner. However, an alternate owner can be specified by using the `ALTER AUTHORIZATION` statement to change ownership.<br /><br />`NULL` if there's no alternate individual owner.<br /><br />Is `NULL` if the object type is one of the following values:<br /><br />`C` = Check constraint<br />`D` = Default (constraint or stand-alone)<br />`F` = Foreign key constraint<br />`PK` = Primary key constraint<br />`R` = Rule (old-style, stand-alone)<br />`TA` = Assembly (CLR-integration) trigger<br />`TR` = SQL trigger<br />`UQ` = Unique constraint<br />`EC` = Edge constraint |
| `schema_id` | **int** | ID of the schema that the object is contained in.<br /><br />Schema-scoped system objects are always contained in the sys or `INFORMATION_SCHEMA` schemas. |
| `parent_object_id` | **int** | ID of the object to which this object belongs.<br /><br />`0` = Not a child object. |
| `type` | **char(2)** | Object type:<br /><br />`AF` = Aggregate function (CLR)<br />`C` = Check constraint<br />`D` = Default (constraint or stand-alone)<br />`F` = Foreign key constraint<br />`FN` = SQL scalar function<br />`FS` = Assembly (CLR) scalar-function<br />`FT` = Assembly (CLR) table-valued function<br />`IF` = SQL inline table-valued function (TVF)<br />`IT` = Internal table<br />`P` = SQL stored procedure<br />`PC` = Assembly (CLR) stored-procedure<br />`PG` = Plan guide<br />`PK` = Primary key constraint<br />`R` = Rule (old-style, stand-alone)<br />`RF` = Replication-filter-procedure<br />`S` = System base table<br />`SN` = Synonym<br />`SO` = Sequence object<br />`U` = Table (user-defined)<br />`V` = View<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions<br /><br />`SQ` = Service queue<br />`TA` = Assembly (CLR) DML trigger<br />`TF` = SQL table-valued-function (TVF)<br />`TR` = SQL DML trigger<br />`TT` = Table type<br />`UQ` = unique constraint<br />`X` = Extended stored procedure<br /><br />**Applies to**: [!INCLUDE [sssql14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]<br /><br />`ST` = Statistics tree<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]<br /><br />`ET` = External table<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]<br /><br />`EC` = Edge constraint |
| `type_desc` | **nvarchar(60)** | Description of the object type:<br /><br />`AGGREGATE_FUNCTION`<br />`CHECK_CONSTRAINT`<br />`CLR_SCALAR_FUNCTION`<br />`CLR_STORED_PROCEDURE`<br />`CLR_TABLE_VALUED_FUNCTION`<br />`CLR_TRIGGER`<br />`DEFAULT_CONSTRAINT`<br />`EDGE_CONSTRAINT`<br />`EXTENDED_STORED_PROCEDURE`<br />`FOREIGN_KEY_CONSTRAINT`<br />`INTERNAL_TABLE`<br />`PLAN_GUIDE`<br />`PRIMARY_KEY_CONSTRAINT`<br />`REPLICATION_FILTER_PROCEDURE`<br />`RULE`<br />`SEQUENCE_OBJECT`<br />`SERVICE_QUEUE`<br />`SQL_INLINE_TABLE_VALUED_FUNCTION`<br />`SQL_SCALAR_FUNCTION`<br />`SQL_STORED_PROCEDURE`<br />`SQL_TABLE_VALUED_FUNCTION`<br />`SQL_TRIGGER`<br />`SYNONYM`<br />`SYSTEM_TABLE`<br />`TYPE_TABLE`<br />`UNIQUE_CONSTRAINT`<br />`USER_TABLE`<br />`VIEW` |
| `create_date` | **datetime** | Date the object was created. |
| `modify_date` | **datetime** | Date the object was last modified by using an `ALTER` statement. If the object is a table or a view, modify_date also changes when an index on the table or view is created or altered. |
| `is_ms_shipped` | **bit** | Object is created by an internal [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] component. |
| `is_published` | **bit** | Object is published. |
| `is_schema_published` | **bit** | Only the schema of the object is published. |

## Remarks

You can apply the [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md), [OBJECT_NAME](../../t-sql/functions/object-name-transact-sql.md), and [OBJECTPROPERTY](../../t-sql/functions/objectproperty-transact-sql.md) built-in functions to the objects shown in `sys.objects`.

There's a version of this view with the same schema, called [sys.system_objects](sys-system-objects-transact-sql.md), that shows system objects. Another view, called [sys.all_objects](sys-all-objects-transact-sql.md), shows both system and user objects. All three catalog views have the same structure.

In this version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], an extended index, such as an XML index or spatial index, is considered an internal table in `sys.objects` (`type` is `IT`, and `type_desc` is `INTERNAL_TABLE`). For an extended index:

- `name` is the internal name of the index table
- `parent_object_id` is the `object_id` of the base table
- `is_ms_shipped`, `is_published`, and `is_schema_published` columns are set to `0`

## Related system views

Subsets of the objects can be viewed by using system views for a specific type of object, such as:

- [sys.tables](sys-tables-transact-sql.md)
- [sys.views](sys-views-transact-sql.md)
- [sys.procedures](sys-procedures-transact-sql.md)

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

### A. Return all the objects that were modified in the last N days

Before you run the following query, replace `<database_name>` and `<n_days>` with valid values.

```sql
USE <database_name>;
GO

SELECT name AS object_name,
    SCHEMA_NAME(schema_id) AS schema_name,
    type_desc,
    create_date,
    modify_date
FROM sys.objects
WHERE modify_date > GETDATE() - <n_days>
ORDER BY modify_date;
GO
```

### B. Return the parameters for a specified stored procedure or function

Before you run the following query, replace `<database_name>` and `<schema_name.object_name>` with valid names.

```sql
USE <database_name>;
GO

SELECT SCHEMA_NAME(schema_id) AS schema_name,
    o.name AS object_name,
    o.type_desc,
    p.parameter_id,
    p.name AS parameter_name,
    TYPE_NAME(p.user_type_id) AS parameter_type,
    p.max_length,
    p.precision,
    p.scale,
    p.is_output
FROM sys.objects AS o
INNER JOIN sys.parameters AS p
    ON o.object_id = p.object_id
WHERE o.object_id = OBJECT_ID('<schema_name.object_name>')
ORDER BY schema_name,
    object_name,
    p.parameter_id;
GO
```

### C. Return all the user-defined functions in a database

Before you run the following query, replace `<database_name>` with a valid database name.

```sql
USE <database_name>;
GO

SELECT name AS function_name,
    SCHEMA_NAME(schema_id) AS schema_name,
    type_desc,
    create_date,
    modify_date
FROM sys.objects
WHERE type_desc LIKE '%FUNCTION%';
GO
```

### D. Return the owner of each object in a schema

Before you run the following query, replace all occurrences of `<database_name>` and `<schema_name>` with valid names.

```sql
USE <database_name>;
GO

SELECT 'OBJECT' AS entity_type,
    USER_NAME(OBJECTPROPERTY(object_id, 'OwnerId')) AS owner_name,
    name
FROM sys.objects
WHERE SCHEMA_NAME(schema_id) = '<schema_name>'

UNION

SELECT 'TYPE' AS entity_type,
    USER_NAME(TYPEPROPERTY(SCHEMA_NAME(schema_id) + '.' + name, 'OwnerId')) AS owner_name,
    name
FROM sys.types
WHERE SCHEMA_NAME(schema_id) = '<schema_name>'

UNION

SELECT 'XML SCHEMA COLLECTION' AS entity_type,
    COALESCE(USER_NAME(xsc.principal_id), USER_NAME(s.principal_id)) AS owner_name,
    xsc.name
FROM sys.xml_schema_collections AS xsc
INNER JOIN sys.schemas AS s
    ON s.schema_id = xsc.schema_id
WHERE s.name = '<schema_name>';
GO
```

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.all_objects (Transact-SQL)](sys-all-objects-transact-sql.md)
- [sys.system_objects (Transact-SQL)](sys-system-objects-transact-sql.md)
- [sys.triggers (Transact-SQL)](sys-triggers-transact-sql.md)
- [Object Catalog Views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [sys.internal_tables (Transact-SQL)](sys-internal-tables-transact-sql.md)
