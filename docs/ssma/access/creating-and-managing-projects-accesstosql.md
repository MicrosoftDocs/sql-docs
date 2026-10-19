---
title: Creating and Managing Projects (AccessToSQL)
description: Learn how to create, customize, save, and open projects in SQL Server Migration Assistant (SSMA) for Access.
author: cwebb-msft
ms.author: chweb
ms.reviewer: niball
ms.date: 04/20/2026
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.workspacedialog.f1"
helpviewer_keywords:
  - "creating projects"
  - "new projects"
  - "opening projects"
  - "projects"
  - "projects, creating and managing"
  - "saving metadata"
  - "saving projects"
---
# Create and manage projects (AccessToSQL)

To migrate Access databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, you must first create a SQL Server Migration Assistant (SSMA) project. The project is a file that contains metadata about the Access databases that you want to migrate to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, metadata about the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL that receives the migrated objects and data, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection information, and project settings.

## Review default project settings

SSMA contains several options for converting and synchronizing database objects and for converting data. The default setting for these options is appropriate for many users. However, before you create a new SSMA project, you should review the options and, if you want to, change the default settings for all your new projects.

1. On the **Tools** menu, select **Default Project Settings**.

1. Select the project type in **Migration Target Version** dropdown list for which settings are to be viewed or changed, and then select the **General** tab.

1. In the left pane, select **Conversion**.

1. In the right pane, review the options. For more information about these options, see [Project Settings (Conversion)](project-settings-conversion-accesstosql.md).

1. Change options as necessary.

1. Repeat the previous steps for the **Migration**, **GUI**, and **Type Mapping** pages.

   - For information about migration options, see [Project Settings (Migration)](project-settings-migration-accesstosql.md).

   - For information about user interface options, see [Project Settings (GUI)](project-settings-gui-accesstosql.md).

   - For more information about data type mapping settings, see [Project Settings (Type Mapping)](project-settings-type-mapping-accesstosql.md).

   - For information about Azure SQL settings, see [Project Settings (Azure SQL)](project-settings-azure-sql-db-accesstosql.md).

> [!NOTE]  
> Azure SQL settings are available only when you select **Migration to Azure SQL** while creating a project.

## Create new projects

SSMA starts without loading a default project. To migrate data from Access databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, you must create a project.

1. On the **File** menu, select **New Project**.

   The **New Project** dialog box appears.

1. In the **Name** box, enter a name for your project.

1. In the **Location** box, enter or select a folder for the project

1. In the **Migration To** dropdown list, select one of the following versions, and then select **OK**:

   [!INCLUDE [ssma-target-versions](../includes/ssma-target-versions.md)]

SSMA creates the project file. You can now perform the next step of [adding one or more Access databases](adding-and-removing-access-database-files-accesstosql.md).

## Customize project settings

In addition to defining default project settings, which apply to all new SSMA projects, you can also customize the settings for each project. For more information, see [Set conversion and migration options](setting-conversion-and-migration-options-accesstosql.md).

When you customize data type mappings between source and target databases, you can define mappings at the project, database, or object level. For more information about type mapping, see [Map source and target data types](mapping-source-and-target-data-types-accesstosql.md).

## Save projects

When you save a project, SSMA persists the project settings, and optionally the database metadata, to the project file.

- On the **File** menu, select **Save Project**.

  If databases within the project have changed or haven't been converted, SSMA prompts you to save metadata into the project. Saving metadata lets you work offline. It also lets you send a complete project file to other people, including technical support personnel. If you're prompted to save metadata, do the following steps:

  1. For each database that shows a status of **Metadata missing**, select the check box next to the database name.

     Saving metadata might take several minutes. If you don't want to save metadata at this point, don't select any check boxes.

  1. Select **Save**.

     SSMA parses the Access schemas and saves the metadata to the project file.

## Open projects

When you open a project, it's disconnected from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. This lets you work offline. To update metadata load database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. To migrate data, you must reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

1. Use one of the following procedures:

   - On the **File** menu, point to **Recent Projects**, and then select the project you want to open.

   - On the **File** menu, select **Open Project**, locate the `.a2ssproj` project file, select the file, and then select **Open**.

1. To reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], on the **File** menu, select **Reconnect to SQL Server**.

1. To reconnect to Azure SQL, on the **File** menu, select **Reconnect to Azure SQL**.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Add and remove Access database files](adding-and-removing-access-database-files-accesstosql.md)
