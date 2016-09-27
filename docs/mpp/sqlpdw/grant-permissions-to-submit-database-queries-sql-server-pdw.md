---
title: "Grant Permissions to Submit Database Queries (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e3fc8b81-0a71-4a59-896e-30eb9a5ccbf1
caps.latest.revision: 18
author: BarbKess
---
# Grant Permissions to Submit Database Queries (SQL Server PDW)
This topic describes how to grant permissions to database roles and users to query data on the SQL Server PDW appliance.  
  
## <a name="PermsAdminConsole"></a>Grant Permissions to Query Data  
The statements used to grant permissions to query data depend on the scope of access desired. The following SQL statements create a login named KimAbercrombie that can access the appliance, create a database user named KimAbercrombie in the **AdventureWorksPDW2012** database, create a database role named PDWQueryData, adds the use KimAbercrombie to the PDWQueryData role, and then show options for granting query access, based on whether the access is granted at the object, or database level.  
  
```  
USE master;  
GO  
  
CREATE LOGIN KimAbercrombie WITH PASSWORD = 'A2c3456$#' MUST_CHANGE,  
CHECK_EXPIRATION = ON,  
CHECK_POLICY = ON;  
GO  
  
USE AdventureWorksPDW2012;  
GO  
  
CREATE USER KimAbercrombie;  
GO  
  
CREATE ROLE PDWQueryData;  
GO  
  
EXEC sp_addrolemember 'PDWQueryData', 'KimAbercrombie'  
GO  
  
-- If the permission is granted against a table or view only, use this syntax:  
GRANT SELECT ON OBJECT::AdventureWorksPDW2012..DimEmployee TO PDWQueryData;  
GO  
  
-- If the permission is granted for the database, use this syntax:  
GRANT SELECT ON DATABASE::AdventureWorksPDW2012 TO PDWQueryData;  
GO  
  
-- If KimAbercrombie is the only user that needs to access a table, use this syntax:  
GRANT SELECT ON OBJECT::AdventureWorksPDW2012..DimEmployee TO KimAbercrombie;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
