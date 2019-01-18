---
title: "SQL Server Management Studio - Changelog (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "12/19/2018"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: 3dc76cc1-3b4c-4719-8296-f69ec1b476f9
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# SQL Server Management Studio - Changelog (SSMS)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

This article provides details about updates, improvements, and bug fixes for the current and previous versions of SSMS. Download [previous SSMS versions](#previous-ssms-releases).



## [SSMS 18.0 (preview 6)](download-sql-server-management-studio-ssms.md)

Build number: 15.0.18075.0<br>
Release date: December 18, 2018

Preview 6 is our latest public preview of SSMS 18.0. For the latest General Availability (GA) version of SSMS, [download and install SSMS 17.9.1](#ssms-1791-latest-ga-release).

### What's new in preview 6

This section lists what's new in SSMS 18.0 preview 6. For a complete changelog since SSMS 17.9.1, see [SSMS 18.0 preview - cumulative changelog through preview 6](#ssms-180-preview---cumulative-changelog-through-preview-6).

- **SSMS**
  - Added "Migrate to Azure" under Tools menu – We have integrated [Database Migration Assistant](https://aka.ms/get-dma) and [Azure Database Migration Service](https://aka.ms/get-dms) to provide quick and easy access to help accelerate your migrations to Azure.
  - Previous version of SSMS 18.0 (< Preview 6) had the "Available Databases" key shortcut bound to **CTRL+ALT+J**. In Preview 6 and later, the key binding has been restored to **CTRL+U**, like it was in SSMS 17.x.
  - Added logic to prompt the user to commit open transactions when "Change connection" is used.

- **SSIS**
  - When you use SQL Agent of MI by SSMS, you can configure parameter and connection manager in SSIS agent job step.


### Bug fixes

- **SSMS Editor**
  - New TRANSLATE function now recognized by intellisense. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898430.
  - Improved intellisense on FORMAT built-in function. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898676.
  - LAG and LEAD are now recognized as built-in functions. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898757.
  - Fixed an issue where intellisense was giving a warning when using "ALTER TABLE...ADD CONSTRAINT...WITH(ONLINE=ON)"

- **Object Scripting**
  - Fixed an issue where trying to script a spatial index with GEOMETRY_AUTO_GRID/GEOGRAPHY_AUTO_GRID on an Azure SQL Database was throwing an error.

- **SMO**
  - Fixed an issue where an application written using SMO would encounter an error if it tried to enumerate databases from the same server on multiple threads even though it uses separate SqlConnection instances on each thread.

- **Data Classification**
  - Fixed an issue when saving classifications in the data classification pane while there are another data classification panes open on other databases.

- **Azure SQL Database**
  - Enabled the Statistics properties sub menu option under menu Statistics in Azure, since it has been fully supported for quite some time now.

- **Query Data Store**
  - Fixed an issue where a "DocumentFrame (SQLEditors)" exception could be thrown.
  - Fixed an issue when trying to set a custom time interval in the build-in Query Store reports the user was not able to select AM or PM on the start/end interval.

- **Copy Database Wizard**
  - Generate scripts/Transfer/Copy Database Wizard try to create a table with an in memory table doesn't force ansi_padding on.
  - Transfer Database task/Copy Database Wizard broken on SQL Server 2017 and SQL Server 2019.
  - Generate scripts/Transfer/Copy Database Wizard script table creation before creation of associated external data source.

- **Profiler**
  - Added "Aggregate Table Rewrite Query" event to Profiler events.

- **ShowPlan**
  - New mem grant operator properties display incorrectly when there is more than one thread.

### Known issues

- Double-clicking on a .sql file launches SSMS, but does not open the actual script.
  - Workaround: drag and drop the .sql file onto the SSMS editor.

## SSMS 18.0 preview - cumulative changelog through preview 6

If there is no *preview 5* or *preview 6* label it indicates the change appeared in our first public preview of SSMS 18.0, which was SSMS 18.0 *preview 4*.

### What's new

- **SSMS**
  - Smaller Download Size
    - The current size of the bundle is less than half of what SSMS 17.x is (~400 MB). The size will eventually grow a little when the IS components are added back to SSMS, but it should not be as large as it used to be.
  - SSMS is based on the new VS 2017 Isolated Shell
    - This means a modern shell (we picked up VS 2107 15.6.4). The new shell unlocks all the accessibility fixes that went in both SSMS and Visual Studio.
  - SSMS accessibility improvements
    - Much work went in to address Accessibility issues in all the tools (SSMS, DTA, and Profiler)
  - SSMS can be installed in custom folder
    - Currently, this is only available on the command-line setup. Pass this extra argument to the SSMS-Setup-ENU.exe:
      - SSMSInstallRoot=C:\MySSMS18
      - By default, the new install location for SSMS is:
      - %ProgramFiles(x86)%\Microsoft SQL Server Management Studio 18\Common7\IDE\ssms.exe
      - Note: this does not mean what SSMS is multi-instance.
  - SSMS does not share components with SQL Engine anymore
    - Much effort went in to avoid sharing components with SQL Engine, which often resulted in serviceability issues (one clobbering the files installed by the other.
  - SSMS requires NetFx 4.7.2 or greater
    - We upgraded our minimum requirement from NetFx4.6.1 to NetFx4.7.2: this will allow us to take advantage of the new functionality exposed by the new framework.
  - SSMS not supported on Windows 8 and Windows Server 2012; Windows 10 / Windows Server 2016 will require at least version 1607 (10.0.14393)
    - Due to the new dependency on NetFx 4.7.2, SSMS 18.0 does not install on Windows 8 and Windows Server 2012 and older versions of Windows 10 and Windows Server 2016. SSMS setup will block on those OSes. Note: "Windows 8.1" is still supported.
  - SSMS is not added to the PATH environment variable
    - Path to SSMS.EXE (and Tools in general) is not added to the path anymore. The users can either add it themselves or, if on a modern Windows, rely on the Start menu.
  - Support for SQL Server SQL2019
    - This is the first release of SSMS that would be fully aware of SQL Server 2019 (compatLevel 150, etc…)
    - Support "BATCH_STARTED_GROUP" and "BATCH_COMPLETED_GROUP" in SQLSERVER2018 and managed instance in SSMS
    - SMO support for UDF Inlining
    - GraphDB: Add flag in showplan for Graph TC Sequence
    - Always Encrypted: added support for AEv2 / Enclave
    - Always Encrypted: connection dialog has a new tab "Always Encrypted" when the user clicks on the "Options" button to enable/configure Enclave support.
  - Package IDs no longer needed to develop SSMS Extensions
    - In the past, SSMS was selectively loading only well-known packages, thus requiring developers to register their own package. This is no longer the case.
  - Support for High DPI is enabled by default
  - Better Azure SQL Database support
  - SLO/Edition/MaxSize database properties now accept custom names, making it easier to support future editions of SQL Azure databases
  - Added support for recently added vCore SKUs (General Purpose and Business Critical): Gen4_24 and all the Gen5.
  - Exposing AUTOGROW_ALL_FILES config option  for Filegroups in SSMS
  - Removed risky 'lightweight pooling' and 'priority boost' options from SSMS GUI (For details, see https://blogs.msdn.microsoft.  com/arvindsh/2010/01/26/priority-boost-details-and-why-its-not-recommended/)
  - SQL Editor honors the CTRL+D shortcut to duplicate lines (For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32896594)
  - New menu and key bindings to creates files: CTRL+ALT+N. CTRL+N will continue to create a new query. Note: if you are migrating from "SSMS 18.0 Preview 1", you must reset the user settings from "**Tools | Import Export Settings | Reset all settings**".
  - "New Firewall Rule" dialog now allow the user to specify a rule name, instead of automatically generating one on behalf of the user (For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32902039)=
  - Data Classification: updated the recommendations
  - Improved intellisense in Editor especially for v140 T-SQL
  - Added support in SSMS UI for UTF-8 on collation dialog.
  - Switched to "Windows Credential Manager" for connection dialog MRU passwords. This will address a long outstanding issue where persistence of passwords was not always reliable. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32896486.
  - Improved support for multi-monitor systems by making sure that more and more dialogs and windows pop up on the expected monitor.
  - Exposed the 'backup checksum default' server configuration in the new Database Settings page of the Server Properties Dialog. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/34634974
  - [New in Preview 5] Exposed "maximum size for error log files" under "Configure SQL Server Error Logs". For details, see  https://feedback.azure.com/forums/908035/suggestions/33624115 
  - [New in Preview 6] Added "Migrate to Azure" under Tools menu – We have integrated [Database Migration Assistant](https://aka.ms/get-dma) and [Azure Database Migration Service](https://aka.ms/get-dms) to provide quick and easy access to help accelerate your migrations to Azure.
  - [New in Preview 6] Previous version of SSMS 18.0 (< Preview 6) had the "Available Databases" key shortcut bound to **CTRL+ALT+J**. In Preview 6 and later, the key binding has been restored to **CTRL+U**, like it was in SSMS 17.x.
  - [New in Preview 6] Added logic to prompt the user to commit open transactions when "Change connection" is used.

- **SMO**
  - Extend SMO Support for Resumable Index Creation
  - Added new event on SMO objects ("PropertyMissing") to help application authors to detect SMO performance issues sooner.  
  - Exposed new DefaultBackupChecksum property on the Configuration object which maps to the "backup checksum default" server configuration
  - [New in Preview 5] Exposed new ProductUpdateLevel property on the Server object, which maps to the servicing level for the version of SQL in use (e.g. CU12, RTM, etc…)
  - [New in Preview 5] Exposed new LastGoodCheckDbTime property on  Database object, which maps to "lastgoodcheckdbtime" database property. If such property is not available, a default value of 1/1/1900 12:00:00 AM will be returned.
  - [New in Preview 5] Moved location for RegSrvr.xml file (Registered Server configuration file) to "%AppData%\Microsoft\SQL Server Management Studio" (unversioned, so it can be shared across versions of SSMS)

- **Azure Data Studio integration**
  - Added menu item to start/download Azure Data Studio
  - [New in Preview 5] Added "Start Azure Data Studio" menu item to Object Explorer

- **ShowPlan**
  - Added actual time elapsed, actual vs estimated rows under ShowPlan operator node if they are available. This will make actual plan look consistent with Live Query Stats plan.
  - Modified tooltip and added comment when clicking on Edit Query Button for a ShowPlan, to indicate to user that the ShowPlan might be truncated by the SQL engine if the query is over 4000 characters.
  - Added logic to display the "Materializer Operator (External Select)"
  - Add new showplan attribute BatchModeOnRowStoreUsed to easily identify queries that are using the " batch-mode scan on rowstores" feature. Anytime a query performs batch-mode scan on rowstores, a new attribute (BatchModeOnRowStoreUsed="true") gets added to StmtSimple element.

- **Database Compatibility Level Upgrade**
  - [New in Preview 5] Added a new option under <Database name> -> Tasks -> Database Upgrade. This will start the new Query Tuning Assistant (QTA) to guide the user through the process of:
    - Collecting a performance baseline before upgrading the database compatibility level. 
    - Upgrading to the desired database compatibility level
    - Collecting a 2nd pass of performance data over the same workload.
    - Detect workload regressions, and provide tested recommendations to improve workload performance.

  This is close to the database upgrade process documented in https://docs.microsoft.com/sql/relational-databases/performance/query-store-usage-scenarios#CEUpgrade, except for the last step where QTA does not rely on a previously known good state to generate recommendations.

- **Query Store**
  - [New in Preview 5] Improved usability of some reports (Overall Resource Consumptions) by adding thousands separator to numbers displayed on the Y-axis of the charts.
  - [New in Preview 5] Added a new Query Wait Statistics report.

- **Data Masking**
  - [New in Preview 5, Add2018114] Added Static Data Masking. Static Data Masking is a data protection tool that allows users to create a copy of their SQL database and mask sensitive data on the copy. The feature will prove useful for those who share their production database with nonproduction users such as dev/test team or analytics team. For more information, see [Static Data Masking for Azure SQL Database and SQL Server](https://azure.microsoft.com/blog/static-data-masking-preview/).

- **Always On**
  - Rehash RTO (estimated recovery time)  and RPO (estimated data loss) in SSMS Always on Dashboard. Documentation is being updated at https://docs.microsoft.com/sql/database-engine/availability-groups/windows/monitor-performance-for-always-on-availability-groups

- **Audit Files**
  - Changed authentication method from Storage Account Key based to Azure AD based authentication

- **SSIS**
  - [New in Preview 5] Added support to allow customers to schedule SSIS packages on Azure-SSIS IRs which are in Azure Government cloud
  - [New in Preview 6] When you use SQL Agent of MI by SSMS, you can configure parameter and connection manager in SSIS agent job step.

- **Data Classification**
  - Reorganized data classification task menu: added sub menu to the database tasks menu and added an option to open the report from the menu without opening the classify data window first.

- **Vulnerability Assessment**
  - [New in Preview 5]  Enabled Vulnerability Assessment tasks menu on SQL Azure DW.

- **Always Encrypted**
  - The Enable Always Encrypted checkbox in the new Always Encrypted tab in the Connect to Server dialog now provides an easy way to enable/disable Always Encrypted for a database connection. 

- [**Always Encrypted with secure enclaves**](https://docs.microsoft.com/sql/relational-databases/security/encryption/always-encrypted-enclaves)
  - Several enhancements have been made to support  Always Encrypted with secure enclaves in SQL Server 2019 preview
    - A text field for specifying enclave attestation URL in the Connect to Server dialog (the new Always Encrypted tab).
    - The new checkbox in the New Column Master Key dialog to control weather a new column master key allows enclave computations.
    - Other Always Encrypted key management dialogs now expose the information on which column master keys allow enclave computations.


### Bug fixes

- **Crashes / Hangs**
  - Fixed a source of common SSMS crashes related to GDI objects
  - Fixed a common source of hangs and poor performance when selecting "Script as Create/Update/Drop" (removed unnecessary fetches of SMO objects)
  - Fixed a hang when connecting to an Azure SQL DB using MFA while ADAL traces are enabled
  - Fixed a hang (or perceived hang) in Live Query Statistics when invoked from Activity Monitor (the issue manifested when using SQL Server authentication with no "Persist Security Info" set).
  - Fixed a hang when selecting "Reports" in Object Explorer which could manifest on high latency connections or temporary non-accessibility of the resources.
  - [New in Preview 5] Fixed a crash in SSSM when trying to use Central Management Server and SQL Azure servers. For details, see https://feedback.azure.com/forums/908035/suggestions/33374884. 
  - [New in Preview 5] Fixed a hang in Object Explorer by optimizing the way IsFullTextEnabled  property is retrieved
  - [New in Preview 5] Fixed a hang in "Copy Database Wizard" by avoiding to build unnecessary queries to retrieve Database properties

- **Connection dialog**
  - Enabled the removal of usernames from previous username list by pressing the DEL key (For details, see https://feedback.azure.com/forums/908035/suggestions/32897632)

- **XEvent**
  - Added two columns "action_name" and "class_type_desc" that show action ID and class type fields as readable strings.
  - Removed the event XEvent Viewer cap of 1,000,000 events.

- **External Tables**
  - Added support for Rejected_Row_Location in template, SMO, intellisense, and property grid

- **SSMS Options**
  - Fixed an issue where "**Tools | Options | SQL Server Object Explorer** | Commands" page was not resizing properly.
  - SSMS will now by default disable automatic download of DTD in XMLA editor -- XMLA script editor (which uses the xml language service) will by default now prevent automatically downloading the DTD for potentially malicious xmla files.  This is controlled by turning off the “Automatically download DTDs and Schemas” setting in Tools->Options->Environment->Text Editor->XML->Miscellaneous.  
  - [New in Preview 5] Restored CTRL+D to be the shortcut as it used to be in older version of SSMS. For details, see https://feedback.azure.com/forums/908035/suggestions/35544754

- **SSMS Editor**
  - Fixed an issue where "SQL System Table" where restoring the default colors was chancing the color to lime green, rather than the default green, making it very hard to read on a white background (For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32896906)
  - Fixed issue where intellisense was not working when connected to Azure SQLDW using AAD authentication.
  - Fixed intellisense in Azure when user lacks master access
  - Fixed code snippets to create "temporal tables" which were broken when the collation of the target database was case sensitive.
  - [New in Preview 6] New TRANSLATE function now recognized by intellisense. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898430
  - [New in Preview 6] Improved intellisense on FORMAT built-in function. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898676
  - [New in Preview 6] LAG and LEAD are now recognized as built-in functions. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898757
  - [New in Preview 6] Fixed an issue where intellisense was giving a warning when using "ALTER TABLE...ADD CONSTRAINT...WITH(ONLINE=ON)"

- **Object Explorer**
  - Fixed an issue where SSMS was throwing an "Object cannot be cast from DBNull to other types" exception when trying to expand "Management" node in OE (misconfigured DataCollector)
  - Fixed an issue where the DEL key was not working while renaming a node (For details, see https://feedback.azure.com/forums/908035/suggestions/32910247 and other duplicates)
  - Fixed an issue where OE wasn't escaping quotes before invoking the "Edit Top N…" causing the designed to get confused
  - Fixed an issue where the "Import Data-Tier application" wizard was failing to launch from the Azure Storage tree.
  - Fixed an issue in "Database Mail Configuration" where the status of the SSL checkbox was not persisted (For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32895541)
  - Fixed an issue where SSMS grayed out option to close existing connections when trying to restore database with is_auto_update_stats_async_on
  - Fixed an issue where right-clicking on nodes in OE the (e.g. "Tables" and wanting to perform an action such as filtering tables by going to Filter > Filter Settings, the filter settings form can appear on the other screen than where SSMS is currently active). For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/34284106.
  - Fixed a long outstanding issue where the DELETE key was not working in OE while trying to rename an object. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/33073510
  - When displaying the properties of existing database files, the size appears under a column "Size (MB)" instead of "Initial Size (MB)" which is what is displayed when creating a new database. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32629024
  - Disabled the "Design" context-menu item on "Graph Tables" since there is no support for those kind of tables in the current version of SSMS.
  - [New in Preview 5, in-cycle] Fixed an issue where the "New Job Schedule" dialog was not rendering properly on High DPI monitors. For details, see https://feedback.azure.com/admin/v3/suggestions/35541262 
  - [New in Preview 5] Fixed/improved the way  an issue where a database size ("Size (MB)") is displayed in Object Explorer details: only 2 decimal digits and formatted using the thousands separator. For details, see https://feedback.azure.com/forums/908035/suggestions/34379308
  - [New in Preview 5] Fixed an issue that was causing the creation of a "Spatial Index" to fail with an error like "To accomplish this action, set property PartitionScheme".
  - [New in Preview 5] Minor performance improvements in Object Explorer to avoid issuing extra queries, when possible.
  - [New in Preview 5] Extended logic to request confirmation when renaming a database to all the schema objects (the setting can be configured and this disabled)

- **Help Viewer**
  - Improved logic around honoring the online/offline modes (there may still be a few issues that need to be addressed)
  - Fixed the "View Help" to honor the online/offline settings. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32897791

- **Object Scripting**
  - Overall perf improvements - Generate Scripts of WideWorldImporters takes half the time compared to SSMS 17.7
  - When scripting objects, DB Scoped configuration which have default values are omitted 
  - Don't generate dynamic T-SQL when scripting(For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898391)
  - Omit the graph syntax "as edge" and "as node" when scripting a table on SQL Server 2016 and earlier.
  - Fixed an issue where scripting of database objects was failing when connecting to a Azure SQL Database using AAD with MFA.
  - [New in Preview 6] Fixed an issue where trying to script a spatial index with GEOMETRY_AUTO_GRID/GEOGRAPHY_AUTO_GRID on a Azure SQL Database was throwing an error.

- **Table Designer**
  - [New in Preview X, X<4] Fixed a crash in "Edit 200 rows"
  - Fixed an issue where the designer was allowing to add a table when connected to a Azure SQL Database

- **SMO**
  - Fixed an issue where SMO/ServerConnection did not SqlCredential-based connections correctly. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/33698941
  - [New in Preview 6] Fixed an issue where an application written using SMO would encounter an error if it tried to enumerate databases from the same server on multiple threads even though it uses separate SqlConnection instances on each thread.

- **AS**
  - Fixed an issue where the "Advanced Settings" to the AS Xevent UI was clipped
  - [New in Preview 5] Fixed an issue where DAX parsing throws file not found exception
  - [New in Preview 5] Added back shortcut to "Deployment Wizard" to Start Menu

- **IS**
  - [New in Preview 5] Fixed a SxS issue that deployment wizard will fail to connect to sql server when SQL Server 2019 and SSMS 18.0 are installed on the same machine.
  - [New in Preview 5] Fixed an issue that maintenance plan task can’t be edited when designing the maintenance plan.
  - [New in Preview 5] Fixed an issue that deployment wizard will stuck if the project under deployment is renamed.
  - [New in Preview 5] Enabled environment setting in Azure-SSIS IR schedule feature.

- **Flat File Import Wizard**
  - Fixed issue where Flat File Import does not allow changing destination table when table is already existing, For details, see (https://feedback.azure.com/forums/908035-sql-server/suggestions/32896186)
  - Fixed an issue where the "Import Flat File Wizard" was not handling double quotes correctly (escaping) (For details, see https://feedback.azure.com/forums/908035/suggestions/32897998)
  - Fixed an issue where related to incorrect handling of floating-point types (on locales that use a different delimiter for floating points)
  - Fixed an issue related to importing of bits when values are 0 or 1. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32898535
  - Fixed an issue where floats were entered as nulls. 

- **HADR / AG**
  - [New in Preview 5] Fixed an issue where roles in "Fail Over Availability Groups" wizard was always displayed as "Resolving" 
  - [New in Preview 5] Fixed an issue where SSMS was showing truncated warnings in "AG Dashboard".

- **Data Classification** 
  - Fixed a setup issue what was causing the recommendation part of Data Classification not to work with fresh install.
  - [New in Preview 6] Fixed an issue when saving classifications in the data classification pane while there are another data classification panes open on other databases.

- **Backup/Restore/Attach/Detach DB**
  - Fixed an issue where the user was unable to attach a database when physical filename of .mdf file does not match the original filename
  - Fixed an issue where SSMS might not find a valid restore plan or might find one which is sub-optimal. For details, see https://feedback.azure.com/forums/908035-sql-server/suggestions/32897752.
  - Fixed a crash in SSMS when trying to restore a URL backup (this was a regression introduced in previous previews.)
  - [New in Preview 5] Fixed issue where the "Attach Database" wizard was not displaying secondary files that were renamed. Now, the file is displayed and a comment about it is is added (e.g. "Not Found"). For details, see https://feedback.azure.com/forums/908035/suggestions/32897434

- **Job Activity Monitor**
  - Fixed crash while using Job Activity Monitor (with filters)

- **Managed Instance support**
  - Improved/polished the support for Managed Instances: disabled unsupported options in UI and and a fix to the View Audit Logs option to handle URL audit target.
  - "Generate and Publish scripts" wizard scripts unsupported CREATE DATABASE clauses
  - Live Query Statistics was disabled for CL instances
  - Database properties->Files was incorrectly scripting ALTER DB ADD FILE
  - Fixed regression with SQL Agent scheduler where ONIDLE scheduling was chosen even when some other scheduling type was chosen
  - Adjusting MAXTRANSFERRATE, MAXBLOCKSIZE for doing backups on Azure Storage
  - The issue where tail log backup is scripted before RESTORE operation (this is not supported on CL).
  - Create database wizard not scripting correctly CREATE DATABASE statement
  - Fixed an issue where an error was displayed while trying to use "Activity Monitor" when connected to Managed Instances.
  - [New in Preview 5] Improved support for AAD Logins (in SSMS Explorer).
  - [New in Preview 5] Improved scripting of SMO Filegroups objects.
  - [New in Preview 5] Improved UI for credentials and audits.
  - [New in Preview 5] Added support for Logical Replication.

- **Azure SQL Database**
  - Fixed an issue where the database list was not populated correctly for Azure SQL Db query window when connected to a user database in Azure SQL DB instead of to master.
  - Fixed an issue where it was not possible to add a "Temporal Table" to a SQL Azure DB.
  - [New in Preview 6] Enabled the Statistics properties sub menu option under menu Statistics in Azure, since it has been fully supported for quite some time now.
  - Fixed issues in common Azure UI control that was preventing the user from displaying Azure subscriptions (if there were more than 50). Also, the sorting has been changed to be by name rather by Subscription ID. The user could run into this one when trying to restore a backup from URL, for example.
  - Fixed an issue in common Azure UI control when enumerating subscriptions which could yield a "Index was out of range. Must be non-negative and less than the size of the collection." error when the user had no subscriptions in some tenants. The user could run into this one when trying to restore a backup from URL, for example.

- **Query Data Store**
  - [New in Preview 6] Fixed an issue where a "DocumentFrame (SQLEditors)" exception could be thrown.
  - [New in Preview 6] Fixed an issue when trying to set a custom time interval in the build-in Query Store reports the user was not able to select AM or PM on the start/end interval

- **Result Grid**
  - Fixed an issue that was causing the in High Contrast mode (selected line numbers not visible).

- **XEvent Profiler**
  - Fixed an issue where XEvent Profiler failed to launch when connected to a 96-core SQL Server.

- **DAC Import Wizard**
  - [New in Preview 5] Fixed an issue DAC Import Wizard was not working when connected using AAD.

- **XEvent Viewer**
  - [New in Preview 5] Fixed an issue where XEvent Viewer was crashing when trying to group the events using the "Extended Event Toolbar Options"

- **Vulnerability Assessment**
  - [New in Preview 5] Fixed an issue where the scan results are not being loaded properly.

- **Copy Database Wizard**
  - [New in Preview 6] Generate scripts/Transfer/Copy Database Wizard try to create a table with an in memory table doesn't force ansi_padding on
  - [New in Preview 6] Transfer Database task/Copy Database Wizard broken on SQL 2017 and SQL 2019
  - [New in Preview 6] Generate scripts/Transfer/Copy Database Wizard script table creation before creation of associated external data source

- **Profiler**
  - [New in Preview 6] Added "Aggregate Table Rewrite Query" event to Profiler events.

- **ShowPlan**
  - [New in Preview 6] New mem grant operator properties display incorrectly when there is more than one thread.

### Deprecated Features

The following features are no longer available in SSMS:

- T-SQL Debugger
- Database Diagrams
- OSQL.EXE
- Dreplay Admin UI
- Configuration Manager tools:
  - Both SQL Server  Configuration Manager and Reporting Server Configuration Manager are not part of SSMS setup anymore.

- DMF Standard Policies
  - The policies are not installed with SSMS anymore. They are moved to Git. Users are able to contribute and download/install them, if they want to.

- SSMS command-line option -P removed
  - Due to security concerns, the option to specify clear-text passwords on the command line was removed.

- Generate Scripts | Publish to Web Service removed. This (deprecated) feature was removed from the SSMS UI.

- Removed node "Maintenance | Legacy" in Object Explorer. In Generate and Publish Scripts | Publish to Web Service option is removed. The *old* "Database Maintenance Plan" and "SQL Mail" nodes won't be accessible anymore. The modern "Database Mail" and "Maintenance Plans" nodes continue to work as usual.

### Known issues

- Double-clicking on a .sql file launches SSMS, but does not open the actual script.
  - Workaround: drag and drop the .sql file onto the SSMS editor.


## SSMS 17.9.1 (latest GA release)

![download](../ssdt/media/download.png) [SSMS 17.9.1](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x409)

- Release number: 17.9.1<br>
- Build number: 14.0.17289.0<br>
- Release date: November 21, 2018

17.9.1 is a small update to 17.9 with the following bug fixes:

- Fixed an issue where users may experience their connection being closed and reopened with each query invocation when using "Active Directory - Universal with MFA Support" authentication with the SQL query editor. Side effects of the connection closing included global temporary tables being dropped unexpectedly, and sometimes a new SPID given to the connection.
- Fixed a long outstanding issue where restore plan would fail to find a restore plan, or would generate an inefficient restore plan under certain conditions.
- Fixed an issue in the "Import Data-tier Application" wizard which could result in an error when connected to an Azure SQL database.



> [!NOTE]
> Non-English localized releases of SSMS 17.x require the [KB 2862966 security update package](https://support.microsoft.com/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2043154&clcid=0x40a)








## SSMS 17.9

![download](../ssdt/media/download.png) [SSMS 17.9](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x409)

Build number: 14.0.17285.0<br>
Release date: September 04, 2018

> [!NOTE]
> Non-English localized releases of SSMS 17.x require the [KB 2862966 security update package](https://support.microsoft.com/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2014306&clcid=0x40a)


### What's new

**General SSMS**


ShowPlan:

- Graphical Showplan now shows the new row mode memory grant feedback attributes when the feature is activated for a specific plan: IsMemoryGrantFeedbackAdjusted and LastRequestedMemory added to the MemoryGrantInfo query plan XML element. For more on row mode memory grant feedback, For details, see [Adaptive query processing in SQL databases](https://docs.microsoft.com/sql/relational-databases/performance/adaptive-query-processing).

Azure SQL: 

- Added support for vCore SKUs in Azure DB creation. For more information, For details, see [vCore-based purchasing model](https://docs.microsoft.com/azure/sql-database/sql-database-service-tiers#vcore-based-purchasing-model).
 

### Bug fixes

**General SSMS**
	
Replication Monitor:

- Fixed an issue that was causing Replication Monitor (SqlMonitor.exe) not to start (User Voice item: https://feedback.azure.com/forums/908035-sql-server/suggestions/34791079).

Import Flat File Wizard:

- Fixed the link to the help page for "Flat File Wizard" dialog 
- Fixed issue where the wizard did not allow changing the destination table when the table already existed: this allows users to retry without having to exit the wizard, delete the failed table, and then reenter the information into the wizard (User Voice item: https://feedback.azure.com/forums/908035-sql-server/suggestions/32896186).

Import/Export Data-Tier Application:

- Fixed an issue (in DacFx) which was causing the import of a .bacpac could fail with a message like "Error SQL72014: .Net SqlClient Data Provider: Msg 9108, Level 16, State 10, Line 1 This type of statistics is not supported to be incremental. " when dealing with tables with partitions defined and no indexes on the table.

Intellisense:

- Fixed an issue where intellisense completion was not working when using AAD with MFA.

Object Explorer:

- Fixed an issue where the "Filter Dialog" was displayed on random monitors instead of the monitor where SSMS was running (multi-monitor systems).

Azure SQL:

- Fixed an issue related to enumeration of databases in the "Available Databases" where "master" was not displayed in the dropdown when connected to a specific database.
- Fixed an issue where trying to generate a script ("Data" or "Schema and Data") was failing then connected to the Azure SQL Database using AAD with MFA.
- Fixed an issue in the View Designer (Views) where it was not possible to select "Add Tables" from the UI when connected to a Azure SQL Database.
- Fixed an issue where SSMS Query Editor was silently closing and reopening connections during MFA token renewal. This prevents side effects unbeknownst to the user (like closing a transaction and never reopening again) from happening. The change adds the token expiration time to the properties window. 
- Fixed an issue where SSMS was not enforcing password prompts for imported MSA accounts for AAD with MFA login. 

Activity Monitor: 

- Fixed an issue that was causing "Live Query Statistics" to hang when launched from Activity Monitor and SQL Authentication was used. 

Microsoft Azure integration: 

- Fixed an issue where SSMS only shows the first 50 subscriptions (Always Encrypted dialogs, Backup/Restore from URL dialogs, etc...). 
- Fixed an issue where SSMS was throwing an exception ("Index out of range") while trying to sign in to a Microsoft Azure account that did not have any storage account (in Restore Backup from URL dialog). 

Object Scripting: 

- When scripting "Drop and Create", SSMS now avoids generating dynamic T-SQL.
- When scripting a database object, SSMS now does not generate script to set database scoped configurations, if they are set to default values.

Help:

- Fixed a long outstanding issue where "Help on Help" was not honoring the online/offline mode.
- When clicking on "Help | Community Projects and Samples" SSMS now opens the default browser that points to a Git page and shows no errors/warnings due to old browser being used.

### Known issues


> [!IMPORTANT]
> When using *Active Directory - Universal with MFA Support* authentication with the SQL query editor, users may experience their connection being closed and reopened with each query invocation. Side effects of such closure include global temporary tables being dropped unexpectedly and sometimes a new SPID being given to the connection. This closure will not occur if there is an open transaction on the connection. To work around this issue, users can set `persist security info=true` in the connection parameters.




## Previous SSMS releases

Download previous SSMS versions by clicking the title links in the following sections:

## ![download](../ssdt/media/download.png) [SSMS 17.8.1](https://go.microsoft.com/fwlink/?linkid=875802)
*A bug was discovered in 17.8 related to provisioning SQL databases, so SSMS 17.8.1 replaces 17.8.*

Build number: 14.0.17277.0<br>
Release date: June 26, 2018

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x40a)


### What's new

**General SSMS**

Database Properties:

- This improvement exposes the **AUTOGROW_ALL_FILES** configuration option for Filegroups. This new config option is added under the Database Properties > Filegroups window in the form of a new column (Autogrow All Files) of checkboxes for each available Filegroup (except for Filestream and Memory Optimized Filegroups). The user can enable/disable AUTOGROW_ALL_FILES for a particular Filegroup by toggling the corresponding Autogrow_All_Files checkbox. Correspondingly, the **AUTOGROW_ALL_FILES** option is properly scripted when scripting the database for CREATE / generating scripts for the database (SQL2016 and above).
	
SQL Editor:

- Improved experience with Intellisense in Azure SQL Database when the user doesn't have master access.

Scripting:

- General performance improvements, especially over high-latency connections.
	
**Analysis Services (AS)**

- Analysis Services client libraries and data providers updated to the latest version, which added support for the new Azure Government AAD authority (login.microsoftonline.us).



### Bug fixes

**General SSMS**
	
Maintenance Plans:

- Fixed an issue when editing maintenance plans with Sql Authentication where "Notify Operator Task" was failing when using SQL authentication.
	
Scripting:

- Fixed an issue where PostProcess actions in SMO lead to resource exhaustion and SQL login failures
	
SMO:

- Fixed an issue where Table.Alter() fails if adding a column with a default constraint and the table already has data. For details, For details, see [sql server smo generating inline default constraint when adding a column to a table containing data](https://feedback.azure.com/forums/908035-sql-server/suggestions/32895625).
	
Always Encrypted:

- Fixed an issue (in DacFx) which was causing a lock timeout error when enabling Always Encrypted on a partitioned table
	

**Analysis Services (AS)**

- Fixed an issue that occurred when modifying an OAuth datasource in a Tabular Analysis Services 1400-level compatibility model, which caused the changes in the OAuth tokens to not get updated in the data source.
- Fixed a crash in SSMS that may have occurred when using some invalid data source credentials or editing data sources that didn't support Change Data Source migration in Power Query (for example, Oracle) in Analysis Services Tabular 1400-level compatibility models.


### Known issues

- Clicking the *Script* button after modifying any filegroup property in the *Properties* window, generates two scripts - one script with a *USE <database>* statement, and a second script with a *USE master* statement.  The script with *USE master* is generated in error and should be discarded. Run the script that contains the *USE <database>* statement.
- Some dialogs display an invalid edition error when working with new *General Purpose* or *Business Critical* Azure SQL Database editions.
- Some latency in XEvents viewer may be observed. This is a [known issue in the .Net Framework](https://github.com/Microsoft/dotnet/blob/master/releases/net472/dotnet472-changes.md#sql). Consider upgrading to NetFx 4.7.2.




## ![download](../ssdt/media/download.png) [SSMS 17.7](https://go.microsoft.com/fwlink/?linkid=873126)

Build number: 14.0.17254.0<br>
Release date: May 09, 2018

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=873126&clcid=0x40a)


### What's new

**General SSMS**

Replication Monitor:   
- Replication monitor now supports registering a listener for scenarios where publisher database and/or distributor database is part of Availability Group. You can now monitor replication environments where publisher database and/or distribution database is part of Always On. 
 
Azure SQL Data Warehouse: 
- Add Rejected Row Location support for External Tables in Azure SQL Data Warehouse. 

**Integration Services (IS)**

- Added a scheduling feature for SSIS packages deployed to Azure SQL Database. Unlike SQL Server on premises and SQL Database Managed Instance, which have SQL Server Agent as a first-class job scheduler, SQL Database does not have a built-in scheduler. This new SSMS feature provides a familiar user interface that's similar to SQL Server Agent for scheduling packages deployed to SQL Database. If you're using SQL Database to host the SSIS catalog database, SSISDB, you can use this SSMS feature to generate the Data Factory pipelines, activities, and triggers required to schedule SSIS packages. You can then edit and extend these objects in Data Factory. For more info, For details, see [Schedule SSIS package execution on Azure SQL Database with SSMS](../integration-services/lift-shift/ssis-azure-schedule-packages-ssms.md). To learn more about Azure Data Factory pipelines, activities, and triggers, For details, see [Pipelines and activities in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-pipelines-activities) and [Pipeline execution and triggers in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-pipeline-execution-triggers).
- Support for SSIS package scheduling in SQL Agent on SQL Managed instance. It is now possible to create SQL Agent jobs to execute SSIS packages on the managed instance. 

### Bug fixes

**General SSMS** 

Maintenance Plan:   
- Fixed an issue where trying to change the schedule of an existing Maintenance Plan was throwing an exception. For details, For details, see [SSMS 17.6 crashes when clicking on a schedule in a maintenance plan](https://feedback.azure.com/forums/908035-sql-server/suggestions/33712924).

Always On: 
- Fixed an issue where Always On Latency Dashboard was not working with SQL Server 2012.
 
Scripting: 
- Fixed an issue where scripting stored procedure against Azure SQL Data Warehouse, is not working for non-admin user.
- Fixed an issue where scripting a database against Azure SQL Database was not scripting the *SCOPED CONFIGURATION* properties.
 
Telemetry: 
- Fixed issue where  SSMS crashes then trying to connect to a server, after opting out of sending telemetry.
 
Azure SQL Database: 
- Fixed an issue  where the user was not able to set or change compatibility level (the drop-down from empty). Note: in order to set the compatibility level to 150, the user still needs to use the *Script* button and manually edit the script. 
 
SMO: 
- Exposed Error Log Size setting in SMO. For details, For details, see [Set the Maximum Size of the SQL Server Error Logs](https://feedback.azure.com/forums/908035-sql-server/suggestions/33624115).  
- Fix linefeed scripting in SMO on Linux.
- Miscellaneous perf improvement when retrieving rarely used properties.  

Intellisense: 
- Perf improvement: reduced volume of intellisense queries for column data. This is especially beneficial when working on tables with huge number of columns. 

SSMS User settings:
- Fixed an issue where the options page was not resizing properly.

Misc:  
- Improved how text is displayed on *Statistics details* page. 

**Integration Services (IS)**

- Better support for Azure SQL Database Managed Instance.
- Fixed an issue where the user was unable to create a catalog for SQL Server 2014 or before.
- Fixed two issues with reports:
   - Removed the machine name for Azure servers.
   - Improved handling of localized object name.


### Known issues

Some dialogs display an invalid edition error when working with new *General Purpose* or *Business Critical* Azure SQL Database editions.

## ![download](../ssdt/media/download.png) [SSMS 17.6](https://go.microsoft.com/fwlink/?linkid=870039)

Build number: 14.0.17230.0<br>
Release date: March 20, 2018

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x40a)

### What's new

**General SSMS**

SQL Database Managed Instance:

- Added a support for [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance). Azure SQL Database Managed Instance provides near 100% compatibility with SQL Server on-premises, a native [virtual network (VNet)](https://docs.microsoft.com/azure/virtual-network/virtual-networks-overview) implementation that addresses common security concerns, and a [business model](https://azure.microsoft.com/pricing/details/sql-database/) favorable for on-premises SQL Server customers.
- Support for common management scenarios like:
   - Create and alter databases.
   - Back up and restore databases.
   - Importing, exporting, extracting, and publishing Data-tier Applications.
   - Viewing and altering Server properties.
   - Full Object Explorer support.
   - Scripting database objects.
   - Support for SQL Agent jobs.
   - Support for Linked Servers.
- Learn more about Managed Instances [here](https://azure.microsoft.com/blog/migrate-your-databases-to-a-fully-managed-service-with-azure-sql-database-managed-instance/).

Object Explorer:
- Added settings to not force brackets around names when dragging & dropping from Object Explorer to Query Window. (User suggestions [32911933](https://feedback.azure.com/forums/908035-sql-server/suggestions/32911933), and [32671051](https://feedback.azure.com/forums/908035-sql-server/suggestions/32671051).)

Data Classification:
- General improvements and bug fixes.

**Integration Services (IS)**

- Added support to deploy packages to a [SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance).

### Bug fixes

**General SSMS**

Data Classification:

- Fixed an issue in *Data Classification that was causing newly added classifications to be displayed with stale *information type* and *sensitivity label*.
- Fixed an issue where *Data Classification* was not working when targeting a server set to a case-sensitive collation.
		
Always On:

- Fixed an issue in AG Show Dashboard where clicking on *Collect Latency Data* could result in an error when the server was set to a case-sensitive collation.
- Fixed an issue where SSMS was incorrectly reporting an AG as *Distributed* when the Cluster service shuts down.
- Fixed an issue when creating AG using *Create Availability Group* dialog the *ReadOnlyRoutingUrl* is required.
- Fixed an issue when the primary is down and manually failover to secondary, a NullReferenceException is thrown.
- Fixed an issue when creating Availability Group using backup/restore to initialize a database, on the secondary replicas, the database files are created in the default directory. The fix includes:
   - Add the data/log directory validator.
   - Only do the file relocation when the replica is on a different OS to the primary replica.
- Fixed an issue where SSMS wizard doesn't generate *CLUSTER_TYPE* option, causing secondary join to fail.

Setup:
- Fixed issue where trying to upgrade SSMS by installing the "upgrade package" was failing when SSMS was installed in a non-default location.

SMO:
- Fixed performance issue where scripting tables on SQL Server 2016 and above could take up to 30 seconds (now, it's down to less than 1 second).

Object Explorer:
- Fixed an issue where SSMS could throw an exception like "Object cannot be cast from DBNull to other types" when trying to expand *Management* node in Object Explorer.
- Fixed an issue where *Start PowerShell* was not detecting the SQLServer module when user-defined PS profile emitted output.
- Fixed an intermittent hang that could occur when right-clicking a Table or Index node in Object Explorer.

Database Mail:
- Fixed an issue where *Database Mail Configuration Wizard* was throwing an exception when trying to display/manage more than 16 profiles.


**Analysis Services (AS)**

- Fixed as issue where modifying a data source on a 1400 compatibility level model in SSMS the changes are not saved to the server.

**Integration Services (IS)**

- Fixed an issue where SSMS did not show SSIS catalog node and reports when connected to SQL Database Managed Instance

### Known issues

> [!WARNING]
> There is a known issue where SSMS 17.6 becomes unstable and crashes when using [Maintenance Plans](../relational-databases/maintenance-plans/maintenance-plans.md). If you use Maintenance Plans, do not install SSMS 17.6. Downgrade to SSMS 17.5 if you already installed 17.6 and this issue is affecting you. 



## ![download](../ssdt/media/download.png) [SSMS 17.5](https://go.microsoft.com/fwlink/?linkid=867670)
Generally available | Build number: 14.0.17224.0

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x40a)

### What's new

**General SSMS**

Data Discovery & Classification:

- Added a new SQL Data Discovery & Classification feature for discovering, classifying, labeling & reporting sensitive data in your databases. 
- Auto-discovering and classifying your most sensitive data (business, financial, healthcare, personal data, etc.) can play a pivotal role in your organizational information protection stature.
- Learn more at [SQL Data Discovery & Classification](../relational-databases/security/sql-data-discovery-and-classification.md).

Query Editor:

- Added support for SkipRows option to the Delimited Text External File Format for Azure SQL DW. This capability allows users to skip a specified number of rows when loading delimited text files into SQL DW. Also added the corresponding intellisense/SMO support for the FIRST_ROW keyword. 

Showplan:

- Enabled display of estimated plan button for SQL Data Warehouse
- Added new showplan attribute *EstimateRowsWithoutRowGoal*; and added new showplan attributes to *QueryTimeStats*: *UdfCpuTime* and *UdfElapsedTime*. For more information, For details, see [Optimizer row goal information in query execution plan added in SQL Server 2017 CU3](https://support.microsoft.com/help/4051361).



### Bug fixes

**General SSMS**

Showplan:

- Fixed Live Query Statistics elapsed time, to show engine execution time instead of time elapsed for LQS connection.
- Fixed an issue where showplan was not able to recognize Apply logical operators like GbApply and InnerApply.
- Fixed an issue related to ExchangeSpill.

Query Editor:

- Fixed on issue related to SPIDs where SSMS could throw an error like "Input string was not in a correct format. (mscorlib)" when executing a simple query preceded by a "SET SHOWPLAN_ALL ON". 


SMO:

- Fixed an issue where SMO was not able to fetch AvailabilityReplica properties in case the server collation happened to be case-sensitive (as a result, SSMS could display an error message like "The multi-part identifier "a.delimited" could not be bound."
- Fixed an issue in DatabaseScopedConfigurationCollection class, where incorrectly handling collations (as a result, an SSMS running on an ma machine with a Turkish locale could display an error like "legacy cardinality estimation is not valid scoped configuration" when right-clicking on a database running on a server with a case-sensitive collation).
- Fixed an issue in JobServer class, where SMO was not able to fetch SQL Agent properties on a SQL 2005 server (as a result, SSMS was throwing an error like "Cannot assign a default value to a local variable. Must declare the scalar variable "\@ServiceStartMode" and, ultimately, was not displaying the SQL Agent node in Object Explorer).

Templates: 

- "Database Mail": fixed a couple of typos [(https://feedback.azure.com/forums/908035/suggestions/33143512)](https://feedback.azure.com/forums/908035/suggestions/33143512).  

Object Explorer:
- Fixed an issue where Managed Compression would fail for indexes (https://feedback.azure.com/forums/908035-sql-server/suggestions/32610058-ssms-17-4-error-when-enabling-page-compression-o).

Auditing:
- Fixed an issue with the *Merge Audit Files* feature.
<br>

### Known issues

Data classification:
- Removing a classification and then manually adding a new classification for the same column results in the old information type and sensitivity label being assigned to the column in the main view.<br>
*Workaround*: Assign the new information type and sensitivity label after the classification was added back to the main view and before saving.


## ![download](../ssdt/media/download.png) [SSMS 17.4](https://go.microsoft.com/fwlink/?linkid=864329)
Generally available | Build number: 14.0.17213.0

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x40a)


### What's new

**General SSMS**

Vulnerability Assessment:
- Added a new SQL Vulnerability Assessment service to scan your databases for potential vulnerabilities and deviations from best practices, such as misconfigurations, excessive permissions, and exposed sensitive data. 
- Results of the assessment include actionable steps to resolve each issue and customized remediation scripts where applicable. The assessment report can be customized for each environment and tailored to specific requirements. Learn more at [SQL Vulnerability Assessment](https://docs.microsoft.com/sql/relational-databases/security/sql-vulnerability-assessment).

SMO:
- Fixed issue where *HasMemoryOptimizedObjects were throwing exception on Azure.
- Added support for new CATALOG_COLLATION feature.

Always On Dashboard:
- Improvements for latency analysis in Availability Groups.
- Added two new reports: *AlwaysOn\_Latency\_Primary* and *AlwaysOn\_Latency\_Secondary*.

Showplan:
- Updated links to point to correct documentation.
- Allow single plan analysis directly from actual plan produced.
- New set of icons.
- Added support for recognize "Apply logical operators" like GbApply, InnerApply.
		
XE Profiler:
- Renamed to XEvent Profiler.
- Stop/Start menu commands now stop/start the session by default.
- Enabled keyboard shortcuts (for example, CTRL-F to search).
- Added database\_name and client\_hostname actions to appropriate events in XEvent Profiler sessions. For the change to take effect, you may need to delete existing QuickSessionStandard or QuickSessionTSQL session instances on the servers - [Connect 3142981](https://connect.microsoft.com/SQLServer/feedback/details/3142981)

Command line:
- Added a new command-line option ("-G") that can be used to automatically have SSMS connect to a server/database using Active Directory Authentication (either 'Integrated' or 'Password'). For details, For details, see [Ssms utility](ssms-utility.md).

Import Flat File Wizard:
- Added a way to pick a schema name other than the default ("dbo") when creating the table.

Query Store:
- Restored the "Regressed Queries" report when expanding the Query Store available reports list.

**Integration Services (IS)**
- Added package validation function in Deployment Wizard, which helps the user figure out components inside SSIS packages that are not supported in Azure-SSIS IR.

### Bug fixes

**General SSMS**

- Object Explorer:
Fixed an issue where Table-Valued Function node was not showing up for database snapshots - [Connect 3140161](https://connect.microsoft.com/SQLServer/feedback/details/3140161).
Improved performance when expanding *Databases* node when the server has autoclose databases.
- Query Editor:
Fixed an issue where IntelliSense was failing for users that don't have access to the master database.
Fixed an issue that was causing SSMS to crash in some cases when the connection to a remote machine was closed - [Connect 3142557](https://connect.microsoft.com/SQLServer/feedback/details/3142557).
- XEvent Viewer:
Re-enabled functionality to export to XEL.
Fixed issues where in some cases the user was not able to load an entire XEL file.
- XEvent Profiler:
Fixed an issue that was causing SSMS to crash when the user did not have *VIEW SERVER STATE* permissions.
Fixed an issue where closing the XE Profiler Live Data window did not stop the underlying session.
- Registered Servers:
Fixed an issue where the "Move To..." command stopped working - [Connect 3142862](https://connect.microsoft.com/SQLServer/feedback/details/3142862) and [Connect 3144359](https://connect.microsoft.com/SQLServer/feedback/details/3144359/).
- SMO:
Fixed an issue where the TransferData method on the Transfer object was not working.
Fixed an issue where Server databases throws exception for paused SQL DW databases.
Fixed an issue where scripting SQL database against SQL Data Warehouse generated incorrect T-SQL parameter values.
Fixed an issue where scripting of a stretched DB incorrectly emitting the *DATA\_COMPRESSION* option.
- Job Activity Monitor:
Fixed an issue where the user was getting an "Index was out of range. Must be non-negative and less than the size of the collection. 
		Parameter name: index (System.Windows.Forms)" error when trying to filter by Category - [Connect 3138691](https://connect.microsoft.com/SQLServer/feedback/details/3138691).
- Connection Dialog:
Fixed an issue where domain users without access to a Read/Write domain controller could not sign in to a SQL Server using SQL Authentication - [Connect 2373381](https://connect.microsoft.com/SQLServer/feedback/details/2373381).
- Replication:
Fixed an issue where an error similar to "Cannot apply value 'null' to property ServerInstance" was displayed when looking at properties of a pull subscription in SQL Server.
- SSMS Setup:
Fixed an issue where SSMS setup was incorrectly causing all the installed products on the machine to be reconfigured.
- User Settings:
   - With this fix, Azure Government users have uninterrupted access to their Azure SQL Database and Azure Resource Manager resources with SSMS via Universal authentication and Azure Active Directory login.  Users of prior versions of SSMS would need to open Tools|Options|Azure Services and under Resource Management change the configuration of the "Active Directory Authority" property to https://login.microsoftonline.us.

**Analysis Services (AS)**

- Profiler: fixed an issue when trying to connect using Window Authentication against Azure AS.
- Fixed an issue that could cause a crash when canceling connection details on a 1400 model.
- When setting an Azure blob key in the connection properties dialog when refreshing credentials, it will now be visually masked.
- Fixed an issue in the Azure Analysis Services User selection dialog to show the Application ID guid instead of the Object ID when searching.
- Fixed an issue in the Browse Database\MDX query designer toolbar that caused the icons to be incorrectly mapped for some buttons.
- Fixed an issue that prevented connecting to SSAS using msmdpump IIS http/https addresses.
- Several strings in the Azure Analysis Services User Picker dialog have now been translated for additional languages.
- MaxConnections property is now visible for data sources in tabular models.
- Deployment Wizard will now generate correct JSON definitions for Azure AS role members.
- Fixed an issue in SQL Profiler where selecting Windows Authentication against Azure AS would still prompt for login.



## ![download](../ssdt/media/download.png) [SSMS 17.3](https://go.microsoft.com/fwlink/?linkid=858904)
Generally available | Build number: 14.0.17199.0

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x40a)


### Enhancements

- New "Import Flat File" wizard added to streamline the import experience of CSV files with an intelligent framework, requiring minimal user intervention, or specialized domain knowledge. For details, For details, see [Import Flat File to SQL Wizard](../relational-databases/import-export/import-flat-file-wizard.md).
- Added "XEvent Profiler" node to Object Explorer. For details, For details, see [Use the SSMS XEvent Profiler](../relational-databases/extended-events/use-the-ssms-xe-profiler.md).
- Updated waits filtering and categorization in Performance Dashboard historical waits report.
- Added the syntax check of the "Predict" function.
- Added the syntax check of the External Library Management queries.
- Added SMO support for External Library Management.
- Added "Start PowerShell" support to "Registered Servers" window (requires a new SQL PowerShell module).
- Always On: added [read-only routing support](../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md) for availability groups.
- Added an option to send tracing details to the Output Window for "Active Directory - Universal with MFA support" logins (off by default; needs to be turned on in user settings under "Tools > Options > Azure Services > Azure Cloud > ADAL Output Window Trace Level"). 
- Query Store: 
  - Query Store UI will be accessible even when QDS is OFF as long as QDS have recorded any data.
  - Query Store UI now exposes waits categorization in all the existing reports. This lets customers unlock the scenarios of Top Waiting Queries and many more.
- Made inclusion of the scripting parameters headers optional (off by default;  can be enabled in user settings under "Tools > Options > SQL Server Object Explorer > Scripting > Include scripting parameters header") - [Connect item 3139199](https://connect.microsoft.com/SQLServer/feedback/details/3139199).
- Removed "RC" branding.

### Bug Fixes

**General SSMS**

- XEvent: 
   - Fixed issue where SSMS opens only part of the events in .xel file.
   - Improved "Watch Live Data" experience when default database is not 'master' - [Connect item 1222582](https://connect.microsoft.com/SQLServer/feedback/details/1222582).
- Always On: Fixed issue where "Restore log backups" may fail with error "The sign in this backup set terminates at LSN x, which is too early to apply to the database".
- Job Activity Monitor: fixed inconsistent icons - [Connect item 3133100](https://connect.microsoft.com/SQLServer/feedback/details/3133100).
- Query Store: Fixed Issue where user cannot choose "custom" date range for Query Store reports. Linked to below connect items.
   - [Connect item 3139842](https://connect.microsoft.com/SQLServer/feedback/details/3139842)
   - [Connect item 3139399](https://connect.microsoft.com/SQLServer/feedback/details/3139399)
- Fixed issue where connection dialog doesn't "clear" the most recently used database when saved info has named database and user selects default.
- Object Scripting:
Fixed an issue where "Generate database script" not working and throwing an error when the user has a paused DW database on the server, but selected another non-DW database and tried t script it.
Fixed issue where the header for scripted Stored Procedures was not matching the script settings, resulting in a misleading script - 
		[Connect item 3139784](https://connect.microsoft.com/SQLServer/feedback/details/3139784).
Re-enabled the "Script button" when targeting SQL Azure objects.
Fixed issue where SSMS was not allowing scripting for "Alter" or "Execute" on some objects (UDF, View, SP, Trigger) when connected to an Azure SQL database - 
		[Connect item 3136386](https://connect.microsoft.com/SQLServer/feedback/details/3136386).
- Query editor:
  - Improved intellisense when targeting Azure SQL databases.
  - Fixed an issue where queries failed due to an expired authentication token (Universal Authentication).
  - Improved intellisense when working against Azure SQL databases (particularly, when connecting to Azure SQL Database, the latest T-SQL grammar (140) will be used).
  - Fixed issue where open a query window with a connection to a non-DataWarehouse database on a server would cause all subsequent query windows for that server to DataWarehouse databases to throw various errors about unsupported types/options.
- Always On:
   - Added For details, seeding mode column to Always On dashboard and AG properties page.
   - Fixed issue where it was not possible to create a Linux AG when primary is on Windows - [Connect item 3139856](https://connect.microsoft.com/SQLServer/feedback/details/3139856).
- Fixed several "Out of Memory" issues in SSMS when running queries - 
	[Connect item 2845190](https://connect.microsoft.com/SQLServer/feedback/details/2845190), 
	[Connect item 3123864](https://connect.microsoft.com/SQLServer/feedback/details/3123864).
- Profiler: 
   - Fixed issue where Profiler was not working when targeting SQL 2005.
   - Fixed issue where Profiler was not honoring the "trust server certificate" connection option.
- Activity Monitor: fixed an issue where Activity Monitor does not work when pointed at SQL Server running on Linux.
- Fixed an issue with the SMO Transfer class where it wouldn't transfer External Data Source or External File Format objects, objects of those types should now correctly be included in the transfer.
- Registered Servers:
   - Enabled multiserver query for UA servers (it tries to use the same token for every UA server in the group).
- AD Universal Authentication:
   - Fixed issue where Azure AD authentication was not supported.
   - Fixed issue where table/view designer was not working.
   - Fixed issue where "Select Top 1000 rows" and "Edit Top 200 rows" were not working.
- Database restore: fixed an issue where restore omits the last folder in the path when moving files to an alternate location.
- Compress wizard:
   - Fixed an issue with manage compression wizard for indexes; fixed issue where compress data wizards were broken for SQL 2016 and lower.
		https://connect.microsoft.com/SQLServer/feedback/details/3139342
   - Added Compress wizard to Azure tables and indexes.
- Showplan: 
   - Fixed issue where PDW operators were not recognized.
- Server Properties:
   - Fixed issue with not being able to modify server processor affinity.


**Analysis Services (AS)**

- Fixed a number of issues with Deployment Wizard to support tabular 1400 compat-level models and Power Query data sources.
- Deployment Wizard can now deploy to AS Azure when running from command line.
- When using Windows Auth in AS Azure the user will now For details, see the name of the user account in Object Explorer correctly.


### Known issues in this 17.3 release:

**General SSMS**

- The following SSMS functionality is not supported for Azure AD auth using UA with MFA:
   - Database Engine Tuning Advisor is not supported for Azure AD auth; there is a known issue where the error message presented to the user is a bit cryptic "Could not load file or assembly 'Microsoft.IdentityModel.Clients.ActiveDirectory,..." instead of the expected "Database Engine Tuning Advisor does not support Microsoft Azure SQL Database. (DTAClient)".
- Trying to analyze a query in DTA results in an error: "Object must implement IConvertible. (mscorlib)".
- *Regressed Queries* is missing from the Query Store list of reports in Object Explorer.
   - Workaround: Right-click the **Query Store** node and select **View Regressed Queries**.

**Integration Services (IS)**

- The [execution_path] in [catalog].[event_messagea] is not correct for package executions in Scale Out. The [execution_path] starts with "\Package" instead of the object name of the package executable. When viewing the overview report of package executions in SSMS, the link of "Execution Path" in Execution Overview cannot work. The workaround is to click "View Messages" on overview report to check all event messages.


## ![download](../ssdt/media/download.png) [SSMS 17.2](https://go.microsoft.com/fwlink/?linkid=854085)
Generally available | Build number: 14.0.17177.0

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=854085&clcid=0x40a)

### Enhancements

- Multi-Factor Authentication (MFA)
  - Multiple-user Azure AD authentication for Universal authentication with Multi-factor authentication (UA with MFA)
  - A new user credential input field was added for Universal Authentication with MFA to support multi-user authentication.
- The connection dialog box now supports the following 5 authentication methods:
  - Windows Authentication
  - SQL Server Authentication
  - Active Directory - Universal with MFA support
  - Active Directory - Password
  - Active Directory - Integrated

- Database export/import for DacFx wizard using Universal Authentication with MFA.
- For API support, see [IUniversalAuthProvider Interface](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.iuniversalauthprovider.aspx).
- ADAL managed library used by Azure AD Universal Authentication with MFA was upgraded to 3.13.9 version.
- In addition, a new CLI interface was delivered supporting Azure AD admin setting for SQL Database and SQL Data Warehouse.

 For more information on the Active Directory authentication methods, For details, see [Universal Authentication with SQL Database and SQL Data Warehouse (SSMS support for MFA)](https://docs.microsoft.com/azure/sql-database/sql-database-ssms-mfa-authentication) and [Configure Azure SQL Database multi-factor authentication for SQL Server Management Studio](https://docs.microsoft.com/azure/sql-database/sql-database-ssms-mfa-authentication-configure).

- Output window has entries for queries run during expansion of Object Explorer nodes
- Enabled View designer Azure SQL Databases
- The default scripting options for scripting objects from Object Explorer in SSMS have changed:
  - Previously, the default on a new install was to have the generated script target the latest version of SQL Server (currently SQL Server 2017).
  - In SSMS 17.2 a new option has been added: *Match Script Settings to Source*. When set to *True*, the generated script targets the same version, engine type, and engine edition as the server the object being scripted is from.
  - The *Match Script Settings to Source* value is set to *True* by default, so new installs of SSMS will automatically default to always scripting objects to the same target as the original server.
  - When the *Match Script Settings to Source* value is set to *False*, the normal scripting target options will be enabled and function as they did previously.
Additionally, all the scripting options have been moved to their own section - *Version Options*. They are no longer under *General Scripting Options*.

- Added support for National Clouds in "Restore from URL"
- QueryStoreUI reports now supports additional metrics (RowCount, DOP, CLR Time etc.) from sys.query_store_runtime_stats.
- IntelliSense is now supported for Azure SQL Database
https://connect.microsoft.com/SQLServer/feedback/details/3100677/ssms-2016-would-be-nice-to-have-intellisense-on-azure-sql-databases
- Security: connection dialog will default to not trusting server certificates and to requesting encryption for Azure SQL DB connections
- General improvements around support for SQL Server on Linux:
 - Database Mail node is back
 - Addressed misc issues related to paths
 - Activity Monitor is more stable
 - Connection Properties dialog displays correct platform
- Performance Dashboard server report now available as a default report:
  - Can connect to SQL Server 2008 and newer versions.
  - Missing indexes sub-report uses scoring to assist in identifying most useful indexes.
  - Historical wait stats subreport now aggregates waits be category. Idle and sleep waits filtered out by default.
  - New Historical latches subreport.
- Showplan node search allows searching in plan properties. Easily look for any operator property such as table name. To use this option when viewing a plan:
  - Right-click on plan, and in the context menu click on Find Node option
  - Use CTRL+F


**Analysis Services (AS)**

- New AAD role member selection for users without email addresses in AS Azure models in SSMS

**Integration Services (IS)**

- Added new column ("Executed Count") to the execution report for SSIS

### Known issues in this release:

- Query windows using "Active Directory - Universal with MFA Support" authentication may experience an error similar to the following, when attempting to execute a query after being open for one hour:

   `Msg 0, Level 11, State 0, Line 0
The connection is broken and recovery is not possible. The client driver attempted to recover the connection one or more times and all attempts failed. Increase the value of ConnectRetryCount to increase the number of recovery attempts.`

   Rerunning the query should get past the error and succeed.

- The following SSMS functionality is not supported for Azure AD auth using Universal Authentication with MFA:
  - The **New Table/View** designer shows the old-style login prompt, and does not work for Azure AD authentication.
  - The **Edit Top 200 Rows** feature doesn't support Azure Ad authentication.
  - The **Registered Server** component does not support Azure AD authentication.
  - The **Database Engine Tuning Advisor** is not supported for Azure AD authentication. There is a known issue where the error message presented to the user is less than helpful: *Could not load file or assembly 'Microsoft.IdentityModel.Clients.ActiveDirectory,...* instead of the expected *Database Engine Tuning Advisor does not support Microsoft Azure SQL Database. (DTAClient)*.

**Analysis Services (AS)**

- Object Explorer in SSAS will not show the Windows Auth username in AS Azure connection properties.

### Bug fixes

- Fixed an issue when trying to print the results of a query (as text).  https://connect.microsoft.com/SQLServer/feedback/details/3055225/
- Fixed an issue where SSMS was incorrectly dropping tables and other objects when scripting the deletion of such objects on a SQL Azure database.
- Fixed an issue where SSMS occasionally SSMS refuses to start with an error like "Cannot find one or more components. Please reinstall the application"
- Fixed an issue where the SPID in SSMS UI could get stale and out of sync. https://connect.microsoft.com/SQLServer/feedback/details/1898875
- Fixed an issue in SSMS (silent) setup where the /passive argument was treated as /quiet.
- Fixed an issue where SSMS occasionally throws an "Object reference not set to an instance of the object" error on startup. https://connect.microsoft.com/SQLServer/feedback/details/3134698
- Fixed an issue on the "Data Compression Wizard" that was causing SSMS to crash when pressing 'Calculate' on Graph Table
- Addressed performance issue when right-clicking on an index for a table (over a slow internet connect). https://connect.microsoft.com/SQLServer/feedback/details/3120783
- Fixed an issue where SSMS was not able to enumerate backup files on servers with a case-sensitive collation. https://connect.microsoft.com/SQLServer/feedback/details/3134787 and https://connect.microsoft.com/SQLServer/feedback/details/3137000
- Showplan and showplan compare assorted fixes
- Fixed an issue where the Connection Dialog  was not allowing the user to specify the "Network Protocol" to use for the connection, unless SQL Server was installed on the machine running SSMS. https://connect.microsoft.com/SQLServer/feedback/details/3134997
- Improved support for multi-monitor configurations where some SSMS dialog were showing up on "random" locations. Added new option "Task Dialogs" under "SQL Server Object Explorer | Commands" user settings to allow remembering the position of a task dialog or property sheet when it closes. https://connect.microsoft.com/SQLServer/feedback/details/889169, https://connect.microsoft.com/SQLServer/feedback/details/1158271, https://connect.microsoft.com/SQLServer/feedback/details/3135260 
- Fixed an issue where SSMS was not able to change DB properties for encrypted Azure SQL DB
- Improved "Discard results after execution" option. https://connect.microsoft.com/SQLServer/feedback/details/1196581
- Improved/fixed issue where users are not able to access Azure subscriptions for which they are not administrators.
- Improved "Database Restore" wizard to keep the target database selected in OE regadless of the source database selection. https://connect.microsoft.com/SQLServer/feedback/details/3118581
- Fixed an issue where Object Explorer was not sorting incorrectly newly added "Natively compiled stored procedures". https://connect.microsoft.com/SQLServer/feedback/details/3133365
- Fixed an issue where "SELECT TOP n ROWS" did not include the "TOP" clause. For Azure SQLDW. https://connect.microsoft.com/SQLServer/feedback/details/3133551 and https://connect.microsoft.com/SQLServer/feedback/details/3135874
- QueryStoreUI: fixed issue where non-custom time intervals were not working correctly for all reports.
- Always Encrypted:
Improved messaging for AKV permission status in New CMK dialog
Added tooltips to CEK dropdown to make it easier to distinguish CEKs with long names
Fixed an issue where some CNG key store providers would not be displayed in the New Column Master Key dialog for Always Encrypted
- Fixed inconsistent "Application Name" for SSMS connections. https://connect.microsoft.com/SQLServer/feedback/details/3135115
- Fixed an issue where SSMS was not generating correct scripts for SQL Azure (tables and indexes with DATA_COMPRESSIONS option). https://connect.microsoft.com/SQLServer/feedback/details/3133148
- Fixed an issue where user was not able to use CTRL+Q shortcut for Quick Launch (note: the new key bindings to toggle the "IntelliSense Enabled" option in Query Editor is now CTRL+B, CTRL+I. https://connect.microsoft.com/SQLServer/feedback/details/3131968
- Fixed an issue in "Restore Database" where SSMS was throwing an exception when trying to select a storage account from a subscription that has accounts with custom domains defined
- Fixed an issue in "Database Diagram" where SSMS was throwing an "Index was outside the bounds of the array" error; also, the user was not able to change the "Table View" to anything but standard. https://connect.microsoft.com/SQLServer/feedback/details/3133792 and https://connect.microsoft.com/SQLServer/feedback/details/3135326
- Fixed an issue in "Backup/Restore to URL" where SSMS was not enumerating classic storage accounts.
- Fixed an issue where an exception was being thrown when trying to add schema-bound securables to DB Roles. https://connect.microsoft.com/SQLServer/feedback/details/3118143
- Fixed an issue where SSMS was intermittently showing the error "Data is Null. This method or property cannot be called on Null values." when expanding a table node https://connect.microsoft.com/SQLServer/feedback/details/3136283
- DTA: Fixed an issue where DTAEngine.exe terminates with Heap Corruption when evaluating Partition Function with Certain Boundary Values.


**Analysis Services (AS)**

- Fixed an issue where AS Restore Database would fail with an error if the DB had a different Name than ID
- Fixed an issue causing the DAX query window to disregard the menu option for toggling IntelliSense Enabled
- Fixed an issue that prevented connecting to SSAS through msmdpump IIS http/https addresses
- Allow connecting to AS Azure using a password that contains a semi-colon
- Scripting out AS Restore Database command with "Skip Membership" option will include the new corresponding JSON option when used with SQL Server 2017 AS server or AS Azure
- Fixed an extremely rare issue that could cause the delete database dialog to raise an error when loading
- Fixed an issue that may occur when attempting to view partitions in 1400-compat level model containing a mix of SQL query and M partition definitions

**Integration Services (IS)**
- Fixed issue where the execution information reports of SSISDB catalog can't be displayed
- Addressed issues in SSMS related to poor performance with large number of projects/packages

## ![download](../ssdt/media/download.png) [SSMS 17.1](https://go.microsoft.com/fwlink/?linkid=849819)
Generally available | Build number: 14.0.17119.0

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x40a)

### Enhancements

- Profiler: Help > About now displays release version number (e.g 17.1)
- Analysis Service users can refresh credentials for their datasources for 1200 TM models and above from the context menu on the datasource
- Built-in SSIS reports now show logs from SSIS scale-out execution in CTP 2.1
- SSIS scale-out management application
  - View basic information about scale-out master.
  - Easily add a Worker to the scale-out deployment.
  - View all the scale-out workers and basic information about them, and can also enable or disable them easily.

### Bug fixes
- Always On:
  - Fixed an issue where the properties of an Availability Replica was always displayed as "Automatic failover" mode for WSFC AGs.
  - Fixed an issue where the read-only routing list was overwritten when updating the Availability Group
- Always Encrypted: fixed an issue where log file generated was missing the information generated by DacFx.
- ShowPlan: fixed in issue where the UI was always showing the Actual join type attribute for non-adaptive join operators.
- Setup:
  - Fixed an issue where SSMS 17.0 was breaking SSDT on Visual Studio 2013 [Connect Item 3133479]
  - Fixed an issue where clicking on "Restart" at the end of setup was not restarting the machine
- Scripting: temporarily preventing SSMS from accidentally deleting Azure database objects when trying to script the deletion by disabling that option.  Proper fix will be in an upcoming release of SSMS.
- Object Explorer: fixed an issue where "Databases" node was not expanded when connected to an Azure database created using "AS COPY"

## ![download](../ssdt/media/download.png) [SSMS 17.0](https://go.microsoft.com/fwlink/?LinkID=847722)
Generally available | Build number: 14.0.17099.0

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=847722&clcid=0x40a)

### Enhancements 

- Upgrade package and Windows Software Update Services (WSUS) 
Future 17.X releases include a smaller cumulative update package 
  - The update package will also be published to the WSUS catalog  
- Icon Updates
Icons have been updated to be consistent with VS Shell provided icons and support High DPI resolutions
New SSMS and Profiler program icons to differentiate between 16.X and 17.X versions
- SQL PowerShell Module
  - SQL Server PowerShell module removed from SSMS and now ships via the PowerShell gallery (PowerShell 5.0 now required to support module versioning)
  - Miscellaneous improvements to the "presentation" (formatting) of some SMO objects (e.g. databases now show the size and the available space and tables show row count and space usage)
  - Added colorization when the PowerShell command prompt is invoked from the "Start PowerShell" menu in OE
  - Added -ClusterType and -RequiredCopiesToCommit parameter to AG cmdlets (New-SqlAvailabilityGroup, Join-SqlAvailabilityGroup, and Set-SqlAvailabilityGroup cmdlets)
  - Added parameters -ActiveDirectoryAuthority and -AzureKeyVaultResourceId to Add-SqlAzureAuthenticationContext cmdlet
  - Added	Revoke-SqlAvailabilityGroupCreateAnyDatabase, Grant-SqlAvailabilityGroupCreateAnyDatabase and Set-SqlAvailabilityReplicaRoleToSecondary cmdlets
  - Added -For details, seedingMode parameter to Set-SqlAvailabilityReplica and New-SqlAvailabilityReplica cmdlets
  - Added -ConnectionString parameter to Get-SqlDatabase
- SQL Server on Linux
General improvements and fixes for Log Shipping
  - Added support for native Linux paths Attach, Restore and Backup database
  - Added support for native Linux paths for audit log destination folder
- Analysis Services
  - DAX Query Window:
    - Parentheses matching in the editor
    - DEFINE MEASURE and DEFINE VAR syntax support
    - Assorted Intellisense improvements
  - Universal Authentication
    - Allows users to specify a username and no password and the Azure Login Dialog will handle the connection
  - SSMS PQ Integration: 
    - Scripting of structured data sources works 
    - Viewing and Editing of structured data sources in PQ UI
- New "Add Unique Constraint" template
- Showplan
Show max instead of sum across the threads in properties window for elapsed time
Expose new mem grant operator properties
Enabled the "Edit Query" button in Live Query Statistics
Support for interleaved execution
  - New option to "Analyze Actual Execution Plan"
  - General improvements to showplan compare
  - Introduced functionality in Showplan Comparison feature to find significant differences in Cardinality Estimation between matching nodes of two query plans and perform basic analysis of the possible root causes
- Removed Configuration Manager from Registered Servers explorer
- Enable reading audit logs from Azure blob storage
- Added Parameterization for Always Encrypted, please refer to [this page](https://blogs.msdn.microsoft.com/sqlsecurity/2016/12/13/parameterization-for-always-encrypted-using-ssms-to-insert-into-update-and-filter-by-encrypted-columns/) for more details 
- AAD Universal auth connection to Azure SQL DB supports custom tenant ID 
- Generate scripts for Azure SQL Database, now scripts full text, rules, and database
- Branding fixes in splash screens for SSMS and Profiler
- Removed Utility Control Point UI from SSMS
- SSMS can now create "PremiumRS" edition SQL Azure databases
- Always On Availability Groups
  - Add support for new cluster types: EXTERNAL and NONE
Add support for SQL Server on Linux
Add automatic For details, seeding as an option for initial data synchronization
Fixed some defects, e.g. endpoint URL handling, DB refresh and UI layout
Removed Azure replica-related features
  - Improved IntelliSense for several Availability Group keywords
- Activity Monitor
  - Added new "Activity Monitor" pane to the SSMS Output window
  - Changed connection error/timeout message to log info to output window rather than a pop-up message
  - Removed empty chart (5th chart) in Overview section
  - Added "(paused)" to Overview title if the Activity Monitor data collection is paused
  - Graph Extensions to SQL Server 
New icons for graph node and edge tables
Graph node and edge tables will be displayed under Graph Tables folder
Templates to create graph node and edge tables available
- Presentation Mode
3 new tasks available via Quick Launch (Ctr-Q)
PresentOn - Turn on presentation mode
PresentEdit - Edit the presentation font sizes for presentation mode.  "Text Editor font" for the Query Editor.  "Environment font" for other components.
RestoreDefaultFonts - Revert back to default settings.
*Note: there is currently no PresentOff command at this time.  Use RestoreDefaultFonts to turn off Presentation Mode*

### Bug fixes

- Fixed an issue where SSMS crashed when showplan scrolled via surface book touchpad
- Fixed an issue where SSMS hangs for a long time while getting the properties of a database which is being restored or offline 
- Fixed an issue where "Help viewer" could not be opened in RC builds
- Fixed an issue where "Maintenance Plans Tasks Toolbox" items may be missing in SSMS.
- Fixed an issue in SSMS where the user was unable to shrink a database when the database name contained curly braces. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3122618)
- Fixed an issue where SSMS was trying to script the deletion of an Azure database was actually causing the deletion of the database itself. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3131458/)
- Fixed an issue where default values were not scripted for user-defined table types. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3119027)
- Another round of perf improvements around context menu on indexes. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3120783)
- Fixed issue which was causing excessive flickering when hovering mouse over missing index in execution plan. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3118510)
- Fixed an issue where SSMS was taking the DB offline when scripting [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3118550)
- Miscellaneous UI fixes on localized (non-English) versions of SSMS.
- Fixed issue where "Always Encrypted Keys" node was missing when targeting SQL 2016 SP1 Standard Edition.
- Always Encrypted
"Always Encrypted" menu was incorrectly enabled when targeting SQL 2016 RTM Standard Edition or any SQL 2014 (and below) servers
Fixed an issue where IntelliSense is reporting an error when the CREATE OR ALTER syntax is used
Fixed issue where encryption fails in case CMK/CEK contain characters that should be escaped, i.e. enclosed in brackets
When an Out of Memory exception occurs in SSMS, the user is presented an error that suggests using the native (64bit) PowerShell instead.
Fixed issue where the AE wizard was failing in case the user was using Resource Group Manager subscriptions instead of Classic Azure subscriptions
Fixed issue where AE wizard was showing an incorrect error when the user had no permissions in any subscriptions or had no Azure Key Vaults in any of them.
Fixed issue in AE wizard where the Azure Key Vault sign-in page was not showing Azure subscriptions in case of multiple AAD
Fixed issue in AE wizard where the Azure Key Vault sign-in page was not showing Azure subscriptions for which the user has reader permission
  - Fixed an issue where resource files may not be loaded correctly, thus resulting in inaccurate error messages
- Improved contrast of hyperlinks on SSMS Setup page
- Fixed an issue where PolyBase nodes were not displayed when connected to SQL Server Express (2016 SP1)
- Fixed an issue where SSMS is unable to change the Compatibility Level of an Azure DB to v140
- Improved performance of Object Explorer when expanding the list of Azure databases [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3100675)
- Fixed an issue where "View SQL Server Log" context menu item appeared incorrectly for non-relational server types (AS\RS\IS) 
- Fixed an issue where checking syntax of an Analysis Services partition query using SQL auth could result in login failed message
- Fixed an issue where renaming a preview 1400 compat-level AS tabular model would fail in SSMS
- Fixed an "operation failed on model" issue that could occur after attempting an invalid operation on the AS server in rare circumstances, revert local changes after unsuccessful save on the model
- Fixed a typo in Analysis Services Synchronize Database popup dialog
- Backup/restore container dialogs come up offscreen on multiple monitor setups. 
- SecurityPolicy create fails if target object has `]` in its name.
- SSMS 2016 "Open recent" menu doesn't show recently saved files. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)
- Removed reset of user settings when VS Shell is updated.
- Fixed an issue that was preventing the user from being able to change Compatibility Level of a database on SQL Server 2017.
- Query windows using AAD Universal authentication cannot refresh the query after an hour.
- Utility Control Point UI removed from SSMS.
- AD Universal auth connections fail to query data after the initial token expiration.
- Unable to script Rules from Azure SQL DB to Azure SQL DB.
- Fixed issue where SQL PowerShell was not able to connect legacy SQL instances (2014 and older). [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/1138754/sql-server-sqlps-powershell-module-fails-connection-to-sql-2012-instance)
- Fixed an issue that was causing SSMS to crash when failing to import registered servers.
- Fixed an issue that was causing SSMS to crash if a user has certain permissions in a database.
- SSMS - tables disappear from design surface while reviewing views. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/2946125/ssms-tables-disappears-from-design-surface-while-reviewing-views) 
- The table scrollbar does not allow the user to scroll the table content, only the up/down Arrow allow this. It's also possible to scroll the table content after trying to scroll using the scrollbar which is a bug. [Connect Item](
https://connect.microsoft.com/SQLServer/feedback/details/3106561/sql-server-manager-2016-bug-in-design-view) 
- Registered Servers not displaying icons after refreshing the root node.
- Script button for Create Database on Azure v12 servers executes script then displays message "No action to be scripted".
- SSMS Connect to Server dialog does not clear "Additional Properties" tab for each new connection.
- Generate Tasks script doesn't generate Create Database scripts for an Azure SQL DB.
- Scrollbar in View Designer appears disabled.
- Always Encrypted AVK key paths do not include version IDs.
- Reduced number of engine edition queries in the query window. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3113387)
- Always Encrypted errors from refreshing modules after encryption are incorrectly handled.
- Changed default connection timeout for OLTP and OLAP from 15 to 30 seconds to fix a class of ignored connection failures. 
- Fixed a crash in SSMS when custom report is launched. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3118856)
- Fixed an issue where "Generate Script..." fails for Azure SQL databases.
- Fix "Script As" and "Generate Script Wizard" to not add extra newlines when scripting objects such as stored procedures. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3115850)
- SQLAS PowerShell Provider: Add LastProcessed property to Dimension and MeasureGroup folders. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3111879)
- Live Query Statistics: fixed issue where it was only showing the first query in a batch. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3114221)  
- Showplan: show max instead of sum across the threads in properties window.
- Query Store: add new report on queries with high execution variation.
- Object explorer performance issues: [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3114074)
Context menu for tables momentarily hangs
SSMS is slow when right-clicking an index for a table (over a remote (Internet) connection). 
Avoid issuing table queries that sort on the server
- Removed Azure Deployment Wizard (Deploy Database to Azure VM) from SSMS
- Fixed issue where missing indexes were not shown in execution plans in SSMS [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3114194)
- Fixed common crash-on-shutdown issue in SSMS
- Fixed issue in Object Explorer where an error occurred when bringing up the context menu on the PolyBase|Scale-Out Group nodes [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3115128)
- Fixed an issue where SSMS may crash when trying to display the permissions on a database
- Query Store: general enhancements in context menu items for result grids of query store report
- Configuring Always Encrypted for an existing table fails with errors on unrelated objects. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3103181)
- Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3109591)
- The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3111925)
- When encrypting using Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.
- Fixed UI truncation issue on "New Server Registration" dialog
- Fix DMF Condition UI incorrectly updating expressions that contain string constant values with quotes in them
- Fixed an issue that may cause SSMS to crash when running custom reports
- Add "Execution in Scale Out..." menu item to the folder node
- Fixed an issue with Azure SQL DB firewall whitelist IP address feature
- Fixed an issue in SSMS which caused an Object reference not set exception when editing the source of AS multi-dimensional partition
- Fixed an issue in SSMS which caused an Object reference not set exception when deleting a customer assembly from multi-dimensional AS server
- Fixed an issue where renaming an AS tabular 1400 db failed
- Fixed an issue with scripting a 1400 compat-level AS tabular datasource from connection properties dialog
- Remove assumption that tables in AS 1400 compat-level model have at least one partition
- Ctrl-R now toggles results pane in SSMS DAX query editor


## ![download](../ssdt/media/download.png) [SSMS 16.5.3](https://go.microsoft.com/fwlink/?LinkID=840946)
Generally available | Build number: 13.0.16106.4

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40a)

The following issues were fixed this release:

* Fixed an issue introduced in SSMS 16.5.2 which was causing the expansion of the 'Table' node when the table had more than one sparse column.

* Users can deploy SSIS packages containing OData Connection Manager which connect to a Microsoft Dynamics AX/CRM Online resource to SSIS catalog. For more information, For details, see [OData Connection Manager](../integration-services/connection-manager/odata-connection-manager.md).

* Configuring Always Encrypted on an existing table fails with errors on unrelated objects. [Connect ID 3103181](https://connect.microsoft.com/SQLServer/feedback/details/3103181/setting-up-always-encrypted-on-an-existing-table-fails-with-errors-on-unrelated-objects)

* Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect ID 3109591](https://connect.microsoft.com/SQLServer/feedback/details/3109591/sql-server-2016-always-encrypted-against-existing-database-with-multiple-schemas-doesnt-work)

* The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect ID 3111925](https://connect.microsoft.com/SQLServer/feedback/details/3111925/sql-server-2016-always-encrypted-encrypted-column-wizard-failed-task-failed-due-to-following-error-cannot-save-package-to-file-the-model-has-build-blocking-errors)

* When encrypting using Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.

* *Open recent* menu doesn't show recently saved files. [Connect ID 3113288](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)

* SSMS is slow when right-clicking an index for a table (over a remote (Internet) connection). [Connect ID 3114074](https://connect.microsoft.com/SQLServer/feedback/details/3114074/ssms-slow-when-right-clicking-an-index-for-a-table-over-a-remote-internet-connection)
 
* Fixed an issue with the SQL Designer scrollbar. [Connect ID 3114856](https://connect.microsoft.com/SQLServer/feedback/details/3114856/bug-in-scrollbar-on-sql-desginer-in-ssms-2016)

* Context menu for tables momentarily hangs 
 
* SSMS occasionally throws exceptions in Activity Monitor and crashes. [Connect ID 697527](https://connect.microsoft.com/SQLServer/feedback/details/697527/)

* SSMS 2016 crashes with error "The process was terminated due to an internal error in the .NET Runtime at IP 71AF8579 (71AE0000) with exit code 80131506"


## Uninstall and reinstall SSMS 17.x

If your SSMS installation is having problems, and a standard uninstall and reinstall doesn't resolve them, you can first try [repairing](https://support.microsoft.com/help/4028054/windows-10-repair-or-remove-programs) the Visual Studio 2015 IsoShell. If repairing the Visual Studio 2015 IsoShell doesn't resolve the problem, the following steps have been found to fix many random issues:

1.	Uninstall SSMS the same way you uninstall any application (using *Apps & features*, *Programs and features*, etc. depending on your version of Windows).

2.	Uninstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:
   
    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```

    ```vs_isoshell.exe /Uninstall /Force /PromptRestart```

3.	Uninstall Microsoft Visual C++ 2015 Redistributable the same way you uninstall any application. Uninstall both x86 and x64 if they're on your computer.

4.	Reinstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:  

    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```  
 
    ```vs_isoshell.exe /PromptRestart```

5.	Reinstall SSMS.

6.	Upgrade to the [latest version of the Visual C++ 2015 Redistributable](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads) if you're not currently up to date.


## Additional Downloads  
For a list of all SQL Server Management Studio downloads, search the [Microsoft Download Center](https://www.microsoft.com/download/search.aspx?q=sql%20server%20management%20studio&p=0&r=10&t=&s=Relevancy~Descending).  
  
For the latest release of SQL Server Management Studio, For details, see [Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md).  
