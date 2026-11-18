---
title: CREATE FUNCTION (Microsoft Fabric, Azure Synapse Analytics)
description: User-defined functions accept parameters, perform an action, such as a complex calculation, and return the result of that action as a value.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, srdjanmatin
ms.date: 11/10/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || =fabric"
---
# CREATE FUNCTION

::: moniker range="=fabric"

[!INCLUDE [applies-to-version/fabricse-fabricdw](../../includes/applies-to-version/fabric-se-dw.md)]

`CREATE FUNCTION` creates inline table-valued functions and scalar functions. 

> [!NOTE]
> External UDFs are a preview feature in Fabric Data Warehouse.

This article applies to Microsoft Fabric Data Warehouse and the SQL analytics endpoint of Fabric items. For other platforms, see [CREATE FUNCTION (Transact-SQL)](create-function-transact-sql.md?view=azuresql-db&preserve-view=true).

A user-defined function is a [!INCLUDE [tsql](../../includes/tsql-md.md)] routine that accepts parameters, performs an action such as a complex calculation, and returns the result of that action as a value. Scalar functions return a scalar value, such as a number or string. User-defined table-valued functions (TVFs) return a table.

Use `CREATE FUNCTION` to create a reusable T-SQL routine that you can use in these ways:

 - In [!INCLUDE [tsql](../../includes/tsql-md.md)] statements such as `SELECT`.
 - In Warehouse, in [!INCLUDE [tsql](../../includes/tsql-md.md)] data manipulation statements (DML) such as `UPDATE`, `INSERT`, and `DELETE`.
 - In applications calling the function.
 - In the definition of another user-defined function.
 - To replace a stored procedure.

Specify `CREATE OR ALTER FUNCTION` to create a new function if one doesn't exist by that
name, or alter an existing function, in a single statement.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

### Scalar function syntax

```syntaxsql
CREATE [ OR ALTER ] FUNCTION [ schema_name. ] function_name
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
    [ INLINE = AUTO ]
  | [ SCHEMABINDING ]
  | [ RETURNS NULL ON NULL INPUT | CALLED ON NULL INPUT ]
}
```

### Inline table-valued function syntax

