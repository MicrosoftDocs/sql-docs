---
title: "sys.server_role_members (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "server_role_members"
  - "sys.server_role_members_TSQL"
  - "server_role_members_TSQL"
  - "sys.server_role_members"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.server_role_members catalog view"
ms.assetid: efa20414-2c6b-45a2-a7a9-60110a24da18
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.server_role_members (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]

  Returns one row for each member of each fixed and user-defined server role.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**role_principal_id**|**int**|Server-Principal ID of the role.|  
|**member_principal_id**|**int**|Server-Principal ID of the member.|  
  
 To add or remove server role membership, use the [ALTER SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-role-transact-sql.md)statement.  
  
## Permissions  
 Logins can view their own server role membership and can view the principal_id's of the members of the fixed server roles. To view all server role membership requires the **VIEW DEFINITION ON SERVER ROLE** permission or membership in the **securityadmin** fixed server role.  
  
 For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example returns the names and id's of the roles and their members.  
  
```  
SELECT sys.server_role_members.role_principal_id, role.name AS RoleName,   
    sys.server_role_members.member_principal_id, member.name AS MemberName  
FROM sys.server_role_members  
JOIN sys.server_principals AS role  
    ON sys.server_role_members.role_principal_id = role.principal_id  
JOIN sys.server_principals AS member  
    ON sys.server_role_members.member_principal_id = member.principal_id;  
```  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
