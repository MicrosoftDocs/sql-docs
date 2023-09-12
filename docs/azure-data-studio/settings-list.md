---
title: User and Workspace settings
description: Descriptions for settings available in Azure Data Studio.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 08/28/2023
ms.service: azure-data-studio
ms.topic: how-to
---

# User and Workspace settings

Customizing your Azure Data Studio experience is easy, as described in [Modify User Settings](settings.md), but understanding the impact of each change requires knowledge of the setting you're modifying. Each setting has a short description in the Settings editor, with additional information provided on this page. Not every setting in Azure Data Studio is included; submit feedback for any missing entries.

## Editor: Detect Indentation

When enabled, the values for **Editor: Tab Size** and **Editor: Insert Spaces** are automatically detected when a file is opened based on the file contents.

## Editor: Font Family

The font used in the editor window can be changed based on your preference. The **Editor: Font Family** setting (`editor.fontFamily` entry in `settings.json`) is a text field where you can set your preferred font. If an invalid font is provided, the editor uses the default font.

- Windows default font: `Consolas, \'Courier New\', monospace`
- macOS default font: `Menlo, Monaco, \'Courier New\', monospace`
- Linux default font: `\'Droid Sans Mono\', \'monospace\', monospace`

Font family only affects text in the editor.

## Editor: Font Size

The font sized for the editor window can be controlled using the **Editor: Font Size** setting (`editor.fontSize` entry in `settings.json`). Enter a numeric value for the font size in pixels.

Font size only affects text in the editor. To increase the font size for the entire environment, use **Ctrl/Cmd +** to zoom in. To decrease the font size for the entire environment, use **Ctrl/Cmd -** to zoom out.

## Editor: Insert Spaces

When enabled, spaces are inserted when `Tab` is pressed. Enabled by default, the **Editor: Insert Spaces** setting (`editor.insertSpaces` entry in `settings.json`) is overridden based on the file contents when **Editor: Detect Indentation** is enabled.

## Editor: Tab Size

Controls the number of spaces to which a tab is equal. Change the **Editor: Tab Size** setting (`editor.tabSize` entry in `settings.json`) to the appropriate number of spaces, the default is four (4). This setting is overridden based on the file contents when **Editor: Detect Indentation** is enabled.

## Editor: Snippet Suggestions

Controls whether Snippets are displayed with other suggestions (for example, Intellisense, if enabled), and how they're sorted relative to other suggestions.

The **Editor: Snippet Suggestions** setting (`editor.snippetSuggestions` entry in `settings.json`) can have the values:

- `top` Show snippet suggestions on top of other suggestions.
- `bottom` Show snippet suggestions below other suggestions.
- `inline` Show snippet suggestions interspersed with other suggestions.
- `none` Do no show snippet suggestions.

Doesn't require **Mssql > Intelli Sense: Enable Suggestions** or **Mssql > Intelli Sense: Enable Intelli Sense** to be enabled.

## Editor > Suggest: Show Snippets

Determines whether Snippets are displayed in the editor window. Doesn't require **Mssql > Intelli Sense: Enable Suggestions** or **Mssql > Intelli Sense: Enable Intelli Sense** to be enabled.

## Execution Plan > Tooltips: Enable On Hover Tooltips

Determines whether tooltips are displayed on hover for an execution plan. When disabled, tooltips are shown on node select or F3 key press. The **Execution Plan > Tooltips: Enable On Hover Tooltips** option is disabled by default.

## Files: Auto Save

By default, Azure Data Studio requires an explicit action to save your changes to disk, **Ctrl+S**. You can enable Auto Save to automatically save your changes after a configured delay or when focus leaves the editor. With this option enabled, there's no need to explicitly save the file.

The **Files: Auto Save** setting (`files.autoSave` entry in `settings.json`) can have the values:

- `off` Disables auto save.
- `afterDelay` Saves files after a configured delay (default of 1000 ms).
- `onFocusChange` Saves files when focus moves out of the editor of the dirty file.
- `onWindowChange` Saves files when the focus moves out of the Azure Data Studio window.

## Files: Auto Save Delay

