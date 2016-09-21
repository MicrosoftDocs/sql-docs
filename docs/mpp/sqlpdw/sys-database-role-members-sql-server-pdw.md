---
title: "sys.database_role_members (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b4cd0ea9-f660-4f3f-b66a-3b507aea8dae
caps.latest.revision: 9
author: BarbKess
---
# sys.database_role_members (SQL Server PDW)
Returns one row for each member of each database role for SQL Server PDW.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**role_principal_id**|**int**|Database Principal ID of the role.|  
|**member_principal_id**|**int**|Database Principal ID of the member.|  
  
## Permissions  
Any user can view their own role membership. To view other role memberships requires membership in the db_securityadmin fixed database role or VIEW DEFINITION on the database.  
  
## Examples  
The following query returns the members of the database roles.  
  
```  
SELECT DP1.name AS DatabaseRoleName,   
   isnull (DP2.name, 'No members') AS DatabaseUserName   
 FROM sys.database_role_members AS DRM  
 RIGHT OUTER JOIN sys.database_principals AS DP1  
   ON DRM.role_principal_id = DP1.principal_id  
 LEFT OUTER JOIN sys.database_principals AS DP2  
   ON DRM.member_principal_id = DP2.principal_id  
ORDER BY DP1.name;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
  
