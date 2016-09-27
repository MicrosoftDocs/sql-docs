---
title: "PRINT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e3bb5a71-26b8-4018-a4d2-5e88f836c01c
caps.latest.revision: 7
author: BarbKess
---
# PRINT (SQL Server PDW)
Returns a user-defined message to the client.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
PRINT msg_str | @local_variable | string_expr  
```  
  
## Arguments  
*msg_str*  
Is a character string or Unicode string constant.  
  
**@***local_variable*  
Is a variable of any valid character data type. **@***local_variable* must be **char**, **nchar**, **varchar**, or **nvarchar**, or it must be able to be implicitly converted to those data types.  
  
*string_expr*  
Is an expression that returns a string. Can include concatenated literal values, functions, and variables.  
  
## Remarks  
A message string can be up to 8,000 characters long if it is a non-Unicode string, and 4,000 characters long if it is a Unicode string. Longer strings are truncated.  
  
RAISERROR can also be used to return messages. The principal difference between the PRINT and RAISERROR is that PRINT supports expressions as the parameter and RAISERROR only supports literals.  
  
## Examples  
  
### A. Conditionally executing print  
The following example uses the `PRINT` statement to conditionally return a message.  
  
```  
IF DB_ID() = 1  
    PRINT N'The current database is master.';  
ELSE  
    PRINT N'The current database is not master.';  
GO  
```  
  
### B. Building and displaying a string  
The following example converts the results of the `GETDATE` function to a `nvarchar` data type and concatenates it with literal text to be returned by `PRINT`.  
  
```  
-- Build the message text by concatenating  
-- strings and expressions.  
PRINT N'This message was printed on '  
    + RTRIM(CAST(GETDATE() AS nvarchar(30)))  
    + N'.';  
GO  
-- This example shows building the message text  
-- in a variable and then passing it to PRINT.  
DECLARE @PrintMessage nvarchar(50);  
SET @PrintMessage = N'This message was printed on '  
    + RTRIM(CAST(GETDATE() AS nvarchar(30)))  
    + N'.';  
PRINT @PrintMessage;  
GO  
```  
  
## See Also  
[RAISERROR &#40;SQL Server PDW&#41;](../sqlpdw/raiserror-sql-server-pdw.md)  
[TRY...CATCH &#40;SQL Server PDW&#41;](../sqlpdw/try-catch-sql-server-pdw.md)  
[ERROR_MESSAGE &#40;SQL Server PDW&#41;](../sqlpdw/error-message-sql-server-pdw.md)  
[ERROR_NUMBER &#40;SQL Server PDW&#41;](../sqlpdw/error-number-sql-server-pdw.md)  
[ERROR_PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/error-procedure-sql-server-pdw.md)  
[ERROR_SEVERITY &#40;SQL Server PDW&#41;](../sqlpdw/error-severity-sql-server-pdw.md)  
[ERROR_STATE &#40;SQL Server PDW&#41;](../sqlpdw/error-state-sql-server-pdw.md)  
[THROW &#40;SQL Server PDW&#41;](../sqlpdw/throw-sql-server-pdw.md)  
  
