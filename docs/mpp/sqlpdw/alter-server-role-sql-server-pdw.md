---
title: "ALTER SERVER ROLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ce597b94-1e6b-4b7d-b239-05c250822ef9
caps.latest.revision: 43
author: BarbKess
---
# ALTER SERVER ROLE (SQL Server PDW)
Adds and removes logins from a server role in SQL Server PDW. Use this statement to manage the capabilities of logins.  
  
In this release, the built-in server roles are called resource classes. They are pre-configured to manage resource allocations for requests. For more information, see [Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md).  
  
User-defined server roles are not available in this release of SQL Server PDW. To define a role, use user-defined database roles instead. For more information, see [ALTER ROLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-role-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER SERVER ROLE  server_role_name  ADD MEMBER login;  
  
ALTER SERVER ROLE  server_role_name  DROP MEMBER login;  
```  
  
## Arguments  
*server_role_name*  
Specifies the name of the server role to be changed.  
  
In this release, the only valid values for *server_role_name*  are the resource classes **MediumRC**, **LargeRC**, and **XLargeRC**.  
  
To understand the details of resource classes and how to use them to manage resources, see [Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md).  
  
ADD MEMBER *login*  
Specifies the login to add to the server role.  
  
DROP MEMBER *login*  
Specifies to the login to drop from the server role.  
  
## Permissions  
Requires the **CONTROL SERVER** permission, or membership in one of the following server roles:  
  
-   **sysadmin**  
  
-   *server_role_name*. For example, a member of the XLargeRC server role can add a member to the XLargeRC server role.  
  
A login can drop itself from any of the server roles.  
  
## General Remarks  
SQL Server PDW uses resource classes for workload management. For more information, see [Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md).  
  
The **sa** login cannot be added to the special roles for resource classes.  
  
## Examples  
  
### A. Basic Syntax  
The following example adds the login `Anna` to the `LargeRC` server role.  
  
```  
ALTER SERVER ROLE LargeRC ADD MEMBER Anna;  
```  
  
### B. Remove a login from a resource class.  
The following example drops Anna’s membership in the `LargeRC` server role.  
  
```  
ALTER SERVER ROLE LargeRC DROP MEMBER Anna;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Definition Statements &#40;SQL Server PDW&#41;](../sqlpdw/data-definition-statements-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md)  
  
