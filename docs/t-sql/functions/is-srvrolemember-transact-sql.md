---
title: "IS_SRVROLEMEMBER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "IS_SRVROLEMEMBER_TSQL"
  - "IS_SRVROLEMEMBER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "roles [SQL Server], members"
  - "IS_SRVROLEMEMBER function"
  - "members [SQL Server], verifying"
ms.assetid: 3241a44a-6958-415b-b8b7-2a1207c36ab3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# IS_SRVROLEMEMBER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Indicates whether a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is a member of the specified server role.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
IS_SRVROLEMEMBER ( 'role' [ , 'login' ] )  
```  
  
## Arguments  
 **'** *role* **'**  
 Is the name of the server role that is being checked. *role* is **sysname**.  
  
 Valid values for *role* are user-defined server roles, and the following fixed server roles:  
  
|||  
|-|-|  
|sysadmin|serveradmin|  
|dbcreator|setupadmin|  
|bulkadmin|securityadmin|  
|diskadmin|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> public|  
|processadmin||  
  
 **'** *login* **'**  
 Is the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to check. *login* is **sysname**, with a default of NULL. If no value is specified, the result is based on the current Execution context. If the parameter contains the word NULL will return NULL.  
  
## Return Types  
 **int**  
  
|Return value|Description|  
|------------------|-----------------|  
|0|*login* is not a member of *role*.<br /><br /> In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], this statement always returns 0.|  
|1|*login* is a member of *role*.|  
|NULL|*role* or *login* is not valid, or you do not have permission to view the role membership.|  
  
## Remarks  
 UseIS_SRVROLEMEMBER to determine whether the current user can perform an action requiring the server role's permissions.  
  
 If a Windows login, such as Contoso\Mary5, is specified for *login*, **IS_SRVROLEMEMBER** returns **NULL**, unless the login has been granted or denied direct access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 If the optional *login* parameter is not provided and if *login* is a Windows domain login, it may be a member of a fixed server role through membership in a Windows group. To resolve such indirect memberships, IS_SRVROLEMEMBER requests Windows group membership information from the domain controller. If the domain controller is inaccessible or does not respond, **IS_SRVROLEMEMBER** returns role membership information by accounting for the user and its local groups only. If the user specified is not the current user, the value returned by IS_SRVROLEMEMBER might differ from the authenticator's (such as Active Directory) last data update to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 If the optional login parameter is provided, the Windows login that is being queried must be present in sys.server_principals, or IS_SRVROLEMEMBER will return NULL. This indicates that the login is not valid.  
  
 When the login parameter is a domain login or based on a Windows group and the domain controller is inaccessible, calls to IS_SRVROLEMEMBER will fail and might return incorrect or incomplete data.  
  
 If the domain controller is not available, the call to IS_SRVROLEMEMBER will return accurate information when the Windows principle can be authenticated locally, such as a local Windows account or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 **IS_SRVROLEMEMBER** always returns 0 when a Windows group is used as the login argument, and this Windows group is a member of another Windows group which is, in turn, a member of the specified server role.  
  
 The User Account Control (UAC) setting might also cause the return different results. This would depend on whether the user accessed the server as a Windows group member or as a specific [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user.  
  
 This function evaluates role membership, not the underlying permission. For example, the **sysadmin** fixed server role has the **CONTROL SERVER** permission. If the user has the **CONTROL SERVER** permission but is not a member of the role, this function will correctly report that the user is not a member of the **sysadmin** role, even though the user has the same permissions.  
  
## Related Functions  
 To determine whether the current user is a member of the specified Windows group or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database role, use [IS_MEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-member-transact-sql.md). To determine whether a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is a member of a database role, use [IS_ROLEMEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-rolemember-transact-sql.md).  
  
## Permissions  
 Requires VIEW DEFINITION permission on the server role.  
  
## Examples  
 The following example indicates whether the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for the current user is a member of the `sysadmin` fixed server role.  
  
```  
IF IS_SRVROLEMEMBER ('sysadmin') = 1  
   print 'Current user''s login is a member of the sysadmin role'  
ELSE IF IS_SRVROLEMEMBER ('sysadmin') = 0  
   print 'Current user''s login is NOT a member of the sysadmin role'  
ELSE IF IS_SRVROLEMEMBER ('sysadmin') IS NULL  
   print 'ERROR: The server role specified is not valid.';  
```  
  
 The following example indicates whether the domain login Pat is a member of the **diskadmin** fixed server role.  
  
```  
SELECT IS_SRVROLEMEMBER('diskadmin', 'Contoso\Pat');  
```  
  
## See Also  
 [IS_MEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-member-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)  
  
  