```syntaxsql
CREATE [ OR ALTER ] FUNCTION [ schema_name. ] function_name
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

### External function syntax

An external UDF is a function that references an external [Fabric User Data Function](/fabric/data-engineering/user-data-functions/user-data-functions-overview).

```syntaxsql
CREATE FUNCTION [ schema_name. ] function_name
[ RETURNS return_data_type ]
AS EXTERNAL FUNCTION external_function_set_name.external_function_name
[ ; ]
```

> [!NOTE]
> External UDFs are a preview feature in Fabric Data Warehouse.

The `CREATE FUNCTION` statement automatically derives the return type, parameters, and parameter types from the definition of the referenced external Fabric User Data Function. If the signature of the external Fabric User Data Function changes, you must recreate the function to synchronize its definition with the updated signature.

## Arguments

#### *schema_name*

The name of the schema to which the user-defined function belongs.

#### *function_name*

The name of the user-defined function. Function names must follow the rules for identifiers and be unique within the database and its schema.

For native T-SQL scalar and inline table-valued function definitions, include
parentheses after the function name even when there are no parameters.
External function definitions use the separate `AS EXTERNAL FUNCTION` syntax.

#### @*parameter_name*

A parameter in the user-defined function. You can declare one or more parameters.

A function can have up to 2,100 parameters. When a user or application calls a function, you must supply the value for each declared parameter unless you define a default value for the parameter.

Specify a parameter name by using an at sign (`@`) as the first character. The parameter
name must follow the rules for identifiers. Parameters are local to the function. You
can use the same parameter names in other functions. Parameters can only replace
constants. They can't be used instead of table names, column names, or the names of
other database objects.

`ANSI_WARNINGS` isn't honored when you pass parameters in a stored procedure, user-defined function, or when you declare and set variables in a batch statement. For example, if you define a variable as **char(3)**, and then set it to a value larger than three characters, the data is truncated to the defined size and SQL statement succeeds.    

#### *parameter_data_type*

The parameter data type. For [!INCLUDE [tsql](../../includes/tsql-md.md)] functions, all [scalar data types supported](/fabric/data-warehouse/data-types) are allowed. 

#### [ = *default* ]

A default value for the parameter. If you define a *default* value, you can execute the function without specifying a value for that parameter.

When a parameter of the function has a default value, you must specify the keyword `DEFAULT` when calling the function to retrieve the default value. This behavior is different from using parameters with default values in stored procedures in which omitting the parameter also implies the default value.

#### *return_data_type*

The return value of a scalar user-defined function. 

For functions in Fabric Data Warehouse, you can use all data types except for **rowversion**/**timestamp**. Nonscalar types like **table** aren't allowed.

For external UDFs (Preview), the `RETURNS` clause is optional. If omitted, the return type is automatically inferred from the return type of the referenced Fabric User Data Function. You can specify the `RETURNS` clause to override the inferred return type when needed.

#### *function_body*

A series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. 

In scalar functions, *function_body* is a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that together evaluate to a scalar value, which can include:

- Single statement expression
- Multi-statement expressions (`IF`/`ELSE` and `BEGIN`/`END` blocks)
- Local variables
- Calls to built-in SQL functions available
- Calls to other UDFs
- `SELECT` statements, and references to tables, views, and inline table-valued functions
- Control-of-flow statements, including `IF`/`ELSE`, `WHILE`, `BREAK`, `CONTINUE`, and `RETURN`

For external UDFs (Preview), you can't specify the *function_body* because the function
implementation is defined externally in the associated Fabric User Data Function.
The database stores only the function metadata. The executable logic resides in the
external function definition.

#### *scalar_expression*

Specifies the scalar value that the scalar function returns.

#### *select_stmt*

The single `SELECT` statement that defines the return value of an inline table-valued
function. For an inline table-valued function, there's no function body. The table is
the result set of a single `SELECT` statement.

#### TABLE

Specifies that the return value of the table-valued function (TVF) is a table. You can only pass constants and @*local_variables* to TVFs.

In inline TVFs (preview), you define the `TABLE` return value through a single `SELECT` statement. Inline functions don't have associated return variables.

#### <function_option>

In Fabric Data Warehouse, the `ENCRYPTION` and `EXECUTE AS` keywords aren't supported. 

The supported function options include:

INLINE = AUTO

Specifies whether a scalar user-defined function can be created or altered
irrespective of inlining requirements. The `INLINE` clause is optional. For an
inlineable scalar UDF, specifying `INLINE = AUTO` doesn't change its inlineability,
or execution behavior.

For a supported standalone calling pattern, see
[Create a scalar UDF for standalone execution](#e-create-a-scalar-udf-for-standalone-execution).

SCHEMABINDING

   Specifies that the function is bound to the database objects that it references. When you specify `SCHEMABINDING`, you can't modify the underlying objects (such as a view or a table, for example) in a way that affects the function definition. You must first modify or drop the function definition to remove dependencies on the object that you want to modify.      

The binding of the function to the objects it references is removed only when one of the following actions occurs:

- You drop the function.

- You `ALTER` the function statement and remove the `SCHEMABINDING` option.

You can only schema bind a function if the following conditions are true:

- Any user-defined functions that the function references are also schema-bound.

- The function references objects by using a two-part name.

- Within the body of UDFs, you can only reference built-in functions and other UDFs in the same database.

- The user who executes the `CREATE FUNCTION` statement has REFERENCES permission on the database objects that the function references.

To remove SCHEMABINDING, use `ALTER`.

##### RETURNS NULL ON NULL INPUT | CALLED ON NULL INPUT

 Specifies the `OnNULLCall` attribute of a scalar-valued function. If you don't specify this attribute, `CALLED ON NULL INPUT` is implied by default, and the function body executes even if `NULL` is passed as an argument.  

#### AS EXTERNAL FUNCTION

References a [Fabric User Data Function (UDF)](/fabric/data-engineering/user-data-functions/user-data-functions-overview). When the function is invoked, execution is delegated to the referenced Fabric User Data Function, where the logic is executed and the result is returned to the T-SQL caller.

This clause accepts the following parameters:

- **Function set name**: The name of the function set that contains the Fabric User Data Function. Every Fabric User Data Function must belong to a function set.
- **Function name**: The name of the Fabric User Data Function to reference.

The following syntax shows how these parameters are specified:

```syntaxsql
CREATE FUNCTION [schema_name.]function_name
AS EXTERNAL FUNCTION <<functionset_name>>.<<python_function_name>>
```

Because the function is executed remotely, calling an external function typically incurs higher latency than executing a native T-SQL user-defined function. Use external functions when the required business logic is implemented in Fabric and cannot be expressed directly in T-SQL.

The `CREATE FUNCTION` statement automatically derives the return type, parameters, and parameter types from the definition of the referenced external Fabric User Data Function. If the signature of the external Fabric User Data Function changes, you must recreate the function to synchronize its definition with the updated signature.

## Best practices

- Write your user-defined functions to be inlineable. For more information about the inlining concept, see [Inlining of scalar UDF](#inlining-of-scalar-udf). For examples of how to make a scalar UDF inlineable, see [Create scalar UDF in Microsoft Fabric Data Warehouse](/fabric/data-warehouse/how-to-inline-udf).

- If you don't create a user-defined function with schemabinding, changes to underlying objects can affect the function's definition and cause unexpected results when you invoke the function. When you specify `WITH SCHEMABINDING` when you create the function, you ensure that later changes to underlying objects cannot change or break the function's behavior.

- Whenever possible, implement your logic as a T-SQL user-defined function (UDF). Use an external UDF only for functionality that cannot be implemented in the T-SQL language. This approach helps minimize remote execution overhead and generally provides better query performance.

## Interoperability

### Inline table-valued user-defined functions

An inline table-valued function accepts only a single `SELECT` statement.

### Scalar user-defined functions

- When a scalar UDF is noninlineable, `CREATE` OR `ALTER` statement would reject DDL statement with a custom error message.
- Noninlineable function can be created by specifying `INLINE=AUTO` function option. Noninlineable function can't be used in a `SELECT ... FROM` query on a user table. `INLINE=AUTO` doesn't make function inlineable.

- The following statements are valid in a scalar-valued function:  
    - Assignment statements.
    - Control-of-flow statements except `TRY...CATCH` and `GOTO` statements.
    - `DECLARE` statements defining local data variables.
    - Calls to built-in functions.
    - References to tables/views/iTVFs/other scalar UDFs.

 - DML statements aren't allowed in scalar user-defined functions. 

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

 - [sys.parameters](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md): Displays information about the parameters defined in user-defined functions. The following example uses the `sys.parameters` catalog view to display function signatures, including return types and parameter data types:
    
   ```sql
    WITH metadata AS
    (
        SELECT  o.schema_id, o.object_id, o.name, p.parameter_id, p.name AS parameter_name, t.name AS type_name
        FROM sys.objects o JOIN sys.parameters p ON p.object_id = o.object_id JOIN sys.types t ON t.user_type_id = p.user_type_id
        WHERE o.type IN ('FN', 'XF')
    ),
    signature AS (
    SELECT SCHEMA_NAME(schema_id) as schema_name, name, ANY_VALUE(CASE WHEN parameter_id = 0 THEN type_name END) return_type, CONCAT(
        SCHEMA_NAME(schema_id), '.', name, '(',STRING_AGG(CASE WHEN parameter_id > 0 THEN CONCAT(parameter_name, ' ', type_name) END, ', ' ) WITHIN GROUP (ORDER BY parameter_id),
        ') -> ',  MAX(CASE WHEN parameter_id = 0 THEN type_name END)
    ) AS signature
    FROM metadata GROUP BY schema_id, object_id, name
    )
    SELECT *
    FROM signature;
   ```

- [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md): Displays the underlying objects referenced by a function.  

## Permissions

Members of the Fabric workspace Administrator, Member, and Contributor roles can create functions.

<a id="scalar-udf-inlining"></a>

## Inlining of Scalar UDF

Microsoft Fabric Data Warehouse automatically inlines eligible scalar UDFs during query
compilation.

Expression Block inlining compiles the function's statements, including branches and loops,
into a scalar expression that executes as part of the calling query. Scalar UDF inlining
transforms the function body into expressions or subqueries as part of the calling query.

A function can meet the requirements for both techniques, but the engine selects one based
on its definition. Different functions within the same query can use different techniques.

As Microsoft Fabric Data Warehouse expands its inlining capabilities, existing functions
gain them automatically.

### Supported function constructs

The following table compares supported function-body constructs.

| Function-body construct | Expression Block inlining | Scalar UDF inlining |
|---|---|---|
| Scalar expressions, local variables, and `IF`/`ELSE` | Supported | Supported |
| `WHILE` loops, including `BREAK` and `CONTINUE` | Supported | Not supported |
| Multiple `RETURN` statements | Supported | Not supported |
| `NEWID()` | Supported | Not supported |
| [Time-dependent functions](../functions/date-and-time-data-types-and-functions-transact-sql.md#functions-that-return-system-date-and-time-values) (for example, `GETDATE()` and `SYSDATETIME()`) | Supported | Not supported |
| Table, view, or inline TVF access | Not supported | Supported |
| Calls to scalar UDFs\* | Not supported | Supported |

\* User-defined functions can be nested: one UDF can call another.
The nesting level increases when a called function starts and decreases when it
finishes. Microsoft Fabric Data Warehouse supports up to four nesting levels when
a UDF body references a table, view, or inline TVF, or up to 32 levels otherwise.
Exceeding the nesting limit causes the calling function chain to fail.

### Built-in functions

Support for built-in functions, including
[system functions](../functions/system-functions-transact-sql.md?view=fabric&preserve-view=true),
differs between the two inlining techniques.

- **Expression Block inlining** doesn't support
  [AI functions](/fabric/data-warehouse/ai-functions),
  [aggregate functions](../functions/aggregate-functions-transact-sql.md?view=fabric&preserve-view=true),
  [metadata functions](../functions/metadata-functions-transact-sql.md),
  or [security functions](../functions/security-functions-transact-sql.md)
  in the function body.
- **Scalar UDF inlining** has built-in-function restrictions described in
  [Scalar UDF inlining requirements](../../relational-databases/user-defined-functions/scalar-udf-inlining.md?view=fabric&preserve-view=true#inlineable-scalar-udf-requirements).

For example, creating a UDF that uses `@@ROWCOUNT` without `INLINE = AUTO`
returns error 19841. The error message includes the following text:

```text
Function <function_name> cannot be inlined.
Expression block: Unsupported built-in @@ROWCOUNT found.
Scalar UDF inlining: Unsupported built-in @@ROWCOUNT found.
```

For a specific function definition, see
[Check whether a scalar UDF can be inlined](#check-whether-a-scalar-udf-can-be-inlined).

### Supported calling queries

The following table compares calling-query support for inlineable scalar UDFs.

| Query feature | Expression Block inlining | Scalar UDF inlining |
|---|---|---|
| UDF calls in the `SELECT` list, `WHERE`, or `JOIN ... ON` | Supported | Supported |
| Direct UDF calls in `GROUP BY` or `HAVING` | Supported | Not supported |
| Direct UDF calls in `ORDER BY` | Supported | Not supported |
| CTEs in the calling query | Supported | Not supported |
| `ROLLUP`, `CUBE`, or `GROUPING SETS` | Not supported | Not supported |

If any UDF call relies on Scalar UDF inlining, the entire query must meet that
technique's calling-query requirements.

With Scalar UDF inlining, project the UDF result in the `SELECT` list and order
by its column alias or ordinal instead of calling the UDF directly in `ORDER BY`.
For query-rewrite examples, see
[Adapt a calling query for Scalar UDF inlining](/fabric/data-warehouse/how-to-inline-udf#adapt-a-calling-query-for-scalar-udf-inlining).
For other execution limits, see [Limitations](#limitations).

### Check whether a scalar UDF can be inlined

The `sys.sql_modules` catalog view describes eligibility based on the function
definition. `is_inlineable` is `1` for an inlineable definition and `0` otherwise.
`inline_eligibility_mask` identifies the eligible techniques:

| `inline_eligibility_mask` | Definition eligibility |
|---|---|
| `0` | Neither technique |
| `1` | Scalar UDF inlining |
| `2` | Expression Block inlining |
| `3` | Either technique |

These values describe the function's definition, not the execution plan of a particular
query.

**Use the following sample query to check whether a scalar UDF is inlineable**:

```sql
SELECT
    SCHEMA_NAME(b.schema_id) AS function_schema_name,
    b.name AS function_name,
    b.type_desc AS function_type,
    a.is_inlineable,
    a.inline_eligibility_mask
