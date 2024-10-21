---
title: "Work with SSMA Projects (Db2ToSQL)"
description: Learn how to work with projects in SSMA for Db2.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Work with SSMA projects (Db2ToSQL)

To migrate Db2 databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you first create an SQL Server Migration Assistant (SSMA) project. The project is a file that contains the following information:

- Metadata about the Db2 databases you want to migrate to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Metadata about the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that receives the migrated objects and data.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection information.

- Project settings.

When you open a project, it's disconnected from Db2 and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. That lets you work offline. For information about reconnecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Connect to SQL Server](connecting-to-sql-server-db2tosql.md).

## Review default project settings

SSMA contains several settings for converting and loading database objects, migrating data, and synchronizing SSMA with Db2 and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The default settings are appropriate for many users. However, before you create a new SSMA project, you should review the settings. If you want to, you can change the default settings that are used for all your new projects.

1. Navigate to **Tools** > **Default Project Settings**.

1. Select the project type in **Migration Target Version** dropdown list for which settings are required to be viewed or changed and then Select **General** tab.

1. In the left pane, select **Conversion**.

1. In the right pane, review and change the settings as necessary. For more information about these settings, see [Project Settings (Conversion) (Db2ToSQL)](project-settings-conversion-db2tosql.md).

1. Repeat Steps 1 - 3 for the Migration, Synchronization, Loading System Objects, GUI, and Type Mapping pages.

   - For information about migration settings, see [Project Settings (Migration) (Db2ToSQL)](project-settings-migration-db2tosql.md).
   - For information about system object settings, see [Project Settings(Loading System objects) (Db2ToSQL)](project-settings-loading-system-objects-db2tosql.md).
   - For information about settings for synchronization to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Project Settings(Synchronization) (Db2ToSQL)](project-settings-synchronization-db2tosql.md).
   - For information about GUI settings, see [Project Settings (GUI) (Db2ToSQL)](project-settings-gui-db2tosql.md).
   - For information about data type mapping settings, see [Project Settings (Type Mapping) (Db2ToSQL)](project-settings-type-mapping-db2tosql.md).

## Create new projects

To migrate data from Db2 databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you must first create a project.

1. Navigate to **File** > **New Project**. The **New Project** dialog box appears.

1. In the **Name** box, enter a name for your project.

1. In the **Location** box, enter or select a folder for the project, and then select **OK**.

1. In the **Migration To** dropdown list, select the version of target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] used for migration. The options available are:

   - [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)]
   - [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)]
   - [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]
   - [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]
   - [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]
   - [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]
   - [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

## Customize project settings

In addition to defining default project settings that apply to all new SSMA projects, you can customize the settings for each project. For more information, see [Set Project Options (Db2ToSQL)](setting-project-options-db2tosql.md) and related sections.

When you customize data type mappings between source and target databases, you can define mappings at the project, database, or object level. For more information, see [Map Db2 and SQL Server Data Types (Db2ToSQL)](mapping-db2-and-sql-server-data-types-db2tosql.md).

## Save projects

When you save a project, SSMA retains the project settings, and optionally the database metadata, to the project file.

Navigate to **File** > **Save Project**.

If schemas in the project change, or aren't converted, SSMA prompts you to load and save metadata. Loading and saving metadata allows you to work offline. It also lets you send a complete project file to other people, such as technical support personnel. If you're prompted to save metadata, do the following steps:

1. For each schema that shows a status of **Metadata missing**, select the check box next to the database name.

   Saving metadata might take several minutes. If you don't want to save metadata yet, don't select any check boxes.

1. Select the **Save** button.

   SSMA parses the Db2 schemas and saves the metadata to the project file.

## Open projects

When you open a project, it's disconnected from Db2 and from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. That lets you work offline. To update metadata, load database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To migrate data, you must reconnect to Db2 and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Use one of the following procedures:

   - Navigate to **File** > **Recent Projects**, and then select the project you want to open.

   - Navigate to **File** > **Open Project**, locate the `.o2ssproj` project file, choose the file, and then select **Open**.

1. To reconnect to Db2, navigate to **File** > **Reconnect to Db2**.

1. To reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], navigate to **File** > **Reconnect to SQL Server**.

## Related content

- [Migrate Db2 Databases to SQL Server (Db2ToSQL)](migrating-db2-databases-to-sql-server-db2tosql.md)
- [Connect to Db2 database (Db2ToSQL)](connecting-to-db2-database-db2tosql.md)
- [Connect to SQL Server (Db2ToSQL)](connecting-to-sql-server-db2tosql.md)
