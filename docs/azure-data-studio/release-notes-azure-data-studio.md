---
title: Release notes
titleSuffix: Azure Data Studio
description: 'Azure Data Studio release notes'
ms.custom: "seodec18"
ms.date: "03/06/2019"
ms.prod: sql
ms.technology: azure-data-studio
ms.reviewer: "alayu; sstein"
ms.topic: conceptual
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Release notes for Azure Data Studio

**[Download and install the latest release!](download.md)**

## February 2019

February 13, 2019 &nbsp; / &nbsp; version: 1.4.5

&nbsp;

| Change | Details |
| :----- | :------ |
| Added **Admin pack for SQL Server** extension pack. | This makes it easier to install SQL Server admin-related extensions. This includes:<br/>&bull; &nbsp; [SQL Server Agent](sql-server-agent-extension.md?view=sql-server-2017)<br/>&bull; &nbsp; [SQL Server Profiler](https://docs.microsoft.com/sql/azure-data-studio/sql-server-profiler-extension.md?view=sql-server-2017)<br/>&bull; &nbsp; [SQL Server Import](sql-server-import-extension.md?view=sql-server-2017) |
| Added filtering extended event support in Profiler extension. | &nbsp; |
| Added Save as XML feature that can save T-SQL results as XML. | &nbsp; |
| Added Data-Tier Application Wizard improvements. | &bull; &nbsp; Added Generate script button<br/>&bull; &nbsp; Added view to give warnings of possible data loss during deployment. |
| Updates to the SQL Server 2019 Preview extension. | See [SQL Server 2019 Preview extension](sql-server-2019-extension.md?view=sql-server-ver15). |
| Results streaming enabled by default for long running queries. | &nbsp; |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/23?closed=1). |
| &nbsp; | &nbsp; |

## January 2019 Hotfix

January 16, 2019 &nbsp; / &nbsp; version: 1.3.9 &nbsp; / &nbsp; Hotfix release

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed a few issues discovered in 1.3.8. | See [January Hotfix Release, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/24?closed=1).<br/><br/>For detailed information, see:<br/>&bull; &nbsp; [Change Log, on GitHub](https://github.com/Microsoft/azuredatastudio/blob/master/CHANGELOG.md).<br/>&bull; &nbsp; [Releases, on GitHub](https://github.com/Microsoft/azuredatastudio/releases). |
| &nbsp; | &nbsp; |

## January 2019

January 09, 2019 &nbsp; / &nbsp; version: 1.3.8

&nbsp;

| Change | Details |
| :----- | :------ |
| Added a new user installer for Windows. | Unlike the existing system installer, the new user installer does not require administrator privileges. This also enables an easier upgrade experience for non-administrators. |
| Added Azure Active Directory Authentication support. | &nbsp; |
| Announcing Idera SQL DM Performance Insights (Preview). | &nbsp; |
| Data-Tier Application Wizard support in SQL Server Import extension. | &nbsp; |
| Update to the SQL Server 2019 Preview extension. | See [SQL Server 2019 Preview extension](sql-server-2019-extension.md?view=sql-server-ver15). |
| SQL Server Profiler improvements. | &nbsp; |
| Results Streaming for large queries (preview). | &nbsp; |
| Community extensions: sp_executesql to sql and New Database. | &nbsp; |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/19?closed=1). |
| &nbsp; | &nbsp; |

## November 2018

November 6, 2018 &nbsp; / &nbsp; version: 1.2.4

&nbsp;

| Change | Details |
| :----- | :------ |
| Update to the SQL Server 2019 Preview extension. | See [SQL Server 2019 Preview extension](sql-server-2019-extension.md?view=sql-server-ver15). |
| Introducing Paste the Plan extension. | &nbsp; |
| Introducing High Color queries extension, including SSMS editor theme. | &nbsp; |
| Fixes in SQL Server Agent, Profiler, and Import extensions. | &nbsp; |
| Fix .Net Core Socket KeepAlive issue causing dropped inactive connections on macOS. | &nbsp; |
| Upgrade SQL Tools Service to .Net Core 2.2 Preview 3 (for eventual AAD support). | &nbsp; |
| &nbsp; | &nbsp; |

### Bug Fixes, November 2018

