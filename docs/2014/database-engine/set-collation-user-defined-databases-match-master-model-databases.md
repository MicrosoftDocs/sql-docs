---
title: "Set the Collation of User-defined Databases to Match Those of the master and model Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: c686446f-dae1-4b05-a3df-837b3422988d
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Set the Collation of User-defined Databases to Match Those of the master and model Databases
  This rule checks whether user-defined databases are defined by using a database collation that is the same as the collation for master or model.  
  
## Best Practices Recommendations  
 We recommend that the collations of user-defined databases match the collation of master or model. Otherwise, collation conflicts can occur that might prevent code from executing. For example, when a stored procedure joins one table to a temporary table, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] might end the batch and return a collation conflict error if the collations of the user-defined database and the model database are different. This occurs because temporary tables are created in tempdb, which bases its collation on that of model.  
  
 If you experience collation conflict errors, consider one of the following solutions:  
  
-   Export the data from the user database and import it into new tables that have the same collation as the master and model databases.  
  
-   Rebuild the system databases to use a collation that matches the user database collation. For more information about how to rebuild the system databases, see [Rebuild System Databases](../relational-databases/databases/system-databases.md).  
  
-   Modify any stored procedures that join user tables to tables in tempdb to create the tables in tempdb by using the collation of the user database. To do this, add the `COLLATE database_default` clause to the column definitions of the temporary table, as shown in the following example:  
  
    ```  
    CREATE TABLE #temp1 ( c1 int, c2 varchar(30) COLLATE database_default )  
    ```  
  
## For More Information  
 [Set or Change the Database Collation](../relational-databases/collations/set-or-change-the-database-collation.md)  
  
 [Set or Change the Column Collation](../relational-databases/collations/set-or-change-the-column-collation.md)  
  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)  
  
 [COLLATE &#40;Transact-SQL&#41;](/sql/t-sql/statements/collations)  
  
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql)  
  
 [Microsoft Knowledge Base article 325335](https://go.microsoft.com/fwlink/?linkid=117751)  
  
 [How to: Install SQL Server 2008 from the Command Prompt](https://go.microsoft.com/fwlink/?LinkId=81585)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
