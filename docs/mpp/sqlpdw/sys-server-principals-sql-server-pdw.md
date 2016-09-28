---
title: "sys.server_principals (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 862fd33a-abe5-4288-8b40-e9d434b534cf
caps.latest.revision: 10
author: BarbKess
---
# sys.server_principals (SQL Server PDW)
Contains a row for every server-level principal in SQL Server PDW.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**name**|**sysname**|Name of the principal. Is unique within a server.|  
|**principal_id**|**int**|ID number of the principal. Is unique within a server.|  
|**sid**|**varbinary(85)**|SID (Security-Identifier) of the principal. If Windows principal, then it matches Windows SID.|  
|**type**|**char(1)**|Principal type:<br /><br />S = SQL login<br /><br />U = Windows login<br /><br />G = Windows group<br /><br />R = Server role<br /><br />C = Login mapped to a certificate (Not available in PDW)<br /><br />K = Login mapped to an asymmetric key (Not available in PDW)|  
|**type_desc**|**nvarchar(60)**|Description of the principal type:<br /><br />SQL_LOGIN<br /><br />WINDOWS_LOGIN<br /><br />WINDOWS_GROUP<br /><br />SERVER_ROLE<br /><br />CERTIFICATE_MAPPED_LOGIN<br /><br />ASYMMETRIC_KEY_MAPPED_LOGIN|  
|**is_disabled**|**int**|1 = Login is disabled.|  
|**create_date**|**datetime**|Time at which the principal was created.|  
|**modify_date**|**datetime**|Time at which the principal definition was last modified.|  
|**default_database_name**|**sysname**|Default database for this principal.|  
|**default_language_name**|**sysname**|Default language for this principal.|  
|**credential_id**|**int**|ID of a credential associated with this principal. If no credential is associated with this principal, **credential_id** will be NULL.|  
|**owning_principal_id**||The **principal_id** of the owner of a server role. NULL if the principal is not a server role.|  
|**is_fixed_role**||Returns 1 if the principal is one of the fixed server roles.|  
  
## Permissions  
Any login can see their own login name, the system logins, and the fixed server roles. To see other logins, requires `ALTER ANY LOGIN`, or a permission on the login.  
  
## Examples  
The following query lists the permissions explicitly granted or denied to server principals.  
  
> [!IMPORTANT]  
> The permissions of fixed server roles do not appear in `sys.server_permissions`. Therefore, server principals may have additional permissions not listed here.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pe.state_desc, pe.permission_name   
FROM sys.server_principals AS pr   
JOIN sys.server_permissions AS pe   
    ON pe.grantee_principal_id = pr.principal_id;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
  