- Fix [issue #2933](https://github.com/Microsoft/azuredatastudio/issues/2933): Connection lost to Azure SQL DB
- Fix [issue #2914](https://github.com/Microsoft/azuredatastudio/issues/2914): "Invalid argument" exception expanding OE database node
- Fix [issue #2935](https://github.com/Microsoft/azuredatastudio/pull/2935): Display multi-line messages correctly in query results
- Fix [issue #2906](https://github.com/Microsoft/azuredatastudio/pull/2906): Fix Edit Data document name when table name contains special characters
- Fix [issue #2929](https://github.com/Microsoft/azuredatastudio/issues/2929): Built in extension changelog says to check the VSCode Release Notes for changes
- Fix [issue #2719](https://github.com/Microsoft/azuredatastudio/issues/2719): High Contrast theme doubles/triples icons
- Fix [issue #3047](https://github.com/Microsoft/azuredatastudio/pull/3047): Add a command line interface for connecting to a SQL Server
- Fix [issue #3031](https://github.com/Microsoft/azuredatastudio/pull/3031): Add query plan theme support

## October 2018

October 29, 2018 &nbsp; / &nbsp; version: 1.1.4

&nbsp;

| Change | Details |
| :----- | :------ |
| Introducing the Azure Resource Explorer to browse Azure SQL Databases. | &nbsp; |
| Improve Object Explorer and Query Editor connectivity robustness. | &nbsp; |
| SQL Agent extensions improvements. | &nbsp; |
| Update to the SQL Server 2019 Preview extension. | See [SQL Server 2019 Preview extension](sql-server-2019-extension.md?view=sql-server-ver15). |
| &nbsp; | &nbsp; |

### Bug Fixes, October 2018

- Fix [issue #2717](https://github.com/Microsoft/azuredatastudio/issues/2717): XML Column result click formatting
- Fix [issue #2993](https://github.com/Microsoft/azuredatastudio/issues/2993): Width's Result windows is incomplete
- Fix [issue #2999](https://github.com/Microsoft/azuredatastudio/issues/2999): Could not load file System.Diagnostics.Tracing on Mac when connecting to DB
- Fix [issue #2851](https://github.com/Microsoft/azuredatastudio/issues/2851): TimeSeries chart does not render correctly
- Fix [issue #2996](https://github.com/Microsoft/azuredatastudio/issues/2996): Temp table loss due to sudden session change

For detailed information, see the [Change Log](https://github.com/Microsoft/azuredatastudio/blob/master/CHANGELOG.md), and [Releases](https://github.com/Microsoft/azuredatastudio/releases).

## September 2018 (GA Release)

September 24, 2018 &nbsp; / &nbsp; version: 1.0 &nbsp; / &nbsp; GA release

General Availability release of Azure Data Studio (formerly SQL Operations Studio).

&nbsp;

| Change | Details |
| :----- | :------ |
| Query Results Grid performance and UX improvements for large number of result sets. | &nbsp; |
| Visual Studio Code source code refresh from 1.23 to 1.26.1 with Grid Layout and Improved Settings Editor (preview). | &nbsp; |
| Accessibility improvements for screen reader, keyboard navigation and high-contrast. | &nbsp; |
| Added `Connection name` option to provide an alternative display name in the Servers view-let. | &nbsp; |
| &nbsp; | &nbsp; |

### Announcing the SQL Server 2019 Preview extension

&nbsp;

| Change | Details |
| :----- | :------ |
| Support for SQL Server 2019 preview features including [big data cluster](../big-data-cluster/big-data-cluster-overview.md) support. | Connect to the HDFS/Spark Gateway shipped with SQL Server 2019 preview.<br/><br/>Browse HDFS, upload files, save files, and launch useful actions such as Analyze in Notebook for CSV files.<br/><br/>Submit Spark jobs from the dashboard or right-click on a HDFS/Spark connection in Object Explorer. |
| Azure Data Studio Notebooks. | Create or open Notebooks using an integrated Notebook viewer. In this release the Notebook viewer supports connecting to local kernels and the SQL Server 2019 big data cluster only.<br/><br/>Use the PROSE Code Accelerator libraries in your Notebook to learn file format and data types for fast data preparation. |
| Azure Resource Explorer. | The Azure Resource Explorer view lets you browse data-related endpoints for your Azure accounts and create connections to them in Object Explorer. In this release Azure SQL Databases and servers are supported. |
| SQL Server PolyBase Create External Table Wizard. | Create an external table and its supporting metadata structures with an easy to use wizard. In this release, remote SQL Server and Oracle servers are supported. |
| &nbsp; | &nbsp; |

### Bug Fixes, September 2018

- Fix [issue #2647](https://github.com/Microsoft/azuredatastudio/issues/143): The charts took a big step backwards.
- Fix [issue #2648](https://github.com/Microsoft/azuredatastudio/issues/143): SELECT that returns a JSON hyperlinks the whole column.

For detailed information, see the [Change Log](https://github.com/Microsoft/azuredatastudio/blob/master/CHANGELOG.md), and [Releases](https://github.com/Microsoft/azuredatastudio/releases).

## August 2018

August 30, 2018 &nbsp; / &nbsp; version: 0.32.8 &nbsp; / &nbsp; Public Preview

The *August Public Preview* focuses on bug fixes, product stabilization, and filling gaps in existing scenarios.

_0.32.8 contains fixes for a couple regressions found in 0.32.7 ([#1971](https://github.com/Microsoft/azuredatastudio/issues/1971), [#2372](https://github.com/Microsoft/azuredatastudio/issues/2372))_

&nbsp;

| Change | Details |
| :----- | :------ |
| Announcing the SQL Server Import Extension. | &nbsp; |
| SQL Server Profiler Session management. | &nbsp; |
| SQL Server Profiler session template support. | &nbsp; |
| SQL Server Agent improvements. | &nbsp; |
| New community extension: First responder kit. | &nbsp; |
| Quality of Life improvements: Connection strings | &nbsp; |
| &nbsp; | &nbsp; |

### Bug Fixes, August 2018

- Parse SQL in a Query Editor window by using the `Parse Syntax` command.
- Fix [issue #143](https://github.com/Microsoft/azuredatastudio/issues/143): Double-click not selecting @ in variable name.
- Fix [issue #387](https://github.com/Microsoft/azuredatastudio/issues/387): SQL Tab DB Icon is red.
- Fix [issue #825](https://github.com/Microsoft/azuredatastudio/issues/825): Request: Auto Connect to current server after Script as... 
- Fix [issue #1278](https://github.com/Microsoft/azuredatastudio/issues/1278): sqlops.desktop [Desktop Entry] - redundant value for Name & Comment.
- Fix [issue #1285](https://github.com/Microsoft/azuredatastudio/issues/1285): Updating causes application icon to be removed/replaced in Windows.
- Fix [issue #1317](https://github.com/Microsoft/azuredatastudio/issues/1317): Fix the decimal separator.
- Fix [issue #1474](https://github.com/Microsoft/azuredatastudio/issues/1474): Cancel change connection disconnects current connection.
- Fix [issue #1497](https://github.com/Microsoft/azuredatastudio/issues/1497): View as Chart options are cut off at the bottom.
- Fix [issue #1524](https://github.com/Microsoft/azuredatastudio/issues/1524): Shell/Dashboard: Main viewlet icons are draggable and can crash the app.
- Fix [issue #1578](https://github.com/Microsoft/azuredatastudio/issues/1578): Not able to expand/collapse remote file browser folder by clicking name.
- Fix [issue #1620](https://github.com/Microsoft/azuredatastudio/issues/1620): Feature Suggestion: Get Connection String for existing connection.
- Fix [issue #1624](https://github.com/Microsoft/azuredatastudio/issues/1624): SelectBox doesn't change color when disabled.
- Fix [issue #1728](https://github.com/Microsoft/azuredatastudio/issues/1728): Save as JSON/EXCEL/CSV not work.
- Fix [issue #1744](https://github.com/Microsoft/azuredatastudio/issues/1744): Results pane loses its scrolling positions when switching between tabs.
- Fix [issue #1748](https://github.com/Microsoft/azuredatastudio/issues/1748): Error message when saving Excel file second (and subsequent) time.
- Fix [issue #1782](https://github.com/Microsoft/azuredatastudio/issues/1782): Edit data: cell doesn't revert to original value on hitting Escape key.
- Fix [issue #1836](https://github.com/Microsoft/azuredatastudio/issues/1836): .sql files not associated with SQL Operations Studio.
- Fix [issue #1850](https://github.com/Microsoft/azuredatastudio/issues/1850): Typing N'' autocompletes to N'''.
- Fix [issue #1985](https://github.com/Microsoft/azuredatastudio/issues/1985): Copy from query results grid is off by 1 column.
- Fix [issue #1998](htpts://github.com/Microsoft/azuredatastudio/pull/1998): Add VS Code version to About dialog.
- Fix [issue #2042](https://github.com/Microsoft/azuredatastudio/pull/2042): Agent: Enabled button to import queries from sql files.
- Fix [issue #2091](https://github.com/Microsoft/azuredatastudio/issues/2091): Can't use Ctrl+C shortcut to copy from result pane.
- Fix [issue #2099](https://github.com/Microsoft/azuredatastudio/pull/2099): Added more saveAsCsv options.
- Fix [issue #2107](https://github.com/Microsoft/azuredatastudio/issues/2107): Update document icon for Dashboard and Profiler documents.
- Fix [issue #2129](https://github.com/Microsoft/azuredatastudio/pull/2129): Save edit data scroll position when switching tabs.
- Fix [issue #2152](https://github.com/Microsoft/azuredatastudio/issues/2152): Results Grid Row Indicator Zero Based.

### Known Issues, August 2018

- [Issue #2371](https://github.com/Microsoft/azuredatastudio/issues/2371) Save As Excel Only Saves First Row of Data
- [Issue #2150](https://github.com/Microsoft/azuredatastudio/issues/2150): Unable to connect on Ubuntu 16.04 to SQL in a container

## July 2018

July 19, 2018 &nbsp; / &nbsp; version: 0.31.4 &nbsp; / &nbsp; Public Preview

The *July Public Preview* focuses on the following items:

- The initial release of the SQL Server Agent configuration scenarios.
- SQL Server Profiler session and view template enhancements.
- Continued bug fixes for customer reported GitHub issues.

&nbsp;

| Change | Details |
| :----- | :------ |
| [SQL Server Agent for SQL Operations Studio extension](sql-server-agent-extension.md) improvements. | Added view of Alerts, Operators, and Proxies and icons on left pane.<br/><br/>Added dialogs for New Job, New Job Step, New Alert, and New Operator.<br/><br/>Added Delete Job, Delete Alert, and Delete Operator (right-click).<br/><br/>Added Previous Runs visualization.<br/><br/>Added Filters for each column name. |
| [SQL Server Profiler for SQL Operations Studio extension](sql-server-profiler-extension.md) improvements. | Added 5 Default Templates to view Extended Events.<br/><br/>Added Server/Database connection name.<br/><br/>Added support for Azure SQL Database instances.<br/><br/>Added suggestion to exit Profiler when tab is closed when Profiler is still running. |
| Release of Combine Scripts Extension. | &nbsp; |
| Wizard and Dialog Extensibility points added for Extension Authors. | &nbsp; |
| &nbsp; | &nbsp; |

### Bug Fixes, July 2018

- Fix [issue 728](https://github.com/Microsoft/azuredatastudio/issues/728): No response to Add Connection on macOS
- Fix [issue 1612](https://github.com/Microsoft/azuredatastudio/issues/1612): Results grid text display is messed up by international characters
- Fix [issue 1693](https://github.com/Microsoft/azuredatastudio/issues/1693): Backup dialog: File browser UI is broken
- Fix [issue 1713](https://github.com/Microsoft/azuredatastudio/issues/1713): Number of rows affected
- Fix [issue 1718](https://github.com/Microsoft/azuredatastudio/issues/1718): Unable to connect to any datasource
- Fix [issue 1719](https://github.com/Microsoft/azuredatastudio/issues/1719): TypeError when Connecting to Server
- Fix [issue 1724](https://github.com/Microsoft/azuredatastudio/issues/1724): Extension dialogs have stopped working
- Fix [issue 1749](https://github.com/Microsoft/azuredatastudio/issues/1749): BUG: HTML data in a column gets interpreted
- Fix [issue 1789](https://github.com/Microsoft/azuredatastudio/issues/1789): Extensibility: if you add a connection provider uninstall will never remove it from the list
- Fix [issue 1791](https://github.com/Microsoft/azuredatastudio/issues/1791): Sqlops Extensions: queryeditor.connect() connects to the target database, but UI does not show the editor is connected
- Fix [issue 1799](https://github.com/Microsoft/azuredatastudio/issues/1799): Top 10 DB Size chart does not work on case sensitive instances
- Fix [issue 1814](https://github.com/Microsoft/azuredatastudio/issues/1814): sqlops.d.ts typo causing implicit 'any' type definition
- Fix [issue 1817](https://github.com/Microsoft/azuredatastudio/issues/1817): Error de Ortografia
- Fix [issue 1830](https://github.com/Microsoft/azuredatastudio/issues/1830): Setting iconPath in ButtonComponent after component() is called does not change icon
- Fix [issue 1843](https://github.com/Microsoft/azuredatastudio/issues/1843): Better Table organization

## June 2018

June 20, 2018 &nbsp; / &nbsp; version: 0.30.6 &nbsp; / &nbsp; Public Preview

&nbsp;

| Change | Details |
| :----- | :------ |
| **SQL Server Profiler for SQL Operations Studio _Preview_** extension initial release. | &nbsp; |
| The new **SQL Data Warehouse** extension includes rich customizable dashboard widgets surfacing insights to your data warehouse. | This unlocks key scenarios around managing and tuning your data warehouse to ensure it is optimized for consistent performance. |
| **Edit Data "Filtering and Sorting"** support. | &nbsp; |
| **SQL Server Agent for SQL Operations Studio _Preview_** extension enhancements for Jobs and Job History views. | &nbsp; |
| Improved **Wizard & Dialog UI Builder Framework** extensibility APIs. | &nbsp; |
| Update VS Code Platform source code. | Integrated the following releases:<br/>&bull; &nbsp; [March 2018 (1.22)](https://code.visualstudio.com/updates/v1_22)<br/>&bull; &nbsp; [April 2018 (1.23)](https://code.visualstudio.com/updates/v1_23) |
| &nbsp; | &nbsp; |

### GitHub Issues Fixes, June 2018

- Feature request ([issue 1204](https://github.com/Microsoft/azuredatastudio/issues/1204)): Please make the results grid auto-fit column width to data, and remember manual changes if the same query is re-run.
- Fix [issue 1398](https://github.com/Microsoft/azuredatastudio/issues/1398): Should show add message and add account account button when linked account is empty.
- Fix [issue 1399](https://github.com/Microsoft/azuredatastudio/issues/1399): Linked account tab is broken when the view is collapsed.
- Fix [issue 1374](https://github.com/Microsoft/azuredatastudio/issues/1374): SQL Tools Service crashes when opening .sql file from disk.
- Fix [issue 1372](https://github.com/Microsoft/azuredatastudio/issues/1372): Missing SQL keyword "BETWEEN".
- Fix [issue 1395](https://github.com/Microsoft/azuredatastudio/issues/1395): 'MATCH' keyword crashes SQL Tools Service.
- Fix [issue 1496](https://github.com/Microsoft/azuredatastudio/issues/1496): "New Profiler" context menu option in Object Explorer does nothing.
- Fix [issue 1495](https://github.com/Microsoft/azuredatastudio/issues/1495): Query editor "Explain" query plan is broken.

## May 2018

May 7, 2018 &nbsp; / &nbsp; version: 0.29.3 &nbsp; / &nbsp; Public Preview

The *May Public Preview* is focused on stabilization and bug fixes.

&nbsp;

| Change | Details |
| :----- | :------ |
| Announcing Redgate SQL Search extension available in Extension Manager. | &nbsp; |
| Community Localization available for 10 languages. | German, Spanish, French, Italian, Japanese, Korean, Portuguese, Russian, Simplified Chinese, and Traditional Chinese. |
| Telemetry collection changes. | &bull; &nbsp; Reduced telemetry collection.<br/>&bull; &nbsp; Improved opt-out experience.<br/>&bull; &nbsp; In-product links to Privacy Statement. |
| Extension Manager has improved Marketplace experience. | More easily discover community extensions. |
| SQL Agent extension. | &bull; &nbsp; Jobs.<br/>&bull; &nbsp; Job History view improvement. |
| Updates for whoisactive and Server Reports extensions. | &nbsp; |
| Improved scrolling of Manage Dashboard Properties. | &nbsp; |
| &nbsp; | &nbsp; |

### Fix GitHub Issues

- Fix [issue 703](https://github.com/Microsoft/azuredatastudio/issues/703): Entering HTML-like text in edit data causes value to display incorrectly until refresh
- Fix [issue 821](https://github.com/Microsoft/azuredatastudio/issues/821): azuredatastudio.deb package dependency
- Fix [issue 1260](https://github.com/Microsoft/azuredatastudio/issues/1260): Keyword 'distinct' not highlighted
- Fix [issue 1332](https://github.com/Microsoft/azuredatastudio/issues/1332): Edit data revert row doesn't work
- Fix [issue 1215](https://github.com/Microsoft/azuredatastudio/issues/1215): SQL Agent extension and the status bar
- Fix [issue 1316](https://github.com/Microsoft/azuredatastudio/issues/1316): SQL Agent Don´t resize after change windows size

## April 2018

April 25, 2018 &nbsp; / &nbsp; version: 0.28.6 &nbsp; / &nbsp; Public Preview

The *April Public Preview* contains bug fixes and improvements.

&nbsp;

| Change | Details |
| :----- | :------ |
| Improvements to the SQL Agent Preview extension: | &nbsp; |
| &nbsp; &nbsp; &nbsp; Improved support for files. | &bull; &nbsp; Large files.<br/>&bull; &nbsp; Protected files, for saving Admin protected.<br/>&bull; &nbsp; Storing \>256M files within SQL Operations Studio. |
| &nbsp; &nbsp; &nbsp; Integrated Terminal Splitting. | Work with multiple open terminals simultaneously. |
| &nbsp; &nbsp; &nbsp; Faster installs and startup times. | Reduced installation of on-disk file count foot print. |
| &nbsp; | &nbsp; |

### Fix GitHub issues, April 2018

- Fix [issue 37](https://github.com/Microsoft/azuredatastudio/issues/37): When the chart viewer throws an error, unexpected behavior occurs.
- Fix [issue 462](https://github.com/Microsoft/azuredatastudio/issues/462): Feature Request: Option for Server Groups to be expanded by default.
- Fix [issue 606](https://github.com/Microsoft/azuredatastudio/issues/606): intellisense - Bad suggestion for 'update' command.
- Fix [issue 967](https://github.com/Microsoft/azuredatastudio/issues/967): Expect query plan when select XML showplan in the result grid.
- Fix [issue 1023](https://github.com/Microsoft/azuredatastudio/issues/1023): Add square brackets for ms_foreachdb call from flyfishingdba.
- Fix [issue 1048](https://github.com/Microsoft/azuredatastudio/issues/1048): Pre-login SSL/TLS handshake error.
- Fix [issue 1050](https://github.com/Microsoft/azuredatastudio/issues/1050): Clear insights view before showing error.
- Fix [issue 1057](https://github.com/Microsoft/azuredatastudio/issues/1057): Restore and new query actions in explorer-widget are broken.
- Fix [issue 1068](https://github.com/Microsoft/azuredatastudio/issues/1068): Dashboard Output windows pops-up with error message for Azure SQL Database.
- Fix [issue 1069](https://github.com/Microsoft/azuredatastudio/issues/1069): Connection Dialog shows Server Required error when initially displayed.
- Fix [issue 1070](https://github.com/Microsoft/azuredatastudio/issues/1070): Server Groups now require a double-click to expand.
- Fix [issue 1072](https://github.com/Microsoft/azuredatastudio/issues/1072): Select control background is semi-transparent.
- Fix [issue 1115](https://github.com/Microsoft/azuredatastudio/issues/1115): Fix all high contrast accessibility issues in SQL Operations Studio.
- Fix [issue 1101](https://github.com/Microsoft/azuredatastudio/issues/1101): Extension fails to upgrade "Download Manually" link goes to wrong location.
- Fix [issue 1103](https://github.com/Microsoft/azuredatastudio/issues/1103): V Scroll not working on Home Tab.
- Fix [issue 1104](https://github.com/Microsoft/azuredatastudio/issues/1104): SQL extension tabs stopped working.

### Visual Studio Code 1.21 platform

A highlight for the April Public Preview is the refresh of the source code for the Visual Studio Code 1.21 platform. This brings several updates to the core editor and workbench from the previous 1.19 sync point. Some examples include the following:

&nbsp;

| Change | Details |
| :----- | :------ |
| [New Notifications UI](https://code.visualstudio.com/updates/v1_21#_new-notifications-ui). | Easily manage and review SQL Operations Studio notifications. |
| [Integrated Terminal splitting](https://code.visualstudio.com/updates/v1_21#_split-terminals). | Work with multiple open terminals at once. |
| [Save large and protected files](https://code.visualstudio.com/updates/v1_20#_save-files-that-need-admin-privileges). | Save Admin protected and \>256M files within SQL Operations Studio. |
| [Improved large file support](https://code.visualstudio.com/updates/v1_21#_text-buffer-improvements). | Text buffer optimizations for large files. |
| [Improved Settings search](https://code.visualstudio.com/updates/v1_20#_settings-search). | Easily find the right setting with natural language search. |
| [Global snippets](https://code.visualstudio.com/updates/v1_20#_global-snippets). | Create snippets you can use across all file types. |
| [Explorer multi-selection](https://code.visualstudio.com/updates/v1_20#_multi-select-in-the-explorer). | Perform actions on multiple files at once. |
| [Errors & warnings in Explorer](https://code.visualstudio.com/updates/v1_20#_error-indicators-in-the-explorer). | Quickly navigate to errors in your code base. |
| [Drag & drop, copy & paste across windows](https://code.visualstudio.com/updates/v1_21#_better-drag-and-drop-support). | Move files across open SQL Operations Studio windows. |
| [Git submodule support](https://code.visualstudio.com/updates/v1_20#_git-submodules). | Perform Git operations on nested Git repositories. |
| [Terminal screen reader support](https://code.visualstudio.com/updates/v1_20#_screen-reader-support). | Integrated Terminal now has **Screen Reader Optimized** mode. |
| [Centered editor layout](https://code.visualstudio.com/updates/v1_21#_centered-editor-layout). | Maximize your code viewing screen real estate. |
| [Horizontal search results (preview)](https://code.visualstudio.com/updates/v1_21#_horizontal-search). | You can now view search results in a horizontal panel. |
| &nbsp; | &nbsp; |

For additional details, checkout the [Visual Studio Code February Release Notes](https://code.visualstudio.com/updates/v1_21), and the [Visual Studio Code January Release Notes](https://code.visualstudio.com/updates/v1_20).

For more information, see the [Change Log](https://github.com/Microsoft/azuredatastudio/blob/master/CHANGELOG.md).

## March 2018

March 28, 2018 &nbsp; / &nbsp; version: 0.27.3 &nbsp; / &nbsp; Public Preview

The *March Public Preview* continues to address the top GitHub issues, and is focused on improving our extensibility story. Specifically enabling Extension Manager, improving dashboard management, and providing SQL Agent and insights extensions. This release includes the following enhancements:

&nbsp;

| Change | Details |
| :----- | :------ |
| Enhance the dashboard extensibility model to support tabbed insights and configuration panes. | Extension Manager enables simple acquisition of extensions.<br/><br/>Dashboard extensions for sp\_whoisactive from [whoisactive.com](http://www.whoisactive.com).<br/><br/>For details, see [Extend the functionality of SQL Operations Studio](extensions.md). |
| Add additional [extensibility APIs for connection and object explorer](https://github.com/Microsoft/azuredatastudio/wiki/Extensibility-API) management. | &nbsp; |
| Continue to fix important customer impacting [GitHub issues](https://github.com/Microsoft/azuredatastudio/issues). | &nbsp; |
| &nbsp; | &nbsp; |

## February 2018

February 15, 2018 &nbsp; / &nbsp; version: 0.26.7 &nbsp; / &nbsp; Public Preview

The *February Public Preview* includes some feature suggestions and high-priority bug fixes. This release includes the following enhancements:

&nbsp;

| Change | Details |
| :----- | :------ |
| Introducing Auto-Update Installation, which provides a notification when a new release is available to download. | &nbsp; |
| The Connection Dialog **Database** field is now a dynamically populated drop-down list that will contain a list of databases populated from the specified server. | &nbsp; |
| Introduce Connection extensibility API. | &nbsp; |
| VS Code Editor 1.19 integration. | &nbsp; |
| Update JustinPealing/html-query-plan component to pick-up several Query Plan viewer improvements. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixed Issues, February 2018

- Fix [issue 6](https://github.com/Microsoft/azuredatastudio/issues/6): Keep connection and selected database when opening new query tabs.
- Fix [issue 22](https://github.com/Microsoft/azuredatastudio/issues/22): 'Server Name' and 'Database Name' - Can these be drop downs instead of text boxes?
- Fix [issue 549](https://github.com/Microsoft/azuredatastudio/issues/549): Silent/Very Silent Install results in application opening after installation.
- Fix [issue 481](https://github.com/Microsoft/azuredatastudio/issues/481): Add "Check for updates" option.
- SQL Editor colorization and auto-completion fixes:
  - Fix [issue 584](https://github.com/Microsoft/azuredatastudio/issues/584): Keyword "FULL" not highlighted by IntelliSense.
  - Fix [issue 345](https://github.com/Microsoft/azuredatastudio/issues/345): Colorize SQL functions within the editor.
  - Fix [issue 300](https://github.com/Microsoft/azuredatastudio/issues/300): [#tempData] latest "]" will display green color.
  - Fix [issue 225](https://github.com/Microsoft/azuredatastudio/issues/225): Keyword color mismatch.
  - Fix [issue 60](https://github.com/Microsoft/azuredatastudio/issues/60): Invalid sql syntax color highlighting when using temporary table in from clause.

## January 2018

January 17, 2018 &nbsp; / &nbsp; version: 0.25.4 &nbsp; / &nbsp; Public Preview

The *January Public Preview* includes some feature suggestions and high-priority bug fixes. This release includes the following enhancements:

&nbsp;

| Change | Details |
| :----- | :------ |
| Saved Server connections are available in the Connection Dialog. | &nbsp; |
| Enable Hot exit. Hot exit is off by default, to enable see [Hot exit setting](settings.md#hot-exit). | &nbsp; |
| Tab-coloring based on Server Group. Tab coloring is off by default, to enable see [Tab color setting](settings.md#tab-color). | &nbsp; |
| Change *Server name* to *Server* in the Connection Dialog. | &nbsp; |
| Fix broken *Run Current Query* command. | &nbsp; |
| Fix drag-and-drop breaking scripting bug. | &nbsp; |
| Fix incorrect pinned Start Menu icon. | &nbsp; |
| Fix missing Azure Account branding icon. | &nbsp; |
| &nbsp; | &nbsp; |

## December 2017

December 19, 2017 &nbsp; / &nbsp; version: 0.24.1 &nbsp; / &nbsp; Public Preview

The *December Public Preview* includes several bugs fixes across all feature areas, as well as the following enhancements:

&nbsp;

| Change | Details |
| :----- | :------ |
| Create Firewall Rule Dialog is now available to assist connecting to Azure SQL Database and Azure SQL Data Warehouse. | &nbsp; |
| Added Windows Setup, and Linux DEB and RPM installation packages. | &nbsp; |
| Manage Dashboard visual layout editor. | &nbsp; |
| *Script As Alter* and *Script As Execute* commands. | &nbsp; |
| *Run Current Query with Actual Plan* command. | &nbsp; |
| Integrate VS Code 1.18.1 editor platform. | &nbsp; |
| Enable Sideloading of VSIX Extension files. | &nbsp; |
| Support "GO N" batch iteration syntax. | &nbsp; |
| &nbsp; | &nbsp; |

## November 2017

November 15, 2017 &nbsp; / &nbsp; version: 0.23.6

- Initial release of [!INCLUDE[name-sos](../includes/name-sos-short.md)].

## Next Steps

See one of the following quickstarts to get started:

- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Data Warehouse](quickstart-sql-dw.md)

Contribute to [!INCLUDE[name-sos](../includes/name-sos-short.md)]:

- [https://github.com/Microsoft/azuredatastudio](https://github.com/Microsoft/azuredatastudio)
