---
title: "SQL Server 2012 Release Notes | Microsoft Docs"
description: This Release Notes document describes known issues that you should read about before you install or troubleshoot Microsoft SQL Server 2012.
ms.service: sql
ms.subservice: release-landing
ms.custom: ""
ms.date: 07/22/2020
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Release Notes, SQL Server"
ms.assetid: 9ccb390a-67a9-4593-85ea-2b4c41c4620f
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-2016"
---
# SQL Server 2012 Release Notes
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]
This Release Notes document describes known issues that you should read about before you install or troubleshoot [Microsoft SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=238647)). This Release Notes document is available online only, not on the installation media, and it is updated periodically.  
  
For information about how to get started and install SQL Server 2012, see the SQL Server 2012 Readme. The Readme document is available on the installation media and from the [Readme](https://download.microsoft.com/download/3/B/D/3BD9DD65-D3E3-43C3-BB50-0ED850A82AD5/ENU/Readme.htm) download page. You can also find more information in [SQL Server Books Online](/previous-versions/sql/sql-server-2012/ms130214(v=sql.110)) and on the [SQL Server Forums](https://go.microsoft.com/fwlink/?LinkId=213599).  
  
## <a name="Install"></a>1.0 Before You Install  
Before installing [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)], consider the following information.  
  
### 1.1 Rules Documentation for SQL Server 2012 Setup  
**Issue:** SQL Server Setup validates your computer configuration before the Setup operation completes. The various rules that are run during the SQL Server Setup operation are captured using the System Configuration Checker (SCC) report. The documentation about these setup rules is no longer available on the MSDN library.  
  
**Workaround :** You can refer to the system configuration check report to learn more about these setup rules. The system configuration check generates a report that contains a short description for each executed rule, and the execution status. The system configuration check report is located at %programfiles%\Microsoft SQL Server\110\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
### 1.2 Adding a Local User Account for the Distributed Replay Controller Service Might Terminate Setup Unexpectedly  
**Issue:** In the **Distributed Replay Controller** page of SQL Server setup, when attempting to add a local user account for the Distributed Replay Controller service, setup will be terminated unexpectedly with a "SQL Server Setup failure" error message.  
  
**Workaround:** During SQL setup, do not add local user accounts via either "Add Current User" or "Add...". After setup, add a local user account manually by using the following steps:  
  
1.  Stop the SQL Server Distributed Replay controller service  
  
2.  On the controller computer on which the controller service is installed, from the command prompt, type dcomcnfg.  
  
3.  In the Component Services window, navigate to **Console Root** -> **Component Services** -> **Computers** -> **My Computer** -> **Dconfig** ->**DReplayController**.  
  
4.  Right-click **DReplayController**, and then click **Properties**.  
  
5.  In the **DReplayController Properties** window, on the **Security** tab, click **Edit** in the **Launch and Activation Permissions** section.  
  
6.  Grant the local user account **Local and Remote activation** permissions, and then click **OK**.  
  
7.  In the Access Permissions section, click **Edit** and grant the local user account **Local and Remote access** permissions, and then click **OK**.  
  
8.  Click **OK** to close the **DReplayController Properties** window.  
  
9. On the controller computer, add the local user account to the **Distributed COM Users** group.  
  
10. Start the SQL Server Distributed Replay controller service.  
  
### 1.3 SQL Server Setup might fail while trying to start the SQL Server Browser service  
**Issue:** SQL Server Setup might fail while trying to start the SQL Server Browser service, with errors similar to the following:  
  
```
The following error has occurred:  
Service 'SQLBrowser' start request failed. Click 'Retry' to retry the failed action, or click 'Cancel' to cancel this action and continue setup.
```
  
or  
  
```
The following error has occurred:  
SQL Server Browser configuration for feature 'SQL_Browser_Redist_SqlBrowser_Cpu32' was cancelled by user after a previous installation failure. The last attempted step: Starting the SQL Server Browser service 'SQLBrowser', and waiting for up to '900' seconds for the process to complete.
```
  
