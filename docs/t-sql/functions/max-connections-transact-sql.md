---
title: "@@MAX_CONNECTIONS (Transact-SQL)"
description: "@@MAX_CONNECTIONS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@MAX_CONNECTIONS"
  - "@@MAX_CONNECTIONS_TSQL"
helpviewer_keywords:
  - "simultaneous connections [SQL Server]"
  - "maximum number of simultaneous user connections"
  - "@@MAX_CONNECTIONS function"
  - "connections [SQL Server], simultaneous"
  - "number of simultaneous user connections"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;MAX_CONNECTIONS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the maximum number of simultaneous user connections allowed on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The number returned is not necessarily the number currently configured.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
@@MAX_CONNECTIONS  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **integer**  
  
## Remarks  
 The actual number of user connections allowed also depends on the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is installed and the limitations of your applications and hardware.  
  
 To reconfigure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for fewer connections, use **sp_configure**.  
  
## Examples  
 The following example shows returning the maximum number of user connections on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The example assumes that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has not been reconfigured for fewer user connections.  
  
```sql 
SELECT @@MAX_CONNECTIONS AS 'Max Connections';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Max Connections  
---------------  
32767            
```  
  
## See Also  
 [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [Configuration Functions](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [Configure the user connections Server Configuration Option](../../database-engine/configure-windows/configure-the-user-connections-server-configuration-option.md)  
  
  
