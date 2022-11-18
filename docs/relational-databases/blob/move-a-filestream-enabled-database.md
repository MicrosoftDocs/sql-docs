---
title: "Move a FILESTREAM-enabled database"
description: Discover how to move a FILESTREAM-enabled database. See which Transact-SQL scripts to use in the Query Editor of SQL Server Management Studio.
ms.custom: ""
ms.date: "01/31/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], moving a FILESTREAM-enabled database"
author: MikeRayMSFT
ms.author: mikeray
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
  
 
## See also  

- [Attach a database](../databases/attach-a-database.md)
- [Detach a database](../databases/detach-a-database.md)  
- [Database Detach and Attach &#40;SQL Server&#41;](../databases/database-detach-and-attach-sql-server.md)    
- [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md) 
- [Configure File System Permissions for Database Engine Access](../../database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access.md)  

## Next steps

- [Manage metadata when making a database available on another server](../databases/manage-metadata-when-making-a-database-available-on-another-server.md)  
- [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)  
