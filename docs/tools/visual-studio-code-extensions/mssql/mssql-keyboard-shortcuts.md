---
title: Customize Keyboard Shortcuts
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to customize MSSQL keyboard shortcuts in Visual Studio Code by using keybindings.json and the mssql.shortcuts setting in settings.json.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: yoleichen
ms.date: 07/10/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: how-to
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Customize keyboard shortcuts

The MSSQL extension supports two mechanisms for keyboard shortcuts:

- **Visual Studio Code keybindings** (`keybindings.json`): Use these keybindings for Command Palette commands such as **Execute Query**, **Connect**, and **Disconnect**.
- **Result view shortcuts** (`mssql.shortcuts` in `settings.json`): Use these shortcuts for actions in the query results pane such as navigating grids, copying data, and saving results.

## Choose a configuration method

- Use the [MSSQL Database Management Keymap extension](#install-the-mssql-database-management-keymap-extension) if you want familiar Azure Data Studio or SSMS-style shortcuts.
- Use [Shortcuts Configuration (Preview)](#use-shortcuts-configuration-preview) for day-to-day shortcut changes.
- Use [Advanced: Manually customize Visual Studio Code keybindings](#advanced-manually-customize-visual-studio-code-keybindings) for direct control of `keybindings.json`.
- Use [Advanced: Manually customize result view shortcuts](#advanced-manually-customize-result-view-shortcuts) for direct control of `mssql.shortcuts` in `settings.json`.

## Install the MSSQL Database Management Keymap extension

If you're moving from SQL Server Management Studio (SSMS) or Azure Data Studio, install the [MSSQL Database Management Keymap](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql-database-management-keymap) companion extension to use familiar shortcuts.

:::image type="content" source="media/mssql-keyboard-shortcuts/key-map-extension.png" alt-text="Screenshot of the MSSQL Database Management Keymap extension page in Visual Studio Code." lightbox="media/mssql-keyboard-shortcuts/key-map-extension.png":::

The extension provides the following key bindings:

| Function | Windows/Linux | macOS |
| --- | --- | --- |
| Run query | <kbd>F5</kbd> | <kbd>F5</kbd> |
| Run current statement | <kbd>Ctrl</kbd>+<kbd>F5</kbd> | <kbd>Cmd</kbd>+<kbd>F5</kbd> |
| Open new SQL query editor | <kbd>Ctrl</kbd>+<kbd>N</kbd> | <kbd>Cmd</kbd>+<kbd>N</kbd> |
| Show query results pane | <kbd>Win</kbd>+<kbd>Shift</kbd>+<kbd>R</kbd> | <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>R</kbd> |
| Show connection list | <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>D</kbd> | <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>D</kbd> |
| Show estimated plan | <kbd>Ctrl</kbd>+<kbd>L</kbd> | <kbd>Cmd</kbd>+<kbd>L</kbd> |
| Toggle actual plan | <kbd>Ctrl</kbd>+<kbd>M</kbd> | <kbd>Cmd</kbd>+<kbd>M</kbd> |
| Cancel running query | <kbd>Alt</kbd>+<kbd>Pause</kbd>/<kbd>Break</kbd> | <kbd>Option</kbd>+<kbd>Pause</kbd>/<kbd>Break</kbd> |

> [!NOTE]  
> This keymap doesn't include user-customized key bindings from Azure Data Studio. Configure custom key bindings manually in MSSQL keyboard preference settings from the **Command Palette**. This extension overrides some default Visual Studio Code shortcuts for database management scenarios.

You can install this extension from the Azure Data Studio migration dialog, or directly from the [Visual Studio Code Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql-database-management-keymap).

## Use Shortcuts Configuration (Preview)

The MSSQL extension includes a **Shortcuts Configuration (Preview)** UI to view and configure keyboard shortcuts.

You can use this UI to work with both shortcut systems:

- [Visual Studio Code keybindings](#advanced-manually-customize-visual-studio-code-keybindings) (`keybindings.json`)
- [Result view shortcuts](#advanced-manually-customize-result-view-shortcuts) (`mssql.shortcuts` in `settings.json`)

Open the Shortcuts Configuration page from the MSSQL extension toolbar.

:::image type="content" source="media/mssql-keyboard-shortcuts/shortcuts-configuration-entry.png" alt-text="Screenshot of the MSSQL extension toolbar showing the Open Shortcuts Configuration button." lightbox="media/mssql-keyboard-shortcuts/shortcuts-configuration-entry.png":::

Open the **Extension Shortcuts** tab to view two keyboard shortcut sections:

- **Query Editor**: Shows the most commonly used [Visual Studio Code keybindings](#advanced-manually-customize-visual-studio-code-keybindings) for MSSQL commands, organized into groups such as **Query Execution**, **Connection**, and **Others**.
- **Result View**: Shows [Result view shortcuts](#advanced-manually-customize-result-view-shortcuts) for the query results pane, including groups such as **Navigation** and **Results**.

:::image type="content" source="media/mssql-keyboard-shortcuts/shortcuts-configuration-extension.png" alt-text="Screenshot of Shortcuts Configuration (Preview) in the MSSQL extension showing Query Editor and Result View shortcut groups." lightbox="media/mssql-keyboard-shortcuts/shortcuts-configuration-extension.png":::

## Advanced: Manually customize Visual Studio Code keybindings

For most shortcut changes, use [Shortcuts Configuration (Preview)](#use-shortcuts-configuration-preview).

Use this section for advanced, manual control of Visual Studio Code keybindings in `keybindings.json`.

To customize a shortcut:

1. Open the Keyboard Shortcuts editor:

   - macOS: <kbd>Cmd</kbd>+<kbd>K</kbd>, then <kbd>Cmd</kbd>+<kbd>S</kbd>
   - Windows and Linux: <kbd>Ctrl</kbd>+<kbd>K</kbd>, then <kbd>Ctrl</kbd>+<kbd>S</kbd>

1. Search for the command name, such as `mssql.newQuery`.
1. Select the pencil icon next to the command, and press the new key combination.
1. Press **Enter** to confirm.

You can also edit `keybindings.json` directly:

1. Open the **Command Palette**:

   - macOS: <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>P</kbd>
   - Windows and Linux: <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>P</kbd>

1. Select **Preferences: Open Keyboard Shortcuts (JSON)**.
1. Add your shortcut.

```json
[
    {
        "key": "ctrl+shift+n",
        "command": "mssql.newQuery",
        "when": "editorTextFocus && editorLangId == 'sql'"
    }
]
```

> [!NOTE]  
> You might override an existing shortcut. Check for conflicts in the Keyboard Shortcuts editor before you save your changes.

### MSSQL extension commands

The extension provides commands in the **Command Palette**.

The following table lists commonly used commands and default shortcuts:

| Command | Description | Windows/Linux | macOS |
| --- | --- | --- | --- |
| `MS SQL: Connect` | Connect to SQL Server, Azure SQL, or SQL database in Microsoft Fabric | <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>C</kbd> | <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>C</kbd> |
| `MS SQL: Disconnect` | Disconnect the current editor session | <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>D</kbd> | <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>D</kbd> |
| `MS SQL: Execute Query` | Run a query for the current active SQL document | <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>E</kbd> | <kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>E</kbd> |
| `MS SQL: Execute Selection or Current Statement` | Execute only the Transact-SQL statement under the cursor | None | None |
| `MS SQL: Cancel Query` | Cancel the running query | None | None |
| `MS SQL: New Query` | Open a new SQL query file | None | None |
| `MS SQL: Toggle SQLCMD Mode` | Enable or disable SQLCMD mode for the active SQL document | None | None |
| `MS SQL: Change Connection` | Change the connection for the active SQL document | None | None |
| `MS SQL: Change Database` | Change the database for the active SQL document | None | None |
| `MS SQL: Estimated Plan` | View the estimated query execution plan | None | None |
| `MS SQL: Toggle Actual Plan` | Toggle actual execution plan collection for SQL queries | None | None |
| `MS SQL: Copy All` | Copy all query result content | None | None |
| `MS SQL: Toggle Query Result Panel` | Show or hide the query result panel | None | None |

## Advanced: Manually customize result view shortcuts

For most shortcut changes, use [Shortcuts Configuration (Preview)](#use-shortcuts-configuration-preview).

Use this section for advanced, manual configuration of result view shortcuts in `mssql.shortcuts` (`settings.json`). These shortcuts apply only to the query results pane.

When you customize these shortcuts, use `ctrlcmd` to represent both key chords:

- <kbd>Ctrl</kbd> on Windows and Linux
- <kbd>Cmd</kbd> on macOS

For example, `ctrlcmd+c` maps to <kbd>Ctrl</kbd>+<kbd>C</kbd> on Windows and Linux, and to <kbd>Cmd</kbd>+<kbd>C</kbd> on macOS.

### Default configuration

```json
{
    "mssql.shortcuts": {
        "event.queryResults.switchToResultsTab": "ctrl+alt+r",
        "event.queryResults.switchToMessagesTab": "ctrl+alt+y",
        "event.queryResults.switchToQueryPlanTab": "ctrl+alt+e",
        "event.queryResults.prevGrid": "ctrlcmd+up",
        "event.queryResults.nextGrid": "ctrlcmd+down",
        "event.queryResults.switchToTextView": "",
        "event.queryResults.maximizeGrid": "",
        "event.queryResults.saveAsJSON": "",
        "event.queryResults.saveAsCSV": "",
        "event.queryResults.saveAsExcel": "",
        "event.queryResults.saveAsInsert": "",
        "event.resultGrid.copySelection": "ctrlcmd+c",
        "event.resultGrid.copyWithHeaders": "",
        "event.resultGrid.copyAllHeaders": "",
        "event.resultGrid.selectAll": "ctrlcmd+a",
        "event.resultGrid.copyAsCSV": "",
        "event.resultGrid.copyAsJSON": "",
        "event.resultGrid.copyAsInsert": "",
        "event.resultGrid.copyAsInClause": "",
        "event.resultGrid.changeColumnWidth": "alt+shift+s",
        "event.resultGrid.expandSelectionLeft": "shift+left",
        "event.resultGrid.expandSelectionRight": "shift+right",
        "event.resultGrid.expandSelectionUp": "shift+up",
        "event.resultGrid.expandSelectionDown": "shift+down",
        "event.resultGrid.openColumnMenu": "f3",
        "event.resultGrid.openFilterMenu": "",
        "event.resultGrid.moveToRowStart": "ctrlcmd+left",
        "event.resultGrid.moveToRowEnd": "ctrlcmd+right",
        "event.resultGrid.selectColumn": "ctrl+space",
        "event.resultGrid.selectRow": "shift+space",
        "event.resultGrid.toggleSort": "alt+shift+o"
    }
}
```

### Query results shortcut reference

| Shortcut event | Default binding | Description |
| --- | --- | --- |
| `event.queryResults.switchToResultsTab` | <kbd>Ctrl</kbd>+<kbd>Alt</kbd>+<kbd>R</kbd> | Switch to the Results tab |
| `event.queryResults.switchToMessagesTab` | <kbd>Ctrl</kbd>+<kbd>Alt</kbd>+<kbd>Y</kbd> | Switch to the Messages tab |
| `event.queryResults.switchToQueryPlanTab` | <kbd>Ctrl</kbd>+<kbd>Alt</kbd>+<kbd>E</kbd> | Switch to the Query Plan tab |
| `event.queryResults.prevGrid` | <kbd>Ctrl</kbd>/<kbd>Cmd</kbd>+<kbd>Up</kbd> | Go to the previous result grid |
| `event.queryResults.nextGrid` | <kbd>Ctrl</kbd>/<kbd>Cmd</kbd>+<kbd>Down</kbd> | Go to the next result grid |
| `event.queryResults.switchToTextView` | None | Switch results to text view |
| `event.queryResults.maximizeGrid` | None | Maximize or minimize the current grid |
| `event.queryResults.saveAsJSON` | None | Save results as JSON |
| `event.queryResults.saveAsCSV` | None | Save results as CSV |
| `event.queryResults.saveAsExcel` | None | Save results as Excel |
| `event.queryResults.saveAsInsert` | None | Save results as `INSERT` statements |

### Result grid shortcut reference

| Shortcut event | Default binding | Description |
| --- | --- | --- |
| `event.resultGrid.copySelection` | <kbd>Ctrl</kbd>/<kbd>Cmd</kbd>+<kbd>C</kbd> | Copy the current selection |
| `event.resultGrid.copyWithHeaders` | None | Copy the selection with column headers |
| `event.resultGrid.copyAllHeaders` | None | Copy all column headers |
| `event.resultGrid.selectAll` | <kbd>Ctrl</kbd>/<kbd>Cmd</kbd>+<kbd>A</kbd> | Select all data in the active grid |
| `event.resultGrid.copyAsCSV` | None | Copy the selection as CSV |
| `event.resultGrid.copyAsJSON` | None | Copy the selection as JSON |
| `event.resultGrid.copyAsInsert` | None | Copy the selection as `INSERT` statements |
| `event.resultGrid.copyAsInClause` | None | Copy the selection as an IN clause |
| `event.resultGrid.changeColumnWidth` | <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>S</kbd> | Adjust column width |
| `event.resultGrid.expandSelectionLeft` | <kbd>Shift</kbd>+<kbd>Left</kbd> | Expand selection to the left |
| `event.resultGrid.expandSelectionRight` | <kbd>Shift</kbd>+<kbd>Right</kbd> | Expand selection to the right |
| `event.resultGrid.expandSelectionUp` | <kbd>Shift</kbd>+<kbd>Up</kbd> | Expand selection upward |
| `event.resultGrid.expandSelectionDown` | <kbd>Shift</kbd>+<kbd>Down</kbd> | Expand selection downward |
| `event.resultGrid.openColumnMenu` | <kbd>F3</kbd> | Open the column context menu |
| `event.resultGrid.openFilterMenu` | None | Open the filter menu |
| `event.resultGrid.moveToRowStart` | <kbd>Ctrl</kbd>/<kbd>Cmd</kbd>+<kbd>Left</kbd> | Move to the start of the row |
| `event.resultGrid.moveToRowEnd` | <kbd>Ctrl</kbd>/<kbd>Cmd</kbd>+<kbd>Right</kbd> | Move to the end of the row |
| `event.resultGrid.selectColumn` | <kbd>Ctrl</kbd>+<kbd>Space</kbd> | Select the entire column |
| `event.resultGrid.selectRow` | <kbd>Shift</kbd>+<kbd>Space</kbd> | Select the entire row |
| `event.resultGrid.toggleSort` | <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>O</kbd> | Toggle sort on the current column |

## Related content

- [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md)
- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [Transition from Azure Data Studio](mssql-azure-data-studio-transition.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
