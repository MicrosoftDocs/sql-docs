---
title: "ERROR_PROCEDURE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a6d24a92-10d6-4d05-a1a0-779e1a223bdc
caps.latest.revision: 7
author: BarbKess
---
# ERROR_PROCEDURE (SQL Server PDW)
Returns the name of the stored procedure where an error occurred that caused the CATCH block of a TRY…CATCH construct to be run.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ERROR_PROCEDURE ()  
```  
  
## Return Types  
**nvarchar(128)**  
  
## Return Value  
When called in a CATCH block, returns the stored procedure name where the error occurred.  
  
Returns NULL if the error did not occur within a stored procedure.  
  
Returns NULL if called outside the scope of a CATCH block.  
  
## Remarks  
ERROR_PROCEDURE may be called anywhere within the scope of a CATCH block.  
  
ERROR_PROCEDURE returns the name of the stored procedure where the error occurred, regardless of the number of times it is called or where it is called within the scope of the CATCH block. This contrasts with functions, such as @@ERROR, which return the error number in the statement immediately following the one that caused the error or in the first statement of the CATCH block.  
  
In nested CATCH blocks, ERROR_PROCEDURE returns the name of the stored procedure specific to the scope of the CATCH block in which it is referenced. For example, the CATCH block of a TRY…CATCH construct could have a nested TRY…CATCH. Within the nested CATCH block, ERROR_PROCEDURE returns the name of the stored procedure where the error occurred that invoked the nested CATCH block. If ERROR_PROCEDURE is run in the outer CATCH block, it returns the name of the stored procedure where the error occurred that invoked that CATCH block.  
  
## Examples  
  
### A. Using ERROR_PROCEDURE in a CATCH block  
The following code example shows a stored procedure that generates a divide-by-zero error. `ERROR_PROCEDURE` returns the name of the stored procedure in which the error occurred.  
  
```  
-- Verify that the stored procedure does not already exist.  
IF OBJECT_ID ( 'usp_ExampleProc', 'P' ) IS NOT NULL   
    DROP PROCEDURE usp_ExampleProc;  
GO  
  
-- Create a stored procedure that   
-- generates a divide-by-zero error.  
CREATE PROCEDURE usp_ExampleProc  
AS  
    SELECT 1/0;  
GO  
  
BEGIN TRY  
    -- Execute the stored procedure inside the TRY block.  
    EXECUTE usp_ExampleProc;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_PROCEDURE() AS ErrorProcedure;  
END CATCH;  
GO  
```  
  
### B. Using ERROR_PROCEDURE in a CATCH block with other error-handling tools  
The following code example shows a stored procedure that generates a divide-by-zero error. Along with the name of the stored procedure in which the error occurred, information that relates to the error is returned.  
  
```  
-- Verify that the stored procedure does not already exist.  
IF OBJECT_ID ( 'usp_ExampleProc', 'P' ) IS NOT NULL   
    DROP PROCEDURE usp_ExampleProc;  
GO  
  
-- Create a stored procedure that   
-- generates a divide-by-zero error.  
CREATE PROCEDURE usp_ExampleProc  
AS  
    SELECT 1/0;  
GO  
  
BEGIN TRY  
    -- Execute the stored procedure inside the TRY block.  
    EXECUTE usp_ExampleProc;  
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
[ERROR_SEVERITY &#40;SQL Server PDW&#41;](../sqlpdw/error-severity-sql-server-pdw.md)  
[ERROR_STATE &#40;SQL Server PDW&#41;](../sqlpdw/error-state-sql-server-pdw.md)  
[THROW &#40;SQL Server PDW&#41;](../sqlpdw/throw-sql-server-pdw.md)  
  
