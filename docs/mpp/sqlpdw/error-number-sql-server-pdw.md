---
title: "ERROR_NUMBER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 18200c9c-41b1-4790-b833-428554b5d693
caps.latest.revision: 7
author: BarbKess
---
# ERROR_NUMBER (SQL Server PDW)
Returns the error number of the error that caused the CATCH block of a TRY…CATCH construct to be run.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ERROR_NUMBER ( )  
```  
  
## Return Types  
**int**  
  
## Return Value  
When called in a CATCH block, returns the error number of the error message that caused the CATCH block to be run.  
  
Returns NULL if called outside the scope of a CATCH block.  
  
## Remarks  
This function may be called anywhere within the scope of a CATCH block.  
  
ERROR_NUMBER returns the error number regardless of how many times it is run, or where it is run within the scope of the CATCH block. This is in contrast to @@ERROR, which only returns the error number in the statement immediately after the one that causes an error, or the first statement of a CATCH block.  
  
In nested CATCH blocks, ERROR_NUMBER returns the error number specific to the scope of the CATCH block in which it is referenced. For example, the CATCH block of an outer TRY...CATCH construct could have a nested TRY...CATCH construct. Within the nested CATCH block, ERROR_NUMBER returns the number from the error that invoked the nested CATCH block. If ERROR_NUMBER is run in the outer CATCH block, it returns the number from the error that invoked that CATCH block.  
  
## Examples  
  
### A. Using ERROR_NUMBER in a CATCH block  
The following code example shows a `SELECT` statement that generates a divide-by-zero error. The number of the error is returned.  
  
```  
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_NUMBER() AS ErrorNumber;  
END CATCH;  
GO  
```  
  
### B. Using ERROR_NUMBER in a CATCH block with other error-handling tools  
The following code example shows a `SELECT` statement that generates a divide-by-zero error. Along with the error number, information that relates to the error is returned.  
  
```  
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT  
        ERROR_NUMBER() AS ErrorNumber,  
        ERROR_SEVERITY() AS ErrorSeverity,  
        ERROR_STATE() AS ErrorState,  
        ERROR_PROCEDURE() AS ErrorProcedure,  
        ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  
```  
  
## See Also  
[TRY...CATCH &#40;SQL Server PDW&#41;](../sqlpdw/try-catch-sql-server-pdw.md)  
[@@ERROR &#40;SQL Server PDW&#41;](../sqlpdw/error-sql-server-pdw.md)  
[ERROR_MESSAGE &#40;SQL Server PDW&#41;](../sqlpdw/error-message-sql-server-pdw.md)  
[ERROR_PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/error-procedure-sql-server-pdw.md)  
[ERROR_SEVERITY &#40;SQL Server PDW&#41;](../sqlpdw/error-severity-sql-server-pdw.md)  
[ERROR_STATE &#40;SQL Server PDW&#41;](../sqlpdw/error-state-sql-server-pdw.md)  
[THROW &#40;SQL Server PDW&#41;](../sqlpdw/throw-sql-server-pdw.md)  
  
