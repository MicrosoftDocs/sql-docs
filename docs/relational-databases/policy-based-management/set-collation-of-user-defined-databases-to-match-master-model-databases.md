---
title: "Set the collation of user-defined databases to match master and model databases"
description:  Learn how to enable a policy to check if the collations of user-defined databases and system databases are the same.
ms.custom: seo-lt-2019
ms.date: "08/31/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
author: dzsquared
ms.author: drskwier
---
# Set the collation of user-defined databases to match master and model databases
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether user-defined databases are defined by using a database collation that is the same as the collation for master or model.
  
## Best practices recommendations  
 We recommend that the collations of user-defined databases match the collation of master or model. Otherwise, collation conflicts can occur that might prevent code from executing. For example, when a stored procedure joins one table to a temporary table, SQL Server might end the batch and return a collation conflict error if the collations of the user-defined database and the model database are different. This occurs because temporary tables are created in tempdb, which bases its collation on that of model.

  If you experience collation conflict errors, consider one of the following solutions:

  - Export the data from the user database and import it into new tables that have the same collation as the master and model databases.

  - Rebuild the system databases to use a collation that matches the user database collation. For more information about how to rebuild the system databases, see [Rebuild System Databases](../databases/rebuild-system-databases.md).

  - Modify any stored procedures that join user tables to tables in tempdb to create the tables in tempdb by using the collation of the user database. To do this, add the COLLATE database_default clause to the column definitions of the temporary table, as shown in the following example:
  
    ```
    CREATE TABLE #temp1 ( c1 int, c2 varchar(30) COLLATE database_default )
    ```

## See also
  
 [Set or Change the Server Collation](../collations/set-or-change-the-server-collation.md)  

 [Set or Change the Database Collation](../collations/set-or-change-the-database-collation.md)

 [Set or Change the Column Collation](../collations/set-or-change-the-column-collation.md)
 
 [View Collation Information](../collations/view-collation-information.md)    
  
  
