---
title: Release notes
description: This article has release notes for Azure Data Studio releases from November, 2017, until now. For many of the summarized issues there are links to additional details.
ms.prod: azure-data-studio
ms.technology: 
ms.topic: conceptual
author: yualan
ms.author: alayu
ms.reviewer: maghan
ms.custom: seodec18
ms.date: 07/17/2020
---

# Release notes for Azure Data Studio

**[Download and install the latest release!](download.md)**

## July 2020 (hotfix)

July 17, 2020 &nbsp; / &nbsp; version: 1.20.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #11372 Object Explorer drag-and-drop table incorrectly wraps table names | [#11372](https://github.com/microsoft/azuredatastudio/issues/11372)  |
| Fix bug #11356 Dark theme is now the default theme | [#11356](https://github.com/microsoft/azuredatastudio/issues/11356)  |
| &nbsp; | &nbsp; |

### Known Issue

- Some users have reported connection errors from the new Microsoft.Data.SqlClient v2.0.0 included in this release. Users have found [following these instructions](https://github.com/microsoft/azuredatastudio/issues/11367#issuecomment-659614111) to successfully connect

## July 2020

July 15, 2020 &nbsp; / &nbsp; version: 1.20.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Added new Feature Tour | From welcome page and command palette, users can now launch a feature tour to get a walkthrough of commonly used features including Connections Viewlet, Notebooks viewlet, and Extensions Marketplace |
| New notebook features | &bull; &nbsp; Header support in Markdown Toolbar<br/> &bull; &nbsp; Side-by-side Markdown Preview in Text Cells
| Drag and Drop Columns and Tables in Query Editor | Users can now directly drag and drop columns and tables from connections viewlet to query editor |
| Added Azure Account icon to Activity Bar | Users can now easily see where to sign in to Azure |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22July+2020+Release%22+is%3Aclosed). |
| &nbsp; | &nbsp; |


## June 2020

June 15, 2020 &nbsp; / &nbsp; version: 1.19.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Added Azure Data Studio to Azure portal Integration | Users can now directly launch to Azure portal from an Azure SQL DB connection, Azure Postgres, and more. |
| New notebook features | &bull; &nbsp; New Notebook toolbar <br/> &bull; &nbsp; New Edit Cell toolbar <br/> &bull; &nbsp; Python dependencies wizard UX updates <br/> &bull; &nbsp; Improved spacing across notebooks |
| Announcing SQL Assessment API extension | This extension adds SQL Server best-practice assessment in ADS. It exposes SQL Assessment API, which was previously available for use in PowerShell SqlServer module and SMO only, to let you evaluate your SQL Server instances and receive for them recommendations by SQL Server Team. Learn more about SQL Assessment API and what it is capable of [in this article.](https://docs.microsoft.com/sql/sql-assessment-api/sql-assessment-api-overview?view=sql-server-ver15) |
| [Machine Learning Extension improvements](https://go.microsoft.com/fwlink/?linkid=2129918) | Now supports Azure SQL Managed Instance. |
| Data Virtualization extension improvements | Now supports MongoDB and Teradata |
| Postgres extension bug fixes | Fixed Azure MFA |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22June+2020+Release%22+is%3Aclosed). |
| &nbsp; | &nbsp; |

## May 2020 (hotfix)

May 27, 2020 &nbsp; / &nbsp; version: 1.18.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #10538 "Run Current Query" keybind no longer behaving as expected | [#10538](https://github.com/microsoft/azuredatastudio/issues/10538)  |
| Fix bug #10537 Unable to open new or existing sql files on v1.18 | [#10537](https://github.com/microsoft/azuredatastudio/issues/10537)  |
| &nbsp; | &nbsp; |

## May 2020

May 20, 2020 &nbsp; / &nbsp; version: 1.18.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Announcing Redgate SQL Prompt extension | This extension lets you manage formatting styles directly within Azure Data Studio, so you can create and edit your styles without leaving the IDE. |
| Announcing Machine Learning Extension | This extension enables you to: <br/> &bull; &nbsp; Manage Python and R packages with SQL Server machine learning services with Azure Data Studio.<br/> &bull; &nbsp; Use ONNX model to make predictions in Azure SQL Edge.<br/> &bull; &nbsp; View ONNX models in an Azure SQL Edge database. <br/> &bull; &nbsp; Import ONNX models from a file or Azure Machine Learning into Azure SQL Edge database. <br/> &bull; &nbsp; Create a notebook to run experiments. |
| New notebook features | &bull; &nbsp; Added new Python Dependencies Wizard to make it easier to install Python dependencies <br/> &bull; &nbsp; Added underline support for Markdown Toolbar |
| Parameterization for Always Encrypted | Allows you to run queries that insert, update, or filter by encrypted database columns.|
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22May+2020+Release%22+is%3Aclosed). |
| &nbsp; | &nbsp; |

