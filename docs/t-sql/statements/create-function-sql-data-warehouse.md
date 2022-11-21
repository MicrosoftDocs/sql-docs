---
title: "CREATE FUNCTION (Azure Synapse Analytics)"
description: CREATE FUNCTION (Azure Synapse Analytics)
author: VanMSFT
ms.author: vanto
ms.date: "09/17/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# CREATE FUNCTION (Azure Synapse Analytics)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Creates a user-defined function in [!INCLUDE[ssSDW](../../includes/ssazuresynapse_md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]. A user-defined function is a [!INCLUDE[tsql](../../includes/tsql-md.md)] routine that accepts parameters, performs an action, such as a complex calculation, and returns the result of that action as a value. In [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], the return value must be a scalar (single) value. In [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], CREATE FUNCTION can return a table by using the syntax for inline table-valued functions (preview) or it can return a single value by using the syntax for scalar functions. Use this statement to create a reusable routine that can be used in these ways:  
  
-   In [!INCLUDE[tsql](../../includes/tsql-md.md)] statements such as SELECT  
  
-   In applications calling the function  
  
-   In the definition of another user-defined function  
  
-   To define a CHECK constraint on a column  
  
-   To replace a stored procedure  
  
-   Use an inline function as a filter predicate for a security policy  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Transact-SQL Scalar Function Syntax  (in dedicated pools in Azure Synapse Analytics and Parallel Data Warehouse)
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
 *schema_name*  
 Is the name of the schema to which the user-defined function belongs.  
  
 *function_name*  
 Is the name of the user-defined function. Function names must comply with the rules for identifiers and must be unique within the database and to its schema.  
  
> [!NOTE]  
>  Parentheses are required after the function name even if a parameter is not specified.  
  
 @*parameter_name*  
 Is a parameter in the user-defined function. One or more parameters can be declared.  
  
 A function can have a maximum of 2,100 parameters. The value of each declared parameter must be supplied by the user when the function is executed, unless a default for the parameter is defined.  
  
 Specify a parameter name by using an at sign (@) as the first character. The parameter name must comply with the rules for identifiers. Parameters are local to the function; the same parameter names can be used in other functions. Parameters can take the place only of constants; they cannot be used instead of table names, column names, or the names of other database objects.  
  
> [!NOTE]  
>  ANSI_WARNINGS is not honored when you pass parameters in a stored procedure, user-defined function, or when you declare and set variables in a batch statement. For example, if a variable is defined as **char(3)**, and then set to a value larger than three characters, the data is truncated to the defined size and the INSERT or UPDATE statement succeeds.  
  
 *parameter_data_type*  
 Is the parameter data type. For [!INCLUDE[tsql](../../includes/tsql-md.md)] functions, all scalar data types supported in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] are allowed. The timestamp (rowversion) data type is not a supported type.  
  
 [ =*default* ]  
 Is a default value for the parameter. If a *default* value is defined, the function can be executed without specifying a value for that parameter.  
  
 When a parameter of the function has a default value, the keyword DEFAULT must be specified when the function is called to retrieve the default value. This behavior is different from using parameters with default values in stored procedures in which omitting the parameter also implies the default value.  
  
 *return_data_type*  
 Is the return value of a scalar user-defined function. For [!INCLUDE[tsql](../../includes/tsql-md.md)] functions, all scalar data types supported in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] are allowed. The timestamp (rowversion) data type is not a supported type. The cursor and table nonscalar types are not allowed.  
  
 *function_body*  
 Series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  The function_body cannot contain a SELECT statement and cannot reference database data.  The function_body cannot reference tables or views. The function body can call other deterministic functions but cannot call nondeterministic functions. 
  
 In scalar functions, *function_body* is a series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that together evaluate to a scalar value.  
  
 *scalar_expression*  
 Specifies the scalar value that the scalar function returns.  

 *select_stmt* **Applies to:** Azure Synapse Analytics  
 Is the single SELECT statement that defines the return value of an inline table-valued function (preview).

 TABLE **Applies to:** Azure Synapse Analytics  
 Specifies that the return value of the table-valued function (TVF) is a table. Only constants and @*local_variables* can be passed to TVFs.

 In inline TVFs (preview), the TABLE return value is defined through a single SELECT statement. Inline functions do not have associated return variables.
  
 **\<function_option>::=** 
  
 Specifies that the function will have one or more of the following options.  
  
 SCHEMABINDING  
 Specifies that the function is bound to the database objects that it references. When SCHEMABINDING is specified, the base objects cannot be modified in a way that would affect the function definition. The function definition itself must first be modified or dropped to remove dependencies on the object that is to be modified.  
  
 The binding of the function to the objects it references is removed only when one of the following actions occurs:  
  
