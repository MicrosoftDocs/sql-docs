---
title: "sys.server_principals (Transact-SQL)"
description: sys.server_principals (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/11/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_principals"
  - "sys.server_principals_TSQL"
  - "sys.server_principals"
  - "server_principals_TSQL"
helpviewer_keywords:
  - "sys.server_principals catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-current||=azuresqldb-mi-current"
---

# sys.server_principals (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

Contains a row for every server-level principal.

[!INCLUDE [entra-id](../../includes/entra-id.md)]

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**name**|**sysname**|Name of the principal. Is unique within a server.|  
|**principal_id**|**int**|ID number of the Principal. Is unique within a server.|  
|**sid**|**varbinary(85)**| Security Identifier (SID) of the principal.<br />A Windows principal's SID matches their Windows SID.<br />A Microsoft Entra user's SID is the binary representation of its Microsoft Entra object ID, plus 'AADE' appended.<br />A Microsoft Entra group's SID is the binary representation of its Microsoft Entra object ID, plus 'AADE' appended.<br />A Microsoft Entra application's SID is the binary representation of its Microsoft Entra client ID, plus 'AADE' appended. |
|**type**|**char(1)**|Principal type:<br /><br /> S = SQL login<br /> R = Server role<br /><br />Available in SQL Server, Azure SQL Managed Instance, and PDW (In preview in Azure SQL Database):<br />E = External login or application from Microsoft Entra ID<br />X = External group from Microsoft Entra ID<br /><br />Available in SQL Server, Azure SQL Managed Instance, and PDW (not Azure SQL Database):<br />U = Windows login<br />G = Windows group<br /> C = Login mapped to a certificate<br />K = Login mapped to an asymmetric key|
|**type_desc**|**nvarchar(60)**|Description of the principal type:<br /><br /> SQL_LOGIN<br />SERVER_ROLE<br /><br />Available in SQL Server, Azure SQL Managed Instance, and PDW (In preview in Azure SQL Database):<br />EXTERNAL_LOGIN<br />EXTERNAL_GROUP<br /><br /> Available in SQL Server, Azure SQL Managed Instance, and PDW (not Azure SQL Database):<br />WINDOWS_LOGIN<br /> WINDOWS_GROUP<br /> CERTIFICATE_MAPPED_LOGIN<br />ASYMMETRIC_KEY_MAPPED_LOGIN|  
|**is_disabled**|**int**|1 = Login is disabled.<br />0 = Login is enabled.|
|**create_date**|**datetime**|Time at which the principal was created.|
|**modify_date**|**datetime**|Time at which the principal definition was last modified.|
|**default_database_name**|**sysname**|Default database for the principal.|
|**default_language_name**|**sysname**|Default language for the principal.|
|**credential_id**|**int**|ID of a credential associated with the principal. If no credential is associated with this principal, credential_id is NULL.|
|**owning_principal_id**|**int**|The **principal_id** of the owner of a server role. NULL if the principal is not a server role.|
|**is_fixed_role**|**bit**|Returns 1 if the principal is one of the built-in server roles with fixed permissions. For more information, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md).|

## Permissions

Any login can see their own login name, the system logins, and the fixed server roles. Viewing other logins requires ALTER ANY LOGIN, or a permission on the login. Viewing user-defined server roles requires ALTER ANY SERVER ROLE, or membership in the role.
 
In Azure SQL Database, only the following principals can see all logins:

- members of the server role **##MS_LoginManager##** or special database role **loginmanager** in `master`
- the Microsoft Entra admin and SQL server admin

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  

The following query lists the permissions explicitly granted or denied to server principals.  

```sql
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pe.state_desc, pe.permission_name   
FROM sys.server_principals AS pr   
JOIN sys.server_permissions AS pe   
    ON pe.grantee_principal_id = pr.principal_id;  
```

> [!IMPORTANT]
> The permissions of fixed server roles (other than public) do not appear in sys.server_permissions. Therefore, server principals may have additional permissions not listed here.

## Related content
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)  
