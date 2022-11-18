---
title: "sys.database_principals (Transact-SQL)"
description: sys.database_principals (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "10/27/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "database_principals"
  - "database_principals_TSQL"
  - "sys.database_principals"
  - "sys.database_principals_TSQL"
helpviewer_keywords:
  - "sys.database_principals catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 8cb239e9-eb8c-4109-9cec-0d35de95fa0e
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_principals (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row for each security principal in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of principal, unique within the database.|  
|**principal_id**|**int**|ID of principal, unique within the database.|  
|**type**|**char(1)**|Principal type:<br /><br /> A = Application role<br /><br /> C = User mapped to a certificate<br /><br /> E = External user from Azure Active Directory<br /><br /> G = Windows group<br /><br /> K = User mapped to an asymmetric key<br /><br /> R = Database role<br /><br /> S = SQL user<br /><br /> U = Windows user<br /><br /> X = External group from Azure Active Directory group or applications|  
|**type_desc**|**nvarchar(60)**|Description of principal type.<br /><br /> APPLICATION_ROLE<br /><br /> CERTIFICATE_MAPPED_USER<br /><br /> EXTERNAL_USER<br /><br /> WINDOWS_GROUP<br /><br /> ASYMMETRIC_KEY_MAPPED_USER<br /><br /> DATABASE_ROLE<br /><br /> SQL_USER<br /><br /> WINDOWS_USER<br /><br /> EXTERNAL_GROUPS|  
|**default_schema_name**|**sysname**|Name to be used when SQL name does not specify a schema. Null for principals not of type S, U, or A.|  
|**create_date**|**datetime**|Time at which the principal was created.|  
|**modify_date**|**datetime**|Time at which the principal was last modified.|  
|**owning_principal_id**|**int**|ID of the principal that owns this principal. All fixed Database Roles are owned by **dbo** by default.|  
|**sid**|**varbinary(85)**|SID (Security Identifier) of the principal.  NULL for SYS and INFORMATION SCHEMAS.|  
|**is_fixed_role**|**bit**|If 1, this row represents an entry for one of the fixed database roles: db_owner, db_accessadmin, db_datareader, db_datawriter, db_ddladmin, db_securityadmin, db_backupoperator, db_denydatareader, db_denydatawriter.|  
|**authentication_type**|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Signifies authentication type. The following are the possible values and their descriptions.<br /><br /> 0 : No authentication<br />1 : Instance authentication<br />2 : Database authentication<br />3 : Windows authentication<br />4 : Azure Active Directory authentication|  
|**authentication_type_desc**|**nvarchar(60)**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Description of the authentication type. The following are the possible values and their descriptions.<br /><br /> `NONE` : No authentication<br />`INSTANCE` : Instance authentication<br />`DATABASE` : Database authentication<br />`WINDOWS` : Windows authentication<br />`EXTERNAL`: Azure Active Directory authentication|  
|**default_language_name**|**sysname**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Signifies the default language for this principal.|  
|**default_language_lcid**|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Signifies the default LCID for this principal.|  
|**allow_encrypted_value_modifications**|**bit**|**Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Suppresses cryptographic metadata checks on the server in bulk copy operations. This enables the user to bulk copy data encrypted using Always Encrypted, between tables or databases, without decrypting the data. The default is OFF. |      
  
## Remarks  
 The *PasswordLastSetTime* properties are available on all supported configurations of SQL Server, but the other properties are only available when SQL Server is running on Windows Server 2003 or later and both CHECK_POLICY and CHECK_EXPIRATION are enabled. See [Password Policy](../../relational-databases/security/password-policy.md) for more information.
The values of the principal_id may be reused in the case that principals have been dropped and therefore is not guaranteed to be ever-increasing.
  
## Permissions  
 Any user can see their own user name, the system users, and the fixed database roles. To see other users, requires ALTER ANY USER, or a permission on the user. To see user-defined roles, requires ALTER ANY ROLE, or membership in the role.  
  
## Examples  
  
### A: Listing all the permissions of database principals  
 The following query lists the permissions explicitly granted or denied to database principals.  
  
> [!IMPORTANT]  
>  The permissions of fixed database roles do not appear in sys.database_permissions. Therefore, database principals may have additional permissions not listed here.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pr.authentication_type_desc, pe.state_desc, pe.permission_name  
FROM sys.database_principals AS pr  
JOIN sys.database_permissions AS pe  
    ON pe.grantee_principal_id = pr.principal_id;  
```  
  
### B: Listing permissions on schema objects within a database  
 The following query joins sys.database_principals and sys.database_permissions to sys.objects and sys.schemas to list permissions granted or denied to specific schema objects.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pr.authentication_type_desc, pe.state_desc,   
    pe.permission_name, s.name + '.' + o.name AS ObjectName  
FROM sys.database_principals AS pr  
JOIN sys.database_permissions AS pe  
    ON pe.grantee_principal_id = pr.principal_id  
JOIN sys.objects AS o  
    ON pe.major_id = o.object_id  
JOIN sys.schemas AS s  
    ON o.schema_id = s.schema_id;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C: Listing all the permissions of database principals  
 The following query lists the permissions explicitly granted or denied to database principals.  
  
> [!IMPORTANT]  
>  The permissions of fixed database roles do not appear in `sys.database_permissions`. Therefore, database principals may have additional permissions not listed here.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pr.authentication_type_desc, pe.state_desc, pe.permission_name  
FROM sys.database_principals AS pr  
JOIN sys.database_permissions AS pe  
    ON pe.grantee_principal_id = pr.principal_id;  
```  
  
### D: Listing permissions on schema objects within a database  
 The following query joins `sys.database_principals` and `sys.database_permissions` to `sys.objects` and `sys.schemas` to list permissions granted or denied to specific schema objects.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pr.authentication_type_desc, pe.state_desc,   
    pe.permission_name, s.name + '.' + o.name AS ObjectName  
FROM sys.database_principals AS pr  
JOIN sys.database_permissions AS pe  
    ON pe.grantee_principal_id = pr.principal_id  
JOIN sys.objects AS o  
    ON pe.major_id = o.object_id  
JOIN sys.schemas AS s  
    ON o.schema_id = s.schema_id;  
```  
  
## See Also  
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Contained Database Users - Making Your Database Portable](../../relational-databases/security/contained-database-users-making-your-database-portable.md)   
 [Connecting to SQL Database By Using Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview)  
  
