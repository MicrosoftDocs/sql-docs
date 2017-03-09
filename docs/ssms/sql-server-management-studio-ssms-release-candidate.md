---
title: "SQL Server Management Studio (SSMS) - Release Candidate | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
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
Welcome to the current release candidate (17.0 RC3) of SQL Server Management Studio!  This release candidate of SQL Server Management Studio (SSMS) includes support for [SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx). 

## Download

SSMS release candidate 17.0 RC3 works side-by-side with our [generally available release &#40;16.x&#41;](../ssms/download-sql-server-management-studio-ssms.md), but is not recommended for production use. 
  
![download](../ssdt/media/download.png)[ Download SQL Server Management Studio 17.0 RC3](https://go.microsoft.com/fwlink/?linkid=844503).  
![download](../ssdt/media/download.png)[ Download SQL Server Management Studio 17.0 RC3 Upgrade Package](https://go.microsoft.com/fwlink/?linkid=844505).  
  
## Enhancements 

- New Upgrade Pacakge
	- Upgrades previous 17.X installations to current version  
	- Provides smaller download size  
	- see known issues below for issues specific to upgrading RC 2 to RC 3
- Icon Updates
	- Final set of icon updates for SSMS 17.0
	- New SSMS and Profiler program icons to differentiate between 16.X and 17.X versions
- Presentation Mode
	- 3 new tasks available via Quick Launch (Ctr-Q)
	- PresentOn - Turn on presentation mode
	- PresentEdit - Edit the presentation font sizes for presentation mode.  "Text Editor font" for the Query Editor.  "Environment font" for other components.
	- RestoreDefaultFonts - Revert back to default settings.
	- *Note: there is currently no PresentOff command at this time.  Use RestoreDefaultFonts to turn off Presentation Mode*
- SQL PowerShell Module 
	- Miscellaneous improvement around the "presentation" (formatting) of some SMO objects (e.g. databases now show the size and the available space and tables show row count and space usage)
	- Colorization when the PowerShell command prompt is invoked from the "Start PowerShell" menu in OE
	- Added -ClusterType and -RequiredCopiesToCommit parameter to AG cmdlets (New-SqlAvailabilityGroup, Join-SqlAvailabilityGroup, and Set-SqlAvailabilityGroup cmdlets)
	- Added parameters -ActiveDirectoryAuthority and -AzureKeyVaultResourceId to Add-SqlAzureAuthenticationContext cmdlet
- SQL Server on Linux
	- General improvements and fixes for Log Shipping
- Analysis Server DAX Query Editor with colorization and IntelliSense
- New "Add Unique Constraint" template
- Showplan
	- Show max instead of sum across the threads in properties window for elapsed time
	- Expose new mem grant operator properties
	- Enabled the "Edit Query" button in Live Query Statistics
	- Support for interleaved execution
- Removed Configuration Manager from Registered Servers explorer
- Enable reading audit logs from Azure blob storage

## Bug fixes

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
- Improved contrast of hyperlinks on SSMS Setup page
- Fixed an issue where Polybase nodes were not displayed when connected to SQL Server Express (2016 SP1)
- Fixed an issue where SSMS is unable to change the Compatibility Level of an Azure DB to v140
- Improved performance of Object Explorer when expanding the list of Azure databases [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3100675)
- Fixed an issue where "View SQL Server Log" context menu item appeared incorrectly for non-relational server types (AS\RS\IS) 
- Fixed an issue where checking syntax of an Analysis Services partition query using SQL auth could result in login failed message
- Fixed an issue where renaming a preview 1400 compat-level AS tabular model would fail in SSMS
- Fixed an “operation failed on model” issue that could occur after attempting an invalid operation on the AS server in rare circumstances, revert local changes after unsuccessful save on the model
- Fixed a typo in Analysis Services Synchronize Database popup dialog

## Known issues

- Upgrade from RC2 to RC3 
	- Unable to access AS, RS, and IS menu options
	- Profiler and Database Tuning Engine Advisor missing from "Tools" menu
	- *Workaround: re-run SSMS 17.0 RC 3 Setup and choose to "Repair"*
	
- SQL on Linux support:
	- Support for native path on "SQL on Linux" will be in an upcoming upate of SSMS
	- Currently some scenarios in SSMS may not work as expected. For example:
		- The path displayed in the "Backup Database" form will look like C:\var\opt\... (instead of the native Linux path)
		- Clicking on the "Content" button on the Backup Database wizard will cause an error.
    
## Feedback  
  
![need help](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback).  
  
## See Also  
[SQL Server Management Studio Tutorial](../ssms/use-sql-server-management-studio.md)  
[What's new in SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx)
