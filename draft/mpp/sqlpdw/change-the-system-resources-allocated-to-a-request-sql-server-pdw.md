---
title: "Change the System Resources Allocated to a Request (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2429826c-fd19-4d0a-a13e-342721ed5d19
caps.latest.revision: 4
author: BarbKess
---
# Change the System Resources Allocated to a Request (SQL Server PDW)
Describes how to figure out which resource class a SQL Server PDW request is running under, and then how to change the system resources for that request. Changing the resources for a request requires changing the resource class membership of the login submitting the request, by using the [ALTER SERVER ROLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-server-role-sql-server-pdw.md) statement.  
  
## Step 1: Determine the resource class for the login running the request.  
This query displays logins which are members of the resource class server role memberships. There are three resource classes, **mediumrc**, **largerc**, and **xlargerc**.  
  
> [!IMPORTANT]  
> This query must be executed by a login having **CONTROL SERVER** permission. If executed by a login without **CONTROL SERVER** permission, this query only returns the role memberships for the current login.  
  
```  
SELECT l.name AS [member], r.name AS [server role]  
FROM sys.server_role_members AS rm  
JOIN sys.server_principals AS l  
  ON l.principal_id = rm.member_principal_id  
JOIN  
  sys.server_principals AS r  
  ON r.principal_id = rm.role_principal_id  
WHERE  
  l.[type] = 'S'   
  AND r.[type] = 'R'  
  AND r.[name] in ('mediumrc', 'largerc', 'xlargerc');  
GO  
```  
  
If there are no logins that are members of a resource class server role, the resulting table will be empty. In this case, if the query returns a login named Ching, then when Ching submits a request, the request will receive the default system resources, which is smaller than the resource class system resources. If a login is a member of more than one resource class, the largest class has precedence.  
  
For a list of resource allocations for each resource class, see [Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md) topic.  
  
## Step 2: Run the request under a login with different resource class membership  
There are two ways to run a request with either larger or smaller system resources:  
  
-   Run the request under a different login that is a member of a larger or smaller resource class.  
  
-   Add the necessary login to one of the resource class roles. Choose this option with caution; changing the resource class for the login will change the system resource level for all requests submitted by the login.  
  
Suppose Ching is a member of the largerc server role. The following example shows how to add login Ching to the xlargerc server role.  
  
```  
ALTER SERVER ROLE xlargerc ADD MEMBER Ching;  
```  
  
Ching is now a member of the largerc and the xlargerc server roles. When Ching submits requests, the requests will receive the xlargerc system resources.  
  
The following example moves Ching back to the mediumrc server role.  To do this, she must be removed from xlargerc, and largerc server roles, and added to the mediumrc server role.  
  
```  
-- Move login Ching back to using medium system resources for requests.  
ALTER SERVER ROLE xlargerc DROP MEMBER Ching;  
ALTER SERVER ROLE largerc DROP MEMBER Ching;  
ALTER SERVER ROLE mediumrc ADD MEMBER Ching;  
```  
  
Ching is now a member of the mediumrc server role.  The following example changes Ching to have the default system resources for her requests.  
  
```  
-- Move login Ching to use the default system resources for requests.  
ALTER SERVER ROLE mediumrc DROP MEMBER Ching;  
```  
  
For more information about changing resource class role membership, see [ALTER SERVER ROLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-server-role-sql-server-pdw.md).  
  
## See Also  
[Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md)  
  
