---
title: "@@TRANCOUNT (Transact-SQL)"
description: "@@TRANCOUNT (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "08/29/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@TRANCOUNT_TSQL"
  - "@@TRANCOUNT"
helpviewer_keywords:
  - "@@TRANCOUNT function"
  - "number of active transactions"
  - "connections [SQL Server], active transactions"
  - "active transactions"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# &#x40;&#x40;TRANCOUNT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the number of BEGIN TRANSACTION statements that have occurred on the current connection.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  

```syntaxsql  
@@TRANCOUNT  
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **integer**  
  
## Remarks  
 The BEGIN TRANSACTION statement increments @@TRANCOUNT by 1. ROLLBACK TRANSACTION decrements @@TRANCOUNT to 0, except for ROLLBACK TRANSACTION *savepoint_name*, which does not affect @@TRANCOUNT. COMMIT TRANSACTION or COMMIT WORK decrement @@TRANCOUNT by 1.  
  
## Examples  
  
### A. Showing the effects of the BEGIN and COMMIT statements  
 The following example shows the effect that nested `BEGIN` and `COMMIT` statements have on the `@@TRANCOUNT` variable.  
  

```sql  
PRINT @@TRANCOUNT  
--  The BEGIN TRAN statement will increment the  
--  transaction count by 1.  
BEGIN TRAN  
    PRINT @@TRANCOUNT  
    BEGIN TRAN  
        PRINT @@TRANCOUNT  
--  The COMMIT statement will decrement the transaction count by 1.  
    COMMIT  
    PRINT @@TRANCOUNT  
COMMIT  
PRINT @@TRANCOUNT  
--Results  
--0  
--1  
--2  
--1  
--0  
```  
  
### B. Showing the effects of the BEGIN and ROLLBACK statements  
 The following example shows the effect that nested `BEGIN TRAN` and `ROLLBACK` statements have on the `@@TRANCOUNT` variable.  
  

```sql  
PRINT @@TRANCOUNT  
--  The BEGIN TRAN statement will increment the  
--  transaction count by 1.  
BEGIN TRAN  
    PRINT @@TRANCOUNT  
    BEGIN TRAN  
        PRINT @@TRANCOUNT  
--  The ROLLBACK statement will clear the @@TRANCOUNT variable  
--  to 0 because all active transactions will be rolled back.  
ROLLBACK  
PRINT @@TRANCOUNT  
--Results  
--0  
--1  
--2  
--0  
```  
  
## See Also  
 [BEGIN TRANSACTION \(Transact-SQL\)](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [COMMIT TRANSACTION \(Transact-SQL\)](../../t-sql/language-elements/commit-transaction-transact-sql.md)   
 [ROLLBACK TRANSACTION \(Transact-SQL\)](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [System Functions \(Transact-SQL\)](../../relational-databases/system-functions/system-functions-category-transact-sql.md)  
  
  
