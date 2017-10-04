---
title: "ERROR_PROCEDURE (Transact-SQL) | Microsoft Docs"
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
  - "ERROR_PROCEDURE_TSQL"
  - "ERROR_PROCEDURE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ERROR_PROCEDURE function"
  - "messages [SQL Server], trigger where occurred"
  - "errors [SQL Server], stored procedure where occurred"
  - "TRY...CATCH [SQL Server]"
  - "CATCH block"
  - "messages [SQL Server], stored procedure where occurred"
  - "errors [SQL Server], trigger where occurred"
ms.assetid: b81edbf0-856a-498f-ba87-48ff1426d980
caps.latest.revision: 44
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ERROR_PROCEDURE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the name of the stored procedure or trigger where an error occurred that caused the CATCH block of a TRY…CATCH construct to be run.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ERROR_PROCEDURE ( )  
```  
  
## Return Types  
 **nvarchar(128)**  
  
## Return Value  
 When called in a CATCH block, returns the stored procedure name where the error occurred.  
  
 Returns NULL if the error did not occur within a stored procedure or trigger.  
  
 Returns NULL if called outside the scope of a CATCH block.  
  
## Remarks  
 ERROR_PROCEDURE may be called anywhere within the scope of a CATCH block.  
  
 ERROR_PROCEDURE returns the name of the stored procedure or trigger where the error occurred, regardless of the number of times it is called or where it is called within the scope of the CATCH block. This contrasts with functions, such as @@ERROR, which return the error number in the statement immediately following the one that caused the error or in the first statement of the CATCH block.  
  
 In nested CATCH blocks, ERROR_PROCEDURE returns the name of the stored procedure or trigger specific to the scope of the CATCH block in which it is referenced. For example, the CATCH block of a TRY…CATCH construct could have a nested TRY…CATCH. Within the nested CATCH block, ERROR_PROCEDURE returns the name of the stored procedure or trigger where the error occurred that invoked the nested CATCH block. If ERROR_PROCEDURE is run in the outer CATCH block, it returns the name of the stored procedure or trigger where the error occurred that invoked that CATCH block.  
  
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
        ERROR_MESSAGE() AS ErrorMessage,  
        ERROR_LINE() AS ErrorLine;  
        END CATCH;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Using ERROR_PROCEDURE in a CATCH block  
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
  
### D. Using ERROR_PROCEDURE in a CATCH block with other error-handling tools  
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
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)   
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)   
 [ERROR_LINE &#40;Transact-SQL&#41;](../../t-sql/functions/error-line-transact-sql.md)   
 [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)   
 [ERROR_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/error-number-transact-sql.md)   
 [ERROR_SEVERITY &#40;Transact-SQL&#41;](../../t-sql/functions/error-severity-transact-sql.md)   
 [ERROR_STATE &#40;Transact-SQL&#41;](../../t-sql/functions/error-state-transact-sql.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)  
  
  

