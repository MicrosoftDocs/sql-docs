---
title: "sys.server_permissions (Transact-SQL)"
description: sys.server_permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "09/20/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.server_permissions_TSQL"
  - "sys.server_permissions"
  - "server_permissions"
  - "server_permissions_TSQL"
helpviewer_keywords:
  - "sys.server_permissions catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 7d78bf17-6c64-4166-bd0b-9e9e20992136
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.server_permissions (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Returns one row for each server-level permission.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**class**|**tinyint**|Identifies class of thing on which permission exists.<br /><br /> 100 = Server<br /><br /> 101 = Server-principal<br /><br /> 105 = Endpoint<br /><br /> 108 = Availability Group|  
|**class_desc**|**nvarchar(60)**|Description of class on which permission exists. One of the following values:<br /><br /> **SERVER**<br /><br /> **SERVER_PRINCIPAL**<br /><br /> **ENDPOINT**<br /><br /> **AVAILABILITY GROUP**|  
|**major_id**|**int**|ID of the securable on which permission exists, interpreted according to class. For most, this is just the kind of ID that applies to what the class represents. Interpretation for non-standard is as follows:<br /><br /> 100 = Always 0|  
|**minor_id**|**int**|Secondary ID of thing on which permission exists, interpreted according to class.|  
|**grantee_principal_id**|**int**|Server-principal-ID to which the permissions are granted.|  
|**grantor_principal_id**|**int**|Server-principal-ID of the grantor of these permissions.|  
|**type**|**char(4)**|Server permission type. For a list of permission types, see the next table.|  
|**permission_name**|**nvarchar(128)**|Permission name.|  
|**state**|**char(1)**|Permission state:<br /><br /> D = Deny<br /><br /> R = Revoke<br /><br /> G = Grant<br /><br /> W = Grant With Grant option|  
|**state_desc**|**nvarchar(60)**|Description of permission state:<br /><br /> DENY<br /><br /> REVOKE<br /><br /> GRANT<br /><br /> GRANT_WITH_GRANT_OPTION|  
  
|Permission type|Permission name|Applies to securable|  
|---------------------|---------------------|--------------------------|  
|AAES|ALTER ANY EVENT SESSION|SERVER|
|ADBO|ADMINISTER BULK OPERATIONS|SERVER|  
|AL|ALTER|ENDPOINT, LOGIN|  
|ALAA|ALTER ANY SERVER AUDIT|SERVER|
|ALAG|ALTER ANY AVAILABILITY GROUP|SERVER|
|ALCD|ALTER ANY CREDENTIAL|SERVER|  
|ALCO|ALTER ANY CONNECTION|SERVER|  
|ALDB|ALTER ANY DATABASE|SERVER|  
|ALES|ALTER ANY EVENT NOTIFICATION|SERVER|  
|ALHE|ALTER ANY ENDPOINT|SERVER|  
|ALLG|ALTER ANY LOGIN|SERVER|  
|ALLS|ALTER ANY LINKED SERVER|SERVER|  
|ALRS|ALTER RESOURCES|SERVER|
|ALSR|ALTER ANY SERVER ROLE|SERVER|  
|ALSS|ALTER SERVER STATE|SERVER|  
|ALST|ALTER SETTINGS|SERVER|  
|ALTR|ALTER TRACE|SERVER|  
|AUTH|AUTHENTICATE SERVER|SERVER|
|CADB|CONNECT ANY DATABASE|SERVER|  
|CL|CONTROL|ENDPOINT, LOGIN|  
|CL|CONTROL SERVER|SERVER|  
|CO|CONNECT|ENDPOINT|  
|COSQ|CONNECT SQL|SERVER|
|CRAC|CREATE AVAILABILITY GROUP|SERVER|  
|CRDB|CREATE ANY DATABASE|SERVER|  
|CRDE|CREATE DDL EVENT NOTIFICATION|SERVER|  
|CRHE|CREATE ENDPOINT|SERVER|
|CRSR|CREATE SERVER ROLE|SERVER|  
|CRTE|CREATE TRACE EVENT NOTIFICATION|SERVER|
|IAL|IMPERSONATE ANY LOGIN|SERVER|  
|IM|IMPERSONATE|LOGIN|  
|SHDN|SHUTDOWN|SERVER|
|SUS|SELECT ALL USER SECURABLES|SERVER|
|TO|TAKE OWNERSHIP|ENDPOINT|  
|VW|VIEW DEFINITION|ENDPOINT, LOGIN|  
|VWAD|VIEW ANY DEFINITION|SERVER|  
|VWDB|VIEW ANY DATABASE|SERVER|  
|VWSS|VIEW SERVER STATE|SERVER|  
|XA|EXTERNAL ACCESS|SERVER|
|XU|UNSAFE ASSEMBLY|SERVER|
  
## Permissions  
 Any user can see their own permissions. To see permissions for other logins, requires VIEW DEFINITION, ALTER ANY LOGIN, or any permission on a login. To see user-defined server roles, requires ALTER ANY SERVER ROLE, or membership in the role.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following query lists the permissions explicitly granted or denied to server principals.  
  
> [!IMPORTANT]  
> The permissions of fixed server roles do not appear in sys.server_permissions. Therefore, server principals may have additional permissions not listed here.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pe.state_desc, pe.permission_name   
FROM sys.server_principals AS pr   
JOIN sys.server_permissions AS pe   
    ON pe.grantee_principal_id = pr.principal_id;  
```  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Securables](../../relational-databases/security/securables.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)  