## April 2020 (hotfix)

April 30, 2020 &nbsp; / &nbsp; version: 1.17.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #10197 Can't connect via MFA | [#10197](https://github.com/microsoft/azuredatastudio/issues/10197)  |
| &nbsp; | &nbsp; |

## April 2020

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
| &nbsp; | &nbsp; |

## March 2020

March 18, 2020 &nbsp; / &nbsp; version: 1.16.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Added charting support in SQL Notebooks | When running a SQL query in a code cell, users can now create and save charts. |
| Added Create Jupyter Book experience | Users can now create their own Jupyter Books using a notebook. |
| Added AAD support for Postgres extension | |
| Fixed many accessibility bugs | [List of accessibility bugs](https://github.com/microsoft/azuredatastudio/issues?page=1&q=is%3Aissue+is%3Aclosed+milestone%3A%22S360+-+Accessibility%22+label%3AA11y_AzureDataStudio) |
| VS Code merge to 1.42 | This release includes updates to VS Code from the 3 previous VS Code releases. [Read their release notes](https://code.visualstudio.com/updates/v1_42) to learn more. |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22March+2020%22+is%3Aclosed). |
| &nbsp; | &nbsp; |

## February (hotfix)

February 19, 2020 &nbsp; / &nbsp; version: 1.15.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #9149 Show active connections | [#9149](https://github.com/microsoft/azuredatastudio/issues/9149)  |
| Fix bug #9061 Edit Data grid doesn't properly resize when showing or hiding SQL Pane | [#9061](https://github.com/microsoft/azuredatastudio/issues/9061)  |
| &nbsp; | &nbsp; |

## February 2020

February 13, 2020 &nbsp; / &nbsp; version: 1.15.0

&nbsp;

| Change | Details |
| :----- | :------ |
| New Azure Sign-in improvement | Added improved Azure Sign-in experience, including removal of copy/paste of device code to make a more seamless connected experience. |
| Find in Notebook support | Users can now use Ctrl+F inside of a notebook. Find in Notebook support searches line by line through both code and text cells. |
| VS Code merge from 1.38 to 1.42 | This release includes updates to VS Code from the 3 previous VS Code releases. [Read their release notes](https://code.visualstudio.com/updates/v1_42) to learn more. |
| Fix for the ["white/blank screen"](https://github.com/microsoft/azuredatastudio/issues/8775) issue reported by many users. | |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+is%3Aclosed+milestone%3A%22February+2020%22). |
| &nbsp; | &nbsp; |

### Known Issue

- Users on macOS Catalina will need to right-click Azure Data Studio and then click open.

## December 2019 (hotfix)

December 26, 2019 &nbsp; / &nbsp; version: 1.14.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #8747 OE Expansion fails | [#8747](https://github.com/microsoft/azuredatastudio/issues/8747)  |
| &nbsp; | &nbsp; |

## December 2019

December 19, 2019 &nbsp; / &nbsp; version: 1.14.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Changed attach to connection dropdown in Notebooks to only list the currently active connection | [#8129](https://github.com/microsoft/azuredatastudio/issues/8129) |
| Added bigdatacluster.ignoreSslVerification setting to allow ignoring TLS/SSL verification errors when connecting to a BDC | [#8582](https://github.com/microsoft/azuredatastudio/pull/8582) |
| Allow changing default language flavor for offline query editors | [#8419](https://github.com/microsoft/azuredatastudio/pull/8419) |
| GA status for Big Data Cluster/SQL 2019 features | [#8269](https://github.com/microsoft/azuredatastudio/issues/8269) |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/44?closed=1). |
| &nbsp; | &nbsp; |


## November 2019 (hotfix)

November 15, 2019 &nbsp; / &nbsp; version: 1.13.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #8210 Copy/Paste results are out of order |  |
| &nbsp; | &nbsp; |

## November 2019

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
| &nbsp; | &nbsp; |

## October 2019 (hotfix 2)

October 11, 2019 &nbsp; / &nbsp; version: 1.12.2

&nbsp;

| Change | Details |
| :----- | :------ |
| Disable automatically starting the EH in inspect mode |  |
| &nbsp; | &nbsp; |

## October 2019 (hotfix)

October 8, 2019 &nbsp; / &nbsp; version: 1.12.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed issue for quotes and backslashes in Notebooks to escape correctly. |  |
| &nbsp; | &nbsp; |

## October 2019

October 2, 2019 &nbsp; / &nbsp; version: 1.12.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of Query History extension | The SQL History extension saves all past queries executed in an Azure Data Studio session and lists them in execution order. Users can see open the query, execute the query, delete the query, pause query history, or delete all query history entries. |
| New Copy/Paste Results | We have added additional ways to copy/paste results from the results grid. |
| Update to PowerShell extension |  |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/42?closed=1). |
| &nbsp; | &nbsp; |

### Known Issues

- Notebooks
  - [7080](https://github.com/microsoft/azuredatastudio/issues/7080) Rare Case when Notebook is Serialized Incorrectly

## September 2019

September 10, 2019 &nbsp; / &nbsp; version: 1.11.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Enable SQLCMD Mode | Query editor now supports toggling of SQLCMD mode to write and edit queries as SQLCMD scripts |
| Community Extension: Query Editor Boost | Query Editor Boost is an open-source extension focused on enhancing the Azure Data Studio query editor for users who are frequently writing queries. &bull; &nbsp; Save the current query as a snippet <br/>&bull; &nbsp; Switch databases using Ctrl+U <br/> &bull; &nbsp; New Query from template <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/dzsquared/query-editor-boost) |
| Notebook Improvements | &bull; &nbsp; Performance improvements for supporting larger notebook files <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22September+2019+Release%22+label%3A%22Area%3A+Notebooks%22+is%3Aclosed) |
| Visual Studio Code August Release Merge 1.38 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_38). |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/39?closed=1). |
| &nbsp; | &nbsp; |

### Known Issues

- Notebooks
  - [7080](https://github.com/microsoft/azuredatastudio/issues/7080) Rare Case when Notebook is Serialized Incorrectly

## August 2019

August 15, 2019 &nbsp; / &nbsp; version: 1.10.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of SandDance 1.3.1 extension | &bull; &nbsp; Smart chart detection <br/>&bull; &nbsp; 3D Visualizations <br/> &bull; &nbsp; Data filtering |
| Notebook Improvements | &bull; &nbsp; Add code or text cell in-line <br/>&bull; &nbsp; Added ability to right-click SQL results grid to save result as CSV, JSON, etc. <br/> &bull; &nbsp; Improvement to notebook loading performance for loading JSON faster <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+label%3A%22Area%3A+Notebooks%22+milestone%3A%22August+2019+Release%22+is%3Aclosed) |
| SQL Server 2019 Support |  This release includes support for additional SQL Server 2019 Big Data Cluster features including: <br/> &bull; &nbsp; Reduced time taken to load table and column information on the object-mapping page. <br/> &bull; &nbsp; Fixed a bug with loading existing database scoped credentials on the connection details page. <br/> &bull; &nbsp; Increased default sample size used for PROSE parsing. | 
| Dacpac extension now supports AAD | 
| Visual Studio Code July Release Merge 1.37 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_37). |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/39?closed=1). |
| &nbsp; | &nbsp; |

## July 2019

July 11, 2019 &nbsp; / &nbsp; version: 1.9.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of SentryOne Plan Explorer extension | Our valued Microsoft partner, SentryOne, will be shipping their [SentryOne Plan Explorer extension for Azure Data Studio](https://www.sentryone.com/products/sentryone-plan-explorer-extension-azure-data-studio). <br> This is a free extension, which provides enhanced plan diagrams for queries run in Azure Data Studio, with optimized layout algorithms and intuitive color-coding to help quickly identify the most expensive operators affecting query performance. To learn more about the extension, check out SentryOne's blog post [here](https://sqlperformance.com/2019/07/sentryone/plan-explorer-extension-azure-data-studio). |
| New Features coming to Schema Compare | &bull; &nbsp; Schema Compare File Support (.SCMP) <br/>&bull; &nbsp; Cancel Schema Compare Support <br/>&bull; &nbsp; Complete changes can be found [here](https://github.com/microsoft/azuredatastudio/issues?utf8=%E2%9C%93&q=is%3Aissue+milestone%3A%22July+2019+Release%22+label%3A%22Area%3A+Schema+Compare%22+is%3Aclosed+)|
| Notebook Improvements | &bull; &nbsp; Plotly Python Support <br/>&bull; &nbsp; Open Notebook from Browser <br/> &bull; &nbsp; Python Package Management Dialog <br/> &bull; &nbsp; Performance and Markdown Enhancements <br/> &bull; &nbsp; Keyboard Shortcuts Update <br/>  &bull; &nbsp; Bug Fixes and Minor Features can be found [here](https://github.com/microsoft/azuredatastudio/issues?utf8=%E2%9C%93&q=is%3Aissue+milestone%3A%22July+2019+Release%22+is%3Aclosed+label%3A%22Area%3A+Notebooks%22+) |
| SQL Server 2019 Support |  This release includes support for additional SQL Server 2019 Big Data Cluster features including: <br/> &bull; &nbsp; Service Endpoints table within the Management Dashboard that lists all key services in the cluster. <br/> &bull; &nbsp; Cluster Status Notebook shows how you can query & troubleshoot cluster status across all services and pods.| 
| Updated Language Packs Available| There are now 10 language packs available in the Extension Manager marketplace. Simply, search for the specific language using the extension marketplace and install. Once you install the selected language, Azure Data Studio will prompt you to restart with the new language. |
| SQL Server Profiler Update | The SQL Server Profile extension has been updated to include new features including: <br/> &bull; &nbsp; Filtering by Database Name <br/> &bull; &nbsp; Copy & Paste Support <br/> &bull; &nbsp; Save/Load Filter <br/>A full list of improvements for SQL Server Profiler Extension can be found [here](https://github.com/microsoft/azuredatastudio/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aclosed+milestone%3A%22July+2019+Release%22+label%3A%22Area%3A+SQL+Profiler%22+).  |
| Visual Studio Code May Release Merge 1.35 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_35). |
| Resolved bugs and issues | In previous releases of Azure Data Studio, if a user database was selected when connecting from the Connection dialog, the resulting Object Explorer entry was scoped entirely to that single database. Beginning in this release, that behavior is being changed so that server level properties are also shown in the object explorer. <br/> For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/35?closed=1). |
| &nbsp; | &nbsp; |

## June 2019

June 6, 2019 &nbsp; / &nbsp; version: 1.8.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of Central Management Servers (CMS) extension | Central Management Servers store a list of instances of SQL Server that is organized into one or more central management server groups. Users can connect to their own existing CMS servers and manage their servers like adding and removing servers. To learn more, you can read [here](https://docs.microsoft.com/sql/relational-databases/administer-multiple-servers-using-central-management-servers) |
| Release of Database Administration Tool Extensions for Windows | This extension launches two of the most used experiences in SQL Server Management Studio from Azure Data Studio. Users can right-click on many different objects (such as Databases, Tables, Columns, Views, and more) and select Properties to view the SSMS Properties Dialog for that object. In addition, users can right-click on a database and select Generate Scripts to launch the well-known SSMS Generate Scripts Wizard. 
| Schema Compare Improvements | &bull; &nbsp; Added Exclude/Include Options <br/>&bull; &nbsp; Generate Script opens script after being generated <br/>&bull; &nbsp; Removed double scroll bars  <br/>&bull; &nbsp; Formatting and layout improvements <br/>&bull; &nbsp; Complete changes can be found [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22June+2019+Release%22+label%3A%22Area%3A+Schema+Compare%22+is%3Aclosed)|
| Moved Messages section to own tab | When users ran SQL queries, results and messages were on stacked panels. Now they are in separate tabs in one panel like in SSMS. |
| SQL Notebook Improvements | &bull; &nbsp; Users can now choose to use their own Python 3 or Anaconda installs in notebooks <br/>&bull; &nbsp; Multiple Stability + fit/finish fixes <br/> &bull; &nbsp; View the full list of improvements [here](https://github.com/microsoft/azuredatastudio/issues?q=is%3Aissue+milestone%3A%22June+2019+Release%22+is%3Aclosed+label%3A%22Area%3A+Notebooks%22)|
| Visual Studio Code April Release Merge 1.34 | Latest improvements can be found [here](https://code.visualstudio.com/updates/v1_34) |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/32?closed=1). |
| &nbsp; | &nbsp; |

### Known Issues

- Database Administration Tool Extensions for Windows
    - Can't launch properties from disconnected server node
    - Can't launch properties for Azure servers
    - Not all objects have property dialogs
    - Dialogs take a long time to start up
    - Errors launching servers with some types of connections (such as AAD)
- Notebooks
    - [5838](https://github.com/microsoft/azuredatastudio/issues/5838) Allow users to use system Python for Notebooks
- Schema Compare
    - [5804](https://github.com/microsoft/azuredatastudio/issues/5804) Schema Compare tasks show default cancel context menu, which does nothing

## May 2019

May 8, 2019 &nbsp; / &nbsp; version: 1.7.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of Schema Compare extension | Schema Compare is a well-known feature in SQL Server Data Tools (SSDT), and its primary use case is to compare and visualize the differences between databases and .dacpac files and to execute actions to make them the same. |
| Moved Task view to Output Window | Users can now view the status of long running tasks like Backup, Restore, and Schema Compare in the Task view in Output window
| Added Welcome page | &bull; &nbsp; Links to common actions like New Query, New File, New Notebook <br/>&bull; &nbsp; Links to documentation and GitHub |
| SQL Notebook Improvements | &bull; &nbsp; Markdown rendering improvements, including better support for notes and tables <br/>&bull; &nbsp; Usability improvements to the toolbar <br/>&bull; &nbsp; Markdown links for trusted notebooks no longer require Cmd/Ctrl + click and can be clicked directly <br/>&bull; &nbsp; Improvements in cleaning up Jupyter processes after closing notebooks and reducing errors when starting multiple notebooks concurrently <br/>&bull; &nbsp; Improvements to SQL notebook connections to ensure errors do not occur when running 2 notebooks against the same database <br/>&bull; &nbsp; Improvements to notebook autoscrolling to the currently executing cell when clicking the Run Cells button from the toolbar <br/>&bull; &nbsp; General stability and performance improvements |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/31?closed=1). |
| &nbsp; | &nbsp; |

## April 2019

April 18, 2019 &nbsp; / &nbsp; version: 1.6.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Renamed **Servers** tab to **Connections** | |
| Moved Azure Resource Explorer as an Azure viewlet under Connections | Users can now view their Azure SQL instances through Azure viewlet in the Connections view and expand to view objects under each server or database.|
| SQL Notebook Improvements | &bull; &nbsp; Added button on toolbar to clear output for all cells <br/>&bull; &nbsp; Added button on toolbar to run all cells <br/>&bull; &nbsp; Fixed connection name instead of server name (if set) in the Attach to dropdown <br/>&bull; &nbsp; Fix for images in markdown not rendering when using relative image paths <br/>&bull; &nbsp; Improved functionality in notebook grids by adding double-click auto resize column size and improved mousewheel support <br/>&bull; &nbsp; Improvements to error handling and python install resiliency when installing python through notebooks <br/>&bull; &nbsp; Improvements to "select all" functionality when selecting notebook cells <br/>&bull; &nbsp; Improvements to notebook connections to prevent closing a notebook and impacting an object explorer connection <br/>&bull; &nbsp; Improved notebook experience to display a message to the user when notebook disconnected and needs a connection to run cells<br/>&bull; &nbsp; Improved support for unsaved notebooks to rehydrate in ADS when ADS is started again |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/26?closed=1). |
| &nbsp; | &nbsp; |

## March 2019 (Hotfix)

March 22, 2019 &nbsp; / &nbsp; version: 1.5.2 &nbsp; / &nbsp; Hotfix release

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed a few issues discovered in 1.5.1. | See [March Hotfix Release, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/28).<br/> <br/>&bull; &nbsp; Fixed issue where user could not close notebook opened from the "Open Notebook" task in the Dashboard <br/>&bull; &nbsp; Fixed issue where Notebook JSON has extra } after save <br/>&bull; &nbsp; Fixed issue where notebook grids were not responding to theme changes <br/>&bull; &nbsp; Fixed issue where full notebook path was shown in the tab header. Now only the filename is shown. |
| &nbsp; | &nbsp; |

## March 2019

March 18, 2019 &nbsp; / &nbsp; version: 1.5.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Added [PostgreSQL extension for Azure Data Studio](postgres-extension.md) | Supported features: <br/>&bull; &nbsp; Connection Dialog <br/>&bull; &nbsp; Object Explorer <br/>&bull; &nbsp; Query Editor <br/>&bull; &nbsp; Charting <br/>&bull; &nbsp; Dashboards <br/>&bull; &nbsp; Snippets <br/>&bull; &nbsp; Edit Data <br/>&bull; &nbsp; Notebooks |
| Added SQL Notebooks | Added SQL Kernel support to built-in Notebook viewer: <br/>&bull; &nbsp; Supports T-SQL <br/>&bull; &nbsp; Support PGSQL |
| Added PowerShell Extension  | Brings over the [PowerShell extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.PowerShell) experience from VS Code.  |
| Added SQL Server dacpac extension  | Removes Data-Tier Application Wizard from SQL Server Import extension into a new extension.  |
| Added Community extension QueryPlan.show | Adds integration support to visualize query plans  |
| Updated SQL Server 2019 Preview extension | &bull; &nbsp; Jupyter Notebook support, specifically Python3, and Spark kernels, have moved into the core Azure Data Studio tool. <br/>&bull; &nbsp; Bug fixes to External Data Wizard  |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/25?closed=1). |
| &nbsp; | &nbsp; |

### Known Issues
- [#4427](https://github.com/Microsoft/azuredatastudio/issues/4427): Clicking Run on Cell Before Kernel is Ready for Spark Results in Fatal Error 
**Workaround:** Wait until kernels are loaded until running any cells
- [#4493](https://github.com/Microsoft/azuredatastudio/issues/4493): ADS launched from SSMS using SQL auth - prompts user for password 
**Workaround:** Use Windows Auth for now. 
- [#4494](https://github.com/Microsoft/azuredatastudio/issues/4494): Unable to install SQL notebook feature <br/>
**Workaround:** Follow workaround steps [here](https://github.com/Microsoft/azuredatastudio/issues/4494#issuecomment-473043832). 
- [#4503](https://github.com/Microsoft/azuredatastudio/issues/4503): Azure Data Studio can't be Opened Directly from Downloads Folder (Mac) <br />
**Workaround:** Restart computer after unzipping the app. Will be investigated. 
- [#4539](https://github.com/Microsoft/azuredatastudio/issues/4539):  Notebook Save As loses connection context <br />
**Workaround:** Will be fixed in next release. 
- [#4458](https://github.com/Microsoft/azuredatastudio/issues/4458): Dacpac Extract crashes SqlToolsService if invalid version is used <br/>
**Workaround:** Restart Azure Data Studio and ensure correct version is used.
- New Notebook and Open Notebook icons are lost <br/>
**Workaround:** The legacy connection type is deprecated. We recommend connecting to the SQL Server endpoint and you'll get all the actions (New Notebook, Spark Job) as expected. 

## February 2019

February 13, 2019 &nbsp; / &nbsp; version: 1.4.5

&nbsp;

| Change | Details |
| :----- | :------ |
| Added **Admin pack for SQL Server** extension pack. | This makes it easier to install SQL Server admin-related extensions. This includes:<br/>&bull; &nbsp; [SQL Server Agent](sql-server-agent-extension.md?view=sql-server-2017)<br/>&bull; &nbsp; [SQL Server Profiler](https://docs.microsoft.com/sql/azure-data-studio/sql-server-profiler-extension)<br/>&bull; &nbsp; [SQL Server Import](sql-server-import-extension.md?view=sql-server-2017) |
| Added filtering extended event support in Profiler extension. | &nbsp; |
| Added Save as XML feature that can save T-SQL results as XML. | &nbsp; |
| Added Data-Tier Application Wizard improvements. | &bull; &nbsp; Added Generate script button<br/>&bull; &nbsp; Added view to give warnings of possible data loss during deployment. |
| Updates to the SQL Server 2019 Preview extension. | See [Data Virtualization extension](data-virtualization-extension.md?view=sql-server-ver15). |
| Results streaming enabled by default for long running queries. | &nbsp; |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/23?closed=1). |
| &nbsp; | &nbsp; |

## January 2019 (Hotfix)

January 16, 2019 &nbsp; / &nbsp; version: 1.3.9 &nbsp; / &nbsp; Hotfix release

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed a few issues discovered in 1.3.8. | See [January Hotfix Release, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/24?closed=1).<br/><br/>For detailed information, see:<br/>&bull; &nbsp; [Change Log, on GitHub](https://github.com/Microsoft/azuredatastudio/blob/main/CHANGELOG.md).<br/>&bull; &nbsp; [Releases, on GitHub](https://github.com/Microsoft/azuredatastudio/releases). |
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
| Update to the SQL Server 2019 Preview extension. | See [Data Virtualization extension](data-virtualization-extension.md?view=sql-server-ver15). |
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
| Update to the SQL Server 2019 Preview extension. | See [Data Virtualization extension](data-virtualization-extension.md?view=sql-server-ver15). |
| Introducing Paste the Plan extension. | &nbsp; |
| Introducing High Color queries extension, including SSMS editor theme. | &nbsp; |
| Fixes in SQL Server Agent, Profiler, and Import extensions. | &nbsp; |
| Fix .NET Core Socket KeepAlive issue causing dropped inactive connections on macOS. | &nbsp; |
| Upgrade SQL Tools Service to .NET Core 2.2 Preview 3 (for eventual AAD support). | &nbsp; |
| &nbsp; | &nbsp; |

### Bug Fixes, November 2018

- Fix [issue #2933](https://github.com/Microsoft/azuredatastudio/issues/2933): Connection lost to Azure SQL DB
- Fix [issue #2914](https://github.com/Microsoft/azuredatastudio/issues/2914): "Invalid argument" exception expanding OE database node
- Fix [issue #2935](https://github.com/Microsoft/azuredatastudio/pull/2935): Display multi-line messages correctly in query results
- Fix [issue #2906](https://github.com/Microsoft/azuredatastudio/pull/2906): Fix Edit Data document name when table name contains special characters
- Fix [issue #2929](https://github.com/Microsoft/azuredatastudio/issues/2929): Built in extension changelog says to check the VSCode Release Notes for changes
- Fix [issue #2719](https://github.com/Microsoft/azuredatastudio/issues/2719): High Contrast theme doubles/triples icons
- Fix [issue #3047](https://github.com/Microsoft/azuredatastudio/pull/3047): Add a command-line interface for connecting to a SQL Server
- Fix [issue #3031](https://github.com/Microsoft/azuredatastudio/pull/3031): Add query plan theme support

## October 2018

October 29, 2018 &nbsp; / &nbsp; version: 1.1.4

&nbsp;

| Change | Details |
| :----- | :------ |
| Introducing the Azure Resource Explorer to browse Azure SQL Databases. | &nbsp; |
| Improve Object Explorer and Query Editor connectivity robustness. | &nbsp; |
| SQL Agent extensions improvements. | &nbsp; |
| Update to the SQL Server 2019 Preview extension. | See [Data Virtualization extension](data-virtualization-extension.md?view=sql-server-ver15). |
| &nbsp; | &nbsp; |

### Bug Fixes, October 2018

- Fix [issue #2717](https://github.com/Microsoft/azuredatastudio/issues/2717): XML Column result click formatting
- Fix [issue #2993](https://github.com/Microsoft/azuredatastudio/issues/2993): Width's Result windows is incomplete
- Fix [issue #2999](https://github.com/Microsoft/azuredatastudio/issues/2999): Could not load file System.Diagnostics.Tracing on Mac when connecting to DB
- Fix [issue #2851](https://github.com/Microsoft/azuredatastudio/issues/2851): TimeSeries chart does not render correctly
- Fix [issue #2996](https://github.com/Microsoft/azuredatastudio/issues/2996): Temp table loss due to sudden session change

For detailed information, see the [Change Log](https://github.com/Microsoft/azuredatastudio/blob/main/CHANGELOG.md), and [Releases](https://github.com/Microsoft/azuredatastudio/releases).

## September 2018 (GA Release)

September 24, 2018 &nbsp; / &nbsp; version: 1.0 &nbsp; / &nbsp; GA release

General Availability release of Azure Data Studio (formerly SQL Operations Studio).

&nbsp;

| Change | Details |
| :----- | :------ |
| Query Results Grid performance and UX improvements for large number of result sets. | &nbsp; |
| Visual Studio Code source code refresh from 1.23 to 1.26.1 with Grid Layout and Improved Settings Editor (preview). | &nbsp; |
| Accessibility improvements for screen reader, keyboard navigation, and high-contrast. | &nbsp; |
| Added `Connection name` option to provide an alternative display name in the Servers view-let. | &nbsp; |
| &nbsp; | &nbsp; |

### Announcing the SQL Server 2019 Preview extension

&nbsp;

| Change | Details |
| :----- | :------ |
| Support for SQL Server 2019 preview features including [big data cluster](../big-data-cluster/big-data-cluster-overview.md) support. | Connect to the HDFS/Spark Gateway shipped with SQL Server 2019 preview.<br/><br/>Browse HDFS, upload files, save files, and launch useful actions such as Analyze in Notebook for CSV files.<br/><br/>Submit Spark jobs from the dashboard or right-click on a HDFS/Spark connection in Object Explorer. |
| Azure Data Studio Notebooks. | Create or open Notebooks using an integrated Notebook viewer. In this release, the Notebook viewer supports connecting to local kernels and the SQL Server 2019 big data cluster only.<br/><br/>Use the PROSE Code Accelerator libraries in your Notebook to learn file format and data types for fast data preparation. |
| Azure Resource Explorer. | The Azure Resource Explorer view lets you browse data-related endpoints for your Azure accounts and create connections to them in Object Explorer. In this release, Azure SQL Databases and servers are supported. |
| SQL Server PolyBase Create External Table Wizard. | Create an external table and its supporting metadata structures with an easy to use wizard. In this release, remote SQL Server and Oracle servers are supported. |
| &nbsp; | &nbsp; |

### Bug Fixes, September 2018

- Fix [issue #2647](https://github.com/Microsoft/azuredatastudio/issues/143): The charts took a significant step backwards.
- Fix [issue #2648](https://github.com/Microsoft/azuredatastudio/issues/143): SELECT that returns a JSON hyperlinks the whole column.

For detailed information, see the [Change Log](https://github.com/Microsoft/azuredatastudio/blob/main/CHANGELOG.md), and [Releases](https://github.com/Microsoft/azuredatastudio/releases).

## August 2018

August 30, 2018 &nbsp; / &nbsp; version: 0.32.8 &nbsp; / &nbsp; Public Preview

The *August Public Preview* focuses on bug fixes, product stabilization, and filling in gaps existing scenarios.

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
- Fix [issue #1985](https://github.com/Microsoft/azuredatastudio/issues/1985): Copy from query results grid is off by one column.
- Fix [issue #1998](https://github.com/Microsoft/azuredatastudio/pull/1998): Add VS Code version to About dialog.
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
- Fix [issue 1799](https://github.com/Microsoft/azuredatastudio/issues/1799): Top 10 DB Size chart does not work on case-sensitive instances
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

- Feature request ([issue 1204](https://github.com/Microsoft/azuredatastudio/issues/1204)): Make the results grid autofit column width to data, and remember manual changes if the same query is rerun.
- Fix [issue 1398](https://github.com/Microsoft/azuredatastudio/issues/1398): Should show add message and add account button when linked account is empty.
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

For additional details, check out the [Visual Studio Code February Release Notes](https://code.visualstudio.com/updates/v1_21), and the [Visual Studio Code January Release Notes](https://code.visualstudio.com/updates/v1_20).

For more information, see the [Change Log](https://github.com/Microsoft/azuredatastudio/blob/main/CHANGELOG.md).

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
| Update JustinPealing/html-query-plan component to pick up several Query Plan viewer improvements. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixed Issues, February 2018

- Fix [issue 6](https://github.com/Microsoft/azuredatastudio/issues/6): Keep connection and selected database when opening new query tabs.
- Fix [issue 22](https://github.com/Microsoft/azuredatastudio/issues/22): 'Server Name' and 'Database Name' - Can these be drop downs instead of text boxes?
- Fix [issue 549](https://github.com/Microsoft/azuredatastudio/issues/549): Silent/Very Silent Install results in application opening after installation.
- Fix [issue 481](https://github.com/Microsoft/azuredatastudio/issues/481): Add "Check for updates" option.
- SQL Editor colorization and autocompletion fixes:
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
| Enable Hot exit. Hot exit is off by default. To enable, see [Hot exit setting](settings.md#hot-exit). | &nbsp; |
| Tab-coloring based on Server Group. Tab coloring is off by default. To enable, see [Tab color setting](settings.md#tab-color). | &nbsp; |
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

- Initial release of Azure Data Studio.

## Next Steps

See one of the following quickstarts to get started:

- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Data Warehouse](quickstart-sql-dw.md)

Contribute to Azure Data Studio:

- [https://github.com/Microsoft/azuredatastudio](https://github.com/Microsoft/azuredatastudio)
