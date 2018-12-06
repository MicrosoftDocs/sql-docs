---
title: "xp_logininfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "xp_logininfo_TSQL"
  - "xp_logininfo"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xp_logininfo"
ms.assetid: ee7162b5-e11f-4a0e-a09c-1878814dbbbd
author: VanMSFT
ms.author: vanto
manager: craigg
---
# xp_logininfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about Windows users and Windows groups.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
xp_logininfo [ [ @acctname = ] 'account_name' ]   
     [ , [ @option = ] 'all' | 'members' ]   
     [ , [ @privilege = ] variable_name OUTPUT]  
```  
  
## Arguments  
 [ **@acctname =** ] **'***account_name***'**  
 Is the name of a Windows user or group granted access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *account_name* is **sysname**, with a default of NULL. If *account_name* is not specified, all Windows groups and Windows users that have been explicitly granted login permission are reported. *account_name* must be fully qualified. For example, 'ADVWKS4\macraes', or 'BUILTIN\Administrators'.  
  
 **'all'** | **'members'**  
 Specifies whether to report information about all permission paths for the account, or to report information about the members of the Windows group. **@option** is **varchar(10)**, with a default of NULL. Unless **all** is specified, only the first permission path is displayed.  
  
 [ **@privilege =** ] *variable_name*  
 Is an output parameter that returns the privilege level of the specified Windows account. *variable_name* is **varchar(10)**, with a default of 'Not wanted'. The privilege level returned is **user**, **admin**, or **null**.  
  
 OUTPUT  
 When specified, puts *variable_name* in the output parameter.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**account name**|**sysname**|Fully qualified Windows account name.|  
|**type**|**char(8)**|Type of Windows account. Valid values are **user** or **group**.|  
|**privilege**|**char(9)**|Access privilege for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Valid values are **admin**, **user**, or **null**.|  
|**mapped login name**|**sysname**|For user accounts that have user privilege, **mapped login name** shows the mapped login name that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to use when logging in with this account by using the mapped rules with the domain name added before it.|  
|**permission path**|**sysname**|Group membership that allowed the account access.|  
  
## Remarks  
 If *account_name* is specified, **xp_logininfo** reports the highest privilege level of the specified Windows user or group. If a Windows user has access as both a system administrator and as a domain user, it will be reported as a system administrator. If the user is a member of multiple Windows groups of equal privilege level, only the group that was first granted access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is reported.  
  
 If *account_name* is a valid Windows user or group that is not associated with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, an empty result set is returned. If *account_name* cannot be identified as a valid Windows user or group, an error message is returned.  
  
 If *account_name* and **all** are specified, all permission paths for the Windows user or group are returned. If *account_name* is a member of multiple groups, all of which have been granted access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], multiple rows are returned. The **admin** privilege rows are returned before the **user** privilege rows, and within a privilege level rows are returned in the order in which the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins were created.  
  
 If *account_name* and **members** are specified, a list of the next-level members of the group is returned. If *account_name* is a local group, the listing can include local users, domain users, and groups. If *account_name* is a domain account, the list is made up of domain users. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must connect to the domain controller to retrieve group membership information. If the server cannot contact the domain controller, no information will be returned.  
  
 **xp_logininfo** only returns information from Active Director global groups, not universal groups.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role or membership in the **public** fixed database role in the **master** database with EXECUTE permission granted.  
  
## Examples  
 The following example displays information about the `BUILTIN\Administrators` Windows group.  
  
```  
EXEC xp_logininfo 'BUILTIN\Administrators';  
```  
  
## See Also  
 [sp_denylogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-denylogin-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [sp_revokelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revokelogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [General Extended Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/general-extended-stored-procedures-transact-sql.md)  
  
  
