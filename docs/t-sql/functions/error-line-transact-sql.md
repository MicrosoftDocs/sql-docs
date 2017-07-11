---
title: "ERROR_LINE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ERROR_LINE"
  - "ERROR_LINE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "errors [SQL Server], line number"
  - "messages [SQL Server], line number"
  - "TRY...CATCH [SQL Server]"
  - "line number of error [SQL Server]"
  - "ERROR_LINE function"
  - "CATCH block"
ms.assetid: 47335734-0baf-45a6-8b3b-6c4fd80d2cb8
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ERROR_LINE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the line number at which an error occurred that caused the CATCH block of a TRY…CATCH construct to be run.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ERROR_LINE ( )  
```  
  
## Return Type  
 **int**  
  
## Return Value  
 When called in a CATCH block:  
  
-   Returns the line number at which the error occurred.  
  
-   Returns the line number in a routine if the error occurred within a stored procedure or trigger.  
  
 Returns NULL if called outside the scope of a CATCH block.  
  
## Remarks  
 This function may be called anywhere within the scope of a CATCH block.  
  
 ERROR_LINE returns the line number at which the error occurred regardless of the number of times it is called or where it is called within the scope of the CATCH block. This contrasts with functions, such as @@ERROR, which return an error number in the statement immediately following the one that causes an error or in the first statement of a CATCH block.  
  
 In nested CATCH blocks, ERROR_LINE returns the error line number specific to the scope of the CATCH block in which it is referenced. For example, the CATCH block of a TRY…CATCH construct could contain a nested TRY…CATCH construct. Within the nested CATCH block, ERROR_LINE returns the line number for the error that invoked the nested CATCH block. If ERROR_LINE is run in the outer CATCH block, it returns the line number for the error that invoked that CATCH block.  
  
## Examples  
  
### A. Using ERROR_LINE in a CATCH block  
 The following code example shows a `SELECT` statement that generates a divide-by-zero error. The line number at which the error occurred is returned.  
  
```  
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_LINE() AS ErrorLine;  
END CATCH;  
GO  
```  
  
### B. Using ERROR_LINE in a CATCH block with a stored procedure  
 The following code example shows a stored procedure that will generate a divide-by-zero error. `ERROR_LINE` returns the line number in the stored procedure in which the error occurred.  
  
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
    SELECT ERROR_LINE() AS ErrorLine;  
END CATCH;  
GO  
```  
  
### C. Using ERROR_LINE in a CATCH block with other error-handling tools  
 The following code example shows a `SELECT` statement that generates a divide-by-zero error. Along with the line number at which the error occurred, information that relates to the error is returned.  
  
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
        ERROR_LINE() AS ErrorLine,  
        ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  
```  
  
## See Also  
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)   
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)   
 [ERROR_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/error-number-transact-sql.md)   
 [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)   
 [ERROR_PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/functions/error-procedure-transact-sql.md)   
 [ERROR_SEVERITY &#40;Transact-SQL&#41;](../../t-sql/functions/error-severity-transact-sql.md)   
 [ERROR_STATE &#40;Transact-SQL&#41;](../../t-sql/functions/error-state-transact-sql.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)  
  
  