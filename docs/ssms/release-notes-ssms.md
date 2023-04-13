---
title: Release notes for (SSMS)
description: Release notes for SQL Server Management Studio (SSMS).
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 03/13/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---
# Release notes for SQL Server Management Studio (SSMS)

[!INCLUDE [sql-asdb-asdbmi-asa](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article details updates, improvements, and bug fixes for the current and previous versions of SSMS.

[!INCLUDE[ssms-connect-aazure-ad](../includes/ssms-connect-azure-ad.md)]

## Current SSMS release

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server Management Studio (SSMS) 19.0.2](https://aka.ms/ssmsfullsetup)**

SSMS 19.0.2 is the latest general availability (GA) release of SSMS. If you need a previous version of SSMS, see [previous SSMS releases](release-notes-ssms.md#previous-ssms-releases).

### 19.0.2

- Release number: 19.0.2
- Build number: 19.0.20209.0
- Release date: March 13, 2023

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2226343&clcid=0x40a) |

#### What's new in 19.0.2

| New Item | Details |
| ---------- | ------- |
| Azure Data Studio installation integration | The installation of SSMS installs Azure Data Studio 1.41.2. |

#### Bug fixes in 19.0.2

| New Item | Details |
| -------- | ------- |
| Connection | Addressed error "Your app has been throttled by AAD due to too many requests" when using Azure Active Directory authentication.  See [SSMS 19 AAD Integrated: Your app has been throttled by AAD due to too many requests](https://feedback.azure.com/d365community/idea/b4b0d281-c2a0-ed11-a81b-6045bd8615b0). | 
| Connection | Resolved SSMS crash behavior when logging into Azure and changing the user. |
| Fulltext | Fixed an issue that caused a table's fulltext index to be rebuilt when moving the table to a different filegroup. |
| General SSMS | Added digital signature to dll files. |
| Link feature for Azure Managed Instance | Fixed error "Exception has been thrown by the target of an invocation" which occurred when a subscription with no resource groups was selected attempting to create a link using Managed Instance link wizard. | 
| Managed Instance | Restored ability to view File and FileGroups pages on Database Properties window. See [bugs in 19.0.1](https://feedback.azure.com/d365community/idea/b3d026e6-b8b2-ed11-a81b-000d3ae6a6aa).|
| Profiler | Fixed issue that generated "Errors in the OLE DB provider. Unable to obtain authentication token using the credentials provided" error trying to run SQL Profiler with a Power BI workspace. |
| Replication | Addressed error "Property Password cannot be changed or read after a connection string has been set" which occurred when trying to configure a replication subscriber. See [SSMS 19 - Issue while connecting to subsciber during replication configuration](https://feedback.azure.com/d365community/idea/4e9073b7-1dad-ed11-a81b-6045bd79fc6e).|
| Replication | Fixed error "SQL Server encountered one or more errors while retrieving information about publication" which occurred when trying to view the properties for a publication. See [SSMS 19.0.1 cannot open Properties dialog for local publications](https://feedback.azure.com/d365community/idea/3dba641e-a9a3-ed11-a81b-6045bd79fc6e).|
| Reports | Corrected server startup time on Server Dashboard report. |
| SQL Agent | Addressed inability to start the SQL Agent from SSMS. |

#### Known issues (19.0.2)

| New Item | Details | Workaround |
| -------- | ------- | ---------- |
| Database Designer | Selecting the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | Import setting from SSMS 17 option not available. | Settings can be imported from SSMS 18. |
| Profiler | The Profiler menu is not localized. | No current alternative. |
| Query Editor | When SSMS opens after double-clicking on a .sql file, the Object Explorer window is displayed as a separate window. |
| SQL Managed Instance | Viewing database properties for a SQL MI database may return the error "Subquery returned more than one value. This isn't permitted when the subquery follows =, !=, <, <=, >, >= or when the subquery is used as an expression. (.NET SqlClient Data Provider)". | There's a known problem due to incorrect data in `msdb`. To resolve, remove back up history. For example, `EXEC`msdb`..sp_delete_backuphistory @oldest_date = '<current date>'`. |
| Storage Account | Trying to delete a container from a storage account fails with a (400) Bad Request error. | Use the Azure portal for container deletion. |
| Server Audit | Error "Item has already been added. Key in dictionary: 'MNDO'  Key being added: 'MNDO'" when viewing Logs for an Audit. | No current workaround. |
| Stretch DB | Removed Stretch DB Wizard. | Use T-SQL to configure Stretch DB or use SSMS 18.9.1 or earlier to use the Stretch DB Wizard. |

You can reference [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server) for other known issues and to provide feedback to the product team.

## Previous SSMS releases

Download previous SSMS versions by selecting the download link in the related section.

| SSMS version | Build number | Release date |
| ------------ | ------------ | ------------ |
| [19.0.1](#1901) | 19.0.20200.0 | February 2, 2023 |
| [19.0](#190) | 19.0.20196.0 | January 26, 2023 |
| [18.12.1](#18121) | 15.0.18420.0 | June 21, 2022 |
| [17.9.1](#1791) | 14.0.17289.0 | November 21, 2018 |
| [16.5.3](#1653) | 13.0.16106.4 | January 30, 2017 |

### 19.0.1

- Release number: 19.0.1
- Build number: 19.0.20200.0
- Release date: February 2, 2023

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2223741&clcid=0x40a) |

#### What's new in 19.0.1

| New Item | Details |
| ---------- | ------- |
| Azure Data Studio installation integration | The installation of SSMS installs Azure Data Studio 1.41.1. |

#### Bug fixes in 19.0.1

| New Item | Details |
| -------- | ------- |
| Maintenance Plan | Fixed issue that generated "Failed to retrieve data for this request" error when using backup to NUL in the Back Up Database Task. |
| Object Explorer | Fixed a regression which caused Object Explorer to not show database objects for Azure SQL DB Basic SLO. |

### 19.0

- Release number: 19.0
- Build number: 19.0.20196.0
- Release date: January 26, 2023

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2223041&clcid=0x40a) |

#### What's new in 19.0

| New Item | Details |
| ---------- | ------- |
| Support for SQL Server 2022 | SSMS 19.0 is the first release that is fully aware of SQL Server 2022 (compatibility level 160). |
| Azure Data Studio installation integration | The installation of SSMS installs Azure Data Studio 1.41. |
| Accessibility | Improved keyboard navigation and screen reader text in the data classification window. |
| Always Encrypted | Added the ability to explicitly configure an attestation protocol in the "Connect To Server" dialog when using Always Encrypted with secure enclaves (column encryption). |
| Azure Active Directory Authentication | Connections to relational SQL instances now support Azure Active Directory Service Principal, Azure Active Directory Managed Identity and Azure Active Directory Default authentication methods. “Default” uses a series of possible sources for authentication, including environment variables, Azure Managed Identity, the user’s shared token cache, Visual Studio, Azure CLI, and Azure PowerShell. For more information, visit: [Options (Azure Services)](menu-help/options-azure-services.md) |
| Auditing | Added support for SQL 2022 actions. |
| Client Driver | Changed to Microsoft.Data.SqlClient. |
| Contained Always On Availability Group | Added support for Contained Always On Availability Groups. |
| Data Classification | Improvements to Data Classification user interface. |
| Database Tuning Advisor | Added support for increased nonclustered index size (up to 1700 bytes), originally made available in SQL Server 2016, to allow for expanded index recommendations. |
| General SSMS | Added a new page, **Tools > Options > Output Window**, to allow users to control what window channels appear in the Output window. See [Options [Output Window - General)](menu-help/options-output-window-page.md) for more information. |
| German Azure | Removed references to Azure Germany from SSMS. |
| Ledger | Added support for import/export of a bacpac or dacpac created from a database with LEDGER = ON. |
| Ledger | Added support for Ledger feature Database ledger. For more information, visit [What is the database ledger?](../relational-databases/security/ledger/ledger-database-ledger.md). |
| Link feature for Azure SQL Managed Instance | Introduced rollback support if selected tasks fail in the MI Link wizard. |
| Maintenance Plan | The Maintenance Plan node is now available in Object Explorer. |
| Object Explorer | Dropped Columns folder now exists under the Columns folder for Ledger tables, which have been altered to remove one or more columns. |
| PowerShell | Removed “Generate PowerShell Script from In-Memory OLTP Migration. |
| Query Execution or Results | Improved checks for open connections. |
| Query Plan | Added ellipses button to Residual property for Merge Join operator. See [SSMS: Add ellipsis button to Residual property for Merge Join operator](https://feedback.azure.com/d365community/idea/025ef426-4a88-ed11-a81b-000d3adb7ffd). |
| Query Results | Added option in **Tools > Options > Query Execution** to not display the “querying transaction count” window when closing a query window. See [SSMS - Querying transaction count - Async](https://feedback.azure.com/d365community/idea/aaaad978-65b9-ec11-a81c-6045bd80aaa9). |
| Query Tuning Assistant | Updated user interface for improved accessibility. |
| Security | Added support for permissions introduced in SQL Server 2022 and SQL Server 2019. |
| Scripting | Compatibility level defaults to 160 when scripting. |
| Showplan | The showplan XML schema has been updated. |
| Showplan | Added support for Hyperscale Optimized Query Processing. |
| Showplan | Added DOPFeedbackAdjusted query plan attribute. |
| SNAC | Removed dependency on SQL Server Native Client (SNAC/SQLNCLI) from SSMS. Visit [Support Policies - SQL Server Native Client](../relational-databases/native-client/applications/support-policies-for-sql-server-native-client.md) for more information. |
| SqlParser | Added support for the TRIM function. |

#### Bug fixes in 19.0

| New Item | Details |
| -------- | ------- |
| Accessibility | Fixed accessibility issue when navigating in the data classification window. |
| Activity Monitor | Fixed issue where SSMS terminated unexpectedly when viewing a query plan from Activity Monitor. |
| Analysis Services | Connection to Analysis Services is now available. |
| Always Encrypted | Addressed issue where users couldn't sign in to the Column Master Key dialog after signing out. |
| Availability Group Dashboard | Fixed the issue when connecting to the Availability Group Dashboard for an AG on SQL Server 2016, which resulted in "unknown property ClusterType" error. |
| Backup | Added ability to create a NUL backup device against SQL Server 2017. |
| Central Management Servers | Provided ability to view SQL ERRORLOGs from Central Management Servers before SQL 2022. |
| Column Master Key | Increased width of Column Master Key name in the rotation dialog, so the full name is visible. |
| Connection | Fixed an issue with authentication that doesn't use Azure Resource Manager and Microsoft Information Protection. |
| Connection | Addressed issue with logging into Azure with a non-SQL authenticated on a machine not joined to a domain. |
| Copy Database Wizard | Fixed the log provider type error, which occurred when copying a database using The SQL Management Object method. |
| Database Restore | Resolved error generated when restoring a backup to an existing database. See [Unexpected behavior by SSMS](https://feedback.azure.com/d365community/idea/95b549d0-6f70-ed11-a81b-000d3adb7ffd). |
| Database Tuning Advisor | Added ability to ignore unexpected elements in showplan XML when generating recommendations. |
| Database Tuning Advisor | Resolved failure to populate the tuning log table issue. |
| Database Tuning Advisor | Implemented security fixes, including replacing ZeroMemory with SecureZeroMemory. |
| Import/Export Data-Tier Application | Restored ability to deploy an extracted dacpac to a database via the Deploy Data-Tier Application option. |
| Link feature for Azure SQL Managed Instance | Fixed problem with leaking connections in MI Link. |
| Link feature for Azure SQL Managed Instance | Updated size of subscription dropdown in the login to Azure pane to properly display subscription names. |
| Link feature for Azure SQL Managed Instance | Updated display to show Azure sign-in options correctly. |
| Microsoft Information Protection | Improved icon display when viewing Microsoft Information Protection in older versions of SQL Server. |
| Object Explorer | SSMS no longer crashes using the Create View UX to create a view with invalid syntax. |
| Object Explorer | No longer display Ledger objects if not supported by the platform. |
| Partitioning | Added support for Azure SQL Managed Instance partitioned tables in the Manage Partition Window UX. |
| Power BI Datamart | Added ability to connect to a Power BI Datamart. |
| Query Editor | Fixed issue with audible notification occurring when closing a query window. See [SSMS 18.11.1 Beeps When I Close a Query Window](/answers/questions/775502/ssms-18111-beeps-when-i-close-a-query-window.html). |
| Registered Servers | Fixed connection issue for a multi-server query with multiple registered servers in the same folder. |
| Replication | Fixed error "Merge publications can't be created from this database until the compatibility level is set to 70 or higher." when using the publication wizard to create a new merge publication. |
| Security | Added missing database permissions. |
| Scripting | Added ability to Script as Insert for DW. |
| SQL Agent | The Queued status is now shown for queued jobs in SQL Agent. |
| SMO/Scripting | Addressed CREATE TABLE scripting failure after adding datetime masking for a column. |
| SqlParser | Fixed incorrect syntax for DATE_BUCKET function. |
| SqlParser | Added missing options for CREATE USER and CREATE LOGIN. |
| SSIS | The "Schedule..." menu item is now visible in the Azure SSIS Catalog. |
| Synapse | Created consistent naming for Synapse offerings in SSMS. |
| XEvents | Fixed an issue where reading target data for event sessions whose name overlaps with another session name caused data from the incorrect event session to appear in the viewer. |

#### Known issues (19.0)

| New Item | Details | Workaround |
| -------- | ------- | ---------- |
| Database Designer | Selecting the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| Profiler | The Profiler menu is not localized. | No current alternative. |
| Query Editor | When SSMS opens after double-clicking on a .sql file, the Object Explorer window is displayed as a separate window. |
| SQL Managed Instance | Viewing database properties for a SQL MI database may return the error "Subquery returned more than one value. This isn't permitted when the subquery follows =, !=, <, <=, >, >= or when the subquery is used as an expression. (.NET SqlClient Data Provider)". | There's a known problem due to incorrect data in `msdb`. To resolve, remove back up history. For example, `EXEC`msdb`..sp_delete_backuphistory @oldest_date = '<current date>'`. |
| Storage Account | Trying to delete a container from a storage account fails with a (400) Bad Request error. | Use the Azure portal for container deletion. |
| Server Audit | Error "Item has already been added. Key in dictionary: 'MNDO'  Key being added: 'MNDO'" when viewing Logs for an Audit. | No current workaround. |
| Stretch DB | Removed Stretch DB Wizard. | Use T-SQL to configure Stretch DB or use SSMS 18.9.1 or earlier to use the Stretch DB Wizard. |

### 18.12.1

- Release number: 18.12.1
- Build number: 15.0.18424.0
- Release date: June 21, 2022

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x40a)

#### What's new in 18.12.1

| New Item | Details |
| -------- | ------- |
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.37. |

#### Bug fixes in 18.12.1

| New Item | Details |
| -------- | ------- |
| Always Encrypted | Fixed issue with Column Master Key creation generating an exception when using Azure Key Vault as the key store. |
| Data Classification | Fixed issue with "Couldn't load file or assembly 'Microsoft.Information.Protection', Version=1.10.98.0" after upgrading to SSMS 18.10 or higher. See [Latest SSMS 18.11.1 breaks the Data Classification. Get missing assembly error after updating](https://feedback.azure.com/d365community/idea/af89d5d7-45a4-ec11-a81c-0022484ee92d). |
| SSMS General | Resolved error related to dacpac deployment using the Deploy Data-tier application option in Azure SQL DB with MFA. |

### 17.9.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 17.9.1](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x409)

- Release number: 17.9.1
- Build number: 14.0.17289.0
- Release date: November 21, 2018

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x804)| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x404)| [English (United States)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x409)| [French](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x40c)| [German](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x407)| [Italian](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x410)| [Japanese](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x411)| [Korean](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x412)| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x416)| [Russian](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x419)| [Spanish](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x40a)

#### What's new in 17.9.1

SQL Server utility is no longer available in versions 17.X and newer.

#### Bug fixes in 17.9.1

- Fixed an issue where users may experience their connection being closed and reopened with each query invocation when using "Azure Active Directory - Universal with MFA support" authentication with the SQL query editor. Side effects of the connection closing included global temporary tables being dropped unexpectedly and sometimes a new SPID given to the connection.
- Fixed a long outstanding issue where a restore plan would fail to find a restore plan or generate an inefficient one under certain conditions.
- Fixed an issue in the "Import Data-tier Application" wizard, which could result in an error when connected to an Azure SQL Database.

> [!NOTE]  
> Non-English localized releases of SSMS 17.x require the [KB 2862966 security update package](https://support.microsoft.com/kb/2862966) if installed on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.

#### Uninstall and reinstall SSMS 17.x

If your SSMS installation is having problems, and a standard uninstall and reinstall doesn't resolve them, you can first try [repairing](https://support.microsoft.com/help/4028054/windows-10-repair-or-remove-programs) the Visual Studio 2015 IsoShell. If repairing the Visual Studio 2015 IsoShell doesn't resolve the problem, the following steps have been found to fix many random issues:

1. Uninstall SSMS the same way you uninstall any application (using *Apps & features*, *Programs, and features*, depending on your version of Windows).

1. Uninstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:

    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```
    ```vs_isoshell.exe /Uninstall /Force /PromptRestart```

1. Uninstall Microsoft Visual C++ 2015 Redistributable the same way you uninstall any application. Uninstall both x86 and x64 if they're on your computer.

1. Reinstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:

    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```
    ```vs_isoshell.exe /PromptRestart```

1. Reinstall SSMS.

1. Upgrade to the [latest version of the Visual C++ 2015 Redistributable](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads) if you're not currently up to date.

### 16.5.3

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 16.5.3](https://go.microsoft.com/fwlink/?LinkID=840946)

- Release number: 16.5.3
- Build number: 13.0.16106.4
- Release date: January 30, 2017

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x804)| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x404)| [English (United States)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x409)| [French](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40c)| [German](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x407)| [Italian](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x410)| [Japanese](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x411)| [Korean](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x412)| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x416)| [Russian](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x419)| [Spanish](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40a)

#### Bug fixes in 16.5.3

- Fixed an issue introduced in SSMS 16.5.2, which was causing the expansion of the 'Table' node when the table had more than one sparse column.

- Users can deploy SSIS packages containing OData Connection Manager, which connects to a Microsoft Dynamics AX/CRM Online resource to the SSIS catalog. For more information, For details, see [OData Connection Manager](../integration-services/connection-manager/odata-connection-manager.md).

- Configuring Always Encrypted on an existing table fails with errors on unrelated objects. [Connect ID 3103181](https://connect.microsoft.com/SQLServer/feedback/details/3103181/setting-up-always-encrypted-on-an-existing-table-fails-with-errors-on-unrelated-objects)

- Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect ID 3109591](https://connect.microsoft.com/SQLServer/feedback/details/3109591/sql-server-2016-always-encrypted-against-existing-database-with-multiple-schemas-doesnt-work)

- The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect ID 3111925](https://connect.microsoft.com/SQLServer/feedback/details/3111925/sql-server-2016-always-encrypted-encrypted-column-wizard-failed-task-failed-due-to-following-error-cannot-save-package-to-file-the-model-has-build-blocking-errors)

- When encrypting with Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.

- *Open recent* menu doesn't show recently saved files. [Connect ID 3113288](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)

- SSMS is slow when right-clicking an index for a table (over a remote (Internet) connection). [Connect ID 3114074](https://connect.microsoft.com/SQLServer/feedback/details/3114074/ssms-slow-when-right-clicking-an-index-for-a-table-over-a-remote-internet-connection)

- Fixed an issue with the SQL Designer scrollbar. [Connect ID 3114856](https://connect.microsoft.com/SQLServer/feedback/details/3114856/bug-in-scrollbar-on-sql-desginer-in-ssms-2016)

- Context menu for tables momentarily stops responding

- SSMS occasionally throws exceptions in Activity Monitor and crashes. [Connect ID 697527](https://connect.microsoft.com/SQLServer/feedback/details/697527/)

- SSMS 2016 crashes with the error "The process was terminated due to an internal error in the .NET Runtime at IP 71AF8579 (71AE0000) with exit code 80131506"

## Additional Downloads

For a list of all SQL Server Management Studio downloads, search the [Microsoft Download Center](https://www.microsoft.com/download/search.aspx?q=sql%20server%20management%20studio&p=0&r=10&t=&s=Relevancy~Descending).

For the latest release of SQL Server Management Studio, For details, see [Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md).

## Next steps

- [Download SSMS](download-sql-server-management-studio-ssms.md)
- [Download Azure Data Studio](../azure-data-studio/download-azure-data-studio.md)
- [Azure Data Studio release notes](../azure-data-studio/release-notes-azure-data-studio.md)
