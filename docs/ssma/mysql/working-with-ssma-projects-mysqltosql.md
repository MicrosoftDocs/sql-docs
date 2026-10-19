---
title: Work with SSMA Projects (MySQLToSQL)
description: Learn how to create, customize, save, and open projects in SQL Server Migration Assistant (SSMA) for MySQL.
author: cwebb-msft
ms.author: chweb
ms.reviewer: niball
ms.date: 04/20/2026
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Working with SSMA projects, create new project"
  - "Working with SSMA projects, customize settings"
  - "Working with SSMA projects, Open project"
  - "Working with SSMA projects, Save project"
---
# Work with SSMA projects (MySQLToSQL)

To migrate MySQL databases to SQL Server or Azure SQL, you must first create a SQL Server Migration Assistant (SSMA) project. The project is a file that contains the following information:

- Metadata about the MySQL databases that you want to migrate to SQL Server or Azure SQL.

- Metadata about the target instance of SQL Server or Azure SQL that receive the migrated objects and data.

- SQL Server or Azure SQL connection information.

- Project settings.

When you open a project, it's disconnected from MySQL and SQL Server or Azure SQL. That lets you work offline. For more information about reconnecting to SQL Server, see [Connecting to SQL Server](connecting-to-sql-server-mysqltosql.md)

## Review default project settings

SSMA contains several settings for converting and loading database, migrating data, and synchronizing SSMA with MySQL and SQL Server or Azure SQL. The default settings are appropriate for many users. However, before you create a new SSMA project, you should review the settings. If necessary, you can change the default settings for all your new projects.

1. Select **Default Project Settings** from the **Tools** menu.

1. Select the project type in **Migration Target Version** dropdown list for which settings are to be viewed/ changed and then select **General** tab.

1. In the left pane, select **Conversion**.

1. In the right pane, review and change the settings as necessary. For more information about these settings, see [Project Settings (Conversion)](project-settings-conversion-mysqltosql.md) .

1. Repeat steps 1-3 for the Migration, Synchronization, Azure SQL, GUI, and Type Mapping pages.

- For information about migration settings, see [Project Settings (Migration)](project-settings-migration-mysqltosql.md).

- For information about settings for Synchronization to SQL Server, see [Project Settings (Synchronization)](project-settings-synchronization-mysqltosql.md).

- For information about GUI settings, see [Project Settings (GUI)](../sybase/project-settings-gui-sybasetosql.md).

- For information about data type mapping settings, see [Project Settings (Type Mapping)](project-settings-type-mapping-mysqltosql.md).

- For information about Azure SQL settings, see [Project Settings (Azure SQL Database)](project-settings-azure-sql-db-mysqltosql.md).

> [!NOTE]  
> The Azure SQL settings are displayed only when you select **Migration to Azure SQL** while creating a project.

## Create new projects

To migrate data from MySQL databases to SQL Server or Azure SQL, you must create a project.

1. Select **New Project** from the **File** menu. The **New Project** dialog box appears. On the **File** menu, select **New Project**. The **New Project** dialog box appears.

1. In the **Name** box, enter a name for your project.

1. In the **Location** box, enter or select a folder for the project.

1. In the **Migration To** dropdown list, select the version of target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] used for migration. The options available are:

   [!INCLUDE [ssma-target-versions](../includes/ssma-target-versions.md)]

   And then select **OK**.

SSMA creates the project file.

## Customize project settings

In addition to defining the default project settings that apply to all the new SSMA projects you can also customize the settings for each project. For more information, see [Setting Project Options](setting-project-options-mysqltosql.md).

When you customize data type mappings between the source and target databases, you can define mappings at the project, database, or object level. For more information, see [Mapping MySQL and SQL Server Data Types](mapping-mysql-and-sql-server-data-types-mysqltosql.md).

## Save projects

The Saving Projects feature allows the user to essentially save the project settings and, optionally, the database metadata to the SSMA project file.

1. On the **File** menu, select **Save** Project.

   If databases within the project have changed or haven't been converted, SSMA prompts you to load and save metadata. Loading and saving metadata lets you work offline. It also lets you send a complete project file to other people, such as technical support personnel. If you're prompted to save metadata, do the following:

1. For each database that shows a status of **Metadata missing**, select the check box next to the database name. Saving metadata might take several minutes. If you don't want to save metadata at this point, don't select any check boxes.

1. Select **Save**.

SSMA parses the MySQL schemas and saves the metadata to the project file.

## Open projects

When you open a project, it's disconnected from MySQL and from SQL Server or Azure SQL. This lets you work offline. To update metadata, load database objects into SQL Server or Azure SQL. To migrate data, you must reconnect to SQL Server or Azure SQL.

1. Use one of the following procedures:

   1. On the **File** menu, point to **Recent Projects**.

   1. Select the project you want to open.

   1. On the **File** menu, select **Open Project**, locate the `.m2ssproj` project file, select the file, and then select **Open**.

1. To reconnect to MySQL, on the **File** menu, select **Reconnect to MySQL**.

1. To reconnect to SQL Server, on the **File** menu, select **Reconnect to SQL Server**.

1. To reconnect to Azure SQL, on the **File** menu, select **Reconnect to Azure SQL.**

## Related content

- [Connecting to MySQL](connecting-to-mysql-mysqltosql.md)
- [Migrating MySQL Databases to SQL Server - Azure SQL Database](migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)
- [Connecting to SQL Server](connecting-to-sql-server-mysqltosql.md)
- [Connecting to Azure SQL Database](connecting-to-azure-sql-db-mysqltosql.md)
