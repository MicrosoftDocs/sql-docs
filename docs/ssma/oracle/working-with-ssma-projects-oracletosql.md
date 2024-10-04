---
title: "Working with SSMA Projects (OracleToSQL)"
description: Learn how to create an SSMA project that contains metadata for Oracle databases to migrate and SQL Server, along with settings and connection information.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.oracle.workplacedialog.f1"
helpviewer_keywords:
  - "Customizing Project Settings"
---
# Work with SSMA Projects (OracleToSQL)

To migrate Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you first create an SSMA project. The project is a file that contains the following information:

- Metadata about the Oracle databases you want to migrate to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- Metadata about the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that receive the migrated objects and data.
- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection information.
- Project settings.

When you open a project, it is disconnected from Oracle and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. That lets you work offline. For information about reconnecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Connecting to SQL Server (OracleToSQL)](connecting-to-sql-server-oracletosql.md).

## Review default project settings

SSMA contains several settings for converting and loading database objects, migrating data, and synchronizing SSMA with Oracle and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The default settings are appropriate for many users. However, before you create a new SSMA project, you should review the settings. If you want to, you can change the default settings that will be used for all your new projects.

#### Review default project settings

1. On the **Tools** menu, select **Default Project Settings**.

1. Select the project type in **Migration Target Version** dropdown for which settings are required to be viewed or changed and then Select **General** tab.

1. In the left pane, select **Conversion**.

1. In the right pane, review and change the settings as necessary. For more information about these settings, see [Project Settings (Conversion) (OracleToSQL)](project-settings-conversion-oracletosql.md).

1. Repeat Steps 1-3 for the Migration, Synchronization, Loading System Objects, GUI, and Type Mapping pages.

   - For information about migration settings, see [Project Settings (Migration) (OracleToSQL)](project-settings-migration-oracletosql.md).

   - For information about system object settings, see [Project Settings(Loading System objects) (OracleToSQL)](project-settings-loading-system-objects-oracletosql.md).

   - For information about settings for synchronization to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Project Settings(Synchronization) (OracleToSQL)](project-settings-synchronization-oracletosql.md).

   - For information about GUI settings, see [Project Settings (GUI) (OracleToSQL)](project-settings-gui-oracletosql.md).

   - For information about data type mapping settings, see [Project Settings (Type Mapping) (OracleToSQL)](project-settings-type-mapping-oracletosql.md).

## Create new projects

To migrate data from Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you must first create a project.

#### Create a project

1. On the **File** menu, select **New Project**.

    The **New Project** dialog box appears.

1. In the **Name** box, enter a name for your project.

1. In the **Location** box, enter or select a folder for the project, and then select **OK**.

1. In the **Migration To** dropdown list, select the version of target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] used for migration. The options available are:

   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2005
   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2008
   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2014
   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2016
   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2017
   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2019
   - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2022
   - Azure SQL Database

## Customize project settings

In addition to defining default project settings that apply to all new SSMA projects, you can customize the settings for each project. For more information, see [Setting Project Options (OracleToSQL)](setting-project-options-oracletosql.md).

When you customize data type mappings between source and target databases, you can define mappings at the project, database, or object level. For more information, see [Mapping Oracle and SQL Server Data Types (OracleToSQL)](mapping-oracle-and-sql-server-data-types-oracletosql.md).

## Save projects

When you save a project, SSMA retains the project settings, and optionally the database metadata, to the project file.

#### Save a project

- On the **File** menu, select **Save Project**.

    If schemas in the project have changed or haven't been converted, SSMA prompts you to load and save metadata. Loading and saving metadata lets you work offline. It also lets you send a complete project file to other people, such as technical support personnel. If you're prompted to save metadata, do the following:

    1. For each schema that shows a status of **Metadata missing**, select the check box next to the database name.

        Saving metadata might take several minutes. If you don't want to save metadata yet, don't select any check boxes.

    1. Select the **Save** button.

        SSMA parses the Oracle schemas and saves the metadata to the project file.

## Open projects

When you open a project, it is disconnected from Oracle and from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. That lets you work offline. To update metadata, load database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To migrate data, you must reconnect to Oracle and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### Open a project

1. Use one of the following procedures:

   - On the **File** menu, point to **Recent Projects**, and then select the project you want to open.

   - On the **File** menu, select **Open Project**, locate the `.o2ssproj` project file, select the file, and then select **Open**.

1. To reconnect to Oracle, on the **File** menu, select **Reconnect to Oracle**.

1. To reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], on the **File** menu, select **Reconnect to SQL Server**.

## See also

- [Migrating Oracle Databases to SQL Server (OracleToSQL)](migrating-oracle-databases-to-sql-server-oracletosql.md)
- [Connecting to Oracle Database (OracleToSQL)](connecting-to-oracle-database-oracletosql.md)
- [Connecting to SQL Server (OracleToSQL)](connecting-to-sql-server-oracletosql.md)

## Next steps

- The next step in the migration process is to [Connecting to Oracle Database (OracleToSQL)](./connecting-to-oracle-database-oracletosql.md).