FROM sys.sql_modules AS a
     INNER JOIN sys.objects AS b
         ON a.object_id = b.object_id
WHERE b.type IN ('FN');
```

## Limitations

- Calling a scalar UDF in an unsupported query context can return the error
  `Scalar UDF execution is currently unavailable in this context.`
- A query that relies on Scalar UDF inlining can fail if it contains more than
  10 UDF calls.

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
        DECLARE @Result VARCHAR(50);
        DECLARE @CleanedInput VARCHAR(50);

        -- Trim whitespace
        SET @CleanedInput = LTRIM(RTRIM(@InputString));

        -- Handle empty or null input
        IF @CleanedInput = '' OR @CleanedInput IS NULL
        BEGIN
            SET @Result = '';
        END
        ELSE IF LEN(@CleanedInput) <= 2
        BEGIN
            -- If string length is 1 or 2, just return the cleaned string
            SET @Result = @CleanedInput;
        END
        ELSE
        BEGIN
            -- Construct the masked string
            SET @Result = 
                LEFT(@CleanedInput, 1) +
                REPLICATE('*', LEN(@CleanedInput) - 2) +
                RIGHT(@CleanedInput, 1);
        END

        RETURN @Result
    END
```

You can call the function like this:

```sql
DECLARE @input varchar(100) = '123456789';

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
WHERE dbo.cleanInput(t.name) = '1*******9';
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

In Warehouse, you can also call scalar UDFs from data manipulation statements
such as `INSERT`, `UPDATE`, or `DELETE`. These data-modification examples don't
apply to the SQL analytics endpoint.

```sql
SELECT t.id, t.name, dbo.cleanInput (t.name) AS function_output 
INTO dbo.MyTable_new
FROM dbo.MyTable AS t;

