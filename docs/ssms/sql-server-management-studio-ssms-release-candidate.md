---
title: "SQL Server Management Studio (SSMS) - Release Candidate | Microsoft Docs"
ms.custom: ""
ms.date: "02/01/2017"
ms.prod: "sql-vnext"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a4561ad6-5cfc-4c22-91a8-cf880fcbf233
caps.latest.revision: 4
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# SQL Server Management Studio (SSMS) - Release Candidate
Welcome to the current release candidate (17.0 RC2) of SQL Server Management Studio!  This release candidate of SQL Server Management Studio (SSMS) includes support for [SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx). 

## Download

SSMS release candidate 17.0 RC2 works side-by-side with our [generally available release &#40;16.x&#41;](../ssms/download-sql-server-management-studio-ssms.md), but is not recommended for production use. 
  
![download](../ssdt/media/download.png)[ Download SQL Server Management Studio 17.0 RC2](https://go.microsoft.com/fwlink/?linkid=840957).  
  
## Enhancements 

- Added Parameterization for Always Encrypted, please refer to [this page](https://blogs.msdn.microsoft.com/sqlsecurity/2016/12/13/parameterization-for-always-encrypted-using-ssms-to-insert-into-update-and-filter-by-encrypted-columns/) for more details 
- AAD Universal auth connection to Azure SQL DB supports custom tenant id 
- Generate scripts for Azure SQL Database, now scripts full text, rules, and database
- Branding fixes in splash screens for SSMS and Profiler
- Removed Utility Control Point UI from SSMS
- Added Presenter mode to SSMS
- Updated numerous SSMS icons to support High-DPI and new UI theme (not all icons are complete in RC 2)
- Introduced functionality in Showplan Comparison feature to find significant differences in Cardinality Estimation between matching nodes of two query plans and perform basic analysis of the possible root causes.

    
## Bug fixes

- Backup/restore container dialogs come up offscreen on multiple monitor setups. 
- SecurityPolicy create fails if target object has ] in its name.
- SSMS 2016 "Open recent" menu doesn't show recently saved files. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)
- Removed reset of user settings when VS Shell is updated.
- Fixed an issue that was preventing the user from being able to change Compatibility Level of a database on SQL vNext CTP1.
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
- Reduced number of engine edition queries in the query window. [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3113387)
- Always Encrypted errors from refreshing modules after encryption are incorrectly handled.
- Changed default connection timeout for OLTP and OLAP from 15 to 30 seconds to fix a class of ignored connection failures. 
- Fixed a crash in SSMS when custom report is launched. [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3118856)
- Fixed an issue where "Generate Script…" fails for Azure SQL databases.
- Fix "Script As" and "Generate Script Wizard" to not add extra newlines when scripting objects such as stored procedures. [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3115850)
- SQLAS PowerShell Provider: Add LastProcessed property to Dimension and MeasureGroup folders. [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3111879)
- Live Query Statistics: fixed issue where it was only showing the first query in a batch. [Connect Item] (https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3114221)  
- Showplan: show max instead of sum across the threads in properties window.
- Query Store: add new report on queries with high execution variation.
- Object explorer performance issues: [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3114074)
	- Context menu for tables momentarily hangs
	- SSMS is slow when right-clicking an index for a table (over a remote (Internet) connection). 
	- Avoid issuing table queries that sort on the server
- Removed Azure Deployment Wizard (Deploy Database to Azure VM) from SSMS
- Fixed issue where missing indexes were not shown in execution plans in SSMS [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3114194)
- Fixed common crash-on-shutdown issue in SSMS
- Fixed issue in Object Explorer where an error occurred when bringing up the context menu on the Polybase|Scale-Out Group nodes [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3115128)
- Fixed an issue where SSMS may crash when trying to display the permissions on a database
- Query Store: general enhancements in context menu items for result grids of query store report
- Configuring Always Encrypted for an existing table fails with errors on unrelated objects. [Connect Item](https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3103181)
- Configuring Always Encrypted for an existing database with multiple schemas doesn't work. [Connect Item] (https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3109591)
- The Always Encrypted, Encrypted Column wizard fails due to the database containing views that reference system views. [Connect Item] (https://connectadmin/Feedback/ConnectTab.aspx?FeedbackID=3111925)
- When encrypting using Always Encrypted, errors from refreshing modules after encryption are incorrectly handled.
- Fixed UI truncation issue on "New Server Registration" dialog
- Fix DMF Condition UI incorrectly updating expressions that contain string constant values with quotes in them
- Fixed an issue that may cause SSMS to crash when running custom reports
- Add “Execution in Scale Out…” menu item to the folder node
    
## Feedback  
  
![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback).  
  
## See Also  
[SQL Server Management Studio Tutorial](../ssms/use-sql-server-management-studio.md)  
[What's new in SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx)