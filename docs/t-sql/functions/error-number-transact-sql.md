---
title: "ERROR_NUMBER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ERROR_NUMBER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns the error number of the error that caused the CATCH block of a TRY...CATCH construct to execute.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ERROR_NUMBER ( )  
```  
  
## Return Types  
 **int**  
  
## Return Value  
When called in a CATCH block, `ERROR_NUMBER` returns the error number of the error that caused the CATCH block to run.  

`ERROR_NUMBER` returns NULL when called outside the scope of a CATCH block.  
  
## Remarks  
`ERROR_NUMBER` supports calls anywhere within the scope of a CATCH block.  
  
`ERROR_NUMBER` returns a relevant error number regardless of how many times it runs, or where it runs within the scope of the `CATCH` block. This contrasts with a function like @@ERROR, which only returns an error number in the statement immediately following the one that causes an error.  

In a nested `CATCH` block, `ERROR_NUMBER` returns the error number specific to the scope of the `CATCH` block that referenced that `CATCH` block. For example, the `CATCH` block of an outer TRY...CATCH construct could have an inner `TRY...CATCH` construct. Inside that inner `CATCH` block, `ERROR_NUMBER` returns the number of the error that invoked the inner `CATCH` block. If `ERROR_NUMBER` runs in the outer `CATCH` block, it returns the number of the error that invoked that outer `CATCH` block.  
  
## Examples  
  
### A. Using ERROR_NUMBER in a CATCH block  
This example shows a `SELECT` statement that generates a divide-by-zero error. The `CATCH` block returns the error number.  
  
```sql  
BEGIN TRY  
    -- Generate a divide-by-zero error.  
    SELECT 1/0;  
END TRY  
BEGIN CATCH  
    SELECT ERROR_NUMBER() AS ErrorNumber;  
END CATCH;  
GO  
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
```
-----------

(0 row(s) affected)

ErrorNumber
-----------
8134

(1 row(s) affected)

```  
  
### B. Using ERROR_NUMBER in a CATCH block with other error-handling tools  
This example shows a `SELECT` statement that generates a divide-by-zero error. Along with the error number, the `CATCH` block returns information about that error.  

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

ErrorNumber ErrorSeverity ErrorState  ErrorProcedure   ErrorLine  ErrorMessage
----------- ------------- ----------- ---------------  ---------- ----------------------------------
8134        16            1           NULL             4          Divide by zero error encountered.

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
  
  