UPDATE t
SET t.mycolumn_new = dbo.cleanInput (t.name)
FROM dbo.MyTable AS t;

DELETE t
FROM dbo.MyTable AS t
WHERE dbo.cleanInput(t.name) = '1*******9';
```

### D. Create an external function

The following example creates an external function named `geohash` in the `dbo` schema that references the `geohash` Fabric User Data Function in the `spatial_functions` function set. Because the parameter definitions and return type are inferred from the referenced Fabric User Data Function, they do not need to be specified explicitly in the `CREATE FUNCTION` statement.

```sql
CREATE FUNCTION dbo.geohash
AS EXTERNAL FUNCTION spatial_functions.geohash;
```

Once created, the external function can be used in T-SQL queries just like any other user-defined or built-in function. It can be referenced wherever function calls are supported, enabling seamless integration of Fabric User Data Functions into T-SQL code.

### E. Create a scalar UDF for standalone execution

The following table creation and data-loading setup requires Warehouse. On a SQL
analytics endpoint, use an existing source-backed table with the required columns and data.

```sql
CREATE TABLE dbo.Customer
(
    CustomerID INT NOT NULL,
    CustomerName VARCHAR(100) NOT NULL,
    CustomerType VARCHAR(20) NOT NULL,
    MemberSince DATE NOT NULL,
    YearToDateRevenue DECIMAL(18, 2) NOT NULL
);
GO

