---
title: "CREATE TYPE (Transact-SQL)"
description: Creates an alias data type or a user-defined type in the current database in SQL Server or Azure SQL Database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "sql13.swb.sysdatatype.properties.f1"
  - "CREATE TYPE"
  - "TYPE_TSQL"
  - "TYPE"
  - "CREATE_TYPE_TSQL"
helpviewer_keywords:
  - "UDTs [SQL Server], creating"
  - "CLR user-defined types [SQL Server]"
  - "user-defined table types [SQL Server]"
  - "user-defined types [SQL Server], creating"
  - "CREATE TYPE statement"
  - "alias data types [SQL Server], creating"
  - "data types [SQL Server], creating"
dev_langs:
  - "TSQL"
---
# CREATE TYPE (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates an alias data type or a user-defined type in the current database in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. The implementation of an alias data type is based on a [!INCLUDE [ssde-md](../../includes/ssde-md.md)] native system type. A user-defined type is implemented through a class of an assembly in the [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR). To bind a user-defined type to its implementation, the CLR assembly that contains the implementation of the type must first be registered in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] by using [CREATE ASSEMBLY](../../t-sql/statements/create-assembly-transact-sql.md).

The ability to run CLR code is off by default in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can create, modify, and drop database objects that reference managed code modules. However, these references don't execute in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] unless the [clr enabled Option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md) is enabled by using [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

> [!NOTE]  
> The integration of .NET Framework CLR into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is discussed in this topic. CLR integration doesn't apply to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

User-defined data type syntax:

```syntaxsql
CREATE TYPE [ schema_name. ] type_name
{
      FROM base_type
      [ ( precision [ , scale ] ) ]
      [ NULL | NOT NULL ]
    | EXTERNAL NAME assembly_name [ .class_name ]
    | AS TABLE ( { <column_definition> | <computed_column_definition> [ , ...n ]
      [ <table_constraint> ] [ , ...n ]
      [ <table_index> ] [ , ...n ] } )
} [ ; ]

<column_definition> ::=
column_name <data_type>
    [ COLLATE collation_name ]
    [ NULL | NOT NULL ]
    [
        DEFAULT constant_expression ]
      | [ IDENTITY [ ( seed , increment ) ]
    ]
    [ ROWGUIDCOL ] [ <column_constraint> [ ...n ] ]

<data type> ::=
[ type_schema_name . ] type_name
    [ ( precision [ , scale ] | max |
                [ { CONTENT | DOCUMENT } ] xml_schema_collection ) ]

<column_constraint> ::=
{ { PRIMARY KEY | UNIQUE }
        [ CLUSTERED | NONCLUSTERED ]
        [
            WITH ( <index_option> [ , ...n ] )
        ]
  | CHECK ( logical_expression )
}

<computed_column_definition> ::=
column_name AS computed_column_expression
[ PERSISTED [ NOT NULL ] ]
[
    { PRIMARY KEY | UNIQUE }
        [ CLUSTERED | NONCLUSTERED ]
        [
            WITH ( <index_option> [ , ...n ] )
        ]
    | CHECK ( logical_expression )
]

<table_constraint> ::=
{
    { PRIMARY KEY | UNIQUE }
        [ CLUSTERED | NONCLUSTERED ]
    ( column [ ASC | DESC ] [ , ...n ] )
        [
    WITH ( <index_option> [ , ...n ] )
        ]
    | CHECK ( logical_expression )
}

<index_option> ::=
{
    IGNORE_DUP_KEY = { ON | OFF }
}

< table_index > ::=
  INDEX index_name
     [ CLUSTERED | NONCLUSTERED ] (column [ ASC | DESC ] [ , ...n ] )
     [INCLUDE (column, ...n)]
```

User-defined memory optimized table types syntax:

