---
title: Release notes for (SSMS)
description: Release notes for SQL Server Management Studio (SSMS).
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 11/29/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---
# Release notes for SQL Server Management Studio (SSMS)

[!INCLUDE [sql-asdb-asdbmi-asa](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article details updates, improvements, and bug fixes for the current and previous versions of SSMS.

[!INCLUDE[ssms-connect-aazure-ad](../includes/ssms-connect-azure-ad.md)]

## Current SSMS release

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server Management Studio (SSMS) 19.2](https://aka.ms/ssmsfullsetup)**

SSMS 19.2 is the latest general availability (GA) release of SSMS. If you need a previous version of SSMS, see [previous SSMS releases](release-notes-ssms.md#previous-ssms-releases).

### 19.2

- Release number: 19.2
- Build number: 19.2.56.2
- Release date: November 13, 2023

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2252307&clcid=0x40a) |

#### What's new in 19.2

| New Item | Details |
| ---------- | ------- |
| Azure Data Studio installation integration | The installation of SSMS installs Azure Data Studio 1.47.0. |
| Always Encrypted | Added support for secure enclaves with Azure SQL Database in the New Database dialog, Database Properties dialog, and Always Encrypted Wizard. |
| Always Encrypted | Improved performance for the Always Encrypted Wizard. |
| Azure SQL Managed Instance | Added the **Page Verify** database option on the Options page within Database Properties. |
| Client Drivers | Updated SSMS to use the latest driver versions for MSODBCSQL.MSI (17.10.5.1) and  MSOLEDBSQL.MSI (18.6.7).  The inclusion of these new versions could require users who also have older versions of the drivers to reboot after installing SSMS 19.2. |
| Connection | References to Azure Active Directory (Azure AD) updated to Microsoft Entra.  See [Azure AD is Becoming Microsoft Entra ID](https://techcommunity.microsoft.com/t5/microsoft-entra-azure-ad-blog/azure-ad-is-becoming-microsoft-entra-id/ba-p/2520436) for details. |
| Connection | Updated F1 links for the [Always Encrypted](f1-help/connect-to-server-always-encrypted-page-database-engine.md) and [Additional Connection Parameters](f1-help/connect-to-server-additional-connection-parameters-page-database-engine.md) pages in the Connection dialog. |
| Extended Events | Added support for **Watch Live Data** for event sessions created in Azure SQL Database and Azure SQL Managed Instance.  For Azure SQL Database, you must specify the database name in the **Connect to database** field in the Connection Properties tab of the Connection dialog, see [Connect to Server (Additional Connection Parameters Page) Database Engine](f1-help/connect-to-server-additional-connection-parameters-page-database-engine.md) for details.  The ability to Watch Live Data is currently in preview. |
| Extended Events | Introduced ability to use the XEvent Profiler for Azure SQL Database.  For Azure SQL Database, you must specify the database name in the **Connect to database** field in the Connection Properties tab of the Connection dialog, see [Connect to Server (Additional Connection Parameters Page) Database Engine](f1-help/connect-to-server-additional-connection-parameters-page-database-engine.md) for details. The ability to use XEvent Profiler is currently in preview. |
| Extended Events | Exposed the histogram target for event sessions in Azure SQL Database. |
| Import Flat File | Updated Import Flat File wizard to improve file encoding detection. |
| General | Introduced on-demand logging of Azure API calls from  SSMS enabling customer-facing monitoring and troubleshooting for Azure-connected features, which can be accessed within **Tools -> Options -> Output Window**.  See [Options (Output Window - General)](menu-help/options-output-window-page.md) for more information. |
| General | Updated **Help -> Technical Support** and **Help -> Send Feedback** to direct to appropriate links. |
| Ledger | Added support for creating a Ledger database in Azure SQL Managed Instance. |
| Link feature for Azure SQL Managed Instance | Improved wizard for performing failover on Managed Instance link. Supports unidirectional failover to Azure and bi-directional failover between SQL Server 2022 and Azure SQL Managed Instance. |
| Link feature for Azure  SQL Managed Instance | Improved wizard for creating the link between SQL Server and Azure SQL Managed Instance. Supports link creation from SQL Server to Azure SQL Managed Instance and from Azure SQL Managed Instance to SQL Server 2022. |
| Link feature for Azure SQL Managed Instance | Improved wizard for testing connectivity between SQL Server and Azure SQL Managed Instance. Creates a temporary testing endpoint if none exists and can be invoked from a database replica on either SQL Server or Azure SQL Managed Instance. |
| Link feature for Azure SQL Managed Instance | **Always On High Availability** menu is now available in Object Explorer for Azure SQL Managed Instance and lists established Managed Instance links. |
| Linked servers | Introduced Azure SQL resources browser in linked servers wizard facilitating linked servers setup for Azure SQL Managed Instance. |
| Object Explorer | Reduced load time for the New Database dialog in Azure SQL Database. |
| Object Explorer | Added support for the External File Format node under External Resources node for Azure SQL Database. |
| Query Editor | Introduced connection pooling for Intellisense to reduce the number of new connections made and keep connections open between refreshes. |
| SSIS | The IS Deployment Wizard now supports the Microsoft Entra Interactive Authentication Login method for Project Deployment. |

#### Bug fixes in 19.2

| New Item | Details |
| -------- | ------- |
| Accessibility | Addressed issues with keyboard focus landing in the incorrect location when classifying data, and improved color contrast in the View Facets dialog. |
| Accessibility | Improved the screen reader for status messages, and for the automatic button in the Options page in Database Properties.  The screen reader was also improved when reading the database name text within the Permissions page in Database Properties. |
| Always Encrypted | Improved Always Encrypted wizard to no longer allow the option to encrypt Ledger and history tables. |
| Always Encrypted | Fixed error 'Cannot read property AllowEnclaveComputations' when using the Always Encrypted Wizard in SQL Server 2016 and SQL Server 2017. |
| Always Encrypted | Addressed issue where Always Encrypted Wizard fails when a randomly encrypted column (using an enclave enabled key) already exists with an index on the column. |
| Always Encrypted | Updated Always Encrypted Wizard to preserve table ownership for encryption and decryption operations. |
| Always Encrypted | Fixed Always Encrypted Wizard to not allow the unsupported behavior of encrypting computed columns. |
| Connection | Updated default value for Encrypt Connection to **False** when a server is selected after using **< browse for more… >** in the connection dialog. |
| Database Properties | Updated **Is Ledger Database** option to be read-only. |
| Extended Events | Fixed exception generated when using Extended Events in Azure SQL Database in a database with a catalog collation that differs from the server collation. |
| General | Updated start menu entry to display **SQL Server Management Studio 19**. |
| General | Fixed displaying of SQL Managed Instance hardware generations when accessing resource properties. |
| General | Fixed 4k display problem in **New Filegroup** dialog. |
| Import/Export Data-Tier Application | Addressed inability to import a dacpac file from, or export a dacpac file to, an Azure Storage Container/blob. |
| Import/Export | Added support for Microsoft Entra users when importing a bacpac file. |
| Installation | Improved SSMS installer to address scenarios where setup-related registry keys are partially removed and installation hangs with 'Loading packages. Please wait...'. |
| Installation | Updated installer to properly update native binaries when upgrading from earlier versions of SSMS 19.x |
| Installation | Updated installer to no longer uninstall native driver SDKs (MSODBCSQL and MSOLEDBSQL) when the driver version on the machine is less than the one installed by SSMS.  See [Installation of SSMS 19.1 removes SQL_SNAC_SDK registry key after installing MS SQL Server 2022 standard edition](/answers/questions/1298826). |
| Intellisense | Fixed error when using $PARTITION.partition_function_name(expression) syntax. |
| Maintenance Plans | Fixed Maintenance Plan Wizard to include **Finish** button after navigating through the configuration of a maintenance plan. |
| Object Explorer | Addressed behavior where Microsoft Entra users in Azure SQL Database are prompted for credentials when modifying a stored procedure, even though the user is authenticated. See [SSMS 19 forgets connection when clicking 'modify' on a stored procedure from Object Explorer](https://feedback.azure.com/d365community/idea/643a6811-f8d8-ed11-a81c-6045bdb23064) and [Authentication Issues](https://feedback.azure.com/d365community/idea/52bc1ac3-f8f3-ed11-a81c-0022485030d1). |
| Object Explorer | Resolved error 'Cannot show requested dialog.' when trying to view properties for a Microsoft Entra database user in Azure SQL Managed Instance. |
| Object Explorer | Updated database role mapping to default to **public** as the only role for new logins. |
| Object Explorer | Fixed error 'Object reference not set to an instance of an object.' when viewing properties for a server role or properties for domain login, in SQL Server 2017. |
| Object Explorer | Removed menu options to create a new key, constraint, trigger, index or statistic on a dropped Ledger table. |
| Object Explorer | Added support for **Edit Top N Rows** scripting menu option when a table's column names are keywords such as ALTER, MERGE, PRECISION, etc. See [SSMS Edit top 200 rows error: Incorrect syntax near the keyword 'merge'](https://feedback.azure.com/d365community/idea/2b8674dc-cb42-ee11-a81c-000d3ae37d1e). |
| Object Explorer | Updated logic in Object Explorer to only execute sp_verify_dtc_configuration for Azure SQL Managed Instance. See [SQL Server Management Studio 19.1 DTC status node causes error reports when logging enabled on message 2812 on on-premises SQL installations](https://feedback.azure.com/d365community/idea/5531a64e-9a53-ee11-a81c-000d3ae37d1e). |
| Query Plan | Fixed XML error 'Instance validation error: 'CONCAT' is not a valid value for ArithmeticOperationType.' when viewing an execution plan for a query with double pipe (``||``) syntax. |
| Replication | Fixed error 'When executing sp_adddistributor for a remote Distributor, you must use a password. The password specified for the @password parameter must be the same when the procedure is executed at the Publisher and at the Distributor.' when configuring a Remote Distributor for Replication. |
| Replication | Addressed error 'Property Password cannot be changed or read after a connection string has been set' when adding a SQL Server subscriber in the New Subscription Wizard dialog. See [SSMS 19 - Issue while connecting to subscriber during replication configuration](https://feedback.azure.com/d365community/idea/4e9073b7-1dad-ed11-a81b-6045bd79fc6e). |
| Replication | Resolved issue where Replication Publication Wizard couldn't support two objects with identical names added as articles in the same publication. |
| Replication | Added support for Replication Monitor with Azure SQL Managed Instance. |
| Replication | Resolved issue where Replication Monitor connected using Windows Authentication only. |
| Replication | Added ability to use Microsoft Entra Service Principal Authentication for Replication Monitor. |
| Reports | Fixed error 'An error occurred during local report processing' when drilling into the blue bar in the CPU utilization report. |
| Result Sets | Updated results grid to respect the user-specified setting **Maximum Characters Retrieved** when selecting output from JSON data types. |

#### Known issues (19.2)

| New Item | Details | Workaround |
| -------- | ------- | ---------- |
| Analysis Services | When you connect to Analysis Services with Microsoft Entra MFA, if you add a new role or open properties for a role, the message “the identity of the user being added to the role is not fetched properly” appears. | This error is benign and can be ignored.  It will be addressed within the Azure infrastructure soon, and no updates to SSMS are required. |
| Analysis Services | After adding a new role, or when opening properties for an existing role, you can't use **Search by name or email address** to add a user. | A user can be added with the **Manual Entry** option. |
| Availability Groups | The primary server name looks empty in the Availability Group Dashboard because the text is white. | Select another entry in the dashboard and the primary server name will appear, or use an earlier version of SSMS 19. |
| Database Designer | Selecting the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | Import settings from SSMS 17 option not available. | Settings can be imported from SSMS 18. |
| Installer | Users may be prompted to update to the new release of SQL Server Management Studio, even if version 19.2 is installed, with the automatic check for updates enabled. | Use Add/Remove Programs to remove the extra installation. |
| Link feature for Azure SQL Managed Instance | After you remove an existing mirroring endpoint certificate on SQL Server, link creation through the wizard might fail due to unestablished trust between SQL Server and Azure SQL Managed Instance, even though all checks are successful. | Use PowerShell command ``Get-AzSqlInstanceServerTrustCertificate`` to check whether SQL Server mirroring endpoint certificate named "<SQL_Server_Instance_Name>" exists in the Azure SQL Managed Instance. If so, use PowerShell command ``Remove-AzSqlInstanceServerTrustCertificate`` to remove it before a new link creation attempt. |
| Linked servers | Creating a linked server to Azure SQL Database with SQL Server selected as Server type connects to the master database. | To create a linked server to Azure SQL Database, select **Other data source** for the **Server type**, and select **Microsoft OLE DB Provider for SQL Server** or **Microsoft OLE DB Driver for SQL Server** as the **Provider**. Enter logical server name in the Data source field, and enter database name in the Catalog field. |
| PolyBase | PolyBase node isn't visible in Object Explorer when connecting to SQL 2022. | Use SSMS 18.12.1. |
| Profiler | The Profiler menu isn't localized. | No current alternative. |
| Replication | If Azure SQL Managed Instance is the publisher and SSMS is running on a machine which isn't in the same vNet as the publisher, you won't be able to insert a tracer token via Replication Monitor. | To insert tracer tokens, use Replication Monitor in SSMS on a machine that is in the same vNet as the Azure SQL Managed Instance publisher. |
| Stretch DB | Removed Stretch DB Wizard. | Use T-SQL to configure Stretch DB or use SSMS 18.9.1 or earlier to use the Stretch DB Wizard. |

You can reference [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server) for other known issues and to provide feedback to the product team.

## Previous SSMS releases

Download previous SSMS versions by selecting the download link in the related section.

| SSMS version | Build number | Release date |
| ------------ | ------------ | ------------ |
| [19.1](#191) | 19.1.56.0 | May 24, 2023 |
| [19.0.2](#1902) | 19.0.20209.0 | March 13, 2023 |
| [19.0.1](#1901) | 19.0.20200.0 | February 2, 2023 |
| [19.0](#190) | 19.0.20196.0 | January 26, 2023 |
| [18.12.1](#18121) | 15.0.18420.0 | June 21, 2022 |
| [17.9.1](#1791) | 14.0.17289.0 | November 21, 2018 |
| [16.5.3](#1653) | 13.0.16106.4 | January 30, 2017 |

### 19.1

- Release number: 19.1
- Build number: 19.1.56.0
- Release date: May 24, 2023

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2237215&clcid=0x40a) |

#### What's new in 19.1

| New Item | Details |
| ---------- | ------- |
| Azure Data Studio installation integration | The installation of SSMS installs Azure Data Studio 1.44. |
| Always Encrypted | Added support for secure enclaves and in-place encryption in the Always Encrypted Wizard. See [Configure column encryption using Always Encrypted Wizard](../relational-databases/security/encryption/always-encrypted-wizard.md). |
| Azure SQL Managed Instance | Introduced visibility to the status of the Distributed Transaction Coordinator (DTC) service for Azure SQL Managed Instance. Object Explorer can be used to determine if DTC is enabled on the Azure SQL Managed Instance (within the Management node). See [Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance](/azure/azure-sql/managed-instance/distributed-transaction-coordinator-dtc). |
| Back up/Restore | Added capability to restore back up files from S3-compatible storage to SQL Server 2022 and Azure SQL Managed Instance. |
| General SSMS | Updated File Version for ssms.exe to align with product version. |
| General SSMS | Removed deprecated hardware from the list of available service-level objects. |
| General SSMS | Changed the system browser setting, within **Tools > Options > Azure Services**, to default to True. The external browser is used, instead of the legacy embedded browser. |
| General SSMS | Removed Vulnerability Assessment from SSMS. |
| Link feature for Azure SQL Managed Instance | Added Network Checker wizard, providing the capability to test the network connection and ports before creating [the link](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview). |
| Link feature for Azure SQL Managed Instance | Added an advanced network troubleshooting capability within the existing link creation wizard.  This change provides the capability to troubleshoot network connectivity issues while [link](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview) creation is in progress. |
| Object Explorer | Removed script header text when selecting the top 1000 rows.  See [Remove the /****** Script for SelectTopNRows command from SSMS ******/ comment when "Select Top 1000 Rows](https://feedback.azure.com/d365community/idea/393fae39-3e7a-ed11-a81b-000d3ae5ae95). |
| PowerShell | Added ability for users to choose the version of PowerShell to use when launched from SSMS. |
| PowerShell | Introduced more PowerShell options within **Tools > Options > SQL Server Object Explorer > Commands**.  For more information, visit [Options (SQL Server Object Explorer - Commands)](menu-help/options-sql-server-object-explorer-commands.md). |

#### Bug fixes in 19.1

| New Item | Details |
| -------- | ------- |
| Accessibility | Introduced multiple screen reader improvements in the installation window and on the main page. |
| Attach Database | Addressed issue where attaching a database with a log file that didn't reside in the default directory displayed the message "Transaction log file not found. A new empty log will be created." |
| Azure SQL Managed Instance | Fixed controls that were incorrectly disabled in "New Linked Servers" wizard and prevented customers from creating a linked server using the MSOLEDBSQL driver. |
| Azure SQL Managed Instance | Addressed inability to configure Integration Services tasks in a SQL Server Agent Job in Azure SQL Managed Instance.  See [Configuration of SSIS Task for Agent Job in SSMS 19.0.1 Does Not Work in Azure SQL Managed Instance](https://feedback.azure.com/d365community/idea/22be840e-01af-ed11-a81b-6045bd79fc6e). |
| Backup/Restore | Updated database backup dialog to enable "OK" button when connect to an Availability Group via the Listener or IP address. |
| Connection | Fixed an issue where an expired Active Azure Directory refresh token cause SSMS to crash. |
| Connection | Addressed intermittent application crash when closing SSMS immediately after connection dialog closes. |
| Connection | Updated authentication process for an alias-based email to allow the authentication token for subsequent connections for the same login. |
| Data Classification | Resolved freeze in SSMS that occurred when trying to set Microsoft Information Protection Policy. |
| Full Text Indexing | Fixed issue that didn't permit viewing the properties of a full text index. |
| General | Delayed the initialization of the output window to prevent slow SSMS startup. |
| Intellisense | Added support for SORT_IN_TEMPDB T-SQL syntax when creating a primary key using ALTER TABLE. |
| Intellisense | Updated Intellisense to identify invalid index operations for ALTER TABLE T-SQL syntax. |
| Intellisense | Introduced support for SQL Server 2022 new compression algorithm for back ups. |
| Intellisense | Added Intellisense support for new functions introduced in SQL Server 2022 (for example, JSON_ARRAY, ISJSON, WINDOW, BIT_COUNT, etc.). |
| Link feature for Azure SQL Managed Instance | Updated T-SQL script generated by the wizard to enclose the certificate name. |
| PolyBase | Resolved a crash that occurred when accessing the Permission table of an existing External Data Source or External File Format. |
| Registered Servers | Expanded options supported when importing connections from Azure Data Studio. |
| Replication | Resolved inability to open properties page for a local publication under the Local Publications node in Object Explorer.  See[SSMS 19.0.1 cannot open Properties dialog for local publications](https://feedback.azure.com/d365community/idea/3dba641e-a9a3-ed11-a81b-6045bd79fc6e). |
| Replication | Resolved error "Value was either too large or too small for an Int32" when creating a publication that contains two different object types with the same name. |
| SMO/Scripting | Addressed error "Attempt to retrieve data for object failed for Server 'server_name_here'" when editing a new job after scripting an action to a job. |
| SMO/Scripting | Fixed inability to generate scripts for tables from selected wizards. |
| SSIS | Resolved inability to connect using Azure SSIS Integration runtime. |
| Storage Account | Fixed issue that prevented deleting a container from an Azure storage account. |
| Table Editor | Fixed issue with application hanging when editing an NVARCHAR(255) column in a table.  See [SSMS v18.12.1 (also) crashes in edit mode when update a field (varchar(255)) of specific record](/answers/questions/1032195/). |
| Table Editor | Addressed incorrect information displayed when editing data in a table that contains a period (.) in an Azure SQL Database.  See [SSMS Table in Azure - Design shows < Unknown >](/answers/questions/1187830/). |

#### Known issues (19.1)

| New Item | Details | Workaround |
| -------- | ------- | ---------- |
| Always Encrypted | Always Encrypted Wizard error "Cannot read property AllowEnclaveComputations" occurs for SQL Server 2016 or SQL Server 2017. | Use an earlier version of SSMS (19.02 or 18.12.1) with SQL Server 2016 and SQL Server 2017. |
| Database Designer | Selecting the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | Import setting from SSMS 17 option not available. | Settings can be imported from SSMS 18. |
| General SSMS | Upgrading from SSMS 19.0.2 to 19.1 results in some bug fixes not appearing to be resolved.  Specifically, users see issues when they edit a NVARCHAR(255) column in a table.  In addition, users see an unknown table name when editing data if the database name in Azure contains a period (.), and SSMS file versions are incorrect. | Uninstall SSMS 19.1 and then re-install SSMS 19.1. |
| Import/Export Data-Tier Application | Using Export to Data-Tier Application with a Microsoft Azure Storage Account generates the error "An error occurred while loading data." | Use SSMS 19.0.2 or earlier. |
| Object Explorer | Modifying a stored procedure when using Azure AD Authentication generates a "Cannot open server" error. | Use a non-Azure AD login, or SSMS 18.12.1. |
| PolyBase | PolyBase node isn't visible in Object Explorer when connecting to SQL 2022. | Use SSMS 18.12.1. |
| Profiler | The Profiler menu isn't localized. | No current alternative. |
| Query Editor | When SSMS opens after double-clicking on an `.sql` file, the Object Explorer window is displayed as a separate window. | No current alternative. |
| Server Audit | Error "Item has already been added. Key in dictionary: 'MNDO'  Key being added: 'MNDO'" when viewing Logs for an Audit. | No current workaround. |
| Stretch DB | Removed Stretch DB Wizard. | Use T-SQL to configure Stretch DB or use SSMS 18.9.1 or earlier to use the Stretch DB Wizard. |

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
| Azure SQL Managed Instance | Restored ability to view File and FileGroups pages on Database Properties window. See [bugs in 19.0.1](https://feedback.azure.com/d365community/idea/b3d026e6-b8b2-ed11-a81b-000d3ae6a6aa).|
| Connection | Addressed error "Your app has been throttled by Azure AD due to too many requests" when using Azure Active Directory authentication.  See [SSMS 19 Azure AD Integrated: Your app has been throttled by Azure AD due to too many requests](https://feedback.azure.com/d365community/idea/b4b0d281-c2a0-ed11-a81b-6045bd8615b0). | 
| Connection | Resolved SSMS crash behavior when logging into Azure and changing the user. |
| Fulltext | Fixed an issue that caused a table's fulltext index to be rebuilt when moving the table to a different filegroup. |
| General SSMS | Added digital signature to dll files. |
| Link feature for Azure SQL Managed Instance | Fixed error "Exception has been thrown by the target of an invocation", which occurred when a subscription with no resource groups was selected attempting to create a link using Azure SQL Managed Instance link wizard. |
| Profiler | Fixed issue that generated "Errors in the OLE DB provider. Unable to obtain authentication token using the credentials provided" error trying to run SQL Profiler with a Power BI workspace. |
| Replication | Addressed error "Property Password can't be changed or read after a connection string has been set" which occurred when trying to configure a replication subscriber. See [SSMS 19 - Issue while connecting to subscriber during replication configuration](https://feedback.azure.com/d365community/idea/4e9073b7-1dad-ed11-a81b-6045bd79fc6e).|
| Replication | Fixed error "SQL Server encountered one or more errors while retrieving information about publication", which occurred when trying to view the properties for a publication. See [SSMS 19.0.1 can't open Properties dialog for local publications](https://feedback.azure.com/d365community/idea/3dba641e-a9a3-ed11-a81b-6045bd79fc6e).|
| Reports | Corrected server startup time on Server Dashboard report. |
| SQL Agent | Addressed inability to start the SQL Agent from SSMS. |

#### Known issues (19.0.2)

| New Item | Details | Workaround |
| -------- | ------- | ---------- |
| Database Designer | Selecting the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | Import setting from SSMS 17 option not available. | Settings can be imported from SSMS 18. |
| Profiler | The Profiler menu isn't localized. | No current alternative. |
| Query Editor | When SSMS opens after double-clicking on an `.sql` file, the Object Explorer window is displayed as a separate window. |
| Storage Account | Trying to delete a container from a storage account fails with a (400) Bad Request error. | Use the Azure portal for container deletion. |
| Server Audit | Error "Item has already been added. Key in dictionary: 'MNDO'  Key being added: 'MNDO'" when viewing Logs for an Audit. | No current workaround. |
| Stretch DB | Removed Stretch DB Wizard. | Use T-SQL to configure Stretch DB or use SSMS 18.9.1 or earlier to use the Stretch DB Wizard. |

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
| Maintenance Plan | Fixed issue that generated "Failed to retrieve data for this request" error when using back up to NUL in the Back Up Database Task. |
| Object Explorer | Fixed a regression, which caused Object Explorer to not show database objects for Azure SQL DB Basic SLO. |

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
| Azure Active Directory Authentication | Connections to relational SQL instances now support Azure Active Directory Service Principal, Azure Active Directory Managed Identity and Azure Active Directory Default authentication methods. "Default" uses a series of possible sources for authentication, including environment variables, Azure Managed Identity, the user's shared token cache, Visual Studio, Azure CLI, and Azure PowerShell. For more information, visit: [Options (Azure Services)](menu-help/options-azure-services.md) |
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
| PowerShell | Removed "Generate PowerShell Script" from In-Memory OLTP Migration. |
| Query Execution or Results | Improved checks for open connections. |
| Query Plan | Added ellipses button to Residual property for Merge Join operator. See [SSMS: Add ellipsis button to Residual property for Merge Join operator](https://feedback.azure.com/d365community/idea/025ef426-4a88-ed11-a81b-000d3adb7ffd). |
| Query Results | Added option in **Tools > Options > Query Execution** to not display the "querying transaction count" window when closing a query window. See [SSMS - Querying transaction count - Async](https://feedback.azure.com/d365community/idea/aaaad978-65b9-ec11-a81c-6045bd80aaa9). |
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
| Back up | Added ability to create a NUL back up device against SQL Server 2017. |
| Central Management Servers | Provided ability to view SQL ERRORLOGs from Central Management Servers before SQL 2022. |
| Column Master Key | Increased width of Column Master Key name in the rotation dialog, so the full name is visible. |
| Connection | Fixed an issue with authentication that doesn't use Azure Resource Manager and Microsoft Purview Information Protection. |
| Connection | Addressed issue with logging into Azure with a non-SQL authenticated on a machine not joined to a domain. |
| Copy Database Wizard | Fixed the log provider type error, which occurred when copying a database using The SQL Management Object method. |
| Database Restore | Resolved error generated when restoring a back up to an existing database. See [Unexpected behavior by SSMS](https://feedback.azure.com/d365community/idea/95b549d0-6f70-ed11-a81b-000d3adb7ffd). |
| Database Tuning Advisor | Added ability to ignore unexpected elements in showplan XML when generating recommendations. |
| Database Tuning Advisor | Resolved failure to populate the tuning log table issue. |
| Database Tuning Advisor | Implemented security fixes, including replacing ZeroMemory with SecureZeroMemory. |
| Import/Export Data-Tier Application | Restored ability to deploy an extracted dacpac to a database via the Deploy Data-Tier Application option. |
| Link feature for Azure SQL Managed Instance | Fixed problem with leaking connections in MI Link. |
| Link feature for Azure SQL Managed Instance | Updated size of subscription dropdown in the sign in to Azure pane to properly display subscription names. |
| Link feature for Azure SQL Managed Instance | Updated display to show Azure sign-in options correctly. |
| Microsoft Purview Information Protection | Improved icon display when viewing Microsoft Purview Information Protection in older versions of SQL Server. |
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
| SqlParser | Added missing options for CREATE USER and CREATE sign in. |
| SSIS | The "Schedule..." menu item is now visible in the Azure SSIS Catalog. |
| Synapse | Created consistent naming for Synapse offerings in SSMS. |
| XEvents | Fixed an issue where reading target data for event sessions whose name overlaps with another session name caused data from the incorrect event session to appear in the viewer. |

#### Known issues (19.0)

| New Item | Details | Workaround |
| -------- | ------- | ---------- |
| Database Designer | Selecting the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| Profiler | The Profiler menu isn't localized. | No current alternative. |
| Query Editor | When SSMS opens after double-clicking on an `.sql` file, the Object Explorer window is displayed as a separate window. |
| Storage Account | Trying to delete a container from a storage account fails with a (400) Bad Request error. | Use the Azure portal for container deletion. |
| Server Audit | Error "Item has already been added. Key in dictionary: 'MNDO'  Key being added: 'MNDO'" when viewing Logs for an Audit. | No current workaround. |
| Stretch DB | Removed Stretch DB Wizard. | Use T-SQL to configure Stretch DB or use SSMS 18.9.1 or earlier to use the Stretch DB Wizard. |

### 18.12.1

:::image type="icon" source="../includes/media/download.svg" border="false"::: [Download SSMS 18.12.1](https://go.microsoft.com/fwlink/?linkid=2199013&clcid=0x409)

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

SQL Server utility is no longer available in versions 17.x and newer.

#### Bug fixes in 17.9.1

- Fixed an issue where users could experience their connection being closed and reopened with each query invocation when using "Azure Active Directory - Universal with MFA support" authentication with the SQL query editor. Side effects of the connection closing included global temporary tables being dropped unexpectedly and sometimes a new SPID given to the connection.
- Fixed a long outstanding issue where a restore plan would fail to find a restore plan or generate an inefficient one under certain conditions.
- Fixed issue in the "Import Data-tier Application" wizard, which could result in an error when connected to an Azure SQL Database.

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

- Fixed issue introduced in SSMS 16.5.2, which was causing the expansion of the 'Table' node when the table had more than one sparse column.

- Users can deploy SSIS packages containing OData Connection Manager, which connects to a Microsoft Dynamics AX/CRM Online resource to the SSIS catalog. For more information, For details, see [OData Connection Manager](../integration-services/connection-manager/odata-connection-manager.md).

- Configuring Always Encrypted on an existing table fails with errors on unrelated objects. [Connect ID 3103181](https://connect.microsoft.com/SQLServer/feedback/details/3103181/setting-up-always-encrypted-on-an-existing-table-fails-with-errors-on-unrelated-objects)

- Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect ID 3109591](https://connect.microsoft.com/SQLServer/feedback/details/3109591/sql-server-2016-always-encrypted-against-existing-database-with-multiple-schemas-doesnt-work)

- The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect ID 3111925](https://connect.microsoft.com/SQLServer/feedback/details/3111925/sql-server-2016-always-encrypted-encrypted-column-wizard-failed-task-failed-due-to-following-error-cannot-save-package-to-file-the-model-has-build-blocking-errors)

- When you encrypt with Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.

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
