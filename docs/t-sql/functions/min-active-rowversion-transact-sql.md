---
title: "MIN_ACTIVE_ROWVERSION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "MIN_ACTIVE_ROWVERSION"
  - "MIN_ACTIVE_ROWVERSION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MIN_ACTIVE_ROWVERSION function [Transact-SQL]"
ms.assetid: 87c89547-8ea1-4820-b75e-36be683e4e10
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MIN_ACTIVE_ROWVERSION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the lowest active **rowversion** value in the current database. A **rowversion** value is active if it is used in a transaction that has not yet been committed. For more information, see [rowversion &#40;Transact-SQL&#41;](../../t-sql/data-types/rowversion-transact-sql.md).  
  
> [!NOTE]  
>  The **rowversion** data type is also known as **timestamp**.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
MIN_ACTIVE_ROWVERSION  
```  
  
## Return Types  
 Returns a **binary(8)** value.  
  
## Remarks  
 MIN_ACTIVE_ROWVERSION is a non-deterministic function that returns the lowest active **rowversion** value in the current database. A new **rowversion** value is typically generated when an insert or update is performed on a table that contains a column of type **rowversion**. If there are no active values in the database, MIN_ACTIVE_ROWVERSION returns the same value as @@DBTS + 1.  
  
 MIN_ACTIVE_ROWVERSION is useful for scenarios such as data synchronization that use **rowversion** values to group sets of changes together. If an application uses @@DBTS rather than MIN_ACTIVE_ROWVERSION, it is possible to miss changes that are active when synchronization occurs.  
  
 The MIN_ACTIVE_ROWVERSION function is not affected by changes in the transaction isolation levels.  
  
## Examples  
 The following example returns **rowversion** values by using `MIN_ACTIVE_ROWVERSION` and `@@DBTS`. Notice that the values differ when there are no active transactions in the database.  
  
```  
-- Create a table that has a ROWVERSION column in it.  
CREATE TABLE RowVersionTestTable (rv ROWVERSION)  
GO  
  
-- Print the current values for the database.  
PRINT ''  
PRINT 'DBTS'  
PRINT @@DBTS  
PRINT 'MIN_ACTIVE_ROWVERSION'  
PRINT MIN_ACTIVE_ROWVERSION()   
GO  
---------------- Results ----------------  
--DBTS  
--0x00000000000007E2  
--MIN_ACTIVE_ROWVERSION  
--0x00000000000007E3  
  
-- Insert a row.  
INSERT INTO RowVersionTestTable VALUES (DEFAULT)  
SELECT * FROM RowVersionTestTable  
GO  
---------------- Results ----------------  
--rv  
--0x00000000000007E3  
  
-- Print the current values for the database.  
PRINT ''  
PRINT 'DBTS'  
PRINT @@DBTS  
PRINT 'MIN_ACTIVE_ROWVERSION'  
PRINT MIN_ACTIVE_ROWVERSION()  
GO  
---------------- Results ----------------  
--DBTS  
--0x00000000000007E3  
--MIN_ACTIVE_ROWVERSION  
--0x00000000000007E4  
  
-- Insert a new row inside a transaction but do not commit.  
BEGIN TRAN  
INSERT INTO RowVersionTestTable VALUES (DEFAULT)  
SELECT * FROM RowVersionTestTable  
GO  
---------------- Results ----------------  
--rv  
--0x00000000000007E3  
--0x00000000000007E4  
  
-- Print the current values for the database.  
PRINT ''  
PRINT 'DBTS'  
PRINT @@DBTS  
PRINT 'MIN_ACTIVE_ROWVERSION'  
PRINT MIN_ACTIVE_ROWVERSION()   
GO  
---------------- Results ----------------  
--DBTS  
--0x00000000000007E4  
--MIN_ACTIVE_ROWVERSION  
--0x00000000000007E4  
  
-- Commit the transaction.  
COMMIT  
GO  
  
-- Print the current values for the database.  
PRINT ''  
PRINT 'DBTS'  
PRINT @@DBTS  
PRINT 'MIN_ACTIVE_ROWVERSION'  
PRINT MIN_ACTIVE_ROWVERSION()  
GO  
---------------- Results ----------------  
--DBTS  
--0x00000000000007E4  
--MIN_ACTIVE_ROWVERSION  
--0x00000000000007E5  
```  
  
## See Also  
 [@@DBTS &#40;Transact-SQL&#41;](../../t-sql/functions/dbts-transact-sql.md)   
 [rowversion &#40;Transact-SQL&#41;](../../t-sql/data-types/rowversion-transact-sql.md)  
  
  