```syntaxsql
CREATE TYPE [ schema_name. ] type_name
AS TABLE ( { <column_definition> [ , ...n ] }
    | [ <table_constraint> ] [ , ...n ]
    | [ <table_index> ] [ , ...n ] )
    [ WITH ( <table_option> [ , ...n ] ) ]
 [ ; ]

<column_definition> ::=
column_name <data_type>
    [ COLLATE collation_name ] [ NULL | NOT NULL ]
      [ IDENTITY [ (1 , 1) ]
    ]
    [ <column_constraint> [ , ...n ] ] [ <column_index> ]

<data type> ::=
 [ type_schema_name . ] type_name [ ( precision [ , scale ] ) ]

<column_constraint> ::=
{ PRIMARY KEY { NONCLUSTERED HASH WITH ( BUCKET_COUNT = bucket_count )
                | NONCLUSTERED }
}

< table_constraint > ::=
{ PRIMARY KEY { NONCLUSTERED HASH (column [ , ...n ] )
                   WITH ( BUCKET_COUNT = bucket_count )
               | NONCLUSTERED ( column [ ASC | DESC ] [ , ...n ] )
           }
}

<column_index> ::=
  INDEX index_name
{ [ NONCLUSTERED ] HASH (column [ , ...n ] ) WITH ( BUCKET_COUNT = bucket_count ) 
      | NONCLUSTERED ( column [ ASC | DESC ] [ , ...n ] )
}

< table_index > ::=
  INDEX index_name
{ [ NONCLUSTERED ] HASH (column [ , ...n ] ) WITH ( BUCKET_COUNT = bucket_count )
    | [ NONCLUSTERED ] ( column [ ASC | DESC ] [ , ...n ] )
}

<table_option> ::=
{
    [ MEMORY_OPTIMIZED = { ON | OFF } ]
}
```

## Arguments

#### *schema_name*

The name of the schema to which the alias data type or user-defined type belongs.

#### *type_name*

The name of the alias data type or user-defined type. Type names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

#### *base_type*

The [!INCLUDE [ssde-md](../../includes/ssde-md.md)] supplied data type on which the alias data type is based. *base_type* is **sysname**, with no default, and can be one of the following values:

- **bigint**, **int**, **smallint**, and **tinyint**
- **binary(*n*)**, **varbinary(*n*)**, and **varbinary(max)**
- **bit**
- **char(*n*)**, **nchar(*n*)**, **nvarchar(*n*)**, **nvarchar(max)**, **varchar(*n*)**, and **varchar(max)**
- **date**, **datetime**, **datetime2**, **datetimeoffset**, **smalldatetime**, and **time**
- **decimal** and **numeric**
- **float** and **real**
- **image**
- **money** and **smallmoney**
- **sql_variant**
- **text** and **ntext**
- **uniqueidentifier**

*base_type* can also be any data type synonym that maps to one of these system data types.

#### *precision*

For **decimal** or **numeric**, *precision* is a non-negative integer that indicates the maximum total number of decimal digits that can be stored, both to the left and to the right of the decimal point. For more information, see [decimal and numeric (Transact-SQL)](../data-types/decimal-and-numeric-transact-sql.md).

#### *scale*

For **decimal** or **numeric**, *scale* is a non-negative integer that indicates the maximum number of decimal digits that can be stored to the right of the decimal point, and it must be less than or equal to the precision. For more information, see [decimal and numeric (Transact-SQL)](../data-types/decimal-and-numeric-transact-sql.md).

#### NULL | NOT NULL

Specifies whether the type can hold a null value. If not specified, `NULL` is the default.

#### *assembly_name*

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]

Specifies the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] assembly that references the implementation of the user-defined type in the common language runtime. *assembly_name* should match an existing assembly in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in the current database.

> [!NOTE]  
> `EXTERNAL_NAME` isn't available in a contained database.

#### [ . *class_name* ]

**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]

Specifies the class within the assembly that implements the user-defined type. *class_name* must be a valid identifier and must exist as a class in the assembly with assembly visibility. *class_name* is case-sensitive, regardless of the database collation, and must exactly match the class name in the corresponding assembly. The class name can be a namespace-qualified name enclosed in square brackets (**[ ]**) if the programming language that is used to write the class uses the concept of namespaces, such as C#. If *class_name* isn't specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] assumes it's the same as *type_name*.

#### \<column_definition>

