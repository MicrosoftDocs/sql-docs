---
title: "SQL Server Management Studio - Changelog (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "08/07/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 3dc76cc1-3b4c-4719-8296-f69ec1b476f9
caps.latest.revision: 72
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# SQL Server Management Studio - Changelog (SSMS)

This article provides details about updates, improvements, and bug fixes for the current and previous versions of SSMS. Download [previous SSMS versions below](#previous-ssms-releases).

## [SSMS 17.2](download-sql-server-management-studio-ssms.md)

Generally available | Build number: 14.0.17177.0

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
- In addition a new CLI interface was delivered supporting Azure AD admin setting for SQL Database and SQL Data Warehouse.

 For more information on the Active Directory authentication methods, see [Universal Authentication with SQL Database and SQL Data Warehouse (SSMS support for MFA)](https://docs.microsoft.com/azure/sql-database/sql-database-ssms-mfa-authentication) and [Configure Azure SQL Database multi-factor authentication for SQL Server Management Studio](https://docs.microsoft.com/azure/sql-database/sql-database-ssms-mfa-authentication-configure).

- Output window has entries for queries run during expansion of Object Explorer nodes
- Enabled View designer Azure SQL Databases
- The default scripting options for scripting objects from Object Explorer in SSMS have changed:
  - Previously, the default on a new install was to have the generated script target the latest version of SQL Server (currently SQL Server 2017).
  - In SSMS 17.2 a new option has been added: *Match Script Settings to Source*. When set to *True*, the generated script targets the same version, engine type, and engine edition as the server the object being scripted is from.
  - The *Match Script Settings to Source* value is set to *True* by default, so new installs of SSMS will automatically default to always scripting objects to the same target as the original server.
  - When the *Match Script Settings to Source* value is set to *False*, the normal scripting target options will be enabled and function as they did previously.
	- Additionally, all the scripting options have been moved to their own section - *Version Options*. They are no longer under *General Scripting Options*.

- Added support for National Clouds in "Restore from URL"
- QueryStoreUI reports now supports additional metrics (RowCount, DOP, CLR Time etc.) from sys.query_store_runtime_stats.
- IntelliSense is now supported for Azure SQL Database
	- https://connect.microsoft.com/SQLServer/feedback/details/3100677/ssms-2016-would-be-nice-to-have-intellisense-on-azure-sql-databases
- Security: connection dialog will default to not trusting server certificates and to requesting encryption for Azure SQL DB connections
- General improvements around support for SQL Server on Linux:
 - Database Mail node is back
 - Addressed misc issues related to paths
 - Activity Monitor is more stable
 - Connection Properties dialog displays correct platform
- Performance Dashboard server report now available as a default report:
  - Can connect to SQL Server 2008 and newer versions.
  - Missing indexes sub-report uses scoring to assist in identifying most useful indexes.
  - Historical wait stats sub-report now aggregates waits be category. Idle and sleep waits filtered out by default.
  - New Historical latches sub-report.
- Showplan node search allows searching in plan properties. Easily look for any operator property such as table name. To use this option when viewing a plan:
  - Right-click on plan, and in the context menu click on Find Node option
  - Use CTRL+F


### Analysis Services (AS)

- New AAD role member selection for users without email addresses in AS Azure models in SSMS

### Integration Services (IS)

- Added new column ("Executed Count") to the execution report for SSIS

### Known issues in this release:

- Query windows using "Active Directory - Universal with MFA Support" authentication may experience an error similar to the following, when attempting to execute a query after being open for one hour:

   `Msg 0, Level 11, State 0, Line 0
The connection is broken and recovery is not possible. The client driver attempted to recover the connection one or more times and all attempts failed. Increase the value of ConnectRetryCount to increase the number of recovery attempts.`

   Re-running the query should get past the error and succeed.

- The following SSMS functionality is not supported for Azure AD auth using Universal Authentication with MFA:
  - The **New Table/View** designer shows the old-style login prompt, and does not work for Azure AD authentication.
  - The **Edit Top 200 Rows** feature doesn't support Azure Ad authentication.
  - The **Registered Server** component does not support Azure AD authentication.
  - The **Database Engine Tuning Advisor** is not supported for Azure AD authentication. There is a known issue where the error message presented to the user is less than helpful: *Could not load file or assembly 'Microsoft.IdentityModel.Clients.ActiveDirectory,…* instead of the expected *Database Engine Tuning Advisor does not support Microsoft Azure SQL Database. (DTAClient)*.

**AS**

- Object Explorer in SSAS will not show the Windows Auth username in AS Azure connection properties.

### Bug fixes

- Fixed an issue when trying to print the results of a query (as text).  https://connect.microsoft.com/SQLServer/feedback/details/3055225/
- Fixed an issue where SSMS was incorrectly dropping tables and other objects when scripting the deletion of such objects on a SQL Azure database.
- Fixed an issue where SSMS occasionally SSMS refuses to start with an error like "Cannot find one or more components. Please reinstall the application"
- Fixed an issue where the SPID in SSMS UI could get stale and out of sync. https://connect.microsoft.com/SQLServer/feedback/details/1898875
- Fixed an issue in SSMS (silent) setup where the /passive argument was treated as /quiet.
- Fixed an issue where SSMS occasionally throws an "Object reference not set to an instance of the object" error on startup. http://connect.microsoft.com/SQLServer/feedback/details/3134698
- Fixed an issue on the "Data Compression Wizard" that was causing SSMS to crash when pressing 'Calculate' on Graph Table
- Addressed performance issue when right clicking on an index for a table (over a slow internet connect). https://connect.microsoft.com/SQLServer/feedback/details/3120783
- Fixed an issue where SSMS was not able to enumerate backup files on servers with a case-sensitive collation. http://connect.microsoft.com/SQLServer/feedback/details/3134787 and https://connect.microsoft.com/SQLServer/feedback/details/3137000
- Showplan and showplan compare assorted fixes
- Fixed an issue where the Connection Dialog  was not allowing the user to specify the "Network Protocol" to use for the connection, unless SQL Server was installed on the machine running SSMS. https://connect.microsoft.com/SQLServer/feedback/details/3134997
- Improved support for multi-monitor configurations where some SSMS dialog were showing up on "random" locations. Added new option "Task Dialogs" under "SQL Server Object Explorer | Commands" user settings to allow remembering the position of a task dialog or property sheet when it closes. https://connect.microsoft.com/SQLServer/feedback/details/889169, https://connect.microsoft.com/SQLServer/feedback/details/1158271, https://connect.microsoft.com/SQLServer/feedback/details/3135260 
- Fixed an issue where SSMS was not able to change DB properties for encrypted Azure SQL DB
- Improved "Discard results after execution" option. https://connect.microsoft.com/SQLServer/feedback/details/1196581
- Improved/fixed issue where users are not able to access Azure subscriptions for which they are not administrators.
- Improved "Database Restore" wizard to keep the target database selected in OE regadless of the source database selection. https://connect.microsoft.com/SQLServer/feedback/details/3118581
- Fixed an issue where Object Explorer was not sorting incorrectly newly added "Natively compiled stored procedures". http://connect.microsoft.com/SQLServer/feedback/details/3133365
- Fixed an issue where "SELECT TOP n ROWS" did not include the "TOP" clause. For Azure SQLDW. https://connect.microsoft.com/SQLServer/feedback/details/3133551 and https://connect.microsoft.com/SQLServer/feedback/details/3135874
- QueryStoreUI: fixed issue where non-custom time intervals were not working correctly for all reports.
- Always Encrypted:
	- Improved messaging for AKV permission status in New CMK dialog
	- Added tooltips to CEK dropdown to make it easier to distinguish CEKs with long names
	- Fixed an issue where some CNG key store providers would not be displayed in the New Column Master Key dialog for Always Encrypted
- Fixed inconsistent "Application Name" for SSMS connections. http://connect.microsoft.com/SQLServer/feedback/details/3135115
- Fixed an issue where SSMS was not generating correct scripts for SQL Azure (tables and indexes with DATA_COMPRESSIONS option). https://connect.microsoft.com/SQLServer/feedback/details/3133148
- Fixed an issue where user was not able to use CTRL+Q shortcut for Quick Launch (note: the new key bindings to toggle the "IntelliSense Enabled" option in Query Editor is now CTRL+B, CTRL+I. https://connect.microsoft.com/SQLServer/feedback/details/3131968
- Fixed an issue in "Restore Database" where SSMS was throwing an exception when trying to select a storage account from a subscription that has accounts with custom domains defined
- Fixed an issue in "Database Diagram" where SSMS was throwing an "Index was outside the bounds of the array" error; also, the user was not able to change the "Table View" to anything but standard. https://connect.microsoft.com/SQLServer/feedback/details/3133792 and http://connect.microsoft.com/SQLServer/feedback/details/3135326
- Fixed an issue in "Backup/Restore to URL" where SSMS was not enumerating classic storage accounts.
- Fixed an issue where an exception was being thrown when trying to add schema-bound securables to DB Roles. https://connect.microsoft.com/SQLServer/feedback/details/3118143
- Fixed an issue were SSMS was intermittently showing the error "Data is Null. This method or property cannot be called on Null values." when expanding a table node http://connect.microsoft.com/SQLServer/feedback/details/3136283
- DTA: Fixed an issue where DTAEngine.exe terminates with Heap Corruption when evaluating Partition Function with Certain Boundary Values.


Analysis Services (AS)
- Fixed an issue where AS Restore Database would fail with an error if the DB had a different Name than ID
- Fixed an issue causing the DAX query window to disregard the menu option for toggling IntelliSense Enabled
- Fixed an issue that prevented connecting to SSAS through msmdpump IIS http/https addresses
- Allow connecting to AS Azure using a password that contain a semi-colon
- Scripting out AS Restore Database command with "Skip Membership" option will include the new corresponding JSON option when used with SQL Server 2017 AS server or AS Azure
- Fixed an extremely rare issue that could cause the delete database dialog to raise an error when loading
- Fixed an issue that may occur when attempting to view partitions in 1400-compat level model containing a mix of SQL query and M partition definitions

Integration Services (IS)
- Fixed issue where the execution information reports of SSISDB catalog can't be displayed
- Addressed issues in SSMS related to poor performance with large number of projects/packages


## Previous SSMS releases

Download previous SSMS versions by clicking the title links in the following sections.

## ![download](../ssdt/media/download.png) [SSMS 17.1](https://go.microsoft.com/fwlink/?linkid=849819)
Generally available | Build number: 14.0.17119.0

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
- ShowPlan: fixed in issue where the UI was always showing the Actual join type attribute for non Adaptive join operators.
- Setup:
  - Fixed an issue where SSMS 17.0 was breaking SSDT on Visual Studio 2013 [Connect Item 3133479]
  - Fixed an issue where clicking on "Restart" at the end of setup was not restarting the machine
- Scripting: temporarily preventing SSMS from accidentally deleting Azure database objects when trying to script the deletion by disabling that option.  Proper fix will be in an upcoming release of SSMS.
- Object Explorer: fixed an issue where "Databases" node was not expanded when connected to an Azure database created using "AS COPY"

## ![download](../ssdt/media/download.png) [SSMS 17.0](http://go.microsoft.com/fwlink/?LinkID=847722)
Generally available | Build number: 14.0.17099.0

### Enhancements 

- Upgrade package and Windows Software Update Services (WSUS) 
	- Future 17.X releases include a smaller cumulative update package 
  - The update package will also be published to the WSUS catalog  
- Icon Updates
	- Icons have been updated to be consistent with VS Shell provided icons and support High DPI resolutions
	- New SSMS and Profiler program icons to differentiate between 16.X and 17.X versions
- SQL PowerShell Module
  - SQL Server PowerShell module removed from SSMS and now ships via the PowerShell gallery (PowerShell 5.0 now required to support module versioning)
  - Miscellaneous improvements to the "presentation" (formatting) of some SMO objects (e.g. databases now show the size and the available space and tables show row count and space usage)
  - Added colorization when the PowerShell command prompt is invoked from the "Start PowerShell" menu in OE
  - Added -ClusterType and -RequiredCopiesToCommit parameter to AG cmdlets (New-SqlAvailabilityGroup, Join-SqlAvailabilityGroup, and Set-SqlAvailabilityGroup cmdlets)
  - Added parameters -ActiveDirectoryAuthority and -AzureKeyVaultResourceId to Add-SqlAzureAuthenticationContext cmdlet
  - Added	Revoke-SqlAvailabilityGroupCreateAnyDatabase, Grant-SqlAvailabilityGroupCreateAnyDatabase and Set-SqlAvailabilityReplicaRoleToSecondary cmdlets
  - Added -SeedingMode parameter to Set-SqlAvailabilityReplica and New-SqlAvailabilityReplica cmdlets
  - Added -ConnectionString parameter to Get-SqlDatabase
- SQL Server on Linux
	- General improvements and fixes for Log Shipping
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
    - Viewing and Editing of structured data sources in PQ UI
- New "Add Unique Constraint" template
- Showplan
	- Show max instead of sum across the threads in properties window for elapsed time
	- Expose new mem grant operator properties
	- Enabled the "Edit Query" button in Live Query Statistics
	- Support for interleaved execution
  - New option to "Analyze Actual Execution Plan"
  - General improvements to showplan compare
  - Introduced functionality in Showplan Comparison feature to find significant differences in Cardinality Estimation between matching nodes of two query plans and perform basic analysis of the possible root causes
- Removed Configuration Manager from Registered Servers explorer
- Enable reading audit logs from Azure blob storage
- Added Parameterization for Always Encrypted, please refer to [this page](https://blogs.msdn.microsoft.com/sqlsecurity/2016/12/13/parameterization-for-always-encrypted-using-ssms-to-insert-into-update-and-filter-by-encrypted-columns/) for more details 
- AAD Universal auth connection to Azure SQL DB supports custom tenant id 
- Generate scripts for Azure SQL Database, now scripts full text, rules, and database
- Branding fixes in splash screens for SSMS and Profiler
- Removed Utility Control Point UI from SSMS
- SSMS can now create "PremiumRS" edition SQL Azure databases
- Always On Availability Groups
  - Add support for new cluster types: EXTERNAL and NONE
	- Add support for SQL Server on Linux
	- Add automatic seeding as an option for initial data synchronization
	- Fixed the some defects, e.g. endpoint URL handling, DB refresh and UI layout
	- Removed Azure replica related features
  - Improved IntelliSense for several Availability Group keywords
- Activity Monitor
  - Added new "Activity Monitor" pane to the SSMS Output window
  - Changed connection error/timeout message to log info to output window rather than a pop up message
  - Removed empty chart (5th chart) in Overview section
  - Added "(paused)" to Overview title if the Activity Monitor data collection is paused
  - Graph Extensions to SQL Server 
	- New icons for graph node and edge tables
	- Graph node and edge tables will be displayed under Graph Tables folder
	- Templates to create graph node and edge tables available
- Presentation Mode
	- 3 new tasks available via Quick Launch (Ctr-Q)
	- PresentOn - Turn on presentation mode
	- PresentEdit - Edit the presentation font sizes for presentation mode.  "Text Editor font" for the Query Editor.  "Environment font" for other components.
	- RestoreDefaultFonts - Revert back to default settings.
	- *Note: there is currently no PresentOff command at this time.  Use RestoreDefaultFonts to turn off Presentation Mode*

### Bug fixes

- Fixed an issue where SSMS crashed when showplan scrolled via surfacebook touchpad
- Fixed an issue where SSMS hangs for a long times while getting the properties of a databases which is being restored or offline 
- Fixed an issue where "Help viewer" could not be opened in RC builds
- Fixed an issue where "Maintenance Plans Tasks Toolbox" items may be missing in SSMS.
- Fixed an issue in SSMS where the user was unable to shrink a database when the database name contained curly braces. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3122618)
- Fixed an issue where SSMS was trying to script the deletion of an Azure database was actually causing the deletion of the database itself. [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3131458/)
- Fixed an issue where default values were not scripted for user defined table types. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3119027)
- Another round of perf improvements around context menu on indexes. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3120783)
- Fixed issue which was causing excessive flickering when hovering mouse over missing index in execution plan. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3118510)
- Fixed an issue where SSMS was taking the DB offline when scripting [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3118550)
- Miscellaneous UI fixes on localized (non-English) versions of SSMS.
- Fixed issue where "Always Encrypted Keys" node was missing when targeting SQL 2016 SP1 Standard Edition.
- Always Encrypted
	- "Always Encrypted" menu was incorrectly enabled when targeting SQL 2016 RTM Standard Edition or any SQL 2014 (and below) servers
	- Fixed an issue where IntelliSense is reporting an error when the CREATE OR ALTER syntax is used
	- Fixed issue where encryption fails in case CMK/CEK contain characters that should be escaped, i.e. enclosed in brackets
	- When an Out of Memory exception occurs in SSMS, the user is presented an error that suggests to use the native (64bit) PowerShell instead.
	- Fixed issue where the AE wizard was failing in case the user was using Resource Group Manager subscriptions instead of Classic Azure subscriptions
	- Fixed issue where AE wizard was showing an incorrect error when the user had no permissions in any subscriptions or had no Azure Key Vaults in any of them.
	- Fixed issue in AE wizard where the Azure Key Vault sign-in page was not showing Azure subscriptions in case of multiple AAD
	- Fixed issue in AE wizard where the Azure Key Vault sign-in page was not showing Azure subscriptions for which the user has reader permission
  - Fixed an issue where resource files may not be loaded correctly, thus resulting in inaccurate error messages
