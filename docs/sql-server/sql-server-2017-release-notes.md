---
title: "SQL Server 2017 Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "09/22/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
caps.latest.revision: 41
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# SQL Server 2017 Release Notes
This topic describes limitations and issues with SQL Server 2017. For related information, see:
- [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)
- [SQL Server on Linux release notes](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes)

**Try SQL Server!**
- [![Download from Evaluation Center](../includes/media/download2.png)](http://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server 2017](http://go.microsoft.com/fwlink/?LinkID=829477)
- [![Create Virtual Machine](../includes/media/azure-vm.png)](https://azure.microsoft.com/en-us/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm) [Spin up a Virtual Machine with SQL Server 2017](https://azure.microsoft.com/en-us/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm)

## SQL Server 2017 - general availability release (October 2017)
### Database Engine

- **Issue and customer impact:** After upgrade, the existing FILESTREAM network share may be no longer available.

- **Workaround:** First, reboot the computer and check if the FILESTREAM network share is available. If the share is still not available, do the following:

    1. In SQL Server Configuration Manager, right click the SQL Server instance, and click **Properties**. 
    2. In the **FILESTREAM** tab clear **Enable FILESTREAM for file I/O streaming access** , then click **Apply**.
    3. Check **Enable FILESTREAM for file I/O streaming access** again with the original share name and click **Apply**.

### Master Data Services (MDS)
- **Issue and customer impact:** 
On the user permissions page, when granting permission to the root level in the entity tree view, you see the following error:
`"The model permission cannot be saved. The object guid is not valid"`

- **Workarounds:** 
  - Grant permission on the sub nodes in the tree view instead of the root level.
  - or
  - Run the script described in this MDS team blog [error applying permission on entity level](http://sqlblog.com/blogs/mds_team/archive/2017/09/05/sql-server-2016-sp1-cu4-regression-error-while-applying-permission-on-entity-level-quick-workaround.aspx)

### Analysis Services
- **Issue and customer impact:** For tabular models at the 1400 compatibility level, when using Get Data, data connectors for some data sources such as  Amazon Redshift, IBM Netezza, and Impala, are not yet available.
- **Workaround:** None.   

- **Issue and customer impact:** Direct Query models at the 1400 compatibility level with perspectives can fail on querying or discovering metadata.
- **Workaround:** Remove perspectives and re-deploy.

### Tools
- **Issue and customer impact:** Running *DReplay* fails with the following message: "Error DReplay Unexpected error occurred!".
- **Workaround:** None.


![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 Release Candidate (RC2 - August 2017)
There are no SQL Server on Windows release notes for this release. See [SQL Server on Linux Release notes](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes).


![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 Release Candidate (RC1 - July 2017)
### SQL Server Integration Services (SSIS) (RC1 - July 2017)
- **Issue and customer impact:** The parameter *runincluster* of the stored procedure **[catalog].[create_execution]** is renamed to *runinscaleout* for consistency and readability.
- **Workaround:** If you have existing scripts to run packages in Scale Out, you have to change the parameter name from *runincluster* to *runinscaleout* to make the scripts work in RC1.

- **Issue and customer impact:** SQL Server Management Studio (SSMS) 17.1 and earlier versions can't trigger package execution in Scale Out in RC1. The error message is: "*@runincluster* is not a parameter for procedure **create_execution**." This issue is fixed in the next release of SSMS, version 17.2. Versions 17.2 and later of SSMS support the new parameter name and package execution in Scale Out. 
- **Workaround:** Until SSMS version 17.2 is available:
  1. Use your existing version of SSMS to generate the package execution script.
  2. Change the name of the *runincluster* parameter to *runinscaleout* in the script.
  3. Run the script.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 CTP 2.1 (May  2017)
### Documentation (CTP 2.1)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### SQL Server Reporting Services (CTP 2.1)

- **Issue and customer impact:** If you have both SQL Server Reporting Services and Power BI Report Server on the same machine and uninstall one of them, you will not be able to connect to the remaining report server with Report Server Configuration Manager.
- **Workaround** To work around this issue, you must perform the following operations after uninstalling one of the servers.

    1. Launch a command prompt in Administrator mode.
    2. Go to the directory where the remaining report server is installed.

        *Default location for Power BI Report Server: C:\Program Files\Microsoft Power BI Report Server*

        *Default location for SQL Server Reporting Services: C:\Program Files\Microsoft SQL Server Reporting Services*

    3. Then go to the next folder, which is either *SSRS* or *PBIRS* depending on what is remaining.
    4. Go to the WMI folder.
    5. Run the following command:

        ```
        regsvr32 /i ReportingServicesWMIProvider.dll
        ```

        If you see the following error, ignore it.

        ```
        The module "ReportingServicesWMIProvider.dll" was loaded but the entry-point DLLInstall was not found. Make sure that "ReportingServicesWMIProvider.dll" is a valid DLL or OCX file and then try again.
        ```

### TSqlLanguageService.msi (CTP 2.1)

- **Issue and customer impact:** After installing on a computer that has a 2016 version of *TSqlLanguageService.msi* installed (either through SQL Setup or as a standalone redistributable) the v13.* (SQL 2016) versions of *Microsoft.SqlServer.Management.SqlParser.dll* and *Microsoft.SqlServer.Management.SystemMetadataProvider.dll* are removed. Any applications that have a dependency on the 2016 versions of those assemblies will then cease to function, giving an error similar to: *error : Could not load file or assembly 'Microsoft.SqlServer.Management.SqlParser, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91' or one of its dependencies. The system cannot find the file specified.*

   In addition, attempts to reinstall a 2016 version of TSqlLanguageService.msi fail with the message: *Installation of Microsoft SQL Server 2016 T-SQL Language Service failed because a higher version already exists on the machine*.

- **Workaround** To work around this issue and fix an application that depends on the v13 version of the assemblies follow these steps:

   1. Go to **Add/Remove Programs**
   2. Find *Microsoft SQL Server vNext T-SQL Language Service CTP2.1*, right-click it, and select **Uninstall**.
   3. After the component is removed, repair the application that is broken or reinstall the appropriate version of *TSqlLanguageService.MSI*.

   This workaround removes the v14 version of those assemblies, so any applications that depend on the v14 versions will no longer function. If those assemblies are needed, then a separate installation without any side-by-side 2016 installs is required.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 CTP 2.0 (April  2017)
### Documentation (CTP 2.0)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### Always On availability groups

- **Issue and customer impact:** A SQL Server instance hosting an availability group secondary replica crashes if the SQL Server major version is lower than the instance that hosts the primary replica. Affects upgrades from all supported versions of SQL Server that host availability groups to SQL Server [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] CTP 2.0. This happens under the following steps. 

> 1. User upgrades SQL Server instance hosting secondary replica in accordance with [best practices](../database-engine/availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md).
> 2. After upgrade, a failover occurs and a newly upgraded secondary becomes primary before completing upgrade for all secondary replicas in the availability group. The old primary is now a secondary, which is lower version than primary.
> 3. The availability group is in an unsupported configuration and any remaining secondary replicas might be vulnerable to crash. 

- **Workaround** Connect to the SQL Server instance hosting the new primary replica and remove the faulty secondary replica from the configuration.

   `ALTER AVAILABILITY GROUP agName REMOVE REPLICA ON NODE instanceName`

   The instance of SQL Server that hosted the secondary replica recovers.

##  ![info_tip](../sql-server/media/info-tip.png) Get Help 
- [Stack Overflow (tag sql-server) - ask SQL development questions](http://stackoverflow.com/questions/tagged/sql-server)
- [MSDN Forums - ask technical questions](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
- [Microsoft Connect - report bugs and request features](https://connect.microsoft.com/SQLServer/Feedback)
- [Reddit - general discussion about SQL Server](https://www.reddit.com/r/SQLServer/)
- [Microsoft SQL Server License Terms and Information](https://www.microsoft.com/en-us/download/details.aspx?id=39299) 

## More information
- [SQL Server Reporting Services release notes](../reporting-services/reporting-services-release-notes.md).
- [Known Issues for Machine Learning Services](../advanced-analytics/known-issues-for-sql-server-machine-learning-services.md)

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)