Defines the columns for a user-defined table type.

#### \<data type>

Defines the data type in a column for a user-defined table type. For more information about data types, see [Data Types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md). For more information about tables, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md).

#### \<column_constraint>

Defines the column constraints for a user-defined table type. Supported constraints include `PRIMARY KEY`, `UNIQUE`, and `CHECK`. For more information about tables, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md).

#### \<computed_column_definition>

Defines a computed column expression as a column in a user-defined table type. For more information about tables, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md).

#### \<table_constraint>

Defines a table constraint on a user-defined table type. Supported constraints include `PRIMARY KEY`, `UNIQUE`, and `CHECK`.

#### \<index_option>

Specifies the error response to duplicate key values in a multiple-row insert operation on a unique clustered or unique nonclustered index. For more information about index options, see [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md).

#### INDEX *index_name* [ CLUSTERED | NONCLUSTERED ] ( *column_name* [ ASC | DESC ] [ , ...n ] )

**Applies to**: [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

Specifies to create an index on the table. This can be a clustered index, or a nonclustered index. The index contains the columns listed, and sorts the data in either ascending or descending order.

#### INDEX

You must specify column and table indexes as part of the `CREATE TABLE` statement. `CREATE INDEX` and `DROP INDEX` aren't supported for memory-optimized tables.

#### MEMORY_OPTIMIZED

**Applies to**: [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]. [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] doesn't support memory optimized tables in General Purpose tier.

Indicates whether the table type is memory optimized. This option is off by default; the table (type) isn't a memory optimized table (type). Memory optimized table types are memory-optimized user tables, the schema of which is persisted on disk similar to other user tables.

#### BUCKET_COUNT

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

Indicates the number of buckets that should be created in the hash index. The maximum value for `BUCKET_COUNT` in hash indexes is 1,073,741,824. For more information about bucket counts, see [Indexes on Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md). *bucket_count* is a required argument.

#### HASH

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

Indicates that a `HASH` index is created. Hash indexes are supported only on memory optimized tables.

## Remarks

The class of the assembly that is referenced in *assembly_name*, together with its methods, should satisfy all the requirements for implementing a user-defined type in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about these requirements, see [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).

Additional considerations include the following:

- The class can contain overloaded methods, but these methods can be called only from within managed code, not from [!INCLUDE [tsql](../../includes/tsql-md.md)].

- Any static members must be declared as **const** or **readonly** if *assembly_name* is `SAFE` or `EXTERNAL_ACCESS`.

Within a database, there can be only one user-defined type registered against any specified type that has been uploaded in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from the CLR. If a user-defined type is created on a CLR type for which a user-defined type already exists in the database, `CREATE TYPE` fails with an error. This restriction is required to avoid ambiguity during SQL Type resolution if a CLR type can be mapped to more than one user-defined type.

If any mutator method in the type doesn't return *void*, the `CREATE TYPE` statement doesn't execute.

To modify a user-defined type, you must drop the type by using a `DROP TYPE` statement and then re-create it.

Unlike user-defined types that are created by using `sp_addtype`, the **public** database role isn't automatically granted `REFERENCES` permission on types that are created by using `CREATE TYPE`. This permission must be granted separately.

In user-defined table types, structured user-defined types that are used in *column_name* \<data type> are part of the database schema scope in which the table type is defined. To access structured user-defined types in a different scope within the database, use two-part names.

In user-defined table types, the primary key on computed columns must be `PERSISTED` and `NOT NULL`.

## Memory-optimized table types

Beginning in [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], processing data in a table type can be done in primary memory, and not on disk. For more information, see [In-Memory OLTP overview and usage scenarios](../../relational-databases/in-memory-oltp/overview-and-usage-scenarios.md). For code samples showing how to create memory-optimized table types, see [Creating a Memory-Optimized Table and a Natively Compiled Stored Procedure](../../relational-databases/in-memory-oltp/creating-a-memory-optimized-table-and-a-natively-compiled-stored-procedure.md).

## Permissions

Requires `CREATE TYPE` permission in the current database and `ALTER` permission on *schema_name*. If *schema_name* isn't specified, the default name resolution rules for determining the schema for the current user apply. If *assembly_name* is specified, a user must either own the assembly or have `REFERENCES` permission on it.

If any columns in the `CREATE TABLE` statement are defined to be of a user-defined type, `REFERENCES` permission on the user-defined type is required.

A user creating a table with a column that uses a user-defined type needs the `REFERENCES` permission on the user-defined type. If this table must be created in `tempdb`, then either the `REFERENCES` permission needs to be granted explicitly each time *before* the table is created, or this data type and `REFERENCES` permission need to be added to the `model` database. For example:

```sql
CREATE TYPE dbo.udt_money FROM varchar(11) NOT NULL;
GO
GRANT REFERENCES ON TYPE::dbo.udt_money TO public
```

If this is done, then this data type and `REFERENCES` permission will be available in `tempdb` permanently. Otherwise, the user-defined data type and permissions will disappear when [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is restarted. For more information, see [CREATE TABLE](create-table-transact-sql.md#permissions-1).

If you don't want every new database to inherit the definition and permissions for this user- defined data type from model, you can use a startup stored procedure to create and assign the appropriate permissions only in `tempdb` database. For example:

```sql
USE master
GO
CREATE PROCEDURE setup_udt_in_tempdb
AS
EXEC ( 'USE tempdb;
CREATE TYPE dbo.udt_money FROM varchar(11) NOT NULL;
GRANT REFERENCES ON TYPE::dbo.udt_money TO public;')
GO
EXEC sp_procoption 'setup_udt_in_tempdb' , 'startup' , 'on'
GO
```

Alternatively, instead of using temporary tables, consider using table variables when you need to reference user-defined data types for temporary storage needs. For table variables to reference user-defined data types, you don't need to explicitly grant permissions for the user-defined data type.

## Examples

### A. Create an alias type based on the varchar data type

The following example creates an alias type based on the system-supplied `varchar` data type.

```sql
CREATE TYPE SSN
FROM VARCHAR(11) NOT NULL;
```

### B. Create a user-defined type

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]

The following example creates a type `Utf8String` that references class `utf8string` in the assembly `utf8string`. Before creating the type, assembly `utf8string` is registered in the local database. Replace the binary portion of the `CREATE ASSEMBLY` statement with a valid description.

```sql
CREATE ASSEMBLY utf8string
AUTHORIZATION [dbi]
FROM 0x4D... ;
GO

CREATE TYPE Utf8String
EXTERNAL NAME utf8string.[Microsoft.Samples.SqlServer.utf8string];
GO
```

### <a id="c-creating-a-user-defined-table-type"></a> C. Create a user-defined table type

The following example creates a user-defined table type that has two columns. For more information about how to create and use table-valued parameters, see [Use Table-Valued Parameters (Database Engine)](../../relational-databases/tables/use-table-valued-parameters-database-engine.md).

```sql
CREATE TYPE LocationTableType AS TABLE (
    LocationName VARCHAR(50),
    CostRate INT
);
GO
```

### D. Create a user-defined table type with primary key and index

The following example creates a user-defined table type that has three columns, one of which (`Name`) is the primary key and another (`Price`) has a nonclustered index. For more information about how to create and use table-valued parameters, see [Use Table-Valued Parameters (Database Engine)](../../relational-databases/tables/use-table-valued-parameters-database-engine.md).

```sql
CREATE TYPE InventoryItem AS TABLE (
    [Name] NVARCHAR(50) NOT NULL,
    SupplierId BIGINT NOT NULL,
    Price DECIMAL(18, 4) NULL,
    PRIMARY KEY (Name),
    INDEX IX_InventoryItem_Price(Price)
);
GO
```

## Related content

- [CREATE ASSEMBLY (Transact-SQL)](create-assembly-transact-sql.md)
- [DROP TYPE (Transact-SQL)](drop-type-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)
- [Working with User-Defined Types in SQL Server](../../relational-databases/clr-integration-database-objects-user-defined-types/working-with-user-defined-types-in-sql-server.md)
