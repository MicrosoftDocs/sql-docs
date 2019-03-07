---
title: "@@SERVICENAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@SERVICENAME_TSQL"
  - "@@SERVICENAME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@SERVICENAME function"
  - "names [SQL Server], registry keys"
  - "registry keys [SQL Server]"
ms.assetid: 5b0b35be-50ae-411d-a607-bf7464b73624
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# &#x40;&#x40;SERVICENAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]

  Returns the name of the registry key under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. @@SERVICENAME returns 'MSSQLSERVER' if the current instance is the default instance; this function returns the instance name if the current instance is a named instance.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@SERVICENAME  
```  
  
## Return Types  
 **nvarchar**  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs as a service named MSSQLServer.  
  
## Examples  
 The following example shows using `@@SERVICENAME`.  
  
```  
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
  
  
