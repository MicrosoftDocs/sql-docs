---
title: "ERROR_PROCEDURE (Transact-SQL)"
description: "ERROR_PROCEDURE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ERROR_PROCEDURE_TSQL"
  - "ERROR_PROCEDURE"
helpviewer_keywords:
  - "ERROR_PROCEDURE function"
  - "messages [SQL Server], trigger where occurred"
  - "errors [SQL Server], stored procedure where occurred"
  - "TRY...CATCH [SQL Server]"
  - "CATCH block"
  - "messages [SQL Server], stored procedure where occurred"
  - "errors [SQL Server], trigger where occurred"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ERROR_PROCEDURE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]  

This function returns the name of the stored procedure or trigger where an error occurs, if that error caused the CATCH block of a TRY...CATCH construct to execute. 
- SQL Server 2017 thru [current version](../../sql-server/what-s-new-in-sql-server-2019.md) returns schema_name.stored_procedure_name
- SQL Server 2016 returns stored_procedure_name

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
ERROR_PROCEDURE ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
**nvarchar(128)**  
  
## Return Value  
When called in a CATCH block, `ERROR_PROCEDURE` returns the name of the stored procedure or trigger in which the error originated.
  
`ERROR_PROCEDURE` returns NULL if the error did not occur within a stored procedure or trigger.  
  
`ERROR_PROCEDURE` returns NULL when called outside the scope of a CATCH block.  
  
## Remarks  
`ERROR_PROCEDURE` supports calls anywhere within the scope of a CATCH block.  
  
`ERROR_PROCEDURE` returns the name of the stored procedure or trigger where an error occurs, regardless of how many times it runs, or where it runs, within the scope of the `CATCH` block. This contrasts with a function like @@ERROR, which only returns an error number in the statement immediately following the one that causes an error.  
   
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]
  
### A. Using ERROR_PROCEDURE in a CATCH block  
This example shows a stored procedure that generates a divide-by-zero error. `ERROR_PROCEDURE` returns the name of the stored procedure where the error occurred.  
  
```sql  
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

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
```  
-----------

(0 row(s) affected)

ErrorProcedure
--------------------
usp_ExampleProc

(1 row(s) affected)
```  

  
### B. Using ERROR_PROCEDURE in a CATCH block with other error-handling tools  
This example shows a stored procedure that generates a divide-by-zero error. Along with the name of the stored procedure where the error occurred, the stored procedure returns information about the error.  
  
```sql
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

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
``` 
-----------

(0 row(s) affected)

ErrorNumber ErrorSeverity ErrorState  ErrorProcedure   ErrorMessage                       ErrorLine
----------- ------------- ----------- ---------------- ---------------------------------- -----------
8134        16            1           usp_ExampleProc  Divide by zero error encountered.  6

(1 row(s) affected)

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
  
