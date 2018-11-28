---
title: "ERROR_LINE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# ERROR_LINE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the line number of occurrence of an error that caused the CATCH block of a TRY...CATCH construct to execute.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ERROR_LINE ( )  
```  
  
## Return Type  
**int**  
  
## Return Value  
When called in a CATCH block, `ERROR_LINE` returns  
  
-   the line number where the error occurred    
-   the line number in a routine, if the error occurred within a stored procedure or trigger  
-   NULL, if called outside the scope of a CATCH block.  
  
## Remarks  
A call to `ERROR_LINE` can happen anywhere within the scope of a CATCH block.  
  
`ERROR_LINE` returns the line number at which the error occurred. This happens regardless of the location of the `ERROR_LINE` call within the scope of the CATCH block, and regardless of the number of calls to `ERROR_LINE`. This contrasts with functions, such as @@ERROR. @@ERROR returns an error number in the statement immediately following the one that causes an error, or in the first statement of a CATCH block.  
  
In nested CATCH blocks, `ERROR_LINE` returns the error line number specific to the scope of the CATCH block in which it is referenced. For example, the CATCH block of a TRY...CATCH construct could contain a nested TRY...CATCH construct. Within the nested CATCH block, `ERROR_LINE` returns the line number for the error that invoked the nested CATCH block. If `ERROR_LINE` runs in the outer CATCH block, it returns the line number for the error that invoked that specific CATCH block.  
  
## Examples  
  
### A. Using ERROR_LINE in a CATCH block  
This code example shows a `SELECT` statement that generates a divide-by-zero error. `ERROR_LINE` returns the line number where the error occurred.  
  
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
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
   
```  
Result 
-----------

(0 row(s) affected)

ErrorLine
-----------
4

(1 row(s) affected)
```  
  
### B. Using ERROR_LINE in a CATCH block with a stored procedure  
This example shows a stored procedure that generates a divide-by-zero error. `ERROR_LINE` returns the line number where the error occurred.  
  
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
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
   
```  
-----------

(0 row(s) affected)

ErrorLine
-----------
7

(1 row(s) affected)  
   
```

### C. Using ERROR_LINE in a CATCH block with other error-handling tools  
This code example shows a `SELECT` statement that generates a divide-by-zero error. `ERROR_LINE` returns the line number where the error occurred, and information relating to the error itself.  
  
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
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
   
```  
-----------

(0 row(s) affected)

ErrorNumber ErrorSeverity ErrorState ErrorProcedure ErrorLine ErrorMessage
----------- ------------- ---------- -------------- --------- ---------------------------------
8134        16            1          NULL           3         Divide by zero error encountered.

(1 row(s) affected)
  
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
  
  
