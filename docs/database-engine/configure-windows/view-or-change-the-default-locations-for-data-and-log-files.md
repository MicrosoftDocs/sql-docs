---
title: View or change the default locations for data and log files
description: "Find out how to view or change the default locations for SQL Server data files and log files. See how to protect the files with access control lists (ACLs)."
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/22/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "log files [SQL Server], changing default location"
  - "data files [SQL Server], changing default location"
---
# View or change the default locations for data and log files

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

The best practice for protecting your data files and log files is to ensure that they are protected by access control lists (ACLs). Set the ACLs on the directory root under which the files are created.

> [!NOTE]  
> These instructions are for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Windows only. To change the default locations for [!INCLUDE [sqlonlinux-md](../../includes/sqlonlinux-md.md)], see [Configure SQL Server on Linux with the mssql-conf tool](../../linux/sql-server-linux-configure-mssql-conf.md).

## Use SQL Server Management Studio

1. In Object Explorer, right-click on your server and select **Properties**.

1. In the left panel on that Properties page, select the **Database settings** tab.

1. In **Database default locations**, view the current default locations for new data files and new log files. To change a default location, enter a new default pathname in the **Data** or **Log** field, or select the browse button to find and select a pathname.

1. After changing the default locations, you must stop and start the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service to complete the change.

## Use Transact-SQL

> [!CAUTION]
> The following example uses an extended stored procedure to modify the server registry. Serious problems might occur if you modify the registry incorrectly. These problems might require you to reinstall the operating system. Microsoft cannot guarantee that these problems can be resolved. Modify the registry at your own risk.

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window. Replace the `<path_*>` values with the new locations you wish to place your data and log files, and then select **Execute**.

   ```sql
   USE [master];
   GO
   EXEC xp_instance_regwrite
       N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer',
       N'BackupDirectory',
       REG_SZ,
       N'<path_to_database_backup_files>'
   GO
   EXEC xp_instance_regwrite
       N'HKEY_LOCAL_MACHINE',
       N'Software\Microsoft\MSSQLServer\MSSQLServer',
       N'DefaultData',
       REG_SZ,
       N'<path_to_data_files>'
   GO
   EXEC xp_instance_regwrite
       N'HKEY_LOCAL_MACHINE',
       N'Software\Microsoft\MSSQLServer\MSSQLServer',
       N'DefaultLog',
       REG_SZ,
       N'<path_to_log_files>'
   GO
   ```

1. After changing the default locations, you must stop and start the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service to complete the change.

## Related content

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Create a database](../../relational-databases/databases/create-a-database.md)
