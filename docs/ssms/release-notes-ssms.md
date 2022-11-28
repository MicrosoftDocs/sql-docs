---
title: Release notes for (SSMS)
description: Release notes for SQL Server Management Studio (SSMS).
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 06/21/2022
adobe-target: true
---

# Release notes for SQL Server Management Studio (SSMS)

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

To view the release notes for SSMS 19 Preview 3, visit [SSMS Release Notes 19](release-notes-ssms-19.md).

This article provides details about updates, improvements, and bug fixes for the current and previous versions of SSMS.

[!INCLUDE[ssms-connect-aazure-ad](../includes/ssms-connect-azure-ad.md)]

## Current SSMS release

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server Management Studio (SSMS) 18.12.1](https://aka.ms/ssmsfullsetup)**

SSMS 18.12.1 is the latest general availability (GA) release of SSMS. If you need a previous version of SSMS, see [previous SSMS releases](release-notes-ssms.md#previous-ssms-releases).

### 18.12.1

- Release number: 18.12.1
- Build number: 15.0.18424.0
- Release date: June 21, 2022

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x40a)

#### What's new in 18.12.1

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.37. |

#### Bug fixes in 18.12.1

| New Item | Details |
|----------|---------|
| Always Encrypted | Fixed issue with Column Master Key creation generating an exception when using Azure Key Vault as they key store. |
| Data Classification | Fixed issue with "Could not load file or assembly 'Microsoft.InformationProtection, Version=1.10.98.0" after upgrading to SSMS 18.10 or higher.  See [Latest SSMS 18.11.1 breaks the Data Classification. Get missing assembly error after updating](https://feedback.azure.com/d365community/idea/af89d5d7-45a4-ec11-a81c-0022484ee92d). |
| SSMS General | Resolved error related to dacpac deployment using the Deploy Data-tier application option in Azure SQL DB with MFA. |

#### Known issues (18.12.1)

| New Item | Details | Workaround |
|----------|---------|------------|
| Database Designer | Clicking the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| SQL Managed Instance | Viewing database properties for a SQL MI database may return the error "Subquery returned more than 1 value. This is not permitted when the subquery follows =, !=, <, <= , >, >= or when the subquery is used as an expression. (.Net SqlClient Data Provider)". | There is a known problem due to incorrect data in msdb. To resolve, remove backup history. For example `EXEC msdb..sp_delete_backuphistory @oldest_date = '<current date>'`. |
| Stretch DB | Unable to stretch an existing table using the Stretch DB Wizard. | Use T-SQL or an earlier version of SSMS (18.9.1 or below) to stretch an existing table. |
| Storage Account | Trying to delete a container from a storage account fails with a (400) Bad Request error | Use the Azure portal for container deletion. |

You can reference [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server) for other known issues and to provide feedback to the product team.

## Previous SSMS releases

Download previous SSMS versions by selecting the download link in the related section.

| SSMS version | Build number | Release date |
|--------------|--------------|--------------|
| [18.12](#1812) | 15.0.18420.0 | June 9, 2022 |
| [18.11.1](#18111) | 15.0.18410.0 | March 8, 2022 |
| [18.11](#1811) | 15.0.18404.0 | February 24, 2022 |
| [18.10](#1810) | 15.0.18390.0 | October 5, 2021 |
| [18.9.2](#1892) | 15.0.18386.0 | July 15, 2021 |
| [18.9.1](#1891) | 15.0.18384.0 | April 20, 2021 |
| [18.9](#189) | 15.0.18382.0 | April 15, 2021 |
| [18.8](#188) | 15.0.18369.0 | December 17, 2020 |
| [18.7.1](#1871) | 15.0.18358.0 | October 27, 2020 |
| [18.7](#187) | 15.0.18357.0 | October 20, 2020 |
| [18.6](#186) | 15.0.18338.0 | July 22, 2020 |
| [18.5.1](#1851) | 15.0.18333.0 | June 09, 2020 |
| [18.5](#185) | 15.0.18330.0 | April 07, 2020 |
| [18.4](#184) | 15.0.18206.0 | November 04, 2019 |
| [18.3.1](#1831) | 15.0.18183.0 | October 02, 2019 |
| [18.2](#182) | 15.0.18142.0 | July 25, 2019 |
| [18.1](#181) | 15.0.18131.0 | June 11, 2019 |
| [18.0](#180) | 15.0.18118.0 | April 24, 2019 |
| [17.9.1](#1791) | 14.0.17289.0 | November 21, 2018 |
| [16.5.3](#1653) | 13.0.16106.4 | January 30, 2017 |

### 18.12

- Release number: 18.12
- Build number: 15.0.18420.0
- Release date: June 09, 2022

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2197629&clcid=0x40a)

#### What's new in 18.12

| New Item | Details |
|----------|---------|
| Always Encrypted | Updated AE Tools to only show keys with no expiration date. | 
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.36.2. |
| Compatibility Mode | Added support for compatibility mode 160.  Note that SSMS 19 is the recommended SSMS release for use with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. Visit [Download SQL Server Management Studio 19](download-sql-server-management-studio-ssms-19.md) to obtain SSMS 19. |
| Data Classification | Improvements to Data Classification user interface. |
| Link feature for Azure SQL Managed Instance | Added the link feature support for SQL Server 2016 for connection and failover wizards. See [Link feature for Azure SQL Managed Instance](/azure/azure-sql/managed-instance/link-feature). |
| Link feature for Azure SQL Managed Instance | Added failover wizard support to migrate multiple databases at the time from SQL Server to Azure SQL Managed Instance using the link feature. |
| Query Editor | Added a dialog to allow users to cancel the transaction count check if it takes longer than two seconds when closing the query editor window. |
| Query Tuning Assistant | Updated user interface for improved accessibility. |
| Showplan | The Showplan XML schema has been updated to include new attributes related to the Query Store Hint feature. |

#### Bug fixes in 18.12

| New Item | Details |
|----------|---------|
| Availability Group Dashboard | Fixed issue when connecting to the Availability Group Dashboard for an AG on SQL Server 2016 which resulted in "unknown property ClusterType" error.  See [SSMS 18.11.1 Bug](https://feedback.azure.com/d365community/idea/1603d3a2-73ab-ec11-a81c-6045bd7d1bee). |
| Analysis Services | Corrected behavior of SSMS not saving Analysis Services settings separately. |
| Integration Services | Fixed an issue that caused the error "System.ArgumentException: Value does not fall within the expected range" that would be encountered unexpectedly at step "Changing protection level" when deploying SSIS project. |
| General SSMS | Fixed issue with Technical Support menu link opening to a blank page. |
| Intellisense | Fixed Intellisense for contained users. |
| Ledger | Fixed issue with display columns and ledger tables displaying more than once. |
| Link feature for Azure SQL Managed Instance | Fixed issues related to the automated connection and failover wizards for the link feature. |
| Query Editor |Fixed issue with audible notification occurring when closing a query window. See [SSMS 18.11.1 Beeps When I Close a Query Window](/answers/questions/775502/ssms-18111-beeps-when-i-close-a-query-window.html). |
| Registered Servers | Fixed issue that caused SSMS crash when displaying registered servers after importing  registered servers from Azure Data Studio via settings.json file. See [SSMS (18.11.1) crashing on "show registered servers"](https://feedback.azure.com/d365community/idea/53d07d5e-7acc-ec11-a81b-6045bd7ac9f9). |
| Partitioning | Addressed missing option to schedule Agent job in the partitioning wizard. |

#### Known issues (18.12)

| New Item | Details | Workaround |
|----------|---------|------------|
| Database Designer | Clicking the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| Stretch DB | Unable to stretch an existing table using the Stretch DB Wizard. | Use T-SQL or an earlier version of SSMS (18.9.1 or below) to stretch an existing table. |
| Storage Account | Trying to delete a container from a storage account fails with a (400) Bad Request error | Use the Azure portal for container deletion. |

### 18.11.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.11.1](https://go.microsoft.com/fwlink/?linkid=2189054)

- Release number: 18.11.1
- Build number: 15.0.18410.0
- Release date: March 08, 2022

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x40a)

#### What's new in 18.11.1

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.35.0. |
| Data Classification | Introduced Set Microsoft Information Protection Policy menu option. |
| Data Classification | Added Microsoft 365 authentication window to set MIP policy. |
| Data Classification | Classify Data page and Add Classification page display the Microsoft 365 sensitivity labels when in MIP policy mode. |
| Data Classification | Added warning icon against the columns if the prior classification and current Information Protection policy mode doesn't match. |
| Link feature for Azure SQL Managed Instance | Introduced connection wizard to assist with establishing a hybrid link between SQL Server and Azure SQL Managed Instance.  See [Link feature for Azure SQL Managed Instance](/azure/azure-sql/managed-instance/link-feature). |
| Link feature for Azure SQL Managed Instance | Introduced failover wizard to assist with migration of databases from SQL Server to Azure SQL Managed Instance using the link feature. |
| Link feature for Azure SQL Managed Instance | Introduced dashboard to monitor the status of the hybrid link established between SQL Server and Azure SQL Managed Instance. |

#### Known issues (18.11.1)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| Availability Group Dashboard | Connecting to the Availability Group Dashboard for an AG on SQL Server 2016 and below results in "unknown property ClusterType" error. | Use SSMS 18.10 to access the AG Dashboard for SQL Server 2016 and earlier. |
| Database Designer | Clicking the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| Stretch DB | Unable to stretch an existing table using the Stretch DB Wizard. | Use T-SQL or an earlier version of SSMS (18.9.1 or below) to stretch an existing table. |

### 18.11

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.11](https://go.microsoft.com/fwlink/?linkid=2188603)

- Release number: 18.11
- Build number: 15.0.18404.0
- Release date: February 24, 2022

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2188603&clcid=0x40a)

#### What's new in 18.11

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.34.0. |
| Data Classification | Updated user interface Modified the Data Classification UI panel to allow users to select specific Schema & Table to display classified columns. |
| Data Classification | Updated the Add Classification pane to allow users to add new classifications for columns that are not yet classified. |
| Data Classification | Added a new category, All Unclassified, in the Column drop down of the Add Classification page. |
| Data Classification | Updated the Recommendations pane to allow Save selected recommendations directly after scanning the entire database. |
| General SSMS | Added a dialog box to display the status of an extended open transaction check when closing a T-SQL Query Editor tab. |
| Profiler | Added support for trace files from Analysis Services version 16.0. |
| Profiler | Added ability to carry over the password from an existing connection when starting SQL Profiler from SSMS. |

#### Bug fixes in 18.11

| New Item | Details |
|----------|---------|
| Accessibility | Fixed focus order for ease of navigation and corrects names read by screen reader in View Facets window. |
| Accessibility | Improved names read by Screen Readers in Database properties window. |
| Accessibility | Fixed contrast and accessible names in Data Classification Report. |
| Accessibility | Multiple Screen reader fixes in the Data Classification Pane. |
| Accessibility | Fixed issue with loss of focus indication in Data Classification Pane. |
| Always Encrypted | Fixes issue with the New Column Master Key wizard freezing up when creating Always Encrypted column master key using Azure Key Vault as the key store, with the key vault having any of the Rotate key permissions set. |
| Analysis Services | Updated Analysis Services dependencies. |
| General SSMS | Fixed icon consistency for temporal tables. |
| General SSMS | Fixed an issue where SSMS could show an error dialog with an unhelpful message (e.g. "syntax error ( *EngineEdition != 11)" ). |
| General SSMS | Updated Technical Support URL to point to sqlfeedback site. |
| General SSMS | Fixed issue with SSMS crashing due to incorrect grid width. |
| General SSMS | Fixed issue with arithmetic overflow error in backup system query. |
| Import/Export Data-Tier Application| Resolved issue with Bacpac wizard failing when browsing storage accounts. |
| Ledger Tables | Ledger objects are visually indicated as such in Object Explorer, history tables are nested under their respective ledger tables, and template ledger table scripts have been added. |
| Profiler | Fixed an issue where SSMS was not able to start SQL Profiler when spaces existed between the server name and its port number. |
| SQL Agent (Managed Instance) | Fixed an issue that prevented creating or adding a step to new or existing SQL Agent job.  See [SSMS error while editing job step](/answers/questions/606328/ssms-error-while-editing-job-step-34object-referen.html). |
| Showplan | Ability to zoom in and out of plans with CTRL + mousewheel. See [SQL Server user feedback](https://feedback.azure.com/d365community/idea/e780ad2c-9e31-ec11-a819-000d3ae2b306). |
| SMO/Scripting | Added support to script External Streaming Jobs when scripting Databases. |
| SSMS Setup | Fixed an issue in the Analysis Services deployment wizard for the JPN SSMS executable. |
| SSMS Setup | Fixed an issue where SSMS setup could fail with an 'Element not found (0x80070490)" [Failed to find expected public key in certificate chain]. |
| SSMS Setup | Fixed an issue to support SSMS migrations to later versions. |

#### Known issues (18.11)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| Availability Group Dashboard | Connecting to the Availability Group Dashboard for an AG on SQL Server 2016 and below results in "unknown property ClusterType" error. | Use SSMS 18.10 to access the AG Dashboard for SQL Server 2016 and earlier. |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| Stretch DB | Unable to stretch an existing table using the Stretch DB Wizard. | Use T-SQL or an earlier version of SSMS (18.9.1 or below) to stretch an existing table. |

### 18.10

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.10](https://go.microsoft.com/fwlink/?linkid=2174135)

- Release number: 18.10
- Build number: 15.0.18390.0
- Release date: October 5, 2021

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2174135&clcid=0x40a)

#### What's new in 18.10

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.32.0. |
| Analysis Services | Update BITools to 150.19714.0 |
| DacFx | Update DacFx to 15.0.5282.3 |
| Import Flat File | Support for rich data types in Import Flat File wizard, including money. |
| Replication | Support for scripting of peer to peer publication with [Last Writer Win (LWW) conflict detection](../relational-databases/replication/transactional/peer-to-peer/configure-last-writer.md) |
| XEvents | Support for Ledger syntax in XEvents interface |

#### Bug fixes in 18.10

| New Item | Details |
|----------|---------|
| Accessibility | Fixes a narrator issue in the Data Classification Pane |
| Accessibility | Fixes a visibility issue in the Data Classification Pane |
| Azure SQL DW | Fixes an issue where trying to create an Azure SQL DW database throws an error. |
| Backup | Fixes an issue where SSMS restore backup from URL fails on Azure storage account SKU. |
| High DPI / Scaling | Fixes high-DPI issue in SSIS Job Step |
| High DPI / Scaling | Fixes high-DPI issue in Object Dependencies |
| High DPI / Scaling | Fixes high-DPI issue in Replication Wizard |
| High DPI / Scaling | Fixes high-DPI issue in table trigger object explorer menu item |
| Import Flat File | Clarifies error reporting option in Import Flat File wizard. |
| Profiler | Fixes an issue where Profiler client crashes with an internal error on table-valued parameter with a missing default value. |
| Reports | Fixes an issue with a Resource Locking Statistics report not filtering on resource database. |
| SqlParser | Fixes an issue where Azure tables fail to script when database catalog collation is case sensitive. |
| XEvents | Fixes issue where long string does not include ellipsis at end when truncated in display |

#### Known issues (18.10)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |


### 18.9.2

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.9.2](https://go.microsoft.com/fwlink/?linkid=2168063)

- Release number: 18.9.2
- Build number: 15.0.18386.0
- Release date: July 15, 2021

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2168063&clcid=0x40a)

#### What's new in 18.9.2


| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.30.0. |
| Always Encrypted | Scripting support for Ledger tables. |
| Analysis Services | Support for XMLA endpoint to refresh datasets in Power BI premium Gen 2. |
| Azure SQL DB | Adds support for backup_storage_redundancy in database create/modify dialogs. |
| Azure SQL DB | Adds `create login` script support for Azure AD-based login syntax. |
| Integration Services | Improvement for IR Creation Wizard, Job Migration Wizard and SSIS package scheduler to support AzureUSGovernment and AzureChinaCloud. |
| Intellisense | Syntax highlighting for External Language Management and Query Store (query_store_plan_feedback, query_store_query_variant). |

#### Bug fixes in 18.9.2

| New Item | Details |
|----------|---------|
| Accessibility | Fixes issue in screen reader announcements for dialogs processing information. |
| Always Encrypted | Fixes issue where sign-in button wasn't fully visible during Master Key Configuration. |
| Azure SQL DB | Fixes issue where multiple tenants on an account made SLO changes from database properties unreliable. |
| Backup/Restore | Fixes issue where browse folder dialog was incorrectly parsing the root of a drive. |
| Backup/Restore | Fixes issue in restoring a database to point in time using timeline dialog. |
| General SSMS | Fixes open SqlConnection leaks in object explorer. |
| High DPI / Scaling | 4k Scaling Fixes in dialogs for Publication Properties, Extended Events, Always Encrypted, Copy Database Wizard, and Database Designer. |
| Maintenance Plan | Fixes an issue where deleting a maintenance plan removed a DTS package from the server. |
| Object Explorer | Fixes issue where "Storage" menu item wasn't appearing for Azure SQL Managed Instance. |
| Replication Tools | Fixes an issue where the script generated is incorrect while configuring ReadOnly Routing. |
| XEvents | Fixes issue where line endings weren't properly displayed in a XEvent popup dialog. |

#### Known issues (18.9.2)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | A fix is coming for .NET Framework 4.8 for Windows 10 version > 1809 on July 21. Earlier versions will be updated the second Tuesday of August.  |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |


### 18.9.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.9.1](https://go.microsoft.com/fwlink/?linkid=2161193)

- Release number: 18.9.1
- Build number: 15.0.18384.0
- Release date: April 20, 2021

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2161193&clcid=0x40a)

#### What's new in 18.9.1

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.28.0. |

#### Bug fixes in 18.9.1

| New Item | Details |
|----------|---------|
| IntelliSense | Fixed issue where column names were omitted from suggestions and error indicators are falsely displayed under the column names. [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/43176843) |

#### Known issues (18.9.1)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | A fix is coming for .NET Framework 4.8 for Windows 10 version > 1809 on July 21. Earlier versions will be updated the second Tuesday of August.  |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |

### 18.9

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.9](https://go.microsoft.com/fwlink/?linkid=2160964)

- Release number: 18.9
- Build number: 15.0.18382.0
- Release date: April 15, 2021

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2160964&clcid=0x40a)

#### What's new in 18.9

| New Item | Details |
|----------|---------|
| Always Encrypted | Updated the New Column Master Key and New Column Encryption Key dialogs to support provisioning and managing column master keys stored in [Azure Key Vault Managed HSMs](/azure/key-vault/managed-hsm/overview). |
| Always Encrypted | Enabled specifying a key length when generating column master keys in Azure Key Vault. |
| Always Encrypted | Improved diagnostics for provisioning and managing column master keys stored in Azure Key Vault. |
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.27.0. |
| Azure SQL DB | Improved the diagnostic reporting when an error occurs while interacting with Azure. |
| General SSMS | Added command `/resetuserdata` to ssms.exe, which resets the user data including the "Most Recently Used" lists. |
| General SSMS | Updated MSODBC and OLEDB drivers installed with SSMS. The updated version to MSODBC notably adds a workaround to connection error *AADSTS530000* (Azure AD configuration with Conditional Access policies that would require Windows Account Manager). See [Using Azure Active Directory](../connect/odbc/using-azure-active-directory.md) for more information. |
| IntelliSense/Editor | Added support for [greatest](../t-sql/functions/logical-functions-greatest-transact-sql.md) and [least](../t-sql/functions/logical-functions-least-transact-sql.md) in IntelliSense |
| IntelliSense/Editor | Added support for BACKUP_STORAGE_REDUNDANCY syntax (CREATE/ALTER DATABASE). |
| ShowPlan | Always show Estimated Number of Rows for All Executions property |

#### Bug fixes in 18.9

| New Item | Details |
|----------|---------|
| Accessibility | Fixed issues in narrator: Items order for New Column Master Key UI to move in a logical order from top to bottom, Narrator focus order on Filter Settings.  |
| Always Encrypted | Fixed the Always Encrypted wizard to not include a port number in key paths for column master keys stored in Azure Key Vault. The bug caused failures in the ODBC and PHP drivers that didn't expect port numbers in key paths. |
| Always On Availability Group | Fixed an issue where the "Add New Availability Group" Wizard doesn't join AG databases into new AG automatically on secondary replica. |
| Auditing | Fixed an issue where SSMS was throwing an error dialog when trying to bring up the Properties on Audit Specification. |
| Auditing | Fixed an issue were some Audit Specifications wizards and property dialogs were not working properly on some localized versions of SSMS (e.g. Create, Japanese). |
| Azure SQL DB | Fixed an issue that was causing the "Create firewall rule" to fail in some cases. |
| Azure SQL DB | Fixed an issue where the "Properties" dialog on a Table was displaying the incorrect server name (local machine instead of the Azure SQL DB server name). |
| Azure SQL DB | Fixed an issue that was causing the "Create firewall rule" to fail when the server name had invalid characters (such as capital letters) in the name. |
| Import/Export Data-Tier Application | Fixed an issue where importing a bacpac into SQL Express may fail. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/42347446). |
| General SSMS | Fixed a crash (in the Visual Studio Shell) that was causing SSMS to crash when re-docking tabs. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/42651556-ssms-18-8-crashes-when-re-docking-tabs) |
| General SSMS | Fixed an issue in "Browse folder" dialog that was causing SSMS to stop responding. |
| General SSMS | Fixed an issue where Object Explorer can lock up the UI thread, and ultimately cause the UI to freeze (and the user to have to hard close the SSMS. |
| General SSMS | Addressed issue where there's no option available in SSMS to rebuild a clustered columnstore index online. SSMS offers an online ON checkbox on the UI, where we can select if the particular index has to be built online.  |
| General SSMS | Fixed the logic to page the user to a proper page in most error dialogs originating from exception at the SQLClient (driver) level. Previously, for almost all the error/exceptions, the help button was always tasking the user to a non-existing page. |
| General SSMS | Fixed issues around the "Server Properties / Processor" UI where SSMS is SSMS shows incorrect CPU Processor information or an error similar to "An item with the same key has already been added." See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/39349960) and another [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/40604089). |
| General SSMS | Fixed errors that may show up while resetting settings. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/42595579). |
| General SSMS | Fixed an issue that was causing SSMS to throw an error message when trying to use the "PresentOn" feature in QuickLaunch. Also, added back the "PresentOff " functionality, which resets the UI to its regular size. |
| General SSMS | Fixed the logic that prevents the creation of a (clustered) columnstore index, which was overly restrictive for newer versions of SQL Server. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37061617). |
| General SSMS | Fixed an issue where SSMS was displaying an incorrect error message when changing the connection of an editor window to DAC ("Dedicated Admin Connection"). The error message was misleading in asserting that the connection wasn't successful, when indeed it was. *Note: while the error has been removed, due to the nature of DAC, keep in mind that: (1) IntelliSense experience will be significantly limited (2) SSMS will still block the Object Explorer experience when connecting using DAC.* |
| High DPI/Scaling | Fixed an issue where the Data Collection Wizard could UI could result in being clipped on High DPI monitors (at 150% scaling). |
| High DPI/Scaling | Fixed an issue where DB Mirroring security wizard may render poorly on High DPI (at 150% scaling). |
| High DPI/Scaling | Fixed an issue with the "New Job Schedule" dialog which was rendering poorly on High DPI (4K monitors and 200% scaling). |
| High DPI/Scaling | Fixed an issue with the "New Session" (XEvents) where some elements of the UI were truncated on High DPI (4K monitors and 200% scaling). |
| Import Flat File | Fixed issue where some messages displayed by the Import Flat File wizard aren't localized. |
| Integration Services | Previous known issue "Remote connections to Integration services may fail with *"The specified service doesn't exist as an installed service."* on newer Operating system." is corrected by a fix delivered with the OS. |
| IntelliSense/Editor | Updated IntelliSense to display more useful information on built-in functions TODATETIMEOFFSET and SWITCHOFFSET. |
| Log Shipping Wizard | Fixed in issue where Log Shipping Wizard may throw an error "Can't show requested dialog" when clicking on Edit Job for Transaction Log Backup/Copy/Restore Setting Tab. |
| Log Viewer | Addressed an issue where on some locales (e.g. Korean) some UI elements looked truncated/clipped. |
| Maintenance Plans | Fixed an issue where SQL Server Management Studio 15.0.18338.0 can't explore "Expression" property in Maintenance plan. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/42009076). |
| Query Tuning Assistant | Fixed an issue where an error message like "User doesn't have enough permissions." was displayed when trying to use the "New Database Upgrade Session" wizard on a database containing a dot in the name.  See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/40286740). |
| SMO/Scripting | Fixed script generation for DW databases where it only worked if the object explorer was connected to logical master instead of the user database. |
| SSMS Setup | Fixed an issue where SSMS Setup wasn't properly executing the Post-Install Custom Action, thus not running `ssms.exe /setup`. This is believed to be the cause for some sporadic issues when SSMS is started after an upgrade (see known issue session for AS). |
| Vulnerability Assessment | Disabling menu options "Tasks -> Vulnerability Assessment" for Azure SQL databases and adding collapsible message tip for better user experience for Azure SQL VM users. |
| XEvent UI | Fixed capability to filter Extended Events using the event name instead of ID. Supports the use of map_value column instead of map_key in wait_type filter predicate as the key value is subjected to change during version upgrade. Originally added in [SSMS 18.8](#whats-new-in-187). |

#### Known issues (18.9)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | A fix is coming for .NET Framework 4.8 for Windows 10 version > 1809 on July 21. Earlier versions will be updated the second Tuesday of August.  |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| IntelliSense | Column names are omitted from suggestions and error indicators are falsely displayed under the column names. |

### 18.8

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.8](https://go.microsoft.com/fwlink/?linkid=2151644)

- Release number: 18.8
- Build number: 15.0.18369.0
- Release date: December 17, 2020

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x40a)

#### What's new in 18.8

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.25.1. |
| Analysis Services | Added support for Power BI Premium Gen2 workspaces. |
| Auditing | Added support for EXTERNAL_MONITOR and "operator audit".  See the configuration [documentation](/azure/azure-sql/managed-instance/auditing-configure) for more information. |
| Integration Services | Improved IR Creation Wizard such that creating SSISDB is optional when creating SSIS IR |

#### Bug fixes in 18.8

| New Item | Details |
|----------|---------|
| Accessibility | Fixed issues impacting Narrator in the ProgressReport Dialog |
| Activity Monitor | Fixed an issue where the "Edit Query Text" context menu in "Active Expensive Queries" grid wasn't working |
| Always Encrypted | Fixed an issue where SSMS could throw a runtime error ("Class not found") |
| High DPI/Scaling | Improved the 4k display of the ProgressReport Dialog |
| High DPI/Scaling | Fixed in issue on the "New Schedule-  Package" Form in Integration Services |
| General SSMS |  Fixed an issue where SSMS could be throwing an error when trying to display the properties of a column which has an identity seed or identity increment that exceeds the size of a bigint.
| General SSMS | Fixed a crash in SSMS caused by a menu item "Projects Export Templates…" which wasn't meant to be displayed |
| General SSMS | Fixed an issue that could cause SSMS to crash when enumerating backups in a folder |
| General SSMS | Mitigated an issue that was causing SSMS to leak handles and memory when displaying results of queries |
| General SSMS | Fixed an issue where the users weren't able to create clustered columnstore index for columns of particular types (UniqueIdentifier on SQL 2014 and above, VarCharMax/NVarCharMax/VarBinaryMax on SQL 2017 and above) |
| General SSMS | Fixed a long outstanding issue where the reordering of XML columns in the "Result Grid" wasn't working. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/32890930) |
| General SSMS | Fixed issue where SSMS was incorrectly displaying a warning about "change tracking not being supported for a database with memory optimized objects " |
| Generate Scripts Wizard | Fixed a bug inhibiting enumeration of database objects on SQL Server 2014 and older. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/41885587). |
| Replication Tools | Fixed an issue where SQL Replication Monitor wasn't honoring the autoconnect settings, thus forcing the user to manually reconnect every time. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/40394743) |
| SMO/Scripting | Fixed an issue when trying to use "Generate Script…" on an encrypted view in SQL Azure DB |
| XEvent UI | Fixed an issue that could cause SSMS to crash when switching from other applications to SSMS. |
| XEvent UI | Reverted the improvement from 18.7 that caused a bug in XEvent creation due to additional quotation marks. *This change is temporary and the improvement will return in a subsequent release without the accompanying bug.* |

#### Known issues (18.8)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | N/A |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| Integration Services | Remote connections to Integration services may fail with "The specified service doesn't exist as an installed service." on newer Operating system. | Identify the Integration services-related registry location under Computer\HKEY_CLASSES_ROOT\AppID & Computer\HKEY_CLASSES_ROOT\ WOW6432Node\AppID and within these hives, rename the registry key named 'LocalService' to 'LocalService_A' for the specific version of Integration services that we are trying to connect |

### 18.7.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.7](https://go.microsoft.com/fwlink/?linkid=2147207)

- Release number: 18.7.1
- Build number: 15.0.18358.0
- Release date: October 27, 2020

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2147207&clcid=0x40a)

#### Bug fixes in 18.7.1

| New Item | Details |
|----------|---------|
| Query Store | Fixed error thrown when right-click on the object explorer node for Query Store. |

#### Known issues (18.7.1)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | To solve this issue, uninstall and then reinstall SSMS. |
| DAX Query Editor | Opening DAX Query Editor causes error "Object not set to instance of an object" | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | N/A |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Generate Scripts Wizard | The wizard fails when attempting to enumerate database objects on SQL Server 2014 and older. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/41885587). | Use SSMS 18.6 to select objects in the generate scripts wizard for SQL 2014 and older. |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| Integration Services | Remote connections to Integration services may fail with "The specified service doesn't exist as an installed service." on newer Operating system. | Identify the Integration services-related registry location under Computer\HKEY_CLASSES_ROOT\AppID & Computer\HKEY_CLASSES_ROOT\ WOW6432Node\AppID and within these hives, rename the registry key named 'LocalService' to 'LocalService_A' for the specific version of Integration services that we are trying to connect |
| Object Explorer | Releases of SSMS before 18.7 have a breaking change in object explorer due to the engine's changes related to [Azure Synapse Analytics serverless SQL pool](/azure/synapse-analytics/sql/on-demand-workspace-overview). | To continue utilizing object explorer in SSMS with Azure Synapse Analytics serverless SQL pool, you need to use SSMS 18.7 or later. |

### 18.7

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.7](https://go.microsoft.com/fwlink/?linkid=2146265)

- Release number: 18.7
- Build number: 15.0.18357.0
- Release date: October 20, 2020

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2146265&clcid=0x40a)

#### What's new in 18.7

[!INCLUDE [ssms-ads-install](../includes/ssms-azure-data-studio-install.md)]

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS also installs Azure Data Studio. |
| Always Encrypted | To recognize the new HSM endpoints, you need to upgrade SSMS. This is done by consuming the new AKV Provider NugetPackage. |
| Import Flat File | Made an improvement to better predict data types by learning on 300 lines by default. |
| Import Flat File | Prevent columns from being declared as TinyInt that should be SmallInt. |
| Import Flat File | Made an improvement where DW tables are properly cleaned, if there's a failure in data import. |
| Resource Governor | Added support for decimal values. |
| ShowPlan | Added PREDICT operator. |
| XEvent UI | Added capability to script out Extended Events using the wait_type name. Users are requesting to use the value of map_value column instead of map_key in wait_type filter predicate as the key value is subjected to change during version upgrade. Fix: Added a checkbox and given the option to users, to choose either map_value or map_key for the wait_type filter predicate value. |

#### Bug fixes in 18.7

| New Item | Details |
|----------|---------|
| Accessibility | Fixed tab ordering of buttons in the "The following settings are not supported by the database" window that pops up when querying against DW. |
| Accessibility | Import and Export Wizard: the page layout is incorrect in high dpi mode. |
| Activity Monitor | Fixed an issue where Activity Monitor was pausing when opening the "Processes" tab. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035/suggestions/37050118). |
| Always On Availability Group | Fixed an issue where Read Scale Availability Group Failover wasn't working. |
| Analysis Services | Updated PowerQuery components to 2.84.982 for AS Tabular models scenarios. |
| Analysis Services | Fixed an issue that may have resulted in an error when trying to connect to SSAS via msmdpump.dll. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/40144696). |
| Backup/Restore | Fixed an issue where selecting "View Connection Properties" yielded a SMO error on HostDistribution prop being missing for SQL 2016 and earlier. |
| Database Designer | Fixed an issue, which was causing SSMS to crash when handling decimal numbers. |
| Database Diagrams | Fixed an issue that could cause SSMS to crash or stop responding when using Database Diagrams where "Add Table" dialog was not properly displayed. |
| Database Mirroring | Fixed an issue that was causing mirror configuration to fail. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/32897281). |
| General SSMS | Fixed an issue where trying to connect to an Azure SQL DB, which could take several seconds (SQL login in a user database). |
| General SSMS | Fixed an issue where SSMS wasn't handling/displaying deadlock captured (.xdl files). |
| General SSMS | Fixed an issue where trying to open error log settings for SQL Server 2008 R2 and below failed with ErrorLogSizeKb property not found. |
| General SSMS | General fixes and improvements around [Azure Synapse Analytics serverless SQL pool](/azure/synapse-analytics/sql/on-demand-workspace-overview) support. |
| Import Flat File | Fixed an issue where the wizard was not detecting that the file could be in use by another application and instead was throwing an error. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035/suggestions/40761574). |
| Import/Export Data-Tier Application | Fixed the default service tier to be Standard S0 when importing a bacpac (same as Azure portal and SqlPackage.exe behavior). |
| Import Flat File | Fixed an issue where the wizard wasn't detecting that the file could be in use by another application and instead was throwing an error. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035/suggestions/40761574). |
| Integration Services | Fixed an issue that Azure Subscription ComboBox items are duplicate in IR Creation Wizard and Job Migration Wizard when different subscriptions have the same name. |
| Integration Services | Fixed an issue that sometimes Connect button can't be enabled in IR Creation Wizard. |
| Integration Services | Fixed an issue that items of "Use Environment variable" ComboBox in "Set Parameter Value" dialog aren't in order. |
| Intellisense | Fixed a crash in SSMS that could happen when executing a query. |
| Intellisense | Fixed an issue where intellisense doesn't work when the user is connected to an Availability Group configured for Read-Only Routing. |
| Linked Servers | Fixed an issue where a user with CONTROL SERVER permission (but not in sysadmin role) wasn't able to add a linked server. |
| Log Viewer | Fixed an issue where a user with VIEW SERVER STATE permissions wasn't able to view the SQL Server Error Logs. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035/suggestions/32899204). |
| Object Explorer | Fixed an issue where selecting the **Start PowerShell** menu on some Object Explorer nodes (like "Policy Management", "Extended Events") would cause PowerShell not to start correctly. |
| Registered Servers | Fixed an issue where SSMS crashed when trying to register a Central Management Server. |
| Registered Servers | Fixed the issue where menu items to launch Azure Data Studio from Registered Servers was missing. |
| Reports | Fixed an issue where on the Performance Dashboard you try to navigate to sublinks (such as **Expensive Queries**) and it doesn't work. This issue was common on most non-English versions of SSMS. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035/suggestions/41454499). |
| ShowPlan | Fixed and issue that was causing SSMS to crash when using Find Node to search for text. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035/suggestions/40421650). |
| ShowPlan | Add KB suffix on the Memory Grant tooltip row |
| Vulnerability Assessment | Fixed an issue that was causing SSMS to throw an error when trying to set baselines in Vulnerability Assessment. See [SQL Server User Feedback](https://feedback.azure.com/forums/908035/suggestions/40578565). |
| XEvent UI | Fixed the issue where hitting F1 wasn't landing on the correct page on DOCS. |
| XEvent UI | Fixed log in XEvent Viewer where the tooltip wasn't  displaying correctly text containing text encoded using surrogate pairs. |

#### Known issues (18.7)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | To solve this issue, uninstall and then reinstall SSMS. |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | N/A |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Generate Scripts Wizard | The wizard fails when attempting to enumerate database objects on SQL Server 2014 and older. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/41885587). | Use SSMS 18.6 to select objects in the generate scripts wizard for SQL 2014 and older. |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. Workaround: Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". | N/A |
| Integration Services | Remote connections to Integration services may fail with "The specified service does not exist as an installed service." on newer Operating system. Workaround: Identify the Integration services-related registry location under Computer\HKEY_CLASSES_ROOT\AppID & Computer\HKEY_CLASSES_ROOT\ WOW6432Node\AppID and within these hives, rename the registry key named 'LocalService' to 'LocalService_A' for the specific version of Integration services that we are trying to connect | N/A |
| Object Explorer | Releases of SSMS before 18.7 have a breaking change in object explorer due to the engine's changes related to [Azure Synapse Analytics serverless SQL pool](/azure/synapse-analytics/sql/on-demand-workspace-overview). | To continue utilizing object explorer in SSMS with Azure Synapse Analytics serverless SQL pool, you need SSMS 18.7 or later. |
| Query Store | The object explorer node for Query Store throws an error on right-click. | Access items directly by expanding the node and right-clicking individual child options. |

### 18.6

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.6](https://go.microsoft.com/fwlink/?linkid=2135491)

- Release number: 18.6
- Build number: 15.0.18338.0
- Release date: July 22, 2020

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2135491&clcid=0x40a)

#### What's new in 18.6

| New Item | Details |
|----------|---------|
| Analysis Services | Updated to latest release of AS client libraries. |
| Auditing | Added support for SENSITIVE_BATCH_COMPLETED_GROUP action ID (string instead of a number). |
| Auditing | Added the following fields to the audit viewer: affected_rows, response_rows, connection_id, duration_milliseconds, and data_sensitivity_information. |
| Data Classification | Update SSMS to support the import/export of policy exported via PowerShell cmdlets. |
| Import Flat File | Added support for Fixed Width files and file type detection for .csv/.tsv files to ensure they're parsed as csv/tsv files respectively. |
| Integration Services | Added support for Azure SQL Managed Instance agent jobs to execute an SSIS package from package store in Azure-SSIS IR. |
| SMO / Scripting | Added support to script Dynamic Data Masking on [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) (formerly SQL Azure DW). |
| SMO / Scripting | Added support to script Security Policy on [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is). |

#### Bug fixes in 18.6

| New Item | Details |
|----------|---------|
| Accessibility | Adjusted border colors for accessibility on the **Database general properties page** (the border around the grid and name box is darker to set the contrast to > 3:1). |
| Accessibility | Added handling for query execution to update the narrator (requires NetFx4.8+ installed on the machine). |
| Always Encrypted | Fixed issue where *New Column Encryption Key* dialog says the CEK isn't enclave-enabled even if the CMK is enclave-enabled. |
| Analysis Services | Fixed an issue viewing Analysis Services partitions that may have caused an unhandled exception. |
| **Database Diagrams** | Fixed long outstanding issue with **Database Diagrams**, causing both the corruption of existing diagrams and SSMS to crash. If you created or saved a diagram using SSMS 18.0 through 18.5.1, and that diagram includes a *Text Annotation*, you won't be able to open that diagram in any version of SSMS. With this fix, SSMS 18.6 can open and save a diagram created by SSMS 17.9.1 and prior. SSMS 17.9.1 and previous releases can also open the diagram after being saved by SSMS 18.6. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37992649). |
| Data Classification | Fixed an issue where the column name doesn't show in the recommendation panel of the data classification pane. |
| General SSMS | Fixed an issue where database properties *Size* and *Space Available* have incorrect values for SQL Azure DB (Hyperscale service tier). |
| General SSMS | Fixed an issue where database properties "Size" display the Max Size instead of the actual size of the database for SQL Azure DBs (note: for DW, it still shows the Max Size). |
| General SSMS | Addressed three common sources of hangs in SSMS. |
| General SSMS | Fixed a few issues related to SSMS Connection Dialog *forgetting* entries (server/user/passwords). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/40256401) and [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/40015519). |
| General SSMS | Fixed an issue on the **Statistic Properties** dialog were selecting the **Update statistics for these columns** checkbox and selecting **OK** yields no effect. Statistics aren't updated, and trying to script the action yields a *There is no action to be scripted* message). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37799992). |
| General SSMS | Addressed issues related to [CVE-2020-1455](https://portal.msrc.microsoft.com/security-guidance/advisory/CVE-2020-1455). | 
| Import/Export Data-Tier Application | Fixed an issue where the SSMS was throwing an error when importing a bacpac file. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/40229137). |
| Integration Services | Fixed a bug that customers can't edit a SQL agent job step when using SSMS versions 18.4 or earlier to execute SSIS packages in Azure SQL Managed Instance. |
| Integration Services | Fixed a bug where the **Use 32-bit runtime** option is missing in the **Execution options** tab to execute an SSIS package in a SQL agent job step for an on-premises SQL Server. |
| Intellisense / Editor | Fixed an issue where an error dialog may pop up when doing File -> New -> Database Engine Query. |
| Object Explorer | Fixed an issue where *Properties window* wasn't available for Azure SQL Database when right-clicking on a Table or Index node in Object Explorer. |
| Object Explorer | Addressed an issue where SSMS can't expand databases node for master in Azure if there's a control plane outage affecting sys.database_service_objectives. |
| Reports | Fixed several standard reports that were broken on Linux </br></br> Example: Memory Consumption report was failing with an error similar to "/var/opt/mssql/log/log_116.trc\log.trc' is invalid…"). |
| SMO / Scripting | Updated the logic to create new databases in Azure SQL Database to use Gen5_2 as the default SLO. |
| Xevent UI | Fixed long outstanding issue (introduced in SSMS 18.0) where "Save to XEL file…" was throwing an error. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37695592). |

#### Known issues (18.6)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | Error when connecting to SSAS via msmdpump.dll. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/40144696). | Upgrade to SSMS 18.7 or newer. |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | N/A |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. Workaround: Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". | N/A|
| Integration Services | Remote connections to Integration services may fail with "The specified service does not exist as an installed service." on newer Operating system. Workaround: Identify the Integration services related registry location under Computer\HKEY_CLASSES_ROOT\AppID & Computer\HKEY_CLASSES_ROOT\ WOW6432Node\AppID and within these hives, rename the registry key named 'LocalService' to 'LocalService_A' for the specific version of Integration services that we are trying to connect | N/A|

### 18.5.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.5.1](https://go.microsoft.com/fwlink/?linkid=2132606)

- Release number: 18.5.1
- Build number: 15.0.18333.0
- Release date: June 09, 2020

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2132606&clcid=0x40a)

