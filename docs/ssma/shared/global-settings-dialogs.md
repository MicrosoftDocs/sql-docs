---
title: Global Settings (Dialogs)
description: Learn how to configure default dialog box and warning settings in SQL Server Migration Assistant (SSMA).
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
# Global Settings (Dialogs)

Use the Dialogs page of the **Global Settings** dialog box to specify the default user action and warning settings for SQL Server Migration Assistant (SSMA).

To access the dialog settings, navigate to **Tools** > **Global Settings**, select **GUI** at the bottom of the left pane, and then select **Dialogs**.

## Options

#### Warn before overwriting objects

When SSMA converts objects to SQL Server, some objects might already exist in the project's SQL Server metadata. These objects might already be converted, or the objects might simply have the same name within the target schema as objects you're going to convert.

Use this option to specify whether SSMA should prompt you for overwriting duplicate object definitions:

- If you select **True**, SSMA displays a warning dialog box when it encounters a duplicate object. In this dialog, you can specify to overwrite individual objects or all duplicate objects, or to skip individual objects or all duplicate objects.

- If you select **False**, the **Object overwrite default action** option appears where you specify the default action.

#### Object overwrite default action

This option appears if you select **False** for the **Warn before overwriting objects** option.

Use this option to specify the default object overwrite behavior:

- If you select **True**, SSMA automatically overwrites objects in the SQL Server project metadata that have the same name and are in the same target schema as the object to be converted.

- If you select **False**, SSMA doesn't overwrite object metadata during conversion.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
