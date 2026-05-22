---
title: "CREATE FUNCTION (Microsoft Fabric, Azure Synapse Analytics)"
description: User-defined functions accept parameters, perform an action, such as a complex calculation, and return the result of that action as a value.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, srdjanmatin
ms.date: 12/01/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || =fabric"
---
# CREATE FUNCTION

::: moniker range="=fabric"

[!INCLUDE [applies-to-version/fabricse-fabricdw](../../includes/applies-to-version/fabric-se-dw.md)]

 `CREATE FUNCTION` can create inline table-value functions and scalar functions. 
 
> [!NOTE]
> Scalar UDFs are a preview feature in Fabric Data Warehouse.

> [!IMPORTANT]
> In Fabric Data Warehouse, to use a scalar udf in a query it must follow the [rules to allow inlining](../../relational-databases/user-defined-functions/scalar-udf-inlining.md#inlineable-scalar-udf-requirements) otherwise you will get an error `Scalar UDF execution is currently unavailable in this context.`.
Scalar UDFs that cannot be inlineable can be used outside of a query. You can check [whether a UDF can be inlined](#check-whether-a-scalar-udf-can-be-inlined).

A user-defined function is a [!INCLUDE [tsql](../../includes/tsql-md.md)] routine that accepts parameters, performs an action, such as a complex calculation, and returns the result of that action as a value. Scalar functions return a scalar value, such as a number or string. User-defined table-valued functions (TVFs) return a table.

Use `CREATE FUNCTION` to create a reusable T-SQL routine that can be used in these ways:

 - In [!INCLUDE [tsql](../../includes/tsql-md.md)] statements such as `SELECT` 
 - In [!INCLUDE [tsql](../../includes/tsql-md.md)] data manipulation statements (DML) such as `UPDATE`, `INSERT`, and `DELETE`
 - In applications calling the function
 - In the definition of another user-defined function
 - To replace a stored procedure

> [!TIP]
> You can specify `CREATE OR ALTER FUNCTION` to create a new function if one does not exist by that name, or alter an existing function, in a single statement.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

### Scalar function syntax

```syntaxsql
CREATE FUNCTION [ schema_name. ] function_name   
( [ { @parameter_name [ AS ] parameter_data_type   
    [ = default ] }   
    [ ,...n ]  
  ]  
)  
RETURNS return_data_type  
    [ WITH <function_option> [ ,...n ] ]  
    [ AS ]  
    BEGIN   
        function_body   
        RETURN scalar_expression  
    END  
[ ; ]  

<function_option>::=   
{  
    [ SCHEMABINDING ]  
  | [ RETURNS NULL ON NULL INPUT | CALLED ON NULL INPUT ]  
}  
```

### Inline table-valued function syntax

```syntaxsql
CREATE FUNCTION [ schema_name. ] function_name
( [ { @parameter_name [ AS ] parameter_data_type
    [ = default ] }
    [ ,...n ]
  ]
)
RETURNS TABLE
    [ WITH SCHEMABINDING ]
    [ AS ]
    RETURN [ ( ] select_stmt [ ) ]
[ ; ]
```

## Arguments

#### *schema_name*

The name of the schema to which the user-defined function belongs.

#### *function_name*

The name of the user-defined function. Function names must comply with the rules for identifiers and must be unique within the database and to its schema.

> [!NOTE]  
> Parentheses are required after the function name even if a parameter is not specified.  

#### @*parameter_name*

A parameter in the user-defined function. One or more parameters can be declared.

A function can have a maximum of 2,100 parameters. The value of each declared parameter must be supplied by the user when the function is executed, unless a default for the parameter is defined.

Specify a parameter name by using an at sign (`@`) as the first character. The parameter name must comply with the rules for identifiers. Parameters are local to the function; the same parameter names can be used in other functions. Parameters can take the place only of constants; they cannot be used instead of table names, column names, or the names of other database objects.

> [!NOTE]  
> `ANSI_WARNINGS` is not honored when you pass parameters in a stored procedure, user-defined function, or when you declare and set variables in a batch statement. For example, if a variable is defined as **char(3)**, and then set to a value larger than three characters, the data is truncated to the defined size and the INSERT or UPDATE statement succeeds.  

#### *parameter_data_type*

The parameter data type. For [!INCLUDE [tsql](../../includes/tsql-md.md)] functions, all [scalar data types supported](/fabric/data-warehouse/data-types) are allowed. 

#### [ = *default* ]

A default value for the parameter. If a *default* value is defined, the function can be executed without specifying a value for that parameter.

When a parameter of the function has a default value, the keyword `DEFAULT` must be specified when the function is called to retrieve the default value. This behavior is different from using parameters with default values in stored procedures in which omitting the parameter also implies the default value.

#### *return_data_type*

The return value of a scalar user-defined function. 

For functions in Fabric Data Warehouse, all data types are allowed except for **rowversion**/**timestamp**. Nonscalar types like **table** are not allowed.

#### *function_body*

A series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. 

In scalar functions, *function_body* is a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that together evaluate to a scalar value, which can include:

- Single statement expression
- Multi-statement expressions (`IF/THEN/ELSE` and `BEGIN/END` blocks)
- Local variables
- Calls to built-in SQL functions available
- Calls to other UDFs
- `SELECT` statements, and references to tables, views, and inline table-valued functions

#### *scalar_expression*

Specifies the scalar value that the scalar function returns.

#### *select_stmt*

The single `SELECT` statement that defines the return value of an inline table-valued function. For an inline table-valued function, there is no function body; the table is the result set of a single `SELECT` statement.

#### TABLE

Specifies that the return value of the table-valued function (TVF) is a table. Only constants and @*local_variables* can be passed to TVFs.

In inline TVFs (preview), the TABLE return value is defined through a single `SELECT` statement. Inline functions do not have associated return variables.

#### <function_option>

In Fabric Data Warehouse, the `INLINE`, `ENCRYPTION`, and `EXECUTE AS` keywords are not supported. 

The function options supported include:

SCHEMABINDING

 Specifies that the function is bound to the database objects that it references. When SCHEMABINDING is specified, the base objects cannot be modified in a way that would affect the function definition. The function definition itself must first be modified or dropped to remove dependencies on the object that is to be modified.  

The binding of the function to the objects it references is removed only when one of the following actions occurs:

-   The function is dropped.

-   The function is modified by using the ALTER statement with the SCHEMABINDING option not specified.

A function can be schema bound only if the following conditions are true:

-   Any user-defined functions referenced by the function are also schema-bound.

-   The objects referenced by the function are referenced using a two-part name.

-   Only built-in functions and other UDFs in the same database can be referenced within the body of UDFs.

-   The user who executed the `CREATE FUNCTION` statement has REFERENCES permission on the database objects that the function references.

To remove SCHEMABINDING, use `ALTER`.

RETURNS NULL ON NULL INPUT | **CALLED ON NULL INPUT**

 Specifies the `OnNULLCall` attribute of a scalar-valued function. If not specified, `CALLED ON NULL INPUT` is implied by default, and the function body executes even if `NULL` is passed as an argument.  

## Best practices

 - If a user-defined function is not created with schemabinding, changes that are made to underlying objects can affect the definition of the function and produce unexpected results when it is invoked. It is recommended to specify the `WITH SCHEMABINDING` clause when you are creating the function. This ensures that the objects referenced in the function definition cannot be modified unless the function is also modified.

 - Writing your user-defined functions to be inlineable. For more information, see [Scalar UDF inlining](#scalar-udf-inlining).

## Interoperability

### Inline table-valued user-defined functions

In an inline table-valued function, only a single select statement is allowed.

### Scalar user-defined functions

- The following statements are valid in a scalar-valued function:  
    -   Assignment statements
    -   Control-of-Flow statements except `TRY...CATCH` statements
    -   `DECLARE` statements defining local data variables

- The following built-in functions are not supported in a scalar-valued function body:
  - [NEWID()](../functions/newid-transact-sql.md)
  - [RAND()](../functions/rand-transact-sql.md)
  - [Configuration Functions](../functions/functions.md#configuration-functions)
  - [DATABASEPROPERTYEX](../functions/databasepropertyex-transact-sql.md) 
  - [OBJECTPROPERTYEX](../functions/objectpropertyex-transact-sql.md)
  - [SERVERPROPERTY](../functions/serverproperty-transact-sql.md) 
  - [NEXT VALUE FOR](../functions/next-value-for-transact-sql.md) 
  - [COLLATIONPROPERTY](../functions/collation-functions-collationproperty-transact-sql.md)
  - [HAS_PERMS_BY_NAME](../functions/has-perms-by-name-transact-sql.md)
  - [HAS_DBACCESS](../functions/has-dbaccess-transact-sql.md)

- Scalar UDFs can't be used in a `SELECT ... FROM` query on a user table when:
    - The UDF body contains a call to nondeterministic built-in function, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).
    - The UDF body contains a [common table expression (CTE)](../queries/with-common-table-expression-transact-sql.md).
    - The UDF body contains multi-statement UDF body beyond six `IF`-`THEN`-`ELSE` blocks.
    - The UDF body contains a WHILE LOOP
    - The UDF body cannot be inlined due to other reasons. For more information, see [Scalar UDF inlining requirements](/sql/relational-databases/user-defined-functions/scalar-udf-inlining?view=fabric&preserve-view=true#inlineable-scalar-udf-requirements).

- Scalar UDFs can't be used in a query when the:
    - UDF is directly called in a `GROUP BY` clause.
    - UDF is directly called in an `ORDER BY `clause.
    - calling query has a [common table expression (CTE)](../queries/with-common-table-expression-transact-sql.md).

- Recursive scalar UDFs are not supported.
- A User query can fail if more than 10 UDF calls are made in a single query.
- In some edge cases, the complexity of the user query and UDF body prevents inlining, in which case the scalar UDF is not inlined, and the user query fails.
- When a scalar UDF is used in any unsupported scenario, you see an error message "`Scalar UDF execution is currently unavailable in this context.`"

## Limitations

> [!NOTE]
> During the current preview, limitations are subject to change.

 User-defined functions cannot be used to perform actions that modify the database state.  

 User-defined functions can be nested; that is, one user-defined function can call another. The nesting level is incremented when the called function starts execution, and decremented when the called function finishes execution. User-defined functions in Fabric Data Warehouse can be nested up to four levels when a UDF body references a table/view/in-line table-valued function, or up to 32 levels otherwise. Exceeding the maximum levels of nesting causes the calling function chain to fail.

## Metadata

 This section lists the system catalog views that you can use to return metadata about user-defined functions.  

 - [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md): Displays the definition of [!INCLUDE [tsql](../../includes/tsql-md.md)] user-defined functions. For example:  

   ```sql
   SELECT definition, type   
   FROM sys.sql_modules AS m  
   JOIN sys.objects AS o   
       ON m.object_id = o.object_id   
       AND type = ('FN');
   ```  

 - [sys.parameters](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md): Displays information about the parameters defined in user-defined functions.  

 - [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md): Displays the underlying objects referenced by a function.  
 
## Permissions

Members of the Fabric workspace Administrator, Member, and Contributor roles can create functions.

## Scalar UDF inlining

Microsoft Fabric Data Warehouse uses [scalar UDF inlining](../../relational-databases/user-defined-functions/scalar-udf-inlining.md) to compile and execute user defined code in a distributed manner. Scalar UDF inlining is enabled by default.

While scalar UDF inlining is a performance optimization technique first introduced in Microsoft SQL Server 2019 (15.0), in Fabric Data Warehouse it determines the supported set of scenarios. In Fabric Data Warehouse, scalar UDFs are automatically transformed into scalar expressions or scalar subqueries that are substituted in the calling query in place of the UDF operator.

Some T-SQL syntax makes a scalar UDF noninlineable. Functions that contain a `WHILE` loop, multiple `RETURN` statements, or a call to a nondeterministic SQL built-in function (such as `GETUTCDATE()` or `GETDATE()`) can't be inlined. For more information, see [Scalar UDF inlining requirements](../../relational-databases/user-defined-functions/scalar-udf-inlining.md#inlineable-scalar-udf-requirements).

### Check whether a scalar UDF can be inlined

The `sys.sql_modules` catalog view includes the column `is_inlineable`, which indicates whether a UDF is inlineable. 

The `is_inlineable` property is derived from checking for syntax inside the UDF definition. The scalar UDF is not inlined before compile time. A value of `1` indicates that the UDF is inlineable, while a value of `0` indicates that it is not inlineable. If a scalar UDF is inlineable, it doesn't guarantee it will always be inlined when the query is compiled. 

Fabric Data Warehouse decides (per query) whether to inline a UDF, depending on overall query complexity.

Use the following sample query to check whether a scalar UDF is inlineable:

```sql
SELECT 
SCHEMA_NAME(b.schema_id) as function_schema_name,
    b.name as function_name,
       b.type_desc as function_type,
       a.is_inlineable
FROM sys.sql_modules AS a
     INNER JOIN sys.objects AS b
         ON a.object_id = b.object_id
WHERE b.type IN ('FN');
```

If a scalar function is not inlineable in `sys.sql_modules.is_inlineable`, you can still execute the query as a standalone call, for example, to set a variable. But the scalar function cannot be part of a `SELECT ... FROM` query on a user table. For example:

```sql
CREATE FUNCTION [dbo].[custom_SYSUTCDATETIME]()
  RETURNS datetime2(6)
  AS
  BEGIN
   RETURN SYSUTCDATETIME();
  END
```

The sample `dbo.custom_SYSUTCDATETIME` scalar user-defined function is not inlineable due to the use of a nondeterminant system function, `SYSUTCDATETIME()`. It will fail when used in a `SELECT ... FROM` query on a user table, but will succeed as a standalone call, for example:

```sql
DECLARE @utcdate datetime2(7);
SET @utcdate = dbo.custom_SYSUTCDATETIME();
SELECT @utcdate as 'utc_date';
```

## Examples 

### A. Create an inline table-valued function

 The following example creates an inline table-valued function to return some key information on modules, filtering by the `objectType` parameter. It includes a default value to return all modules when the function is called with the `DEFAULT` parameter. This example makes use of some of the system catalog views mentioned in [Metadata](#metadata).

```sql
CREATE FUNCTION dbo.ModulesByType (@objectType CHAR(2) = '%%')
RETURNS TABLE
AS
RETURN (
        SELECT sm.object_id AS 'Object Id',
            o.create_date AS 'Date Created',
            OBJECT_NAME(sm.object_id) AS 'Name',
            o.type AS 'Type',
            o.type_desc AS 'Type Description',
            sm.DEFINITION AS 'Module Description',
            sm.is_inlineable AS 'Inlineable'
        FROM sys.sql_modules AS sm
        INNER JOIN sys.objects AS o ON sm.object_id = o.object_id
        WHERE o.type LIKE '%' + @objectType + '%'
        );
GO
```

The function can then be called to return all inline table-valued functions (`IF`) with:

```sql
SELECT * FROM dbo.ModulesByType('IF'); -- SQL_INLINE_TABLE_VALUED_FUNCTION
```

Or, find all scalar functions (`FN`):

```sql
SELECT * FROM dbo.ModulesByType('FN'); -- SQL_SCALAR_FUNCTION
```

<a id="b-combining-results-of-an-inline-table-valued-function"></a>

### B. Combine results of an inline table-valued function

 This simple example uses the previously created inline TVF to demonstrate how its results can be combined with other tables using cross apply. Here, we select all columns from both `sys.objects` and the results of `ModulesByType` for all rows matching on the `type` column. For more information on using apply, see [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md?view=fabric&preserve-view=true).

```sql
SELECT * 
FROM sys.objects AS o
CROSS APPLY dbo.ModulesByType(o.type);
GO
```

### C. Create a scalar UDF function

The following example creates an inlineable scalar UDF that masks an input text.

```sql
CREATE OR ALTER FUNCTION [dbo].[cleanInput] (@InputString VARCHAR(100))
    RETURNS VARCHAR(50)
    AS
    BEGIN
        DECLARE @Result VARCHAR(50)
        DECLARE @CleanedInput VARCHAR(50)

        -- Trim whitespace
        SET @CleanedInput = LTRIM(RTRIM(@InputString))

        -- Handle empty or null input
        IF @CleanedInput = '' OR @CleanedInput IS NULL
        BEGIN
            SET @Result = ''
        END
        ELSE IF LEN(@CleanedInput) <= 2
        BEGIN
            -- If string length is 1 or 2, just return the cleaned string
            SET @Result = @CleanedInput
        END
        ELSE
        BEGIN
            -- Construct the masked string
            SET @Result = 
                LEFT(@CleanedInput, 1) +
                REPLICATE('*', LEN(@CleanedInput) - 2) +
                RIGHT(@CleanedInput, 1)
        END

        RETURN @Result
    END
```

You can call the function like this:

```sql
DECLARE @input varchar(100) = '123456789'

SELECT dbo.cleanInput (@input) AS function_output;
```

More examples of how you can use scalar UDFs in Fabric Data Warehouse:

In a `SELECT` statement:

```sql
SELECT TOP 10 
t.id, t.name, 
dbo.cleanInput (t.name) AS function_output
FROM dbo.MyTable AS t;
```

In a `WHERE` clause:

```sql
 SELECT t.id, t.name, dbo.cleanInput(t.name) AS function_output
FROM dbo.MyTable AS t
WHERE dbo.cleanInput(t.name)='myvalue'
```

In a `JOIN` clause:

```sql
SELECT t1.id, t1.name, 
     dbo.cleanInput (t1.name) AS function_output, 
     dbo.cleanInput (t2.name) AS function_output_2
FROM dbo.MyTable1 AS t1
    INNER JOIN dbo.MyTable2 AS t2 
        ON dbo.cleanInput(t1.name)=dbo.cleanInput(t2.name);
```

In an `ORDER BY` clause:

```sql
SELECT  t.id, t.name, dbo.cleanInput (t.name) AS function_output
FROM dbo.MyTable AS t
ORDER BY function_output;
```

In data manipulation language (DML) statements like `INSERT`, `UPDATE`, or `DELETE`:

```sql
SELECT t.id, t.name, dbo.cleanInput (t.name) AS function_output 
INTO dbo.MyTable_new
FROM dbo.MyTable AS t;

UPDATE t
SET t.mycolumn_new = dbo.cleanInput (t.name)
FROM dbo.MyTable AS t;

DELETE t
FROM dbo.MyTable AS t
WHERE dbo.cleanInput (t.name) ='myvalue';
```

## Related content

- [Scalar UDF inlining](../../relational-databases/user-defined-functions/scalar-udf-inlining.md)
- [Fabric Data Warehouse Migration Assistant](/fabric/data-warehouse/migration-assistant/)

::: moniker-end
::: moniker range=">=aps-pdw-2016 || =azure-sqldw-latest"

[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Creates a user-defined function (UDF) in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]. A user-defined function is a [!INCLUDE [tsql](../../includes/tsql-md.md)] routine that accepts parameters, performs an action, such as a complex calculation, and returns the result of that action as a value. User-defined table-valued functions (TVFs) return a table data type.

> [!TIP]
> For syntax in Fabric Data Warehouse, see the version of [CREATE FUNCTION](create-function-sql-data-warehouse.md?view=fabric&preserve-view=true) for Fabric Data Warehouse.

- In [!INCLUDE [ssPDW](../../includes/sspdw-md.md)], the return value must be a scalar (single) value.
- In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], `CREATE FUNCTION` can return a table by using the syntax for inline table-valued functions (preview) or it can return a single value by using the syntax for scalar functions.
- In serverless SQL pools in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], `CREATE FUNCTION` can create inline table-value functions but not scalar functions. 

  Use this statement to create a reusable routine that can be used in these ways:  

-   In [!INCLUDE [tsql](../../includes/tsql-md.md)] statements such as `SELECT`  
-   In applications calling the function
-   In the definition of another user-defined function
-   To define a CHECK constraint on a column
-   To replace a stored procedure
-   Use an inline function as a filter predicate for a security policy

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  


## Syntax

### Scalar function syntax

```syntaxsql
-- Transact-SQL Scalar Function Syntax (in dedicated pools in Azure Synapse Analytics and Parallel Data Warehouse)
-- Not available in the serverless SQL pools in Azure Synapse Analytics

CREATE FUNCTION [ schema_name. ] function_name   
( [ { @parameter_name [ AS ] parameter_data_type   
    [ = default ] }   
    [ ,...n ]  
  ]  
)  
RETURNS return_data_type  
    [ WITH <function_option> [ ,...n ] ]  
    [ AS ]  
    BEGIN   
        function_body   
        RETURN scalar_expression  
    END  
[ ; ]  

<function_option>::=   
{  
    [ SCHEMABINDING ]  
  | [ RETURNS NULL ON NULL INPUT | CALLED ON NULL INPUT ]  
}  
```

### Inline table-valued function syntax

```syntaxsql
-- Transact-SQL Inline Table-Valued Function Syntax
-- Preview in dedicated SQL pools in Azure Synapse Analytics
-- Available in the serverless SQL pools in Azure Synapse Analytics
CREATE FUNCTION [ schema_name. ] function_name
( [ { @parameter_name [ AS ] parameter_data_type
    [ = default ] }
    [ ,...n ]
  ]
)
RETURNS TABLE
    [ WITH SCHEMABINDING ]
    [ AS ]
    RETURN [ ( ] select_stmt [ ) ]
[ ; ]
```

## Arguments

#### *schema_name*

The name of the schema to which the user-defined function belongs.

#### *function_name*

The name of the user-defined function. Function names must comply with the rules for identifiers and must be unique within the database and to its schema.

> [!NOTE]  
> Parentheses are required after the function name even if a parameter is not specified.  

#### @*parameter_name*

A parameter in the user-defined function. One or more parameters can be declared.

A function can have a maximum of 2,100 parameters. The value of each declared parameter must be supplied by the user when the function is executed, unless a default for the parameter is defined.

Specify a parameter name by using an at sign (`@`) as the first character. The parameter name must comply with the rules for identifiers. Parameters are local to the function; the same parameter names can be used in other functions. Parameters can take the place only of constants; they cannot be used instead of table names, column names, or the names of other database objects.

> [!NOTE]  
> `ANSI_WARNINGS` is not honored when you pass parameters in a stored procedure, user-defined function, or when you declare and set variables in a batch statement. For example, if a variable is defined as **char(3)**, and then set to a value larger than three characters, the data is truncated to the defined size and the INSERT or UPDATE statement succeeds.  

#### *parameter_data_type*

The parameter data type. For [!INCLUDE [tsql](../../includes/tsql-md.md)] functions, all scalar data types supported in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] are allowed. The timestamp (rowversion) data type is not a supported type.

#### [ = *default* ]

A default value for the parameter. If a *default* value is defined, the function can be executed without specifying a value for that parameter.

When a parameter of the function has a default value, the keyword DEFAULT must be specified when the function is called to retrieve the default value. This behavior is different from using parameters with default values in stored procedures in which omitting the parameter also implies the default value.

#### *return_data_type*

The return value of a scalar user-defined function. For [!INCLUDE [tsql](../../includes/tsql-md.md)] functions, all scalar data types supported in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] are allowed. The **rowversion**/**timestamp** data type is not a supported type. The cursor and table nonscalar types are not allowed.

#### *function_body*

Series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. The *function_body* cannot contain a `SELECT` statement and cannot reference database data. The *function_body* cannot reference tables or views. The function body can call other deterministic functions but cannot call nondeterministic functions.

In scalar functions, *function_body* is a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that together evaluate to a scalar value.

#### *scalar_expression*

Specifies the scalar value that the scalar function returns.

#### *select_stmt*

The single `SELECT` statement that defines the return value of an inline table-valued function. For an inline table-valued function, there is no function body; the table is the result set of a single `SELECT` statement.

#### TABLE

Specifies that the return value of the table-valued function (TVF) is a table. Only constants and @*local_variables* can be passed to TVFs.

In inline TVFs (preview), the TABLE return value is defined through a single `SELECT` statement. Inline functions do not have associated return variables.

#### <function_option>

Specifies that the function has one or more of the following options.

SCHEMABINDING

 Specifies that the function is bound to the database objects that it references. When SCHEMABINDING is specified, the base objects cannot be modified in a way that would affect the function definition. The function definition itself must first be modified or dropped to remove dependencies on the object that is to be modified.  

The binding of the function to the objects it references is removed only when one of the following actions occurs:

-   The function is dropped.

-   The function is modified by using the ALTER statement with the SCHEMABINDING option not specified.

A function can be schema bound only if the following conditions are true:

-   Any user-defined functions referenced by the function are also schema-bound.

-   The functions and other UDFs referenced by the function are referenced using a one-part or two-part name.

-   Only built-in functions and other UDFs in the same database can be referenced within the body of UDFs.

-   The user who executed the `CREATE FUNCTION` statement has REFERENCES permission on the database objects that the function references.

To remove SCHEMABINDING, use `ALTER`.

RETURNS NULL ON NULL INPUT | **CALLED ON NULL INPUT**

 Specifies the `OnNULLCall` attribute of a scalar-valued function. If not specified, `CALLED ON NULL INPUT` is implied by default, and the function body executes even if `NULL` is passed as an argument.  

## Best practices

 If a user-defined function is not created with the SCHEMABINDING clause, changes that are made to underlying objects can affect the definition of the function and produce unexpected results when it is invoked. It is recommended to specify the `WITH SCHEMABINDING` clause when you are creating the function. This ensures that the objects referenced in the function definition cannot be modified unless the function is also modified.

## Interoperability

 The following statements are valid in a scalar-valued function:  

-   Assignment statements.  

-   Control-of-Flow statements except TRY...CATCH statements.  

-   DECLARE statements defining local data variables.  

In an inline table-valued function (preview), only a single select statement is allowed.

## Limitations

 User-defined functions cannot be used to perform actions that modify the database state.  

 User-defined functions can be nested; that is, one user-defined function can call another. The nesting level is incremented when the called function starts execution, and decremented when the called function finishes execution. Exceeding the maximum levels of nesting causes the whole calling function chain to fail.

 Objects, including functions, cannot be created in the `master` database of your serverless SQL pool in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

## Metadata

 This section lists the system catalog views that you can use to return metadata about user-defined functions.  

 - [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md): Displays the definition of [!INCLUDE [tsql](../../includes/tsql-md.md)] user-defined functions. For example:  

   ```sql
   SELECT definition, type   
   FROM sys.sql_modules AS m  
   JOIN sys.objects AS o   
       ON m.object_id = o.object_id   
       AND type = ('FN');
   ```  

 - [sys.parameters](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md): Displays information about the parameters defined in user-defined functions.  

 - [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md): Displays the underlying objects referenced by a function.  


## Permissions

 Requires CREATE FUNCTION permission in the database and ALTER permission on the schema in which the function is being created.  

<a id="examples-"></a>

## Examples

<a id="a-using-a-scalar-valued-user-defined-function-to-change-a-data-type"></a>

### A. Use a scalar-valued user-defined function to change a data type

 This simple function takes a **int** data type as an input, and returns a **decimal(10,2)** data type as an output.  

```sql
CREATE FUNCTION dbo.ConvertInput (@MyValueIn int)  
RETURNS decimal(10,2)  
AS  
BEGIN
    DECLARE @MyValueOut int;  
    SET @MyValueOut= CAST( @MyValueIn AS decimal(10,2));  
    RETURN(@MyValueOut);  
END;  
GO  

SELECT dbo.ConvertInput(15) AS 'ConvertedValue';  
```  

> [!NOTE]  
> Scalar functions are not available in the serverless SQL pools.

<a id="a-creating-an-inline-table-valued-function"></a>

### B. Create an inline table-valued function

 The following example creates an inline table-valued function to return some key information on modules, filtering by the `objectType` parameter. It includes a default value to return all modules when the function is called with the `DEFAULT` parameter. This example makes use of some of the system catalog views mentioned in [Metadata](#metadata).

```sql
CREATE FUNCTION dbo.ModulesByType(@objectType CHAR(2) = '%%')
RETURNS TABLE
AS
RETURN
(
    SELECT 
        sm.object_id AS 'Object Id',
        o.create_date AS 'Date Created',
        OBJECT_NAME(sm.object_id) AS 'Name',
        o.type AS 'Type',
        o.type_desc AS 'Type Description', 
        sm.definition AS 'Module Description'
    FROM sys.sql_modules AS sm  
    JOIN sys.objects AS o ON sm.object_id = o.object_id
    WHERE o.type like '%' + @objectType + '%'
);
GO
```

The function can then be called to return all view (`V`) objects with:

```sql
select * from dbo.ModulesByType('V');
```

> [!NOTE]  
> Inline table-value functions are available in the serverless SQL pools, but in preview in the dedicated SQL pools.

<a id="b-combining-results-of-an-inline-table-valued-function"></a>

### C. Combine results of an inline table-valued function

 This simple example uses the previously created inline TVF to demonstrate how its results can be combined with other tables using cross apply. Here, we select all columns from both `sys.objects` and the results of `ModulesByType` for all rows matching on the `type` column. For more information on using apply, see [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md).

```sql
SELECT * 
FROM sys.objects o
CROSS APPLY dbo.ModulesByType(o.type);
GO
```

> [!NOTE]  
> Inline table-value functions are available in the serverless SQL pools, but in preview in the dedicated SQL pools.

## Next step

> [!div class="nextstepaction"]
> [Create user-defined functions (Database Engine)](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md)

::: moniker-end
