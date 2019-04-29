---
title: "CLOSE MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CLOSE MASTER KEY"
  - "CLOSE_MASTER_KEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "encryption [SQL Server], Database Master Key"
  - "CLOSE MASTER KEY statement"
  - "database master key [SQL Server], closing"
  - "cryptography [SQL Server], Database Master Key"
  - "closing Database Master Keys"
ms.assetid: bb04ef7a-9f3a-437e-a6f9-ba0204082cb9
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CLOSE MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Closes the master key of the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CLOSE MASTER KEY  
```  
  
## Arguments  
 Takes no arguments.  
  
## Remarks  
 This statement reverses the operation performed by OPEN MASTER KEY. CLOSE MASTER KEY only succeeds when the database master key was opened in the current session by using the OPEN MASTER KEY statement.  
  
## Permissions  
 No permissions are required.  
  
## Examples  
  
```  
USE AdventureWorks2012;  
CLOSE MASTER KEY;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
```  
USE master;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = '43987hkhj4325tsku7';  
GO   
CLOSE MASTER KEY;  
GO  
```  
  
## See Also  
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [OPEN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-master-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  

