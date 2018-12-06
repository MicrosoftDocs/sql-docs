---
title: Workload Management Tasks - Analytics Platform System | Microsoft Docs
description: Workload management tasks in Analytics Platform System.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Workload Management Tasks in Analytics Platform System
Workload management tasks in Analytics Platform System.

## View Login Members of Each Resource Class
Describes how to display the login members of each resource class server role in SQL Server PDW. Use this query to figure out the class of resources allowed for requests submitted by each login.  
  
For resource class descriptions, see [Workload Management](workload-management.md).  
  
This query displays the membership list for each resource class. There are three resource classes, mediumrc, largerc, and xlargerc.  
  
```sql  
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
  
The resource allocations are listed in [Workload Management](workload-management.md).  
  
## Change the System Resources Allocated to a Request
Describes how to figure out which resource class a SQL Server PDW request is running under, and then how to change the system resources for that request. Changing the resources for a request requires changing the resource class membership of the login submitting the request, by using the [ALTER SERVER ROLE](../t-sql/statements/alter-server-role-transact-sql.md) statement.  
  
### Step 1: Determine the resource class for the login running the request.  
This query displays logins that are members of the resource class server role memberships. There are three resource classes, **mediumrc**, **largerc**, and **xlargerc**.  
  
> [!IMPORTANT]  
> This query must be executed by a login having **CONTROL SERVER** permission. If executed by a login without **CONTROL SERVER** permission, this query only returns the role memberships for the current login.  
  
```sql  
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
  
If there are no logins that are members of a resource class server role, the resulting table will be empty. In this case, if the query returns a login named Ching, then when Ching submits a request, the request will receive the default system resources, which are smaller than the resource class system resources. If a login is a member of more than one resource class, the largest class has precedence.  
  
For a list of resource allocations for each resource class, see [Workload Management](workload-management.md).  
  
### Step 2: Run the request under a login with different resource class membership  
There are two ways to run a request with either larger or smaller system resources:  
  
-   Run the request under a different login that is a member of a larger or smaller resource class.  
  
-   Add the necessary login to one of the resource class roles. Choose this option with caution; changing the resource class for the login will change the system resource level for all requests submitted by the login.  
  
Suppose Ching is a member of the largerc server role. The following example shows how to add login Ching to the xlargerc server role.  
  
```sql  
ALTER SERVER ROLE xlargerc ADD MEMBER Ching;  
```  
  
Ching is now a member of the largerc and the xlargerc server roles. When Ching submits requests, the requests will receive the xlargerc system resources.  
  
The following example moves Ching back to the mediumrc server role.  To change to the new role, the login must be removed from xlargerc, and largerc server roles, and added to the mediumrc server role.  
  
```sql  
-- Move login Ching back to using medium system resources for requests.  
ALTER SERVER ROLE xlargerc DROP MEMBER Ching;  
ALTER SERVER ROLE largerc DROP MEMBER Ching;  
ALTER SERVER ROLE mediumrc ADD MEMBER Ching;  
```  
  
Ching is now a member of the mediumrc server role.  The following example changes Ching to have the default system resources for requests.  
  
```sql  
-- Move login Ching to use the default system resources for requests.  
ALTER SERVER ROLE mediumrc DROP MEMBER Ching;  
```  
  
For more information about changing resource class role membership, see [ALTER SERVER ROLE](../t-sql/statements/alter-server-role-transact-sql.md).  

## Change a login to the default system resources for its requests
Describes how to change the system resource allocations assigned to a SQL Server PDW login to the default amounts. 
  
For resource class descriptions, see [Workload Management](workload-management.md)  
  
When a login is not a member of any resource class server role, requests submitted by the login will receive the default amount of system resources.  
  
Suppose the login Matt is currently a member of all resource class server roles and wants to revert back to having requests receive only the default resources.  The following example assigns the default resources to Matt's requests by dropping his membership from all three resource class server roles.  
  
```sql  
--Give the requests submitted by Matt the default system resources   
--by dropping Matt from all resource class server roles.  
ALTER SERVER ROLE XLargeRC DROP MEMBER Matt;  
ALTER SERVER ROLE LargeRC DROP MEMBER Matt;  
ALTER SERVER ROLE MediumRC DROP MEMBER Matt;  
```  
  
## Display the Number of Concurrency Slots Needed for a Waiting Request
Describes how to figure out the number of concurrency slots are needed by a request that is waiting to run on SQL Server PDW.  
  
For more information, see [Workload Management](workload-management.md).  
  
A request could be waiting too long without getting executed. One of the ways to troubleshoot the request is to look at the number of concurrency slots the request needs.  The following example shows the number of concurrency slots needed by each waiting request.  
  
```sql  
--Display the number of concurrency slots required   
--for each request that is waiting to run.  
SELECT request_id, concurrency_slots_used AS [Slots Needed], resource_class AS [Resource Class]  
FROM sys.dm_pdw_resource_waits;  
```  
  
  
## See Also  
[Workload Management](workload-management.md)  
  
