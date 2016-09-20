---
title: "sys.server_role_members (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2ffdd276-ce03-4d00-ac42-abc8929c555b
caps.latest.revision: 7
author: BarbKess
---
# sys.server_role_members (SQL Server PDW)
Returns one row for each member of each fixed server role in SQL Server PDW.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**role_principal_id**|**int**|Server-Principal ID of the role.|  
|**member_principal_id**|**int**|Server-Principal ID of the member.|  
  
## Permissions  
Logins can view their own server role membership and can view the principal_id’s of the members of the fixed server roles. To view all server role membership requires the **VIEW DEFINITION ON SERVER ROLE**.  
  
## Examples  
The following query returns the members of the server roles.  
  
```  
SELECT SP1.name AS ServerRoleName,   
 isnull (SP2.name, 'No members') AS LoginName  
 FROM sys.server_role_members AS SRM  
 RIGHT OUTER JOIN sys.server_principals AS SP1  
   ON SRM.role_principal_id = SP1.principal_id  
 LEFT OUTER JOIN sys.server_principals AS SP2  
   ON SRM.member_principal_id = SP2.principal_id  
 ORDER BY SP1.name;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
