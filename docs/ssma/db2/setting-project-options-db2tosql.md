---
title: "Set Project Options (Db2ToSQL)"
description: Learn how to set project level options in SSMA for Db2.
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
# Set Project Options (Db2ToSQL)

For each SQL Server Migration Assistant (SSMA) project, you can set project level options. These options specify object conversion, object loading, user interface, and data migration settings. Before you convert objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or migrate data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], verify that the configuration options are appropriate for the project.

SSMA lets you configure default options for all projects. These options are applied to any new project that you create. You can then customize the options for each project.

## Configuration Options and Modes

SSMA has five sets of project settings:

- Project Information
- General (Conversion, Migration, Loading Objects)
- Synchronization
- GUI
- Type Mapping

It also has four modes for configuring these settings:

- Default
- Optimistic
- Full
- Custom

The Default mode is recommended for most users. The Optimistic mode keeps more of the current Db2 syntax, and is easier to read. However, keeping current syntax might not be accurate. If the Db2 syntax must be converted to equivalent [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax, the Full mode performs the most complete conversion, but the resulting code might be more difficult to read. In the Custom mode, you set the options.

For more information about the settings and how the settings are applied in each mode, see the following articles:

- [Project Settings (Conversion)](project-settings-conversion-db2tosql.md)
- [Project Settings (Migration)](project-settings-migration-db2tosql.md)
- [Project Settings(Synchronization)](project-settings-synchronization-db2tosql.md)
- [Project Settings (GUI)](project-settings-gui-db2tosql.md)
- [Project Settings (Type Mapping)](project-settings-type-mapping-db2tosql.md)

## Set project options

In SSMA, you can configure default settings for all projects. These settings are saved to the SSMA configuration file and applied to any new project that you create.

1. navigate to **Tools** > **Default Project Settings**.

1. In the **Default Project Settings** dialog box, use one of the following procedures:

   - Select migration project type for which settings are required to be viewed or changed from **Migration Target Version** dropdown list select **General** at the bottom of the left pane, and then select Conversion or Migration.

   - To select a predefined mode, in the **Mode** dropdown list box, select **Default**, **Optimistic**, or **Full**.

   - To specify custom settings, select or enter the new settings or values.

1. Select **OK** to save the settings.

You can also customize settings for the current project. These settings are saved to the current project file.

1. navigate to **Tools** > **Project Settings**.

1. In the **Project Settings** dialog box, use one of the following procedures:

   - To select a predefined mode, in the **Mode** dropdown list box, select **Default**, **Optimistic**, or **Full**.

   - To specify a custom mode, in the **Mode** box, select **Custom**, and then select the appropriate project settings.

1. Select **OK** to save the settings.

## Related content

- [Map Db2 and SQL Server Data Types (Db2ToSQL)](mapping-db2-and-sql-server-data-types-db2tosql.md)
- [Convert Db2 schemas (Db2ToSQL)](converting-db2-schemas-db2tosql.md)
