---
title: "DROP DATABASE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 19d0f26f-1dff-4881-bddc-c1c1c13f926a
caps.latest.revision: 38
author: BarbKess
---
# DROP DATABASE (SQL Server PDW)
Removes a user database in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP DATABASE database_name   
[;]  
```  
  
## Arguments  
*database_name*  
The name of the database to be removed. To display a list of databases on the appliance, use [sys.databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-databases-sql-server-pdw.md).  
  
## Permissions  
Requires the **CONTROL** permission on the database, or **ALTER ANY DATABASE** permission, or membership in the **db_owner** fixed database role.  
  
## Error Handling  
With regard to differences between SQL Server PDW and SQL Server, these products issue different errors when a user attempts to drop a database in use by the current session, and the user does not have the required permissions to drop the database. In this situation, SQL Server PDW issues error 3702 (in use) and SMP SQL Server issues 3701 (no permission).  
  
## General Remarks  
Dropping a database removes the database from an instance of SQL Server PDW and deletes the files used by the database.  
  
## Limitations and Restrictions  
You cannot drop a database that is open for reading or writing by any user, including the [USE (DW-SQL)](../sqlpdw/use-sql-server-pdw.md) statement.  
  
The DROP DATABASE statement cannot be run from within an explicit transaction. For information on explicit transactions, see [Transactions &#40;SQL Server PDW&#41;](../sqlpdw/transactions-sql-server-pdw.md).  
  
You cannot drop the system databases master and tempdb.  
  
## Locking  
Takes a shared lock on the DATABASE object.  
  
## Examples  
The following example removes the `Sales` database.  
  
```  
DROP DATABASE Sales;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/create-database-sql-server-pdw.md)  
[ALTER DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/alter-database-sql-server-pdw.md)  
  
