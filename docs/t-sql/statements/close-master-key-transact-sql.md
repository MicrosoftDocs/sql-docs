---
title: "CLOSE MASTER KEY (Transact-SQL)"
description: CLOSE MASTER KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CLOSE MASTER KEY"
  - "CLOSE_MASTER_KEY_TSQL"
helpviewer_keywords:
  - "encryption [SQL Server], Database Master Key"
  - "CLOSE MASTER KEY statement"
  - "database master key [SQL Server], closing"
  - "cryptography [SQL Server], Database Master Key"
  - "closing Database Master Keys"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CLOSE MASTER KEY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Closes the master key of the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
CLOSE MASTER KEY  
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 Takes no arguments.  
  
## Remarks  
 This statement reverses the operation performed by OPEN MASTER KEY. CLOSE MASTER KEY only succeeds when the database master key was opened in the current session by using the OPEN MASTER KEY statement.  
  
## Permissions  
 No permissions are required.  
  
## Examples  
  
```sql  
USE AdventureWorks2012;  
CLOSE MASTER KEY;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
```sql  
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
  
  

