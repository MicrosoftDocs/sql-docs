---
title: "Project Settings (Migration) (AccessToSQL)"
description: "Project Settings (Migration) (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
f1_keywords:
  - "ssma.access.projectsettingsmigration.f1"
helpviewer_keywords:
  - "Migration settings"
  - "Project Settings dialog box, Migration"
---
# Project Settings (Migration) (AccessToSQL)

The SQL Server Migration Assistant (SSMA) Migration project settings let you configure how data is migrated to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

The Migration pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.

- Use the **Project Settings** dialog box to set configuration options for the current project. To access the migration settings, on the **Tools** menu, select **Project Settings**, select **General** at the bottom of the left pane, and then select **Migration**.

- Use the **Default Project Settings** dialog box to set configuration options for all projects. To access the migration settings, on the **Tools** menu, select **Default Project Settings**, select the project type in the **Migration Target Version** combo box, select **General** at the bottom of the left pane, and then select **Migration**.

## Options

#### Check constraints

Specifies whether SSMA checks constraints when it adds data to tables.

- **Default Mode**: False
- **Optimistic Mode**: True
- **Full Mode**: False

#### Fire triggers

Specifies whether SSMA fires insertion triggers when it adds data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tables.

- **Default Mode**: False
- **Optimistic Mode**: True
- **Full Mode**: False

#### Keep identity

Specifies whether SSMA preserves Access identity values when it adds data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If this value is False, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] assigns identity values.

- **Default Mode**: True
- **Optimistic Mode**: True
- **Full Mode**: False

#### Keep nulls

Specifies whether SSMA preserves null values in the source data when it adds data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], regardless of existing default values in the target.

- **Default Mode**: True
- **Optimistic Mode**: False
- **Full Mode**: True

#### Table locks

Specifies whether SSMA locks tables when it adds data to tables during data migration. If the value is False, SSMA uses row locks.

- **Default Mode**: True
- **Optimistic Mode**: True
- **Full Mode**: True

#### Replace unsupported dates

Specifies whether SSMA should correct Access dates that are earlier than the earliest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] datetime date (January 1, 1753).

- To keep the current date values, select **Do nothing**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't accept dates before January 1, 1753, in a datetime column. If you use older dates, you must convert the datetime values to character values.

- To convert dates before January 1, 1753, to `NULL`, select **Replace with NULL**.

- To replace dates before January 1, 1753, with a supported date, select **Replace with nearest supported date**. If you select this value, by default nearest supported date will be selected as January 1, 1753.

#### Batch size

Batch size used during data migration. A transaction is logged after each batch. By default, the batch size for all schemes is 10,000.

## Related content

- [User interface reference](user-interface-reference-accesstosql.md)
