---
title: Azure Data Studio release notes
description: This article has release notes for Azure Data Studio.
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: 11/23/2022
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom:
  - seodec18
  - contperf-fy21q4
---

# Release notes for Azure Data Studio

This article provides details about updates, improvements, and bug fixes for the current and previous versions of Azure Data Studio.

## Current Azure Data Studio release

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download and install the latest release!](./download-azure-data-studio.md)**

### November 2022 (hotfix)

Azure Data Studio 1.40.1 is the latest general availability (GA) release.

- Release number: 1.40.1
- Release date: November 23, 2022

#### Bug fixes in 1.40.1

| New Item | Details |
|----------|---------|
| Object Explorer | Fixed bug that caused folders in the Servers tree to display incorrect contents. |

## Azure Data Studio feedback

You can reference [Azure Data Studio feedback](https://github.com/microsoft/azuredatastudio/issues/new/choose) for other known issues and to provide feedback to the product team.

## Previous Azure Data Studio releases and updates

| Azure Data Studio release | Build number | Release date | Hotfix |
|---------------------------|--------------|--------------|---------|
| [November 2022](#november-2022) | 1.40.0 | November 16, 2022 |N/A| 
| [August 2022](#august-2022) | 1.39.1 | August 30, 2022 |[hotfix](#august-2022-hotfix)|
| [August 2022](#august-2022) | 1.39.0 | August 24, 2022 |N/A|
| [July 2022](#july-2022) | 1.38.0 | July 27, 2022 |N/A|
| [June 2022](#june-2022) | 1.37.0 | June 15, 2022 |N/A|
| [April 2022](#april-2022) | 1.36.0 | April 20, 2022 |[hotfix](#may-2022-hotfix)|
| [February 2022](#february-2022) | 1.35.0 | February 24, 2022 |[hotfix](#february-2022-hotfix)|
| [December 2021](#december-2021) | 1.34.0 | December 15, 2021 | N/A |  
| [October 2021](#october-2021) | 1.33.0 | October 27, 2021 | N/A |
| [August 2021](#august-2021) | 1.32.0 | August 18, 2021 | N/A |
| [July 2021](#july-2021) | 1.31.0 | July 21, 2021 | N/A |
| [June 2021](#june-2021) | 1.30.0 | June 17, 2021 | N/A |
| [May 2021](#may-2021) | 1.29.0 | May 19, 2021 | N/A |
| [April 2021](#april-2021) | 1.28.0 | April 15, 2021 | N/A |
| [March 2021](#march-2021) | 1.27.0 | March 17, 2021 | N/A |
| [February 2021](#february-2021) | 1.26.0 | February 18, 2021 | N/A |
| [December 2020](#december-2020) | 1.25.0 | December 9, 2020 | [hotfix](#december-2020-hotfix) |
| [November 2020](#november-2020) | 1.24.0 | November 12, 2020 | N/A |
| [October 2020](#october-2020) | 1.23.0 | October 14, 2020 | N/A |
| [September 2020](#september-2020) | 1.22.0 | September 22, 2020 | [hotfix](#september-2020-hotfix) |
| [August 2020](#august-2020) | 1.21.0 | August 12, 2020 | N/A |
| [July 2020](#july-2020) | 1.20.0 | July 15, 2020 | [hotfix](#july-2020-hotfix) |
| [June 2020](#june-2020) | 1.19.0 | June 15, 2020 | N/A |
| [May 2020](#may-2020) | 1.18.0 | May 20, 2020 | [hotfix](#may-2020-hotfix) |
| [April 2020](#april-2020) | 1.17.0 | April 27, 2020 | [hotfix](#april-2020-hotfix) |
| [March 2020](#march-2020) | 1.16.0 | March 18, 2020 | N/A |
| [February 2020](#february-2020) | 1.15.0 | February 13, 2020 | [hotfix](#february-hotfix) |
| [December 2019](#december-2019) | 1.14.0 | December 19, 2019 | [hotfix](#november-2019-hotfix) |
| [November 2019](#november-2019) | 1.13.0 | November 4, 2019 | [hotfix](#november-2019-hotfix) |
| [October 2019](#october-2019) | 1.12.0 | October 2, 2019 | [hotfix 1](#october-2019-hotfix) </br> [hotfix 2](#october-2019-hotfix-2) |
| [September 2019](#september-2019) | 1.11.0 | September 10, 2019 | N/A |
| [August 2019](#august-2019) | 1.10.0 | August 15, 2019 | N/A |
| [July 2019](#july-2019) | 1.9.0 | July 11, 2019 | N/A |
| [June 2019](#june-2019) | 1.8.0 | June 6, 2019 | N/A |
| [May 2019](#may-2019) | 1.7.0 | May 8, 2019 | N/A |
| [April 2019](#april-2019)| 1.6.0 | April 18, 2019 | N/A |
| [March 2019](#march-2019) | 1.5.1 | March 18, 2019 | [hotfix](#march-2019-hotfix) |
| [February 2019](#february-2019) | 1.4.5 | February 13, 2019 | N/A |
| [January 2019](#january-2019) | 1.3.8 | January 09, 2019 | [hotfix](#january-2019-hotfix) |

[Download the previous release of Azure Data Studio](https://github.com/microsoft/azuredatastudio/releases).

> [!NOTE]
> All previous versions of Azure Data Studio are not supported.

### November 2022

Azure Data Studio 1.40 is the latest general availability (GA) release.

- Release number: 1.40
- Release date: November 16, 2022

#### What's new in 1.40

| New Item | Details |
|----------|---------|
| Connections | Connections for SQL now default to Encrypt = 'True'. |
| ARM64 Support for macOS | ARM64 build for macOS is now available.  |
| Table Designer | Announcing the General Availability of the Table Designer. |
| Table Designer | Period columns now added by default when System-Versioning table option is selected. |
| Table Designer | Added support for hash indexes for In-Memory tables, and added support for columnstore indexes. |
| Table Designer | New checkbox added, "Preview Database Updates", when making database changes to ensure that users are aware of potential risks prior to updating the database.|
| Table Designer | "Move Up" and "Move Down" buttons added to support column reordering for Primary Keys. |
| Query Plan Viewer | Announcing the General Availability of the Query Plan Viewer in Azure Data Studio. |
| Query Plan Viewer | Introduced the ability to identify the most expensive operator in plan, based on user-selected metric (e.g. cost, elapsed time). |
| Query Plan Viewer | Updates were made to the properties window to allow for full display of text when hovering over a cell, and text can now be copied to the clipboard. |
| Query Plan Viewer | Implemented filter functionality in the Properties pane for an execution plan. |
| Query Plan Viewer | Added support for collapsing and expanding all subcategories within the Plan Comparison Properties window. |
| Query History Extension | Announcing the General Availability of the SQL History Extension. |
| Query History Extension | Now includes ability to persist history across multiple user sessions. |
| Query History Extension | Added the ability to limit the number of entries stored in the history. |
| Schema Compare | Users can now open .scmp files directly from the context menu for existing files in the file explorer. |
| Query Editor | Now allows full display for text strings larger than 65,535 characters. |
| Query Editor | Added support for the SHIFT key when making multiple cell selections.  |
| MySQL Extension | Support for MySQL extension is now available in preview. |
| Azure SQL Migration Extension | Azure SQL Database Offline Migrations is now available in preview. Customers can use this new capability to save and share reports as needed. |
| Azure SQL Migration Extension | Addition of elastic Azure recommendations model. |
| Database Migration Assessment for Oracle | Assessment tooling for Oracle database migrations to Azure Database for PostgreSQL and Azure SQL available in preview. |
| VS Code merge| VS Code merges to version 1.67. Read [their release notes](https://code.visualstudio.com/updates/v1_67) to learn more. |
|SQL Database Projects|Adds SQL projects support for syntax introduced in SQL Server 2022.|

#### Bug fixes in 1.40

| New Item | Details |
|----------|---------|
| Connections | Fixed bug that occurred when trying to connect to the Dedicated Admin Connection (DAC) on SQL Server. |
| Connections | Fixed issue with wrong tenant showing up while trying to connect to a database with Azure Active Directory login. |
| Connections | Fixed zoom reset behavior when adding a new connection. |
| Connections | Fixed loading bug what occurred when attempting to sign in to Azure via proxy. |
| Connections | Fixed issue encountered while attempting to connect to a "sleeping" Azure SQL Database. |
| Object Explorer | Fixed the SELECT script generation issue for Synapse Databases. |
| Schema Compare | Fixed error that caused duplication of comment headers when applying schema changes on stored procedure objects. |
| Schema Compare | Fixed issue that prevented schema compare issues when creating a new empty schema with a "DOMAIN\User" pattern. |
| Query Editor | Fixed bug that caused results to be lost upon saving query files. |
| Table Designer | Fixed a bug that caused creation of a new table when renaming an existing table. |
| Query Plan Viewer | Fixed missing index recommendation T-SQL syntax. |
| SQL Projects | Fixed bug in SQL Projects that led to extension not using output path when publishing a project. |
| SQL Projects | Fixed bug that caused .NET install to not be found when using the SQL Projects extension on Linux platforms. |

### August 2022 (hotfix)

- Release number: 1.39.1
- Release date: August 30, 2022

#### Bug fixes in 1.39.1

| New Item | Details |
|----------|---------|
| Object Explorer | Fixed bug that caused Database Trees in server connections to not expand in the Object Explorer. |

### August 2022

- Release number: 1.39.0
- Release date: August 24, 2022

#### What's new in 1.39.0

| New Item | Details |
|----------|---------|
| Deployment Wizard | Azure Data Studio now supports [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] in the Deployment Wizard for both local and container installation. |
| Object Explorer | Added Ledger icons and scripting support to Object Explorer for Ledger objects. |
| Dashboard | Added hexadecimal values to support color detection. |
| Query Plan Viewer | Added the ability to copy text from cells in the Properties Pane of a query plan. |
| Query Plan Viewer | Introduced a "find node" option in plan comparison to search for nodes in either the original or added plan. |
| Table Designer | Now supports the ability to add included columns to a nonclustered index, and the ability to create filtered indexes. |
| SQL Projects | Publish options were added to the Publish Dialog. |
| Query History Extension | Added double-click support for query history to either open the query or immediately execute it, based on user configuration. |

#### Bug fixes in 1.39.0

| New Item | Details |
|----------|---------|
| Dashboard | Fixed an accessibility issue that prevented users from being able to access tooltip information using the keyboard. |
| Voiceover | Fixed a bug that caused voiceover errors across the Dashboard, SQL Projects, SQL Import Wizard, and SQL Migration extensions. |
| Schema Compare | Fixed a bug that caused the UI to jump back to the top of the options list after selecting/deselecting any option. |
| Schema Compare | Fixed a bug involving Schema Compare (.SCMP) file incompatibility with Database Project information causing errors when reading and using information stored in this file type. |
| Object Explorer | Fixed a bug that caused menu items in Object Explorer not to show up for non-English languages. |
| Table Designer | Fixed a bug that caused the History Table name not to be consistent with the current table name when working with System-Versioned Tables. |
| Table Designer | Fixed a bug in the Primary Key settings that caused the "Allow Nulls" option to be checked, but disabled, preventing users from changing this option. |
| Query Editor | Fixed a bug that prevented the SQLCMD in T-SQL from working correctly, giving false errors when running scripts in Azure Data Studio. |
| Query Editor | Fixed a bug that caused user-specified zoom settings to reset to default when selecting JSON values after query that returned JSON dataset was ran. |
| SQL Projects | Fixed a bug that caused the "Generate Script" command to not work correctly when targeting a new Azure SQL Database. |
| Notebooks | Fixed a bug that caused pasted images to disappear from editor after going out of edit mode. |
| Notebooks | Fixed a bug that caused a console error message to appear after opening a markdown file. |
| Notebooks | Fixed a bug that prevented markdown cell toolbar shortcuts from working after creating a new split view cell. |
| Notebooks | Fixed a bug that caused text cells to be erroneously created in split view mode when the notebook default text edit mode was set to "Markdown". |

### July 2022

### What's new in 1.38.0

| New Item | Details |
|----------|---------|
| VS Code merges to 1.62 | This release includes updates to VS Code from the three previous VS Code releases. Read [their release notes](https://code.visualstudio.com/updates/v1_62) to learn more. |
| Table Designer | New column added to Table Designer for easier access to additional actions specific to individual rows. |
| Query Plan Viewer | The Top Operations pane view now includes clickable links to operations in each of its rows to show the runtime statistics which can be used to evaluate estimated and actual rows when analyzing a plan. |
| Query Plan Viewer | Improved UI on selected operation node in Execution Plan. |
| Query Plan Viewer | The keyboard command **CTRL + M** no longer executes queries. It now just enables or disables the actual execution plan creation when a query is executed. |
| Query Plan Viewer | Plan labels are now updated in the Properties window when plans are compared and the orientation is toggled from horizontal to vertical, and back. |
| Query Plan Viewer | Updates were made to the Command Palette. All execution plan commands are prefixed with "Execution Plan", so that they are easier to find and use. |
| Query Plan Viewer | A collapse/expand functionality is now available at the operator level to allow users to hide or display sections of the plan during analysis. |
| Query History | The Query History extension was refactored to be fully implemented in an extension. This makes the history view behave like all other extension views and also allows for searching and filtering in the view by selecting the view and typing in your search text. |

### Bug fixes in 1.38.0

| New Item | Details |
|----------|---------|
| Table Designer | Error found in edit data tab when switching back to previously selected column when adding a new row. To fix this, editing the table is now disabled while new rows are being added and only reenabled afterwards. |
| Query Editor | Fixed coloring issues for new T-SQL functions in the Query Editor. |
| Query Plan Viewer| Fixed bug that caused custom zoom level spinner to allow values outside valid range. |
| Dashboard | Fixed issue that caused incorrect displaying of insight widgets on the dashboard. |
| Notebooks | Fixed issue where keyboard shortcuts and toolbar buttons were not working when first creating a Split View markdown cell. |
| Notebooks | Fixed issue where cell languages were not being set correctly when opening an ADS .NET Interactive notebook in VS Code. |
| Notebooks | Fixed issue where notebook was being opened as empty when exporting a SQL query as a notebook. |
| Notebooks | Disables install and uninstall buttons in Manage Packages dialog while a package is being installed or uninstalled. |
| Notebooks | Fixed issue where cell toolbar buttons were not refreshing when converting cell type. |
| Notebooks | Fixed issue where notebook was not opening if a cell contains an unsupported output type. |
| Schema Compare | Fixed issue where views and stored procedures were not correctly recognized by schema compare after applying changes. |

### June 2022

### What's new in 1.37.0

| New Item | Details |
|----------|---------|
| Backup & Restore | Backup & Restore to URL is now available in preview for Azure SQL Managed Instances. |
| Table Designer | Added support for computed columns in Table Designer. |
| Table Designer | Can now specify where to add new columns and columns can now be re-arranged by mouse dragging. |
| Table Designer | Table Designer is now supported in the SQL Database Projects extension for editing tables in the SQL project. |
| Query Plan Viewer | Plan comparison is now available and includes visual indicators in the Properties pane for easier identification of differences. |
| Query Plan Viewer | Added a toolbar button to toggle the display for actual execution plans. |
| Query Plan Viewer | Larger query plans will now display additional precision for operator cost. |
| MongoDB Extension for Azure Cosmos DB (Preview) | This extension introduces support for access to Mongo resources for Cosmos DB. |

### Bug fixes in 1.37.0

| New Item | Details |
|----------|---------|
| Table Designer | Fixed issue that caused app to not prompt user to save before closing. |
| Table Designer | Fixed issue that returned empty data set upon attempting to edit the first cell of a new row. |
| Table Designer | Improved resize to fit experience when zooming in on user interface as well as tab behavior issues. |
| Query Plan Viewer | Fixed bug that caused custom zoom level spinner to allow values outside valid range. |
| Schema Compare | Fixed issue with indexes not being added correctly when updating project from database. |
| Notebooks | Fixed inconsistencies with notebook cell behavior and toolbars. |
| Notebooks | Fixed issues with keyboard navigation. |

### May 2022 (hotfix)

- Release number: 1.36.2
- Release date: May 20, 2022

#### What's new in 1.36.2

| New Item | Details |
|----------|---------|
| Power BI | Introduced support for Power BI Datamart connectivity.  Please see [Announcing public preview of datamart in Power BI](https://powerbi.microsoft.com/blog/announcing-public-preview-of-datamart-in-power-bi/). |

#### Bug fixes in 1.36.2

| New Item | Details |
|----------|---------|
| Query Plan Viewer | Fixed issue with execution plan zoom and operator icons. |
| Query Plan Viewer | Updated parallelism icon direction. |

### April 2022 (hotfix)

- Release number: 1.36.1
- Release date: April 22, 2022

#### Bug fixes in 1.36.1

| New Item | Details |
|----------|---------|
| Table Designer | Fix for timeout occurring when viewing table list  |

### April 2022

- Release number: 1.36.0
- Release date: April 20, 2022

#### What's new in 1.36.0

| New item | Details |
|----------|---------|
| Table Designer | Added support for System Versioning, Memory Optimized, and Graph Tables. |
| Query Plan Viewer | Added support icons and additional support for searching within plans. Added additional telemetry to Execution Plans. Provided support on plan toolbar to enable or disable tooltips. Added support for saving .sql plan files on Azure Data Studio  |
| SQL Projects |Introduced new SQL Project format based on an SDK-Style project file |
| Azure SQL Migration Extension | Announcing General Availability of the Azure SQL Migration Extension |
| .NET Interactive Notebooks extension | This extension provides additional multi-language support to Jupyter Notebooks. Please reference [.NET Notebooks in Visual Studio Code](https://devblogs.microsoft.com/dotnet/net-interactive-with-sql-net-notebooks-in-visual-studio-code/)for an introduction to using SQL and .NET interactive |

#### Bug fixes in 1.36.0

| New Item | Details |
|----------|---------|
| Table Designer | Added missing validation rules for primary key column specifications |
| Table Designer | Now able to add description to Primary Key ,Foreign Key, and check constraints|
| Table Designer | Fixed bug that prevents the primary key checkbox from being unchecked for unsupported primary key types|
| Query Plan Viewer | Added option to turn off tooltip in execution plan|
| Query Plan Viewer | Fixed display and sizing issues |
| Query Plan Viewer | Fixed latency issues while switching tabs when execution plan is shown |
| Query Editor | Fixed performance issues in Query Editor |
| Notebooks | Fixed keyboard navigation issues|
| Notebooks | Fixed .NET Interactive log errors on startup|
| Notebooks | Fixed inconsistencies with notebook URI handling|

For a full list of bug fixes addressed for the April 2022 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/milestone/84?closed=1).

#### Known issues in 1.36.0

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### February 2022 (hotfix)

- Release number: 1.35.1
- Release date: March 17, 2022

#### Bug fixes in 1.35.1

| New Item | Details |
|----------|---------|
| Query Editor | Geometry Data Type Returned as Unknown Charset in Results Grid |
| Query Editor | Excel number format  |

### February 2022

- Release number: 1.35.0
- Release date: February 24, 2022

#### What's new in 1.35.0

| New item | Details |
|----------|---------|
| Table Designer | Added functionality for creation and management of tables for SQL Servers. Built using DacFx framework |
| Query Plan Viewer | Added functionality for users to view a graphic view of estimated and actual query plans without need for an extension |
| Azure Arc Extension | Updated the Data Controller deployment wizard and the SQL Managed Instance - Azure Arc deployment wizard to reflect the deployment experience in Azure Portal |

#### Bug fixes in 1.35.0

| New Item | Details |
|----------|---------|
| Azure Arc Extension | SQL Managed Instance-Azure Arc is now fixed for both indirect connectivity mode and direct connectivity mode |
| Notebooks | Support for keyboard navigation between cells to minimize mouse clicking |

For a full list of bug fixes addressed for the February 2022 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22February+2022+Release%22+is%3Aclosed).

#### Known issues in 1.35.0

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### December 2021

- Release number: 1.34.0
- Release date: December 15, 2021

#### What's new in 1.34.0

| New item | Details |
|----------|---------|
| SQL Migration extension | Added 'Currently restoring backup file' in the migration progress details page of Azure SQL migration extension when backup files location is Azure Storage blob container |
| Notebooks | Added undo/redo support |
| SQL Database Projects extension | Support for project build with .NET 6 in SQL Database Projects extension |
| SQL Database Projects extension | Publish to container in SQL Database Projects extension |
| Extension update | [SQL Database Projects](extensions/sql-database-project-extension.md) |
| Extension update | Lang packs |
| Extension update | Azure SQL Migration |

#### Bug fixes in 1.34.0

| New Item | Details |
|----------|---------|
| SQL Migration | Fix for multiple database migrations when using network share as backup files location in Azure SQL migration extension |
| SQL Migration | Fix for multiple database migrations when using blob storage containers as backup files location in Azure SQL migration extension |
| SQL Migration| Fix to pre-populate target database names in the migration wizard in Azure SQL migration extension |
| Grid | Fix to column sorting in grids where the presence of null values could lead to unexpected results |
| Notebooks | Fix for Python upgrades when two or more notebooks were open |

### October 2021

- Release number: 1.33.0
- Release date: October 27, 2021

#### What's new in 1.33.0

| New item | Details |
|----------|---------|
| Notebooks | Added Notebook Views support |
| Notebooks | Added split cell support |
| Notebooks | Added keyboard shortcuts for Markdown Toolbar cells |
| Notebooks | Large performance improvement for large notebooks <br/> &bull; &nbsp; Ctrl/Cmd + B = Bold Text <br/> &bull; &nbsp; Ctrl/Cmd + I = Italicize Text <br/> &bull; &nbsp; Ctrl/Cmd + U = Underline Text <br/>  &bull; &nbsp; Ctrl/Cmd + Shift + K = Add Code Block <br/> &bull; &nbsp; Ctrl/Cmd + Shift + H = Highlight Text |
| Notebooks | Added Book improvements <br/> &bull; &nbsp; Add a new section <br/> &bull; &nbsp; Drag and Drop |
| Extension update  | [Azure Monitor Logs](extensions/azure-monitor-logs-extension.md) |
| Extension update | [Schema Compare](extensions/schema-compare-extension.md) |
| Extension update | [SQL Database Projects](extensions/sql-database-project-extension.md) |
| Extension update | [Machine Learning](extensions/machine-learning-extension.md) |
| Extension update | [Profiler](extensions/sql-server-profiler-extension.md) |
| Extension update | [Import](extensions/sql-server-profiler-extension.md) |
| Extension update | [Kusto](extensions/kusto-extension.md) |
| Extension update | [Dacpac](extensions/sql-server-dacpac-extension.md)|
| Extension update | Lang packs |
| Extension update | Azure SQL Migration |
| Extension update | CMS |
| Extension update | Kusto |

#### Bug fixes in 1.33.0

| New Item | Details |
|----------|---------|
| Notebook | Fixed Notebook linking |
| Notebook | Fixed horizontal scrollbar  (when word wrap is off in MD Splitview / MD mode) in Notebooks |
| Notebook| Fixed vertical scrollbar for MD Splitview in Notebooks |

For a full list of bug fixes addressed for the August 2021 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22October+2021+Release%22+is%3Aclosed).

#### Known issues in 1.33.0

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### August 2021

- Release number: 1.32.0
- Release date: August 18, 2021

#### What's new in 1.32.0

| New item | Details |
|----------|---------|
| Notebooks | Large performance improvement for large notebooks |
| Extension (new)  | [Azure Monitor Logs](extensions/azure-monitor-logs-extension.md) |
| Extension update | [SchemaCompare](extensions/schema-compare-extension.md) |
| Extension update | [SQLDatabaseProjects](extensions/sql-database-project-extension.md) |
| Extension update | [MachineLearning](extensions/machine-learning-extension.md) |
| Extension update | [Azure Arc](extensions/azure-arc-extension.md) |
| Extension update | Lang packs |

#### Bug fixes in 1.32.0

| New Item | Details |
|----------|---------|
| Database Projects | Fixed Seeing "Project already opened" message for every project I open or create |
| Schema Compare | Fixed Schema compare subsequent results after once are not loading in view |
| Lang packs | Fixed Localization for previously untranslated extensions. |
| Machine Learning | Fixed Machine Learning - View models dialog is broken |

For a full list of bug fixes addressed for the August 2021 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22August+2021+Release%22+is%3Aclosed).

#### Known issues in 1.32.0

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### July Hotfix 2021

- Release number: 1.31.1
- Release date: July 29, 2021

#### Bug fixes in 1.31.1

| New Item | Details |
|----------|---------|
| Connections | Fixed Database connection toolbar missing for sql scripts |
| Connections | Fixed Connection dropped / is not maintained when saving / opening scripts |
| Connections | Script file opened from command line does not allow DB connection|

For a full list of bug fixes addressed for the July Hotfix 2021 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/milestone/75).

#### Known issues in 1.31.1

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### July 2021

- Release number: 1.31.0
- Release date: July 21, 2021

#### What's new in 1.31.0

| New item | Details |
|----------|---------|
| Notebooks | WYSIWYG link improvements |
| Extension update | [SchemaCompare](extensions/schema-compare-extension.md) |
| Extension update | [SQLDatabaseProjects](extensions/sql-database-project-extension.md) |

#### Bug fixes in 1.31.0

| New Item | Details |
|----------|---------|
| Import Wizard | Fixed Import extension next button does not work in July release |
| Schema Compare| Fixed issue that Schema compare Select Source target dialog OK button not enabled |
| Notebooks | Fixed Export Notebook as SQL file has no query editor toolbar |
| SQL Server Big Data Clusters | Fixed Can't connect to BDC Clusters |
| Accessibility bug fixes | |

For a full list of bug fixes addressed for the July 2021 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/milestone/75).

#### Known issues in 1.31.0

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### June 2021

- Release number: 1.30.0
- Release date: June 17, 2021

#### What's new in 1.30.0

| New item | Details |
|----------|---------|
| Results Grid | Added filtering/sorting feature for query result grid in query editor and notebook, the feature can be invoked from the column headers. note that this feature is only available when you enable the preview features. |
| Results Grid | Added a status bar item to show summary of the selected cells if there are multiple numeric values |
| Notebooks | Added new book icon |
| Notebooks | Notebook URI Handler File Support |
| Python | Updated Python to 3.8.10 |

#### Bug fixes in 1.30.0

| New Item | Details |
|----------|---------|
| Notebooks | Fixed WYWIWYG Table cell adding new line in table cell |
| Notebooks | Fixed issue that Kusto notebook does not change kernels properly |

For a full list of bug fixes addressed for the May 2021 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/milestone/73?closed=1).

#### Known issues in 1.30.0

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### May 2021

- Release number: 1.29.0
- Release date: May 19, 2021

#### What's new in 1.29.0

| New item | Details |
|----------|---------|
| Notebooks | Added run with parameters action. Learn more [here](./notebooks/run-with-parameters.md) |

#### Bug fixes in 1.29.0

| New Item | Details |
|----------|---------|
| Database | Fixed an issue with the title not changing when the database connection changes. |
| Extensions | Fixed an issue with no longer prompting the user to install 3rd party extensions. |
| General Azure Data Studio | Fixed an issue with the account button in the sidebar getting stuck loading. |
| General Azure Data Studio | Fixed an issue with the *Run With Parameters* silently failing if the parameters cell is empty or invalid. |
| General Azure Data Studio | Fixed an issue with the *Run With Parameters* not handling multiple parameters on the same line. |
| General Azure Data Studio | Fixed an issue with being unable to connect to Azure. |
| General Azure Data Studio | Fixed an issue with the wrong line number showing up in the output. |
| General Azure Data Studio | Fixed an issue when receiving an error when connecting to an Azure server in the Azure viewlet. |
| General Azure Data Studio | Fixed an issue with the loading spinner not being animated. |
| General Azure Data Studio | Fixed an issue with the links inserted in the split view/MD view not being inserted as a list item. |
| General Azure Data Studio | Fixed an issue with the *Issue Reporter* being blank. |
| Extensions | Fixed an issue with the extensions view having many filter options that aren't applicable to Azure Data Studio. |
| General Azure Data Studio | Fixed an issue with getting the Azure subscriptions API failing across Azure Data Studio. |
| General Azure Data Studio | Fixed an issue with the wrong event prefix. |
| General Azure Data Studio | Fixed an issue with converting the default tables from Excel, Word, and OneNote into markdown tables. |
| Notebooks | Fixed an issue with selecting notebooks from the notebooks viewlet recentering the viewlet vertically. |
| Notebooks | Fixed an issue with not connecting to SQL Server from SQL Notebook. |
| Notebooks | Fixed an issue with the notebook icons being sized incorrectly. |
| PowerShell | Fixed an issue with using the Cloud Shell (PowerShell). |
| SQL Database Project | Fixed an issue with showing an unnecessary horizontal scrollbar in the *create project from database* dialog SQL Database Project dashboard update. |

For a full list of bug fixes addressed for the May 2021 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?page=3&q=is%3Aissue+is%3Aclosed+milestone%3A%22May+2021+Release%22).

#### Known issues in 1.29.0

For a list of the current known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### April 2021

April 15, 2021 &nbsp; / &nbsp; version: 1.28.0

#### What's new in 1.28.0

| New item | Details |
|----------|---------|
| Extension update | [Kusto (KQL)](extensions/kusto-extension.md) |
| Extension update | [MachineLearning](extensions/machine-learning-extension.md) |
| Extension update | [SchemaCompare](extensions/schema-compare-extension.md) |
| Extension update | [SQLDatabaseProjects](extensions/sql-database-project-extension.md) |
| Notebook features | Added *Add Notebook* and *Remove Notebook commands* |

#### Bug fixes in 1.28.0

For the list of the bug fixes addressed for the April 2021 release, visit the [bugs and issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?page=1&q=is%3Aissue+is%3Aclosed+milestone%3A%22April+2021+Release%22).

#### Known issues in 1.28.0

For the list of known issues, visit the [issues list on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue).

### March 2021

March 17, 2021 &nbsp; / &nbsp; version: 1.27.0

&nbsp;

| Change | Details |
| ------ | ------- |
| Bug Fixes | For a complete list of fixes, see [Bugs and issues on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+is%3Aclosed+milestone%3A%22March+2021+Release%22). |
| Extension(s) update | [Dacpac](extensions/sql-server-dacpac-extension.md) </br> [SQLDatabaseProjects](extensions/sql-database-project-extension.md) |
| New Notebook features | Added create book dialog |

### February 2021

February 18, 2021 &nbsp; / &nbsp; version: 1.26.0

&nbsp;

| Change | Details |
| ------ | ------- |
| Bug Fixes | For a complete list of fixes see [Bugs and issues on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22February+2021+Release%22+is%3Aclosed). |
| Extension(s) update | [Dacpac](extensions/sql-server-dacpac-extension.md) <br/> [Kusto (KQL)](extensions/kusto-extension.md) </br> [MachineLearning](extensions/machine-learning-extension.md) </br> [Profiler](extensions/sql-server-profiler-extension.md) </br> [SchemaCompare](extensions/schema-compare-extension.md) </br> [SQLDatabaseProjects](extensions/sql-database-project-extension.md) |
| New Azure Arc features | Multiple data controllers now supported <br/> New connection dialog options like the *kube config file* <br/> Postgres dashboard enhancements |
| New Notebook features | Improved Jupyter server start-up time by 50% on Windows <br/> Added support to edit Jupyter Books through right-click <br/> Added URI notebook parameterization support and [added notebook parameterization documentation](./notebooks/parameterize-papermill.md) |

### December 2020 (hotfix)

February 10, 2021 &nbsp; / &nbsp; version: 1.25.3

| Change | Details |
| ------ | ------- |
| Fix bug [#13899](https://github.com/microsoft/azuredatastudio/issues/13899) | Scrolling to the appropriate cross-reference links in Notebooks |
| Upgrade Electron to incorporate important bug fixes| N/A |

### December 2020

December 9, 2020 &nbsp; / &nbsp; version: 1.25.0

&nbsp;

| Change | Details |
| ------ | ------- |
| Bug Fixes | For a complete list of fixes see [Bugs and issues on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+is%3Aclosed+milestone%3A%22December+2020+Release%22). |
| Database Projects extension update | Added workspaces and improved sidebar |

### November 2020

November 12, 2020 &nbsp; / &nbsp; version: 1.24.0

&nbsp;

| Change | Details |
| ------ | ------- |
| Bug Fixes | For a complete list of fixes see [Bugs and issues on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22November+2020+Release%22+is%3Aclosed). |
| Connection dialog | Added new browse tab for connection dialog. |
| Extension(s) update | Released update to Postgres extension. |
| New notebook features | Added new features to SQL to notebook support. <br/> Added new features to Notebook parameterization support. <br/>  Added new features to results streaming for SQL Notebooks. |
| Python installation | PROSE package has been removed from default Python installation. |

### Known issues (1.24.0)

| New Item | Details | Workaround |
|----------|---------|------------|
| Azure Arc extension | [Known Issue:](https://github.com/microsoft/azuredatastudio/issues/13319) The "Script to Notebook" button for Arc MIAA & PG deployments doesn't do field validation before scripting the notebook. This means that if users enter the password wrong in the password confirm inputs then they may end up with a notebook that has the wrong value for the password.| The "Deploy" button works as expected though so users should use that instead. |
| Object Explorer | Releases of Azure Data Studio before 1.24.0 have a breaking change in object explorer because of the engine's changes related to [Azure Synapse Analytics serverless SQL pool](/azure/synapse-analytics/sql/on-demand-workspace-overview). | To continue utilizing object explorer in Azure Data Studio with Azure Synapse Analytics serverless SQL pool, you need to use Azure Data Studio 1.24.0 or later. |

You can reference [Azure Data Studio feedback](https://github.com/microsoft/azuredatastudio) for other known issues and to provide feedback to the product team.

### October 2020

October 14, 2020 &nbsp; / &nbsp; version: 1.23.0

&nbsp;

| Change | Details |
| ------ | ------- |
| Azure SQL Edge | Support for Azure SQL Edge objects. |
| Bug Fixes | For a complete list of fixes see [Bugs and issues on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is:issue+milestone:%22October+2020+Release%22+is:closed). |
| Databases| Support for same database reference. |
| Extension updates | [Azure Arc](extensions/azure-arc-extension.md)</br>[azdata](../azdata/install/deploy-install-azdata.md)</br>[Machine Learning](extensions/machine-learning-extension.md)</br>[Kusto (KQL)](extensions/kusto-extension.md)</br>[Schema Compare](extensions/schema-compare-extension.md)</br>SQL Assessment</br>[SQL Database Projects](extensions/sql-database-project-extension.md)</br>[SQL Server Import](extensions/sql-server-import-extension.md) |
| New deployment features | Added Azure SQL DB and VM deployments. |
| PowerShell | Added PowerShell kernel results streaming support. |

### September 2020 (hotfix)

September 30, 2020 &nbsp; / &nbsp; version: 1.22.1

&nbsp;

| Change | Details |
| ------ | ------- |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues on GitHub](https://github.com/microsoft/azuredatastudio/releases/tag/1.22.1). |

### September 2020

September 22, 2020 &nbsp; / &nbsp; version: 1.22.0

&nbsp;

| Change | Details |
| ------ | ------- |
| New notebook features | <br/> &bull; &nbsp; Supports brand new text cell editing experience based on rich text formatting and seamless conversion to markdown, also known as WYSIWYG toolbar (What You See Is What You Get) <br/> &bull; &nbsp; Supports Kusto kernel <br/> &bull; &nbsp; Supports pinning of notebooks <br/> &bull; &nbsp; Added support for new version of Jupyter Books <br/> &bull; &nbsp; Improved Jupyter Shortcuts <br/> &bull; &nbsp; Introduced perf loading improvements |
| SQL Database Projects extension | The SQL Database Projects extension brings project-based database development to Azure Data Studio. In this preview release, SQL projects can be created and published from Azure Data Studio. |
| Kusto (KQL) extension | Brings native Kusto experiences in Azure Data Studio for data exploration and data analytics against massive amount of real-time streaming data stored in Azure Data Explorer. This preview release supports connecting and browsing Azure Data Explorer clusters, writing KQL queries and authoring notebooks with Kusto kernel. |
| Azure Arc extension | Users can try out Azure Arc public preview through Azure Data Studio. This includes: <br/> &bull; &nbsp; Deploy data controller <br/> &bull; &nbsp; Deploy Postgres <br/> &bull; &nbsp; Deploy Managed Instance for Azure Arc <br/> &bull; &nbsp; Connect to data controller <br/> &bull; &nbsp; Access data service dashboards <br/> &bull; &nbsp; Azure Arc Jupyter Book |
| Deployment options | <br/> &bull; &nbsp; Azure SQL Database Edge <br/> (Edge will require Azure SQL Edge Deployment Extension) |
| SQL Server Import extension GA | Announcing the GA of the SQL Server Import extension, features no longer in preview. This extension facilitates importing csv/txt files. Learn more about the extension in [this article](./extensions/sql-server-import-extension.md). |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22September+2020+Release%22+is%3Aclosed). |

### August 2020

August 12, 2020 &nbsp; / &nbsp; version: 1.21.0

&nbsp;

| Change | Details |
| :----- | :------ |
| New notebook features | &bull; &nbsp; Move cell locations <br/> &bull; &nbsp; Convert cells to Text Cell or Code cell
| Jupyter Books picker | Users can now choose Jupyter Books from GitHub releases and open seamlessly in Azure Data Studio |
| Search added to Notebooks Viewlet | Users can easily search through content across their notebooks and Jupyter Books |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22August+2020+Release%22+is%3Aclosed). |

### July 2020 (hotfix)

July 17, 2020 &nbsp; / &nbsp; version: 1.20.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #11372 Object Explorer drag-and-drop table incorrectly wraps table names | [#11372](https://github.com/microsoft/azuredatastudio/issues/11372) |
| Fix bug #11356 Dark theme is now the default theme | [#11356](https://github.com/microsoft/azuredatastudio/issues/11356) |

### Known Issue

- Some users have reported connection errors from the new Microsoft.Data.SqlClient v2.0.0 included in this release. Users have found [following these instructions](https://github.com/microsoft/azuredatastudio/issues/11367#issuecomment-659614111) to successfully connect

### July 2020

July 15, 2020 &nbsp; / &nbsp; version: 1.20.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Added new Feature Tour | From welcome page and command palette, users can now launch a feature tour to get a walkthrough of commonly used features including Connections Viewlet, Notebooks viewlet, and Extensions Marketplace |
| New notebook features | &bull; &nbsp; Header support in Markdown Toolbar<br/> &bull; &nbsp; Side-by-side Markdown Preview in Text Cells
| Drag and Drop Columns and Tables in Query Editor | Users can now directly drag and drop columns and tables from connections viewlet to query editor |
| Added Azure Account icon to Activity Bar | Users can now easily see where to sign in to Azure |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22July+2020+Release%22+is%3Aclosed). |

### June 2020

June 15, 2020 &nbsp; / &nbsp; version: 1.19.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Added Azure Data Studio to Azure portal Integration | Users can now directly launch to Azure portal from an Azure SQL Database connection, Azure Postgres, and more. |
| New notebook features | &bull; &nbsp; New Notebook toolbar <br/> &bull; &nbsp; New Edit Cell toolbar <br/> &bull; &nbsp; Python dependencies wizard UX updates <br/> &bull; &nbsp; Improved spacing across notebooks |
| Announcing SQL Assessment API extension | This extension adds SQL Server best-practice assessment in Azure Data Studio. It exposes SQL Assessment API, which was previously available for use in PowerShell SqlServer module and SMO only, to let you evaluate your SQL Server instances and receive for them recommendations by SQL Server Team. Learn more about SQL Assessment API and what it's capable of [in this article.](../tools/sql-assessment-api/sql-assessment-api-overview.md) |
| [Machine Learning Extension improvements](./extensions/machine-learning-extension.md) | Now supports Azure SQL Managed Instance. |
| Data Virtualization extension improvements | Now supports MongoDB and Teradata |
| Postgres extension bug fixes | Fixed Azure MFA |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22June+2020+Release%22+is%3Aclosed). |

### May 2020 (hotfix)

May 27, 2020 &nbsp; / &nbsp; version: 1.18.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #10538 "Run Current Query" keybind no longer behaving as expected | [#10538](https://github.com/microsoft/azuredatastudio/issues/10538)  |
| Fix bug #10537 Unable to open new or existing sql files on v1.18 | [#10537](https://github.com/microsoft/azuredatastudio/issues/10537)  |

### May 2020

May 20, 2020 &nbsp; / &nbsp; version: 1.18.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Announcing Redgate SQL Prompt extension | This extension lets you manage formatting styles directly within Azure Data Studio, so you can create and edit your styles without leaving the IDE. |
| Announcing Machine Learning Extension | This extension enables you to: <br/> &bull; &nbsp; Manage Python and R packages with SQL Server machine learning services with Azure Data Studio.<br/> &bull; &nbsp; Use ONNX model to make predictions in Azure SQL Edge.<br/> &bull; &nbsp; View ONNX models in an Azure SQL Edge database. <br/> &bull; &nbsp; Import ONNX models from a file or Azure Machine Learning into Azure SQL Edge database. <br/> &bull; &nbsp; Create a notebook to run experiments. |
| New notebook features | &bull; &nbsp; Added new Python Dependencies Wizard to make it easier to install Python dependencies <br/> &bull; &nbsp; Added underline support for Markdown Toolbar |
| Parameterization for Always Encrypted | Allows you to run queries that insert, update, or filter by encrypted database columns.|
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22May+2020+Release%22+is%3Aclosed). |

### April 2020 (hotfix)

April 30, 2020 &nbsp; / &nbsp; version: 1.17.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #10197 Can't connect via MFA | [#10197](https://github.com/microsoft/azuredatastudio/issues/10197)  |

### April 2020

April 27, 2020 &nbsp; / &nbsp; version: 1.17.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Improved welcome page | UI update on the welcome page to make it easier to see common actions and highlighting extensions. |
| New notebook features | &bull; &nbsp; Added Markdown toolbar when editing text cells to help write with Markdown <br/> &bull; &nbsp; Revamped Jupyter Books viewlet to become a Notebooks viewlet where you can manage Jupyter Books and notebooks together <br/>&bull; &nbsp; Added support for persisting charts when saving a notebook <br/> &bull; &nbsp; Added support for KQL magic in Python notebooks|
| Improved dashboards | Dashboards throughout Azure Data Studio have been updated with latest design patterns, including an actions toolbar. This also applies to many extensions. |
| Added Cloud Shell integration in the Azure view. | |
| Support for Always Encrypted and Always Encrypted with secure enclaves. | |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+is%3Aclosed+milestone%3A%22April+2020+Release%22). |
| &nbsp; | &nbsp; |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+is%3Aclosed+milestone%3A%22April+2020+Release%22). |

### March 2020

March 18, 2020 &nbsp; / &nbsp; version: 1.16.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Added charting support in SQL Notebooks | When running a SQL query in a code cell, users can now create and save charts. |
| Added Create Jupyter Book experience | Users can now create their own Jupyter Books using a notebook. |
| Added Azure AD support for Postgres extension | |
| Fixed many accessibility bugs | [List of accessibility bugs](https://github.com/microsoft/azuredatastudio/issues?page=1&q=is%3Aissue+is%3Aclosed+milestone%3A%22S360+-+Accessibility%22+label%3AA11y_AzureDataStudio) |
| VS Code merges to 1.42 | This release includes updates to VS Code from the 3 previous VS Code releases. [Read their release notes](https://code.visualstudio.com/updates/v1_42) to learn more. |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22March+2020%22+is%3Aclosed). |

### February (hotfix)

February 19, 2020 &nbsp; / &nbsp; version: 1.15.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #9149 Show active connections | [#9149](https://github.com/microsoft/azuredatastudio/issues/9149)  |
| Fix bug #9061 Edit Data grid doesn't properly resize when showing or hiding SQL Pane | [#9061](https://github.com/microsoft/azuredatastudio/issues/9061)  |

### February 2020

February 13, 2020 &nbsp; / &nbsp; version: 1.15.0

&nbsp;

| Change | Details |
| :----- | :------ |
| New Azure Sign-in improvement | Added improved Azure Sign-in experience, including removal of copy/paste of device code to make a more seamless connected experience. |
| Find in Notebook support | Users can now use Ctrl+F in a notebook. Find in Notebook support searches line by line through both code and text cells. |
| VS Code merges from 1.38 to 1.42 | This release includes updates to VS Code from the 3 previous VS Code releases. [Read their release notes](https://code.visualstudio.com/updates/v1_42) to learn more. |
| Fix for the ["white/blank screen"](https://github.com/microsoft/azuredatastudio/issues/8775) issue reported by many users. | |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+is%3Aclosed+milestone%3A%22February+2020%22). |

### Known Issue

- Users on macOS Catalina will need to right-click Azure Data Studio and then click open.

### December 2019 (hotfix)

December 26, 2019 &nbsp; / &nbsp; version: 1.14.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #8747 OE Expansion fails | [#8747](https://github.com/microsoft/azuredatastudio/issues/8747)  |

### December 2019

December 19, 2019 &nbsp; / &nbsp; version: 1.14.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Changed attach to connection dropdown in Notebooks to only list the currently active connection | [#8129](https://github.com/microsoft/azuredatastudio/issues/8129) |
| Added bigdatacluster.ignoreSslVerification setting to allow ignoring TLS/SSL verification errors when connecting to a BDC | [#8582](https://github.com/microsoft/azuredatastudio/pull/8582) |
| Allow changing default language flavor for offline query editors | [#8419](https://github.com/microsoft/azuredatastudio/pull/8419) |
| GA status for Big Data Cluster/SQL 2019 features | [#8269](https://github.com/microsoft/azuredatastudio/issues/8269) |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/44?closed=1). |

### November 2019 (hotfix)

November 15, 2019 &nbsp; / &nbsp; version: 1.13.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #8210 Copy/Paste results are out of order |  |

### November 2019

November 4, 2019 &nbsp; / &nbsp; version: 1.13.0

&nbsp;

| Change | Details |
| :----- | :------ |
| New SQL Server 2019 support | &bull; &nbsp; Deploy SQL Server 2019 big data cluster with BDC Deploy wizard <br/>&bull; &nbsp; Manage cluster health with controller dashboard <br/>&bull; &nbsp; Manage HDFS access control lists using Security ACLs Dialog <br/> &bull; &nbsp; Add mounts using HDFS Tiering Dialog <br/> &bull; &nbsp; Troubleshoot using built-in Jupyter Book, SQL Server 2019 guide <br/> &bull; &nbsp; Renamed to SQL vNext extension Data virtualization extension <br/> &bull; &nbsp; Added Teradata and Mongo support in External Table Wizard|
| New notebook features | &bull; &nbsp; Announcing PowerShell notebooks <br/> &bull; &nbsp; Announcing collapsible code cells <br/>&bull; &nbsp; Perf improvements in Notebooks <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22November+2019+Release%22+is%3Aclosed+label%3A%22Area+-+Notebooks%22) |
| Announcing Jupyter Books  | Jupyter Books are a collection of notebooks and markdown files organized in a table of contents. |
| New SQL Server Deploy wizard  | Now includes support for deploying: <br/> &bull; &nbsp; SQL Server 2019 on Windows <br/> &bull; &nbsp; SQL Server 2017 on Windows <br/> &bull; &nbsp; SQL Server 2019 on Docker <br/> &bull; &nbsp; SQL Server 2017 on Docker |
| Announcing GA of Schema Compare extension| &bull; &nbsp; SQLCMD mode <br/> &bull; &nbsp; Localization support <br/> &bull; &nbsp; Accessibility fixes <br/> &bull; &nbsp; Security bugs  |
| Announcing GA of SQL Server Dacpac extension| <br/> &bull; &nbsp; Localization support <br/> &bull; &nbsp; Accessibility fixes <br/> &bull; &nbsp; Security bugs |
| Announcing Visual Studio IntelliCode extension | Visual Studio IntelliCode now supports SQL, which allows for smarter suggestions of reserved keywords. |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22November+2019+Release%22+is%3Aclosed). |

### October 2019 (hotfix 2)

October 11, 2019 &nbsp; / &nbsp; version: 1.12.2

&nbsp;

| Change | Details |
| :----- | :------ |
| Disable automatically starting the EH in inspect mode |  |

### October 2019 (hotfix)

October 8, 2019 &nbsp; / &nbsp; version: 1.12.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed issue for quotes and backslashes in Notebooks to escape correctly. |  |

### October 2019

October 2, 2019 &nbsp; / &nbsp; version: 1.12.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of Query History extension | The SQL History extension saves all past queries executed in an Azure Data Studio session and lists them in execution order. Users can see open the query, execute the query, delete the query, pause query history, or delete all query history entries. |
| New Copy/Paste Results | We have added more ways to copy/paste results from the results grid. |
| Update to PowerShell extension |  |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/42?closed=1). |

### Known Issues

- Notebooks
  - [7080](https://github.com/microsoft/azuredatastudio/issues/7080) Rare Case when Notebook is Serialized Incorrectly

### September 2019

September 10, 2019 &nbsp; / &nbsp; version: 1.11.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Enable SQLCMD Mode | Query editor now supports toggling of SQLCMD mode to write and edit queries as SQLCMD scripts |
| Community Extension: Query Editor Boost | Query Editor Boost is an open-source extension focused on enhancing the Azure Data Studio query editor for users who are frequently writing queries. &bull; &nbsp; Save the current query as a snippet <br/>&bull; &nbsp; Switch databases using Ctrl+U <br/> &bull; &nbsp; New Query from template <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/dzsquared/query-editor-boost) |
| Notebook Improvements | &bull; &nbsp; Performance improvements for supporting larger notebook files <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22September+2019+Release%22+label%3A%22Area%3A+Notebooks%22+is%3Aclosed) |
| Visual Studio Code August Release Merge 1.38 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_38). |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/39?closed=1). |

### Known Issues

- Notebooks
  - [7080](https://github.com/microsoft/azuredatastudio/issues/7080) Rare Case when Notebook is Serialized Incorrectly

### August 2019

August 15, 2019 &nbsp; / &nbsp; version: 1.10.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of SandDance 1.3.1 extension | &bull; &nbsp; Smart chart detection <br/>&bull; &nbsp; 3D Visualizations <br/> &bull; &nbsp; Data filtering |
| Notebook Improvements | &bull; &nbsp; Add code or text cell in-line <br/>&bull; &nbsp; Added ability to right-click SQL results grid to save result as CSV, JSON, etc. <br/> &bull; &nbsp; Improvement to notebook loading performance for loading JSON faster <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+label%3A%22Area%3A+Notebooks%22+milestone%3A%22August+2019+Release%22+is%3Aclosed) |
| SQL Server 2019 Support | This release includes support for extra SQL Server 2019 Big Data Cluster features including: <br/> &bull; &nbsp; Reduced time taken to load table and column information on the object-mapping page. <br/> &bull; &nbsp; Fixed a bug with loading existing database scoped credentials on the connection details page. <br/> &bull; &nbsp; Increased default sample size used for PROSE parsing. |
| Dacpac extension now supports Azure AD |
| Visual Studio Code July Release Merge 1.37 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_37). |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/39?closed=1). |

### July 2019

July 11, 2019 &nbsp; / &nbsp; version: 1.9.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of SentryOne Plan Explorer extension | Our valued Microsoft partner, SentryOne, will be shipping their [SentryOne Plan Explorer extension for Azure Data Studio](https://www.sentryone.com/products/sentryone-plan-explorer-extension-azure-data-studio). <br> This is a free extension, which provides enhanced plan diagrams for queries run in Azure Data Studio, with optimized layout algorithms and intuitive color-coding to help quickly identify the most expensive operators affecting query performance. To learn more about the extension, check out SentryOne's blog post [here](https://sqlperformance.com/2019/07/sentryone/plan-explorer-extension-azure-data-studio). |
| New Features coming to Schema Compare | &bull; &nbsp; Schema Compare File Support (.SCMP) <br/>&bull; &nbsp; Cancel Schema Compare Support <br/>&bull; &nbsp; Complete changes can be found [here](https://github.com/microsoft/azuredatastudio/issues?utf8=%E2%9C%93&q=is%3Aissue+milestone%3A%22July+2019+Release%22+label%3A%22Area%3A+Schema+Compare%22+is%3Aclosed+)|
| Notebook Improvements | &bull; &nbsp; Plotly Python Support <br/>&bull; &nbsp; Open Notebook from Browser <br/> &bull; &nbsp; Python Package Management Dialog <br/> &bull; &nbsp; Performance and Markdown Enhancements <br/> &bull; &nbsp; Keyboard Shortcuts Update <br/>  &bull; &nbsp; Bug Fixes and Minor Features can be found [here](https://github.com/microsoft/azuredatastudio/issues?utf8=%E2%9C%93&q=is%3Aissue+milestone%3A%22July+2019+Release%22+is%3Aclosed+label%3A%22Area%3A+Notebooks%22+) |
| SQL Server 2019 Support | This release includes support for extra SQL Server 2019 Big Data Cluster features including: <br/> &bull; &nbsp; Service Endpoints table within the Management Dashboard that lists all key services in the cluster. <br/> &bull; &nbsp; Cluster Status Notebook shows how you can query & troubleshoot cluster status across all services and pods.|
| Updated Language Packs Available| There are now 10 language packs available in the Extension Manager marketplace. Simply, search for the specific language using the extension marketplace and install. Once you install the selected language, Azure Data Studio will prompt you to restart with the new language. |
| SQL Server Profiler Update | The SQL Server Profile extension has been updated to include new features including: <br/> &bull; &nbsp; Filtering by Database Name <br/> &bull; &nbsp; Copy & Paste Support <br/> &bull; &nbsp; Save/Load Filter <br/>A full list of improvements for SQL Server Profiler Extension can be found [here](https://github.com/microsoft/azuredatastudio/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aclosed+milestone%3A%22July+2019+Release%22+label%3A%22Area%3A+SQL+Profiler%22+).  |
| Visual Studio Code May Release Merge 1.35 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_35). |
| Resolved bugs and issues | In previous releases of Azure Data Studio, if a user database was selected when connecting from the Connection dialog, the resulting Object Explorer entry was scoped entirely to that single database. Beginning in this release, that behavior is being changed so that server level properties are also shown in the object explorer. <br/> For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/35?closed=1). |

### June 2019

June 6, 2019 &nbsp; / &nbsp; version: 1.8.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of Central Management Servers (CMS) extension | Central Management Servers store a list of instances of SQL Server that is organized into one or more central management server groups. Users can connect to their own existing CMS servers and manage their servers like adding and removing servers. To learn more, you can read [here](../relational-databases/administer-multiple-servers-using-central-management-servers.md) |
| Release of Database Administration Tool Extensions for Windows | This extension launches two of the most used experiences in SQL Server Management Studio from Azure Data Studio. Users can right-click on many different objects (such as Databases, Tables, Columns, Views, and more) and select Properties to view the SSMS Properties Dialog for that object. In addition, users can right-click on a database and select Generate Scripts to launch the well-known SSMS Generate Scripts Wizard.
| Schema Compare Improvements | &bull; &nbsp; Added Exclude/Include Options <br/>&bull; &nbsp; Generate Script opens script after being generated <br/>&bull; &nbsp; Removed double scroll bars  <br/>&bull; &nbsp; Formatting and layout improvements <br/>&bull; &nbsp; Complete changes can be found [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22June+2019+Release%22+label%3A%22Area%3A+Schema+Compare%22+is%3Aclosed)|
| Moved Messages section to own tab | When users ran SQL queries, results and messages were on stacked panels. Now they are in separate tabs in one panel like in SSMS. |
| SQL Notebook Improvements | &bull; &nbsp; Users can now choose to use their own Python 3 or Anaconda installs in notebooks <br/>&bull; &nbsp; Multiple Stabilities + fit/finish fixes <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22June+2019+Release%22+is%3Aclosed+label%3A%22Area%3A+Notebooks%22)|
| Visual Studio Code April Release Merge 1.34 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_34) |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/32?closed=1). |

### Known Issues

- Database Administration Tool Extensions for Windows
  - Can't launch properties from disconnected server node
  - Can't launch properties for Azure servers
  - Not all objects have property dialogs
  - Dialogs take a long time to start up
  - Errors launching servers with some types of connections (such as Azure AD)
- Notebooks
  - [5838](https://github.com/microsoft/azuredatastudio/issues/5838) Allow users to use system Python for Notebooks
- Schema Compare
  - [5804](https://github.com/microsoft/azuredatastudio/issues/5804) Schema Compare tasks show default cancel context menu, which doesn'thing

### May 2019

May 8, 2019 &nbsp; / &nbsp; version: 1.7.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of Schema Compare extension | Schema Compare is a well-known feature in SQL Server Data Tools (SSDT), and its primary use case is to compare and visualize the differences between databases and .dacpac files and to execute actions to make them the same. |
| Moved Task view to Output Window | Users can now view the status of long running tasks like Backup, Restore, and Schema Compare in the Task view in Output window
| Added Welcome page | &bull; &nbsp; Links to common actions like New Query, New File, New Notebook <br/>&bull; &nbsp; Links to documentation and GitHub |
| SQL Notebook Improvements | &bull; &nbsp; Markdown rendering improvements, including better support for notes and tables <br/>&bull; &nbsp; Usability improvements to the toolbar <br/>&bull; &nbsp; Markdown links for trusted notebooks no longer require Cmd/Ctrl + click and can be clicked directly <br/>&bull; &nbsp; Improvements in cleaning up Jupyter processes after closing notebooks and reducing errors when starting multiple notebooks concurrently <br/>&bull; &nbsp; Improvements to SQL notebook connections to ensure errors don't occur when running 2 notebooks against the same database <br/>&bull; &nbsp; Improvements to notebook autoscrolling to the currently executing cell when clicking the Run Cells button from the toolbar <br/>&bull; &nbsp; General stability and performance improvements |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/31?closed=1). |

### April 2019

April 18, 2019 &nbsp; / &nbsp; version: 1.6.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Renamed **Servers** tab to **Connections** | |
| Moved Azure Resource Explorer as an Azure viewlet under Connections | Users can now view their Azure SQL instances through Azure viewlet in the Connections view and expand to view objects under each server or database.|
| SQL Notebook Improvements | &bull; &nbsp; Added button on toolbar to clear output for all cells <br/>&bull; &nbsp; Added button on toolbar to run all cells <br/>&bull; &nbsp; Fixed connection name instead of server name (if set) in the Attach to dropdown <br/>&bull; &nbsp; Fix for images in markdown not rendering when using relative image paths <br/>&bull; &nbsp; Improved functionality in notebook grids by adding double-click auto resize column size and improved mousewheel support <br/>&bull; &nbsp; Improvements to error handling and python install resiliency when installing python through notebooks <br/>&bull; &nbsp; Improvements to "select all" functionality when selecting notebook cells <br/>&bull; &nbsp; Improvements to notebook connections to prevent closing a notebook and impacting an object explorer connection <br/>&bull; &nbsp; Improved notebook experience to display a message to the user when notebook disconnected and needs a connection to run cells<br/>&bull; &nbsp; Improved support for unsaved notebooks to rehydrate in Azure Data Studio when Azure Data Studio is started again |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/26?closed=1). |

### March 2019 (Hotfix)

March 22, 2019 &nbsp; / &nbsp; version: 1.5.2 &nbsp; / &nbsp; Hotfix release

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed a few issues discovered in 1.5.1. | See [March Hotfix Release, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/28).<br/> <br/>&bull; &nbsp; Fixed issue where user couldn't close notebook opened from the "Open Notebook" task in the Dashboard <br/>&bull; &nbsp; Fixed issue where Notebook JSON has extra } after save <br/>&bull; &nbsp; Fixed issue where notebook grids weren't responding to theme changes <br/>&bull; &nbsp; Fixed issue where full notebook path was shown in the tab header. Now only the filename is shown. |

### March 2019

March 18, 2019 &nbsp; / &nbsp; version: 1.5.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Added [PostgreSQL extension for Azure Data Studio](./extensions/postgres-extension.md) | Supported features: <br/>&bull; &nbsp; Connection Dialog <br/>&bull; &nbsp; Object Explorer <br/>&bull; &nbsp; Query Editor <br/>&bull; &nbsp; Charting <br/>&bull; &nbsp; Dashboards <br/>&bull; &nbsp; Snippets <br/>&bull; &nbsp; Edit Data <br/>&bull; &nbsp; Notebooks |
| Added SQL Notebooks | Added SQL Kernel support to built-in Notebook viewer: <br/>&bull; &nbsp; Supports T-SQL <br/>&bull; &nbsp; Support PGSQL |
| Added PowerShell Extension | Brings over the [PowerShell extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.PowerShell) experience from VS Code.  |
| Added SQL Server dacpac extension  | Removes Data-Tier Application Wizard from SQL Server Import extension into a new extension.  |
| Added Community extension QueryPlan.show | Adds integration support to visualize query plans  |
| Updated SQL Server 2019 Preview extension | &bull; &nbsp; Jupyter Notebook support, specifically Python3, and Spark kernels, have moved into the core Azure Data Studio tool. <br/>&bull; &nbsp; Bug fixes to External Data Wizard |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/25?closed=1). |

### Known Issues

- [#4427](https://github.com/Microsoft/azuredatastudio/issues/4427): Clicking Run on Cell Before Kernel is Ready for Spark Results in Fatal Error
**Workaround:** Wait until kernels are loaded until running any cells
- [#4493](https://github.com/Microsoft/azuredatastudio/issues/4493): Azure Data Studio launched from SSMS using SQL auth - prompts user for password
**Workaround:** Use Windows Auth for now.
- [#4494](https://github.com/Microsoft/azuredatastudio/issues/4494): Unable to install SQL notebook feature <br/>
**Workaround:** Follow workaround steps [here](https://github.com/Microsoft/azuredatastudio/issues/4494#issuecomment-473043832).
- [#4503](https://github.com/Microsoft/azuredatastudio/issues/4503): Azure Data Studio can't be Opened Directly from DownloAzure Data Studio Folder (Mac) <br />
**Workaround:** Restart computer after unzipping the app. Will be investigated.
- [#4539](https://github.com/Microsoft/azuredatastudio/issues/4539):  Notebook Save As loses connection context <br />
**Workaround:** Will be fixed in next release.
- [#4458](https://github.com/Microsoft/azuredatastudio/issues/4458): Dacpac Extract crashes SqlToolsService if invalid version is used <br/>
**Workaround:** Restart Azure Data Studio and ensure correct version is used.
- New Notebook and Open Notebook icons are lost <br/>
**Workaround:** The legacy connection type is deprecated. We recommend connecting to the SQL Server endpoint and you'll get all the actions (New Notebook, Spark Job) as expected.

### February 2019

February 13, 2019 &nbsp; / &nbsp; version: 1.4.5

&nbsp;

| Change | Details |
| :----- | :------ |
| Added **Admin pack for SQL Server** extension pack. | This makes it easier to install SQL Server admin-related extensions. This includes:<br/>&bull; &nbsp; [SQL Server Agent](./extensions/sql-server-agent-extension.md)<br/>&bull; &nbsp; [SQL Server Profiler](./extensions/sql-server-profiler-extension.md)<br/>&bull; &nbsp; [SQL Server Import](./extensions/sql-server-import-extension.md) |
| Added filtering extended event support in Profiler extension. | &nbsp; |
| Added Save as XML feature that can save T-SQL results as XML. | &nbsp; |
| Added Data-Tier Application Wizard improvements. | &bull; &nbsp; Added Generate script button<br/>&bull; &nbsp; Added view to give warnings of possible data loss during deployment. |
| Updates to the SQL Server 2019 Preview extension. | See [Data Virtualization extension](./extensions/data-virtualization-extension.md). |
| Results streaming enabled by default for long running queries. | &nbsp; |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/23?closed=1). |

### January 2019 (Hotfix)

January 16, 2019 &nbsp; / &nbsp; version: 1.3.9 &nbsp; / &nbsp; Hotfix release

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed a few issues discovered in 1.3.8. | See [January Hotfix Release, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/24?closed=1).<br/><br/>For detailed information, see:<br/>&bull; &nbsp; [Change Log, on GitHub](https://github.com/Microsoft/azuredatastudio/blob/main/CHANGELOG.md).<br/>&bull; &nbsp; [Releases, on GitHub](https://github.com/Microsoft/azuredatastudio/releases). |

### January 2019

January 09, 2019 &nbsp; / &nbsp; version: 1.3.8

&nbsp;

| Change | Details |
| :----- | :------ |
| Added a new user installer for Windows. | Unlike the existing system installer, the new user installer doesn't require administrator privileges. This also enables an easier upgrade experience for non-administrators. |
| Added Azure Active Directory Authentication support. | &nbsp; |
| Announcing Idera SQL DM Performance Insights (preview). | &nbsp; |
| Data-Tier Application Wizard support in SQL Server Import extension. | &nbsp; |
| Update to the SQL Server 2019 Preview extension. | See [Data Virtualization extension](./extensions/data-virtualization-extension.md). |
| SQL Server Profiler improvements. | &nbsp; |
| Results Streaming for large queries (preview). | &nbsp; |
| Community extensions: sp_executesql to sql and New Database. | &nbsp; |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/19?closed=1). |

## Next steps

See one of the following quickstarts to get started:

- [Download Azure Data Studio](download-azure-data-studio.md)
- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Synapse Analytics](quickstart-sql-dw.md)

Contribute to Azure Data Studio:

- [https://github.com/Microsoft/azuredatastudio](https://github.com/Microsoft/azuredatastudio)