INSERT INTO dbo.Customer
(
    CustomerID,
    CustomerName,
    CustomerType,
    MemberSince,
    YearToDateRevenue
)
VALUES
    (1, 'Customer 1', 'Regular', '2020-01-15', 12000.00),
    (2, 'Customer 2', 'Premium', '2022-06-01', 24000.00),
    (3, 'Customer 3', 'VIP',     '2024-03-10', 36000.00);
GO
```

The function below combines table access with `GETUTCDATE()`. Table access prevents
Expression Block inlining, and `GETUTCDATE()` prevents Scalar UDF inlining.

Without `WITH INLINE = AUTO`, the definition is rejected at creation or alteration.
The error reports that the function cannot be inlined and gives the rejection
reasons for both techniques. Specify `WITH INLINE = AUTO` to allow creation:

```sql
CREATE OR ALTER FUNCTION dbo.GetCustomerTenureDays (@CustomerID INT)
RETURNS INT
WITH INLINE = AUTO
AS
BEGIN
    DECLARE @MemberSince DATE;

    SELECT @MemberSince = MemberSince
    FROM dbo.Customer
    WHERE CustomerID = @CustomerID;

    RETURN DATEDIFF(DAY, @MemberSince, GETUTCDATE());
END;
GO
```

The function is created but remains non-inlineable: `is_inlineable` and
`inline_eligibility_mask` are both `0`. A standalone call succeeds:

```sql
DECLARE @udf_output int;
SET @udf_output = dbo.GetCustomerTenureDays(1);
SELECT @udf_output as 'udf_output';
```

Calling the function in a query over a user table isn't supported:

```sql
SELECT
    CustomerID,
    dbo.GetCustomerTenureDays(CustomerID) AS TenureDays
