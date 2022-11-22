---
description: "sp_helplogins (Transact-SQL)"
title: "sp_helplogins (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_helplogins_TSQL"
  - "sp_helplogins"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helplogins"
ms.assetid: f9ad3767-5b9f-420d-8922-b637811404f7
author: markingmyname
ms.author: maghan
---
# sp_helplogins (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Provides information about logins and the users associated with them in each database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helplogins [ [ @LoginNamePattern = ] 'login' ]  
```  
  
## Arguments  
`[ @LoginNamePattern = ] 'login'`
 Is a login name. *login* is **sysname**, with a default of NULL. *login* must exist if specified. If *login* is not specified, information about all logins is returned.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 The first report contains information about each login specified, as shown in the following table.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**LoginName**|**sysname**|Login name.|  
|**SID**|**varbinary(85)**|Login security identifier (SID).|  
|**DefDBName**|**sysname**|Default database that **LoginName** uses when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**DefLangName**|**sysname**|Default language used by **LoginName**.|  
|**Auser**|**char(5)**|Yes = **LoginName** has an associated user name in a database.<br /><br /> No = **LoginName** does not have an associated user name.|  
|**ARemote**|**char(7)**|Yes = **LoginName** has an associated remote login.<br /><br /> No = **LoginName** does not have an associated login.|  
  
 The second report contains information about users mapped to each login, and the role memberships of the login as shown in the following table.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**LoginName**|**sysname**|Login name.|  
|**DBName**|**sysname**|Default database that **LoginName** uses when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**UserName**|**sysname**|User account that **LoginName** is mapped to in **DBName**, and the roles that **LoginName** is a member of in **DBName**.|  
|**UserOrAlias**|**char(8)**|MemberOf = **UserName** is a role.<br /><br /> User = **UserName** is a user account.|  
  
## Remarks  
 Before removing a login, use **sp_helplogins** to identify user accounts that are mapped to the login.  
  
## Permissions  
 Requires membership in the **securityadmin** fixed server role.  
  
 To identify all user accounts mapped to a given login, **sp_helplogins** must check all databases within the server. Therefore, for each database on the server, at least one of the following conditions must be true:  
  
-   The user that is executing **sp_helplogins** has permission to access the database.  
  
-   The **guest** user account is enabled in the database.  
  
 If **sp_helplogins** cannot access a database, **sp_helplogins** will return as much information as it can and display error message 15622.  
  
## Examples  
 The following example reports information about the login `John`.  
  
```  
EXEC sp_helplogins 'John';  
GO  
  
LoginName SID                        DefDBName DefLangName AUser ARemote   
--------- -------------------------- --------- ----------- ----- -------   
John      0x23B348613497D11190C100C  master    us_english  yes   no  
  
(1 row(s) affected)  
  
LoginName   DBName   UserName   UserOrAlias   
---------   ------   --------   -----------   
John        pubs     John       User          
  
(1 row(s) affected)  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_helpdb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdb-transact-sql.md)   
 [sp_helpuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpuser-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
