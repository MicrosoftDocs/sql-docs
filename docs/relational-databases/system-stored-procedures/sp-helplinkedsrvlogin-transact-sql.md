---
title: "sp_helplinkedsrvlogin (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helplinkedsrvlogin_TSQL"
  - "sp_helplinkedsrvlogin"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helplinkedsrvlogin"
ms.assetid: a2b1eba0-bf71-47e7-a4c7-9f55feec82a3
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helplinkedsrvlogin (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Provides information about login mappings defined against a specific linked server used for distributed queries and remote stored procedures.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helplinkedsrvlogin [ [ @rmtsrvname = ] 'rmtsrvname' ]   
     [ , [ @locallogin = ] 'locallogin' ]  
```  
  
## Arguments  
 [ **@rmtsrvname=**] **'***rmtsrvname***'**  
 Is the name of the linked server that the login mapping applies to. *rmtsrvname* is **sysname**, with a default of NULL. If NULL, all login mappings defined against all the linked servers defined in the local computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are returned.  
  
 [ **@locallogin=**] **'***locallogin***'**  
 Is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login on the local server that has a mapping to the linked server *rmtsrvname*. *locallogin* is **sysname**, with a default of NULL. NULL specifies that all login mappings defined on *rmtsrvname* are returned. If not NULL, a mapping for *locallogin* to *rmtsrvname* must already exist. *locallogin* can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or a Windows user. The Windows user must have been granted access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] either directly or through its membership in a Windows group that has been granted access.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Linked Server**|**sysname**|Linked server name.|  
|**Local Login**|**sysname**|Local login for which the mapping applies.|  
|**Is Self Mapping**|**smallint**|0 = **Local Login** is mapped to **Remote Login** when connecting to **Linked Server**.<br /><br /> 1 = **Local Login** is mapped to the same login and password when connecting to **Linked Server**.|  
|**Remote Login**|**sysname**|Login name on **LinkedServer** that is mapped to **LocalLogin** when **IsSelfMapping** is 0. If **IsSelfMapping** is 1, **RemoteLogin** is NULL.|  
  
## Remarks  
 Before you delete login mappings, use **sp_helplinkedsrvlogin** to determine the linked servers that are involved.  
  
## Permissions  
 No permissions are checked.  
  
## Examples  
  
### A. Displaying all login mappings for all linked servers  
 The following example displays all login mappings for all linked servers defined on the local computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
EXEC sp_helplinkedsrvlogin;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Linked Server    Local Login   Is Self Mapping Remote Login   
---------------- ------------- --------------- --------------   
Accounts         NULL          1               NULL  
Sales            NULL          1               NULL  
Sales            Mary          0               sa  
Marketing        NULL          1               NULL  
  
(4 row(s) affected)  
```  
  
### B. Displaying all login mappings for a linked server  
 The following example displays all locally defined login mappings for the `Sales` linked server.  
  
```  
EXEC sp_helplinkedsrvlogin 'Sales';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Linked Server    Local Login   Is Self Mapping Remote Login   
---------------- ------------- --------------- --------------   
Sales            NULL          1               NULL  
Sales            Mary          0               sa  
  
(2 row(s) affected)  
```  
  
### C. Displaying all login mappings for a local login  
 The following example displays all locally defined login mappings for the login `Mary`.  
  
```  
EXEC sp_helplinkedsrvlogin NULL, 'Mary';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Linked Server    Local Login   Is Self Mapping Remote Login   
---------------- ------------- --------------- --------------   
Sales            NULL          1               NULL  
Sales            Mary          0               sa  
  
(2 row(s) affected)  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sp_droplinkedsrvlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droplinkedsrvlogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
