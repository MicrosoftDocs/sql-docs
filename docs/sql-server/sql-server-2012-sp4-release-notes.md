---
title: "SQL Server 2012 Service Pack release notes | Microsoft Docs"
ms.prod: sql
ms.technology: install
ms.custom: ""
ms.date: 02/26/2018
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 67cb8b3e-3d82-47f4-840d-0f12a3bff565
author: craigg-msft
ms.author: craigg
manager: jhubbard
monikerRange: "= sql-server-2014 || = sqlallproducts-allversions"
---
# SQL Server 2012 Service Pack release notes
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]
This topic contains the aggregated release notes of the four service packs for SQL Server 2012. Each service pack is cumulative of prior service packs.

The Service Packs are available online only, not on the installation media, and can be downloaded as follows:
- [SQL Server 2012 SP4 ](https://go.microsoft.com/fwlink/?linkid=846937)
- [SQL Server 2012 SP3](https://support.microsoft.com/help/3072779/sql-server-2012-service-pack-3-release-information)
- [SQL Server 2012 SP2](https://support.microsoft.com/KB/2958429)
- [SQL Server 2012 SP1](https://go.microsoft.com/fwlink/p/?LinkID=268158)

## Service Pack 4 release notes

### Download pages

- [SQL Server 2012 SP4 Feature Pack](https://go.microsoft.com/fwlink/?linkid=846907)
- [SQL Server 2012 SP4 Patch installation](https://go.microsoft.com/fwlink/?linkid=846829)
- [SQL Server 2012 SP4 Express](https://go.microsoft.com/fwlink/?linkid=846905)


### Performance and scale improvements
- **Improved distribution agent cleanup procedure** - An oversized distribution database caused blocking and deadlock situation. An improved cleanup procedure aims to eliminate some of these blocking or deadlock scenarios. 
- **Dynamic Memory Object Scaling** - Dynamically partition memory objects based on number of nodes and cores to scale on modern hardware. The goal of dynamic promotion is to prevent potential bottlenecks and automatically partition a thread safe memory object. Unpartitioned memory objects can be dynamically promoted to be partitioned by node. The number of partitions equals the number of NUMA nodes. Memory objects partitioned by node can by further promoted to be partitioned by CPU, where the number of partitions equals number of CPUs.
- **Enable > 8TB for Buffer Pool** - Enable 128-TB Virtual address space for buffer pool usage
- **Change Tracking Cleanup** - Improved change tracking cleanup performance and efficiency for Change Tracking side tables. 

### Supportability and diagnostics improvements
- **Full dumps support for Replication Agents** - Today if replication agents encounter an unhandled exception, the default behavior is to create a mini dump of the exception symptoms. The default behavior requires complex troubleshooting steep for unhandled exceptions. SP4 introduces a new Registry key, which supports the creation of a full dump for Replication Agents.
- **Enhanced diagnostics in showplan XML** - Showplan XML has been enhanced to expose information about enabled trace flags, memory fractions for optimized nested loop join, CPU time, and elapsed time. 
- **Better correlation between diagnostics XE and DMVs** - Query_hash and query_plan_hash fields are used for identifying a query uniquely. DMV defines them as varbinary(8), while XEvent defines them as UINT64. Since SQL server does not have "unsigned bigint", casting does not always work. This improvement introduces new XEvent action/filter columns equivalent to query_hash and query_plan_hash except they are defined as INT64 which can help correlating queries between XE and DMVs. 
- **Better memory grant/usage diagnostics** - New query_memory_grant_usage XEvent (backport from Server 2016 SP1)
- **Add protocol tracing to SSL negotiation steps**  - Add bit trace information for successful/failed negotiation, including the protocol etc. Can be useful when troubleshooting connectivity scenarios while, for example, deploying TLS 1.2
- **Setting correct compatibility level for distribution database** - After Service Pack Installation the Distribution database compatibility level changes to 90. The level change was due to an issue in sp_vupgrade_replication stored procedure. The SP has now been changed to set the correct compatibility level for the distribution database. 
- **New DBCC command for cloning a database** - Clone database is a new DBCC command added that allows power users such as CSS to trouble shoot existing production databases by cloning the schema and metadata, without the data. The call is performed with DBCC clonedatabase ('source_database_name', 'clone_database_name'). Cloned databases should not be used in production environments. To see if a database has been generated from a call to clone database you can use the following command, select DATABASEPROPERTYEX('clonedb', 'isClone').The return value of 1 is true, and 0 is false. 
- **TempDB file and file size information in SQL Error Log** - If size and auto growth is different for TempDB data files during startup, print the number of files and trigger a warning.
- **IFI support messages in SQL Server Error Log** -  Indicate in the error log that Database Instant File Initialization is enabled/disabled
- **New DMF to replace DBCC INPUTBUFFER** -  A new Dynamic Management Function sys.dm_input_buffer that takes the session_id as parameter is introduced to replace DBCC INPUTBUFFER
- **XEvents enhancement for read routing failure for an Availability Group** - Currently the read_only_rout_fail XEvent only gets fired if there is a routing list present, but none of the servers in the routing list is available for connections. This improvement includes additional information to assist with troubleshooting and it also expands on the code points where the XEvent gets fired. 
- **Improved handling of Service Broker with Availability group failover** - Currently when Service Broker is enabled on an Availability Group Databases, during an AG failover all Service broker connections that originated on the Primary Replica are left open. The improvement closes all such open connections during an AG failover.
- **Automatic Soft-NUMA partitioning** - With SQL 2014 SP2, Automatic [Soft-NUMA](../database-engine/configure-windows/soft-numa-sql-server.md) partitioning is introduced when Trace Flag 8079 is enabled at the server level. When Trace Flag 8079 is enabled during startup, SQL Server 2014 SP2 interrogates the hardware layout and automatically configures Soft NUMA on systems reporting 8 or more CPUs per NUMA node. The automatic soft NUMA behavior is Hyperthread (HT/logical processor) aware. The partitioning and creation of additional nodes scales background processing by increasing the number of listeners, scaling, and network and encryption capabilities. It is recommended to first test the performance of the workload with Auto-Soft NUMA before it is turned ON in production.

## Service Pack 3 release notes

### Download pages
- [SQL Server 2012 SP3 Feature Pack](https://go.microsoft.com/fwlink/?linkid=615935)
- [SQL Server 2012 SP3 Express](https://go.microsoft.com/fwlink/?linkid=692144)

For more detailed information to identify the location and name of the file to download based on your currently installed version, see the "Select the correct file to download" section in [SQL Server 2012 Service Pack 3 release information](https://support.microsoft.com/help/3072779/sql-server-2012-service-pack-3-release-information).

## Service Pack 2 release notes
  
### Download pages 
- [SQL Server 2012 SP2 Feature Pack](https://go.microsoft.com/fwlink/?LinkID=401008)
- [SQL Server 2012 SP2 Express](https://go.microsoft.com/fwlink/?LinkID=401007)

Use the table below to identify the location and name of the file to download based on your currently installed version. Download pages have system requirements and basic installation instructions.  

|If your current installed version is...|And you want to...|Download and install...|  
|---|---|---|   
|32-bit Installations:|||  
|A 32-bit version of any edition of SQL Server 2012|Upgrade to the 32-bit version of SQL Server 2012 SP2|**SQLServer2012SP2-KB2958429-**<arch>**-**<lang id>**.exe** from [SQL Server 2012 SP2 download page](https://go.microsoft.com/fwlink/?LinkID=401006)|  
|A 32-bit version of SQL Server 2012 RTM Express|Upgrade to the 32-bit version of SQL Server 2012 Express SP2|**SQLEXPR_**<arch>**_**<lang>**.msi** from [SQL Server 2012 SP2 Express download page](https://go.microsoft.com/fwlink/?LinkID=401007)|  
|A 32-bit version of only the client and manageability tools for SQL Server 2012 (including SQL Server 2012 Management Studio)|Upgrade the client and manageability tools to the 32-bit version of SQL Server 2012 SP2|**SQLEXPRWT_**<arch>**_**<lang>**.msi** from [SQL Server 2012 SP2 Express download page](https://go.microsoft.com/fwlink/?LinkID=401007)|  
|A 32-bit version of SQL Server 2012 Management Studio Express|Upgrade to the 32-bit version of SQL Server 2012 SP2 Management Studio Express|**SQLManagementStudio_**<arch>**_**<lang>**.msi** from [SQL Server 2012 SP2 Express download page](https://go.microsoft.com/fwlink/?LinkID=401007)|  
|A 32-bit version of any edition of SQL Server 2012 and a 32-bit version of the client and manageability tools (including SQL Server 2012 RTM Management Studio)|Upgrade all products to the 32-bit version of SQL Server 2012 SP2|**SQLEXPRADV_**<arch>**_**<lang>**.msi** from [SQL Server 2012 SP2 Express download page.](https://go.microsoft.com/fwlink/?LinkID=401007)|  
|A 32-bit version of one or more tools from the [Microsoft SQL Server 2012 RTM Feature Pack](https://www.microsoft.com/download/details.aspx?id=29065) or the [Microsoft SQL Server 2012 SP1 Feature Pack](https://go.microsoft.com/fwlink/p/?LinkID=268266)|Upgrade the tools to the 32-bit version of Microsoft SQL Server 2012 SP2 Feature Pack|One or more tools from Microsoft [SQL Server 2012 SP2 Feature Pack download page](https://go.microsoft.com/fwlink/?LinkID=401008)|  
|64-bit Installations:|||  
|A 64-bit version of any edition of SQL Server 2012|Upgrade to the 64-bit version of SQL Server 2012 SP2|SQLServer2012SP2-KB2958429-<arch>-<langid>.exe from [SQL Server 2012 SP2 download page](https://go.microsoft.com/fwlink/?LinkID=401006)|  
|A 64-bit version of SQL Server 2012 RTM Express|Upgrade to the 64-bit version of SQL Server 2012 SP2|**SQLEXPR_**<arch>**_**<lang>**.msi** from [SQL Server 2012 SP2 Express download page](https://go.microsoft.com/fwlink/?LinkID=401007)|  
|A 64-bit version of only the client and manageability tools for SQL Server 2012 (including SQL Server 2012 Management Studio)|Upgrade the client and manageability tools to the 64-bit version of SQL Server 2012 SP2|**SQLEXPRWT_**<arch>**_**<lang>**.msi** from [SQL Server 2012 SP2 Express download page](https://go.microsoft.com/fwlink/?LinkID=401007)|  
|A 64-bit version of SQL Server 2012 Management Studio Express|Upgrade to the 64-bit version of SQL Server 2012 SP2 Management Studio Express|**SQLManagementStudio_**<arch>**_**<lang>**.msi** from [SQL Server 2012 SP2 Express download page](https://go.microsoft.com/fwlink/?LinkID=401007)|  
|A 64-bit version of one or more tools from the [Microsoft SQL Server 2012 RTM Feature Pack](https://www.microsoft.com/download/details.aspx?id=29065) or the [Microsoft SQL Server 2012 SP1 Feature Pack](https://go.microsoft.com/fwlink/p/?LinkID=268266)|Upgrade the tools to the 64-bit version of Microsoft SQL Server 2012 SP2 Feature Pack|One or more tools from Microsoft [SQL Server 2012 SP2 Feature Pack download page](https://go.microsoft.com/fwlink/?LinkID=401008)|   


## Service Pack 1 release notes

### Download pages  
- [SQL Server 2012 SP1 Feature Pack](https://go.microsoft.com/fwlink/p/?LinkID=268158)
- [SQL Server 2012 SP1 Express](https://go.microsoft.com/fwlink/p/?LinkID=26815)


Use the following table to determine which file to download and install. Verify that you have the correct system requirements before installing the service pack. The system requirements are provided on the download pages that are linked to in the table.  

|If your current installed version is...|And you want to...|Download and install...|  
|---|---|---|  
|**32-bit Installations:**|||  
|A 32-bit version of any edition of SQL Server 2012|Upgrade to the 32-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x86-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 32-bit version of SQL Server 2012 RTM Express|Upgrade to the 32-bit version of SQL Server 2012 Express SP1|SQLServer2012SP1-KB2674319-x86-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 32-bit version of only the client and manageability tools for SQL Server 2012 (including SQL Server 2012 Management Studio)|Upgrade the client and manageability tools to the 32-bit version of SQL Server 2012 SP1|SQLManagementStudio_x86_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 32-bit version of SQL Server 2012 Management Studio Express|Upgrade to the 32-bit version of SQL Server 2012 SP1 Management Studio Express|SQLManagementStudio_x86_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 32-bit version of any edition of SQL Server 2012 **and** a 32-bit version of the client and manageability tools (including SQL Server 2012 RTM Management Studio)|Upgrade all products to the 32-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x86-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 32-bit version of one or more tools from the [Microsoft SQL Server 2012 RTM Feature Pack](https://www.microsoft.com/download/details.aspx?id=16978)|Upgrade the tools to the 32-bit version of Microsoft SQL Server 2012 SP1 Feature Pack|One or more files from [Microsoft SQL Server 2012 SP1 Feature Pack](https://go.microsoft.com/fwlink/p/?LinkID=268266)|  
|No 32-bit installation of SQL Server 2012|Install 32-bit Server 2012 including SP1 (New instance with SP1 pre-installed)|SQLServer2012SP1-FullSlipstream-x86-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x86-ENU.box from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|No 32-bit installation of SQL Server 2012 Management Studio|Install 32-bit SQL Server 2012 Management Studio including SP1|SQLManagementStudio_x86_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=267905)|  
|No 32-bit version of SQL Server 2012 RTM Express|Install 32-bit SQL Server 2012 Express including SP1|SQLEXPR32_x86_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=267905)|  
|A 32-bit installation of **SQL Server 2008** or **SQL Server 2008 R2**|**In place upgrade** to 32-bit SQL Server 2012 including SP1|SQLServer2012SP1-FullSlipstream-x86-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x86-ENU.box from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|**64-bit Installations:**|||  
|A 64-bit version of any edition of SQL Server 2012|Upgrade to the 64-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x64-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 64-bit version of SQL Server 2012 RTM Express|Upgrade to the 64-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x64-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 64-bit version of only the client and manageability tools for SQL Server 2012 (including SQL Server 2012 Management Studio)|Upgrade the client and manageability tools to the 64-bit version of SQL Server 2012 SP1|SQLManagementStudio_x64_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 64-bit version of SQL Server 2012 Management Studio Express|Upgrade to the 64-bit version of SQL Server 2012 SP1 Management Studio Express|SQLManagementStudio_x64_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 64-bit version of any edition of SQL Server 2012 **and** a 64-bit version of the client and manageability tools (including SQL Server 2012 RTM Management Studio)|Upgrade all products to the 64-bit version of SQL Server 2012 SP1|SQLServer2012SP1-KB2674319-x64-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|A 64-bit version of one or more tools from the [Microsoft SQL Server 2012 RTM Feature Pack](https://www.microsoft.com/download/en/details.aspx?id=16978)|Upgrade the tools to the 64-bit version of Microsoft SQL Server 2012 SP1 Feature Pack|One or more files from [Microsoft SQL Server 2012 SP1 Feature Pack](https://go.microsoft.com/fwlink/p/?LinkID=268266)|  
|No 64-bit installation of SQL Server 2012|Install 64-bit Server 2012 including SP1 (New instance with SP1 pre-installed)|SQLServer2012SP1-FullSlipstream-x64-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x64-ENU.box from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  
|No 64-bit installation of SQL Server 2012 Management Studio|Install 64-bit SQL Server 2012 Management Studio including SP1|SQLManagementStudio_x64_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|No 64-bit version of SQL Server 2012 RTM Express|Install 64-bit SQL Server 2012 Express including SP1|SQLEXPR_x64_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkID=267905)|  
|A 64-bit installation of **SQL Server 2008** or **SQL Server 2008 R2**|**In place upgrade** to 64-bit SQL Server 2012 including SP1|SQLServer2012SP1-FullSlipstream-x64-ENU.exe **and** SQLServer2012SP1-FullSlipstream-x64-ENU.box from [here](https://go.microsoft.com/fwlink/p/?LinkID=268158)|  

### Known Issues Fixed in this Service Pack  
For a complete list of bugs and known issues fixed in this service pack, see [this KB article](https://support.microsoft.com/kb/2674319).   

### Reinstalling  instances of SQL Server Failover Cluster fails if you use the same IP address  
**Issue:** If you specify an incorrect IP address during an installation of a SQL Server Failover Cluster instance, the installation fails. After you uninstall the failed instance, and if you try to reinstall the SQL Server failover cluster instance with the same instance name, and correct IP address, the installation fails. The failure is because of the duplicate resource group left behind by the previous installation.  
  
**Workaround:** To resolve this issue, use a different instance name during the reinstallation, or manually delete the resource group before reinstalling. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster](failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md). 
  
### Analysis Services and PowerPivot  
  
##### PowerPivot configuration tool does not create the PowerPivot Gallery  
**Issue:** The PowerPivot Configuration Tool provisions a Team Site, and therefore the PowerPivot Gallery is not created.  
  
**Workaround:** Create a new app (library).  
  
1.  Verify the site collection feature **PowerPivot Feature Integration for Site Collections** is active.  
  
2.  From the **Site Contents** page of an existing site, click **add app**.  
  
3.  Click **PowerPivot Gallery**.  
  
#### To use PowerPivot for Excel with Excel 2013, you must use the add-in that is installed with Excel  
**Issue:** With Office 2010, PowerPivot for Excel is a stand-alone add-in that is downloadable from [https://www.microsoft.com/bi/powerpivot.aspx](https://www.microsoft.com/bi/powerpivot.aspx). Alternatively it can also be downloaded from the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=29074). Note that there are two versions of the PowerPivot add-in available as a download: One that shipped with SQL Server 2008 R2 and another that shipped with SQL Server 2012. However, for Office 2013, PowerPivot for Excel ships with Office and is installed when you install Excel. While the SQL Server 2008 R2 and SQL Server 2012 versions of PowerPivot for Excel 2010 are not compatible with Excel 2013, you still can install PowerPivot for Excel 2010 on your client computer if you want to run Excel 2010 side-by-side with Excel 2013. In other words, the two versions of Excel can coexist and so can the corresponding PowerPivot add-ins.  
  
**Workaround:** To use PowerPivot for Excel 2013 you must enable the COM add-in. From Excel 2013, select **File** | **Options** | **Add-Ins**. From the **Manage** drop-down box, select **COM Add-ins** and click **Go**. From **COM Add-ins**, select **Microsoft Office PowerPivot for Excel 2013** and click **Okay**.  
  
### Reporting Services  
  
#### Install and Configure SharePoint Server 2013 prior to installing Reporting Services  
**Issue:** Complete the following requirements **before** you install SQL Server Reporting Services (SSRS).  
  
1.  Run the SharePoint 2013 Products Preparation Tool.  
  
2.  Install SharePoint Server 2013.  
  
3.  Run the SharePoint 2013 Product Configuration Wizard, or complete an equivalent set of configuration steps to configure the SharePoint farm.  
  
**Workaround:**  If you installed [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode before the SharePoint farm was configured, the required work around depends on what other components are installed.  
  
#### Power View in SharePoint Server 2013 Requires Microsoft.AnalysisServices.SPClient.dll  
**Issue:**[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not install a required component, **Microsoft.AnalysisServices.SPClient.dll**. If you install SharePoint Server 2013 Preview and [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode, but do not download and install the PowerPivot for SharePoint 2013 installer package, **spPowerPivot.msi** then Power View will not work and Power View will exhibit the following symptoms.  
  
**Symptoms:** When you attempt to create a Power View report, you see an error message similar to the following:  
  
-   "Cannot create a connection to data source..."  
  
The inner error details will contain a message similar to the following:  
  
-   "The value 'SharePoint Principal' is not supported for the connection string property 'User Identity'."  
  
**Workaround:** Install the PowerPivot for SharePoint 2013 installer package (**spPowerPivot.msi**) on the SharePoint Server 2013. The installer package is available as part of the [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] feature pack. The feature pack can be downloaded from the [!INCLUDE[msCoName](../includes/msconame-md.md)] download center at [SQL Server 2012 SP1 Feature Pack](https://go.microsoft.com/fwlink/p/?LinkID=268266)  
  
#### Power View sheets in a PowerPivot workbook are deleted after a scheduled data refresh  
**Issue**: In the PowerPivot add-in for SharePoint, using **Scheduled Data Refresh** on a workbook with Power View will delete any Power View sheets.  
  
**Workaround**: To use **Scheduled Data Refresh** with Power View workbooks, create a PowerPivot workbook that is just the data model. Create a separate workbook with your Excel sheets and Power View sheets that links to the PowerPivot workbook with the data model. Only the PowerPivot workbook with the data model should be scheduled for data refresh.  
  
### Data Quality Services  
  
#### DQS available in the incorrect edition of SQL Server 2012  
**Issue:** In the [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] RTM release, the Data Quality Services (DQS) feature is available in SQL Server editions other than the Enterprise, Business Intelligence, and Developer editions. After installing SQL Server 2012 SP1, DQS will be unavailable in all editions except the Enterprise, Business Intelligence, and Developer editions.  
  
**Workaround**: If you are using DQS in a unsupported edition, either upgrade to a supported edition or remove the dependency on this feature from your applications.  
  
### SQL Server Express  
  
#### Full Version of SQL Server Management Studio Available in SQL Server 2012 Express SP1  
The SQL Server 2012 Express Service Pack 1 (SP1) release includes the full version of SQL Server 2012 Management Studio (which was previously available only on the SQL Server 2012 DVD) instead of SQL Server 2012 Management Studio Express. To download and install SQL Server 2012 Express SP1, see [SQL Server 2012 Express Service Pack 1](https://go.microsoft.com/fwlink/p/?linkid=267905).  
  
### Change Data Capture Service and Designer for Oracle by Attunity  
  
#### Upgrading the CDC Service and Designer  
**Issue:** If the Change Data Capture Designer for Oracle and the Change Data Capture Service for Oracle by Attunity are installed on your machine at the time that you install SQL Server 2012 SP1, these components are not upgraded by installing SP1.  
  
**Workaround:** To upgrade the CDC components to the latest version:  
  
1.  Download the .msi files for Change Data Capture Service for Oracle by Attunity from the [SQL Server 2012 SP1 Feature Pack download page](https://go.microsoft.com/fwlink/p/?LinkID=268266).  
  
2.  Run the .msi file.  
  
### SQL Server Data-Tier Application Framework (DACFx)  
**In-place Upgrade Support**  
  
This version of the Data-Tier Application Framework (DACFx) supports in-place upgrade from previous versions, so it is not required to remove previous DACFx installations before upgrading to this release. You can find future releases of DACFx [here](https://msdn.microsoft.com/library/dn702988.aspx).  
  
**Support for Selective XML Index**  
  
SQL Server 2012 SP1 includes support for [Selective XML Index (SXI)](https://msdn.microsoft.com/598ecdcd-084b-4032-81b2-eed6ae9f5d44), a new SQL Server feature that provides a new way of indexing XML column data with increased performance and efficiency.  
  
DACFx now supports SXI indexes across all DAC scenarios and client tools. SXI is only supported in the latest version of SSDT. SSDT RTM and September 2012 versions do not support SXI.  
  
**Support for Native BCP data format**  
  
Previously, the data format used to store table data inside DACPAC and BACPAC packages was JSON. With this update, Native BCP is now the data persistence format. This change brings improved SQL Server data type fidelity to DACFx including support for SQL_Variant types as well as enhanced data deployment performance for large scale databases.  
  
**Preservation of Check Constraint state across package creation/deployment**  
  
Previously, DACFx did not preserve the state (WITH CHECK/NOCHECK) of check constraints defined on tables in the database schema or store this information inside DACPACs. This behavior could lead to potential issues on package deployment when there is existing table data that violates check constraints. With this update, DACFx now stores the current state of check constraints within the DACPAC when extracted from a database and appropriately restores this state upon package deployment.  
  
**Updates to SqlPackage.exe (DACFx command-line tool)**  
  
-   Extract DACPAC with data - Creates a database snapshot file (.dacpac) from a live SQL Server or Windows Azure SQL Database that contains data from user tables in addition to the database schema. These packages can be published to a new or existing SQL Server or Windows Azure SQL Database using the SqlPackage.exe Publish action. Data contained in package replaces the existing data in the target database.  
  
-   Export BACPAC - Creates a logical backup file (.bacpac) of a live SQL Server or Windows Azure SQL Database containing the database schema and user data which can be used to migrate a database from on-premise SQL Server to Windows Azure SQL Database. Databases compatible with Azure can be exported and then later imported between supported versions of SQL Server.  
  
-   Import BACPAC - Import a .bacpac file to create a new or populate an empty SQL Server or Windows Azure SQL Database.  
  
Full SqlPackage.exe documentation on MSDN can be found [here](https://msdn.microsoft.com/library/hh550080%28v=vs.103%29.aspx).  
  
**Package compatibility**  
  
This release introduces several forward compatibility scenarios for DAC packages.  
  
-   DAC packages created by this release that do not contain SXI elements or table data may be consumed by previous releases of DACFx (SQL Server 2012 RTM, SQL Server 2012 CU1, and DACFx September, 2012).  
  
-   All DAC packages created by previous versions of DACFx can be consumed by this release.  
  
## See Also
- [Install SQL Server 2012 Servicing Updates](https://msdn.microsoft.com/library/hh479746(v=sql.110).aspx)
- [How to identify your SQL Server version and edition](https://support.microsoft.com/help/321185)
- [Install SQL Server 2012 Servicing Updates](https://msdn.microsoft.com/library/hh479746(v=sql.110).aspx)
- [How to identify your SQL Server version and edition ](https://support.microsoft.com/help/321185) 
- [How to determine the version and edition of SQL Server](https://support.microsoft.com/kb/321185)  
- [Features Supported by the Editions of SQL Server 2014](https://msdn.microsoft.com/5da61ff5-12b9-48e6-b3c8-0dacca1751c4)  

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
