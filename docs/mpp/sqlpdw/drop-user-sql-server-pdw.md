---
title: "DROP USER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ad80e07e-53d8-47ea-9514-67e929e5ac61
caps.latest.revision: 8
author: BarbKess
---
# DROP USER (SQL Server PDW)
Removes a user from the current SQL Server PDW database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP USER user_name  
```  
  
## Arguments  
*user_name*  
Specifies the name by which the user is identified inside this database.  
  
## Remarks  
Users that own securables cannot be dropped from the database. Before dropping a database user that owns securables, you must first drop or transfer ownership of those securables.  
  
The guest user cannot be dropped, but guest user can be disabled by revoking its **CONNECT** permission by executing `REVOKE CONNECT FROM GUEST` within any database other than master or tempdb.  
  
## Permissions  
Requires ALTER ANY USER permission on the database.  
  
## Locking  
Takes an exclusive lock on the SCHEMARESOLUTION and DATABASE objects.  
  
## Examples  
The following example removes database user `AbolrousHazem` from the `AdventureWorks2008R2` database.  
  
```  
USE AdventureWorks2008R2;  
DROP USER AbolrousHazem;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[ALTER USER &#40;SQL Server PDW&#41;](../sqlpdw/alter-user-sql-server-pdw.md)  
[CREATE USER &#40;SQL Server PDW&#41;](../sqlpdw/create-user-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
  