#### Bug fixes in 18.5.1

| New Item | Details |
|----------|---------|
| Analysis Services | Improved performance when expanding the list of databases while connected to AS Azure or Power BI servers. |
| Analysis Services | Fixed an issue where an error would occur trying to open the Synchronize Database Wizard of an Analysis Services server. |
| Analysis Services | Fixed an issue preventing users from querying SSAS 2017 and earlier versions with cell data permissions. |
| General SSMS | [Table Designer - Fixed beep when trying to TAB in a Table Designer grid](https://feedback.azure.com/forums/908035/suggestions/40318435) |

#### Known issues (18.5.1)

| New Item | Details | Workaround |
|----------|---------|------------|
| General SSMS | There's a known bug with Diagram Design that causes your existing diagrams to get corrupted. For example, you create a Diagram Design with SSMS 17.9.1, then update/save it with SSMS 18.x, and then later try to open it with 17.9.1. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37992649) for more details. | N/A |
| General SSMS | New Server Audit Specification dialog may cause SSMS to crash with an access violation error. | N/A |
| SMO/Scripting | SSMS Extensions using SMO need to be recompiled targeting the new SMO v160. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. Workaround: | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |

### 18.5

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.5](https://go.microsoft.com/fwlink/?linkid=2125901)

- Release number: 18.5
- Build number: 15.0.18330.0
- Release date: April 07, 2020

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2125901&clcid=0x40a)

