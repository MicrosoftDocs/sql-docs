---
title: "sys.server_permissions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7faabe7e-467f-4894-9e0d-d98617f20cbc
caps.latest.revision: 11
author: BarbKess
---
# sys.server_permissions (SQL Server PDW)
Returns one row for each server-level permission in SQL Server PDW.  
  
> [!NOTE]  
> Fixed server roles are not available in this release. Assign permissions to user-defined database roles instead.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**class**|**tinyint**|Identifies class of thing on which permission exists.<br /><br />100 = Server<br /><br />101 = Server-principal<br /><br />105 = Endpoint (Not available in PDW)|  
|**class_desc**|**nvarchar(60)**|Description of class on which permission exists:<br /><br />SERVER<br /><br />SERVER_PRINCIPAL<br /><br />ENDPOINT (Not available in PDW)|  
|**major_id**|**int**|id of the securable on which permission exists, interpreted according to class. For most, this is just the kind of id that applies to what the class represents. Interpretation for non-standard is as follows:<br /><br />100 = Always 0|  
|**minor_id**|**int**|Secondary id of thing on which permission exists, interpreted according to class.|  
|**grantee_principal_id**|**int**|Server-principal-id to which the permissions are granted.|  
|**grantor_principal_id**|**int**|Server-principal-id of the grantor of these permissions.|  
|**type**|**char(4)**|Server permission type. For a list of permission types, see the next table.|  
|**permission_name**|**nvarchar(128)**|Permission name.|  
|**state**|**char(1)**|Permission state:<br /><br />D = Deny<br /><br />R = Revoke<br /><br />G = Grant<br /><br />W = Grant With Grant option|  
|**state_desc**|**nvarchar(60)**|Description of permission state:<br /><br />DENY<br /><br />REVOKE<br /><br />GRANT<br /><br />GRANT_WITH_GRANT_OPTION|  
  
|Permission type|Permission name|Applies to securable|  
|-------------------|-------------------|------------------------|  
|ADBO|ADMINISTER BULK OPERATIONS|SERVER|  
|AL|ALTER|LOGIN|  
|ALCO|ALTER ANY CONNECTION|SERVER|  
|ALDB|ALTER ANY DATABASE|SERVER|  
|ALES|ALTER ANY EVENT NOTIFICATION|SERVER|  
|ALLG|ALTER ANY LOGIN|SERVER|  
|ALSS|ALTER SERVER STATE|SERVER|  
|CL|CONTROL|LOGIN|  
|CL|CONTROL SERVER|SERVER|  
|COSQ|CONNECT SQL|SERVER|  
|CRDB|CREATE ANY DATABASE|SERVER|  
|VW|VIEW DEFINITION|LOGIN|  
|VWAD|VIEW ANY DEFINITION|SERVER|  
|VWDB|VIEW ANY DATABASE|SERVER|  
|VWSS|VIEW SERVER STATE|SERVER|  
  
## Permissions  
Any user can see their own permissions. To see permissions for other logins, requires VIEW DEFINITION, ALTER ANY LOGIN, or any permission on a login.  
  
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
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
[sys.database_permissions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-database-permissions-sql-server-pdw.md)  
[sys.server_principals &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-server-principals-sql-server-pdw.md)  
  
