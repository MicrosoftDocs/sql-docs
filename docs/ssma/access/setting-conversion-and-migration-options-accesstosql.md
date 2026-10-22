---
title: "Setting Conversion and Migration Options (AccessToSQL)"
description: "Setting Conversion and Migration Options (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "conversion, setting options"
  - "migration options"
  - "modes"
  - "options, conversion settings"
  - "project settings"
  - "schemas"
---
# Set conversion and migration options (AccessToSQL)

You can set project-level options for each SQL Server Migration Assistant (SSMA) project. These options specify how objects are converted, how data is migrated, and how source data types map to target data types. Before you convert objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL or migrate data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, verify that the configuration options are appropriate for the project.

## Configuration options and modes

SSMA has four sets of configuration settings and four modes for configuring these settings: Default, Optimistic, Full, and Custom. The Default mode is recommended for most users. Use the Optimistic mode for simple conversions. Use the Full mode if you want to see all messages. In the Custom mode, you set the options.

The [User interface reference](user-interface-reference-accesstosql.md) describes the settings. For more information about the settings and how the settings are applied in each mode, see:

- [Project Settings (Conversion)](project-settings-conversion-accesstosql.md)
- [Project Settings (Migration)](project-settings-migration-accesstosql.md)
- [Project Settings (GUI)](project-settings-gui-accesstosql.md)
- [Project Settings (Type Mapping)](project-settings-type-mapping-accesstosql.md)
- [Project Settings (Azure SQL)](project-settings-azure-sql-db-accesstosql.md)

## Set project options

You can configure default settings for all projects in SSMA. These settings are saved to the SSMA configuration file and applied to any new project that you create.

1. On the **Tools** menu, select **Default Project Settings**.

1. In the **Default Project Settings** dialog box, complete one of the following steps:

   - Select the migration project type from the **Migration Target Version** dropdown list. To view or change settings, select **General** at the bottom of the left pane, and then select **Conversion**, **Migration**, or **Azure SQL**.

     > [!NOTE]  
     > The **Azure SQL** option is available in the **General** tab only if the project type is **Azure SQL**.

   - To select a predefined mode, select **Default**, **Optimistic**, or **Full** in the **Mode** dropdown list.

   - To specify a custom mode, select **Custom** in the **Mode** box. Select an option in the left pane, select the setting or value in the right pane, and then select or enter the new setting or value.

1. Select **OK** to save the settings.

You can also customize settings for the current project. SSMA saves these settings to the current project file.

1. On the **Tools** menu, select **Project Settings**.

1. In the **Project Settings** dialog box, complete one of the following steps:

   - To select a predefined mode, select **Default**, **Optimistic**, or **Full** in the **Mode** dropdown list.

   - To specify a custom mode, select **Custom** in the **Mode** box. Select an option in the left pane, select the setting or value in the right pane, and then select or enter the new setting or value.

1. Select **OK** to save the settings.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Map source and target data types](mapping-source-and-target-data-types-accesstosql.md)
- [Map source and target databases](mapping-source-and-target-databases-accesstosql.md)
- [Convert Access database objects](converting-access-database-objects-accesstosql.md)
