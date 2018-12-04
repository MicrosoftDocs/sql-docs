---
title: "CREATE FUNCTION (SQL Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 8cad1b2c-5ea0-4001-9060-2f6832ccd057
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# CREATE FUNCTION (SQL Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Creates a user-defined function in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. A user-defined function is a [!INCLUDE[tsql](../../includes/tsql-md.md)] routine that accepts parameters, performs an action, such as a complex calculation, and returns the result of that action as a value. The return value must be a scalar (single) value. Use this statement to create a reusable routine that can be used in these ways:  
  
-   In [!INCLUDE[tsql](../../includes/tsql-md.md)] statements such as SELECT  
  
-   In applications calling the function  
  
-   In the definition of another user-defined function  
  
-   To define a CHECK constraint on a column  
  
-   To replace a stored procedure  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
--Transact-SQL Scalar Function Syntax  
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
  
## Best Practices  
 If a user-defined function is not created with the SCHEMABINDING clause, changes that are made to underlying objects can affect the definition of the function and produce unexpected results when it is invoked. We recommend that you implement one of the following methods to ensure that the function does not become outdated because of changes to its underlying objects:  
  
-   Specify the WITH SCHEMABINDING clause when you are creating the function. This ensures that the objects referenced in the function definition cannot be modified unless the function is also modified.  
  
## Interoperability  
 The following statements are valid in a function:  
  
-   Assignment statements.  
  
-   Control-of-Flow statements except TRY...CATCH statements.  
  
-   DECLARE statements defining local data variables.  
  
## Limitations and Restrictions  
 User-defined functions cannot be used to perform actions that modify the database state.  
  
 User-defined functions can be nested; that is, one user-defined function can call another. The nesting level is incremented when the called function starts execution, and decremented when the called function finishes execution. User-defined functions can be nested up to 32 levels. Exceeding the maximum levels of nesting causes the whole calling function chain to fail.   
  
## Metadata  
 This section lists the system catalog views that you can use to return metadata about user-defined functions.  
  
 [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) : Displays the definition of [!INCLUDE[tsql](../../includes/tsql-md.md)] user-defined functions. For example:  
  
```  
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
  
```  
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
  
## See Also  
 [ALTER FUNCTION (SQL Server PDW)](https://msdn.microsoft.com/25ff3798-eb54-4516-9973-d8f707a13f6c)   
 [DROP FUNCTION (SQL Server PDW)](https://msdn.microsoft.com/1792a90d-0d06-4852-9dec-6de1b9cd229e)  
  
  


