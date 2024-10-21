---
title: "Project Settings (GUI) (Db2ToSQL)"
description: Learn how to use the Project Settings dialog (Db2ToSQL).
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.projectsettingsgui.f1"
---
# Project Settings (GUI) (Db2ToSQL)

The GUI project settings let you configure how data appears on the **Data** tab and whether to show the assessment report after conversion or not.

The GUI pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.

- Use the **Project Settings** dialog box to set user interface options for the current project. To access the GUI settings, navigate to **Tools** > **Project Settings**, and then select **GUI** at the bottom of the left pane.

- Use the **Default Project Settings** dialog box to set user interface options for all projects. To access the GUI settings, navigate to **Tools** > **Default Project Settings**, select migration project type for which settings are required to be viewed or changed from **Migration Target Version** dropdown list, and then select **GUI** at the bottom of the left pane.

## Options

#### Maximum row number for source

Configures the number of rows of data displayed on the **Data** tab for the selected source table.

**Default**: 100

#### Maximum row number for target

Configures the number of rows of data displayed on the **Data** tab for the selected target table.

**Default**: 100

#### Show report after conversion

To display a report after you convert schemas, select True. The resulting Conversion Report contains the same layout and information as the Assessment Report.

**Default**: False