Controls the delay, in milliseconds, after which an editor with unsaved changes is saved automatically. This setting is only applicable when **Files: Auto Save** is set to `afterDelay`. The default is 1000 ms. You can optionally edit the `files.autoSaveDelay` entry in `settings.json`.

## Files: Hot Exit

Azure Data Studio remembers unsaved changes to files when you exit by default.

The **Files: Hot Exit** setting (`files.hotExit` entry in `settings.json`) can have the values:

- `off` disables hot exit
- `onExit` Hot exit is triggered when the application is closed, which occurs when the last window is closed on Windows/Linux or when the `workbench.action.quit` command is triggered from the Command Palette or keyboard shortcut. All windows without folders opened will be restored upon next launch.
- `onExitAndWindowClose` Hot exit is triggered when the application is closed, which occurs when the last window is closed on Windows/Linux or when the `workbench.action.quit` command is triggered from the Command Palette or keyboard shortcut, and also for any window with a folder opened regardless of whether it's the last window. All windows without folders opened will be restored upon next launch. To restore folder windows as they were before shutdown, set `window.restoreWindows` to all.

If there's a problem with hot exit, all backups are stored in the following folders for standard install locations:

- Windows %APPDATA%\Code\Backups
- macOS $HOME/Library/Application Support/Code/Backups
- Linux $HOME/.config/Code/Backups

## Mssql > Intelli Sense: Enable Intelli Sense

Determines whether Intellisense is enabled for the MSSQL provider, along with **Mssql > Intelli Sense: Enable Suggestions**. Both settings must be disabled (unchecked) to turn off Intellisense.

## Mssql > Intelli Sense: Enable Suggestions

Determines whether Intellisense suggestions are enabled for the MSSQL provider, along with **Mssql > Intelli Sense: Enable Intelli Sense**. Both settings must be disabled (unchecked) to turn off Intellisense.

## Query Editor: Tab color

To simplify identifying what connections you're working with, open tabs in the editor can have their colors set to match the color of the Server Group the connection belongs to. Tab colors are off by default.

The **Query Editor: Tab color** setting (`queryEditor.tabColorMode` entry in `settings.json`) can have the values:

- `off` Tab coloring is disabled.
- `border` The top border of each editor tab is colored to match the relevant server group.
- `fill` Each editor tab's background color matches the relevant server group.

:::image type="content" source="media/settings/settings-list-tab-color.png" alt-text="Screenshot of the query editor tab background color matching the server group color.":::

## Sql: Default Authentication Type

Determines the default authentication type to use when connecting to Azure resources.

The **Sql: Default Authentication Type** setting (`sql.defaultAuthenticationType` entry in `settings.json`) can have the values:

- `SqlLogin` Uses SQL authentication.
- `AzureMFA` Uses Azure Active Directory - Universal with MFA Support authentication.
- `AzureMFAandUser` Uses Azure Active Directory - Password authentication.
- `Integrated` Uses Windows authentication.

## Window: Restore Windows

Controls how windows are reopened after starting Azure Data Studio. This setting has no effect if the application is already running.

The **Window: Restore Windows** setting (`window.restoreWindows` entry in `settings.json`) can have the values:

- `preserve` Always reopen all windows. If a folder or workspace is opened (for example, from the command line), it opens as a new window unless it was opened before. If files are opened, they open in one of the restored windows.
- `all` Reopen all windows unless a folder, workspace or file is opened (for example, from the command line).
- `folders` Reopen all windows that had folders or workspaces opened unless a folder, workspace or file is opened (for example, from the command line).
- `one` Reopen the last active window unless a folder, workspace or file is opened (for example, from the command line).
- `none` Never reopen a window. Unless a folder or workspace is opened (for example, from the command line), an empty window appears.

## Workbench: Enable Preview Features

By default, preview features aren't enabled in Azure Data Studio. Enable this option to access unreleased preview features.

## Resources

Because Azure Data Studio inherits user settings functionality from Visual Studio Code, additional information about settings can be found in the [Settings for Visual Studio Code](https://code.visualstudio.com/docs/getstarted/settings) article.
