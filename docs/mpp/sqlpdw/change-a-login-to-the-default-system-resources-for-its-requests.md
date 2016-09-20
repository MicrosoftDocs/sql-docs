---
title: "Change a login to the default system resources for its requests"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f13b84d3-7344-49b4-ac24-c49e299313de
caps.latest.revision: 3
author: BarbKess
---
# Change a login to the default system resources for its requests
Describes how to change the system resource allocations assigned to a SQL Server PDW login to the default amounts. This affects the system resources that SQL Server PDW assigns to requests submitted by the login.  
  
For resource class descriptions, see [Workload Management &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/workload-management-sql-server-pdw.md)  
  
## Use the default level of system resources  
When a login is not a member of any resource class server role, requests submitted by the login will receive the default amount of system resources.  
  
Suppose the login Matt is currently a member of all resource class server roles and wants to revert back to having his requests receive only the default resources.  The following example assigns the default resources to Matt’s requests by dropping his membership from all three resource class server roles.  
  
```  
--Give the requests submitted by Matt the default system resources   
--by dropping Matt from all resource class server roles.  
ALTER SERVER ROLE XLargeRC DROP MEMBER Matt;  
ALTER SERVER ROLE LargeRC DROP MEMBER Matt;  
ALTER SERVER ROLE MediumRC DROP MEMBER Matt;  
```  
  
