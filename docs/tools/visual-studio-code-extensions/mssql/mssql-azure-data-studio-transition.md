---
title: Transition from Azure Data Studio to the MSSQL Extension for Visual Studio Code
description: Learn how to transition from Azure Data Studio to the MSSQL extension for Visual Studio Code by importing connections, connection groups, settings, and key bindings.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: tsiddique, roblescarlos
ms.date: 02/21/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Transition from Azure Data Studio (Preview)

The Azure Data Studio Migration feature in the MSSQL extension for Visual Studio Code helps you transition from Azure Data Studio to the MSSQL extension by importing existing connections, connection groups, and settings. This feature ensures a familiar and productive experience when you move to Visual Studio Code.

> [!TIP]
> Azure Data Studio Migration is currently in preview and might change based on feedback. Join our community at [GitHub Discussions](https://aka.ms/vscode-mssql-discussions) to share ideas or report issues.

## Features

Azure Data Studio Migration enables you to:

- Import saved database connections and connection groups from Azure Data Studio.
- Migrate supported editor and SQL-related settings.
- Install Azure Data Studio-style key bindings through the MSSQL Database Management Keymap companion extension.

## Import connections and settings

1. Open the Azure Data Studio Migration dialog from the **Command Palette** or from the changelog notification.

1. Select or browse to your **Azure Data Studio settings file**.

1. Choose the items to import:

   - **Connection groups**: Select which connection groups to bring over. Groups keep their names and color assignments.
   - **Connections**: Select individual connections to import. Connections keep server, database, and authentication metadata.
   - **Settings and key bindings**: Import supported editor and SQL-related settings from Azure Data Studio.

1. If prompted, install the **MSSQL Database Management Keymap** extension to apply Azure Data Studio-style key bindings.

1. Select **Import selected** to migrate the chosen items.

:::image type="content" source="media/mssql-azure-data-studio-transition/migration-dialog.png" alt-text="Screenshot of the Azure Data Studio Migration dialog showing connection groups, connections, and settings import options." lightbox="media/mssql-azure-data-studio-transition/migration-dialog.png":::

Imported connections appear in the **Connections** view, organized by their original connection groups.

## MSSQL Database Management key map

The MSSQL Database Management Keymap is a companion extension that provides Azure Data Studio key bindings for common database management tasks. Install it from the migration dialog or directly from the Visual Studio Code Marketplace.

:::image type="content" source="media/mssql-azure-data-studio-transition/key-map-extension.png" alt-text="Screenshot of the MSSQL Database Management Keymap extension page in Visual Studio Code." lightbox="media/mssql-azure-data-studio-transition/key-map-extension.png":::

The extension provides the following key bindings:

| Function | Windows / Linux | macOS |
| --- | --- | --- |
| Run query | <kbd>F5</kbd> | <kbd>F5</kbd> |
| Run current statement | <kbd>Ctrl</kbd>+<kbd>F5</kbd> | <kbd>Cmd</kbd>+<kbd>F5</kbd> |
| Open new SQL query editor | <kbd>Ctrl</kbd>+<kbd>N</kbd> | <kbd>Cmd</kbd>+<kbd>N</kbd> |
| Show query results pane | <kbd>Win</kbd>+<kbd>Shift</kbd>+<kbd>R</kbd> | <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>R</kbd> |
| Show connection list | <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>D</kbd> | <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>D</kbd> |
| Show estimated plan | <kbd>Ctrl</kbd>+<kbd>L</kbd> | <kbd>Cmd</kbd>+<kbd>L</kbd> |
| Toggle actual plan | <kbd>Ctrl</kbd>+<kbd>M</kbd> | <kbd>Cmd</kbd>+<kbd>M</kbd> |
| Cancel running query | <kbd>Alt</kbd>+<kbd>Break</kbd> | <kbd>Option</kbd>+<kbd>Break</kbd> |

> [!NOTE]
> This key map doesn't include user-customized key bindings from Azure Data Studio. You need to manually configure custom key bindings in the MSSQL keyboard preferences settings, accessible from the Command Palette. This extension overrides some default Visual Studio Code shortcuts for database management scenarios.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Quickstart: Connect to and query a database with the MSSQL extension for Visual Studio Code](connect-database-visual-studio-code.md)
- [Database operations (Preview)](mssql-database-operations.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
