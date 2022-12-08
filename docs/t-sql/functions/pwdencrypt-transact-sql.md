---
title: "PWDENCRYPT (Transact-SQL)"
description: "PWDENCRYPT (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "PWDENCRYPT"
  - "PWDENCRYPT_TSQL"
helpviewer_keywords:
  - "PWDENCRYPT function [Transact-SQL]"
dev_langs:
  - "TSQL"
---
# PWDENCRYPT (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] password hash of the input value that uses the current version of the password hashing algorithm.  
  
 PWDENCRYPT is an older function and might not be supported in a future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use [HASHBYTES](../../t-sql/functions/hashbytes-transact-sql.md) instead. HASHBYTES provides more hashing algorithms.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
PWDENCRYPT ( 'password' )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *password*  
 Is the password to be encrypted. *password* is **sysname**.  
  
## Return Types  
 **varbinary(128)**  
  
## Permissions  
 PWDENCRYPT is available to public.  
  
## See Also  
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [PWDCOMPARE &#40;Transact-SQL&#41;](../../t-sql/functions/pwdcompare-transact-sql.md)  
  
  
