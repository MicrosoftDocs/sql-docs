---
title: "Previous SQL Server Management Studio Releases | Microsoft Docs"
ms.custom: ""
ms.date: "01/30/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 87f593c3-8b76-4c12-963a-31d35f2fb294
caps.latest.revision: 18
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Previous SQL Server Management Studio Releases
  
The following previous releases of SQL Server Management Studio are available.
   
## ![download](../ssdt/media/download.png) [SQL Server Management Studio 16.5.3 release](http://go.microsoft.com/fwlink/?LinkID=840946)

**Version Information**  
  
*This release of SSMS uses the Visual Studio 2015 Isolated shell.*  
The release number: 16.5.3  
The build number for this release: 13.0.16106.4

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



## ![download](../ssdt/media/download.png) [SQL Server Management Studio 16.5 release](http://go.microsoft.com/fwlink/?LinkID=832812)

**Version Information**  
  
*This release of SSMS uses the Visual Studio 2015 Isolated shell.*  
The release number: 16.5  
The build number for this release: 13.0.16000.28

**Known Issues with this Build**  

1. Invoke-Sqlcmd erroneously inserts multiple rows when check constraint occurs. [Microsoft Connect Item: 811560](https://connect.microsoft.com/SQLServer/feedback/details/811560)

2. Non-ENU language versions do not work completely when creating Availability Groups.

3. Clicking query plan XML does not open the proper SSMS UI.

**Changelog**

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


   
## ![download](../ssdt/media/download.png) [SQL Server Management Studio 16.4.1 (September 2016) release](http://go.microsoft.com/fwlink/?LinkID=828615)

**Version Information**  
  
*This release of SSMS uses the Visual Studio 2015 Isolated shell.*  
The release number: 16.4.1  
The build number for this release: 13.0.15900.1
  
**Known Issues with this Build**  

1. **Only a single Azure Active Directory account can log in for an SSMS instance using Active Directory Universal Authentication.**  
    This restriction is limited to Active Directory Universal Authentication - you can log in to different servers using Active Directory Password Authentication, Active Directory Integrated Authentication or SQL Server Authentication.
    
    As a workaround, you can use another instance of SSMS to login as another Azure Active Directory account. 
    
2. **Data-Tier Application Framework (DacFx) commands and the Schema Designer in SSMS do not support Active Directory Universal Authentication.**  
    Commands that use DacFx (e.g. import and export), the schema designer in SSMS do not currently support Active Directory Universal Authentication.
    
    As a workaround, you can use the other forms of authentication provided in SSMS - Active Directory Password Authentication, Active Directory Integrated Authentication or SQL Server Authentication.

3. **SSMS can only connect to SQL Server 2016 Integrated Services (SSIS 2016) instances.**  
    There is a known compatibility limitation with SQL Server Integration Services that prevents connecting to previous versions.
    
    As a workaround for this problem, you can connect to your SQL Server Integration Service instance by using the [SSMS release aligned with your SSIS instance.](../ssms/previous-sql-server-management-studio-releases.md) 
  
4. **SSMS won't save maintenance plans for SQL Server 2008 R2 and earlier SQL Server versions.**  
    This is a known limitation that we hope to address in the future. In the meantime, you can use the [SSMS 2014 release](../ssms/previous-sql-server-management-studio-releases.md) to save the maintenance plans.  
    
5. **Non-English SSMS installations may require the installation of an additional security package.**  
Non-English localized releases of SSMS [require the KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.
  
6. **SQL Server Configuration Manager will fail to launch if there is no SQL Server installed on the client machine**  
    If you do not have SQL Server installed on your client machine and launch SQL Server Configuration Manager, you will see the following error:   
     `Cannot connect to WMI provider. You do not have permission or the server is unreachable. Note that you can only manage SQL Server 2005 and later servers with SQL Server Configuration Manager. Invalid namespace \[0x8004100e]`,   
   
     * If you have added your SQL Server instances to 'Registered Servers' list in SSMS:  
        1. Navigate to 'Registered Servers' view in SSMS.  
        2. Right-click the SQL Server instance you would like to configure.  
        3. Select 'SQL Server Configuration Manager...' from the right-click menu.    
          
      * If you have not added a SQL Server instance to 'Registered Servers' list in SSMS:  
        1. Open a command promopt as Administrator.  
        2. Run the Mofcomp tool using the following command:  
    `mofcomp "%programfiles(x86)%\Microsoft SQL Server\130\Shared\sqlmgmproviderxpsp2up.mof"`  
        3. After you run the Mofcomp tool, run the following commands to restart the WMI service for the changes to take effect:  
        `net stop "Windows Management Instrumentation"`  
        `net start “Windows Management Instrumentation”`  

 
**Changelog** 

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
	
*  Fixed an issue in Analysis Services where creating a script from an server's properties page resulted in a hidden connection dialog.
	
*  Fixed an issue where Ctrl+Q would not select the Quick Launch toolbar.
	
*  Fixed an issue where changing the MaxSize of a database using the Server Properties dialog was broken for databases > 2 TB.  
[Microsoft Connect item #1231091](https://connect.microsoft.com/SQLServer/feedback/details/1231091)
	
*  Fixed an issue where the Restore Database wizard wouldn't accept filenames with leading whitespaces:   
[Microsoft Connect item #2395147](https://connect.microsoft.com/SQLServer/feedback/details/2395147)

*  Fixed an issue where the Restore Database wizard wouldn't accept filenames with leading whitespaces:   
[Microsoft Connect item #2395147](https://connect.microsoft.com/SQLServer/feedback/details/2395147)



## ![download](../ssdt/media/download.png) [SQL Server Management Studio 16.3 (August 2016) release](http://go.microsoft.com/fwlink/?LinkID=824938)
 August 15, 2016 | Version number:	13.0.15700.28

**Features**  
1. New authentication option 'Active Directory Universal Authentication'
2. New cmdlets for SQL Server PowerShell module
3. Improvements to Database Engine Tuning Advisor (DTA) and Extended Events templates
4. Beta support for [High Resolution Display in SSMS](https://blogs.msdn.microsoft.com/sqlreleaseservices/ssms-highdpi-support/)

[More information on features available in the SSMS changelog.](../ssms/sql-server-management-studio-changelog-ssms.md)

**Known Issues**

The following are issues and limitations with this release of SQL Server Management Studio:  

1. **Only a single Azure Active Directory account can log in for an SSMS instance using Active Directory Universal Authentication.**  
    This restriction is limited to Active Directory Universal Authentication - you can log in to different servers using Active Directory Password Authentication, Active Directory Integrated Authentication or SQL Server Authentication.
    
    As a workaround, you can use another instance of SSMS to login as another Azure Active Directory account. 
    
2. **Data-Tier Application Framework (DacFx) commands and the Schema Designer in SSMS do not support Active Directory Universal Authentication.**  
    Commands that use DacFx (e.g. import and export), the schema designer in SSMS do not currently support Active Directory Universal Authentication.
    
    As a workaround, you can use the other forms of authentication provided in SSMS - Active Directory Password Authentication, Active Directory Integrated Authentication or SQL Server Authentication.

3. **SSMS can only connect to SQL Server 2016 Integrated Services (SSIS 2016) instances.**  
    There is a known compatibility limitation with SQL Server Integration Services that prevents connecting to previous versions.
    
    As a workaround for this problem, you can connect to your SQL Server Integration Service instance by using the [SSMS release aligned with your SSIS instance.](../ssms/previous-sql-server-management-studio-releases.md) 
  
4. **SSMS won't save maintenance plans for SQL Server 2008 R2 and earlier SQL Server versions.**  
    This is a known limitation that we hope to address in the future. In the meantime, you can use the [SSMS 2014 release](../ssms/previous-sql-server-management-studio-releases.md) to save the maintenance plans.  
    
5. **Non-English SSMS installations may require the installation of an additional security package.**  
Non-English localized releases of SSMS [require the KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.
  
6. **SQL Server Configuration Manager will fail to launch if there is no SQL Server installed on the client machine**  
    If you do not have SQL Server installed on your client machine and launch SQL Server Configuration Manager, you will see the following error:   
     `Cannot connect to WMI provider. You do not have permission or the server is unreachable. Note that you can only manage SQL Server 2005 and later servers with SQL Server Configuration Manager. Invalid namespace \[0x8004100e]`,   
   
     * If you have added your SQL Server instances to 'Registered Servers' list in SSMS:  
        1. Navigate to 'Registered Servers' view in SSMS.  
        2. Right-click the SQL Server instance you would like to configure.  
        3. Select 'SQL Server Configuration Manager...' from the right-click menu.    
          
      * If you have not added a SQL Server instance to 'Registered Servers' list in SSMS:  
        1. Open a command promopt as Administrator.  
        2. Run the Mofcomp tool using the following command:  
    `mofcomp "%programfiles(x86)%\Microsoft SQL Server\130\Shared\sqlmgmproviderxpsp2up.mof"`  
        3. After you run the Mofcomp tool, run the following commands to restart the WMI service for the changes to take effect:  
        `net stop "Windows Management Instrumentation"`  
        `net start “Windows Management Instrumentation”`  


**Fixes**
1. Bug fix to view cleartext of decrypted AlwaysEncrypted large object (LOB) columns in SSMS (Microsoft Connect item #2413024).
2. Bug fix in Always Encrypted dialog to fix crash when Windows visual styles are not enabled (e.g. enabling high contrast display).
3. Bug fix for 'Method not found' error preventing connection to SQL Server instances.
4. Bug fix for SSMS crash when creating a partition function with datetime offset.
5. Bug fix to remove Microsoft .NET 3.5 requirement for starting Distributed Replay administration tool (DReplay.exe).
6. Bug fix in Analysis Services Deployment wizard to support fully-qualified server names.
7. Bug fix in SSMS to display partitions in Analysis Services tabular models with a 2016 compatibility model (Microsoft Connect item #2845053).

[More information on fixes available in the SSMS changelog.](../ssms/sql-server-management-studio-changelog-ssms.md)
 
---
## ![download](../ssdt/media/download.png) [SQL Server Management Studio July 2016 hotfix update release](http://go.microsoft.com/fwlink/?LinkID=822301)

July 13, 2016 | Version number: 13.0.15600.2

**Features**  
1. Support for Azure SQL Data Warehouse in SSMS.   
2. Significant updates to the SQL Server PowerShell module.   
3. Significantly improved connection times to Azure SQL databases.  
4. Improved support for SQL Server 2016 (1200 compatibility level) tabular databases in the Analysis Services process dialog, and many more.   

[More information and features available in the SSMS changelog.](../ssms/sql-server-management-studio-changelog-ssms.md)

**Known Issues** 
1. **The installer for the SSMS July hotfix release shows up as SSMS August release.**
The setup page for the July Update hotfix says August due to an internal build setting. This package is in fact a hotfix for the SSMS July release. 
2. **SSMS cannot connect to SQL Server instances after installing the 'July 2016 hotfix' release.**
We are aware of an issue regarding the latest SSMS update where attempting to connect to a server results in the following error message: 
   ```
    "Method not found: 'Void Microsoft.SqlServer.Management.Common.SqlConnectionInfo.set_ApplicationIntent(System.String)'"
    ```
  
    The fix for this problem will be available in the next SSMS release. As a workaround for this issue, you can uninstall and reinstall SSMS. For more details, [visit this Microsoft Connect thread on the issue](https://connect.microsoft.com/SQLServer/feedback/details/2925257/not-able-to-connect-to-sql-server-instances-after-installing-ssms-2016-july-update).
    
3. **SSMS crashes when trying to select an Azure storage account.**
The SSMS July release and July hotfix release crash if you try to select an Azure storage account and do not have a 'classic' storage account.
The fix for this issue will be available in an upcoming SSMS release. As a workaround for this problem, you can backup/restore your databases to an Azure storage account by creating a classic storage account, or by [using T-SQL to backup or restore](https://msdn.microsoft.com/library/jj720552(v=sql.120).aspx).

4. **SSMS will only display 'classic' Azure storage accounts in the Backup/Restore wizards.**
The SSMS July release and July hotfix release display only 'classic' Azure storage accounts for new credential creation if you're trying to backup or restore using the backup or restore wizards.
The fix for this issue will be available in an upcoming SSMS release. As a workaround for this problem, you can backup/restore your databases to the available Azure 'classic' storage account, or backup to the 'ARM-type' storage accounts [using T-SQL to backup or restore](https://msdn.microsoft.com/library/jj720552(v=sql.120).aspx). 

5. **Non-English SSMS installations may require the installation of an additional security package.**
Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.
6. **SSMS can only connect to SQL Server 2016 Integrated Services (SSIS 2016) instances.** There is a known compatibility limitation with SQL Server Integration Services that prevents connecting to previous versions.
As a workaround for this problem, you can connect to your SQL Server Integration Service instance by using the SSMS release aligned with your SSIS instance.
7. **SSMS won't save maintenance plans for SQL Server 2008 R2 and earlier SQL Server versions.** We are working on a fix for this issue. In the meantime, you can use the SSMS 2014 release below to save the maintenance plans.
8. **SQL Server Configuration Manager will fail to launch if there is no SQL Server installed on the client machine.** If you do not have SQL Server installed on your client machine and launch SQL Server Configuration Manager, you will receive the 'Cannot connect to WMI provider' error. As a workaround:
    * Open a command prompt as Administrator.
    * Run the Mofcomp tool using the following command:  
      mofcomp "%programfiles(x86)%\Microsoft SQL Server\130\Shared\sqlmgmproviderxpsp2up.mof"
    * After you run the Mofcomp tool, run the following commands to restart the WMI service for the changes to take effect:  
       net stop "Windows Management Instrumentation"  
       net start "Windows Management Instrumentation"

**Fixes**  
1. Bug fix in SSMS query designer to allow adding tables to the designer if a user doesn't have SELECT permissions on them.
2. Bug fix to add IntelliSense support for 'TRY_CAST()', and 'TRY_CONVERT()' functions [(Microsoft Connect item #2453461)](https://connect.microsoft.com/SQLServer/feedback/details/2453461/sql-server-2012-issue-with-try-cast).
3. Bug fix in the SSMS editor window to allow drag-and-drop open of Sql files [(Microsoft Connect item #2690658)](https://connect.microsoft.com/SQLServer/feedback/details/2690658/cannot-drag-sql-files-into-management-studios).
4. Bug fix in Analysis Services to correctly show the Data Feed provider for multi-dimensional Analysis Services models.
5. Bug fix in SSMS to prevent crash when trying to edit a join link in the SSMS table designer [(Microsoft Connect item #2721052)](https://connect.microsoft.com/SQLServer/feedback/details/2721052/ssms-view-design-mode-right-click-on-join-crashes-ssms).  

[More information and bug fixes available in the SSMS changelog.](../ssms/sql-server-management-studio-changelog-ssms.md)

---
## ![download](../ssdt/media/download.png) [SQL Server Management Studio June 2016 release](http://go.microsoft.com/fwlink/?LinkID=799832)

June 1, 2016 | Version number: 13.0.15000.23

**Features**  
New streamlined SSMS installer. Standalone SSMS releases. Automatic check for updates. A new Quick Find dialog. SSMS built on the Visual Studio 2015 shell, and many more.   
[More information available in the SSMS changelog.](../ssms/sql-server-management-studio-changelog-ssms.md)

**Known Issues** 
1. **PowerShell script generation from the Always Encrypted wizard is currently disabled.**
A fix for this issue will be available in a subsequent monthly SSMS update.
2. **The 'Encrypt Columns' context menu item in Object Explorer is disabled for tables and columns when you are connected to Azure SQL Database.** A fix for this issue will be available in a subsequent monthly SSMS update. As a workaround, right-click your database in Object Explorer, select 'Tasks', and choose 'Encrypt Columns' to encrypt Azure SQL Database columns and tables.
3. **SSMS can only connect to SQL Server 2016 Integrated Services (SSIS 2016) instances.** There is a known compatibility limitation with SQL Server Integration Services that prevents connecting to previous versions.
As a workaround for this problem, you can connect to your SQL Server Integration Service instance by using the SSMS release aligned with your SSIS instance.
4. **SSMS won't save maintenance plans for SQL Server 2008 R2 and earlier SQL Server versions.** We are working on a fix for this issue. In the meantime, you can use the SSMS 2014 release below to save the maintenance plans.
5. **SQL Server Configuration Manager will fail to launch if there is no SQL Server installed on the client machine.** If you do not have SQL Server installed on your client machine and launch SQL Server Configuration Manager, you will receive the 'Cannot connect to WMI provider' error. As a workaround:
    * Open a command prompt as Administrator.
    * Run the Mofcomp tool using the following command:  
      mofcomp "%programfiles(x86)%\Microsoft SQL Server\130\Shared\sqlmgmproviderxpsp2up.mof"
    * After you run the Mofcomp tool, run the following commands to restart the WMI service for the changes to take effect:  
       net stop "Windows Management Instrumentation"  
       net start "Windows Management Instrumentation"

**Fixes**  
1. Quick find dialog in SSMS that is better integrated into the current document and allows searching via regular expressions ([Microsoft Connect item #2735513](https://connect.microsoft.com/SQLServer/feedback/details/2735513/quick-find-replace-in-ssms-2016-rc3/)).
2. Bug fix in SSMS context-sensitive F1 help to correctly display help documents and articles.
3. Bug fix in Query Data Store 'Regressed Queries' view that caused SSMS to crash when scrolling.
4. Bug fix in Excel Analysis Services OLEDB connector to allow connections from Excel 2016 to SQL Server Analysis Services.
5. Bug fix in SSMS Connection dialog to show the connection dialog on the same monitor as the main SSMS window in multi-monitor systems ([Microsoft Connect item #724909)](https://connect.microsoft.com/SQLServer/feedback/details/724909/connection-dialog-appears-off-screen/).
6. Bug fixes in Always Encrypted experience. Fixed bug where Always Encrypted menu option was not enabled correctly for Stretch databases. Also fixed bug in the Always Encrypted wizard where it was not properly using the SafeNet (Luna SA) HSM provider.

---
## ![download](../ssdt/media/download.png) [SQL Server Management Studio 2014 SP1](http://download.microsoft.com/download/1/5/6/156992E6-F7C7-4E55-833D-249BD2348138/ENU/x86/SQLManagementStudio_x86_ENU.exe)

May 14, 2015 | Version number: 12.0.4100.1

**Features**  
Improved Azure SQL Database support with new open in management portal menu, table designer integration, and more.   
[More information available in the release notes](https://support.microsoft.com/en-us/kb/3058865).

**Known Issues**  
N/A

**Fixes**
1. SSMS Crashes during Movement of Maintenance Plan tasks if the Maintenance Plan name and the first SUB_PLAN name are the same.
2. You cannot debug a stored procedure that calls sp_executesql in SQL Server Management Studio (SSMS). When F11 is pressed, you receive an 'Object reference not set to an instance of object' error message ([Microsoft Connect item #736509](https://connect.microsoft.com/SQLServer/feedback/details/736509/sql-server-2012-rtm-management-studio-cannot-debug-sp-executesql)).
3. SSMS does not fully manage Full-Text in SQL Server Express ([Microsoft Connect item #740181](https://connect.microsoft.com/SQLServer/feedback/details/740181/management-studio-does-not-fully-manage-full-text-in-sql-server-express)).
4. SSMS handles Numbered Stored procedures inconsistently [(Microsoft Connect item #764197](https://connect.microsoft.com/SQLServer/feedback/details/764197/ssms-2012-inconsistently-handles-numbered-procedures)).
5. SSMS occasionally crashes on close, which then causes it to automatically restart ([Microsoft Connect item #774317](https://connect.microsoft.com/SQLServer/feedback/details/774317/sql-server-management-studio-2012-2014-crashes-when-closing)).
6. Create script duplicates the statements when scripting column level permissions in SSMS ([Microsoft Connect item #797967](https://connect.microsoft.com/SQLServer/feedback/details/797967/ssms-create-script-duplicates-the-statements-for-grant-or-deny-column-permissions)).
7. SSMS may crash when you try to refresh the SSMS window icon on the task bar ([Microsoft Connect item #799430](https://connect.microsoft.com/SQLServer/feedback/details/799430/ssms-2012-sp-1-cu-5-installed-crash-when-enforce-refresh-on-connect)).

---
## ![download](../ssdt/media/download.png) [SQL Server Management Studio 2012 SP3](http://download.microsoft.com/download/F/6/7/F673709C-D371-4A64-8BF9-C1DD73F60990/ENU/x86/SQLManagementStudio_x86_ENU.exe)  
  
November 21, 2015 | Version number: 11.0.6020.0

**Features**  
Full-feature SSMS express editions. Code snippets. Column store indexes, and more.  
[More information available in the release notes.](https://support.microsoft.com/en-us/kb/3072779)
 
**Known Issues**  
N/A

**Fixes**
1. Missing columns can't be indicated in the error message when you import data by using Import and Export Wizard.
2. "Unable to create restore plan due to break in the LSN chain" error when you restore differential backup in SSMS

---
## Additional Downloads  
For a list of all SQL Server Management Studio downloads, search the [Microsoft Download Center](https://www.microsoft.com/en-us/download/search.aspx?q=sql%20server%20management%20studio&p=0&r=10&t=&s=Relevancy~Descending).  
  
For the latest release of SQL Server Management Studio, see [Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md).  

---  
## Related resources
[Update center for Microsoft SQL Server](https://technet.microsoft.com/sqlserver/ff803383.aspx)   
[SQL Server Management Studio quick start](http://msdn.microsoft.com/en-us/d2bade70-07cf-4d94-b5d2-88aecb538ed1)  
[SQL Server Management Studio forum](https://social.msdn.microsoft.com/forums/en-us/home?forum=sqltools)  

  
