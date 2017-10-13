---
title: Carbon User and Workspace Settings | Microsoft Docs
description: How to modify Carbon User and Workspace Settings.
services: sql-database
author: stevestein
ms.author: sstein
manager: craigg
ms.reviewer: achatter, alayu, erickang, sanagama, sstein
ms.service: data-tools
ms.workload: data-tools
ms.prod: NEEDED
ms.topic: article
ms.date: 10/05/2017
---
#  Carbon User and Workspace Settings

## Section here H2 

Carbon provides two different scopes for settings:

* **User** These settings apply globally to any instance of Carbon you open
* **Workspace** These settings are stored inside your workspace in a `.vscode`?? folder and only apply when the workspace is opened. Settings defined on this scope override the user scope.

## Creating User and Workspace Settings

The menu command **File** > **Preferences** > **Settings** (**Code** > **Preferences** > **Settings** on Mac) provides entry to configure user and workspace settings. You are provided with a list of Default Settings. Copy any setting that you want to change to the appropriate `settings.json` file. The tabs on the right let you switch quickly between the user and workspace settings files.

You can also open the user and workspace settings from the **Command Palette** (`kb(workbench.action.showCommands)`) with **Preferences: Open User Settings** and **Preferences: Open Workspace Settings** or use the keyboard shortcut (`kb(workbench.action.openGlobalSettings)`).




## Next steps
For information about Carbon, see [Carbon Overview](overview.md)