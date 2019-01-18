---
title: "SUSER_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SUSER_NAME"
  - "SUSER_NAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "security identification names [SQL Server]"
  - "logins [SQL Server], users"
  - "identification names for logins [SQL Server]"
  - "users [SQL Server], logins"
  - "SUSER_NAME function"
  - "logins [SQL Server], names"
  - "names [SQL Server], logins"
ms.assetid: ae598d9f-9baa-49b8-b1c1-042854206de4
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SUSER_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]

  Returns the login identification name of the user.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SUSER_NAME ( [ server_user_id ] )   
```  
  
## Arguments  
 *server_user_id*  
 Is the login identification number of the user. *server_user_id*, which is optional, is **int**. *server_user_id* can be the login identification number of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user or group that has permission to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If *server_user_id* is not specified, the login identification name for the current user is returned. If the parameter contains the word NULL will return NULL.  
  
## Return Types  
 **nvarchar(128)**  
  
## Remarks  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0, the security identification number (SID) replaced the server user identification number (SUID).  
  
 SUSER_NAME returns a login name only for a login that has an entry in the **syslogins** system table.  
  
 SUSER_NAME can be used in a select list, in a WHERE clause, and anywhere an expression is allowed, and must always be followed by parentheses, even if no parameter is specified.  
  
## Examples  
 The following example returns the login identification name of the user with a login identification number of `1`.  
  
```  
SELECT SUSER_NAME(1);  
```  
  
## See Also  
 [SUSER_ID &#40;Transact-SQL&#41;](../../t-sql/functions/suser-id-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
