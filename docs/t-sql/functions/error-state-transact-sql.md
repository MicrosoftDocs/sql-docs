---
title: "ERROR_STATE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw, sql-database"
ms.component: "t-sql|functions"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: t-sql
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ERROR_STATE_TSQL"
  - "ERROR_STATE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "messages [SQL Server], state"
  - "ERROR_STATE function"
  - "errors [SQL Server], state"
  - "TRY...CATCH [SQL Server]"
  - "CATCH block"
  - "states [SQL Server], error numbers"
ms.assetid: 6059af00-83fe-409f-ab7c-daad111bc671
caps.latest.revision: 39
author: edmacauley
ms.author: edmaca
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || >= sql-server-2016 || = sqlallproducts-allversions"
---
# ERROR_STATE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

This function returns the state number of the error where an error occurs, if that error caused the `CATCH` block of a TRY…CATCH construct to execute.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ERROR_STATE ( )  
```  
  
## Return Types  
 **int**  
  
## Return Value  
When called in a `CATCH` block where an error occurs, `ERROR_STATE` returns the state number of the error message that caused the `CATCH` block to run.  
  
`ERROR_STATE` returns NULL if called outside the scope of a `CATCH` block.  
  
## Remarks  
[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] supports the raising of some error messages at multiple points in the code. For example, code can raise an "1105" error for several different conditions. Each specific condition that raises the error assigns a unique state code.  
  
When viewing databases of known issues, for example the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base, you can use the state number to determine if the detected issue might match the relevant encountered error. For example, if a Knowledge Base article describes an 1105 error message with a state of 2, and a detected 1105 error message has a state of 3, the error probably had a cause different from the one reported in the article.  
  
A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support engineer can also use the state code of an error to find the source code location which raised that error, which may provide additional problem diagnostic help.  
  
`ERROR_STATE` supports calls anywhere within the scope of a `CATCH` block.  
  
`ERROR_STATE` returns the error state value of an error, regardless of how many times it runs or where it runs within the scope of the `CATCH` block. This contrasts with a function like @@ERROR, which only returns an error number in the statement immediately following the one that causes an error.  

In a nested `CATCH` block, `ERROR_STATE` returns the error number specific to the scope of the `CATCH` block that referenced that `CATCH` block. For example, the `CATCH` block of an outer TRY...CATCH construct could have an inner `TRY...CATCH` construct. Inside that inner `CATCH` block, `ERROR_STATE` returns the number of the error that invoked the inner `CATCH` block. If `ERROR_STATE` runs in the outer `CATCH` block, it returns the number of the error that invoked that outer `CATCH` block.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Using ERROR_STATE in a CATCH block  
This example shows a `SELECT` statement that generates a divide-by-zero error. `ERROR_STATE` returns the state of the error.  
  
```  
BEGIN TRY  
    -- Generate a divide by zero error  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_STATE() AS ErrorState;  
END CATCH;  
GO  

-----------

(0 row(s) affected)

ErrorState
-----------
1

(1 row(s) affected)

```  
  
### B. Using ERROR_STATE in a CATCH block with other error-handling tools  
This example shows a `SELECT` statement that generates a divide-by-zero error. The example shows information about the error.  
  
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

-----------

(0 row(s) affected)

ErrorNumber  ErrorSeverity  ErrorState  ErrorProcedure  ErrorLine  ErrorMessage
------------ -------------- ----------- --------------- ---------- ----------------------------------
8134         16             1           NULL            3          Divide by zero error encountered.

(1 row(s) affected)

```  
  
## See Also  
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)   
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)   
 [ERROR_LINE &#40;Transact-SQL&#41;](../../t-sql/functions/error-line-transact-sql.md)   
 [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)   
 [ERROR_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/error-number-transact-sql.md)   
 [ERROR_PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/functions/error-procedure-transact-sql.md)   
 [ERROR_SEVERITY &#40;Transact-SQL&#41;](../../t-sql/functions/error-severity-transact-sql.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)  
  
  

