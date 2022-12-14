---
title: "ERROR_SEVERITY (Transact-SQL)"
description: "ERROR_SEVERITY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ERROR_SEVERITY_TSQL"
  - "ERROR_SEVERITY"
helpviewer_keywords:
  - "errors [SQL Server], severity"
  - "messages [SQL Server], severity"
  - "TRY...CATCH [SQL Server]"
  - "CATCH block"
  - "ERROR_SEVERITY function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ERROR_SEVERITY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the severity value of the error where an error occurs, if that error caused the CATCH block of a TRY...CATCH construct to execute.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
ERROR_SEVERITY ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **int**  
  
## Return Value  
When called in a CATCH block where an error occurs, `ERROR_SEVERITY` returns the severity value of the error that caused the `CATCH` block to run.  

`ERROR_SEVERITY` returns NULL if called outside the scope of a CATCH block.  
  
## Remarks  
`ERROR_SEVERITY` supports calls anywhere within the scope of a CATCH block..  
  
`ERROR_SEVERITY` returns the error severity value of an error, regardless of how many times it runs or where it runs within the scope of the `CATCH` block. This contrasts with a function like @@ERROR, which only returns an error number in the statement immediately following the one that causes an error.  
  
`ERROR_SEVERITY` typically operates in a nested `CATCH` block. `ERROR_SEVERITY` returns the error severity value specific to the scope of the `CATCH` block that referenced that `CATCH` block. For example, the `CATCH` block of an outer TRY...CATCH construct could have an inner `TRY...CATCH` construct. Inside that inner `CATCH` block, `ERROR_SEVERITY` returns the severity value of the error that invoked the inner `CATCH` block. If `ERROR_SEVERITY` runs in the outer `CATCH` block, it returns the error severity value of the error that invoked that outer `CATCH` block.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Using ERROR_SEVERITY in a CATCH block  
This example shows a stored procedure that generates a divide-by-zero error. `ERROR_SEVERITY` returns the severity value of that error.  

```sql  
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_SEVERITY() AS ErrorSeverity;  
END CATCH;  
GO  
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
```
-----------

(0 row(s) affected)

ErrorSeverity
-------------
16

(1 row(s) affected)

```  
  
### B. Using ERROR_SEVERITY in a CATCH block with other error-handling tools  
This example shows a `SELECT` statement that generates a divide by zero error. The stored procedure returns information about the error.  

```sql  
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

ErrorNumber ErrorSeverity ErrorState  ErrorProcedure  ErrorLine   ErrorMessage
----------- ------------- ----------- --------------- ----------- ----------------------------------
8134        16            1           NULL            4           Divide by zero error encountered.

(1 row(s) affected)

```  
  
## See Also  
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)   
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)   
 [ERROR_LINE &#40;Transact-SQL&#41;](../../t-sql/functions/error-line-transact-sql.md)   
 [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)   
 [ERROR_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/error-number-transact-sql.md)   
 [ERROR_PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/functions/error-procedure-transact-sql.md)   
 [ERROR_STATE &#40;Transact-SQL&#41;](../../t-sql/functions/error-state-transact-sql.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)  
 [Errors and Events Reference &#40;Database Engine&#41;](../../relational-databases/errors-events/errors-and-events-reference-database-engine.md)     
  
    

