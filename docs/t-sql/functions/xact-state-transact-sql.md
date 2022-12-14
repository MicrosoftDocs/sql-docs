---
title: "XACT_STATE (Transact-SQL)"
description: "XACT_STATE (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "XACT_STATE"
  - "XACT_STATE_TSQL"
helpviewer_keywords:
  - "states [SQL Server], transactions"
  - "uncommittable transactions"
  - "sessions [SQL Server], active transactions"
  - "XACT_STATE function"
  - "transactions [SQL Server], state"
  - "active transactions"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# XACT_STATE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Is a scalar function that reports the user transaction state of a current running request. XACT_STATE indicates whether the request has an active user transaction, and whether the transaction is capable of being committed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
XACT_STATE()  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Return Type  
 **smallint**  
  
## Remarks  
 XACT_STATE returns the following values.  
  
|Return value|Meaning|  
|------------------|-------------|  
|1|The current request has an active user transaction. The request can perform any actions, including writing data and committing the transaction.|  
|0|There is no active user transaction for the current request.|  
|-1|The current request has an active user transaction, but an error has occurred that has caused the transaction to be classified as an uncommittable transaction. The request cannot commit the transaction or roll back to a savepoint; it can only request a full rollback of the transaction. The request cannot perform any write operations until it rolls back the transaction. The request can only perform read operations until it rolls back the transaction. After the transaction has been rolled back, the request can perform both read and write operations and can begin a new transaction.<br /><br /> When the outermost batch finishes running, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will automatically roll back any active uncommittable transactions. If no error message was sent when the transaction entered an uncommittable state, when the batch finishes, an error message will be sent to the client application. This message indicates that an uncommittable transaction was detected and rolled back.|  
  
 Both the XACT_STATE and @@TRANCOUNT functions can be used to detect whether the current request has an active user transaction. @@TRANCOUNT cannot be used to determine whether that transaction has been classified as an uncommittable transaction. XACT_STATE cannot be used to determine whether there are nested transactions.  
  
## Examples  
 The following example uses `XACT_STATE` in the `CATCH` block of a `TRY...CATCH` construct to determine whether to commit or roll back a transaction. Because `SET XACT_ABORT` is `ON`, the constraint violation error causes the transaction to enter an uncommittable state.  
  
```sql  
USE AdventureWorks2012;  
GO  
  
-- SET XACT_ABORT ON will render the transaction uncommittable  
-- when the constraint violation occurs.  
SET XACT_ABORT ON;  
  
BEGIN TRY  
    BEGIN TRANSACTION;  
        -- A FOREIGN KEY constraint exists on this table. This   
        -- statement will generate a constraint violation error.  
        DELETE FROM Production.Product  
            WHERE ProductID = 980;  
  
    -- If the delete operation succeeds, commit the transaction. The CATCH  
    -- block will not execute.  
    COMMIT TRANSACTION;  
END TRY  
BEGIN CATCH  
    -- Test XACT_STATE for 0, 1, or -1.  
    -- If 1, the transaction is committable.  
    -- If -1, the transaction is uncommittable and should   
    --     be rolled back.  
    -- XACT_STATE = 0 means there is no transaction and  
    --     a commit or rollback operation would generate an error.  
  
    -- Test whether the transaction is uncommittable.  
    IF (XACT_STATE()) = -1  
    BEGIN  
        PRINT 'The transaction is in an uncommittable state.' +  
              ' Rolling back transaction.'  
        ROLLBACK TRANSACTION;  
    END;  
  
    -- Test whether the transaction is active and valid.  
    IF (XACT_STATE()) = 1  
    BEGIN  
        PRINT 'The transaction is committable.' +   
              ' Committing transaction.'  
        COMMIT TRANSACTION;     
    END;  
END CATCH;  
GO  
```  
  
## See Also  
 [@@TRANCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/trancount-transact-sql.md)   
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [COMMIT TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-transaction-transact-sql.md)   
 [ROLLBACK TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [SAVE TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/save-transaction-transact-sql.md)   
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)  
  
  
