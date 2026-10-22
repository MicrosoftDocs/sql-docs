---
title: Project Settings (GUI)
description: Learn how to configure user interface settings for your SQL Server Migration Assistant (SSMA) project.
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
# Project Settings (GUI)

The GUI project settings in SQL Server Migration Assistant (SSMA) let you configure how data appears on the **Data** tab and whether to show the assessment report after conversion.

The GUI pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.

- Use the **Project Settings** dialog box to set user interface options for the current project. To access the GUI settings, navigate to **Tools** > **Project Settings**, and then select **GUI** at the bottom of the left pane.

- Use the **Default Project Settings** dialog box to set user interface options for all projects. To access the GUI settings, navigate to **Tools** > **Default Project Settings**, select the migration project type from the **Migration Target Version** dropdown list, and then select **GUI** at the bottom of the left pane.

## Options

#### Maximum row number for source

Configures the number of rows of data displayed on the **Data** tab for the selected source table.

**Default**: 100

#### Maximum row number for target

Configures the number of rows of data displayed on the **Data** tab for the selected target table.

**Default**: 100

#### Show report after conversion

To display a report after you convert schemas, select **True**. The resulting Conversion Report contains the same layout and information as the Assessment Report.

**Default**: False

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
