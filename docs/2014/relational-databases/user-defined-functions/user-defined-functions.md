---
title: "User-Defined Functions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined functions [SQL Server], components"
  - "user-defined functions [SQL Server], about user-defined functions"
ms.assetid: d7ddafab-f5a6-44b0-81d5-ba96425aada4
author: rothja
ms.author: jroth
manager: craigg
---
# User-Defined Functions
  Like functions in programming languages, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user-defined functions are routines that accept parameters, perform an action, such as a complex calculation, and return the result of that action as a value. The return value can either be a single scalar value or a result set.  
  
 **In This Topic**  
  
 [User-defined Function Benefits](#Benefits)  
  
 [Types of Functions](#FunctionTypes)  
  
 [Guidelines](#Guidelines)  
  
 [Valid Statements in a Function](#ValidStatements)  
  
 [Schema Bound Functions](#SchemaBound)  
  
 [Specifying Parameters](#Parameters)  
  
 [Related Tasks](#Tasks)  
  
##  <a name="Benefits"></a> User-defined Function Benefits  
 The benefits of using user-defined functions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are:  
  
-   They allow modular programming.  
  
     You can create the function once, store it in the database, and call it any number of times in your program. User-defined functions can be modified independently of the program source code.  
  
-   They allow faster execution.  
  
     Similar to stored procedures, [!INCLUDE[tsql](../../includes/tsql-md.md)] user-defined functions reduce the compilation cost of [!INCLUDE[tsql](../../includes/tsql-md.md)] code by caching the plans and reusing them for repeated executions. This means the user-defined function does not need to be reparsed and reoptimized with each use resulting in much faster execution times.  
  
     CLR functions offer significant performance advantage over [!INCLUDE[tsql](../../includes/tsql-md.md)] functions for computational tasks, string manipulation, and business logic. [!INCLUDE[tsql](../../includes/tsql-md.md)] functions are better suited for data-access intensive logic.  
  
-   They can reduce network traffic.  
  
     An operation that filters data based on some complex constraint that cannot be expressed in a single scalar expression can be expressed as a function. The function can then invoked in the WHERE clause to reduce the number or rows sent to the client.  
  
> [!NOTE]  
>  [!INCLUDE[tsql](../../includes/tsql-md.md)] user-defined functions in queries can only be executed on a single thread (serial execution plan).  
  
##  <a name="FunctionTypes"></a> Types of Functions  
 Scalar Function  
 User-defined scalar functions return a single data value of the type defined in the RETURNS clause. For an inline scalar function, there is no function body; the scalar value is the result of a single statement. For a multistatement scalar function, the function body, defined in a BEGIN...END block, contains a series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that return the single value. The return type can be any data type except `text`, `ntext`, `image`, `cursor`, and `timestamp`.  
  
 Table-Valued Functions  
 User-defined table-valued functions return a `table` data type. For an inline table-valued function, there is no function body; the table is the result set of a single SELECT statement.  
  
 System Functions  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides many system functions that you can use to perform a variety of operations. They cannot be modified. For more information, see [Built-in Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/functions), [System Stored Functions &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/system-functions-for-transact-sql), and [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/system-dynamic-management-views).  
  
##  <a name="Guidelines"></a> Guidelines  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] errors that cause a statement to be canceled and continue with the next statement in the module (such as triggers or stored procedures) are treated differently inside a function. In functions, such errors cause the execution of the function to stop. This in turn causes the statement that invoked the function to be canceled.  
  
 The statements in a BEGIN...END block cannot have any side effects. Function side effects are any permanent changes to the state of a resource that has a scope outside the function such as a modification to a database table. The only changes that can be made by the statements in the function are changes to objects local to the function, such as local cursors or variables. Modifications to database tables, operations on cursors that are not local to the function, sending e-mail, attempting a catalog modification, and generating a result set that is returned to the user are examples of actions that cannot be performed in a function.  
  
> [!NOTE]  
>  If a CREATE FUNCTION statement produces side effects against resources that do not exist when the CREATE FUNCTION statement is issued, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executes the statement. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not execute the function when it is invoked.  
  
 The number of times that a function specified in a query is actually executed can vary between execution plans built by the optimizer. An example is a function invoked by a subquery in a WHERE clause. The number of times the subquery and its function is executed can vary with different access paths chosen by the optimizer.  
  
##  <a name="ValidStatements"></a> Valid Statements in a Function  
 The types of statements that are valid in a function include:  
  
-   DECLARE statements can be used to define data variables and cursors that are local to the function.  
  
-   Assignments of values to objects local to the function, such as using SET to assign values to scalar and table local variables.  
  
-   Cursor operations that reference local cursors that are declared, opened, closed, and deallocated in the function. FETCH statements that return data to the client are not allowed. Only FETCH statements that assign values to local variables using the INTO clause are allowed.  
  
-   Control-of-flow statements except TRY...CATCH statements.  
  
-   SELECT statements containing select lists with expressions that assign values to variables that are local to the function.  
  
-   UPDATE, INSERT, and DELETE statements modifying table variables that are local to the function.  
  
-   EXECUTE statements calling an extended stored procedure.  
  
### Built-in System Functions  
 The following nondeterministic built-in functions can be used in Transact-SQL user-defined functions.  
  
|||  
|-|-|  
|CURRENT_TIMESTAMP|@@MAX_CONNECTIONS|  
|GET_TRANSMISSION_STATUS|@@PACK_RECEIVED|  
|GETDATE|@@PACK_SENT|  
|GETUTCDATE|@@PACKET_ERRORS|  
|@@CONNECTIONS|@@TIMETICKS|  
|@@CPU_BUSY|@@TOTAL_ERRORS|  
|@@DBTS|@@TOTAL_READ|  
|@@IDLE|@@TOTAL_WRITE|  
|@@IO_BUSY||  
  
 The following nondeterministic built-in functions cannot be used in Transact-SQL user-defined functions.  
  
|||  
|-|-|  
|NEWID|RAND|  
|NEWSEQUENTIALID|TEXTPTR|  
  
 For a list of deterministic and nondeterministic built-in system functions, see [Deterministic and Nondeterministic Functions](../user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
##  <a name="SchemaBound"></a> Schema-Bound Functions  
 CREATE FUNCTION supports a SCHEMABINDING clause that binds the function to the schema of any objects it references, such as tables, views, and other user-defined functions. An attempt to alter or drop any object referenced by a schema-bound function fails.  
  
 These conditions must be met before you can specify SCHEMABINDING in CREATE FUNCTION:  
  
-   All views and user-defined functions referenced by the function must be schema-bound.  
  
-   All objects referenced by the function must be in the same database as the function. The objects must be referenced using either one-part or two-part names.  
  
-   You must have REFERENCES permission on all objects (tables, views, and user-defined functions) referenced in the function.  
  
 You can use ALTER FUNCTION to remove the schema binding. The ALTER FUNCTION statement should redefine the function without specifying WITH SCHEMABINDING.  
  
##  <a name="Parameters"></a> Specifying Parameters  
 A user-defined function takes zero or more input parameters and returns either a scalar value or a table. A function can have a maximum of 1024 input parameters. When a parameter of the function has a default value, the keyword DEFAULT must be specified when calling the function to get the default value. This behavior is different from parameters with default values in user-defined stored procedures in which omitting the parameter also implies the default value. User-defined functions do not support output parameters.  
  
##  <a name="Tasks"></a> Related Tasks  
  
|||  
|-|-|  
|**Task Description**|**Topic**|  
|Describes how to create a Transact-SQL user-defined function.|[Create User-defined Functions &#40;Database Engine&#41;](../user-defined-functions/create-user-defined-functions-database-engine.md)|  
|Describes how create a CLR function.|[Create CLR Functions](../user-defined-functions/create-clr-functions.md)|  
|Describes how to create a user-defined aggregate function|[Create User-defined Aggregates](../user-defined-functions/create-user-defined-aggregates.md)|  
|Describes how to modify a Transact-SQL user-defined function.|[Modify User-defined Functions](../user-defined-functions/user-defined-functions.md)|  
|Describes how to delete a user-defined function.|[Delete User-defined Functions](../user-defined-functions/delete-user-defined-functions.md)|  
|Describes how to execute a user-defined function.|[Execute User-defined Functions](../user-defined-functions/execute-user-defined-functions.md)|  
|Describes how to rename a user-defined function|[Rename User-defined Functions](../user-defined-functions/rename-user-defined-functions.md)|  
|Describes how to view the definition of a user-defined function.|[View User-defined Functions](view-user-defined-functions.md)|  
  
  
