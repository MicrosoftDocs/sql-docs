---
title: ERROR_MESSAGE (Transact-SQL)
description: "ERROR_MESSAGE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ERROR_MESSAGE_TSQL"
  - "ERROR_MESSAGE"
helpviewer_keywords:
  - "ERROR_MESSAGE function"
  - "errors [SQL Server], text of"
  - "messages [SQL Server], text of"
  - "TRY...CATCH [SQL Server]"
  - "CATCH block"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ERROR_MESSAGE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the message text of the error that caused the CATCH block of a TRY...CATCH construct to execute.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
ERROR_MESSAGE ( )   
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **nvarchar(4000)**  
  
## Return Value  
When called in a CATCH block, `ERROR_MESSAGE` returns the complete text of the error message that caused the `CATCH` block to run. The text includes the values supplied for any substitutable parameters - for example, lengths, object names, or times.  
  
`ERROR_MESSAGE` returns NULL when called outside the scope of a CATCH block.  
  
## Remarks  
`ERROR_MESSAGE` supports calls anywhere within the scope of a CATCH block.  
  
`ERROR_MESSAGE` returns a relevant error message regardless of how many times it runs, or where it runs within the scope of the `CATCH` block. This contrasts with a function like @@ERROR, which only returns an error number in the statement immediately following the one that causes an error.  
  
In nested `CATCH` blocks, `ERROR_MESSAGE` returns the error message specific to the scope of the `CATCH` block that referenced that `CATCH` block. For example, the `CATCH` block of an outer TRY...CATCH construct could have an inner `TRY...CATCH` construct. Inside that inner `CATCH` block, `ERROR_MESSAGE` returns the message from the error that invoked the inner `CATCH` block. If `ERROR_MESSAGE` runs in the outer `CATCH` block, it returns the message from the error that invoked that outer `CATCH` block.  
  
## Examples  
  
### A. Using ERROR_MESSAGE in a CATCH block  
This example shows a `SELECT` statement that generates a divide-by-zero error. The `CATCH` block returns the error message.  
  
```sql   
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)] 
```
-----------

(0 row(s) affected)

ErrorMessage
----------------------------------
Divide by zero error encountered.

(1 row(s) affected)

```  
  
### B. Using ERROR_MESSAGE in a CATCH block with other error-handling tools  
This example shows a `SELECT` statement that generates a divide-by-zero error. Along with the error message, the `CATCH` block returns information about that error.  
  
```sql  
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
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;  
GO  
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)] 
```
-----------

(0 row(s) affected)

ErrorNumber ErrorSeverity ErrorState  ErrorProcedure  ErrorLine  ErrorMessage
----------- ------------- ----------- --------------- ---------- ----------------------------------
8134        16            1           NULL            4          Divide by zero error encountered.

(1 row(s) affected)

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
 [Errors and Events Reference &#40;Database Engine&#41;](../../relational-databases/errors-events/errors-and-events-reference-database-engine.md)     
  
    

