---
title: "SQL Server Management Studio (SSMS) - Release Candidate | Microsoft Docs"
ms.custom: ""
ms.date: "2016-12-16"
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
---
# SQL Server Management Studio (SSMS) - Release Candidate
Welcome to the current release candidate (17.0 RC2) of SQL Server Management Studio!  This release candidate of SQL Server Management Studio (SSMS) includes support for [SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx). 

## Download

SSMS release candidate 17.0 RC2 works side-by-side with our [generally available release &#40;16.x&#41;](../ssms/download-sql-server-management-studio-ssms.md), but is not recommended for production use. 
  
![download](../ssdt/media/download.png)[ Download SQL Server Management Studio 17.0 RC2](https://go.microsoft.com/fwlink/?LinkID=835608).  
  
## Release notes 

- Added Parameterization for Always Encrypted, please refer to [this page](https://blogs.msdn.microsoft.com/sqlsecurity/2016/12/13/parameterization-for-always-encrypted-using-ssms-to-insert-into-update-and-filter-by-encrypted-columns/) for more details 
- AAD Universal auth connection to Azure SQL DB supports custom tenant id 
- Generate scripts for Azure SQL Database, now scripts full text, rules, and database
    
## Bug fixes

**RC2**

- Backup/restore container dialogs come up offscreen on multimon setups. 8929757 
- SecurityPolicy create fails if target object has ] in its name. 8982544 
- SSMS 2016 "Open recent" menu doesn't show recently saved files. 9006961 [Link](https://connect.microsoft.com/SQLServer/feedback/details/3113288/ssms-2016-open-recent-menu-doesnt-show-recently-saved-files)
- SSMS settings scorch no longer needed. 8538541 
- Modernize splash screens for both SSMS and Profiler. 8955228 
- Fixed an issue that was preventing the user from being able to change Compatibility Level of a database on SQL vNext CTP1. 8985472 
- Query windows using AAD Universal authentication cannot refresh the query after an hour. 8995371 
- Utility Control Point UI removed from SSMS. 9000043 
- AD Universal auth connections fail to query data after the initial token expiration. 8976481 
- Unable to script Rules from Azure SQL DB to Azure SQL DB. 8355825 


**RC1**

- Fixed issue were SQL PowerShell was not able to connect legacy SQL instances (2014 and older). 8389011 [Link](https://connect.microsoft.com/SQLServer/feedback/details/1138754/sql-server-sqlps-powershell-module-fails-connection-to-sql-2012-instance)
- Added PresenterMode to SSMS. 8718423
- Fixed an issue that was causing SSMS to crash when failing to import registered servers. 8820172 
- Fixed an issue that was causing SSMS to crash if a user has certain permissions an a database. 8820291 
- SSMS - tables disappears from design surface while reviewing views. 7974694 [Link](https://connect.microsoft.com/SQLServer/feedback/details/2946125/ssms-tables-disappears-from-design-surface-while-reviewing-views) 
- The table scrollbar does not allow the user to scroll the table content, only the up/down Arrow allow this. Its also possible to scroll the table content after trying to scroll using the scrollbar which is a bug. 8684454 [Link](http://connect.microsoft.com/SQLServer/feedback/details/3106561/sql-server-manager-2016-bug-in-design-view) 
    
## Feedback  
  
![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback).  
  
## See Also  
[SQL Server Management Studio Tutorial](../ssms/use-sql-server-management-studio.md)  
[What's new in SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx)