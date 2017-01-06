---
title: "View Login Members of Each Resource Class (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b5f0e689-aef9-4aec-9212-d5ad2ee89737
caps.latest.revision: 6
author: BarbKess
---
# View Login Members of Each Resource Class (SQL Server PDW)
Describes how to display the login members of each resource class server role in SQL Server PDW. Use this query to figure out the class of resources allowed for requests submitted by each login.  
  
For resource class descriptions, see [Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md).  
  
## View Login Members of Each Resource Class  
This query displays the membership list for each resource class. There are three resource classes, mediumrc, largerc, and xlargerc.  
  
```  
SELECT l.name AS [member], r.name AS [server role]  
FROM sys.server_role_members AS rm  
JOIN sys.server_principals AS l  
  ON l.principal_id = rm.member_principal_id  
JOIN  
  sys.server_principals AS r  
  ON r.principal_id = rm.role_principal_id  
WHERE  
  ( l.[type] = 'S' OR l.[type] = 'U' OR l.[type] = 'G' )  
  AND r.[type] = 'R'  
  AND r.[name] in ('mediumrc', 'largerc', 'xlargerc');  
```  
  
If a login is not in this list, its requests will receive the default resources. If a login is a member of more than one resource class, the largest class has precedence.  
  
The resource allocations are listed in the [Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md) topic.  
  
