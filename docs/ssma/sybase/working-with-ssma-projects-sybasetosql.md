---
title: Work with SSMA Projects (SybaseToSQL)
description: Learn how to create, customize, save, and open projects in SQL Server Migration Assistant (SSMA) for SAP ASE.
author: cwebb-msft
ms.author: chweb
ms.reviewer: niball
ms.date: 04/20/2026
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Work with SSMA projects (SybaseToSQL)

To migrate SAP Adaptive Server Enterprise (ASE) databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you first create an SSMA project. The project is a file that contains metadata about the ASE databases that you want to migrate to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, metadata about the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure that will receive the migrated objects and data, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure connection information, and project settings.

When you open a project, it's disconnected from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. This lets you work offline. You can reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. For more information, see [Connecting to SQL Server](connecting-to-sql-server-sybasetosql.md) / [Connecting to Azure SQL Database](connecting-to-azure-sql-db-sybasetosql.md).

## Review default project settings

SSMA contains several options for converting and loading database objects, migrating data, and synchronizing SSMA with ASE and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. The default settings for these options are appropriate for many users. However, before you create a new SSMA project, you should review the options and, if you want to, change the defaults that will be used for all your new projects.

1. On the **Tools** menu, select **Default Project Settings**.

1. Select the project type in **Migration Target Version** dropdown list for which settings are required to be viewed or changed and then Select **General** tab.

1. In the left pane, select **Conversion**.

1. In the right pane, review the options, changing options as necessary. For more information about these options, see [Project Settings (Conversion)](project-settings-conversion-sybasetosql.md).

1. Repeat steps 1-3 for the Migration, SQL Azure, Loading Objects, GUI, and Type Mapping pages.

   - For information about migration options, see [Project Settings (Migration)](project-settings-migration-sybasetosql.md).

   - For information about options for loading objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Project Settings (Synchronization)](project-settings-synchronization-sybasetosql.md).

   - For more information about GUI options, see [Project Settings (GUI)](project-settings-gui-sybasetosql.md).

   - For more information about data type mapping settings, select [Project Settings (Type Mapping)](project-settings-type-mapping-sybasetosql.md).

   - For more information about SQL Azure options, see [Project Settings (Azure SQL Database)](project-settings-azure-sql-db-sybasetosql.md).

   > [!NOTE]  
   > The SQL Azure settings will be displayed only when you select **Migration to SQL Azure** while creating a project.

## Create new projects

To migrate data from ASE databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you must first create a project.

1. On the **File** menu, select **New Project**.

   The **New Project** dialog box appears.

1. In the **Name** box, enter a name for your project.

1. In the **Location** box, enter or select a folder for the project.

1. In the **Migration To** dropdown list, select the version of target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] used for migration. The options available are:

   [!INCLUDE [ssma-target-versions](../includes/ssma-target-versions.md)]

   And then select **OK**.

## Customize project settings

In addition to defining default project settings that apply to all new SSMA projects, you can customize the settings for each project. For more information, see [Setting Project Options](setting-project-options-sybasetosql.md).

When you customize data type mappings between source and target databases, you can define mappings at the project, database, or object level. For more information about type mapping, see [Mapping Sybase ASE and SQL Server Data Types](mapping-sybase-ase-and-sql-server-data-types-sybasetosql.md).

## Save projects

When you save a project, SSMA retains the project settings, and optionally the database metadata, to the project file.

- On the **File** menu, select **Save Project**.

  If databases within the project have changed or haven't been converted, SSMA will prompt you to save metadata into the project. Saving metadata will let you work offline and to send a complete project file to other people, including technical support personnel. If you're prompted to save metadata, do the following:

  1. For each database that shows a status of **Metadata missing**, select the check box next to the database name.

     Saving metadata might take several minutes. If you don't want to save metadata at this point, don't select any check boxes.

  1. Select the **Save** button.

     SSMA will parse the SAP ASE schemas and save the metadata to the project file.

## Open projects

When you open a project, it's disconnected from ASE and from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. This lets you work offline. To update metadata, load database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. To migrate data, you must reconnect to ASE and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.

1. Use one of the following procedures:

   - On the **File** menu, point to **Recent Projects**, and then select the project you want to open.

   - On the **File** menu, select **Open Project**, locate the .s2ssproj project file, select the file, and then select **Open**.

1. To reconnect to ASE, on the **File** menu, select **Reconnect to Sybase**.

1. To reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, on the **File** menu, select **Reconnect to SQL Server** / **Reconnect to SQL Azure**.

## Related content

- [Migrating SAP ASE databases to SQL Server - Azure SQL Database](migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)
- [Connecting to SAP ASE](connecting-to-sybase-ase-sybasetosql.md)
- [Connecting to SQL Server](connecting-to-sql-server-sybasetosql.md)
- [Connecting to Azure SQL Database](connecting-to-azure-sql-db-sybasetosql.md)
