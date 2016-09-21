---
title: "EXECUTE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3d4451aa-7b80-44ba-b50c-39a242ad8a8f
caps.latest.revision: 20
author: BarbKess
---
# EXECUTE (SQL Server PDW)
Executes a stored procedure or character string in SQL Server PDW.  
  
Invocation of a stored procedure without using EXECUTE or EXEC is possible only if the statement is the first statement in the batch.  
  
> [!IMPORTANT]  
> Before you call EXECUTE with a character string, validate the character string. Never execute a command constructed from user input that has not been validated. For more information, see [SQL Injection](http://msdn.microsoft.com/en-us/library/ms161953(v=sql11).aspx).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)[Transact-SQL Syntax Conventions](../Topic/Transact-SQL%20Syntax%20Conventions%20(Transact-SQL).md)  
  
## Syntax  
  
```Transact-SQL  
Execute a stored procedure  
[ { EXEC | EXECUTE } ]  
    procedure_name   
        [ { value | @variable [ OUT | OUTPUT ] } ] [ ,...n ] }  
[;]  
  
Execute a SQL string  
{ EXEC | EXECUTE }  
    ( { @string_variable | [ N ] 'tsql_string' } [ +...n ] )  
[;]  
```  
  
## Arguments  
*procedure_name*  
Is the fully qualified or non-fully qualified name of the stored procedure to call. Procedure names must comply with the rules for identifiers. The names of extended stored procedures are always case-sensitive.  
  
*value*  
Is the value of the parameter to pass to the stored procedure. In SQL Server PDW parameter values must be supplied in the order defined in the procedure.  
  
If the value of a parameter is an object name, character string, or qualified by a database name or schema name, the whole name must be enclosed in single quotation marks. If the value of a parameter is a keyword, the keyword must be enclosed in double quotation marks.  
  
The default can also be NULL. Generally, the procedure definition specifies the action that should be taken if a parameter value is NULL.  
  
@*variable*  
Is the variable that stores a parameter or a return parameter.  
  
OUTPUT  
Specifies that the module or command string returns a parameter. The matching parameter in the module or command string must also have been created by using the keyword OUTPUT (or OUT). Use this keyword when you use cursor variables as parameters.  
  
If *value* is defined as OUTPUT of a module executed against a linked server, any changes to the corresponding @*parameter* performed by the OLE DB provider will be copied back to the variable at the end of the execution of module.  
  
If OUTPUT parameters are being used and the intent is to use the return values in other statements within the calling batch or module, the value of the parameter must be passed as a variable, such as  @*parameter* = @*variable*. You cannot execute a module by specifying OUTPUT for a parameter that is not defined as an OUTPUT parameter in the module. Constants cannot be passed to module by using OUTPUT; the return parameter requires a variable name. The data type of the variable must be declared and a value assigned before executing the procedure.  
  
Return parameters can be of any data type except the LOB data types.  
  
*@string_variable*  
Is the name of a local variable. *@string_variable* can be any **char**, **varchar**, **nchar**, or **nvarchar** data type.  
  
[N] '*tsql_string*'  
Is a constant string. *tsql_string* can be any **nvarchar** or **varchar** data type. If the N is included, the string is interpreted as **nvarchar** data type.  
  
## Remarks  
For more information on using the EXECUTE statement, see [EXECUTE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188332(v=sql11).aspx).  
  
To execute a stored procedure, list the parameter values in order, or name the parameter.  
  
When using positional parameters the invocation of the procedure does not explicitly define the parameter name, but relies on the parameter value being in the expected parameter position. For example, `EXEC [dbo].[sp_ExampleProc] 0, 1, 2` as positional parameters 0, 1, 2 being provided to the `[dbo].[sp_ExampleProc]` stored procedure.  
  
When using named parameters the invocation explicitly identifies the parameter name while assigning its value as part of the stored procedure invocation. For example, `EXEC [dbo].[sp_ExampleProc] @Param0 = 0, @Param1 = 1, @Param2 = 2` as named parameters being assigned the values 0, 1, 2, respectively, to the `[dbo].[sp_ExampleProc]` stored procedure.  
  
When using both positional and named parameters, invocation respects the ordinal position of the parameters while having some combination of explicit naming of parameters and not.  For example, `EXEC [dbo].[sp_ExampleProc] 0, 1, @Param2 = 2` as a combination of ordinal and explicitly named parameters being provided for invocation of the `[dbo].[sp_ExampleProc]` stored procedure. Positional parameters can be used until first appearance of a named parameter. Once first named parameter is specified, all subsequent parameters also must be named.  
  
Connections that use the .NET Framework Data Provider for SQL Server do not support positional parameters. This might affect connections from SSIS, SSRS, and SSAS, and linked servers.  
  
## Permissions  
Any database user can use the **EXECUTE** statement. To execute a store procedure requires **EXECUTE** permission on the procedure and permission to perform the actions contained in the stored procedure. To execute a string requires permission to perform the actions contained in the string.  
  
## Examples  
  
### Example A: Basic Procedure Execution  
Executing a stored procedure:  
  
```  
EXEC proc1;  
```  
  
Calling a stored procedure with name determined at runtime:  
  
```  
EXEC ('EXEC ' + @var);  
```  
  
Calling a stored procedure from within a stored procedure:  
  
```  
CREATE sp_first AS EXEC sp_second; EXEC sp_third;  
```  
  
### Example B: Executing Strings  
Executing a SQL string:  
  
```  
EXEC ('SELECT * FROM sys.types');  
```  
  
Executing a nested string:  
  
```  
EXEC ('EXEC (''SELECT * FROM sys.types'')');  
```  
  
Executing a string variable:  
  
```  
DECLARE @stringVar nvarchar(100);  
SET @stringVar = N'SELECT name FROM' + ' sys.sql_logins';  
EXEC (@stringVar);  
```  
  
### Example C: Procedures with Parameters  
The following example creates a procedure with parameters and demonstrates 3 ways to execute the procedure:  
  
```  
USE AdventureWorksPDW2012;  
GO  
CREATE PROC ProcWithParameters  
    @name nvarchar(50),  
@color nvarchar (15)  
AS   
SELECT ProductKey, EnglishProductName, Color FROM [dbo].[DimProduct]  
WHERE EnglishProductName LIKE @name  
AND Color = @color;  
GO  
  
-- Executing using positional parameters  
EXEC ProcWithParameters N'%arm%', N'Black';  
-- Executing using named parameters in order  
EXEC ProcWithParameters @name = N'%arm%', @color = N'Black';  
-- Executing using named parameters out of order  
EXEC ProcWithParameters @color = N'Black', @name = N'%arm%';  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/create-procedure-sql-server-pdw.md)  
[ALTER PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/alter-procedure-sql-server-pdw.md)  
[DROP PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/drop-procedure-sql-server-pdw.md)  
  
