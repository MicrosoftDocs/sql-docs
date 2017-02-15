---
title: "SQL Server 2012 SP1 Release Notes | Microsoft Docs"
ms.prod: "sql-server-2012"
ms.technology: "server-general"
ms.custom: ""
ms.date: "01/31/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 72171357-28de-4edd-bdfd-194f97225a6f
caps.latest.revision: 49
author: "byham"
ms.author: "rickbyh"
manager: "jhubbard"
---
# SQL Server 2012 SP1 Release Notes
This Release Notes document describes known issues that you should read about before you install or troubleshoot [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssSQL11](../includes/sssql11-md.md)] Service Pack 1. This Release Notes document is available online only, not on the installation media, and it is updated periodically.  
  
## <a name="bkmk_top"></a>Contents  
[1.0 Before You Install](#bmk_Install)  
  
[2.0 Analysis Services and PowerPivot](#bkmk_AS)  
  
[3.0 Reporting Services](#bkmk_RS)  
  
[4.0 Data Quality Services](#bkmk_DQS)  
  
[5.0 SQL Server Express](#bkmk_Express)  
  
[6.0 Change Data Capture Service and Designer for Oracle by Attunity](#bkmk_CDC)  
  
[7.0 SQL Server Data-Tier Application Framework (DACFx)](#DACFx)  
  
[8.0 Known Issues Fixed in this Service Pack](#bkmk_known_issues_fixed)  
  
## <a name="bmk_Install"></a>1.0 Before You Install  
Before installing SQL Server 2012 SP1, consider the following information.  
  
### 1.1 Reinstalling an Instance of SQL Server Failover Custer Fails if You Use the Same IP Address  
**Issue:** If you specify an incorrect IP address during an installation of a SQL Server Failover Cluster instance, the installation fails. After you uninstall the failed instance, and if you try to reinstall the SQL Server failover cluster instance with the same instance name, and correct IP address, the installation fails. The failure is because of the duplicate resource group left behind by the previous installation.  
  
**Workaround:** To resolve this issue, use a different instance name during the reinstallation, or manually delete the resource group before reinstalling. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster](http://msdn.microsoft.com/library/ms191545).  
  
### 1.2 Choose the Correct File to Download and Install  
Use the following table to determine which file to download and install. Verify that you have the correct system requirements before installing the service pack. The system requirements are provided on the download pages that are linked to in the table.  
  
|If your current installed version is...|And you want to...|Download and install...|  
|-------------------------------------------|----------------------|---------------------------|  
|**32-bit Installations:**|||  
|A 32-bit version of any edition of SQL Server 2012|Upgrade to the 32-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x86-ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 32-bit version of SQL Server 2012 RTM Express|Upgrade to the 32-bit version of SQL Server 2012 Express SP1|SQLServer2012SP1-KB2674319-x86-ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 32-bit version of only the client and manageability tools for SQL Server 2012 (including SQL Server 2012 Management Studio)|Upgrade the client and manageability tools to the 32-bit version of SQL Server 2012 SP1|SQLManagementStudio_x86_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 32-bit version of SQL Server 2012 Management Studio Express|Upgrade to the 32-bit version of SQL Server 2012 SP1 Management Studio Express|SQLManagementStudio_x86_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 32-bit version of any edition of SQL Server 2012 **and** a 32-bit version of the client and manageability tools (including SQL Server 2012 RTM Management Studio)|Upgrade all products to the 32-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x86-ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 32-bit version of one or more tools from the [Microsoft SQL Server 2012 RTM Feature Pack](http://www.microsoft.com/download/details.aspx?id=16978)|Upgrade the tools to the 32-bit version of Microsoft SQL Server 2012 SP1 Feature Pack|One or more files from [Microsoft SQL Server 2012 SP1 Feature Pack](http://go.microsoft.com/fwlink/p/?LinkID=268266)|  
|No 32-bit installation of SQL Server 2012|Install 32-bit Server 2012 including SP1 (New instance with SP1 pre-installed)|SQLServer2012SP1-FullSlipstream-x86-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x86-ENU.box from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|No 32-bit installation of SQL Server 2012 Management Studio|Install 32-bit SQL Server 2012 Management Studio including SP1|SQLManagementStudio_x86_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkId=267905)|  
|No 32-bit version of SQL Server 2012 RTM Express|Install 32-bit SQL Server 2012 Express including SP1|SQLEXPR32_x86_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkId=267905)|  
|A 32-bit installation of **SQL Server 2008** or **SQL Server 2008 R2**|**In place upgrade** to 32-bit SQL Server 2012 including SP1|SQLServer2012SP1-FullSlipstream-x86-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x86-ENU.box from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|**64-bit Installations:**|||  
|A 64-bit version of any edition of SQL Server 2012|Upgrade to the 64-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x64-ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 64-bit version of SQL Server 2012 RTM Express|Upgrade to the 64-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x64-ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 64-bit version of only the client and manageability tools for SQL Server 2012 (including SQL Server 2012 Management Studio)|Upgrade the client and manageability tools to the 64-bit version of SQL Server 2012 SP1|SQLManagementStudio_x64_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 64-bit version of SQL Server 2012 Management Studio Express|Upgrade to the 64-bit version of SQL Server 2012 SP1 Management Studio Express|SQLManagementStudio_x64_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 64-bit version of any edition of SQL Server 2012 **and** a 64-bit version of the client and manageability tools (including SQL Server 2012 RTM Management Studio)|Upgrade all products to the 64-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x64-ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 64-bit version of one or more tools from the [Microsoft SQL Server 2012 RTM Feature Pack](http://www.microsoft.com/download/en/details.aspx?id=16978)|Upgrade the tools to the 64-bit version of Microsoft SQL Server 2012 SP1 Feature Pack|One or more files from [Microsoft SQL Server 2012 SP1 Feature Pack](http://go.microsoft.com/fwlink/p/?LinkID=268266)|  
|No 64-bit installation of SQL Server 2012|Install 64-bit Server 2012 including SP1 (New instance with SP1 pre-installed)|SQLServer2012SP1-FullSlipstream-x64-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x64-ENU.box from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|No 64-bit installation of SQL Server 2012 Management Studio|Install 64-bit SQL Server 2012 Management Studio including SP1|SQLManagementStudio_x64_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|No 64-bit version of SQL Server 2012 RTM Express|Install 64-bit SQL Server 2012 Express including SP1|SQLEXPR_x64_ENU.exe from [here](http://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 64-bit installation of **SQL Server 2008** or **SQL Server 2008 R2**|**In place upgrade** to 64-bit SQL Server 2012 including SP1|SQLServer2012SP1-FullSlipstream-x64-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x64-ENU.box from [here](http://go.microsoft.com/fwlink/p/?LinkID=268158)|  
  
![Arrow icon used with Back to Top link](../release-notes/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Contents](#bkmk_top)  
  
## <a name="bkmk_AS"></a>2.0 Analysis Services and PowerPivot  
  
### 2.1 PowerPivot Configuration Tool Does not Create the PowerPivot Gallery  
**Issue:** The PowerPivot Configuration Tool provisions a Team Site, and therefore the PowerPivot Gallery is not created.  
  
**Workaround:** Create a new app (library).  
  
1.  Verify the site collection feature **PowerPivot Feature Integration for Site Collections** is active.  
  
2.  From the **Site Contents** page of an existing site, click **add app**.  
  
3.  Click **PowerPivot Gallery**.  
  
### 2.2 To use PowerPivot for Excel with Excel 2013, you must use the add-in that is installed with Excel  
**Issue:** With Office 2010, PowerPivot for Excel is a stand-alone add-in that is downloadable from [http://www.microsoft.com/bi/powerpivot.aspx](http://www.microsoft.com/bi/powerpivot.aspx). Alternatively it can also be downloaded from the [Microsoft Download Center](http://www.microsoft.com/download/details.aspx?id=29074). Note that there are two versions of the PowerPivot add-in available as a download: One that shipped with SQL Server 2008 R2 and another that shipped with SQL Server 2012. However, for Office 2013, PowerPivot for Excel ships with Office and is installed when you install Excel. While the SQL Server 2008 R2 and SQL Server 2012 versions of PowerPivot for Excel 2010 are not compatible with Excel 2013, you still can install PowerPivot for Excel 2010 on your client computer if you want to run Excel 2010 side-by-side with Excel 2013. In other words, the two versions of Excel can coexist and so can the corresponding PowerPivot add-ins.  
  
**Workaround:** To use PowerPivot for Excel 2013 you must enable the COM add-in. From Excel 2013, select **File** | **Options** | **Add-Ins**. From the **Manage** drop-down box, select **COM Add-ins** and click **Go**. From **COM Add-ins**, select **Microsoft Office PowerPivot for Excel 2013** and click **Okay**.  
  
![Arrow icon used with Back to Top link](../release-notes/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Contents](#bkmk_top)  
  
## <a name="bkmk_RS"></a>3.0 Reporting Services  
  
### 3.1 Install and Configure SharePoint Server 2013 Prior to Installing Reporting Services  
**Issue:** Complete the following requirements **before** you install SQL Server Reporting Services (SSRS).  
  
1.  Run the SharePoint 2013 Products Preparation Tool.  
  
2.  Install SharePoint Server 2013.  
  
3.  Run the SharePoint 2013 Product Configuration Wizard, or complete an equivalent set of configuration steps to configure the SharePoint farm.  
  
**Workaround:**  If you installed [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode before the SharePoint farm was configured, the required work around depends on what other components are installed.  
  
### 3.2 Power View in SharePoint Server 2013 Requires Microsoft.AnalysisServices.SPClient.dll  
**Issue:**[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not install a required component, **Microsoft.AnalysisServices.SPClient.dll**. If you install SharePoint Server 2013 Preview and [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode, but do not download and install the PowerPivot for SharePoint 2013 installer package, **spPowerPivot.msi** then Power View will not work and Power View will exhibit the following symptoms.  
  
**Symptoms:** When you attempt to create a Power View report, you see an error message similar to the following:  
  
-   "Cannot create a connection to data source..."  
  
The inner error details will contain a message similar to the following:  
  
-   "The value 'SharePoint Principal' is not supported for the connection string property 'User Identity'."  
  
**Workaround:** Install the PowerPivot for SharePoint 2013 installer package (**spPowerPivot.msi**) on the SharePoint Server 2013. The installer package is available as part of the [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] feature pack. The feature pack can be downloaded from the [!INCLUDE[msCoName](../includes/msconame-md.md)] download center at [SQL Server 2012 SP1 Feature Pack](http://go.microsoft.com/fwlink/p/?LinkID=268266)  
  
### 3.3 Power View sheets in a PowerPivot workbook are deleted after a scheduled data refresh  
**Issue**: In the PowerPivot add-in for SharePoint, using **Scheduled Data Refresh** on a workbook with Power View will delete any Power View sheets.  
  
**Workaround**: To use **Scheduled Data Refresh** with Power View workbooks, create a PowerPivot workbook that is just the data model. Create a separate workbook with your Excel sheets and Power View sheets that links to the PowerPivot workbook with the data model. Only the PowerPivot workbook with the data model should be scheduled for data refresh.  
  
## <a name="bkmk_DQS"></a>4.0 Data Quality Services  
  
### 4.1 DQS available in the incorrect edition of SQL Server 2012  
**Issue:** In the [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] RTM release, the Data Quality Services (DQS) feature is available in SQL Server editions other than the Enterprise, Business Intelligence, and Developer editions. After installing SQL Server 2012 SP1, DQS will be unavailable in all editions except the Enterprise, Business Intelligence, and Developer editions.  
  
**Workaround**: If you are using DQS in a nonsupported edition, either upgrade to a supported edition or remove the dependency on this feature from your applications.  
  
![Arrow icon used with Back to Top link](../release-notes/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Contents](#bkmk_top)  
  
## <a name="bkmk_Express"></a>5.0 SQL Server Express  
  
### 5.1 Full Version of SQL Server Management Studio Available in SQL Server 2012 Express SP1  
The SQL Server 2012 Express Service Pack 1 (SP1) release includes the full version of SQL Server 2012 Management Studio (which was previously available only on the SQL Server 2012 DVD) instead of SQL Server 2012 Management Studio Express. To download and install SQL Server 2012 Express SP1, see [SQL Server 2012 Express Service Pack 1](http://go.microsoft.com/fwlink/p/?linkid=267905).  
  
![Arrow icon used with Back to Top link](../release-notes/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Contents](#bkmk_top)  
  
## <a name="bkmk_CDC"></a>6.0 Change Data Capture Service and Designer for Oracle by Attunity  
  
### 6.1 Upgrading the CDC Service and Designer  
**Issue:** If the Change Data Capture Designer for Oracle and the Change Data Capture Service for Oracle by Attunity are installed on your machine at the time that you install SQL Server 2012 SP1, these components are not upgraded by installing SP1.  
  
**Workaround:** To upgrade the CDC components to the latest version:  
  
1.  Download the .msi files for Change Data Capture Service for Oracle by Attunity from the [SQL Server 2012 SP1 Feature Pack download page](http://go.microsoft.com/fwlink/p/?LinkID=268266).  
  
2.  Run the .msi file.  
  
![Arrow icon used with Back to Top link](../release-notes/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Contents](#bkmk_top)  
  
## <a name="DACFx"></a>7.0 SQL Server Data-Tier Application Framework (DACFx)  
**In-place Upgrade Support**  
  
This version of the Data-Tier Application Framework (DACFx) supports in-place upgrade from previous versions, so it is not required to remove previous DACFx installations before upgrading to this release. You can find future releases of DACFx [here](https://msdn.microsoft.com/library/dn702988.aspx).  
  
**Support for Selective XML Index**  
  
SQL Server 2012 SP1 includes support for [Selective XML Index (SXI)](http://msdn.microsoft.com/en-us/598ecdcd-084b-4032-81b2-eed6ae9f5d44), a new SQL Server feature that provides a new way of indexing XML column data with increased performance and efficiency.  
  
DACFx now supports SXI indexes across all DAC scenarios and client tools. SXI is only supported in the latest version of SSDT. SSDT RTM and September 2012 versions do not support SXI.  
  
**Support for Native BCP data format**  
  
Previously, the data format used to store table data inside DACPAC and BACPAC packages was JSON. With this update, Native BCP is now the data persistence format. This change brings improved SQL Server data type fidelity to DACFx including support for SQL_Variant types as well as enhanced data deployment performance for large scale databases.  
  
**Preservation of Check Constraint state across package creation/deployment**  
  
Previously, DACFx did not preserve the state (WITH CHECK/NOCHECK) of check constraints defined on tables in the database schema or store this information inside DACPACs. This behavior could lead to potential issues on package deployment when there is existing table data that violates check constraints. With this update, DACFx now stores the current state of check constraints within the DACPAC when extracted from a database and appropriately restores this state upon package deployment.  
  
**Updates to SqlPackage.exe (DACFx command-line tool)**  
  
-   Extract DACPAC with data – Creates a database snapshot file (.dacpac) from a live SQL Server or Windows Azure SQL Database that contains data from user tables in addition to the database schema. These packages can be published to a new or existing SQL Server or Windows Azure SQL Database using the SqlPackage.exe Publish action. Data contained in package replaces the existing data in the target database.  
  
-   Export BACPAC - Creates a logical backup file (.bacpac) of a live SQL Server or Windows Azure SQL Database containing the database schema and user data which can be used to migrate a database from on-premise SQL Server to Windows Azure SQL Database. Databases compatible with Azure can be exported and then later imported between supported versions of SQL Server.  
  
-   Import BACPAC – Import a .bacpac file to create a new or populate an empty SQL Server or Windows Azure SQL Database.  
  
Full SqlPackage.exe documentation on MSDN can be found [here](http://msdn.microsoft.com/library/hh550080%28v=vs.103%29.aspx).  
  
**Package compatibility**  
  
This release introduces several forward compatibility scenarios for DAC packages.  
  
-   DAC packages created by this release that do not contain SXI elements or table data may be consumed by previous releases of DACFx (SQL Server 2012 RTM, SQL Server 2012 CU1, and DACFx September, 2012).  
  
-   All DAC packages created by previous versions of DACFx can be consumed by this release.  
  
## <a name="bkmk_known_issues_fixed"></a>8.0 Known Issues Fixed in this Service Pack  
For a complete list of bugs and known issues fixed in this service pack, see [this KB article](http://support.microsoft.com/kb/2674319).  
  
[Contents](#bkmk_top)  
  
## See Also  
[How to determine the version and edition of SQL Server](http://support.microsoft.com/kb/321185)  
[Features Supported by the Editions of SQL Server 2014](http://msdn.microsoft.com/en-us/5da61ff5-12b9-48e6-b3c8-0dacca1751c4)  
  