**Workaround:** This can happen when SQL Server Engine or Analysis Services fails to install. To fix this issue, refer the SQL Server Setup logs, and troubleshoot the SQL Server Engine and Analysis Services failures. For more information, see View and Read SQL Server Setup Log Files. For more information, see [View and Read SQL Server Setup Log Files](../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
### 1.4 SQL Server 2008, 2008 R2 Analysis Services Failover Cluster upgrade to SQL Server 2012 might fail after renaming the network name  
**Issue:** After you change the network name of a Microsoft SQL Server 2008, or 2008 R2 Analysis Services failover cluster instance using the Windows Cluster Administrator tool, the upgrade operation might fail.  
  
**Workaround:** To resolve this issue update the ClusterName registry entry following the instructions in the resolution section of [this KB article](https://support.microsoft.com/kb/955784).  
  
### 1.5 Installing SQL Server 2012 on Windows Server 2008 R2 Server Core Service Pack 1  
You can install SQL Server on Windows Server 2008 R2 Server Core SP1, with the following limitations:  
  
-   Microsoft SQL Server 2012 does not support Setup using the installation wizard on the Server Core operating system. When installing on Server Core, SQL Server Setup supports full quiet mode by using the /Q parameter, or Quiet Simple mode by using the /QS parameter.  
  
-   Upgrade of an earlier version of SQL Server to Microsoft SQL Server 2012 is not supported on a computer that is running Windows Server 2008 R2 Server Core SP1.  
  
-   Installing a 32-bit version of Microsoft SQL Server 2012 edition is not supported on a computer running Windows Server 2008 R2 Server Core SP1.  
  
-   Microsoft SQL Server 2012 cannot be installed side-by-side with earlier versions of SQL Server on a computer that is running Windows Server 2008 R2 Server Core SP1.  
  
-   Not all features of SQL Server 2012 are supported on the Server Core operating system. For more information on features supported, and on installing SQL Server 2012 on Server Core, see [Install SQL Server 2012 on Server Core](/previous-versions/sql/sql-server-2012/hh231669(v=sql.110)).  
  
### 1.6 Semantic Search Requires You to Install an Additional Dependency  
**Issue:** Statistical Semantic Search has an additional prerequisite, the semantic language statistics database, which is not installed by the SQL Server Setup program.  
  
**Workaround:** To set up the semantic language statistics database as a prerequisite for semantic indexing, perform the following tasks:  
  
1.  Locate and run the Windows Installer package named SemanticLanguageDatabase.msi on the SQL Server installation media to extract the database. For SQL Server 2012 Express, download the semantic language statistics database from [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=52681) (https://www.microsoft.com/download/details.aspx?id=52681), and then run the Windows Installer package.  
  
2.  Move the database to an appropriate data folder. If you leave the database in the default location, you must change permissions before you can attach it successfully.  
  
3.  Attach the extracted database.  
  
4.  Register the database by calling the stored procedure **sp_fulltext_semantic_register_language_statistics_db** and providing the name that you gave to the database when you attached it.  
  
If these tasks are not completed, you will see the following error message when you try to create a semantic index.  
  
```
Msg 41209, Level 16, State 3, Line 1  
A semantic language statistics database is not registered. Full-text indexes using 'STATISTICAL_SEMANTICS' cannot be created or populated.
```
  
### 1.7 Installation Prerequisite Handling During SQL Server 2012 Setup  
The following items describe the prerequisite installation behavior during SQL Server 2012 Setup:  
  
-   Installing SQL Server 2012 is supported only on Windows 7 SP1 or Windows Server 2008 R2 SP1. However, Setup does not block installing SQL Server 2012 on Windows 7 or Windows Server 2008 R2.  
  
-   The .NET Framework 3.5 SP1 is a requirement for SQL Server 2012 when you select Database Engine, Replication, Master Data Services, Reporting Services, Data Quality Services (DQS), or SQL Server Management Studio, and the framework is no longer installed by SQL Server Setup.  
  
    -   If you run Setup on a computer with either the Windows Vista SP2 or Windows Server 2008 SP2 operating system and you do not have the .NET Framework 3.5 SP1 installed, SQL Server Setup requires you to download and install the .NET Framework 3.5 SP1 before you can continue with the SQL Server installation. You can download the .NET Framework 3.5 SP1 from Windows Update or directly from [here](https://www.microsoft.com/download/details.aspx?id=25150). To avoid interruption during SQL Server Setup, download and install the .NET Framework 3.5 SP1 before you run SQL Server Setup.  
  
    -   If you run Setup on a computer with either the Windows 7 SP1 or Windows Server 2008 R2 SP1 operating system, you must enable the .NET Framework 3.5 SP1 before you install SQL Server 2012.  
  
        **Use one of the following methods to enable .NET Framework 3.5 SP1 on Windows Server 2008 R2 SP1:**  
  
        Method 1: Use Server Manager  
  
        1.  In Server Manager, click **Add Features** to display a list of possible features.  
  
        2.  In the **Select Features** interface, expand the **.NET Framework 3.5.1 Features** entry.  
  
        3.  After you expand **.NET Framework 3.5.1 Features**, you see two check boxes. One check box is for .NET Framework 3.5.1 and other check box is for WCF Activation. Select **.NET Framework 3.5.1**, and then click **Next**. You cannot install .NET Framework 3.5.1 features unless the required role services and features are also installed.  
  
        4.  In the **Confirm Installation Selections**, review the selections, and then click Install.  
  
        5.  Let the installation process complete, and then click **Close**.  
  
        Method 2: Use Windows PowerShell  
  
        1.  Click **Start** | **All Programs** | **Accessories**.  
  
        2.  Expand **Windows PowerShell**, right-click **Windows PowerShell**, and click **Run as administrator**. Click **Yes** in the **User Account Control** box.  
  
        3.  At the PowerShell command prompt, type the following commands and press ENTER after each command:  
  
            ```  
            Import-Module ServerManager  
            Add-WindowsFeature as-net-framework  
            ```  
  
        **Use the following method to enable .NET Framework 3.5 SP1 on Windows 7 SP1:**  
  
        1.  Click **Start** | **Control Panel** | **Programs**, and then click **Turn Windows features on or off**. If you are prompted for an administrator password or confirmation, type the password or provide confirmation.  
  
        2.  To enable **Microsoft .NET Framework 3.5.1**, select the check box next to the feature. To turn a Windows feature off, clear the check box.  
  
        3.  Click **OK**.  
  
        **Use Deployment Image Servicing and Management (DISM.exe) to enable .NET Framework 3.5 SP1:**  
  
        You can also enable .NET Framework 3.5 SP1 using Deployment Image Servicing and Management (DISM.exe). For more information about enabling Windows features online, see [Enable or disable Windows features online](/previous-versions/windows/it-pro/windows-7/dd744582(v=ws.10)). The following are the instructions to enable .NET Framework 3.5 SP1:  
  
        1.  At the command prompt, type the following command to list all of the features available in the operating system.  
  
            ```  
            sm /online /Get-Features  
            ```  
  
        2.  Optional: At the command prompt, type the following command to list information about the specific feature you are interested in.  
  
            ```  
            Dism /online /Get-FeatureInfo /FeatureName:NetFx3  
            ```  
  
        3.  Type the following command to enable a Microsoft .NET Framework 3.5.1.  
  
            ```  
            Dism /online /Enable-Feature /FeatureName:NetFx3  
            ```  
  
-   The .NET Framework 4 is a requirement for SQL Server 2012 . SQL Server Setup installs the .NET Framework 4 during the feature installation step.  
  
    SQL Server 2012 Express does not install the .NET Framework 4 when installing on the Windows Server 2008 R2 SP1 Server Core operating system. When installing SQL Server 2012 Express (Database only) .NET Framework 4 is not required if .NET Framework 3.5 SP1 is present. When .NET Framework 3.5 SP1 is not present or when installing SQL Server 2012 Management Studio Express, SQL Server 2012 Express with Tools, or SQL Server 2012 Express with Advanced Services, you must install the .NET Framework 4 before you install SQL Server2012 Express on a Windows Server 2008 R2 SP1 Server Core operating system.  
  
-   To make sure that the Visual Studio component can be installed correctly, SQL Server requires you to install an update. SQL Server Setup checks for the presence of this update and then requires you to download and install the update before you can continue with the SQL Server installation. To avoid the interruption during SQL Server Setup, you can download and install the update as described below before running SQL Server Setup (or you can install all the updates for the .NET Framework 3.5 SP1 that are available on Windows Update):  
  
    -   If you install SQL Server 2012 on a computer with the Windows Vista SP2 or Windows Server 2008 SP2 operating system, you can get the required update from [here](https://support.microsoft.com/?kbid=956250).  
  
    -   If you install SQL Server 2012 on a computer with the Windows 7 SP1 or Windows Server 2008 R2 SP1 operating system, this update is already installed on the computer.  
  
-   Windows PowerShell 2.0 is a prerequisite for installing SQL Server 2012 Database Engine components and SQL Server Management Studio, but Windows PowerShell is no longer installed by SQL Server Setup. If PowerShell 2.0 is not present on your computer, you can enable it by following the instructions on the [Windows Management Framework](https://mskb.pkisolutions.com/kb/968929) page. How you get Windows PowerShell 2.0 depends on which operating system you are running:  
  
    -   Windows Server 2008 - Windows PowerShell 1.0 is a feature and can be added. Windows PowerShell 2.0 versions are downloaded and installed (effectively as an OS Patch).  
  
    -   Windows 7/Windows Server 2008 R2 - Windows PowerShell 2.0 are installed by default.  
  
-   If you plan to use SQL Server 2012 features in a SharePoint environment, then SharePoint Server 2010 Service Pack 1 (SP1) and the SharePoint August Cumulative Update is required. You must install SP1, the SharePoint [August Cumulative Update](/archive/blogs/), and fully patch the server farm before you add SQL Server 2012 features to the farm. This requirement applies to the following SQL Server 2012 features: using an instance of Database Engine as the farm's database server, configuring PowerPivot for SharePoint, or deploying Reporting Services in SharePoint mode.  
  
### 1.8 Supported Operating Systems for SQL Server 2012  
SQL Server 2012 is supported on the Windows Vista SP2, Windows Server 2008 SP2, Windows 2008 R2 SP1, and Windows 7 SP1 operating systems.  
  
### 1.9 Sync Framework Is Not Included in the Installation Package  
**Issue:** Sync Framework is not included in the SQL Server 2012 installation package.  
  
**Workaround:** Download the appropriate version of Sync Framework from [this Microsoft Download Center page](https://www.microsoft.com/download/en/details.aspx?displaylang=en&id=23217).  
  
### 1.10 If Visual Studio 2010 Service Pack 1 is uninstalled, the SQL Server 2012 instance must be repaired to restore certain components  
**Issue:**[!INCLUDE[ssSQL11](../includes/sssql11-md.md)] installation is dependent on some components of the Visual Studio 2010 Service Pack 1. If you uninstall Service Pack 1, some of the shared components are downgraded to their original versions, and a few other components are completely removed from the machine.  
  
**Workaround:** Repair the instance of [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] from the original source media or network installation location.  
  
1.  Launch the SQL Server Setup program (setup.exe) from SQL Server installation media.  
  
2.  After prerequisites and system verification, the Setup program will display the **SQL Server Installation Center** page.  
  
3.  Click **Maintenance** in the left-hand navigation area, and then click **Repair** to start the repair operation. If the Installation Center was launched using the **Start** menu, you will need to provide the location of the installation media at this time.  
  
4.  Setup support rule and file routines will run to ensure that your system has prerequisites installed and that the computer passes Setup validation rules. Click **OK** or **Install** to continue.  
  
5.  On the **Select Instance** page, select the instance to repair, and then click **Next** to continue.  
  
6.  The repair rules will run to validate the operation. To continue, click **Next**.  
  
7.  The **Ready to Repair** page indicates that the operation is ready to proceed. To continue, click **Repair**.  
  
8.  The **Repair Progress** page shows the status of the repair operation. The **Complete** page indicates that the operation is finished.  
  
For more information on how to repair an instance of SQL Server, see [Repair a Failed SQL Server 2012 Installation](../database-engine/install-windows/repair-a-failed-sql-server-installation.md).  
  
### 1.11 An instance of SQL Server 2012 might fail after an OS upgrade  
**Issue:** An instance of SQL Server 2012 might fail with the following error after you upgrade the operating system to Windows 7 SP1 from Windows Vista.  
  
`Setup has detected that the .NET Framework version 4 needs to be repaired. Do not restart your computer until Setup is complete.`  
  
**Workaround**: Repair your installation of the .NET Framework 4 after you upgrade your operating system. For more information, see [How to repair an existing installation of the.NET Framework](https://support.microsoft.com/kb/306160).  
  
### 1.12 SQL Server Edition upgrade requires a restart  
**Issue**: When you edition upgrade an instance of SQL Server 2012, some of the functionalities associated with the new edition might not be activated immediately.  
  
**Workaround**: Restart the machine after the edition upgrade of an instance of SQL Server 2012. For more information about supported upgrades in SQL Server 2012, see [Supported Version and Edition Upgrades](../database-engine/install-windows/supported-version-and-edition-upgrades-2017.md).  
  
### 1.13 Database with read-only filegroup or files cannot be upgraded  
**Issue**:  You cannot upgrade a database by either attaching the database or restoring the database from backup if the database or its files/filegroups are set to read-only.  Error 3415 is returned.  This issue also applies when performing an in-place upgrade of an instance of SQL Server. That is, you attempt to replace an existing instance of SQL Server by installing SQL Server 2012 and one or more of the existing databases is set to read-only.  
  
**Workaround:** Before upgrading, ensure that the database and its files/filegroups are set to read-write.  
  
### 1.14 Reinstalling an instance of SQL Server Failover Custer fails if you use the same IP address  
**Issue:** If you specify an incorrect IP address during an installation of a SQL Server Failover Cluster instance, the installation fails. After you uninstall the failed instance, and if you try to reinstall the SQL Server failover cluster instance with the same instance name, and correct IP address, the installation fails. The failure is because of the duplicate resource group left behind by the previous installation.  
  
**Workaround:** To resolve this issue, use a different instance name during the reinstallation, or manually delete the resource group before reinstalling. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster](failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="AS"></a>2.0 Analysis Services  
  
### 2.1 SQL editor and AS editor cannot connect to their respective server instances in the same SSMS instance  
**Issue:** Cannot connect to an Analysis Services server using the MDX/DMX editor when the SQL editor is already connected.  
  
When using SQL Server Management Studio 2012 (SSMS), if a .sql file is open in the editor and is connected to a SQL Server instance, an MDX or DMX file when opened in the same  instance of SSMS cannot connect to an instance of AS server. Likewise, if an MDX or DMX file is already open in the editor in SSMS and connected to an AS server instance, a .sql file when opened in the same instance of SSMS cannot connect to an instance of SQL Server.  
  
**Workaround**:  Use one of the following options to resolve this issue.  
  
-   Start another instance of SSMS to open the MDX / DMX file.  
  
-   Disconnect the SQL editor and then connect the MDX / DMX editor to an AS server.  
  
### 2.2 Cannot Create or Open Tabular Projects When BUILTIN\Administrators Group Name Cannot Be Resolved  
**Issue:** You must be an administrator on a workspace database server before you can create or open tabular projects. A user can be added to the server administrators group by adding the user name or group name. If you are a member of the BUILTIN\Administrator group, you cannot create or edit BIM files unless the workspace database server is joined to the domain from which it was originally provisioned. If you open or create the BIM file, it will fail with the following error message:  
  
`"The BIM file cannot be opened. The server connected to is not valid. Reason: You are not an administrator of server [server name]."`  
  
**Workarounds:**  
  
-   Re-join the workspace database server and SQL Server Data Tools (SSDT) computer to the domain.  
  
-   If the workspace database server and/or SSDT computers are not going to be domain joined at all times, add individual user names instead of the BUILTIN\Administrators group as administrators on the workspace database server.  
  
### 2.3 SSIS Components for AS Tabular Models Do Not Work as Expected  
SQL Server Integration Services (SSIS) components for Analysis Services (AS) do not work as expected for tabular models. The following are known issues that may occur when you try to write an SSIS package for working with tabular models.  
  
**Issue:** The AS Connection Manager cannot use a tabular model in the same solution as a data source.  
  
**Workaround:** You must explicitly connect to the AS server before configuring the AS Processing Task or the AS Execute DDL Task.  
  
There are problems with the AS Processing Task when you work with tabular models:  
  
**Issue:** Instead of databases, tables, and partitions, you see cubes, measure groups, and dimensions. This is a limitation of the task.  
  
**Workaround:** You can still process your tabular model using the cube/measure group/dimension structure.  
  
**Issue:** Some processing options supported by AS running in tabular mode are not exposed in the AS Processing Task, such as Process Defrag.  
  
**Workaround:** Use the Analysis Services Execute DDL task instead to execute an XMLA script that contains the ProcessDefrag command.  
  
**Issue:** Some configuration options in the tool are not applicable. For example, "Process related objects" should not be used when processing partitions, and the "Parallel Processing" configuration option contains an invalid error message stating that parallel processing is not supported on the Standard SKU.  
  
**Workaround:** None  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="BOL"></a>3.0 Books Online  
  
### 3.1 Help Viewer for SQL Server Crashes in Environments Configured to Run Only IPv6  
**Issue**: If your environment is configured to run only IPv6, the Help Viewer for SQL Server 2012 will crash, and you will be presented with the following error message:  
  
`HelpLibAgent.exe has stopped working.`  
  
> [!IMPORTANT]  
> This applies to all environments running with only IPv6 enabled. IPv4 (and IPv4 with IPv6) enabled environments are not impacted.  
  
**Workaround**: To avoid this issue, enable IPv4, or use the following steps to add a registry entry and create an ACL to enable the Help viewer for IPv6:  
  
1.  Create a registry key with the name "IPv6" and a value of "1 (DWORD(32 bit))" under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Help\v1.0.  
  
2.  Set the security ACL's for the port for IPv6, executing the following from an admin CMD window:  
  
    ```  
    netsh http add urlacl url=https://[::1]:47873/help/ sddl=D:(A;;GX;;;WD)  
    ```  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="DQS"></a>4.0 Data Quality Services  
  
### 4.1 DQS Not Supported in a Cluster  
**Issue:** DQS is not supported in a SQL Server cluster installation. If you are installing a cluster instance of SQL Server, you must not select the **Data Quality Services** and **Data Quality Client** check boxes on the **Feature Selection** page. If these check boxes are selected during cluster instance installation (and you complete the Data Quality Server installation by running the DQSInstaller.exe file), DQS will be installed on this node, but will not be available on additional nodes when you add more nodes to the cluster, and hence will not work on additional nodes.  
  
**Workaround:** Install SQL Server 2012 Cumulative Update 1 to resolve this issue. For instructions, see [https://support.microsoft.com/kb/2674817](https://support.microsoft.com/kb/2674817).  
  
### 4.2 To Reinstall Data Quality Server, Delete the DQS Objects After Uninstalling Data Quality Server  
**Issue:** If you uninstall the Data Quality Server, the DQS objects (DQS databases, DQS logins, and a DQS stored procedure) are not deleted from the SQL Server instance.  
  
**Workaround:** To reinstall Data Quality Server on the same computer and in the same SQL Server instance, you must manually delete the DQS objects from the SQL Server instance. Additionally, you must also delete the DQS databases (DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA) files from the C:\Program Files\Microsoft SQL Server\MSSQL11.<SQL_Server_Instance>\MSSQL\DATA folder on your computer before you reinstall Data Quality Server. Otherwise, the Data Quality Server installation fails. Move the database files instead of deleting them if you want to preserve data, such as knowledge bases or data quality projects. For more information about removing DQS objects after the uninstall process is complete, see [Remove Data Quality Server Objects](./install/remove-data-quality-server-objects.md).  
  
### 4.3 Indication of a Terminated Knowledge Discovery or Interactive Cleansing Activity is Delayed  
**Issue:** If an administrator terminates an activity in the Activity Monitoring screen, an interactive user who is running the knowledge discovery, domain management, or interactive cleansing activity will not receive any indication that his or her activity was terminated until he or she performs the next operation.  
  
**Workaround:** None  
  
### 4.4 A Cancel Operation Discards Work from Multiple Activities  
**Issue:** If you click **Cancel** for a running knowledge discovery or domain management activity, and other activities have completed previously without a publish operation being performed while the activity is running, the work from all the activities performed since the last publish will be discarded, not just the current one.  
  
**Workaround:** To avoid this, publish work that you need to persist in the knowledge base before starting a new activity.  
  
### 4.5 Controls Do Not Scale Properly On Large Font Sizes  
**Issue:** If you change the size of text to "Larger - 150%" (in Windows Server 2008 or Windows 7), or change the Custom DPI setting to 200% (in Windows 7), the **Cancel** and **Create** buttons on the **New Knowledge Base** page are not accessible.  
  
**Workaround:** To resolve the issue, set the font to a smaller size.  
  
### 4.6 Screen Resolution of 800x600 Is Not Supported  
**Issue:** The Data Quality Client application does not display correctly if the screen resolution is set to 800x600.  
  
**Workaround:** To resolve the issue, set the screen resolution to a higher value.  
  
### 4.7 Map Bigint Column in the Source Data to a Decimal Domain to Prevent Data Loss  
**Issue:** If a column in your source data is of the **bigint** data type, you must map the column to a domain of the **decimal** data type rather than the **integer** data type in DQS. This is because the **decimal** data type represents a larger range of values than the **int** data type and therefore can hold larger values.  
  
### 4.8 NVARCHAR(MAX) and VARCHAR(MAX) Data Types Are Not Supported in the DQS Cleansing Component in Integration Services  
**Issue:** Data columns of the **nvarchar(max)** and **varchar(max)** data types are not supported in the DQS Cleansing component in Integration Services. As such, these data columns are unavailable for mapping in the Mapping tab of DQS Cleansing Transformation Editor, and hence cannot be cleansed.  
  
**Workaround:** Before processing these data columns using the DQS Cleansing component, you must convert them to the **DT_STR** or **DT_WSTR** data type using the Data Conversion transform.  
  
### 4.9 The Item to Run DQSInstaller.exe on the Start Menu Is Overwritten On New SQL Server Instance Installation  
**Issue:** If you choose to install Data Quality Services in a SQL Server instance, an item is created on the **Start** menu under the **Data Quality Services** program group called **Data Quality Server Installer** after you complete the SQL Server setup. However, if you install multiple SQL Server instances on the same computer, there is still a single **Data Quality Server Installer** item on the **Start** menu. Clicking this item runs the DQSInstaller.exe file in the most recent installed SQL Server instance.  
  
### 4.10 Activity Monitoring Displays Incorrect Status for Failed Integration Services Cleansing Activities  
The Activity Monitoring screen incorrectly displays **Succeeded** even for failed Integration Services Cleansing activities in the **Current Status** column.  
  
### 4.11 Schema Name Is Not Displayed As Part of Table/View Name  
When selecting a SQL Server data source in any of the DQS activities during the mapping stage in Data Quality Client, the list of tables and views is displayed without the schema name. Therefore, if there are several tables/views with the same name but different schema, they can be distinguished only by looking at the data preview, or by selecting them, and then looking at the available fields to map.  
  
### 4.12 Issue with Cleansing Output and Export If Data Source Is Mapped To a Composite Domain Containing a Child Domain of Date Type  
In a cleansing data quality project, if you have mapped a field in your source data with a composite domain that has a child domain of date data type, the child domain output in the cleansing result has incorrect date format, and the export to database operation fails.  
  
### 4.13 Error When Mapping To An Excel Sheet That Contains a ; (Semicolon) In Its Name  
**Issue:** On the **Map** page of any DQS activity in Data Quality Client, if you map to the source excel sheet that contains a ; (semicolon) in its name, an unhandled exception message is displayed when you click **Next** on the **Map** page.  
  
**Workaround:** Remove the ; (semicolon) from the sheet name in the Excel file that contains the source data to be mapped, and try again.  
  
### 4.14 Issue with Date or DateTime Values in Unmapped Source Fields in Excel during Cleansing and Matching  
**Issue**: If your source data is Excel and you have not mapped the source fields containing values of **Date** or **DateTime** data type, the following happens during the cleansing and matching activities:  
  
-   The unmapped **Date** values are displayed and exported in the yyyy-mm-dd format.  
  
-   The time value is lost for the unmapped **DateTime** values, and they are displayed and exported in the yyyy-mm-dd format.  
  
**Workaround:** You can view the unmapped field values in the right-lower pane on the **Manage and view results** page in the cleansing activity, and on the **Matching** page in the matching activity.  
  
### 4.15 Cannot Import Domain Values from an Excel File (.xls) Containing More Than 255 Columns of Data  
**Issue:** If you import values into a domain from an Excel 97-2003 file (.xls) that contains more than 255 columns of data, an exception message appears, and the import fails.  
  
**Workaround:** To fix this issue, you can do one of the following:  
  
-   Save the .xls file as .xlsx file, and then import the values from the .xlsx file into a domain.  
  
-   Remove data in all the columns beyond column 255 in the .xls file, save the file, and then import the values from the .xls file into a domain.  
  
### 4.16 Activity Monitoring Feature is Unavailable for Roles Other Than dqs_administrator  
The Activity Monitoring feature is available only for the users having the dqs_administrator role. If your user account has the dqs_kb_editor or dqs_kb_operator role, the Activity Monitoring feature will be unavailable in the Data Quality Client application.  
  
### 4.17 Error on Opening a Knowledge Base in the Recent Knowledge Base List for Domain Management  
Issue: You might receive the following error if you open a knowledge base in the **Recent Knowledge Base** list for the domain management activity in the Data Quality Client home screen:  
  
`"A configuration with name 'RecentList:KB:<domain>\<username>' already exists in the database."`  
  
This occurs because of the difference in the way DQS compares strings in the SQL Server database and C#. The string comparison in the SQL Server database is case insensitive whereas it is case sensitive in C#.  
  
Let us illustrate this with an example. Consider a user, Domain\user1. The user signs in to the Data Quality Client computer using the "user1" account, and works on a knowledge base. DQS stores the recent knowledge base for each user as a record in the A_CONFIGURATION table in the DQS_MAIN database. In this case, the record will be stored with the following name: RecentList:KB:Domain\user1. Later, the user logs on the Data Quality Client computer as "User1" (note the U in upper case), and tries to open the knowledge base in the **Recent Knowledge Base** list for the domain management activity. The underlying code in DQS will compare the two strings, RecentList:KB:DOMAIN\user1 and DOMAIN\User1, and considering the case-sensitive string comparison in C#, the strings won't match and therefore DQS will attempt to insert a new record for the user (User1) in the A_CONFIGURATION table in the DQS_MAIN database. However, owing to the case-insensitive string comparison in SQL database, the string already exists in the A_CONFIGURATION table in the DQS_MAIN database, and the insert operation will fail.  
  
**Workaround:** To fix this issue, you can do one of the following:  
  
-   Verify that duplicate entries exist by running the following statement:  
  
    ```  
    SELECT * FROM DQS_MAIN.dbo.A_CONFIGURATION WHERE NAME like 'RecentList%'  
    ```  
  
    Next, you can run the following statement to delete the record just for the affected user by changing the value in the WHERE clause to match the affected domain and user name.  
  
    ```  
    DELETE DQS_MAIN.dbo.A_Configuration WHERE NAME LIKE 'RecentList%<domain>\<username>'  
    ```  
  
    Alternatively, you could remove all recent items for all users in DQS:  
  
    ```  
    DELETE DQS_MAIN.dbo.A_Configuration WHERE NAME LIKE 'RecentList%'  
    ```  
  
-   Use same capitalization as the last time to specify your user account while logging on to the Data Quality Client computer.  
  
> [!NOTE]  
> To avoid this issue, use consistent capitalization rules to specify your user account while logging on the Data Quality Client computer.  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="DE"></a>5.0 Database Engine  
  
### 5.1 Use of Distributed Replay Controller and Distributed Replay Client Features  
**Issue:** Distributed Replay Controller and Distributed Replay Client features are made available in the Server Core SKU of Windows Server 2008, Windows Server 2008 R2, and Windows Server 7, even though these two features are not supported in the Server Core SKU.  
  
**Workaround:** Do not install or use these two features in the Server Core SKU of Windows Server 2008, Windows Server 2008 R2, and Windows Server 7.  
  
### 5.2 SQL Server Management Studio depends on Visual Studio 2010 SP1  
**Issue**:  SQL Server 2012 Management Studio depends on Visual Studio 2010 SP1 to function correctly. Uninstalling Visual Studio 2010 SP1 may cause functionality loss in SQL Server Management Studio and will leave Management Studio in an unsupported state. The following issues may be seen in this case:  
  
-   Command-line parameters to ssms.exe will not work correctly.  
  
-   Help information displayed when trying to run ssms.exe with the /? switch will be incorrect.  
  
-   For every file that is opened by double clicking on it in Windows Explorer, a new instance of SSMS will be launched to open the file.  
  
-   Queries cannot be debugged in the normal user mode.  
  
**Workaround**: Install Visual Studio 2010 SP1 again and restart Management Studio.  
  
### 5.3 x64 Operating Systems Require 64-bit PowerShell 2.0  
**Issue:** 32-bit installations of Windows PowerShell Extensions for SQL Server are not supported for instances of SQL Server 2012 on 64-bit operating systems.  
  
**Workarounds:**  
  
-   Install 64-bit SQL Server 2012 with 64-bit Management Tools and 64-bit Windows PowerShell Extensions for SQL Server.  
  
-   Or, import the SQLPS Module from a 32-bit Windows PowerShell 2.0 prompt.  
  
### 5.4 An Error Might Occur When Navigating in the Generate Script Wizard  
**Issue:** After generating a script in the Generate Script Wizard by clicking **Save or Publish Scripts**, then navigating by clicking **Choose Options** or **Set Scripting Options**, clicking **Save or Publish Scripts** again might result in the following error:  
  
```
An exception occurred while executing a Transact-SQL statement or batch. (Microsoft.SqlServer.ConnectionInfo)  
------------------------------  
ADDITIONAL INFORMATION:  
Invalid object name 'sys.federations'. (Microsoft SQL Server, Error: 208)
```
  
**Workaround:** Close and reopen the Generate Scripts Wizard.  
  
### 5.5 New Maintenance Plan Layout Not Compatible with Earlier SQL Server Tools  
**Issue:** When SQL Server 2012 management tools are used to modify an existing maintenance plan created in a previous version of SQL Server management tools (SQL Server 2008 R2, SQL Server 2008, or SQL Server 2005), the maintenance plan is saved in a new format. Earlier versions of SQL Server management tools do not support this new format.  
  
**Workaround**: None  
  
### 5.6 Intellisense Has Limitations When Logged in to a Contained Database  
Issue: Intellisense in SQL Server Management Studio (SSMS) and SQL Server Data Tools (SSDT) does not function as expected when contained users are logged in to contained databases. The following behavior is seen in such cases:  
  
1.  Underlining for invalid objects does not appear.  
  
2.  Auto-complete list does not appear.  
  
3.  Tooltip help for built-in functions does not work.  
  
**Workaround**:  None  
  
### 5.7 Always On Availability Groups

Before you attempt to create an availability group, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups (SQL Server)](https://go.microsoft.com/?linkid=9753168) in Books Online. For an introduction to Always On Availability Groups, see [Always On Availability Groups (SQL Server)](https://go.microsoft.com/?linkid=9753166)in Books Online.  
  
#### 5.7.1 Client-Connectivity for Always On Availability Groups  
**Updated on:** August 13, 2012  
  
This section describes driver support for Always On Availability Groups and workarounds for not supported drivers.  
  
**Driver Support**  
  
The following table summarizes driver support for Always On Availability Groups:  
  
|Driver|Multi-Subnet Failover|Application Intent|Read-Only Routing|Multi-Subnet Failover: Faster Single Subnet Endpoint Failover|Multi-Subnet Failover: Named Instance Resolution For SQL Clustered Instances|  
|----------|--------------------------|----------------------|----------------------|------------------------------------------------------------------|---------------------------------------------------------------------------------|  
|SQL Native Client 11.0 ODBC|Yes|Yes|Yes|Yes|Yes|  
|SQL Native Client 11.0 OLEDB|No|Yes|Yes|No|No|  
|ADO.NET with .NET Framework 4.0 with connectivity patch <sup>1</sup>|Yes|Yes|Yes|Yes|Yes|  
|ADO.NET with .NET Framework 3.5 SP1 with connectivity patch <sup>2</sup>|Yes|Yes|Yes|Yes|Yes|  
|Microsoft JDBC driver 4.0 for SQL Server|Yes|Yes|Yes|Yes|Yes|  
  
<sup>1</sup> Download the connectivity patch for ADO .NET with .NET Framework 4.0: [https://support.microsoft.com/kb/2600211](https://support.microsoft.com/kb/2600211).  
  
<sup>2</sup> Download the connectivity patch for ADO.NET with .NET Framework 3.5 SP1: [https://support.microsoft.com/kb/2654347](https://support.microsoft.com/kb/2654347).  
  
**MultiSubnetFailover Keyword and Associated Features**  
  
MultiSubnetFailover is a new connection string keyword used to enable faster failover with Always On Availability Groups and Always On Failover Cluster Instances in SQL Server 2012. The following three subfeatures are enabled when MultiSubnetFailover=True is set in connection string:  
  
-   Faster multi-subnet failover to a multi-subnet listener for an Always On Availability Group or Failover Cluster Instances.  
  
    -   Named instance resolution to a multi-subnet Always On Failover Cluster Instance.  
  
-   Faster single subnet failover to a single subnet listener for an Always On Availability Group or Failover Cluster Instances.  
  
    -   This feature is used when connecting to a listener that has a single IP in a single subnet. This performs more aggressive TCP connection retries to speed up single subnet failovers.  
  
-   Named instance resolution to a multi-subnet Always On Failover Cluster Instance.  
  
    -   This is to add named instance resolution support for an Always On Failover Cluster Instances with multiple subnet endpoints.  
  
**MultiSubnetFailover=True Not Supported by NET Framework 3.5 or OLEDB**  
  
**Issue:** If your Availability Group or Failover Cluster Instance has a listener name (known as the network name or Client Access Point in the WSFC Cluster Manager) depending on multiple IP addresses from different subnets, and you are using either ADO.NET with .NET Framework 3.5SP1 or SQL Native Client 11.0 OLEDB, potentially, 50% of your client-connection requests to the availability group listener will hit a connection timeout.  
  
**Workarounds:** We recommend that you do one of the following tasks.  
  
-   If do not have the permission to manipulate cluster resources, change your connection timeout to 30 seconds (this value results in a 20-second TCP timeout period plus a 10-second buffer).  
  
    **Pros**: If a cross-subnet failover occurs, client recovery time is short.  
  
    **Cons**: Half of the client connections will take more than 20 seconds  
  
-   If you have the permission to manipulate cluster resources, the more recommended approach is to set the network name of your availability group listener to **RegisterAllProvidersIP**=0. For more information, see "Sample PowerShell Script to Disable RegisterAllProvidersIP and Reduce TTL", later in this section.  
  
    **Pros:** You do not need to increase your client-connection timeout value.  
  
    **Cons:** If a cross-subnet failover occurs, the client recovery time could be 15 minutes or longer, depending on your HostRecordTTL setting and the setting of your cross-site DNS/AD replication schedule.  
  
**Sample PowerShell Script to Disable RegisterAllProvidersIP and Reduce TTL**  
  
The following sample PowerShell script demonstrates how to disable `RegisterAllProvidersIP` and reduce TTL. Replace `yourListenerName` with the name of the listener you are changing.  
  
```  
Import-Module FailoverClusters  
Get-ClusterResource yourListenerName|Set-ClusterParameter RegisterAllProvidersIP 0  
Get-ClusterResource yourListenerName|Set-ClusterParameter HostRecordTTL 300  
```  
  
#### 5.7.2 Upgrading from CTP3 with availability group configured is not supported  
Drop the availability group and recreate it before you upgrade. This is due to a limitation in the CTP3 build. Future builds will not have this restriction.  
  
#### 5.7.3 Side by Side Installation of CTP3 with later versions is not supported if you have an availability group configured in your instance  
This is due to a limitation in the CTP3 build. Future builds will not have this restriction.  
  
#### 5.7.4 Side by Side Installation of CTP3 with later versions of Failover Cluster Instances is not supported.  
This is due to a limitation in the CTP3 build. Future builds will not have this restriction. To upgrade failover cluster instances from CTP3 make sure to upgrade all instances on a node at the same time.  
  
#### 5.7.5  Timeouts may occur when using multi IPs in the same subnet with Always On  
**Issue:** When using multi IPs in the same subnet with Always On, customers may sometimes see a timeout. This happens if the top IP in the list is bad.  
  
**Workaround:** Use 'multisubnetfailover = true' in the connection string.  
  
#### 5.7.6 Failure to Create New Availability Group Listeners Because of Active Directory Quotas  
**Issue:** The creation of a new availability group listener may fail upon creation because you have reached an Active Directory quota for the participating cluster node machine account. For more information, see [How to troubleshoot the Cluster service account when it modifies computer objects](https://support.microsoft.com/kb/307532) and [Active Directory Quotas](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc904295(v=ws.10)).,  
  
#### 5.7.7 NetBIOS Conflicts Because Availability Group Listener Names Use an Identical 15-Character Prefix  
If you have two WSFC clusters that are controlled by the same Active Directory and you try to create availability group listeners in both of clusters using names with more than 15 characters and an identical 15 character prefix, you will get an error reporting that the Virtual Network Name resource could not be brought online. For information about prefix naming rules for DNS names, see [Assigning Domain Names](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc731265(v=ws.10))  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="IS"></a>6.0 Integration Services  
  
### 6.1 The Change Data Capture Service for Oracle and the Change Data Capture Designer Console for Oracle  
The CDC Service for Oracle is a Windows service that scans Oracle transaction logs and captures changes to Oracle tables of interest into SQL Server change tables. The CDC Designer Console is used to develop and maintain Oracle CDC Instances. The CDC Designer Console is a Microsoft Management Console (MMC) snap-in.  
  
#### 6.1.1 Install the CDC Service for Oracle and the CDC Designer for Oracle  
**Issue:** The CDC Service and CDC Designer are not installed by SQL Server Setup. You must manually install the CDC Service or CDD Designer on a computer that meets the requirements and prerequisites as described in the updated Help files.  
  
**Workaround:** To install the CDC Service for Oracle, manually run AttunityOracleCdcService.msi from the SQL Server installation media. To install the CDC Designer Console, manually run AttunityOracleCdcDesigner.msi from the SQL Server installation media.  Installation packages for x86 and x64 are located in .\Tools\AttunityCDCOracle\ on the SQL Server installation media.  
  
#### 6.1.2 F1 Help Functionality Points to Incorrect Documentation Files  
**Issue:** You cannot access the correct Help documentation by using either the F1 Help drop-down list or by clicking the "?" in the Attunity Consoles. These methods point to incorrect chm files.  
  
**Workaround:** The correct chm files are installed when the CDC Service for Oracle and CDC Designer for Oracle are installed. To view the correct Help content, launch the chm files directly from this location: `%Program Files%\Change Data Capture for Oracle by Attunity\*.chm`.  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="MDS"></a>7.0 Master Data Services  
  
### 7.1 Fixing an MDS installation in a Cluster  
**Issue:** If you install a clustered instance of the RTM version of SQL Server 2012 with the **Master Data Services** checkbox selected, MDS will be installed on a single node, but it will not be available and will not work on other nodes that you add to the cluster.  
  
**Workaround**: To resolve this issue, you must install the SQL Server 2012 Cumulative Release 1 (CU1), performing the following steps:  
  
1.  Make sure that there is no existing SQL/MDS installation.  
  
2.  Download SQL Server 2012 CU1 into a local directory.  
  
3.  Install SQL Server 2012 with the MDS feature on the primary cluster node, and then install SQL Server 2012 with the MDS feature on any other cluster nodes.  
  
For more information about the issues, and information about how to perform the above steps, see [https://support.microsoft.com/kb/2683467](https://support.microsoft.com/kb/2683467).  
  
### 7.2 Microsoft Silverlight 5 Required  
To work in the Master Data Manager web application, Silverlight 5.0 must be installed on the client computer. If you do not have the required version of Silverlight, you will be prompted to install it when you navigate to an area of the web application that requires it. You can install Silverlight 5 from [https://go.microsoft.com/fwlink/?LinkId=243096](https://go.microsoft.com/fwlink/?LinkId=243096).  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="RS"></a>8.0 Reporting Services  
  
### 8.1 Reporting Services Connectivity to SQL Server PDW Requires Updated Drivers  
Connectivity from SQL Server 2012 Reporting Services to Microsoft SQL Server PDW Appliance Update 2 and higher requires an update to the PDW connectivity drivers. For more information, SQL Server PDW customers should contact Microsoft support.  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="SI"></a>9.0 StreamInsight  
SQL Server 2012 includes StreamInsight 2.1. StreamInsight 2.1 requires a Microsoft SQL Server 2012 license and .NET Framework 4.0. It includes a number of performance improvements along with few bug fixes. For more information, see the [Microsoft StreamInsight 2.1 Release Notes](https://social.technet.microsoft.com/wiki/contents/articles/6539.aspx). In order to download StreamInsight 2.1 separately, please visit the [Microsoft StreamInsight 2.1 download page](https://www.microsoft.com/download/details.aspx?id=30149) on the Microsoft Download Center.  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")  
  
## <a name="UA"></a>10.0 Upgrade Advisor  
  
### 10.1 Link to Install Upgrade Advisor Is Not Enabled on Chinese (HK) Operating Systems  
Issue: When you try to install Upgrade Advisor on any supported Windows version in Chinese (Hong Kong SAR) operating systems (OS), you might find that the link to install Upgrade Advisor is not enabled.  
  
**Workaround**: Locate the **SQLUA.msi** file on your SQL Server 2012 media at `\1028_CHT_LP\x64\redist\Upgrade Advisor` or at `\1028_CHT_LP\x86\redist\Upgrade Advisor`, depending on your operating system architecture.  
  
![horizontal_bar](media/horizontal-bar.png "horizontal_bar")