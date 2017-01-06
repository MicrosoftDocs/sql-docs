---
title: "sys.database_principals (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a24b6c64-b535-4a44-8864-ecc06bb4e7fd
caps.latest.revision: 12
author: BarbKess
---
# sys.database_principals (SQL Server PDW)
Returns a row for each security principal in a database.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**name**|**sysname**|Name of principal, unique within the database.|  
|**principal_id**|**int**|ID of principal, unique within the database.|  
|**type**|**char(1)**|Principal type:<br /><br />S = SQL user<br /><br />U = Windows user<br /><br />G = Windows group<br /><br />A = Application role (Not available in PDW)<br /><br />R = Database role<br /><br />C = User mapped to a certificate (Not available in PDW)<br /><br />K = User mapped to an asymmetric key (Not available in PDW)|  
|**type_desc**|**nvarchar(60)**|Description of principal type.<br /><br />SQL_USER<br /><br />WINDOWS_USER<br /><br />WINDOWS_GROUP<br /><br />APPLICATION_ROLE (Not available in PDW)<br /><br />DATABASE_ROLE<br /><br />CERTIFICATE_MAPPED_USER (Not available in PDW)<br /><br />ASYMMETRIC_KEY_MAPPED_USER (Not available in PDW)|  
|**default_schema_name**|**sysname**|Name to be used when SQL name does not specify schema. Null for principals not of type S, U, or A.|  
|**create_date**|**datetime**|Time at which the principal was created.|  
|**modify_date**|**datetime**|Time at which the principal was last modified.|  
|**owning_principal_id**|**int**|ID of the principal that owns this principal. All principals except Database Roles must be owned by **dbo**.|  
|**sid**|**varbinary(85)**|SID (Security Identifier) of the principal.  NULL for SYS and INFORMATION SCHEMAS|  
|**is_fixed_role**|**bit**|If 1, then this row represents an entry for one of the fixed database roles: db_owner, db_accessadmin, db_datareader, db_datawriter, db_ddladmin, db_securityadmin, db_backupoperator, db_denydatareader, db_denydatawriter.|  
|**authentication_type**|**int**|Signifies authentication type:<br /><br />**0** = No authentication<br /><br />**1** = Instance authentication<br /><br />**2** = Database authentication<br /><br />**3** = Windows authentication|  
|**authentication_type_desc_**|**nvarchar(60)**|Description of the authentication type:<br /><br />**NONE** = No authentication<br /><br />**INSTANCE** = Instance authentication<br /><br />**DATABASE** = Database authentication<br /><br />**WINDOWS** = Windows authentication|  
|**Default_language_name**|**sysname**|Signifies the default language for this principal. Always NULL in SQL Server PDW.|  
|**Default_language_lcid**|**int**|Signifies the default LCID for this principal. Always NULL in SQL Server PDW.|  
  
## Permissions  
Any user can see their own user name, the system users, and the fixed database roles. To see other users, requires ALTER ANY USER, or a permission on the user. To see user-defined roles, requires ALTER ANY ROLE, or membership in the role.  
  
## Examples  
  
### A: Listing all the permissions of database principals  
The following query lists the permissions explicitly granted or denied to database principals.  
  
> [!IMPORTANT]  
> The permissions of fixed database roles do not appear in `sys.database_permissions`. Therefore, database principals may have additional permissions not listed here.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pr.authentication_type_desc, pe.state_desc, pe.permission_name  
FROM sys.database_principals AS pr  
JOIN sys.database_permissions AS pe  
    ON pe.grantee_principal_id = pr.principal_id;  
```  
  
### B: Listing permissions on schema objects within a database  
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
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[sys.server_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-server-principals-sql-server-pdw.md)  
[sys.sql_logins &#40;SQL Server PDW&#41;](../sqlpdw/sys-sql-logins-sql-server-pdw.md)  
  
