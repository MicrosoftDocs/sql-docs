---
title: "master Database (SQL Server PDW)"
ms.custom: na
ms.date: 01/13/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c71617c0-6689-4f52-81c6-58f4cf7c7377
caps.latest.revision: 8
author: BarbKess
---
# master Database
The SQL Server PDW master database stores appliance-level login information and the database catalog. It is a SQL Server master database that resides on the Control node. As such, it provides similar functionality to SQL Server PDW as master provides to SQL Server.  
  
For more information about system databases, see [System Databases](system-databases.md).  
  
## Limitations and Restrictions  
The following list describes the operations you cannot perform on the SQL Server PDW master database.  
  
You *cannot:*  
  
-   Perform a differential backup of master.  
  
-   Create user objects in master.  
  
-   Change the collation of master.  
  
-   Change the owner of master.  
  
-   Drop or rename master.  
  
-   Modify permissions to master.  
  
-   Execute **DBCC SHRINKLOG**.  
  
## Related Tasks  
  
|Task|Description|  
|--------|---------------|  
|Create a full backup of master.|Example:<br /><br />`BACKUP DATABASE master TO backup_directory;`<br /><br />For more information, see [BACKUP DATABASE](../../docs/t-sql/statements/backup-database-parallel-data-warehouse.md).|  
|Restore the master database|To restore the master database, use the [Restore the Master Database](restore-the-master-database.md) page in the Configuration Manager tool.|  
|View database catalog information.|`SELECT * FROM master.sys.databases;`|  
|View system-wide login and permission information.|`SELECT * FROM master.sys.server_permissions;`<br /><br />`SELECT * FROM master.sys.server_principals;`<br /><br />`SELECT * FROM master.sys.sql_logins;`|  
  
<!-- MISSING LINKS 
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  
