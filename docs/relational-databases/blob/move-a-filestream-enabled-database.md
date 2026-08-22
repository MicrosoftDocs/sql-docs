---
title: "Move a FILESTREAM-enabled database"
description: Discover how to move a FILESTREAM-enabled database. See which Transact-SQL scripts to use in the Query Editor of SQL Server Management Studio.
author: markingmyname
ms.author: maghan
ms.date: "01/31/2022"
ms.service: sql
ms.subservice: filestream
ms.topic: how-to
helpviewer_keywords:
  - "FILESTREAM [SQL Server], moving a FILESTREAM-enabled database"
---
# Move a FILESTREAM-enabled database
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This article shows how to move a FILESTREAM-enabled database.  
  
> [!NOTE]  
>  The examples in this topic require the `Archive` database that is created in [Create a FILESTREAM-Enabled Database](../../relational-databases/blob/create-a-filestream-enabled-database.md).  
  
### To move a FILESTREAM-enabled database  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select **New Query** to open the Query Editor.  
  
2.  Copy the following [!INCLUDE[tsql](../../includes/tsql-md.md)] script into the Query Editor, and then select **Execute**. This script displays the location of the physical database files that the FILESTREAM database uses.  
  
    ```sql  
    USE [Archive] 
    GO  
    SELECT type_desc, name, physical_name from sys.database_files;
    ```  
  
3.  Copy the following [!INCLUDE[tsql](../../includes/tsql-md.md)] script into the Query Editor, and then select **Execute**. This code takes the `Archive` database offline.  
  
    ```sql  
    USE [master]
    EXEC sp_detach_db [Archive];
    GO  
    ```  
  
4.  Create the folder `C:\moved_location`, and then move the files and folders that are listed in step 2 into it.  
  
5.  Copy the following [!INCLUDE[tsql](../../includes/tsql-md.md)] script into the Query Editor, and then select **Execute**. This script sets the `Archive` database online.  
  
    ```sql  
    CREATE DATABASE [Archive] ON  
    PRIMARY ( NAME = Arch1,  
        FILENAME = 'c:\moved_location\archdat1.mdf'),  
    FILEGROUP FileStreamGroup1 CONTAINS FILESTREAM( NAME = Arch3,  
        FILENAME = 'c:\moved_location\filestream1')  
    LOG ON  ( NAME = Archlog1,  
        FILENAME = 'c:\moved_location\archlog1.ldf')  
    FOR ATTACH;
    GO  
    ```  
  
 
## Related content

- [Attach a Database](../databases/attach-a-database.md)
- [Detach a database](../databases/detach-a-database.md)
- [Database detach and attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Configure file system permissions for Database Engine access](../../database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access.md)
- [Manage Metadata When Making a Database Available on Another Server](../databases/manage-metadata-when-making-a-database-available-on-another-server.md)
- [ALTER DATABASE (Transact-SQL) compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
