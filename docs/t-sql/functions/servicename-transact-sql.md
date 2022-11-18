---
title: "@@SERVICENAME (Transact-SQL)"
description: "@@SERVICENAME (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@SERVICENAME_TSQL"
  - "@@SERVICENAME"
helpviewer_keywords:
  - "@@SERVICENAME function"
  - "names [SQL Server], registry keys"
  - "registry keys [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
---
# &#x40;&#x40;SERVICENAME (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the name of the registry key under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. @@SERVICENAME returns 'MSSQLSERVER' if the current instance is the default instance; this function returns the instance name if the current instance is a named instance.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@SERVICENAME  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **nvarchar**  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs as a service named MSSQLServer.  
  
## Examples  
 The following example shows using `@@SERVICENAME`.  
  
```sql  
SELECT @@SERVICENAME AS 'Service Name';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Service Name                    
------------------------------  
MSSQLSERVER                     
```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md)  
  
  
