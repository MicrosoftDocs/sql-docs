---
title: "@@MAX_CONNECTIONS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@MAX_CONNECTIONS"
  - "@@MAX_CONNECTIONS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "simultaneous connections [SQL Server]"
  - "maximum number of simultaneous user connections"
  - "@@MAX_CONNECTIONS function"
  - "connections [SQL Server], simultaneous"
  - "number of simultaneous user connections"
ms.assetid: 57eb9f4b-548f-4212-9684-a11d831c4732
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;MAX_CONNECTIONS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the maximum number of simultaneous user connections allowed on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The number returned is not necessarily the number currently configured.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@MAX_CONNECTIONS  
```  
  
## Return Types  
 **integer**  
  
## Remarks  
 The actual number of user connections allowed also depends on the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is installed and the limitations of your applications and hardware.  
  
 To reconfigure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for fewer connections, use **sp_configure**.  
  
## Examples  
 The following example shows returning the maximum number of user connections on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The example assumes that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has not been reconfigured for fewer user connections.  
  
```  
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
  
  
