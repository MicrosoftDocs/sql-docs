---
title: "sys.server_role_members (Transact-SQL)"
description: sys.server_role_members (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/15/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_role_members"
  - "sys.server_role_members_TSQL"
  - "server_role_members_TSQL"
  - "sys.server_role_members"
helpviewer_keywords:
  - "sys.server_role_members catalog view"
dev_langs:
  - "TSQL"
ms.assetid: efa20414-2c6b-45a2-a7a9-60110a24da18
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.server_role_members (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Returns one row for each member of each fixed and user-defined server role.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**role_principal_id**|**int**|Server-Principal ID of the role.|  
|**member_principal_id**|**int**|Server-Principal ID of the member.|  
  
 To add or remove server role membership, use the [ALTER SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-role-transact-sql.md)statement.  
  
## Permissions

Logins can view their own server role membership and can view the principal_id's of the members of the fixed server roles. To view all server role membership requires the **VIEW ANY DEFINITION** permission or membership in the **securityadmin** fixed server role.  

Logins can also view role memberships of roles they own.

In Azure SQL Database, members of the server-role **##MS_DefinitionReader##** can query all catalog views.

For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## <a name="_examples"></a> Examples

The examples in this section show how to work with server-level roles in Azure SQL Database.  

### A. Return list of server-role members

 The following example returns the names and IDs of the roles and their members.

```  
SELECT	roles.principal_id							AS RolePrincipalID
	,	roles.name									AS RolePrincipalName
	,	server_role_members.member_principal_id		AS MemberPrincipalID
	,	members.name								AS MemberPrincipalName
FROM sys.server_role_members AS server_role_members
INNER JOIN sys.server_principals AS roles
    ON server_role_members.role_principal_id = roles.principal_id
INNER JOIN sys.server_principals AS members 
    ON server_role_members.member_principal_id = members.principal_id  
;
```  

> [!NOTE]
> In Azure SQL Database, SQL logins are not persisted in the `sys.server_principals` catalog view. Therefore, to retrieve the server-level role membership in Azure SQL Database, the catalog view `sys.sql_logins` needs to be joined.


### B. Azure SQL Database: Listing all principals (SQL authentication) which are members of a server-level role

The following statement returns all members of any fixed server-level role using the `sys.server_role_members` and `sys.sql_logins` catalog views. This statement has to be run in the virtual master database.
  
```sql  
SELECT
		sql_logins.principal_id			AS MemberPrincipalID
	,	sql_logins.name					AS MemberPrincipalName
	,	roles.principal_id				AS RolePrincipalID
	,	roles.name						AS RolePrincipalName
FROM sys.server_role_members AS server_role_members
INNER JOIN sys.server_principals AS roles
    ON server_role_members.role_principal_id = roles.principal_id
INNER JOIN sys.sql_logins AS sql_logins 
    ON server_role_members.member_principal_id = sql_logins.principal_id
;  
GO  
```  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