#### What's new in 18.5

| New Item | Details |
|----------|---------|
| Analysis Services | Added support for Power BI endpoint in Analysis Services - matching functionality of Azure Analysis Services. |
| Analysis Services | Profiler: added support for Analysis Services Trace Definition 15.1. |
| Data Classification | Added a button to the VA scan result view, in order to remediate data classification rule by going to the data classification pane. |
| Data Classification | Added support for sensitivity rank in Data Classification. |
| Hyperscale | Added support for *Import Data-Tier Application* (.bacpac) to SQL Azure HyperScale. |
| Integration Services | Support executing SSIS Package from file system in MI Agent Job. |
| Integration Services | Made user-friendly improvements in configuring Azure-enabled DTExec to invoke SSIS package executions on Azure-SSIS Integration Runtime.
| Integration Services | Support connecting Azure-SSIS integration runtime and managing or executing SSIS packages in package stores.
| Integration Services | Support migrating on-premises SSIS agent jobs to ADF pipelines and triggers.
| Integration Services | Made an improvement for the user experience of exporting SSIS projects from SSIS DB. Compared with the old Export, which loaded and upgraded packages in the SSIS project, the new version-independent Export won't load and upgrade packages in the SSIS project. Instead, it keeps packages in the projects as they are in SSIS DB except changing protection level to EncryptSensitiveWithUserKey. |
| SMO/Scripting | Added new DwMaterializedViewDistribution property to View object. |
| SMO/Scripting | Removed support for *Feature Restriction* (this preview feature has been removed from SQL Azure and SQL on-premises). |
| SMO/Scripting | Added *Notebook* as a destination for Generate Scripts wizard. |
| SMO/Scripting | Added support for *Azure Synapse Analytics serverless SQL pool*. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Platform, Name, and engineEdition fields can now contain usual comma-separated lists (*platform*: \[*Windows*, *Linux*\]), not only regular expressions (*platform*: *\/Windows\|Linux\/*)
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Added 13 assessment rules. For more details, go to [GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-assessment-api)). |

#### Bug fixes in 18.5

| New Item | Details |
|----------|---------|
| Accessibility | SSIS ADF / New Schedule: fixed an issue where the focus order isn't logical in the  scan mode of narrator under *New Schedule* wizard. |
| Accessibility | Stretch database wizard: fixed an issue where the screen reader doesn't inform about the name of the query table when providing information about the table. |
| Analysis Services | Fix cached connection when scripting in AS with Azure AD connection. |
| Always On | Fixed an issue where the first database added to Always On AG doesn't join correctly.
| Always On | Fixed an issue where an error was displayed when trying to display the dashboard when connected to a Big Data Cluster endpoint. |
| Auditing | Fixed an issue where the Audit logs merges window crashes when there's a folder with an empty name in the root folder of the storage account. |
| Auditing | Fixed an issue where the Audit logs merge window doesn't show all servers when there are too many items in the root of the container. |
| Data Classification | Fixed an issue where the *Data Classification* wizard won't open for databases with large number of tables. |
| Data Classification | We are now enforcing different GUIDs for every label/infoType and GUID's structure in the validation process. |
| Data Classification | Remove classification process in SqlServer2019. |
| Data Classification | Correcting the previous validation tests (adding rank, removing the illegal property *InformationTypes*) and adding new ones for the first two points. |
| Data Classification | The button just above the classified columns table now minimizes the recommendations panel, as it says. |
| General SSMS | Updating the version of MSODBC and MSOLEDB drivers. |
| General SSMS | Addressed at least two common sources hangs and crashes in SSMS. |
| General SSMS | Addressed one more case where *Restore dialog* hangs when selecting the Browse button. |
| General SSMS | Fixed *New Database GUI* for Azure Synapse Analytics serverless SQL pool. |
| General SSMS | Fixed *New External Table...* and *New External Data Source...* templates for Azure Synapse Analytics serverless SQL pool. |
| General SSMS | Fixed database properties, connection properties, hiding reports and rename for Azure Synapse Analytics serverless SQL pool. |
| General SSMS | Always Encrypted: Fixed an issue where the key name dropdown becomes read-only on selecting new enclave enabled key. |
| General SSMS | Cleaned up the *Database Property Options* grid, which was showing two *Miscellaneous Categories*. |
| General SSMS | Fixed an issue where the scroll bar started from middle in "Database Properties Options" grid. |
| General SSMS | Fixed an issue that was causing SSMS to crash when opening .sql file while connected to Analysis Services server. |
| General SSMS | Connection Dialog: fixed an issue where unchecking the "Remember Password" doesn't work. |
| General SSMS | Fixed an issue where credentials associated to Server/Users are always remembered. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37875172). |
| General SSMS | Fixed issue where occasionally Editor windows wasn't properly refreshed. This is achieved by disabling the hardware acceleration in *Tools > Options > Environment*. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37474042). |
| General SSMS | Fixed an issue where Azure Active Directory authentication wasn't working through a proxy. |
| High DPI/Scaling | Fixed an issue where the controls on the *Index properties* could be incorrectly rendered (buttons overlapping grid). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/36030424). |
| High DPI/Scaling | Fixed multiple issues in *Database Properties* dialog, which might show clipped controls on 4K monitors. |
| High DPI/Scaling | Fixed Publication and Subscription Wizards on 4k displays. |
| High DPI/Scaling | Minor fix on New Audit Server Specification page. |
| High DPI/Scaling | Fixed 4k display issue on High Availability Wizard. |
| High DPI/Scaling | Fixed an issue where the user wasn't able to add a target in an Xevent New Session window + Set Session Event Filters in Xevent Session Wizard when display scaling at 125%. |
| High DPI/Scaling | Fixing an issue where controls on the *Backup Database to URL* UI render out of sight under scaling above 100%. |
|Import flat file | Updated Flat File Import Wizard to allow check all for the allow null column. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/38027137). |
| Object Explorer | Fixed an issue where Object Explorer could display incorrect information when connection strings are used to connect in Connection Dialog. |
| Object Explorer | Fixed an issue where OE was slow in expanding tables for databases with several thousands tables (20k+). |
| Query Store UI | Fixed TRC report calculate execution count (for *wait time* metric) as the sum of execution counts for each individual wait category, which is incorrect. But for a single execution of query it will be registered for each of the wait category the query waited for. So, if TRC just sums it across the wait category it will bloat the execution count. Actually it should be the Max across the wait_category. |
| Query Store UI | Fixed TRC detailed view returns incorrect data when result set is filtered on top x. This happens because the query uses multiple common table expressions, which are then joined together to create the final result set. If top x is pushed into CTE, it sometimes can filter out the required rows. This can sometimes make the result set non-deterministic. The fix is to not push top x clause to CTEs. |
| Query Store UI | Fixed Plan summary in both - grid or chart view needs last query execution wait time. Absence of this column is breaking the query. This change set will add this column to the wait stats CTE. |
| ShowPlan | Improved how SSMS displays estimated row counts for operators with multiple executions: (1) Modified *Estimated Number of Rows* in SSMS to "Estimated Number of Rows Per Execution"; (2) Added a new property *Estimated Number of Rows for All Executions*; (3) Modify the property *Actual Number of Rows* to *Actual Number of Rows for All Executions*. |
| SQL Agent | Fixed an issue where trying to edit a SQL Agent job step could have resulted in the SSMS UI freezing. SSMS is now allowing viewing (*View* button) an output_file whose name is tokenized (at least for the simple macros/tokens supported by SQL Agent that aren't determined at runtime). Also SSMS isn't disabling the "View" button when the user doesn't have access to the file (as far as SQL permissions go). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/39063124). |
| SQL Agent | Fixed the tab ordering on the Job Step page. |
| SQL Agent | Reversed the position of the "Next" and "Previous" buttons on the Job Step page to put them in a logical order. |
| SQL Agent | Adjusted the Job Schedule window to not clip the UI. |
| SMO/Scripting | Fixed database scripting for Azure Synapse Analytics serverless SQL pool. |
| SMO/Scripting | Removing explicit sqlvariant cast (illegal T-SQL syntax for SqlOnDemand) which fixes scripting for SqlOnDemand. |
| SMO/Scripting | Fixed an issue where FILLFACTOR on indexes for SQL Azure was skipped. |
| SMO/Scripting | Fixed an issue related to scripting External objects. |
| SMO/Scripting | Fixed an issue where *Generate Scripts* wasn't allowing choosing the scripting option for Extended Properties against SQL Database. Also, fixed the scripting of such extended properties. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Wrong help link in XTPHashAvgChainBuckets rule. |
| XEvent UI | Fixed an issue here items in the grid were being selected on hovering. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/38262124) and [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/37873921). |

#### Known issues (18.5)

| New Item | Details | Workaround |
|----------|---------|------------|

- Database Diagram created from SSMS running on machine A cannot be modified from machine B (SSMS crashes). See [SQL Server user feedback 37992649](https://feedback.azure.com/forums/908035/suggestions/37992649) for more details.

- When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. A workaround is to remove the folder *C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild*.

- New Server Audit Specification dialog may cause SSMS to crash with an access violation error.

- SSMS Extensions using SMO would need to be recompiled targeting the new SMO v160 (package will be available on nuget.org right after SSMS 18.5 is released).

- [Error when connecting to SSAS via msmdpump.dll in SSMS](https://feedback.azure.com/forums/908035-sql-server/suggestions/40144696-error-when-connecting-to-ssas-via-msmdpump-dll-in).

### 18.4

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.4](https://go.microsoft.com/fwlink/?linkid=2108895)

- Release number: 18.4
- Build number: 15.0.18206.0
- Release date: November 04, 2019

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2108895&clcid=0x40a)

| New Item | Details |
|----------|---------|
| Data Classification | Added support for custom information protection policy for data classification. |
| Query Store | Added the *Max Plan per query* value in the dialog properties. |
| Query Store | Added support for the new Custom Capture Policies. |
| Query Store | Added **Wait Statistics Capture Mode** to the **Query Store** **Database Properties** options. |
| SMO/Scripting | Support Script of materialized view in SQL DW. |
| SMO/Scripting | Added support for *Azure Synapse Analytics serverless SQL pool*. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Added 50 assessment rules (see details on GitHub). |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Added base math expressions and comparisons to rules conditions. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Added support for RegisteredServer object. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Updated way how rules are stored in the JSON format and also updated the mechanism of applying overrides/customizations. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Updated rules to support SQL on Linux. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Updated the ruleset JSON format and added SCHEMA version. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Updated cmdlets output to improve readability of recommendations. |
| XEvent Profiler | Added *error_reported* event to XEvent Profiler sessions. |

#### Bug fixes in 18.4

| New Item | Details |
|----------|---------|
| Analysis Services | Fixed an issue where the DAX Script editor for multi-dimensional databases didn't show tables in the IntelliSense. |
| Analysis Services | Use DAX Parser to convert to an Engine string. This is for international separators, decimal, and whitespace. |
| Always Encrypted | Fixed an issue where the *claim validation* wasn't *case insensitive*. |
| Always Encrypted | Fixed an issue where error/warning reporting wasn't properly working. |
| Copy Database Wizard | Fixed assorted truncation and layout issues in the rendering of this dialog. |
| General SSMS | Fixed a long outstanding issue where SSMS wasn't honoring the connection info passed at the command line when SQL files were also specified. |
| General SSMS | Fixed a crash in SSMS while trying to display Securables on "Replication Filter" objects. |
| General SSMS | Mitigated the removal of the -P command-line option by having SSMS look at its cache of credentials: if the required credential were found, the connection would be established using it. |
| Import flat file | Fixed an issue where *Import Flat File* functionality not handling text qualifiers correctly. |
| Object Explorer | Fixed an issue where dropping an Azure SQL Database in Object Explorer was showing an incorrect message. |
| Query Results | Fixing an issue introduced in SSMS 18.3.1, where grids are drawn slightly too narrow and show *...* at the end of the longest string in every column. |
| Replication Tools | Fixed an issue that was causing the application to throw an error ("couldn't load file or assembly…") when trying to edit SQL Agent jobs. |
| SMO/Scripting | Fixed an issue when *Script Table As…* for SQL DW whose collation is Japanese_BIN2 wasn't working.|
| SMO/Scripting | Fixed an issue where ScriptAlter() ended up executing the statements on the server.|
| SQL Agent | Fixed an issue where the agent operator UI would not update the operator name when it was changed in the UI, nor is it scripted. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/32897647) for more details.|

#### Known issues (18.4)

- Database Diagram created from SSMS running on machine A cannot be modified from machine B (SSMS crashes). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37992649) for more details.

- There are redraw issues when switching between multiple query windows. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37474042) for more details. A workaround for this issue is to disable hardware acceleration under *Tools > Options*.

You can reference [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server) for other known issues and to provide feedback to the product team.

### 18.3.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.3.1](https://go.microsoft.com/fwlink/?linkid=2105412)

- Release number: 18.3.1
- Build number: 15.0.18183.0
- Release date: October 02, 2019

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2105412&clcid=0x40a)

#### What's new in 18.3.1

| New Item | Details |
|----------|---------|
| Data Classification | Add Data Classification information to column properties UI (*Information Type*, *Information Type ID*, *Sensitivity Label*, and *Sensitivity Label ID* aren't exposed in the SSMS UI). |
| Intellisense/Editor | Updated support for features recently added to SQL Server 2019 (for example, *ALTER SERVER CONFIGURATION*). |
| Integration Services | Add a new selection menu item `Tools > Migrate to Azure > Configure Azure-enabled DTExec` that invokes SSIS package executions on Azure-SSIS Integration Runtime as Execute SSIS Package activities in ADF pipelines. |
| SMO/Scripting | Added support for Support scripting of Azure SQL DW unique constraint. |
| SMO/Scripting | Data Classification </br> - Added support for SQL version 10 (SQL 2008) and higher. </br> - Added new sensitivity attribute 'rank' for SQL version 15 (SQL 2019) and higher and Azure SQL Database. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Added versioning to ruleset format. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Added new checks. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Added support for Azure SQL Managed Instance. |
| SMO/Scripting | [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) - Updated default view of cmdlets to display results as a table. |

#### Bug fixes in 18.3.1

| New Item | Details |
|----------|---------|
| Analysis Services | Fix scaling issue in MDX Query Editor.|
| Analysis Services | Fixed an issue in XEvent UI that prevents the users from being able to create a new session. |
| Database Deployment to SQL Azure | Fixed an issue (in DacFx) which was causing this feature not to work.|
| General SSMS | Fixed an issue, which was causing SSMS to crash when using the sorting feature in the XEvent viewer. |
| General SSMS | Fixed long outstanding issues where SSMS Restore database may stop responding indefinitely. </br></br> See SQL Server user feedback items for more details: </br> [Restore Database - Select Backup Devices Slow to Load](https://feedback.azure.com/forums/908035/suggestions/32899099/).  </br> [SSMS 2016 very slow in the database restore dialogs](https://feedback.azure.com/forums/908035/suggestions/32900767/). </br> [Restoring database is slow](https://feedback.azure.com/forums/908035/suggestions/32900224/).  </br> [Restore Database from Device HANGS on clicking "..."](https://feedback.azure.com/forums/908035/suggestions/34281658/).  |
| General SSMS | Fixed an issue where the default language for all logins was shown as Arabic. </br></br> See SQL Server user feedback item for more details: [SSMS 18.2 default language display bug](https://feedback.azure.com/forums/908035/suggestions/38236363). |
| General SSMS | Fixed the hard to see the dialog for *Query Options* (when the user right-clicks on the T-SQL editor window) by making it resizable.|
| General SSMS | The *Completion time* message visible in the Result Grid/File (introduced in SSMS 18.2) is now configurable under Tools > Options >  Query Execution > SQL Server > Advanced > Show completion time. |
| General SSMS | In the connection dialog, replaced *Active Directory - Password* and *Active Directory - Integrated* with *Azure Active Directory - Password* and *Azure Active Directory - Integrated*, respectively. |
| General SSMS | Fixed an issue that prevents users from being able to use SSMS to configure auditing on SQL Azure managed Instances when located in a TZ with negative UTC offset. |
| General SSMS | Fixing an issue in XEvent UI where hovering over the grid was causing rows to be selected. </br></br> See SQL Server user feedback item for more details: [SSMS Extended Events UI Selects Actions on Hover](https://feedback.azure.com/forums/908035/suggestions/38262124). |
| Import flat file | Fixed the issue where Import Flat File wasn't importing all data by letting the user choose between a simple or rich data type detection.</br></br> See SQL Server user feedback item for more details: [SSMS Import Flat File fails to import all data](https://feedback.azure.com/forums/908035/suggestions/38096989). |
| Integration Services | Add new operation type *StartNonCatalogExecution* for SSIS Operation report.|
| Integration Services | Fixed an issue in the Azure Data Factory pipelines generated by Azure-enabled `DTExec` utility to use the correct parameter type. (explicit for 18.3.1) |
| SMO/Scripting | Fixed an issue, which was causing SMO to throw errors when fetching properties when **SMO.Server.SetDefaultInitFields(true)** was being used.|
| Query Store UI | Fixed an issue where the Y-axis didn't scale when *Execution Count* Metric was selected in *Tracked Query* View. |
| Vulnerability Assessment | Disabled clearing and approving baseline for Azure SQL Database.|

#### Known issues (18.3.1)

- Database Diagram created from SSMS running on machine A cannot be modified from machine B (SSMS crashes). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37992649) for more details.

- There are redraw issues when switching between multiple query windows. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37474042) for more details. A workaround for this issue is to disable hardware acceleration under Tools > Options.

You can reference [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server) for other known issues and to provide feedback to the product team.

### 18.2

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.2](https://go.microsoft.com/fwlink/?linkid=2099720)

- Release number: 18.2
- Build number: 15.0.18142.0
- Release date: July 25, 2019

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2099720&clcid=0x40a)

#### What's new in 18.2

| New Item | Details |
|----------|---------|
| Integration Services (SSIS) | Perf optimization for SSIS package scheduler in Azure. |
| Intellisense/Editor | Added support for Data Classification. |
| OPTIMIZE_FOR_SEQUENTIAL_KEY | Added Intellisense support. |
| OPTIMIZE_FOR_SEQUENTIAL_KEY | Turns on an optimization within the database engine that helps improve throughput for high-concurrency inserts into the index. This option is intended for indexes that are prone to last-page insert contention, typically seen with indexes that have a sequential key such as an identity column, sequence, or date/time column. See [CREATE INDEX](../t-sql/statements/create-index-transact-sql.md#sequential-keys) for more details. |
| Query Execution or Results | Added a *Completion time* in the messages to track when a given query completed its execution. |
| Query Execution or Results | Allow more data to be displayed (Result to Text) and stored in cells (Result to Grid). SSMS now allows up to 2M characters for both (up from 256 K   and 64 K, respectively). This also addressed the issue of users not able to grab more than 43680 chars from the cells of the grid. |
| ShowPlan | Added a new attribute in QueryPlan when [inline scalar UDF feature](../relational-databases/performance/intelligent-query-processing-details.md#scalar-udf-inlining) is enabled  (ContainsInlineScalarTsqludfs). |
| SMO | Added support for *SQL Assessment API*. For more information, see [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md). |

#### Bug fixes in 18.2

| New Item | Details |
|------------|-------|
| Accessibility | Updated the XEvent UI (the grid) to be sortable by pressing F3. |
| Always On | Fixed an issue where SSMS was throwing an error when trying to delete an Availability Group (AG) |
| Always On | Fixed an issue where SSMS was presenting the wrong failover wizard when replicas are configured as Synchronous when using read scale AGs (cluster type=NONE). Now, SSMS presents the wizard for Force_Failover_Allow_Data_Loss option, which is the only one allowed for cluster-type NONE Availability |
| Always On | Fixed an issue where the wizard was restricting the number of allowed synchronizations to three |
| Data Classification | Fixed an issue where SSMS was throwing an *Index (zero-based) must be greater than or equal to zero* error when trying to view data classification reports on databases with CompatLevel less than 150. |
| General SSMS | Fixed an issue where the user was unable to horizontal scroll Results pane via mouse wheel. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/34145641) for more details. |
| General SSMS | Updated *Activity Monitor* to ignore benign wait types SQLTRACE_WAIT_ENTRIES |
| General SSMS | Fixed an issue where some color options *(Text Editor > Editor Tab and Status Bar)* weren't persisted. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37924165)
| General SSMS | In the connection dialog, replaced *Azure Active Directory - Universal with MFA support* with *Azure Active Directory - Universal with MFA* (functionality is the same, but hopefully it's less confusing). |
| General SSMS | Updated SSMS to use correct default values when creating an Azure SQL Database. |
| General SSMS | Fixed an issue where the user wasn't able to *Start PowerShell* from a node in [Register Servers](register-servers/register-servers.md) when the server is a [SQL Linux container](../linux/quickstart-install-connect-docker.md). |
| Import Flat File | Fixed an issue where *Import Flat File* wasn't working after upgrading from SSMS 18.0 to 18.1. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37912636) |
| Import Flat File | Fixed an issue where *Import Flat File Wizard was reporting a duplicate or invalid column* on a .csv file with headers with Unicode characters. |
| Object Explorer | Fixed an issue where some menu items (for example, SQL server *Import and Export Wizard*) were missing or disabled when connected to SQL Express. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37500016) for more details. |
| Object Explorer | Fixed an issue, which was causing SSMS to crash when an object is dragged from Object Explorer to the editor. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37887988) for more details. |
| Object Explorer | Fixed an issue where renaming databases was causing incorrect database names to show up in Object Explorer. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37638472) for more details. |
| Object Explorer | Fixed a long outstanding issue where trying to expand the *Tables* node in Object Explorer for a database, which is set to use a collation that isn't supported by Windows anymore triggers an error (and the user can't expand their tables). An example of such collation would be Korean_Wansung_Unicode_CI_AS. |
| [Register Servers](register-servers/register-servers.md) | Fixed an issue where trying to issue a query against multiple servers (under a *Group* in Registered Servers) when the Registered Server uses either *Active Directory - Integrated* or *Azure Active Directory - Universal with MFA* didn't work because SSMS failed to connect. |
| [Register Servers](register-servers/register-servers.md) | Fixed an issue where trying to issue a query against multiple servers (under a *Group* in Registered Servers) when the registered server uses either *Active Directory - Password* or *SQL Auth* and the user chose not to remember the password would cause SSMS to crash. |
| Reports | Fixed an issue in *Disk Usage* reports where the report was failing to when data files had a vast number of extents. |
| Replication Tools | Fixed an issue where Replication Monitor wasn't working with publisher DB in AG and distributor in AG (this was previously fixed in SSMS 17.x |
| SQL Agent | Fixed an issue that when Adding, inserting, editing, or removing job steps, was causing focus to be reset the first row instead of the active row. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/38070892) for more details. |
| SMO/Scripting | Fixed an issue where *CREATE OR ALTER* wasn't scripting objects that had extended properties on them. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/37236748) for more details. |
| SMO/Scripting | Fixed an issue where SSMS wasn't able to script CREATE EXTERNAL LIBRARY correctly. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37868089) for more details. |
| SMO/Scripting | Fixed an issue where trying to run the *Generate Scripts* against a database with a few thousand tables (was causing the progress dialog to appear to be stuck. |
| SMO/Scripting | Fixed an issue where scripting of *External Table* on SQL 2019 didn't work. |
| SMO/Scripting | Fixed an issue where scripting of *External Data Source* on SQL 2019 didn't work. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/34295080) for more details. |
| SMO/Scripting | Fixed an issue where * extended properties* on columns weren't scripted when targeting Azure SQL Database. See [stackoverflow](https://stackoverflow.com/questions/56952337/how-can-i-script-the-descriptions-of-columns-in-ms-sql-server-management-studio) for more details. |
| SMO/Scripting | Last-page insert: SMO - Add property *Index.IsOptimizedForSequentialKey* |
|**SSMS Setup**| **Mitigated an issue where SSMS setup was incorrectly blocking the installation of SSMS reporting mismatching languages. This could have been an issue in some abnormal situations, like an aborted setup or an incorrect uninstall of a previous version of SSMS. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37483594/) for more details.** |
| XEvent Profiler | Fixed a crash when the viewer is being closed. |

#### Known issues (18.2)

- Database Diagram created from on an SSMS running on machine A cannot be modified from machine B (it would crash SSMS). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37992649) for more details.

- There are redraw issues when switching between multiple query windows. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37474042). A workaround for this issue is to disable hardware acceleration under **Tools** > **Options**.

- There's a limitation on the size of the data you see from SSMS results shown in grid, text, or file.

- There's an issue with receiving an error when deleting an Azure SQL Database in Object Explorer, but it actually succeeds.

- The default language for SQL logins may display as Arabic in the Login Properties dialog, regardless of the actual default language set for the login. To view the actual default language for a given login, use T-SQL to select the login's **default_language_name** from **master.sys.server_principles**.

### 18.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.1](https://go.microsoft.com/fwlink/?linkid=2094583)

- Release number: 18.1
- Build number: 15.0.18131.0
- Release date: June 11, 2019

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2094583&clcid=0x40a)

#### What's new in 18.1

| New Item | Details |
| :-------| :------|
| Database diagrams | [Database diagrams were added back into SSMS](https://feedback.azure.com/forums/908035/suggestions/37507828).
| SSBDIAGNOSE.EXE |The SQL Server Diagnose (command-line tool) was added back into the SSMS package.|
| Integration Services (SSIS) | Support for scheduling SSIS package, located in SSIS Catalog in Azure or File System, in Azure. There are three entries for launching the New Schedule dialog, *New Schedule…* menu item is shown when right-clicking the SSIS package in SSIS Catalog in Azure, *Schedule SSIS Package in Azure* menu item under *Migrate to Azure* menu item under *Tools* menu item and "Schedule SSIS in Azure" shown when right-clicking Jobs folder under SQL Server agent of Azure SQL Managed Instance.|

#### Bug fixes in 18.1

| New Item | Details |
|---------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Accessibility | Improved accessibility of the Agent Job UI. |
| Accessibility | Improved accessibility on the Stretch Monitor page by adding an accessible name for *Auto Refresh* button and also adding an intelligent Accessible Name that helps users know not only what button they're on but the impact of pressing it. |
| ADS integration| Fixed a possible crash in SSMS when trying to use the ADS registered servers.|
| Database designer | Added support for Latin1_General_100_BIN2_UTF8 collation (available in SQL Server 2019 CTP3.0) |
| Data classification | Prevent adding sensitivity labels to columns in the history table, which isn't supported. |
| Data classification | Addressed issue related to incorrect handling of compatibility level (server vs. database). |
| Database designer | Added support for Latin1_General_100_BIN2_UTF8 collation (available in SQL2019 CTP3.0). |
| General SSMS | Fixed an issue where scripting of pseudo columns in an index was incorrect. |
| General SSMS | Fixed an issue in the Login properties page where clicking on the *Add Credential* button could throw a Null Reference Exception. |
| General SSMS | Fixed money column size display in the index properties page. |
| General SSMS | Fixed an issue where SSMS wasn't honoring the Intellisense settings from *Tools/Options* in the SQL editor window. |
| General SSMS | Fixed an issue where SSMS wasn't honoring the Help settings (online vs. offline). |
| High DPI | Fixed layout of controls in error dialogs for unsupported query options. |
| High DPI | Fixed layout of controls in *New Availability Group* page, which is on some localized version of SSMS. |
| High DPI | Fixed layout of *New Job Schedule* page. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37632094) for more details. |
| Import flat file | Fixed in an issue where rows could be silently lost during the import.|
| Intellisense/editor | Reduced SMO-based query traffic to Azure SQL Database for IntelliSense. |
| Intellisense/editor | Fixed grammatical error in the tooltip displayed when typing T-SQL to create a user. Also, fixed the error message to disambiguate between users and logins. |
| Log Viewer | Fixed an issue where SSMS always opens the current server (or agent) log, even if double-clicking an older archive sign in the Object Explorer. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37633648) for more details. |
| SSMS setup | Fixed the issue that was causing SSMS setup to fail when the setup log path contained spaces. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37496110) for more details. |
| SSMS setup | Fixed an issue where SSMS was exiting immediately after showing the splash screen. </br> See these sites for more details: [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37502512), [SSMS Refuses to Start](https://dba.stackexchange.com/questions/238609/ssms-refuses-to-start), and [Database Administrators](https://dba.stackexchange.com/questions/237086/sql-server-management-studio-18-wont-open-only-splash-screen-pops-up). |
| Object explorer | Lifted restriction on enabling *start PowerShell* when connected to SQL on Linux. |
| Object explorer | Fixed an issue that was causing SSMS to crash when trying to expand the PolyBase/Scale-out Group node (when connected to a compute node). |
| Object explorer | Fixed an issue where the *Disabled* menu item was still enabled, even after disabling a given Index. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37735375) for more details. |
| Reports | Correcting report to display GrantedQueryMemory in KB (SQL Performance   Dashboard report). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37167289) for more details. |
| Reports | Improved the tracing of the log block in Always-On scenarios. |
| ShowPlan | Added new showplan element *SpillOccurred* to showplan schema. |
| ShowPlan | Added remote reads (*ActualPageServerReads*, *ActualPageServerReadAheads* *ActualLobPageServerReads*, *ActualLobPageServerReadAheads*) to showplan schema. |
| SMO/scripting | Avoid querying edge constraints during scripting for non-graph tables. |
| SMO/scripting | Removed constraint on sensitivity classification when scripting columns with *Data classification*. |
| SMO/scripting | Fixed an issue where "Generate Script" on a graph table fails when   generating data. See [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server/suggestions/32898466) for more details. |
| SMO/scripting | Fixed an issue where the EnumObjects() method wasn't fetching schema name for a Synonym. |
| SMO/scripting | Fixed an issue in UIConnectionInfo.LoadFromStream() where the *AdvancedOptions* section wasn't read (when a password wasn't specified). |
| SQL Agent | Fixed an issue that was causing SSMS to crash while working with a Job Properties window. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37164244) for more details. |
| SQL Agent | Fixed an issue where the "View" button on the *Job-Step Properties* wasn't always enabled, thus preventing viewing the output of a given job step. |
| XEvent UI | Added "Package" column to XEvents list to disambiguate events with identical names. |
| XEvent UI | Added missing "EXTERNAL LIBRARY" class type mapping to XEventUI. |

#### Known issues (18.1)

- Users may see an error when dragging a table object from the Object Explorer into the Query Editor. We are aware of the issue, and the fix is planned for the next release.

- The *Group connections* and *Single-server connections* color options under the Options -> Text Editor -> Editor Tab and Status Bar -> Status Bar Layout and Colors do not persist after closing SSMS 18.1. After you reopen SSMS, the Status Bar Layout and Colors option revert to default (white).

- There is a limitation on the size of the data you see from SSMS results shown in the grid, text, or file.

- Database Diagram created from SSMS running on machine A cannot be modified from machine B (SSMS crashes). See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37992649) for more details.

### 18.0

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.0](https://go.microsoft.com/fwlink/?linkid=2088649)

- Release number: 18.0  
- Build number: 15.0.18118.0  
- Release date: April 24, 2019

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x804)| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x404)| [English (United States)](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x409)| [French](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x40c)| [German](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x407)| [Italian](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x410)| [Japanese](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x411)| [Korean](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x412)| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x416)| [Russian](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x419)| [Spanish](https://go.microsoft.com/fwlink/?linkid=2088649&clcid=0x40a)

#### What's new in 18.0

| New Item| Details|
| :-------| :------|
|Support for SQL Server 2019|SSMS 18.0 is the first release that is fully *aware* of SQL Server 2019 (compatLevel 150).|
|Support for SQL Server 2019|Support for "BATCH_STARTED_GROUP" and "BATCH_COMPLETED_GROUP" in SQL Server 2019 and SQL Managed Instance.|
|Support for SQL Server 2019|SMO: Added support for UDF Inlining.|
|Support for SQL Server 2019|GraphDB: Add a flag in showplan for Graph TC Sequence.|
|Support for SQL Server 2019|Always Encrypted: added support for AEv2 / Enclave.|
|Support for SQL Server 2019|Always Encrypted: connection dialog has a new tab "Always Encrypted" when the user clicks on the "Options" button to enable/configure Enclave support.|
|Smaller SSMS download size| The current size is ~500 MB, approximately half of the SSMS 17.x bundle.
|SSMS is based on the Visual Studio 2017 Isolated Shell|The new shell (SSMS is based on Visual Studio 2017 15.9.11) unlocks all the accessibility fixes that went into both SSMS and Visual Studio, and includes the latest security fixes.|
|SSMS accessibility improvements| Much work went in to address accessibility issues in all the tools (SSMS, DTA, and Profiler)|
|SSMS can now be installed in a custom folder| This option is available from both the command line (useful for unattended installation) and the setup UI. From the command line, pass this extra argument to the SSMS-Setup-ENU.exe:   SSMSInstallRoot=C:\MySSMS18  By default, the new install location for SSMS is: %ProgramFiles(x86)%\Microsoft SQL Server Management Studio 18\Common7\IDE\ssms.exe.  This does not mean that SSMS is multi-instance.|
|SSMS allows installing in a language other than the OS language|The block on mixed languages setup has been lifted. You can, for example, install SSMS German on a French Windows. If the OS language does not match the SSMS language, the user needs to change the language under **Tools** > **Options** > **International Settings**, otherwise SSMS shows the English UI.|
|SSMS no longer shares components with the SQL Engine|Much effort went in to avoid sharing components with SQL Engine, which often resulted in serviceability issues (one clobbering the files installed by the other).|
|SSMS requires NetFx 4.7.2 or greater|We upgraded our minimum requirement from NetFx4.6.1 to NetFx4.7.2: this allows us to take advantage of the new functionality exposed by the new framework.|
|Ability to migrate SSMS settings| When SSMS 18 is started for the first time, the user is prompted to migrate the 17.x settings. The user setting files are now stored as a plain XML file, thus improving portability and possibly allowing editing.|
|Support for High DPI| High DPI is now enabled by default.|
|SSMS ships with the Microsoft OLE DB driver| For details, see [Download Microsoft OLE DB Driver for SQL Server](../connect/oledb/download-oledb-driver-for-sql-server.md).|
|SSMS isn't supported on Windows 8. Windows 10 and Windows Server 2016 require version 1607 (10.0.14393) or later|Due to the new dependency on NetFx 4.7.2, SSMS 18.0 does not install on Windows 8 and older versions of Windows 10 and Windows Server 2016. SSMS setup blocks those systems. Windows 8.1 is still supported.|
|SSMS is no longer added to the PATH environment variable|Path to SSMS.EXE (and tools in general) isn't added to the path anymore. Users can either manually add it, or if on a modern Windows computer, use on the Start menu.|
|Package IDs are no longer needed to develop SSMS Extensions| In the past, SSMS was selectively loading only well-known packages, thus requiring developers to register their own package. This is no longer the case.|
|General SSMS|Exposing AUTOGROW_ALL_FILES config option for Filegroups in SSMS.|
|General SSMS|Removed risky 'lightweight pooling' and 'priority boost' options from SSMS GUI. For details, see [Priority boost details – and why it's not recommended](https://deep.data.blog/2010/01/26/priority-boost-details-and-why-its-not-recommended/).
|General SSMS|New menu and key bindings to creates files: **CTRL+ALT+N**. **CTRL+N** continues to create a new query.|
|General SSMS|**New Firewall Rule** dialog now allows the user to specify a rule name, instead of automatically generating one on behalf of the user.|
|General SSMS|Improved IntelliSense in Editor especially for v140+ T-SQL.|
|General SSMS|Added support in SSMS UI for UTF-8 on collation dialog.|
|General SSMS|Switched to "Windows Credential Manager" for connection dialog MRU passwords. This addresses a long outstanding issue where persistence of passwords wasn't always reliable.|
|General SSMS|Improved support for multi-monitor systems by making sure that more and more dialogs and windows pop up on the expected monitor.|
|General SSMS|Exposed the 'backup checksum default' server configuration in the new Database Settings page of the Server Properties Dialog. For details, see [https://feedback.azure.com/forums/08035-sql-server/suggestions/34634974](https://feedback.azure.com/forums/08035-sql-server/suggestions/34634974).|
|General SSMS|Exposed "maximum size for error log files" under "Configure SQL Server Error Logs". For details, see  [https://feedback.azure.com/forums/908035/suggestions/33624115](https://feedback.azure.com/forums/908035/suggestions/33624115).|
|General SSMS|Added "Migrate to Azure" under Tools menu – We have integrated Database Migration Assistant and Azure Database Migration Service to provide quick and easy access to help accelerate your migrations to Azure.|
|General SSMS|Added logic to prompt the user to commit open transactions when "Change connection" is used.|
|Azure Data Studio integration|Added menu item to start/download Azure Data Studio.|
|Azure Data Studio integration|Added "Start Azure Data Studio" menu item to Object Explorer.|
|Azure Data Studio integration|When right-clicking on a database node in OE, the user is presented with context menus to either run a query or create a new notebook in Azure Data Studio.|
|Azure SQL support| SLO/Edition/MaxSize database properties now accept custom names, making it easier to support future editions of Azure SQL Database.|
|Azure SQL support| Added support for vCore SKUs (General Purpose and Business Critical): Gen4_24 and all the Gen5.|
|Azure SQL Managed Instance|Added new "Azure AD logins" as a new login type in SMO and SSMS when connected to Azure SQL Managed Instance.|
|Always On|Rehash RTO (estimated recovery time)  and RPO (estimated data loss) in SSMS Always on Dashboard. For more information, see [Monitor performance for Always On availability groups](../database-engine/availability-groups/windows/monitor-performance-for-always-on-availability-groups.md).|
|Always Encrypted| The Enable Always Encrypted checkbox in the new Always Encrypted tab in the Connect to Server dialog now provides an easy way to enable/disable Always Encrypted for a database connection.|
|Always Encrypted with secure enclaves| Several enhancements have been made to support  Always Encrypted with secure enclaves in SQL Server 2019:  A text field for specifying enclave attestation URL in the Connect to Server dialog (the new Always Encrypted tab).  The new checkbox in the New Column Master Key dialog to control whether a new column master key allows enclave computations.  Other Always Encrypted key management dialogs now expose the information on which column master keys allow enclave computations.|
|Audit Files|Changed authentication method from Storage Account Key based to Azure AD-based authentication.|
|Data Classification| Reorganized data classification task menu: added sub menu to the database tasks menu and added an option to open the report from the menu without opening the classify data window first.|
|Data Classification|Added new feature 'Data classification' to SMO. Column object exposes new properties: SensitivityLabelName, SensitivityLabelId, SensitivityInformationTypeName, SensitivityInformationTypeId, and IsClassified (read-only). For more information, see [ADD SENSITIVITY CLASSIFICATION (Transact-SQL)](../t-sql/statements/add-sensitivity-classification-transact-sql.md)|
|Data Classification|Added new "Classification Report" menu item to the "Data Classification" flyout.|
|Data Classification| Updated recommendations.|
|Database Compatibility Level Upgrade|Added a new option under ***Database name*** > ***Tasks*** > ***Database Upgrade***. This starts the new **Query Tuning Assistant (QTA)** to guide the user through the process of: Collecting a performance baseline before upgrading the database compatibility level. Upgrading to the desired database compatibility level.  Collecting a second pass of performance data over the same workload. Detect workload regressions, and provide tested recommendations to improve workload performance.  This is close to the database upgrade process documented in [query store usage scenarios](../relational-databases/performance/query-store-usage-scenarios.md#CEUpgrade), except for the last step where QTA does not rely on a previously known good state to generate recommendations.|
|Data-tier Application Wizard|Added support to Import/Export data-tier application with graph tables.|
|Flat File Import Wizard|Added logic to notify the user that an import may have resulted in a renaming of the columns.|
|Integration Services (SSIS)|Added support to allow customers to schedule SSIS packages on Azure-SSIS IRs that are in Azure Government cloud.|
|Integration Services (SSIS)|When you use SQL Agent of Azure SQL Managed Instance via SSMS, you can configure parameter and connection manager in SSIS agent job step.|
|Integration Services (SSIS)|When connecting to Azure SQL Database / Azure SQL Managed Instance, you can connect to it with *default* as initial db.|
|Integration Services (SSIS)|Added a new entry item **Try SSIS in Azure Data Factory** under "Integration Services Catalogs" node, which can be used to launch the "Integration Runtime Creation Wizard" and create "Azure-SSIS Integration Runtime" quickly.
|Integration Services (SSIS)|Added **Create SSIS IR** button in "Catalog Creation Wizard," which can be used to launch the "Integration Runtime Creation Wizard" and create "Azure-SSIS Integration Runtime" quickly.|
|Integration Services (SSIS)|ISDeploymentWizard now supports SQL Auth, Azure Active Directory Integrated Auth, and Azure Active Directory Password Auth in command-line mode.|
|Integration Services (SSIS)|Deployment Wizard now supports creating and deploying to Azure Data Factory SSIS Integration Runtime.|
|Object Scripting|Add new menu items for "CREATE OR ALTER" when scripting objects.|
|Query Store|Improved usability of some reports (Overall Resource Consumptions) by adding thousands of separators to numbers displayed on the Y-axis of the charts.|
|Query Store|Added a new Query Wait Statistics report.|
|Query Store|Added "Execution Count" metric to "Tracked Query" View.|
|Replication Tools|Added support for non-default port specification feature in Replication Monitor and SSMS.|
|ShowPlan|Added actual time elapsed, actual vs. estimated rows under ShowPlan operator node if they're available. This makes the actual plan look consistent with Live Query Stats plan.|
|ShowPlan|Modified tooltip and added comment when clicking on Edit Query Button for a ShowPlan, to indicate to user that the ShowPlan might be truncated by the SQL engine if the query is over 4000 characters.|
|ShowPlan|Added logic to display the "Materializer Operator (External Select)".|
|ShowPlan|Add new showplan attribute BatchModeOnRowStoreUsed to easily identify queries that are using the " batch-mode scan on rowstores" feature. Anytime a query performs batch-mode scan on rowstores, a new attribute (BatchModeOnRowStoreUsed="true") gets added to StmtSimple element.|
|ShowPlan|Added Showplan Support to LocalCube RelOp for DW ROLLUP and CUBE.|
|ShowPlan|New LocalCube operator for the new ROLLUP and CUBE aggregation feature in Azure Synapse Analytics.|
|SMO| Extend SMO Support for Resumable Index Creation.|
|SMO| Added new event on SMO objects ("PropertyMissing") to help application authors to detect SMO performance issues sooner.|
|SMO| Exposed new DefaultBackupChecksum property on the Configuration object, which maps to the "backup checksum default" server configuration.|
|SMO| Exposed new ProductUpdateLevel property on the Server object, which maps to the servicing level for the version of SQL in use (for example, CU12, RTM).|
|SMO| Exposed new LastGoodCheckDbTime property on the Database object, which maps to the "lastgoodcheckdbtime" database property. If such property isn't available, a default value of `1/1/1900 12:00:00 AM` is returned.|
|SMO|Moved location for RegSrvr.xml file (Registered Server configuration file) to "%AppData%\Microsoft\SQL Server Management Studio" (unversioned, so it can be shared across versions of SSMS).|
|SMO|Added "Cloud Witness" as a new quorum type and as a new resource ty'e.|
|SMO|Added support for "Edge Constraints" in both SMO and SSMS.|
|SMO|Added cascade delete support to "Edge Constraints" in both SMO and SSMS.|
|SMO|Added support for data classification "read-write" permissions.|
|Vulnerability Assessment| Enabled Vulnerability Assessment tasks menu on Azure SQL DW.|
|Vulnerability Assessment|Change the set of vulnerability assessment rules that are run on Azure SQL Managed Instance, so that "Vulnerability Assessment" scan results can be consistent with the ones in Azure SQL DB.|
|Vulnerability Assessment| "Vulnerability Assessment" now supports Azure SQL DW.|
|Vulnerability Assessment|Added a new exporting feature to export the vulnerability assessment scan results to Excel.|
|XEvent Viewer|XEvent Viewer: enabled showplan window for more XEvents.|

#### Bug fixes in 18.0

| New Item | Details|
|----------|--------|
|Crashes and freezes|Fixed a source of common SSMS crashes related to GDI objects.|
|Crashes and freezes|Fixed a common source of hangs and poor performance when selecting "Script as Create/Update/Drop" (removed unnecessary fetches of SMO objects).|
|Crashes and freezes|Fixed an issue where system stops responding when connecting to an Azure SQL Database using MFA while ADAL traces are enabled.|
|Crashes and freezes|Fixed an issue where system stops responding (or perceived hang) in Live Query Statistics when invoked from Activity Monitor (the issue manifested when using SQL Server authentication with no "Persist Security Info" set).|
|Crashes and freezes|Fixed an issue where system stops responding when selecting "Reports" in Object Explorer, which could manifest on high latency connections or temporary non-accessibility of the resources.|
|Crashes and freezes|Fixed a crash in SSMS when trying to use Central Management Server and Azure SQL servers. For details, see [SMSS 17.5 application error and crash when using Central Management Server](https://feedback.azure.com/forums/908035/suggestions/33374884).|
|Crashes and freezes|Fixed an issue where system stops responding in Object Explorer by optimizing the way IsFullTextEnabled  property is retrieved.|
|Crashes and freezes|Fixed an issue where system stops responding in "Copy Database Wizard" by avoiding to build unnecessary queries to retrieve Database properties.|
|Crashes and freezes|Fixed an issue that was causing SSMS to stop responding/crash while editing T-SQL.|
|Crashes and freezes|Mitigated an issue where SSMS was becoming unresponsive when editing large T-SQL scripts.|
|Crashes and freezes|Fixed an issue that was causing SSMS to run out of memory when handling the large datasets returned by queries.|
|General SSMS|Fixed an issue there the "ApplicationIntent" wasn't passed along in connections in "Registered Servers".|
|General SSMS|Fixed an issue where the "New XEvent Session Wizard UI" form wasn't rendered properly on High DPI monitors.|
|General SSMS|Fixed an issue where trying to import a bacpac file.|
|General SSMS|Fixed an issue where trying to display the properties of a database (with FILEGROWTH > 2048 GB) was throwing an arithmetic overflow error.|
|General SSMS|Fixed an issue where the Perf Dashboard Report was reporting PAGELATCH and PAGEIOLATCH waits that couldn't found in subreports.|
|General SSMS|Another round of fixes to make SSMS more multi-monitor aware by having it open dialog in the correct monitor.|
|Analysis Services (AS)|Fixed an issue where the "Advanced Settings" to the AS XEvent UI was clipped.|
|Analysis Services (AS)|Fixed an issue where DAX parsing throws file not found exception.|
|Azure SQL Database|Fixed an issue where the database list wasn't populated correctly for Azure SQL Database query window when connected to a user database in Azure SQL Database instead of to master.|
|Azure SQL Database|Fixed an issue where it wasn't possible to add a "Temporal Table" to an Azure SQL Database.|
|Azure SQL Database|Enabled the Statistics properties sub menu option under menu Statistics in Azure, since it has been fully supported for quite some time now.|
|Azure SQL - General Support|Fixed issues in common Azure UI control that was preventing the user from displaying Azure subscriptions (if there were more than 50). Also, the sorting has been changed to be by name rather by Subscription ID. The user could run into this one when trying to restore a backup from URL, for example.|
|Azure SQL - General Support|Fixed an issue in common Azure UI control when enumerating subscriptions, which could yield an "Index was out of range. Must be non-negative and less than the size of the collection." error when the user had no subscriptions in some tenants. The user could run into this one when trying to restore a backup from URL, for example.|
|Azure SQL - General Support|Fixed issue where Service Level Objectives were hardcoded, thus making it harder for SSMS to support newer Azure SQL SLOs. Now, the user can sign in to Azure and allow SSMS to retrieve all the applicable SLO data (Edition and Max Size)|
|Azure SQL Managed Instance support|Improved/polished the support for managed instances: disabled unsupported options in UI and a fix to the View Audit Logs option to handle URL audit target.|
|Azure SQL Managed Instance support|"Generate and Publish scripts" wizard scripts unsupported CREATE DATABASE clauses.|
|Azure SQL Managed Instance support|Enable Live Query Statistics for managed instances.|
|Azure SQL Managed Instance support|Database properties->Files was incorrectly scripting ALTER DB ADD FILE.|
|Azure SQL Managed Instance support|Fixed regression with SQL Agent scheduler where ONIDLE scheduling was chosen even when some other scheduling type was chosen.|
|Azure SQL Managed Instance support|Adjusting MAXTRANSFERRATE, MAXBLOCKSIZE for doing backups on Azure Storage.|
|Azure SQL Managed Instance support|The issue where tail log backup is scripted before RESTORE operation (this isn't supported on CL).|
|Azure SQL Managed Instance support|Create database wizard not scripting correctly CREATE DATABASE statement.|
|Azure SQL Managed Instance support|Special handling of SSIS packages within SSMS when connected to managed instances.|
|Azure SQL Managed Instance support|Fixed an issue where an error was displayed while trying to use "Activity Monitor" when connected to managed instances.|
|Azure SQL Managed Instance support|Improved support for Azure AD Logins (in SSMS Explorer).|
|Azure SQL Managed Instance support|Improved scripting of SMO Filegroups objects.|
|Azure SQL Managed Instance support|Improved UI for credentials.|
|Azure SQL Managed Instance support|Added support for Logical Replication.|
|Azure SQL Managed Instance support|Fixed an issue, which was causing right-clicking on a database and choosing 'Import data-tier application' to fail.|
|Azure SQL Managed Instance support|Fixed an issue, which was causing right-clicking on a "TempDB" to show errors.|
|Azure SQL Managed Instance support|Fixed an issue where trying to scripting ALTER DB ADD FILE statement in SMO was causing the generated T-SQL script to be empty.|
|Azure SQL Managed Instance support|Improved display of managed instances server-specific properties (hardware generation, service tier, storage used and reserved).|
|Azure SQL Managed Instance support|Fixed an issue where scripting of a database ("Script as Create...") wasn't scripting extra filegroups and files. For details, see [https://feedback.azure.com/forums/908035/suggestions/37326799](https://feedback.azure.com/forums/908035/suggestions/37326799). |
|Backup/Restore/Attach/Detach DB|Fixed an issue where the user was unable to attach a database when physical filename of .mdf file does not match the original filename.|
|Backup/Restore/Attach/Detach DB|Fixed an issue where SSMS might not find a valid restore plan or might find one, which is suboptimal. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32897752](https://feedback.azure.com/forums/908035-sql-server/suggestions/32897752). |
|Backup/Restore/Attach/Detach DB|Fixed issue where the "Attach Database" wizard wasn't displaying secondary files that were renamed. Now, the file is displayed, and a comment about it is added (for example "Not Found"). For details, see [https://feedback.azure.com/forums/908035/suggestions/32897434](https://feedback.azure.com/forums/908035/suggestions/32897434). |
|Copy Database Wizard|Generate scripts/Transfer/Copy Database Wizard try to create a table with an in memory table doesn't force ansi_padding on.|
|Copy Database Wizard|Transfer Database task/Copy Database Wizard broken on SQL Server 2017 and SQL Server 2019.|
|Copy Database Wizard|Generate scripts/Transfer/Copy Database Wizard script table creation before creation of associated external data source.|
|Connection dialog|Enabled the removal of usernames from previous username list by pressing the DEL key. For details, see [Allow deletion of users from SSMS login window](https://feedback.azure.com/forums/908035/suggestions/32897632).|
|DAC Import Wizard|Fixed an issue DAC Import Wizard wasn't working when connected using Azure Active Directory (Azure AD).|
|Data Classification|Fixed an issue when saving classifications in the data classification pane while there are another data classification panes open on other databases.|
|Data-tier Application Wizard|Fixed an issue where the user wasn't able to import a Data-tier Application (.dacpac) due to limited access to the server (for example, no access to all the databases on the same server).|
|Data-tier Application Wizard|Fixed an issue, which was causing the import to be extremely slow when many databases happened to be hosted on the same Azure SQL server.|
|External Tables|Added support for Rejected_Row_Location in template, SMO, intellisense, and property grid.|
|Flat File Import Wizard|Fixed an issue where the "Import Flat File Wizard" wasn't handling double quotes correctly (escaping). For details, see [https://feedback.azure.com/forums/908035/suggestions/32897998](https://feedback.azure.com/forums/908035/suggestions/32897998). |
|Flat File Import Wizard|Fixed an issue where related to incorrect handling of floating-point types (on locales that use a different delimiter for floating points).|
|Flat File Import Wizard|Fixed an issue related to importing of bits when values are 0 or 1. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32898535](https://feedback.azure.com/forums/908035-sql-server/suggestions/32898535). |
|Flat File Import Wizard|Fixed an issue where the *float* data-type were entered as *nulls*.|
|Flat File Import Wizard|Fixed an issue where the import wizard wasn't able to process negative decimal values.|
|Flat File Import Wizard|Fixed an issue where the wizard wasn't able to import from single column CSV files.|
|Flat File Import Wizard|Fixed issue where Flat File Import does not allow changing destination table when table is already existing. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32896186](https://feedback.azure.com/forums/908035-sql-server/suggestions/32896186). |
|Help Viewer|Improved logic around honoring the online/offline modes (there may still be a few issues that need to be addressed).|
|Help Viewer|Fixed the "View Help" to honor the online/offline settings. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32897791](https://feedback.azure.com/forums/908035-sql-server/suggestions/32897791). |
|High Availability Disaster Recovery (HADR)<BR> Availability Groups (AG)|Fixed an issue where roles in "Fail Over Availability Groups" wizard was always displayed as "Resolving".|
|High Availability Disaster Recovery (HADR)<BR> Availability Groups (AG)|Fixed an issue where SSMS was showing truncated warnings in "AG Dashboard".|
|Integration Services (IS)|Fixed a SxS issue that deployment wizard fails to connect to sql server when SQL Server 2019 and SSMS 18.0 are installed on the same machine.|
|Integration Services (IS)|Fixed an issue that maintenance plan task can't be edited when designing the maintenance plan.|
|Integration Services (IS)|Fixed an issue that the deployment wizard gets stuck if the project under deployment is renamed.|
|Integration Services (IS)|Enabled environment setting in Azure-SSIS IR schedule feature.|
|Integration Services (IS)|Fixed an issue that SSIS Integration Runtime Creation Wizard stops responding when the customer account belongs to more than 1 tenant.|
|Job Activity Monitor|Fixed crash while using Job Activity Monitor (with filters).|
|Object Explorer|Fixed an issue where SSMS was throwing an "Object cannot be cast from DBNull to other types" exception when trying to expand "Management" node in OE (misconfigured DataCollector).|
|Object Explorer|Fixed an issue where OE wasn't escaping quotes before invoking the "Edit Top N..." causing the designer to get confused.|
|Object Explorer|Fixed an issue where the "Import Data-Tier application" wizard was failing to launch from the Azure Storage tree.|
|Object Explorer|Fixed an issue in "Database Mail Configuration" where the status of the TLS/SSL checkbox wasn't persisted. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32895541](https://feedback.azure.com/forums/908035-sql-server/suggestions/32895541). |
|Object Explorer|Fixed an issue where SSMS grayed out option to close existing connections when trying to restore database with is_auto_update_stats_async_on.|
|Object Explorer|Fixed an issue where right-clicking on nodes in OE the (for example "Tables" and wanting to perform an action such as filtering tables by going to Filter > Filter Settings, the filter settings form can appear on the other screen than where SSMS is currently active). For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/34284106](https://feedback.azure.com/forums/908035-sql-server/suggestions/34284106). |
|Object Explorer|Fixed a long outstanding issue where the DELETE key wasn't working in OE while trying to rename an object. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/33073510](https://feedback.azure.com/forums/908035-sql-server/suggestions/33073510), [https://feedback.azure.com/forums/908035/suggestions/32910247](https://feedback.azure.com/forums/908035/suggestions/32910247) and other duplicates.|
|Object Explorer|When displaying the properties of existing database files, the size appears under a column "Size (MB)" instead of "Initial Size (MB)" which is what is displayed when creating a new database. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32629024](https://feedback.azure.com/forums/908035-sql-server/suggestions/32629024). |
|Object Explorer|Disabled the "Design" context-menu item on "Graph Tables" since there is no support for those tables in the current version of SSMS.|
|Object Explorer|Fixed an issue where the "New Job Schedule" dialog wasn't rendering properly on High DPI monitors. For details, see [https://feedback.azure.com/admin/v3/suggestions/35541262](https://feedback.azure.com/admin/v3/suggestions/35541262). |
|Object Explorer|Fixed/improved the way an issue where a database size ("Size (MB)") is displayed in Object Explorer details: only 2 decimal digits and formatted using the thousands separator. For details, see [https://feedback.azure.com/forums/908035/suggestions/34379308](https://feedback.azure.com/forums/908035/suggestions/34379308).|
|Object Explorer|Fixed an issue that was causing the creation of a "Spatial Index" to fail with an error like "To accomplish this action, set property PartitionScheme".|
|Object Explorer|Minor performance improvements in Object Explorer to avoid issuing extra queries, when possible.|
|Object Explorer|Extended logic to request confirmation when renaming a database to all the schema objects (the setting can be configured).|
|Object Explorer|Added proper escaping in Object Explorer filtering. For details, see [https://feedback.azure.com/forums/908035/suggestions/36678803](https://feedback.azure.com/forums/908035/suggestions/36678803). |
|Object Explorer|Fixed/improved the view in Object Explorer Details to show numbers with proper separators. For details, see [https://feedback.azure.com/forums/908035/suggestions/32900944](https://feedback.azure.com/forums/908035/suggestions/32900944). |
|Object Explorer|Fixed context menu on "Tables" node when connected to SQL Express, where the "New" fly-out was missing, Graph tables were incorrectly listed, and System-Versioned table was missing. For details, see [https://feedback.azure.com/forums/908035/suggestions/37245529](https://feedback.azure.com/forums/908035/suggestions/37245529). |
|Object Scripting|Overall perf improvements - Generate Scripts of WideWorldImporters takes half the time compared to SSMS 17.7.|
|Object Scripting|When scripting objects, DB Scoped configuration, which has default values are omitted.|
|Object Scripting|Don't generate dynamic T-SQL when scripting. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32898391](https://feedback.azure.com/forums/908035-sql-server/suggestions/32898391). |
|Object Scripting|Omit the graph syntax "as edge" and "as node" when scripting a table on SQL Server 2016 and earlier.|
|Object Scripting|Fixed an issue where scripting of database objects was failing when connecting to an Azure SQL Database using Azure AD with MFA.|
|Object Scripting|Fixed an issue where trying to script a spatial index with GEOMETRY_AUTO_GRID/GEOGRAPHY_AUTO_GRID on an Azure SQL Database was throwing an error.|
|Object Scripting|Fixed an issue, which was causing the database scripting (of an Azure SQL Database) to always target an on-premises SQL, even if the "Object Explorer" scripting settings were set to match the source.|
|Object Scripting|Fixed an issue where trying to script a table in a SQL DW database involving clustered and nonclustered indexes was generating incorrect T-SQL statements.|
|Object Scripting|Fixed an issue where trying to script a table in a SQL DW database with both "Clustered Columnstore Indexes" and "Clustered Indexes" was generating incorrect T-SQL (duplicate statements).|
|Object Scripting|Fixed Partitioned table scripting with no range values (SQL DW databases).|
|Object Scripting|Fixed an issue where the user would be unable to script an audit/audit specification SERVER_PERMISSION_CHANGE_GROUP.|
|Object Scripting|Fix and issue where the user is unable to script statistics from SQL DW. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32897296](https://feedback.azure.com/forums/908035-sql-server/suggestions/32897296). |
|Object Scripting|Fixed an issue where the "Generate script wizard" shows incorrect table having scripting error when "Continue scripting on Error" is set to false.|
|Object Scripting|Improved script generation on SQL Server 2019.|
|Profiler|Added "Aggregate Table Rewrite Query" event to Profiler events.|
|Query Data Store|Fixed an issue where a "DocumentFrame (SQLEditors)" exception could be thrown.|
|Query Data Store|Fixed an issue when trying to set a custom time interval in the build-in Query Store reports the user wasn't able to select AM or PM on the start/end interval.|
|Results Grid|Fixed an issue that was causing the in High Contrast mode (selected line numbers not visible).|
|Results Grid|Fixed an issue, which resulted in an "Index out of range" exception when clicking on the grid.|
|Results Grid|Fixed an issue where the grid result background color was being ignored. For details, see [https://feedback.azure.com/forums/908035/suggestions/32895916](https://feedback.azure.com/forums/908035/suggestions/32895916). |
|ShowPlan|New memory grant operator properties display incorrectly when there is more than one thread.|
|ShowPlan|Add the following 4 attributes in RunTimeCountersPerThread of actual execution xml plan: HpcRowCount (Number of rows processed by a *hpc* device), HpcKernelElapsedUs (elapsed time wait for kernel execution in use), HpcHostToDeviceBytes (bytes transferred from host to device), and HpcDeviceToHostBytes (bytes transferred from device to host).|
|ShowPlan|Fixed an issue where the similar plan nodes are highlighted in the wrong position.|
|SMO|Fixed an issue where SMO/ServerConnection didn't SqlCredential-based connections correctly. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/33698941](https://feedback.azure.com/forums/908035-sql-server/suggestions/33698941). |
|SMO|Fixed an issue where an application written using SMO would encounter an error if it tried to enumerate databases from the same server on multiple threads even though it uses separate SqlConnection instances on each thread.|
|SMO|Fixed performance regression in Transfer from External Tables.|
|SMO|Fixed issue in ServerConnection thread-safety, which was causing SMO to leak SqlConnection instances when targeting Azure.|
|SMO|Fixed an issue, which was causing a StringBuilder.FormatError when trying to restore a database, which had curly braces in its name.|
|SMO|Fixed an issue where Azure databases in SMO were defaulting to Case-Insensitive collation for all string comparisons instead of using the specified collation for the database.|
|SSMS Editor|Fixed an issue where "SQL System Table" where restoring the default colors was chancing the color to lime green, rather than the default green, making it hard to read on a white background. For details, see [Restoring wrong default color for SQL System Table](https://feedback.azure.com/forums/908035-sql-server/suggestions/32896906). The issue still persists on non-English versions of SSMS.|
|SSMS Editor|Fixed issue where intellisense wasn't working when connected to Azure SQLDW using Azure Active Directory (Azure AD) authentication.|
|SSMS Editor|Fixed intellisense in Azure when user lacks access to **master** database.|
|SSMS Editor|Fixed code snippets to create "temporal tables", which were broken when the collation of the target database was case-sensitive.|
|SSMS Editor|New TRANSLATE function now recognized by intellisense. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32898430](https://feedback.azure.com/forums/908035-sql-server/suggestions/32898430). |
|SSMS Editor|Improved intellisense on FORMAT built-in function. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32898676](https://feedback.azure.com/forums/908035-sql-server/suggestions/32898676). |
|SSMS Editor|LAG and LEAD are now recognized as built-in functions. For details, see [https://feedback.azure.com/forums/908035-sql-server/suggestions/32898757](https://feedback.azure.com/forums/908035-sql-server/suggestions/32898757). |
|SSMS Editor|Fixed an issue where intellisense was giving a warning when using "ALTER TABLE...ADD CONSTRAINT...WITH(ONLINE=ON)".|
|SSMS Editor|Fixed an issue where several system views and table values functions weren't properly colorized.|
|SSMS Editor|Fixed an issue where clicking on editor tabs could cause the tab to be closed instead of getting the focus. For details, see [https://feedback.azure.com/forums/908035/suggestions/37291114](https://feedback.azure.com/forums/908035/suggestions/37291114). |
|SSMS Options|Fixed an issue where **Tools** > **Options** > **SQL Server Object Explorer** > **Commands** page wasn't resizing properly.|
|SSMS Options|SSMS now by default disables automatic download of DTD in XMLA editor -- XMLA script editor (which uses the xml language service) by default now prevents automatically downloading the DTD for potentially malicious xmla files. This is controlled by turning off the "Automatically download DTDs and Schemas" setting in **Tools** > **Options** > **Environment** > **Text Editor** > **XML** > **Miscellaneous**.|
|SSMS Options|Restored **CTRL+D** to be the shortcut as it used to be in older version of SSMS. For details, see [https://feedback.azure.com/forums/908035/suggestions/35544754](https://feedback.azure.com/forums/908035/suggestions/35544754). |
|Table Designer|Fixed a crash in "Edit 200 rows".|
|Table Designer|Fixed an issue where the designer was allowing to add a table when connected to an Azure SQL Database.|
|Vulnerability Assessment|Fixed an issue where the scan results aren't being loaded properly.|
|XEvent|Added two columns "action_name" and "class_type_desc" that show action ID and class type fields as readable strings.|
|XEvent|Removed the event XEvent Viewer cap of 1,000,000 events.|
|XEvent Profiler|Fixed an issue where XEvent Profiler failed to launch when connected to a 96-core SQL Server.|
|XEvent Viewer|Fixed an issue where XEvent Viewer was crashing when trying to group the events using the "Extended Event Toolbar Options".|

#### Deprecated and removed features in 18.0

Here are the deprecated and removed Features from SSMS version 18.0.

- T-SQL Debugger
- Database Diagrams
- The following tools are no longer installed with SSMS:
  - OSQL.EXE
  - DReplay.exe
  - SQLdiag.exe
  - SSBDiagnose.exe
  - bcp.exe
  - sqlcmd.exe
- Configuration Manager tools:
  - Both SQL Server  Configuration Manager and Reporting Server Configuration Manager aren't part of SSMS setup anymore.
- DMF Standard Policies
  - The policies aren't installed with SSMS anymore. They are moved to Git. Users are able to contribute and download/install them, if they want to.
- SSMS command-line option -P removed
  - Due to security concerns, the option to specify clear-text passwords on the command line was removed.
- Generate Scripts > Publish to Web Service removed
  - This deprecated feature was removed from the SSMS UI.
- Removed node "Maintenance > Legacy" in Object Explorer.
  - The old "Database Maintenance Plan" and "SQL Mail" nodes won't be accessible anymore. The modern "Database Mail" and "Maintenance Plans" nodes continue to work as usual.

#### Known issues (18.0)

- You might encounter an issue installing version 18.0, where you cannot run SQL Server Management Studio. If you encounter this issue, follow the steps from the [SSMS2018 - Installed, but not run](https://feedback.azure.com/forums/908035-sql-server/suggestions/37502512-ssms2018-installed-but-will-not-run) article.

- There is a limitation on the size of the data you see from SSMS results shown in grid, text, or file

- There are redraw issues when switching between multiple query windows. See [SQL Server user feedback](https://feedback.azure.com/forums/908035/suggestions/37474042) for more details. A workaround for this issue is to disable hardware acceleration under Tools > Options.

### 17.9.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 17.9.1](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x409)

- Release number: 17.9.1
- Build number: 14.0.17289.0
- Release date: November 21, 2018

#### Bug fixes in 17.9.1

- Fixed an issue where users may experience their connection being closed and reopened with each query invocation when using "Azure Active Directory - Universal with MFA support" authentication with the SQL query editor. Side effects of the connection closing included global temporary tables being dropped unexpectedly, and sometimes a new SPID given to the connection.
- Fixed a long outstanding issue where restore plan would fail to find a restore plan, or would generate an inefficient restore plan under certain conditions.
- Fixed an issue in the "Import Data-tier Application" wizard, which could result in an error when connected to an Azure SQL Database.

> [!NOTE]
> Non-English localized releases of SSMS 17.x require the [KB 2862966 security update package](https://support.microsoft.com/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x804)| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x404)| [English (United States)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x409)| [French](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x40c)| [German](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x407)| [Italian](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x410)| [Japanese](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x411)| [Korean](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x412)| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x416)| [Russian](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x419)| [Spanish](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x40a)

#### Uninstall and reinstall SSMS 17.x

If your SSMS installation is having problems, and a standard uninstall and reinstall doesn't resolve them, you can first try [repairing](https://support.microsoft.com/help/4028054/windows-10-repair-or-remove-programs) the Visual Studio 2015 IsoShell. If repairing the Visual Studio 2015 IsoShell doesn't resolve the problem, the following steps have been found to fix many random issues:

1. Uninstall SSMS the same way you uninstall any application (using *Apps & features*, *Programs, and features*, depending on your version of Windows).

2. Uninstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:

    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```

    ```vs_isoshell.exe /Uninstall /Force /PromptRestart```

3. Uninstall Microsoft Visual C++ 2015 Redistributable the same way you uninstall any application. Uninstall both x86 and x64 if they're on your computer.

4. Reinstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:  

    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```  

    ```vs_isoshell.exe /PromptRestart```

5. Reinstall SSMS.

6. Upgrade to the [latest version of the Visual C++ 2015 Redistributable](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads) if you're not currently up to date.

### 16.5.3

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 16.5.3](https://go.microsoft.com/fwlink/?LinkID=840946)

- Release number: 16.5.3  
- Build number: 13.0.16106.4  
- Release date: January 30, 2017

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x804)| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x404)| [English (United States)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x409)| [French](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40c)| [German](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x407)| [Italian](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x410)| [Japanese](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x411)| [Korean](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x412)| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x416)| [Russian](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x419)| [Spanish](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40a)

#### Bug fixes in 16.5.3

- Fixed an issue introduced in SSMS 16.5.2, which was causing the expansion of the 'Table' node when the table had more than one sparse column.

- Users can deploy SSIS packages containing OData Connection Manager, which connect to a Microsoft Dynamics AX/CRM Online resource to SSIS catalog. For more information, For details, see [OData Connection Manager](../integration-services/connection-manager/odata-connection-manager.md).

- Configuring Always Encrypted on an existing table fails with errors on unrelated objects. [Connect ID 3103181](https://connect.microsoft.com/SQLServer/feedback/details/3103181/setting-up-always-encrypted-on-an-existing-table-fails-with-errors-on-unrelated-objects)

- Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect ID 3109591](https://connect.microsoft.com/SQLServer/feedback/details/3109591/sql-server-2016-always-encrypted-against-existing-database-with-multiple-schemas-doesnt-work)

- The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect ID 3111925](https://connect.microsoft.com/SQLServer/feedback/details/3111925/sql-server-2016-always-encrypted-encrypted-column-wizard-failed-task-failed-due-to-following-error-cannot-save-package-to-file-the-model-has-build-blocking-errors)

- When encrypting using Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.

- *Open recent* menu doesn't show recently saved files. [Connect ID 3113288](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)

- SSMS is slow when right-clicking an index for a table (over a remote (Internet) connection). [Connect ID 3114074](https://connect.microsoft.com/SQLServer/feedback/details/3114074/ssms-slow-when-right-clicking-an-index-for-a-table-over-a-remote-internet-connection)

- Fixed an issue with the SQL Designer scrollbar. [Connect ID 3114856](https://connect.microsoft.com/SQLServer/feedback/details/3114856/bug-in-scrollbar-on-sql-desginer-in-ssms-2016)

- Context menu for tables momentarily stops responding

- SSMS occasionally throws exceptions in Activity Monitor and crashes. [Connect ID 697527](https://connect.microsoft.com/SQLServer/feedback/details/697527/)

- SSMS 2016 crashes with error "The process was terminated due to an internal error in the .NET Runtime at IP 71AF8579 (71AE0000) with exit code 80131506"

## Additional Downloads

For a list of all SQL Server Management Studio downloads, search the [Microsoft Download Center](https://www.microsoft.com/download/search.aspx?q=sql%20server%20management%20studio&p=0&r=10&t=&s=Relevancy~Descending).  
  
For the latest release of SQL Server Management Studio, For details, see [Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md).
