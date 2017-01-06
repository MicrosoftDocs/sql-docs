---
title: "ERROR_STATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 36c5484d-bf8d-4eba-a91c-ccd31d205173
caps.latest.revision: 7
author: BarbKess
---
# ERROR_STATE (SQL Server PDW)
Returns the state number of the error that caused the CATCH block of a TRY…CATCH construct to be run.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ERROR_STATE ()  
```  
  
## Return Types  
**int**  
  
## Return Value  
When called in a CATCH block, returns the state number of the error message that caused the CATCH block to be run.  
  
Returns NULL if called outside the scope of a CATCH block.  
  
## Remarks  
Some error messages can be raised at multiple points in the code. For example, an "1105" error can be raised for several different conditions. Each specific condition that raises the error assigns a unique state code.  
  
When viewing databases of known issues, such as the Microsoft Knowledge Base, you can use the state number to determine if the recorded issue might be the same as the error you have encountered. For example, if a Knowledge Base article discusses an 1105 error message with a state of 2, and the 1105 error message you received had a state of 3, your error probably had a different cause than the one reported in the article.  
  
A support engineer can also use the state code from an error to find the location in the source code where that error is being raised, which may provide additional ideas on how to diagnose the problem.  
  
ERROR_STATE may be called anywhere within the scope of a CATCH block.  
  
ERROR_STATE returns the error state regardless of how many times it is run, or where it is run within the scope of the CATCH block. This is in contrast to functions like @@ERROR, which only returns the error number in the statement immediately after the one that causes an error, or in the first statement of a CATCH block.  
  
In nested CATCH blocks, ERROR_STATE returns the error state specific to the scope of the CATCH block in which it is referenced. For example, the CATCH block of an outer TRY...CATCH construct could have a nested TRY...CATCH construct. Within the nested CATCH block, ERROR_STATE returns the state from the error that invoked the nested CATCH block. If ERROR_STATE is run in the outer CATCH block, it returns the state from the error that invoked that CATCH block.  
  
## Examples  
  
### A. Using ERROR_STATE in a CATCH block  
The following example shows a `SELECT` statement that generates a divide-by-zero error. The state of the error is returned.  
  
```  
BEGIN TRY  
    -- Generate a divide by zero error  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_STATE() AS ErrorState;  
END CATCH;  
GO  
```  
  
### B. Using ERROR_STATE in a CATCH block with other error-handling tools  
The following example shows a `SELECT` statement that generates a divide-by-zero error. Along with the error state, information that relates to the error is returned.  
  
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
[ERROR_NUMBER &#40;SQL Server PDW&#41;](../sqlpdw/error-number-sql-server-pdw.md)  
[ERROR_PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/error-procedure-sql-server-pdw.md)  
[ERROR_SEVERITY &#40;SQL Server PDW&#41;](../sqlpdw/error-severity-sql-server-pdw.md)  
[THROW &#40;SQL Server PDW&#41;](../sqlpdw/throw-sql-server-pdw.md)  
  
