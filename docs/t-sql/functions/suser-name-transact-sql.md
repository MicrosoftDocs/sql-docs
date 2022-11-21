---
title: "SUSER_NAME (Transact-SQL)"
description: "SUSER_NAME (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "12/12/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SUSER_NAME"
  - "SUSER_NAME_TSQL"
helpviewer_keywords:
  - "security identification names [SQL Server]"
  - "logins [SQL Server], users"
  - "identification names for logins [SQL Server]"
  - "users [SQL Server], logins"
  - "SUSER_NAME function"
  - "logins [SQL Server], names"
  - "names [SQL Server], logins"
dev_langs:
  - "TSQL"
monikerRange: "= azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SUSER_NAME (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi-asa-svrless-poolonly.md)]

Returns the login identification name of the user.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SUSER_NAME ( [ server_user_id ] )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
_server\_user\_id_  
Is the login identification number of the user. _server\_user\_id_, which is optional, is **int**. _server\_user\_id_ can be the login identification number of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user or group that has permission to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When _server\_user\_id_ isn't specified, the login identification name for the current user is returned. If the parameter contains the word NULL, it will return NULL.  
  
## Return Types  
**nvarchar(128)**  
  
## Remarks  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0, the security identification number (SID) replaced the server user identification number (SUID).  
  
SUSER_NAME returns a login name only for a login that has an entry in the **syslogins** system table.  
  
SUSER_NAME can be used in a select list, in a WHERE clause, and anywhere an expression is allowed. Use parentheses after SUSER_NAME, even if no parameter is specified.  

> [!NOTE]
> Although the SUSER_NAME function is supported on Azure SQL Database, using *Execute as* with SUSER_NAME is not supported on Azure SQL Database. 
  
## Examples  
The following example returns the login identification name of the user with a login identification number of `1`.  
  
```sql
SELECT SUSER_NAME(1);  
```  
  
## See Also  
[SUSER_ID &#40;Transact-SQL&#41;](../../t-sql/functions/suser-id-transact-sql.md)   
[Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
