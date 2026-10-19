---
title: Data Migration Settings
description: Learn how to write custom queries for data migration in SQL Server Migration Assistant (SSMA).
author: cwebb-msft
ms.author: chweb
ms.date: 04/16/2026
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ai-usage: ai-assisted
---
# Data Migration Settings

Use the **Data Migration Settings** tab in SQL Server Migration Assistant (SSMA) to write custom queries for data migration.

## Remarks

This tab is available when **Extended data migration options** is set to **Show** and is hidden when the setting is set to **Hide** in the project migration settings.

Parsing of custom SQL statements is implemented in the **Data migration settings** tab of the Table Node.

The following checkboxes are available in the **Data Migration Settings** tab:

1. **Truncate SQL Server table**

   This option clears the target table before migration, giving you a clean view of the migrated data at the target database.

   - By default, this checkbox is checked.

   - If this checkbox is unchecked, the data that is migrated is added to the existing data at the target database.

1. **Use custom select**

   This option lets you modify the **select** statement that retrieves data to be migrated to the target database.

   1. By default, this checkbox is unchecked.

   1. If this checkbox is checked, you can modify the **select** statement.

There are two buttons:

- Select **Apply** to apply the settings that have been changed.

- Select **Cancel** to restore the settings to their previous values.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
