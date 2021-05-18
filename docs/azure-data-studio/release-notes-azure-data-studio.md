---
title: Azure Data Studio release notes
description: This article has release notes for Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: yualan
ms.author: alayu
ms.reviewer: maghan
ms.custom: seodec18, contperf-fy21q4
ms.date: 05/19/2021
---

# Release notes for Azure Data Studio

This article provides details about updates, improvements, and bug fixes for the current and previous versions of Azure Data Studio.

## Current Azure Data Studio release

:::image type="icon" source="media/download-icon.png" border="false":::**[Download and install the latest release!](./download-azure-data-studio.md)**

### May 2021

Azure Data Studio 1.29.0 is the latest general availability (GA) release.

- Release number: 1.29.0
- Release date: May 19, 2021

#### What's new in 1.29.0

| New item | Details |
|----------|---------|


#### Bug fixes in 1.29.0

Some of the bug fixes for the May 2021 release.

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

## Feedback

You can reference [Azure Data Studio feedback](https://github.com/microsoft/azuredatastudio/issues/new/choose) for other known issues and to provide feedback to the product team.

## Previous Azure Data Studio releases

| Azure Data Studio release | Build number | Release date | Hotfix |
|---------------------------|--------------|--------------|---------
| [April 2021](#april-2021) | 1.28.0 | April 15, 2021 | N/A |
| [March 2021](#march-2021) | 1.27.0 | March 17, 2021 | N/A |
| [February 2021](#february-2021) | 1.26.0 | February 18, 2021 | N/A |
| [December 2020](#december-2020) | 1.27.0 | December 9, 2020 | [hotfix](#december-2020-hotfix) |
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
| [April 2019 ](#april-2019)| 1.6.0 | April 18, 2019 | N/A |
| [March 2019](#march-2019) | 1.5.1 | March 18, 2019 | [hotfix](#march-2019-hotfix) |
| [February 2019](#february-2019) | 1.4.5 | February 13, 2019 | N/A |
| [January 2019](#january-2019) | 1.3.8 | January 09, 2019 | [hotfix](#january-2019-hotfix) |

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
| New Notebook features | Improved Jupyter server start-up time by 50% on Windows <br/> Added support to edit Jupyter Books through right-click <br/> Added URI notebook parameterization support and [added notebook parameterization documentation](./notebooks/notebooks-parameterization.md) |

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
| New notebook features | Added new features to SQL to notebook support. <br/> Added new features to	Notebook parameterization support. <br/>  Added new features to results streaming for SQL Notebooks. |
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
| &nbsp; | &nbsp; |

### July 2020 (hotfix)

July 17, 2020 &nbsp; / &nbsp; version: 1.20.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #11372 Object Explorer drag-and-drop table incorrectly wraps table names | [#11372](https://github.com/microsoft/azuredatastudio/issues/11372) |
| Fix bug #11356 Dark theme is now the default theme | [#11356](https://github.com/microsoft/azuredatastudio/issues/11356) |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

### May 2020 (hotfix)

May 27, 2020 &nbsp; / &nbsp; version: 1.18.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #10538 "Run Current Query" keybind no longer behaving as expected | [#10538](https://github.com/microsoft/azuredatastudio/issues/10538)  |
| Fix bug #10537 Unable to open new or existing sql files on v1.18 | [#10537](https://github.com/microsoft/azuredatastudio/issues/10537)  |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

### April 2020 (hotfix)

April 30, 2020 &nbsp; / &nbsp; version: 1.17.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #10197 Can't connect via MFA | [#10197](https://github.com/microsoft/azuredatastudio/issues/10197)  |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

### February (hotfix)

February 19, 2020 &nbsp; / &nbsp; version: 1.15.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #9149 Show active connections | [#9149](https://github.com/microsoft/azuredatastudio/issues/9149)  |
| Fix bug #9061 Edit Data grid doesn't properly resize when showing or hiding SQL Pane | [#9061](https://github.com/microsoft/azuredatastudio/issues/9061)  |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

### Known Issue

- Users on macOS Catalina will need to right-click Azure Data Studio and then click open.

### December 2019 (hotfix)

December 26, 2019 &nbsp; / &nbsp; version: 1.14.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #8747 OE Expansion fails | [#8747](https://github.com/microsoft/azuredatastudio/issues/8747)  |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |


### November 2019 (hotfix)

November 15, 2019 &nbsp; / &nbsp; version: 1.13.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fix bug #8210 Copy/Paste results are out of order |  |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

### October 2019 (hotfix 2)

October 11, 2019 &nbsp; / &nbsp; version: 1.12.2

&nbsp;

| Change | Details |
| :----- | :------ |
| Disable automatically starting the EH in inspect mode |  |
| &nbsp; | &nbsp; |

### October 2019 (hotfix)

October 8, 2019 &nbsp; / &nbsp; version: 1.12.1

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed issue for quotes and backslashes in Notebooks to escape correctly. |  |
| &nbsp; | &nbsp; |

### October 2019

October 2, 2019 &nbsp; / &nbsp; version: 1.12.0

&nbsp;

| Change | Details |
| :----- | :------ |
| Release of Query History extension | The SQL History extension saves all past queries executed in an Azure Data Studio session and lists them in execution order. Users can see open the query, execute the query, delete the query, pause query history, or delete all query history entries. |
| New Copy/Paste Results | We have added more ways to copy/paste results from the results grid. |
| Update to PowerShell extension |  |
| Resolved bugs and issues | For a complete list of fixes see [Bugs and issues, on GitHub](https://github.com/microsoft/azuredatastudio/milestone/42?closed=1). |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

### April 2019

April 18, 2019 &nbsp; / &nbsp; version: 1.6.0 

&nbsp;

| Change | Details |
| :----- | :------ |
| Renamed **Servers** tab to **Connections** | |
| Moved Azure Resource Explorer as an Azure viewlet under Connections | Users can now view their Azure SQL instances through Azure viewlet in the Connections view and expand to view objects under each server or database.|
| SQL Notebook Improvements | &bull; &nbsp; Added button on toolbar to clear output for all cells <br/>&bull; &nbsp; Added button on toolbar to run all cells <br/>&bull; &nbsp; Fixed connection name instead of server name (if set) in the Attach to dropdown <br/>&bull; &nbsp; Fix for images in markdown not rendering when using relative image paths <br/>&bull; &nbsp; Improved functionality in notebook grids by adding double-click auto resize column size and improved mousewheel support <br/>&bull; &nbsp; Improvements to error handling and python install resiliency when installing python through notebooks <br/>&bull; &nbsp; Improvements to "select all" functionality when selecting notebook cells <br/>&bull; &nbsp; Improvements to notebook connections to prevent closing a notebook and impacting an object explorer connection <br/>&bull; &nbsp; Improved notebook experience to display a message to the user when notebook disconnected and needs a connection to run cells<br/>&bull; &nbsp; Improved support for unsaved notebooks to rehydrate in Azure Data Studio when Azure Data Studio is started again |
| Resolved bugs and issues. | See [Bugs and issues, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/26?closed=1). |
| &nbsp; | &nbsp; |

### March 2019 (Hotfix)

March 22, 2019 &nbsp; / &nbsp; version: 1.5.2 &nbsp; / &nbsp; Hotfix release

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed a few issues discovered in 1.5.1. | See [March Hotfix Release, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/28).<br/> <br/>&bull; &nbsp; Fixed issue where user couldn't close notebook opened from the "Open Notebook" task in the Dashboard <br/>&bull; &nbsp; Fixed issue where Notebook JSON has extra } after save <br/>&bull; &nbsp; Fixed issue where notebook grids weren't responding to theme changes <br/>&bull; &nbsp; Fixed issue where full notebook path was shown in the tab header. Now only the filename is shown. |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

### January 2019 (Hotfix)

January 16, 2019 &nbsp; / &nbsp; version: 1.3.9 &nbsp; / &nbsp; Hotfix release

&nbsp;

| Change | Details |
| :----- | :------ |
| Fixed a few issues discovered in 1.3.8. | See [January Hotfix Release, on GitHub](https://github.com/Microsoft/azuredatastudio/milestone/24?closed=1).<br/><br/>For detailed information, see:<br/>&bull; &nbsp; [Change Log, on GitHub](https://github.com/Microsoft/azuredatastudio/blob/main/CHANGELOG.md).<br/>&bull; &nbsp; [Releases, on GitHub](https://github.com/Microsoft/azuredatastudio/releases). |
| &nbsp; | &nbsp; |

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
| &nbsp; | &nbsp; |

## Next steps

See one of the following quickstarts to get started:

- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Synapse Analytics](quickstart-sql-dw.md)

Contribute to Azure Data Studio:

- [https://github.com/Microsoft/azuredatastudio](https://github.com/Microsoft/azuredatastudio)
