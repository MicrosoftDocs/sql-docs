---
title: CREATE SYNONYM (Transact-SQL)
description: Creates a new synonym.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/12/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE_SYNONYM_TSQL"
  - "SYNONYM_TSQL"
  - "SYNONYM"
  - "CREATE SYNONYM"
helpviewer_keywords:
  - "alternate names [SQL Server]"
  - "names [SQL Server], synonyms"
  - "CREATE SYNONYM statement"
  - "synonyms [SQL Server], creating"
dev_langs:
  - "TSQL"
---
# CREATE SYNONYM (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates a new synonym.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

SQL Server syntax:

```syntaxsql
CREATE SYNONYM [ schema_name_1. ] synonym_name FOR <object>

<object> ::=
{
    [
        server_name. [ database_name ] . [ schema_name_2 ] .
        | database_name. [ schema_name_2 ] .
        | schema_name_2.
    ]
    object_name
}
```

Azure SQL Database syntax:

```syntaxsql
CREATE SYNONYM [ schema_name_1. ] synonym_name FOR <object>

<object> ::=
{
    [ database_name. [ schema_name_2 ] . | schema_name_2. ] object_name
}
```

## Arguments

#### *schema_name_1*

Specifies the schema in which the synonym is created. If *schema_name* isn't specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the default schema of the current user.

#### *synonym_name*

The name of the new synonym.

#### *server_name*

The name of the server on which base object is located.

#### *database_name*

The name of the database in which the base object is located. If *database_name* isn't specified, the name of the current database is used.

#### *schema_name_2*

The name of the schema of the base object. If *schema_name* isn't specified, the default schema of the current user is used.

#### *object_name*

The name of the base object that the synonym references.

> [!NOTE]  
> [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] supports the three-part name format `database_name.[schema_name].object_name` when the *database_name* is the current database, or the *database_name* is `tempdb` and the *object_name* starts with `#`.

## Remarks

The base object need not exist at synonym create time. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] checks for the existence of the base object at run time.

- Synonyms can be created for the following types of objects:

  - Assembly (CLR) stored procedure
  - Assembly (CLR) table-valued function
  - Assembly (CLR) scalar function
  - Assembly (CLR) aggregate functions
  - Replication-filter-procedure
  - Extended stored procedure
  - T-SQL scalar function
  - T-SQL table-valued function
  - T-SQL inline-table-valued function
  - T-SQL stored procedure
  - Table (user-defined, includes local and global temporary tables)
  - View

- Four-part names for function base objects aren't supported.

- Synonyms can be created, dropped and referenced in dynamic T-SQL.

- Synonyms are database-specific, and can't be accessed by other databases.

## Permissions

To create a synonym in a given schema, a user must have `CREATE SYNONYM` permission and either own the schema or have ALTER SCHEMA permission.

The `CREATE SYNONYM` permission is a grantable permission.

> [!NOTE]  
> You don't need permission on the base object to successfully compile the `CREATE SYNONYM` statement, because all permission checking on the base object is deferred until run time.

## Examples

### A. Create a synonym for a local object

The following example first creates a synonym for the base object, `Product` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, and then queries the synonym.

```sql
-- Create a synonym for the Product table in AdventureWorks2022.
CREATE SYNONYM MyProduct
FOR AdventureWorks2022.Production.Product;
GO

-- Query the Product table by using the synonym.
SELECT ProductID, Name
FROM MyProduct
WHERE ProductID < 5;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
ProductID   Name
----------- --------------------------
1           Adjustable Race
2           Bearing Ball
3           BB Ball Bearing
4           Headset Ball Bearings

(4 row(s) affected)
```

### B. Create a synonym to remote object

In the following example, the base object, `Contact`, resides on a remote server named `Server_Remote`.

```sql
EXEC sp_addlinkedserver Server_Remote;
GO
USE tempdb;
GO
CREATE SYNONYM MyEmployee FOR Server_Remote.AdventureWorks2022.HumanResources.Employee;
GO
```

### C. Create a synonym for a user-defined function

The following example creates a function named `dbo.OrderDozen` that increases order amounts to 12 units. The example then creates the synonym `dbo.CorrectOrder` for the `dbo.OrderDozen` function.

```sql
-- Creating the dbo.OrderDozen function
CREATE FUNCTION dbo.OrderDozen (@OrderAmt INT)
RETURNS INT
    WITH EXECUTE AS CALLER
AS
BEGIN
    IF @OrderAmt % 12 <> 0
    BEGIN
        SET @OrderAmt += 12 - (@OrderAmt % 12)
    END

    RETURN (@OrderAmt);
END;
GO

-- Using the dbo.OrderDozen function
DECLARE @Amt INT;

SET @Amt = 15;

SELECT @Amt AS OriginalOrder,
    dbo.OrderDozen(@Amt) AS ModifiedOrder;

-- Create a synonym dbo.CorrectOrder for the dbo.OrderDozen function.
CREATE SYNONYM dbo.CorrectOrder
FOR dbo.OrderDozen;
GO

-- Using the dbo.CorrectOrder synonym.
DECLARE @Amt INT;

SET @Amt = 15;

SELECT
    @Amt AS OriginalOrder,
    dbo.CorrectOrder(@Amt) AS ModifiedOrder;
```

## See also

- [DROP SYNONYM (Transact-SQL)](drop-synonym-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [GRANT (Transact-SQL)](grant-transact-sql.md)
- [Synonyms (Database Engine)](../../relational-databases/synonyms/synonyms-database-engine.md)

## Next steps

- [Create Synonyms](../../relational-databases/synonyms/create-synonyms.md)
