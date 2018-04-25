---
title: Microsoft SQL Operations Studio (preview) release notes| Microsoft Docs
description: 'Microsoft SQL Operations Studio (preview) release notes'
ms.custom: "tools|sos"
ms.date: "04/25/2018"
ms.prod: sql
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# SQL Operations Studio (preview) release notes

**[Download the April Public Preview](download.md)**


## April 2018 (April Public Preview)

release date: April 25, 2018  
version: 0.28.6

The *April Public Preview* contains bug fixes and improvements. 

- Improvements to the SQL Agent Preview extension.
- Improved large and protected file support for saving Admin protected and >256M files within SQL Operations Studio.
- Integrated Terminal splitting to work with multiple open terminals at once.
- Reduced installation on-disk file count foot print for faster installs and startup times.
- Continue to fix GitHub issues:
   - Fix [issue 37](https://github.com/Microsoft/sqlopsstudio/issues/37): When the chart viewer throws an error, unexpected behavior occurs.
   - Fix [issue 462](https://github.com/Microsoft/sqlopsstudio/issues/462): Feature Request: Option for Server Groups to be expanded by default.
   - Fix [issue 606](https://github.com/Microsoft/sqlopsstudio/issues/606): intellisense - Bad suggestion for 'update' command.
   - Fix [issue 967](https://github.com/Microsoft/sqlopsstudio/issues/967): Expect query plan when select XML showplan in the result grid.
   - Fix [issue 1023](https://github.com/Microsoft/sqlopsstudio/issues/1023): Add square brackets for ms_foreachdb call from flyfishingdba.
   - Fix [issue 1048](https://github.com/Microsoft/sqlopsstudio/issues/1048): Pre-login SSL/TLS handshake error.
   - Fix [issue 1050](https://github.com/Microsoft/sqlopsstudio/issues/1050): Clear insights view before showing error.
   - Fix [issue 1057](https://github.com/Microsoft/sqlopsstudio/issues/1057): Restore and new query actions in explorer-widget are broken.
   - Fix [issue 1068](https://github.com/Microsoft/sqlopsstudio/issues/1068): Dashboard Output windows pops-up with error message for Azure SQL Database.
   - Fix [issue 1069](https://github.com/Microsoft/sqlopsstudio/issues/1069): Connection Dialog shows Server Required error when initially displayed.
   - Fix [issue 1070](https://github.com/Microsoft/sqlopsstudio/issues/1070): Server Groups now require a double-click to expand.
   - Fix [issue 1072](https://github.com/Microsoft/sqlopsstudio/issues/1072): Select control background is semi-transparent.
   - Fix [issue 1115](https://github.com/Microsoft/sqlopsstudio/issues/1115): Fix all high contrast accessibility issues in SQL Operations Studio.
   - Fix [issue 1101](https://github.com/Microsoft/sqlopsstudio/issues/1101): Extension fails to upgrade "Download Manually" link goes to wrong location.
   - Fix [issue 1103](https://github.com/Microsoft/sqlopsstudio/issues/1103): V Scroll not working on Home Tab.
   - Fix [issue 1104](https://github.com/Microsoft/sqlopsstudio/issues/1104): SQL extension tabs stopped working.


A significant highlight for the April Public Preview is the Visual Studio Code 1.21 platform source code refresh. This brings in several updates to the core editor and workbench from the previous 1.19 sync point. Some examples include the following:

- [New Notifications UI](https://code.visualstudio.com/updates/v1_21#_new-notifications-ui) - Easily manage and review SQL Operations Studio notifications.
- [Integrated Terminal splitting](https://code.visualstudio.com/updates/v1_21#_split-terminals) - Work with multiple open terminals at once.
- [Save large and protected files](https://code.visualstudio.com/updates/v1_20#_save-files-that-need-admin-privileges) - Save Admin protected and >256M files within SQL Operations Studio.
- [Improved large file support](https://code.visualstudio.com/updates/v1_21#_text-buffer-improvements) - Text buffer optimizations for large files.
- [Improved Settings search](https://code.visualstudio.com/updates/v1_20#_settings-search) - Easily find the right setting with natural language search.
- [Global snippets](https://code.visualstudio.com/updates/v1_20#_global-snippets) - Create snippets you can use across all file types.
- [Explorer multi-selection](https://code.visualstudio.com/updates/v1_20#_multi-select-in-the-explorer) - Perform actions on multiple files at once.
- [Errors & warnings in Explorer](https://code.visualstudio.com/updates/v1_20#_error-indicators-in-the-explorer) - Quickly navigate to errors in your code base.
- [Drag & drop, copy & paste across windows](https://code.visualstudio.com/updates/v1_21#_better-drag-and-drop-support) - Move files across open SQL Operations Studio windows.
- [Git submodule support](https://code.visualstudio.com/updates/v1_20#_git-submodules) - Perform Git operations on nested Git repositories.
- [Terminal screen reader support](https://code.visualstudio.com/updates/v1_20#_screen-reader-support) - Integrated Terminal now has "Screen Reader Optimized" mode.
- [Centered editor layout](https://code.visualstudio.com/updates/v1_21#_centered-editor-layout) - Maximize your code viewing screen real estate.
- [Horizontal search results (preview)](https://code.visualstudio.com/updates/v1_21#_horizontal-search) - You can now view search results in a horizontal panel.

For additional details, checkout the [Visual Studio Code February Release Notes](https://code.visualstudio.com/updates/v1_21), and the [Visual Studio Code January Release Notes](https://code.visualstudio.com/updates/v1_20).

For more information, see the [Change Log](https://github.com/Microsoft/sqlopsstudio/blob/master/CHANGELOG.md).

## March 2018 (March Public Preview)

release date: March 28, 2018  
version: 0.27.3

The *March Public Preview* continues to address the top GitHub issues and is focused on improving our extensibility story. Specifically enabling Extension Manager, improving dashboard management, and providing SQL Agent and insights extensions. This release includes the following enhancements:

- Enhance the dashboard extensibility model to support tabbed insights and configuration panes.
   - Extension Manager enables simple acquisition of extensions.
   - Dashboard extensions for sp_whoisactive from [whoisactive.com](http://www.whoisactive.com).
   - For details, see [Extend the functionality of SQL Operations Studio](extensions.md).
- Add additional [extensibility APIs for connection and object explorer](https://github.com/Microsoft/sqlopsstudio/wiki/Extensibility-API) management.
- Continue to fix important customer impacting [GitHub issues](https://github.com/Microsoft/sqlopsstudio/issues).


## February 2018 (February Public Preview)

release date: February 15, 2018  
version: 0.26.7

The *February Public Preview* includes some feature suggestions and high-priority bug fixes. This release includes the following enhancements:

- Introducing Auto-Update Installation, which provides a notification when a new release is available to download 
- The Connection Dialog 'Database' field is now a dynamically populated drop-down list that will contain a list of databases populated from the specified server.
- Fix [issue 6](https://github.com/Microsoft/sqlopsstudio/issues/6): Keep connection and selected database when opening new query tabs.
- Fix [issue 22](https://github.com/Microsoft/sqlopsstudio/issues/22): 'Server Name' and 'Database Name' - Can these be drop downs instead of text boxes?
- Fix [issue 549](https://github.com/Microsoft/sqlopsstudio/issues/549): Silent/Very Silent Install results in application opening after installation.
- Fix [issue 481](https://github.com/Microsoft/sqlopsstudio/issues/481): Add "Check for updates" option.
- SQL Editor colorization and auto-completion fixes:
   - Fix [issue 584](https://github.com/Microsoft/sqlopsstudio/issues/584): Keyword "FULL" not highlighted by IntelliSense.
   - Fix [issue 345](https://github.com/Microsoft/sqlopsstudio/issues/345): Colorize SQL functions within the editor.
   - Fix [issue 300](https://github.com/Microsoft/sqlopsstudio/issues/300): [#tempData] latest "]" will display green color.
   - Fix [issue 225](https://github.com/Microsoft/sqlopsstudio/issues/225): Keyword color mismatch.
   - Fix [issue 60](https://github.com/Microsoft/sqlopsstudio/issues/60): Invalid sql syntax color highlighting when using temporary table in from clause.
- Introduce Connection extensibility API.
- VS Code Editor 1.19 integration.
- Update JustinPealing/html-query-plan component to pick-up several Query Plan viewer improvements.


## January 2018 (January Public Preview)

release date: January 17, 2018  
version: 0.25.4

The *January Public Preview* includes some feature suggestions and high-priority bug fixes. This release includes the following enhancements:

- Saved Server connections are available in the Connection Dialog.
- Enable Hot exit. Hot exit is off by default, to enable see [Hot exit setting](settings.md#hot-exit).
- Tab-coloring based on Server Group. Tab coloring is off by default, to enable see [Tab color setting](settings.md#tab-color).
- Change *Server name* to *Server* in the Connection Dialog.
- Fix broken *Run Current Query* command.
- Fix drag-and-drop breaking scripting bug.
- Fix incorrect pinned Start Menu icon.
- Fix missing Azure Account branding icon.


## December 2017 (December Public Preview)

release date: December 19, 2017  
version: 0.24.1

The *December Public Preview* includes several bugs fixes across all feature areas, as well as the following enhancements:

- Create Firewall Rule Dialog is now available to assist connecting to Azure SQL Database and Azure SQL Data Warehouse.
- Added Windows Setup, and Linux DEB and RPM installation packages.
- Manage Dashboard visual layout editor.
- *Script As Alter* and *Script As Execute* commands.
- *Run Current Query with Actual Plan* command.
- Integrate VS Code 1.18.1 editor platform.
- Enable Sideloading of VSIX Extension files.
- Support "GO N" batch iteration syntax.


## November 2017

release date: November 15, 2017  
version: 0.23.6

- Initial release of [!INCLUDE[name-sos](../includes/name-sos-short.md)].


## Next Steps

See one of the following quickstarts to get started:
- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Data Warehouse](quickstart-sql-dw.md)

Contribute to [!INCLUDE[name-sos](../includes/name-sos-short.md)]:
- [https://github.com/Microsoft/sqlopsstudio](https://github.com/Microsoft/sqlopsstudio)
