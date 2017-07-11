---
title: "@@SERVERNAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "@@SERVERNAME"
  - "@@SERVERNAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@SERVERNAME function"
  - "local servers [SQL Server]"
ms.assetid: b0ef33fb-954a-4294-b05b-a87c14ce25a3
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# @@SERVERNAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the name of the local server that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
@@SERVERNAME  
```  
  
## Return Types  
 **nvarchar**  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets the server name to the computer name during installation. To change the name of the server, use **sp_addserver**, and then restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 With multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed, @@SERVERNAME returns the following local server name information if the local server name has not been changed since setup.  
  
|Instance|Server information|  
|--------------|------------------------|  
|Default instance|'*servername*'|  
|Named instance|'*servername*\\*instancename*'|  
|failover clustered instance - default instance|'*virtualservername*'|  
|failover clustered instance - named instance|'*virtualservername*\\*instancename*'|  
  
 Although the @@SERVERNAME function and the SERVERNAME property of SERVERPROPERTY function may return strings with similar formats, the information can be different. The SERVERNAME property automatically reports changes in the network name of the computer.  
  
 In contrast, @@SERVERNAME does not report such changes. @@SERVERNAME reports changes made to the local server name using the **sp_addserver** or **sp_dropserver** stored procedure.  
  
## Examples  
 The following example shows using `@@SERVERNAME`.  
  
```  
SELECT @@SERVERNAME AS 'Server Name'  
```  
  
 Here is a sample result set.  
  
```  
Server Name  
---------------------------------  
ACCTG  
  
```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)   
 [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)  
  
  