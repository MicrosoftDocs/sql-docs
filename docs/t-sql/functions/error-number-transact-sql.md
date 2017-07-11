---
title: "ERROR_NUMBER (Transact-SQL) | Microsoft Docs"
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
  - "ERROR_NUMBER_TSQL"
  - "ERROR_NUMBER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "errors [SQL Server], line number"
  - "messages [SQL Server], numbers"
  - "TRY...CATCH [SQL Server]"
  - "ERROR_NUMBER function"
  - "CATCH block"
ms.assetid: 1de85fff-1ca2-4b31-841b-926e571cb150
caps.latest.revision: 50
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ERROR_NUMBER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the error number of the error that caused the CATCH block of a TRY…CATCH construct to be run.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
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
        ERROR_LINE() AS ErrorLine,  
        ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Using ERROR_NUMBER in a CATCH block  
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
  
### D. Using ERROR_NUMBER in a CATCH block with other error-handling tools  
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
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)   
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)   
 [ERROR_LINE &#40;Transact-SQL&#41;](../../t-sql/functions/error-line-transact-sql.md)   
 [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)   
 [ERROR_PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/functions/error-procedure-transact-sql.md)   
 [ERROR_SEVERITY &#40;Transact-SQL&#41;](../../t-sql/functions/error-severity-transact-sql.md)   
 [ERROR_STATE &#40;Transact-SQL&#41;](../../t-sql/functions/error-state-transact-sql.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)  
  
  

