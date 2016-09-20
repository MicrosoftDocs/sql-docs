---
title: "ERROR_MESSAGE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 02533166-3f7d-4297-8b6c-aef59a8f5505
caps.latest.revision: 7
author: BarbKess
---
# ERROR_MESSAGE (SQL Server PDW)
Returns the message text of the error that caused the CATCH block of a TRY…CATCH construct to be run.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ERROR_MESSAGE ()  
```  
  
## Return Types  
**nvarchar(4000)**  
  
## Return Value  
When called in a CATCH block, returns the complete text of the error message that caused the CATCH block to be run. The text includes the values supplied for any substitutable parameters, such as lengths, object names, or times.  
  
Returns NULL if called outside the scope of a CATCH block.  
  
## Remarks  
ERROR_MESSAGE may be called anywhere within the scope of a CATCH block.  
  
ERROR_MESSAGE returns the error message regardless of how many times it is run, or where it is run within the scope of the CATCH block. This is in contrast to functions like @@ERROR, which only returns an error number in the statement immediately after the one that causes an error, or the first statement of a CATCH block.  
  
In nested CATCH blocks, ERROR_MESSAGE returns the error message specific to the scope of the CATCH block in which it is referenced. For example, the CATCH block of an outer TRY...CATCH construct could have a nested TRY...CATCH construct. Within the nested CATCH block, ERROR_MESSAGE returns the message from the error that invoked the nested CATCH block. If ERROR_MESSAGE is run in the outer CATCH block, it returns the message from the error that invoked that CATCH block.  
  
## Examples  
  
### A. Using ERROR_MESSAGE in a CATCH block  
The following code example shows a `SELECT` statement that generates a divide-by-zero error. The message of the error is returned.  
  
```  
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  
```  
  
### B. Using ERROR_MESSAGE in a CATCH block with other error-handling tools  
The following code example shows a `SELECT` statement that generates a divide-by-zero error. Along with the error message, information that relates to the error is returned.  
  
```  
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT  
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  
```  
  
## See Also  
[TRY...CATCH &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/try-catch-sql-server-pdw.md)  
[@@ERROR &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-sql-server-pdw.md)  
[ERROR_NUMBER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-number-sql-server-pdw.md)  
[ERROR_PROCEDURE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-procedure-sql-server-pdw.md)  
[ERROR_SEVERITY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-severity-sql-server-pdw.md)  
[ERROR_STATE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-state-sql-server-pdw.md)  
[THROW &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/throw-sql-server-pdw.md)  
  
