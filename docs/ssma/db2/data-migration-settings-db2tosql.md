---
title: "Data Migration Settings (Db2ToSQL)"
description: Data Migration Settings allow you to write custom queries for data migration, in SSMA for Db2.
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
# Data Migration Settings (Db2ToSQL)

Use the **Data Migration Settings** tab to write custom queries for data migration.

## Remarks

This tab is available when **Extended data migration options** is set to **Show** and is hidden when the setting is set to **Hide** in Project Settings. For more information about Project Migration Settings, see [Project Settings (Migration)](project-settings-migration-db2tosql.md) .

Parsing of custom SQL statements are implemented in **Data migration settings** tab of the Table Node.

The following checkboxes are available in the **Data Migration Settings** tab:

1. **Truncate SQL Server table:**

   This option allows the user to have a clear view of the migrated data at the target database.

   - By default, this textbox is checked.

   - If this textbox is unchecked, then the data that is migrated will be added on to the existing data at the target database.

1. **Use custom select:**

   This option allows the user to modify the **select** statement present (**select** statement allows the users to select the data to be displayed at the target database).

   1. By default, this textbox is unchecked.

   1. If this textbox is checked, then it allows the users to modify the **select** statement present.

There are two buttons present:

- Select **Apply** to apply the settings that have been changed.

- Select **Cancel** to restore the settings present before the changes were being made.

## Related content

- [Migrate Db2 Data into SQL Server (Db2ToSQL)](migrating-db2-data-into-sql-server-db2tosql.md)
