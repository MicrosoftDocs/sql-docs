---
title: "@@PROCID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@PROCID"
  - "@@PROCID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "stored procedures [SQL Server], identification numbers"
  - "UDTs [SQL Server], object identifiers"
  - "@@PROCID function"
  - "user-defined functions [SQL Server], object identifiers"
  - "triggers [SQL Server], object identifiers"
  - "identification numbers [SQL Server], modules"
  - "IDs [SQL Server], modules"
  - "module object identifiers [SQL Server]"
ms.assetid: 0d4882c7-edb8-49b1-a470-2c7497b8998f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;PROCID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the object identifier (ID) of the current [!INCLUDE[tsql](../../includes/tsql-md.md)] module. A [!INCLUDE[tsql](../../includes/tsql-md.md)] module can be a stored procedure, user-defined function, or trigger. @@PROCID cannot be specified in CLR modules or the in-process data access provider.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@PROCID  
```  
  
## Return Types  
 **int**  
  
## Examples  
 The following example uses `@@PROCID` as the input parameter in the `OBJECT_NAME` function to return the name of the stored procedure in the `RAISERROR` message.  
  
```  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID ( 'usp_FindName', 'P' ) IS NOT NULL   
DROP PROCEDURE usp_FindName;  
GO  
CREATE PROCEDURE usp_FindName  
    @lastname varchar(40) = '%',   
    @firstname varchar(20) = '%'  
AS  
DECLARE @Count int;  
DECLARE @ProcName nvarchar(128);  
SELECT LastName, FirstName  
FROM Person.Person   
WHERE FirstName LIKE @firstname AND LastName LIKE @lastname;  
SET @Count = @@ROWCOUNT;  
SET @ProcName = OBJECT_NAME(@@PROCID);  
RAISERROR ('Stored procedure %s returned %d rows.', 16,10, @ProcName, @Count);  
GO  
EXECUTE dbo.usp_FindName 'P%', 'A%';  
```  
  
## See Also  
 [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)  
  
  
