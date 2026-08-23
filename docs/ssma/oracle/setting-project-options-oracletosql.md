---
title: "Set Project Options (OracleToSQL)"
description: Learn how to set project options for your SSMA projects.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Configuration Options and Modes"
---

# Set project options (OracleToSQL)

You can set project options for each Microsoft SQL Server Migration Assistant (SSMA) for Oracle project. Options specify object conversion, object loading, user interface, and data migration settings. Before you convert objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or migrate data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], verify that the configuration options are appropriate for the project.

By using SSMA, you can configure default options for all projects. Default options are applied to any new project that you create. You can then customize the options for each project.

## Configuration options and modes

SSMA has five sets of project settings:

- **Project Information**
- **General** (Conversion, migration, loading objects)
- **Synchronization**
- **GUI**
- **Type Mapping**

SSMA has four modes for configuring these settings:

- **Default**
- **Optimistic**
- **Full**
- **Custom**

We recommend the **Default** mode for most users. The **Optimistic** mode keeps more of the current Oracle syntax, and is easier to read. However, keeping current syntax might not be accurate. If the Oracle syntax is converted to equivalent [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax, the **Full** mode performs the most complete conversion, but the resulting code might be more difficult to read. In the **Custom** mode, you set the options.

For more information about settings and how they're applied in each mode, see the following articles:

- [Project Settings (Conversion)](project-settings-conversion-oracletosql.md)
- [Project Settings (Migration)](project-settings-migration-oracletosql.md)
- [Project Settings (Synchronization)](project-settings-synchronization-oracletosql.md)
- [Project Settings (GUI)](project-settings-gui-oracletosql.md)
- [Project Settings (Type Mapping)](project-settings-type-mapping-oracletosql.md)

## Set project options

In SSMA, you can configure default settings for all projects. These settings are saved to the SSMA configuration file and applied to any new project that you create.

## Set default project options

1. On the **Tools** menu, select **Default Project Settings**.

1. In the **Default Project Settings** dialog, use one of the following procedures:

   - Select migration project type for which settings are required to be viewed or changed from the **Migration Target Version** dropdown list and then select **General** at the bottom of the left pane. Then select **Conversion** or **Migration**.
   - To select a predefined mode, in the **Mode** dropdown list, select **Default**, **Optimistic**, or **Full**.
   - To specify custom settings, select or enter the new settings or values.

1. Select **OK** to save the settings.

You can also customize settings for the current project. These settings are saved to the current project file.

## Customize settings for the current project

1. On the **Tools** menu, select **Project Settings**.

1. In the **Project Settings** dialog, use one of the following procedures:

   - To select a predefined mode, in the **Mode** dropdown list, select **Default**, **Optimistic**, or **Full**.
   - To specify a custom mode, in the **Mode** box, select **Custom**, and then select the appropriate project settings.

1. Select **OK** to save the settings.

## Related content

- [Map Oracle and SQL Server data types](mapping-oracle-and-sql-server-data-types-oracletosql.md)
- [Convert Oracle schemas](converting-oracle-schemas-oracletosql.md)
