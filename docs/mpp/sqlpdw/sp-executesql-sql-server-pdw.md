---
title: "sp_executesql (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 665f7f0b-9314-4f5d-b09b-58241cba20d6
caps.latest.revision: 11
author: BarbKess
---
# sp_executesql (SQL Server PDW)
Executes a Transact\-SQL statement or batch that can be reused many times, or one that has been built dynamically. The Transact\-SQL statement or batch can contain embedded parameters.  
  
> [!IMPORTANT]  
> Run time-compiled Transact\-SQL statements can expose applications to malicious attacks.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_executesql [ @stmt= ] statement  
[   
  { , [ @params = ] N'@parameter_name data_type [ OUT | OUTPUT ][ ,...n ]' }   
     { , [ @param1= ] 'value1' [ ,...n ] }  
]  
```  
  
## Arguments  
[ @stmt= ] *statement*  
Is a Unicode string that contains a Transact\-SQL statement or batch. @stmt must be either a Unicode constant or a Unicode variable. More complex Unicode expressions, such as concatenating two strings with the + operator, are not allowed. Character constants are not allowed. If a Unicode constant is specified, it must be prefixed with an **N**. For example, the Unicode constant **N'sp_who'** is valid, but the character constant **'sp_who'** is not. If specified as a string literal, *@stmt* has a maximum size of 2 GB, the maximum size of **nvarchar(max)**. If specified as a Unicode variable, *@stmt* cannot exceed 4000 characters due to SQL Server PDW limitations related to Unicode variables.  
  
> [!NOTE]  
> @stmt can contain parameters having the same form as a variable name, for example: `N'SELECT * FROM HumanResources.Employee WHERE EmployeeID = @IDParameter'`  
  
Each parameter included in @stmt must have a corresponding entry in both the @params parameter definition list and the parameter values list.  
  
[ @params= ] N'@*parameter_name**data_type* [ ,... *n* ] '  
Is one string that contains the definitions of all parameters that have been embedded in @stmt. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in @stmtmust be defined in @params. If the Transact\-SQL statement or batch in @stmt does not contain parameters, @params is not required. The default value for this parameter is NULL. If specified as a string literal, *@params* has a maximum size of 2 GB, the maximum size of **nvarchar(max)**. If specified as a Unicode variable, *@params* cannot exceed 4000 characters due to SQL Server PDW limitations related to Unicode variables.  
  
[ @param1= ] '*value1*'  
Is a value for the first parameter that is defined in the parameter string. The value can be a Unicode constant or a Unicode variable. There must be a parameter value supplied for every parameter included in @stmt. The values are not required when the Transact\-SQL statement or batch in @stmt has no parameters. If specified as a string literal, *@param1* has a maximum size of 2 GB, the maximum size of **nvarchar(max)**. If specified as a Unicode variable, *@param1* cannot exceed 4000 characters due to SQL Server PDW limitations related to Unicode variables.  
  
[ OUT | OUTPUT ]  
Indicates that the parameter is an output parameter. **text**, **ntext**, and **image** parameters can be used as OUTPUT parameters, unless the procedure is a common language runtime (CLR) procedure. An output parameter that uses the OUTPUT keyword can be a cursor placeholder, unless the procedure is a CLR procedure.  
  
*n*  
Is a placeholder for the values of additional parameters. Values can only be constants or variables. Values cannot be more complex expressions such as functions, or expressions built by using operators.  
  
## Return Code Values  
None  
  
## Result Sets  
Returns the result sets from all the SQL statements built into the SQL string.  
  
## Remarks  
sp_executesql parameters must be entered in the specific order as described in the "Syntax" section earlier in this topic. If the parameters are entered out of order, an error message will occur.  
  
sp_executesql has the same behavior as EXECUTE with regard to batches, the scope of names, and database context. The Transact\-SQL statement or batch in the sp_executesql @stmt parameter is not compiled until the sp_executesql statement is executed. The contents of @stmt are then compiled and executed as an execution plan separate from the execution plan of the batch that called sp_executesql. The sp_executesql batch cannot reference variables declared in the batch that calls sp_executesql. Local cursors or variables in the sp_executesql batch are not visible to the batch that calls sp_executesql. Changes in database context last only to the end of the sp_executesql statement.  
  
sp_executesql can be used instead of stored procedures to execute a Transact\-SQL statement many times when the change in parameter values to the statement is the only variation. Because the Transact\-SQL statement itself remains constant and only the parameter values change, the SQL Server query optimizer is likely to reuse the execution plan it generates for the first execution.  
  
> [!NOTE]  
> To improve performance use fully qualified object names in the statement string.  
  
sp_executesql supports the setting of parameter values separately from the Transact\-SQL string as shown in the following example.  
  
```  
USE AdventureWorksPDW2012  
GO  
  
DECLARE @IntVariable int;  
DECLARE @SQLString nvarchar(500);  
DECLARE @ParmDefinition nvarchar(500);  
  
/* Build the SQL string one time.*/  
  
SET @SQLString =  
     N'SELECT EmployeeKey, LastName, FirstName, Title, LoginID  
       FROM AdventureWorksPDW2012.dbo.DimEmployee   
       WHERE EmployeeKey = @EmployeeKey';  
SET @ParmDefinition = N'@EmployeeKey tinyint';  
/* Execute the string with the first parameter value. */  
SET @IntVariable = 197;  
EXECUTE sp_executesql @SQLString, @ParmDefinition,  
                      @EmployeeKey = @IntVariable;  
/* Execute the same string with the second parameter value. */  
SET @IntVariable = 109;  
EXECUTE sp_executesql @SQLString, @ParmDefinition,  
                      @EmployeeKey = @IntVariable;  
```  
  
Being able to substitute parameters in sp_executesql offers the following advantages to using the EXECUTE statement to execute a string:  
  
-   Because the actual text of the Transact\-SQL statement in the sp_executesql string does not change between executions, the query optimizer will probably match the Transact\-SQL statement in the second execution with the execution plan generated for the first execution. Therefore, SQL Server does not have to compile the second statement.  
  
-   The Transact\-SQL string is built only one time.  
  
-   The integer parameter is specified in its native format. Casting to Unicode is not required.  
  
## Permissions  
Requires membership in the public role.  
  
## Examples  
  
### A. Executing a simple SELECT statement  
The following example creates and executes a simple `SELECT` statement that contains an embedded parameter named `@level`.  
  
```  
USE AdventureWorksPDW2012  
GO  
  
EXECUTE sp_executesql   
          N'SELECT * FROM AdventureWorksPDW2012.dbo.DimEmployee   
          WHERE EmployeeKey = @level',  
          N'@level tinyint',  
          @level = 109;  
```  
  
For additional examples, see [sp_executesql (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188001.aspx).  
  
## See Also  
[EXECUTE &#40;SQL Server PDW&#41;](../sqlpdw/execute-sql-server-pdw.md)  
[Procedures &#40;SQL Server PDW&#41;](../sqlpdw/procedures-sql-server-pdw.md)  
  
