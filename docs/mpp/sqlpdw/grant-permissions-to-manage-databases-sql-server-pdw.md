---
title: "Grant Permissions to Manage Databases (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 84437c5d-e55b-403d-b687-0ed5e1ce387a
caps.latest.revision: 13
author: BarbKess
---
# Grant Permissions to Manage Databases (SQL Server PDW)
This topic describes how to grant permissions to a database user to manage a database on the SQL Server PDW appliance.  
  
## <a name="PermsAdminConsole"></a>Grant Permissions to Manage Databases  
In some situations, a company assigns a manager for a database. The manager controls the access that other logins have to the database, as well as the data and objects in the database. To manage all objects, roles, and users in a database grant the user the **CONTROL** permission on the database. The following statement grants the **CONTROL** permission on the **AdventureWorksPDW2012** database to the user `KimAbercrombie`.  
  
```Transact-SQL  
USE AdventureWorksPDW2012;  
GO  
GRANT CONTROL ON DATABASE:: AdventureWorksPDW2012 TO KimAbercrombie;  
```  
  
To grant someone the permission to control all the databases on the appliance, grant the **ALTER ANY DATABASE** permission in the master database.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