-   The function is dropped.  
  
-   The function is modified by using the ALTER statement with the SCHEMABINDING option not specified.  
  
 A function can be schema bound only if the following conditions are true:  
  
-   Any user-defined functions referenced by the function are also schema-bound.  
  
-   The functions and other UDFs referenced by the function are referenced using a one-part or two-part name.  
  
-   Only built-in functions and other UDFs in the same database can be referenced within the body of UDFs.  
  
-   The user who executed the CREATE FUNCTION statement has REFERENCES permission on the database objects that the function references.  
  
 To remove SCHEMABINDING use ALTER  
  
 RETURNS NULL ON NULL INPUT | **CALLED ON NULL INPUT**  
 Specifies the **OnNULLCall** attribute of a scalar-valued function. If not specified, CALLED ON NULL INPUT is implied by default. This means that the function body executes even if NULL is passed as an argument.  
  
## Best practices  
 If a user-defined function is not created with the SCHEMABINDING clause, changes that are made to underlying objects can affect the definition of the function and produce unexpected results when it is invoked. We recommend that you implement one of the following methods to ensure that the function does not become outdated because of changes to its underlying objects:  
  
-   Specify the WITH SCHEMABINDING clause when you are creating the function. This ensures that the objects referenced in the function definition cannot be modified unless the function is also modified.  
  
## Interoperability  
 The following statements are valid in a scalar-valued function:  
  
-   Assignment statements.  
  
-   Control-of-Flow statements except TRY...CATCH statements.  
  
-   DECLARE statements defining local data variables.  

In an inline table-valued function (preview), only a single select statement is allowed.
  
## Limitations and restrictions  
 User-defined functions cannot be used to perform actions that modify the database state.  
  
 User-defined functions can be nested; that is, one user-defined function can call another. The nesting level is incremented when the called function starts execution, and decremented when the called function finishes execution. User-defined functions can be nested up to 32 levels. Exceeding the maximum levels of nesting causes the whole calling function chain to fail.   
  
## Metadata  
 This section lists the system catalog views that you can use to return metadata about user-defined functions.  
  
 [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) : Displays the definition of [!INCLUDE[tsql](../../includes/tsql-md.md)] user-defined functions. For example:  
  
```sql  
SELECT definition, type   
FROM sys.sql_modules AS m  
JOIN sys.objects AS o   
    ON m.object_id = o.object_id   
    AND type = ('FN');  
GO  
  
```  
  
 [sys.parameters](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md) : Displays information about the parameters defined in user-defined functions.  
  
 [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) : Displays the underlying objects referenced by a function.  
  
## Permissions  
 Requires CREATE FUNCTION permission in the database and ALTER permission on the schema in which the function is being created.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Using a scalar-valued user-defined function to change a data type  
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
>  Scalar functions are not available in the serverless SQL pools.

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)]  

### A. Creating an inline table-valued function
 The following example creates an inline table-valued function to return some key information on modules, filtering by the `objectType` parameter. It includes a default value to return all modules when the function is called with the DEFAULT parameter. This example makes use of some of the system catalog views mentioned in [Metadata](#metadata).

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
The function can then be called to return all view (**V**) objects with:
```sql
select * from dbo.ModulesByType('V');
```

> [!NOTE]  
>  Inline table-value functions are available in the serverless SQL pools, but in preview in the dedicated SQL pools.

### B. Combining results of an inline table-valued function
 This simple example uses the previously created inline TVF to demonstrate how its results can be combined with other tables using cross apply. Here, we select all columns from both sys.objects and the results of `ModulesByType` for all rows matching on the *type* column. For more details on using apply, see [FROM clause plus JOIN, APPLY, PIVOT](../../t-sql/queries/from-transact-sql.md).

```sql
SELECT * 
FROM sys.objects o
CROSS APPLY dbo.ModulesByType(o.type);
GO
```

> [!NOTE]  
>  Inline table-value functions are available in the serverless SQL pools, but in preview in the dedicated SQL pools.

## See also  
 [ALTER FUNCTION (SQL Server PDW)](/previous-versions/sql/)   
 [DROP FUNCTION (SQL Server PDW)](/previous-versions/sql/)  
  
  

