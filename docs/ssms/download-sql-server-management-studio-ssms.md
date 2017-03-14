---
title: "Download SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "install ssms, download ssms, latest ssms"
  - "SQL Server Management Studio"
  - "ssms.exe"
  - "sql man studio"
  - "sql management studio"
  - "sql management studio install"
  - "download sql management studio"
  - "ms sql management studio"
  - "install sql management studio"
  - "ssms download"
  - "sql server ssms"
  - "ssms express"
ms.assetid: adafeeef-4255-4924-8042-02f503d599ca
caps.latest.revision: 145
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Download SQL Server Management Studio (SSMS)
SQL Server Management Studio (SSMS) is an integrated environment for accessing, configuring, managing, administering, and developing all components of SQL Server. SSMS combines a broad group of graphical tools with a number of rich script editors to provide developers and administrators of all skill levels access to SQL Server. This release features improved compatibility with previous versions of SQL Server, a stand-alone web installer, and toast notifications within SSMS when new releases become available.  

    
| ![download](../ssdt/media/download.png) Download SQL Server Management Studio (SSMS)  |  |
|:---|:---|
|**[Download SQL Server Management Studio (16.5.3)](https://go.microsoft.com/fwlink/?LinkID=840946)**|Current release for production use.|
|**[Download SQL Server Management Studio - Release Candidate (17.0 RC3)](../ssms/sql-server-management-studio-ssms-release-candidate.md)**|Includes support for SQL Server vNext CTP1.3, and works side-by-side with 16.x, but not recommended for production use.| 


> [!NOTE]
> * SSMS releases are now branded numerically, not by months.
> * This generally available release of SSMS is free and does not require a SQL Server license to install and use.  


## SQL Server Management Studio   
**Version Information**  
  
The release number: 16.5.3  
The build number for this release: 13.0.16106.4
  
**Supported SQL Server versions**  
  
* This version of SSMS works with all [supported versions of SQL Server (SQL Server 2008 - SQL Server 2016),](https://support.microsoft.com/en-us/lifecycle?C2=1044) and provides the greatest level of support for working with the latest cloud features in Azure SQL Database, and Azure SQL Data Warehouse.  
* There is no explicit block for SQL Server 2000 or SQL Server 2005, but some features may not work properly.  
* Additionally, one SSMS 16.x release or SSMS 2016 can be installed side by side with previous versions of SSMS 2014 and earlier. 
  
**Supported Operating systems**  
  
This release of SSMS supports the following platforms when used with the latest available service pack:   
 Windows 10, Windows 8, Windows 8.1, Windows 7 (SP1), Windows Server 2012 (64-bit), Windows Server 2012 R2 (64-bit), Windows Server 2008 R2 (64-bit)  
   
 **Available Languages**  
> [!NOTE]  
> Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2. 
  
 This release of SSMS can be installed in the following languages:  
[Chinese (People's Republic of China)](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x804) | [Chinese (Taiwan)](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x404) | [English (United States)](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x409) | [French](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40c)  
[German](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x407) | [Italian](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x410) | [Japanese](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x411) | [Korean](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x412) | [Portuguese (Brazil)](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x416) | [Russian](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x419) | [Spanish](http://go.microsoft.com/fwlink/?linkid=840946&clcid=0x40a)  

 
## Changelog  

16.5.3

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





For the full list of features, see   
                [SQL Server Management Studio - Changelog (SSMS)](../ssms/sql-server-management-studio-changelog-ssms.md)  
  
To see the list of known issues and work arounds, see   
                [SQL Server Management Studio -  Release Notes](../ssms/sql-server-management-studio-release-notes.md)  
  
For information about user data collection, see   
                [SQL Server Privacy Statement](http://www.microsoft.com/privacystatement/en-us/SQLServer/Default.aspx).  
  
## Previous releases  
[Previous SQL Server Management Studio Releases](../ssms/previous-sql-server-management-studio-releases.md)  
  
## Feedback  
  
![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback).  
  
## See Also  
[Tutorial: SQL Server Management Studio](http://msdn.microsoft.com/en-us/d2bade70-07cf-4d94-b5d2-88aecb538ed1)  
[SQL Server Management Studio documentation](https://msdn.microsoft.com/library/hh213248(v=sql.130).aspx)  
[Microsoft SQL Server](https://msdn.microsoft.com/library/bb545450.aspx)  
[Additional updates and service packs](https://technet.microsoft.com/sqlserver/ff803383.aspx)  
[Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)  