- Improved contrast of hyperlinks on SSMS Setup page
- Fixed an issue where Polybase nodes were not displayed when connected to SQL Server Express (2016 SP1)
- Fixed an issue where SSMS is unable to change the Compatibility Level of an Azure DB to v140
- Improved performance of Object Explorer when expanding the list of Azure databases [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3100675)
- Fixed an issue where "View SQL Server Log" context menu item appeared incorrectly for non-relational server types (AS\RS\IS) 
- Fixed an issue where checking syntax of an Analysis Services partition query using SQL auth could result in login failed message
- Fixed an issue where renaming a preview 1400 compat-level AS tabular model would fail in SSMS
- Fixed an “operation failed on model” issue that could occur after attempting an invalid operation on the AS server in rare circumstances, revert local changes after unsuccessful save on the model
- Fixed a typo in Analysis Services Synchronize Database popup dialog
- Backup/restore container dialogs come up offscreen on multiple monitor setups. 
- SecurityPolicy create fails if target object has ] in its name.
- SSMS 2016 "Open recent" menu doesn't show recently saved files. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)
- Removed reset of user settings when VS Shell is updated.
- Fixed an issue that was preventing the user from being able to change Compatibility Level of a database on SQL Server 2017.
- Query windows using AAD Universal authentication cannot refresh the query after an hour.
- Utility Control Point UI removed from SSMS.
- AD Universal auth connections fail to query data after the initial token expiration.
- Unable to script Rules from Azure SQL DB to Azure SQL DB.
- Fixed issue were SQL PowerShell was not able to connect legacy SQL instances (2014 and older). [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/1138754/sql-server-sqlps-powershell-module-fails-connection-to-sql-2012-instance)
- Fixed an issue that was causing SSMS to crash when failing to import registered servers.
- Fixed an issue that was causing SSMS to crash if a user has certain permissions an a database. 
- SSMS - tables disappear from design surface while reviewing views. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/2946125/ssms-tables-disappears-from-design-surface-while-reviewing-views) 
- The table scrollbar does not allow the user to scroll the table content, only the up/down Arrow allow this. Its also possible to scroll the table content after trying to scroll using the scrollbar which is a bug. [Connect Item](
http://connect.microsoft.com/SQLServer/feedback/details/3106561/sql-server-manager-2016-bug-in-design-view) 
- Registered Servers not displaying icons after refreshing the root node.
- Script button for Create Database on Azure v12 servers executes script then displays message "No action to be scripted".
- SSMS Connect to Server dialog does not clear "Additional Properties" tab for each new connection.
- Generate Tasks script doesn't generate Create Database scripts for an Azure SQL DB.
- Scrollbar in View Designer appears disabled.
- Always Encrypted AVK key paths do not include version ids.
- Reduced number of engine edition queries in the query window. [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3113387)
- Always Encrypted errors from refreshing modules after encryption are incorrectly handled.
- Changed default connection timeout for OLTP and OLAP from 15 to 30 seconds to fix a class of ignored connection failures. 
- Fixed a crash in SSMS when custom report is launched. [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3118856)
- Fixed an issue where "Generate Script…" fails for Azure SQL databases.
- Fix "Script As" and "Generate Script Wizard" to not add extra newlines when scripting objects such as stored procedures. [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3115850)
- SQLAS PowerShell Provider: Add LastProcessed property to Dimension and MeasureGroup folders. [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3111879)
- Live Query Statistics: fixed issue where it was only showing the first query in a batch. [Connect Item] (http://connect.microsoft.com/SQLServer/feedback/details/3114221)  
- Showplan: show max instead of sum across the threads in properties window.
- Query Store: add new report on queries with high execution variation.
- Object explorer performance issues: [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3114074)
	- Context menu for tables momentarily hangs
	- SSMS is slow when right-clicking an index for a table (over a remote (Internet) connection). 
	- Avoid issuing table queries that sort on the server
- Removed Azure Deployment Wizard (Deploy Database to Azure VM) from SSMS
- Fixed issue where missing indexes were not shown in execution plans in SSMS [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3114194)
- Fixed common crash-on-shutdown issue in SSMS
- Fixed issue in Object Explorer where an error occurred when bringing up the context menu on the Polybase|Scale-Out Group nodes [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3115128)
- Fixed an issue where SSMS may crash when trying to display the permissions on a database
- Query Store: general enhancements in context menu items for result grids of query store report
- Configuring Always Encrypted for an existing table fails with errors on unrelated objects. [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3103181)
- Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect Item] (http://connect.microsoft.com/SQLServer/feedback/details/3109591)
- The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect Item] (http://connect.microsoft.com/SQLServer/feedback/details/3111925)
- When encrypting using Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.
- Fixed UI truncation issue on "New Server Registration" dialog
- Fix DMF Condition UI incorrectly updating expressions that contain string constant values with quotes in them
- Fixed an issue that may cause SSMS to crash when running custom reports
- Add “Execution in Scale Out…” menu item to the folder node
- Fixed an issue with Azure SQL DB firewall whitelist IP address feature
- Fixed an issue in SSMS which caused an Object reference not set exception when editing the source of AS multi-dimensional partition
- Fixed an issue in SSMS which caused an Object reference not set exception when deleting a customer assembly from multi-dimensional AS server
- Fixed an issue where renaming an AS tabular 1400 db failed
- Fixed an issue with scripting a 1400 compat-level AS tabular datasource from connection properties dialog
- Remove assumption that tables in AS 1400 compat-level model have at least one partition
- Ctrl-R now toggles results pane in SSMS DAX query editor


## ![download](../ssdt/media/download.png) [SSMS 16.5.3](http://go.microsoft.com/fwlink/?LinkID=840946)
Generally available | Build number: 13.0.16106.4

The following issues were fixed this release:

* Fixed an issue introduced in SSMS 16.5.2 which was causing the expansion of the 'Table' node when the table had more than one sparse column.

* Users can deploy SSIS packages containing OData Connection Manager which connect to a Microsoft Dynamics AX/CRM Online resource to SSIS catalog. For more information, see [OData Connection Manager](https://msdn.microsoft.com/library/dn584133.aspx).

* Configuring Always Encrypted on an existing table fails with errors on unrelated objects. [Connect ID 3103181](https://connect.microsoft.com/SQLServer/feedback/details/3103181/setting-up-always-encrypted-on-an-existing-table-fails-with-errors-on-unrelated-objects)

* Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect ID 3109591](https://connect.microsoft.com/SQLServer/feedback/details/3109591/sql-server-2016-always-encrypted-against-existing-database-with-multiple-schemas-doesnt-work)

* The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect ID 3111925](https://connect.microsoft.com/SQLServer/feedback/details/3111925/sql-server-2016-always-encrypted-encrypted-column-wizard-failed-task-failed-due-to-following-error-cannot-save-package-to-file-the-model-has-build-blocking-errors)

* When encrypting using Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.

* *Open recent* menu doesn't show recently saved files. [Connect ID 3113288](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)

* SSMS is slow when right-clicking an index for a table (over a remote (Internet) connection). [Connect ID 3114074](https://connect.microsoft.com/SQLServer/feedback/details/3114074/ssms-slow-when-right-clicking-an-index-for-a-table-over-a-remote-internet-connection)
 
* Fixed an issue with the SQL Designer scrollbar. [Connect ID 3114856](http://connect.microsoft.com/SQLServer/feedback/details/3114856/bug-in-scrollbar-on-sql-desginer-in-ssms-2016)

* Context menu for tables momentarily hangs 
 
* SSMS occasionally throws exceptions in Activity Monitor and crashes. [Connect ID 697527](https://connect.microsoft.com/SQLServer/feedback/details/697527/)

* SSMS 2016 crashes with error "The process was terminated due to an internal error in the .NET Runtime at IP 71AF8579 (71AE0000) with exit code 80131506"


## SSMS 16.5.1
Generally available | Build number: 13.0.16100.1

* Fixed an issue where Invoke-Sqlcmd erroneously inserts multiple rows when check constraint occurs. [Microsoft Connect Item: 811560](https://connect.microsoft.com/SQLServer/feedback/details/811560)

* Fixed an issue where non-ENU language versions do not work completely when creating Availability Groups.

* Fixed an issue where clicking query plan XML does not open the proper SSMS UI.


## ![download](../ssdt/media/download.png) [SSMS 16.5](http://go.microsoft.com/fwlink/?LinkID=832812)
Generally available | Build number: 13.0.16000.28

* Fixed an issue where a crash could occur when a database with table name containing “;:” was clicked on.
* Fixed an issue where changes made to the Model page in AS Tabular Database Properties window would script out the original definition. 
[Microsoft Connect Item: 3080744](https://connect.microsoft.com/SQLServer/feedback/details/3080744) 
* Fixed the issue that temporary files are added to the “Recent Files” list.  
[Microsoft Connect Item: 2558789](https://connect.microsoft.com/SQLServer/feedback/details/2558789)
* Fixed the issue that “Manage Compression” menu item is disabled for the user table nodes in object explorer tree.  
[Microsoft Connect Item: 3104616](https://connect.microsoft.com/SQLServer/feedback/details/3104616)

* Fixed the issue that user is not able to set the font size for object explorer, registered server explorer, template explorer as well as object explorer details. Font for the explorers will be using the Environment font.  
[Microsoft Connect Item: 691432](https://connect.microsoft.com/SQLServer/feedback/details/691432)

* Fixed the issue that SSMS always reconnect to the default database when connection is lost.  
[Microsoft Connect Item: 3102337](https://connect.microsoft.com/SQLServer/feedback/details/3102337)

* Fixed many of high dpi issues in policy management and query editor window including the execution plan icons.

* Fixed the issue that option to config font and color for Extended Event is missing.

* Fixed the issue of SSMS crashes that occur when closing the application or when it is trying to show the error dialog.


## ![download](../ssdt/media/download.png) [SSMS 16.4.1 (September 2016)](http://go.microsoft.com/fwlink/?LinkID=828615)
Generally available | Build number: 13.0.15900.1

*  Fixed an issue where attempting to ALTER/Modify a Stored Procedure fails:  
[Microsoft Connect item #3103831](https://connect.microsoft.com/SQLServer/feedback/details/3103831)

* New 'Read-SqlTableData', 'Read-SqlViewData', and 'Write-SqlTableData' cmdlets to view and write data using PowerShell.  
[Trello Read-SqlTableData Card](https://trello.com/c/FXVUNJ8x/131-read-sqltabledata)  
[Microsoft Connect item #2685363](https://connect.microsoft.com/SQLServer/feedback/details/2685363)
	
* New 'Add-SqlLogin' cmdlet to enable new login management scenarios using PowerShell.  
[Microsoft Connect item #2588952](https://connect.microsoft.com/SQLServer/feedback/details/2588952)
	
*  Improved support and usability for users connecting to various national clouds.
	
	
*  Fixed an issue where an Out Of Memory Exceptions were being thrown.  
[Microsoft Connect item #3062914](https://connect.microsoft.com/SQLServer/feedback/details/3062914)  
[Microsoft Connect item #3074856](https://connect.microsoft.com/SQLServer/feedback/details/3074856)
	
*  Fixed an issue where filtering by schema was not a valid filter option.  
[Microsoft Connect item #3058105](https://connect.microsoft.com/SQLServer/feedback/details/3058105)  
[Microsoft Connect item #3101136](https://connect.microsoft.com/SQLServer/feedback/details/3101136)
	
*  Fixed an issue where the Monitor window for a stretched database would not be accessible.
	
*  Fixed an issue where the F1 Help always opened online content. Users can now select whether they prefer online or offline help via the "Set Help Preference" in the Help menu.   
[Microsoft Connect item #2826366](https://connect.microsoft.com/SQLServer/feedback/details/2826366)
	
*  Fixed an issue where scripting out a 1200-level Analysis Services tabular model wouldn’t strip out the password for scripting, even though the server version had [client model object is now sync’d before scripting].
	
*  Fixed an issue where 'SELECT TOP N ROWS' option generated deprecated syntax for the the TOP operator.  
[Microsoft Connect item #3065435](https://connect.microsoft.com/SQLServer/feedback/details/3065435)
	
*  Fixed various layout issues throughout SSMS, including the Login Properties page and Advanced Query Execution Options.   
[Microsoft Connect item #3058199](https://connect.microsoft.com/SQLServer/feedback/details/3058199)  
[Microsoft Connect item #3079122](https://connect.microsoft.com/SQLServer/feedback/details/3058199)  
[Microsoft Connect item #3071384](https://connect.microsoft.com/SQLServer/feedback/details/3071384)
	
*  Fixed an issue where a solution was created automatically whenever a user opened a new query window.   
[Microsoft Connect item #2924667](https://connect.microsoft.com/SQLServer/feedback/details/2924667)    
[Microsoft Connect item #2917742](https://connect.microsoft.com/SQLServer/feedback/details/2917742)   
[Microsoft Connect item #2612635](https://connect.microsoft.com/SQLServer/feedback/details/2612635)
	
*  Fixed an issue where temporal tables could not be expanded in Object Explorer when in system databases.  
[Microsoft Connect item #2551649](https://connect.microsoft.com/SQLServer/feedback/details/2551649)
	
*  Fixed an issue where SSMS runs a query to SELECT @@trancount after executing a batch.    
[Microsoft Connect item #3042364](https://connect.microsoft.com/SQLServer/feedback/details/3042364)
	
*  Fixed an issue in Analysis Services where creating a script from a server's properties page resulted in a hidden connection dialog.
	
*  Fixed an issue where Ctrl+Q would not select the Quick Launch toolbar.
	
*  Fixed an issue where changing the MaxSize of a database using the Server Properties dialog was broken for databases > 2 TB.  
[Microsoft Connect item #1231091](https://connect.microsoft.com/SQLServer/feedback/details/1231091)
	
*  Fixed an issue where the Restore Database wizard wouldn't accept filenames with leading whitespaces:   
[Microsoft Connect item #2395147](https://connect.microsoft.com/SQLServer/feedback/details/2395147)



## ![download](../ssdt/media/download.png) [SSMS 16.3 (August 2016)](http://go.microsoft.com/fwlink/?LinkID=824938)
Generally available | Version number: 13.0.15700.28

* SSMS monthly releases are now branded numerically.

* [New authentication option **'Active Directory Universal Authentication'**](https://azure.microsoft.com/documentation/articles/sql-database-ssms-mfa-authentication/). This is a token-based authentication mechanism driven by Azure Active Directory that supports multi-factor, password, and integrated authentication mechanisms.

* New Extended Events templates matching the functionality of SQL Server Profiler templates [(Microsoft Connect item #2543925).](https://connect.microsoft.com/SQLServer/feedback/details/2543925/sql-server-extended-events-profiler-tool). Learn more about the included [SQL Server Profiler templates](https://msdn.microsoft.com/library/ms190176.aspx).

* New Create database and database properties dialogs for Azure SQL databases.

* New 'Get-SqlLogin' and 'Remove-SqlLogin' cmdlets to help perform SQL Server login management using PowerShell.  
*Linked customer bug requests:*   
[Microsoft Connect item #2588952.](https://connect.microsoft.com/SQLServer/feedback/details/2588952/)

* New PowerShell cmdlet 'New-SqlColumnMasterKeySettings' that adds support for creation of column master keys for arbitrary providers and key paths.

* New 'Create database' dialog to streamline creation of Azure SQL databases in SSMS>

* Support for filtering in the 'Databases' node of SSMS Object Explorer. Navigate to the 'Databases' node in Object explorer and click the filter icon in the Object explorer toolbar to filter the list of databases.

* Support for Azure-Resource Manager (ARM) type storage accounts in the Backup and Restore wizards.

* [Intial beta support for high-resolution displays](https://blogs.msdn.microsoft.com/sqlreleaseservices/ssms-highdpi-support/).  
*Linked customer bug requests:*   
[Microsoft Connect item #1129301](https://connect.microsoft.com/SQLServer/feedback/details/1129301/management-studio-is-unusable-on-a-4k-display), 
[Microsoft Connect item #1858763](https://connect.microsoft.com/SQLServer/feedback/details/1858763/), [Microsoft Connect item #1852671](https://connect.microsoft.com/SQLServer/feedback/details/1852671/), [Microsoft Connect item #1487643](https://connect.microsoft.com/SQLServer/feedback/details/1487643/),  [Microsoft Connect item #1355641](https://connect.microsoft.com/SQLServer/feedback/details/1355641/), [Microsoft Connect item #2161595](https://connect.microsoft.com/SQLServer/feedback/details/2161595/), [Microsoft Connect item #1854041](https://connect.microsoft.com/SQLServer/feedback/details/1854041/), [Microsoft Connect item #1055617](https://connect.microsoft.com/SQLServer/feedback/details/1055617/), [Microsoft Connect item #2448774](https://connect.microsoft.com/SQLServer/feedback/details/2448774/), [Microsoft Connect item #1521405](https://connect.microsoft.com/SQLServer/feedback/details/1521405/), [Microsoft Connect item #2117853](https://connect.microsoft.com/SQLServer/feedback/details/2117853/), [Microsoft Connect item #2014256](https://connect.microsoft.com/SQLServer/feedback/details/2014256/), [Microsoft Connect item #2162218](https://connect.microsoft.com/SQLServer/feedback/details/2162218/), [Microsoft Connect item #2344551](https://connect.microsoft.com/SQLServer/feedback/details/2344551/), [Microsoft Connect item #1664436](https://connect.microsoft.com/SQLServer/feedback/details/1664436/), [Microsoft Connect item #2554043](https://connect.microsoft.com/SQLServer/feedback/details/2554043/), [Microsoft Connect item #2983216](https://connect.microsoft.com/SQLServer/feedback/details/2983216/), [Microsoft Connect item #2021706](https://connect.microsoft.com/SQLServer/feedback/details/2021706/)

* Improvements in Database Engine Tuning Advisor (DTA) to support automatically reading a workload from the SQL Server Query Store.

* Improvements in Database Engine Tuning Advisor (DTA) to display index recommendations for clustered columnstore indexes, non-clustered columnstore indexes, and rowstore indexes.

* Support for sending Database Console (DBCC) commands using SQL Server Analysis Services PowerShell cmdlets.

* Bug fix to view cleartext of decrypted AlwaysEncrypted large object (LOB) columns in SSMS.  
*Linked customer bug requests:*   
[Microsoft Connect item #2413024](https://connect.microsoft.com/SQLServer/feedback/details/2413024/cannot-view-cleartext-of-alwaysencrypted-lob-columns-in-ssms)

* Bug fix in Always Encrypted dialog to fix crash when Windows visual styles aren't enabled (e.g. enabling high contrast display).

* Bug fix for 'Method not found' error preventing connection to SQL Server instances.

* Bug fix for SSMS crash when creating a partition function with datetime offset.

* Bug fix to add remove Microsoft .NET 3.5 requirement for starting Distributed Replay administration tool (DReplay.exe).

* Bug fix in Analysis Services Deployment wizard to support fully-qualified server names.

* Bug fix in SSMS to display partitions in Analysis Services tabular models with a 2016 compatibility model.  
*Linked customer bug requests:*   
[Microsoft Connect item #2845053](https://connect.microsoft.com/SQLServer/feedback/details/2845053/ssms-cannot-display-partitions-in-tabular-models-in-2016-compatibility-level) 

* Performance improvements and bug fixes in Analysis services tabular models, and SQL Server Shared Management Objects (SMO). 


---
## ![download](../ssdt/media/download.png) [SSMS July 2016 hotfix update](http://go.microsoft.com/fwlink/?LinkID=822301)
Generally available | Version number: 13.0.15600.2

* **Bug fix in SSMS to enable missing right-click menu items**.  
*Linked customer bug requests:*  
[Microsoft Connect item #2883440](https://connect.microsoft.com/SQLServer/feedback/details/2883440/lost-table-design-and-edit-top-n-rows-in-tables-context-menu)  
[Microsoft Connect item #2909644](https://connect.microsoft.com/SQLServer/feedback/details/2909644/ssms-2016-is-missing-edit-options-against-sql-express-2014)  
[Microsoft Connect item #2924345](https://connect.microsoft.com/SQLServer/feedback/details/2924345/some-ssms-object-explorer-right-click-menu-options-missing-in-july-update)

---
## SSMS July 2016 
Generally available | Version number: 13.0.15500.91

* *Edit, July 5:* Improved support for SQL Server 2016 (1200 compatibility level) tabular databases in the Analysis Services Process dialog and the Analysis Services deployment wizard.

* *Edit, July 5:* New option in SSMS 'query execution options' dialog to set 'XACT_ABORT'. This option is enabled by default in this release of SSMS and instructs SQL Server to rollback the entire transaction and abort the batch if a run-time error occurs.

* Support for Azure SQL Data Warehouse in SSMS.

* Significant updates to the SQL Server PowerShell module. This includes a new [SQL PowerShell module and new CMDLETs for Always Encrypted, SQL Agent, and SQL Error Logs](https://blogs.technet.microsoft.com/dataplatforminsider/2016/06/30/sql-powershell-july-2016-update).

* Support for PowerShell script generation in the Always Encrypted wizard.

* Significantly improved connection times to Azure SQL databases.

* New ‘Backup to URL’ dialog to support the creation of Azure storage credentials for SQL Server 2016 database backups. This dialog provides a more streamlined experience for storing database backups in an Azure storage account.
 
* New Restore dialog to streamline restoring a SQL Server 2016 database backup from the Microsoft Azure storage service.
 
* Bug fix in SSMS query designer to allow adding tables to the designer if a user doesn’t have SELECT permissions on them.

* Bug fix to add IntelliSense support for 'TRY_CAST()', and 'TRY_CONVERT()' functions.  
*Linked customer bug requests:*  
[Microsoft Connect item #2453461](https://connect.microsoft.com/SQLServer/feedback/details/2453461/sql-server-2012-issue-with-try-cast).

* Bug fix in PowerShell module to enable loading of ‘SQLAS’ Analysis Services extension.  
*Linked customer bug requests:*  
[Microsoft Connect item #2544902](https://connect.microsoft.com/SQLServer/feedback/details/2544902/ssms-march-2016-refresh-sqlps-failed-to-load-the-sqlas-extension).

* Bug fix in the SSMS editor window to allow drag-and-drop open of Sql files.  
*Linked customer bug requests:*  
[Microsoft Connect item #2690658](https://connect.microsoft.com/SQLServer/feedback/details/2690658/cannot-drag-sql-files-into-management-studios).

* Bug fix in Profiler to fix Profiler crash when exiting.  
*Linked customer bug requests:*  
[Microsoft Connect item #2616550](https://connect.microsoft.com/SQLServer/feedback/details/2616550/sql-server-2016-rc2-profiler-version-13-0-1300-275-wont-close-after-trace-is-started-even-after-trace-is-stopped).  
[Microsoft Connect item #2319968](https://connect.microsoft.com/SQLServer/Feedback/Details/2319968).

* Bug fix in SSMS to prevent crash when trying to edit a join link in the SSMS table designer.  
*Linked customer bug requests:*  
[Microsoft Connect item #2721052](https://connect.microsoft.com/SQLServer/feedback/details/2721052/ssms-view-design-mode-right-click-on-join-crashes-ssms).

* Bug fix in SSMS to enable database script generation for db_owner role members.  
*Linked customer bug requests:*  
[Microsoft Connect item #2869241](https://connect.microsoft.com/SQLServer/feedback/details/2869241/error-with-script-database-as-create-to-in-ssms-2008r2-and-ssms-2016-june).

* Bug fix in SSMS editor to remove the delay in closing a query tab if the server has gone offline.  
*Linked customer bug requests:*  
[Microsoft Connect item #2656058](https://connect.microsoft.com/SQLServer/feedback/details/2656058/ssms-2014-2016-query-tab-takes-significantly-longer-to-close-if-the-instance-it-was-connected-to-is-now-offline).

* Bug fix to enable Backup option in SQL Server Express databases. 
*Linked customer bug requests:*  
[Microsoft Connect item #2801910](https://connect.microsoft.com/SQLServer/feedback/details/2801910/ssms-2016-backup-option-not-appearing-in-tasks).  
[Microsoft Connect item #2874434](https://connect.microsoft.com/SQLServer/feedback/details/2874434/backup-missing-from-tasks-context-menu-in-ssms-2016-when-you-are-connected-to-an-express-instance).

* Bug fix in Analysis Services to correctly show the Data Feed provider for multi-dimensional Analysis Services models.

----
## ![download](../ssdt/media/download.png) [SSMS June 2016](http://go.microsoft.com/fwlink/?LinkID=799832)
Generally available | Version number: 13.0.15000.23

* SSMS is generally available starting with the June 2016 release.

* New quick find dialog in SSMS that is better integrated into the current document and allows searching via regular expressions. 
*Linked customer bug requests:*  
<https://connect.microsoft.com/SQLServer/feedback/details/2735513/quick-find-replace-in-ssms-2016-rc3/>

* Improvements in SSMS installer to allow you to track installation progress and process exit codes for unattended installations via scripts.

* Bug fix in SSMS context-sensitive F1 help to correctly display help documents and articles.

* Bug fix in Query Data Store 'Regressed Queries' view that caused SSMS to crash when scrolling.

* Bug fix in Excel Analysis Services OLEDB connector to allow connections from Excel 2016 to SQL Server Analysis Services.

* Bug fix in SSMS Connection dialog to show the connection dialog on the same monitor as the main SSMS window in multi-monitor systems.  
*Linked customer bug requests:*  
<https://connect.microsoft.com/SQLServer/feedback/details/724909/connection-dialog-appears-off-screen/>
<https://connect.microsoft.com/SQLServer/feedback/details/755689/sql-server-management-studio-connect-to-server-popup-dialog/>  
<https://connect.microsoft.com/SQLServer/feedback/details/389165/sql-server-management-studio-gets-confused-dealing-with-multiple-displays/>

* Bug fixes in Always Encrypted experience. Fixed bug where Always Encrypted menu option was not enabled correctly for Stretch databases. Also fixed bug in the Always Encrypted wizard where it was not properly using the SafeNet (Luna SA) HSM provider.


## Additional Downloads  
For a list of all SQL Server Management Studio downloads, search the [Microsoft Download Center](https://www.microsoft.com/en-us/download/search.aspx?q=sql%20server%20management%20studio&p=0&r=10&t=&s=Relevancy~Descending).  
  
For the latest release of SQL Server Management Studio, see [Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md).  