FROM dbo.Customer;
```

This query returns `Scalar UDF execution is currently unavailable in this context.`

> [!TIP]
> To use this logic in a query over user tables, evaluate the current-time value in the
> caller and pass it as a parameter. For the rewritten function and calling query, see
> [Pass a time-dependent value into a data-access UDF](/fabric/data-warehouse/how-to-inline-udf#pass-a-time-dependent-value-into-a-data-access-udf).

## Related content

- [Create scalar user-defined functions in Microsoft Fabric Data Warehouse](/fabric/data-warehouse/how-to-inline-udf)
- [Scalar UDF inlining](../../relational-databases/user-defined-functions/scalar-udf-inlining.md)
- [Fabric Data Warehouse Migration Assistant](/fabric/data-warehouse/migration-assistant/)

::: moniker-end
::: moniker range="=azure-sqldw-latest"

[!INCLUDE [asa-md](../../includes/applies-to-version/asa.md)]

  Creates a user-defined function (UDF) in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. A user-defined function is a [!INCLUDE [tsql](../../includes/tsql-md.md)] routine that accepts parameters, performs an action, such as a complex calculation, and returns the result of that action as a value. User-defined table-valued functions (TVFs) return a table data type.

> [!TIP]
> For syntax in Fabric Data Warehouse, see the version of [CREATE FUNCTION](create-function-sql-data-warehouse.md?view=fabric&preserve-view=true) for Fabric Data Warehouse.

- In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], `CREATE FUNCTION` can return a table by using the syntax for inline table-valued functions (preview) or it can return a single value by using the syntax for scalar functions.
- In serverless SQL pools in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], `CREATE FUNCTION` can create inline table-value functions but not scalar functions. 

  Use this statement to create a reusable routine that you can use in these ways:  

- In [!INCLUDE [tsql](../../includes/tsql-md.md)] statements such as `SELECT`  
- In applications that call the function
- In the definition of another user-defined function
- To define a CHECK constraint on a column
- To replace a stored procedure
- Use an inline function as a filter predicate for a security policy

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

### Scalar function syntax

```syntaxsql
-- Transact-SQL Scalar Function Syntax (in dedicated pools in Azure Synapse Analytics)
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

A function can have up to 2,100 parameters. When a user or application calls a function, you must supply the value for each declared parameter unless you define a default value for the parameter.

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

- You drop the function.

- You `ALTER` the function statement and remove the `SCHEMABINDING` option.

You can only schema bind a function if the following conditions are true:

- Any user-defined functions that the function references are also schema-bound.

- The function references use one-part or two-part names.

- Within the body of UDFs, you can only reference built-in functions and other UDFs in the same database.

- The user who executes the `CREATE FUNCTION` statement has REFERENCES permission on the database objects that the function references.

To remove SCHEMABINDING, use `ALTER`.

RETURNS NULL ON NULL INPUT | **CALLED ON NULL INPUT**

 Specifies the `OnNULLCall` attribute of a scalar-valued function. If you don't specify this attribute, `CALLED ON NULL INPUT` is implied by default, and the function body executes even if `NULL` is passed as an argument.  

## Best practices

 If you don't create a user-defined function with the SCHEMABINDING clause, changes to underlying objects can affect the function's definition and cause unexpected results when you invoke it. Specify the `WITH SCHEMABINDING` clause when you create the function. This clause ensures that you can't modify the objects referenced in the function definition unless you also modify the function.

## Interoperability

 The following statements are valid in a scalar-valued function:  

- Assignment statements.  

- Control-of-Flow statements, except TRY...CATCH statements.  

- DECLARE statements that define local data variables.  

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
