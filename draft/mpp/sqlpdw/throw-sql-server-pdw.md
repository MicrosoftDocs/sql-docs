---
title: "THROW (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d9f3d29e-a663-418d-beba-9a997a41f712
caps.latest.revision: 6
author: BarbKess
---
# THROW (SQL Server PDW)
Raises user specified errors with a fixed severity of 16 and transfers execution to a CATCH block of a TRY…CATCH construct in Analytics Platform System. It is similar to RAISERROR statement. When used without additional arguments, THROW can be used to re-throw the same error within a CATCH block.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
THROW [ { error_number | @local_variable },  
        { message | @local_variable },  
        { state | @local_variable } ]   
[ ; ]  
```  
  
## Arguments  
*error_number*  
Is a constant or variable that represents the exception. *error_number* is **int** and must be between 50000 and 100000 or between 150000 and 2147483647.  
  
*message*  
Is an string or variable that describes the exception. *message* is **nvarchar(2048)**.  
  
*state*  
Is a constant or variable between 0 and 255 that indicates the state to associate with the message. *state* is **tinyint**.  
  
## Remarks  
The THROW statement must be executed as the first statement in the batch, or the statement before the THROW statement must be followed by the semicolon (;) statement terminator.  
  
If a TRY…CATCH construct is not available, the session is ended. The procedure where the exception is raised is set. The severity is set to 16.  
  
If the THROW statement is specified without parameters, it must appear inside a CATCH block. This causes the caught exception to be raised. Any error that occurs in a THROW statement causes the statement batch to be ended.  
  
THROW in PDW differs from SQL Server in two ways:  
  
-   Error numbers which fall in the PDW reserved range of 100,000 to 150,000 (inclusive) is not be supported.  
  
-   SQL Server reserved error number range (0 – 50,000, inclusive) is not be supported, by default.  
  
## Differences Between RAISERROR and THROW  
The following table lists differences between the RAISERROR and THROW statements.  
  
|RAISERROR statement|THROW statement|  
|-----------------------|-------------------|  
|The *msg_str* parameter can contain **printf** formatting styles.|The *message* parameter does not accept **printf** style formatting.|  
|The *severity* parameter specifies the severity of the exception.|There is no *severity* parameter. The exception severity is always set to 16.|  
  
## Examples  
  
### A. Using THROW to raise an exception  
The following example shows how to use the `THROW` statement to raise an exception.  
  
```Transact-SQL  
THROW 51000, 'The record does not exist.', 1;  
```  
  
Here is the result set.  
  
<pre>Msg 51000, Level 16, State 1, Line 1  
The record does not exist.</pre>  
  
## See Also  
[XACT_STATE &#40;SQL Server PDW&#41;](../sqlpdw/xact-state-sql-server-pdw.md)  
[@@ERROR &#40;SQL Server PDW&#41;](../sqlpdw/error-sql-server-pdw.md)  
[TRY...CATCH &#40;SQL Server PDW&#41;](../sqlpdw/try-catch-sql-server-pdw.md)  
[ERROR_MESSAGE &#40;SQL Server PDW&#41;](../sqlpdw/error-message-sql-server-pdw.md)  
[ERROR_NUMBER &#40;SQL Server PDW&#41;](../sqlpdw/error-number-sql-server-pdw.md)  
[ERROR_PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/error-procedure-sql-server-pdw.md)  
[ERROR_SEVERITY &#40;SQL Server PDW&#41;](../sqlpdw/error-severity-sql-server-pdw.md)  
[ERROR_STATE &#40;SQL Server PDW&#41;](../sqlpdw/error-state-sql-server-pdw.md)  
[RAISERROR &#40;SQL Server PDW&#41;](../sqlpdw/raiserror-sql-server-pdw.md)  
  
