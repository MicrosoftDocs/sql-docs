---
title: "CREATE FUNCTION (Microsoft Fabric, Azure Synapse Analytics)"
description: User-defined functions accept parameters, perform an action, such as a complex calculation, and return the result of that action as a value.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, srdjanmatin
ms.date: 07/02/2026
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

 `CREATE FUNCTION` creates inline table-valued functions and scalar functions. 
 
> [!NOTE]
> Scalar UDFs are a preview feature in Fabric Data Warehouse.

> [!IMPORTANT]
> In Fabric Data Warehouse, [scalar UDFs must be inlineable](#scalar-udf-inlining) for use with `SELECT ... FROM` queries on user tables, but you can still create functions that aren't inlineable. Scalar UDFs that aren't inlineable work in a limited number of scenarios. You can check [whether a UDF can be inlined](#check-whether-a-scalar-udf-can-be-inlined).

A user-defined function is a [!INCLUDE [tsql](../../includes/tsql-md.md)] routine that accepts parameters, performs an action such as a complex calculation, and returns the result of that action as a value. Scalar functions return a scalar value, such as a number or string. User-defined table-valued functions (TVFs) return a table.

Use `CREATE FUNCTION` to create a reusable T-SQL routine that you can use in these ways:

 - In [!INCLUDE [tsql](../../includes/tsql-md.md)] statements such as `SELECT` 
 - In [!INCLUDE [tsql](../../includes/tsql-md.md)] data manipulation statements (DML) such as `UPDATE`, `INSERT`, and `DELETE`
 - In applications calling the function
 - In the definition of another user-defined function
 - To replace a stored procedure

You can specify `CREATE OR ALTER FUNCTION` to create a new function if one doesn't exist by that name, or alter an existing function, in a single statement.

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

The name of the user-defined function. Function names must follow the rules for identifiers and be unique within the database and its schema.

> [!NOTE]  
> You must include parentheses after the function name even if you don't specify a parameter.  

#### @*parameter_name*

A parameter in the user-defined function. You can declare one or more parameters.

A function can have up to 2,100 parameters. When a user or application calls a function, the value of each declared parameter must be supplied unless a default for the parameter is defined.

Specify a parameter name by using an at sign (`@`) as the first character. The parameter name must follow the rules for identifiers. Parameters are local to the function; you can use the same parameter names in other functions. Parameters can only replace constants; they can't be used instead of table names, column names, or the names of other database objects.

> [!NOTE]  
> `ANSI_WARNINGS` isn't honored when you pass parameters in a stored procedure, user-defined function, or when you declare and set variables in a batch statement. For example, if you define a variable as **char(3)**, and then set it to a value larger than three characters, the data is truncated to the defined size and SQL statement succeeds.    

#### *parameter_data_type*

The parameter data type. For [!INCLUDE [tsql](../../includes/tsql-md.md)] functions, all [scalar data types supported](/fabric/data-warehouse/data-types) are allowed. 

#### [ = *default* ]

A default value for the parameter. If you define a *default* value, you can execute the function without specifying a value for that parameter.

When a parameter of the function has a default value, you must specify the keyword `DEFAULT` when calling the function to retrieve the default value. This behavior is different from using parameters with default values in stored procedures in which omitting the parameter also implies the default value.

#### *return_data_type*

The return value of a scalar user-defined function. 

For functions in Fabric Data Warehouse, all data types are allowed except for **rowversion**/**timestamp**. Nonscalar types like **table** aren't allowed.

#### *function_body*

A series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. 

In scalar functions, *function_body* is a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that together evaluate to a scalar value, which can include:

- Single statement expression
- Multi-statement expressions (`IF/THEN/ELSE` and `BEGIN/END` blocks)
- Local variables
- Calls to built-in SQL functions available
- Calls to other UDFs
- `SELECT` statements, and references to tables, views, and inline table-valued functions
- Control flow statements (`WHILE` loops, `RETURNS`)

#### *scalar_expression*

Specifies the scalar value that the scalar function returns.

#### *select_stmt*

The single `SELECT` statement that defines the return value of an inline table-valued function. For an inline table-valued function, there's no function body; the table is the result set of a single `SELECT` statement.

#### TABLE

Specifies that the return value of the table-valued function (TVF) is a table. You can only pass constants and @*local_variables* to TVFs.

In inline TVFs (preview), you define the `TABLE` return value through a single `SELECT` statement. Inline functions don't have associated return variables.

#### <function_option>

In Fabric Data Warehouse, the `INLINE`, `ENCRYPTION`, and `EXECUTE AS` keywords aren't supported. 

The supported function options include:

SCHEMABINDING

   Specifies that the function is bound to the database objects that it references. When you specify `SCHEMABINDING`, you can't modify the underlying objects (such as a view or a table, for example) in a way that affects the function definition. You must first modify or drop the function definition to remove dependencies on the object that you want to modify.      

The binding of the function to the objects it references is removed only when one of the following actions occurs:

-   You drop the function.

-   You `ALTER` the function statement and remove the `SCHEMABINDING` option.

You can only schema bind a function if the following conditions are true:

-   Any user-defined functions that the function references are also schema-bound.

-   The function references objects by using a two-part name.

-   Within the body of UDFs, you can only reference built-in functions and other UDFs in the same database.

-   The user who executes the `CREATE FUNCTION` statement has REFERENCES permission on the database objects that the function references.

To remove SCHEMABINDING, use `ALTER`.

RETURNS NULL ON NULL INPUT | **CALLED ON NULL INPUT**

 Specifies the `OnNULLCall` attribute of a scalar-valued function. If you don't specify this attribute, `CALLED ON NULL INPUT` is implied by default, and the function body executes even if `NULL` is passed as an argument.  

## Best practices

- If you don't create a user-defined function with schemabinding, changes to underlying objects can affect the function's definition and cause unexpected results when you invoke the function. When you specify `WITH SCHEMABINDING` when you create the function, you ensure that later changes to underlying objects cannot change or break the function's behavior.

- Write your user-defined functions to be inlineable. For more information, see [Inlining of scalar UDF](#inlining-of-scalar-udf).

## Interoperability

### Inline table-valued user-defined functions

An inline table-valued function accepts only a single `SELECT` statement.

### Scalar user-defined functions

- The following statements are valid in a scalar-valued function:  
    -   Assignment statements.
    -   Control-of-Flow statements except `TRY...CATCH` and `GO..TO` statements.
    -   `DECLARE` statements defining local data variables.
    -   References to tables/views/iTVFs/other scalar UDFs.

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


## Limitations

> [!NOTE]
> During the current preview, limitations are subject to change.

 - You can't use user-defined functions to perform actions that modify the database state.  

 - You can nest user-defined functions. That is, one user-defined function can call another. The nesting level increments when the called function starts execution, and decrements when the called function finishes execution. In Fabric Data Warehouse, you can nest user-defined functions up to four levels when a UDF body references a table, view, or inline table-valued function, or up to 32 levels otherwise. If you exceed the maximum levels of nesting, the calling function chain fails.

 - Scalar UDFs can't be used in a `SELECT ... FROM` query on a user table when:
    - The UDF body contains a call to nondeterministic built-in function (such as `GETDATE()`), see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).
    - The UDF body contains `BREAK` or `CONTINUE` statement.
    - There is a recursive scalar UDF call.

 - A scalar UDF can't be used in all query shapes, such as CTEs and `GROUP BY` when:
    - The scalar UDF body contains reference to tables/views/iTVFs/other scalar UDFs.
    - The scalar UDF contains any of these data types as an input parameter, local variable, or return data type: **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **binary(max)**.
    - The scalar UDF body contains calls to [AI functions](/fabric/data-warehouse/ai-functions).
    - In above scenarios, general scalar udf inlining requirements applly, see [Scalar UDF inlining requirements](/sql/relational-databases/user-defined-functions/scalar-udf-inlining?view=fabric&preserve-view=true#inlineable-scalar-udf-requirements).

- If a scalar UDF contains any of the following, a user query can fail if more than 10 UDF calls are made in a single query: 
    - The scalar UDF body contains reference to tables/views/iTVFs/other scalar UDFs.
    - The scalar UDF contains any of these data types as an input parameter, local variable, or return data type: **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **binary(max)**.
    - The scalar UDF body contains calls to [AI Functions](/fabric/data-warehouse/ai-functions).

- When a scalar UDF is used in any unsupported scenario, you see an error message "`Scalar UDF execution is currently unavailable in this context.`" at query execution time.

## Metadata

 This section lists the system catalog views that you can use to return metadata about user-defined functions.  

 - [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md): Displays the definition of [!INCLUDE [tsql](../../includes/tsql-md.md)] user-defined functions, as well as inlineability information. For example:  

   ```sql
    SELECT 
        SCHEMA_NAME(o.schema_id) AS SchemaName,
        o.name AS FunctionName,
        m.definition AS FunctionDefinition,
        m.is_inlineable AS Inlineable,
        m.inline_eligibility_mask AS InlineEligibilityMask
    FROM sys.objects o
    JOIN sys.sql_modules m ON o.object_id = m.object_id
    WHERE o.type = 'FN';
   ```  

 - [sys.parameters](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md): Displays information about the parameters defined in user-defined functions.  

 - [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md): Displays the underlying objects referenced by a function.  
 
## Permissions

Members of the Fabric workspace Administrator, Member, and Contributor roles can create functions.

<a id="scalar-udf-inlining"></a>

## Inlining of Scalar UDF

Microsoft Fabric Data Warehouse uses different inlining techniques to compile and execute user defined code in a distributed manner. 

Inlining of scalar UDF is enabled by default.

Some T-SQL syntax makes a scalar UDF noninlineable. For example, functions that contain a combination of a `WHILE` loop and reference a table inside UDF body can't be inlined. 

### Check whether a scalar UDF can be inlined

The `sys.sql_modules` catalog view includes the column `is_inlineable`, which indicates whether a UDF is inlineable. The `is_inlineable` property comes from checking the syntax inside the UDF definition. The scalar UDF isn't inlined before compile time. 

The `inline_eligibility_mask` property explains which type of inlining is applicable to a UDF.

- A value of `0` means that the UDF isn't inlineable. 
- A value of `1` indicates that the UDF is eligible for [Scalar UDF inlining](../../relational-databases/user-defined-functions/scalar-udf-inlining.md). 
- A value of `2` means that the UDF is eligible for inlining via expression block. 
- A value of `3` means that UDF is eligible for either inlining technique.

If a scalar UDF is inlineable, it doesn't guarantee it is always inlined when the query is compiled.

Fabric Data Warehouse decides (per query) which inlining technique to apply.

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

If a scalar function isn't inlineable in `sys.sql_modules.is_inlineable`, you can still execute the query as a standalone call, for example, to set a variable. But the scalar function can't be part of a `SELECT ... FROM` query on a user table. For example:

```sql
CREATE FUNCTION [dbo].[custom_SYSUTCDATETIME]()
  RETURNS datetime2(6)
  AS
  BEGIN
   RETURN SYSUTCDATETIME();
  END
```

The sample `dbo.custom_SYSUTCDATETIME` scalar user-defined function isn't inlineable due to the use of a nondeterminant system function, `SYSUTCDATETIME()`. It fails when used in a `SELECT ... FROM` query on a user table, but succeeds as a standalone call. For example:

```sql
DECLARE @utcdate datetime2(7);
SET @utcdate = dbo.custom_SYSUTCDATETIME();
SELECT @utcdate as 'utc_date';
```

## Examples 

### A. Create an inline table-valued function

 The following example creates an inline table-valued function that returns key information on modules, filtering by the `objectType` parameter. It includes a default value to return all modules when you call the function with the `DEFAULT` parameter. This example uses some of the system catalog views mentioned in [Metadata](#metadata).

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

Call the function to return all inline table-valued functions (`IF`):

```sql
SELECT * FROM dbo.ModulesByType('IF'); -- SQL_INLINE_TABLE_VALUED_FUNCTION
```

Or, find all scalar functions (`FN`):

```sql
SELECT * FROM dbo.ModulesByType('FN'); -- SQL_SCALAR_FUNCTION
```

<a id="b-combining-results-of-an-inline-table-valued-function"></a>

### B. Combine results of an inline table-valued function

 This simple example uses the previously created inline TVF to demonstrate how you can combine its results with other tables by using `CROSS APPLY`. Here, you select all columns from both `sys.objects` and the results of `ModulesByType` for all rows that match on the `type` column. For more information about using `APPLY`, see [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md?view=fabric&preserve-view=true).

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

  Use this statement to create a reusable routine that you can use in these ways:  

-   In [!INCLUDE [tsql](../../includes/tsql-md.md)] statements such as `SELECT`  
-   In applications that call the function
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

The name of the user-defined function. Function names must follow the rules for identifiers and be unique within the database and its schema.

> [!NOTE]  
> You must include parentheses after the function name even if you don't specify a parameter.  

#### @*parameter_name*

A parameter in the user-defined function. You can declare one or more parameters.

A function can have up to 2,100 parameters. When a user or application calls a function, the value of each declared parameter must be supplied unless a default for the parameter is defined.

Specify a parameter name by using an at sign (`@`) as the first character. The parameter name must follow the rules for identifiers. Parameters are local to the function; you can use the same parameter names in other functions. Parameters can only replace constants; they can't be used instead of table names, column names, or the names of other database objects.

> [!NOTE]  
> `ANSI_WARNINGS` isn't honored when you pass parameters in a stored procedure, user-defined function, or when you declare and set variables in a batch statement. For example, if you define a variable as **char(3)**, and then set it to a value larger than three characters, the data is truncated to the defined size and the `INSERT` or `UPDATE` statement succeeds.    

#### *parameter_data_type*

The parameter data type. For [!INCLUDE [tsql](../../includes/tsql-md.md)] functions, all scalar data types supported in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] are allowed. The timestamp (rowversion) data type isn't a supported type.

#### [ = *default* ]

A default value for the parameter. If you define a *default* value, you can execute the function without specifying a value for that parameter.

When a parameter of the function has a default value, you must specify the keyword `DEFAULT` when calling the function to retrieve the default value. This behavior is different from using parameters with default values in stored procedures in which omitting the parameter also implies the default value.

#### *return_data_type*

The return value of a scalar user-defined function. For [!INCLUDE [tsql](../../includes/tsql-md.md)] functions, all scalar data types supported in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] are allowed. The **rowversion**/**timestamp** data type isn't a supported type. The cursor and table nonscalar types aren't allowed.

#### *function_body*

Series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. The *function_body* can't contain a `SELECT` statement and can't reference database data. The *function_body* can't reference tables or views. The function body can call other deterministic functions but can't call nondeterministic functions.

In scalar functions, *function_body* is a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that together evaluate to a scalar value.

#### *scalar_expression*

Specifies the scalar value that the scalar function returns.

#### *select_stmt*

The single `SELECT` statement that defines the return value of an inline table-valued function. For an inline table-valued function, there's no function body; the table is the result set of a single `SELECT` statement.

#### TABLE

Specifies that the return value of the table-valued function (TVF) is a table. You can only pass constants and @*local_variables* to TVFs.

In inline TVFs (preview), you define the `TABLE` return value through a single `SELECT` statement. Inline functions don't have associated return variables.

#### <function_option>

Specifies that the function has one or more of the following options.

SCHEMABINDING

   Specifies that the function is bound to the database objects that it references. When you specify `SCHEMABINDING`, you can't modify the underlying objects (such as a view or a table, for example) in a way that affects the function definition. You must first modify or drop the function definition to remove dependencies on the object that you want to modify.      

The binding of the function to the objects it references is removed only when one of the following actions occurs:

-   You drop the function.

-   You `ALTER` the function statement and remove the `SCHEMABINDING` option.

You can only schema bind a function if the following conditions are true:

-   Any user-defined functions that the function references are also schema-bound.

-   The function references use one-part or two-part names.

-   Within the body of UDFs, you can only reference built-in functions and other UDFs in the same database.

-   The user who executes the `CREATE FUNCTION` statement has REFERENCES permission on the database objects that the function references.

To remove SCHEMABINDING, use `ALTER`.

RETURNS NULL ON NULL INPUT | **CALLED ON NULL INPUT**

 Specifies the `OnNULLCall` attribute of a scalar-valued function. If you don't specify this attribute, `CALLED ON NULL INPUT` is implied by default, and the function body executes even if `NULL` is passed as an argument.  

## Best practices

 If you don't create a user-defined function with the SCHEMABINDING clause, changes to underlying objects can affect the function's definition and cause unexpected results when you invoke it. Specify the `WITH SCHEMABINDING` clause when you create the function. This clause ensures that you can't modify the objects referenced in the function definition unless you also modify the function.

## Interoperability

 The following statements are valid in a scalar-valued function:  

-   Assignment statements.  

-   Control-of-Flow statements, except TRY...CATCH statements.  

-   DECLARE statements that define local data variables.  

In an inline table-valued function (preview), you can only use a single select statement.

## Limitations

 You can't use user-defined functions to perform actions that modify the database state.  

 You can nest user-defined functions. One user-defined function can call another. The nesting level increments when the called function starts execution, and decrements when the called function finishes execution. If you exceed the maximum levels of nesting, the whole calling function chain fails.

 You can't create objects, including functions, in the `master` database of your serverless SQL pool in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

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

 This simple function takes an **int** data type as an input, and returns a **decimal(10,2)** data type as an output.  

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
> Scalar functions aren't available in serverless SQL pools.

<a id="a-creating-an-inline-table-valued-function"></a>

### B. Create an inline table-valued function

 The following example creates an inline table-valued function that returns key information on modules, filtering by the `objectType` parameter. It includes a default value to return all modules when you call the function with the `DEFAULT` parameter. This example uses some of the system catalog views mentioned in [Metadata](#metadata).

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

You can call the function to return all view (`V`) objects with:

```sql
select * from dbo.ModulesByType('V');
```

> [!NOTE]  
> Inline table-value functions are available in serverless SQL pools, but in preview in the dedicated SQL pools.

<a id="b-combining-results-of-an-inline-table-valued-function"></a>

### C. Combine results of an inline table-valued function

 This simple example uses the previously created inline TVF to demonstrate how you can combine its results with other tables by using `CROSS APPLY`. In this example, you select all columns from both `sys.objects` and the results of `ModulesByType` for all rows that match on the `type` column. For more information about using `APPLY`, see [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md).

```sql
SELECT * 
FROM sys.objects o
CROSS APPLY dbo.ModulesByType(o.type);
GO
```

> [!NOTE]  
> Inline table-value functions are available in serverless SQL pools, but in preview in the dedicated SQL pools.

## Next step

> [!div class="nextstepaction"]
> [Create user-defined functions (Database Engine)](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md)

::: moniker-end
