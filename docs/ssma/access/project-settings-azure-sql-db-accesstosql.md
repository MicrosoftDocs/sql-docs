---
title: "Project Settings (Azure SQL Database) (AccessToSQL)"
description: "Project Settings (Azure SQL Database) (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.projectsettingssqlazure.f1"
helpviewer_keywords:
  - "Project Settings dialog box, Azure SQL"
  - "Azure SQL settings"
---
# Project Settings (Azure SQL) (AccessToSQL)

The Azure SQL project settings let you configure the Azure SQL Database suffix that appears in the connection dialog. You can also set up a heartbeat mechanism for an Azure SQL connection.

The Azure SQL pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.

- Use the **Project Settings** dialog box to set configuration options for the current project. To access the Azure SQL settings, on the **Tools** menu, select **Project Settings**, select **General** at the bottom of the left pane, and then select **Azure SQL**.

- Use the **Default Project Settings** dialog box to set configuration options for all projects. To access the Azure SQL settings, on the **Tools** menu, select **DefaultProject Settings**, select the project type as "Azure SQL" in **Migration Target Version** combo box to access the settings in Azure SQL pane, select **General** at the bottom of the left pane, and then select **Azure SQL**.

## Options

## Connectivity

#### Heartbeat Interval

Specifies a time interval to use for the heartbeat mechanism that keeps the Azure SQL connection alive, in `minutes:seconds` format.

**Default value**: `4:45`

Specify the value in `m:ss` format (for example, `4:45` or `0:50`).

#### Azure SQL Server Suffix

Specifies the Azure SQL server suffix.

**Default value**: `database.windows.net`
