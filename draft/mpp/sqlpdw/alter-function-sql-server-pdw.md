---
title: "ALTER FUNCTION (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 65c2b673-3389-48f4-996f-6e7ee0448578
caps.latest.revision: 5
author: BarbKess
---
# ALTER FUNCTION (SQL Server PDW)
Alters an existing user-defined function that was previously created by executing the CREATE FUNCTION statement, without changing permissions and without affecting any dependent functions, or stored procedures.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions (SQL Server PDW)](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
Scalar Functions  
ALTER FUNCTION [ schema_name. ] function_name ( [ { @parameter_name [ AS ] parameter_data_type   
    [ =default ] }   
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
{   [ SCHEMABINDING ]  
  | [ RETURNS NULL ON NULL INPUT | CALLED ON NULL INPUT ]  
}  
```  
  
## Arguments  
*schema_name*  
Is the name of the schema to which the user-defined function belongs.  
  
*function_name*  
Is the user-defined function to be changed.  
  
> [!NOTE]  
> Parentheses are required after the function name even if a parameter is not specified.  
  
**@***parameter_name*  
Is a parameter in the user-defined function. One or more parameters can be declared.  
  
A function can have a maximum of 2,100 parameters. The value of each declared parameter must be supplied by the user when the function is executed, unless a default for the parameter is defined.  
  
Specify a parameter name by using an at sign (**@**) as the first character. The parameter name must comply with the rules for [identifiers](assetId:///171291bb-f57f-4ad1-8cea-0b092d5d150c). Parameters are local to the function; the same parameter names can be used in other functions. Parameters can take the place only of constants; they cannot be used instead of table names, column names, or the names of other database objects.  
  
> [!NOTE]  
> ANSI_WARNINGS is not honored when passing parameters in a stored procedure, user-defined function, or when declaring and setting variables in a batch statement. For example, if a variable is defined as **char(3)**, and then set to a value larger than three characters, the data is truncated to the defined size and the INSERT or UPDATE statement succeeds.  
  
*parameter_data_type*  
Is the parameter data type. For Transact\-SQL functions, all scalar data types supported in PDW are allowed. The timestamp (rowversion) data type is not a supported type in SQL Server PDW.  
  
[ **=***default* ]  
Is a default value for the parameter. If a *default* value is defined, the function can be executed without specifying a value for that parameter.  
  
When a parameter of the function has a default value, the keyword DEFAULT must be specified when calling the function to retrieve the default value. This behavior is different from using parameters with default values in stored procedures in which omitting the parameter also implies the default value.  
  
*return_data_type*  
Is the return value of a scalar user-defined function. The timestamp (rowversion) data type is not a supported type in SQL Server PDW.  
  
*function_body*  
Specifies that a series of Transact\-SQL statements, which do not reference database data (tables or views), define the value of the function.  
  
In scalar functions, *function_body* is a series of Transact\-SQL statements that together evaluate to a scalar value.  
  
*scalar_expression*  
Specifies that the scalar function returns a scalar value.  
  
**<function_option>::=**  
  
Specifies the function will have one or more of the following options.  
  
SCHEMABINDING  
Specifies that the function is bound to the database objects that it references. This condition will prevent changes to the function if other schema bound objects are referencing it.  
  
The binding of the function to the objects it references is removed only when one of the following actions occurs:  
  
-   The function is dropped.  
  
-   The function is modified by using the ALTER statement with the SCHEMABINDING option not specified.  
  
RETURNS NULL ON NULL INPUT | CALLED ON NULL INPUT  
Specifies the **OnNULLCall** attribute of a scalar-valued function. If not specified, CALLED ON NULL INPUT is implied by default. This means that the function body executes even if NULL is passed as an argument.  
  
## Permissions  
Requires ALTER permission on the function or on the schema.  
  
## Examples  
  
### A. Altering a scalar-valued user-defined function used to change a data type  
The following example alters the `ConvertInput` user-defined function from the `dbo` schema in the current database. (To create this function, see Example A in [CREATE FUNCTION](../Topic/CREATE%20FUNCTION.md).) This ALTER statement changes the output from **decimal(10,2)**, to **decimal(10,4)**.  
  
```  
ALTER FUNCTION dbo.ConvertInput (@MyValueIn int)  
RETURNS decimal(10,4)  
AS  
BEGIN  
    DECLARE @MyValueOut int;  
    SET @MyValueOut= CAST( @MyValueIn AS decimal(10,4));  
    RETURN(@MyValueOut);  
END;  
GO  
  
SELECT dbo.ConvertInput(15) AS 'ConvertedValue';  
```  
  
## See Also  
[CREATE FUNCTION](../Topic/CREATE%20FUNCTION.md)  
[DROP FUNCTION](../Topic/DROP%20FUNCTION.md)  
  
