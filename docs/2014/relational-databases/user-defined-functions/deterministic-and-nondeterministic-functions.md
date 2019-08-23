---
title: "Deterministic and Nondeterministic Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "built-in functions [SQL Server]"
  - "nondeterministic functions"
  - "extended stored procedures [SQL Server], functions that call"
  - "deterministic functions"
  - "user-defined functions [SQL Server], deterministic"
ms.assetid: 2f3ce5f5-c81c-4470-8141-8144d4f218dd
author: rothja
ms.author: jroth
manager: craigg
---
# Deterministic and Nondeterministic Functions
  Deterministic functions always return the same result any time they are called with a specific set of input values and given the same state of the database. Nondeterministic functions may return different results each time they are called with a specific set of input values even if the database state that they access remains the same. For example, the function AVG always returns the same result given the qualifications stated above, but the GETDATE function, which returns the current datetime value, always returns a different result.  
  
 There are several properties of user-defined functions that determine the ability of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to index the results of the function, either through indexes on computed columns that call the function, or through indexed views that reference the function. The determinism of a function is one such property. For example, a clustered index cannot be created on a view if the view references any nondeterministic functions. For more information about the properties of functions, including determinism, see [User-Defined Functions](user-defined-functions.md).  
  
 This topic identifies the determinism of built-in system functions and the effect on the deterministic property of user-defined functions when it contains a call to extended stored procedures.  
  
## Built-in Function Determinism  
 You cannot influence the determinism of any built-in function. Each built-in function is deterministic or nondeterministic based on how the function is implemented by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example, specifying an ORDER BY clause in a query does not change the determinism of a function that used in that query.  
  
 All of the string built-in functions are deterministic. For a list of these functions, see [String Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/string-functions-transact-sql).  
  
 The following built-in functions from categories of built-in functions other than string functions are always deterministic.  
  
||||  
|-|-|-|  
|ABS|DATEDIFF|POWER|  
|ACOS|DAY|RADIANS|  
|ASIN|DEGREES|ROUND|  
|ATAN|EXP|SIGN|  
|ATN2|FLOOR|SIN|  
|CEILING|ISNULL|SQUARE|  
|COALESCE|ISNUMERIC|SQRT|  
|COS|LOG|TAN|  
|COT|LOG10|YEAR|  
|DATALENGTH|MONTH||  
|DATEADD|NULLIF||  
  
 The following functions are not always deterministic, but can be used in indexed views or indexes on computed columns when they are specified in a deterministic manner.  
  
|Function|Comments|  
|--------------|--------------|  
|all aggregate functions|All aggregate functions are deterministic unless they are specified with the OVER and ORDER BY clauses. For a list of these functions, see [Aggregate Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/aggregate-functions-transact-sql).|  
|CAST|Deterministic unless used with `datetime`, `smalldatetime`, or `sql_variant`.|  
|CONVERT|Deterministic unless one of these conditions exists:<br /><br /> Source type is `sql_variant`.<br /><br /> Target type is `sql_variant` and its source type is nondeterministic.<br /><br /> Source or target type is `datetime` or `smalldatetime`, the other source or target type is a character string, and a nondeterministic style is specified. To be deterministic, the style parameter must be a constant. Additionally, styles less than or equal to 100 are nondeterministic, except for styles 20 and 21. Styles greater than 100 are deterministic, except for styles 106, 107, 109 and 113.|  
|CHECKSUM|Deterministic, with the exception of CHECKSUM(*).|  
|ISDATE|Deterministic only if used with the CONVERT function, the CONVERT style parameter is specified and style is not equal to 0, 100, 9, or 109.|  
|RAND|RAND is deterministic only when a *seed* parameter is specified.|  
  
 All the configuration, cursor, metadata, security, and system statistical functions are nondeterministic. For a list of these functions, see [Configuration Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/configuration-functions-transact-sql), [Cursor Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/cursor-functions-transact-sql), [Metadata Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/metadata-functions-transact-sql), [Security Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/security-functions-transact-sql), and [System Statistical Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/system-statistical-functions-transact-sql).  
  
 The following built-in functions from other categories are always nondeterministic.  
  
|||  
|-|-|  
|@@CONNECTIONS|GETDATE|  
|@@CPU_BUSY|GETUTCDATE|  
|@@DBTS|GET_TRANSMISSION_STATUS|  
|@@IDLE|LAG|  
|@@IO_BUSY|LAST_VALUE|  
|@@MAX_CONNECTIONS|LEAD|  
|@@PACK_RECEIVED|MIN_ACTIVE_ROWVERSION|  
|@@PACK_SENT|NEWID|  
|@@PACKET_ERRORS|NEWSEQUENTIALID|  
|@@TIMETICKS|NEXT VALUE FOR|  
|@@TOTAL_ERRORS|NTILE|  
|@@TOTAL_READ|PARSENAME|  
|@@TOTAL_WRITE|PERCENTILE_CONT|  
|CUME_DIST|PERCENTILE_DISC|  
|CURRENT_TIMESTAMP|PERCENT_RANK|  
|DENSE_RANK|RAND|  
|FIRST_VALUE|RANK|  
||ROW_NUMBER|  
||TEXTPTR|  
  
## Calling Extended Stored Procedures from Functions  
 Functions that call extended stored procedures are nondeterministic, because the extended stored procedures can cause side effects on the database. Side effects are changes to a global state of the database, such as an update to a table, or to an external resource, such as a file or the network; for example, modifying a file or sending an e-mail message. You should not rely on returning a consistent result set when executing an extended stored procedure from a user-defined function. User-defined functions that create side effects on the database are not recommended.  
  
 When called from inside a function, the extended stored procedure cannot return result sets to the client. Any Open Data Services API that returns result sets to the client will have a return code of FAIL.  
  
 The extended stored procedure can connect back to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, the procedure cannot join the same transaction as the original function that invoked the extended stored procedure.  
  
 Similar to invocations from a batch or stored procedure, the extended stored procedure is executed in the context of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows security account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The owner of the extended stored procedure should consider this when granting permissions to other users to execute the procedure.  
  
  
