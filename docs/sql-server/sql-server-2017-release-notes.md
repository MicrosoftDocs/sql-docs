---
title: "SQL Server 2017 Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "05/16/2017"
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
This topic describes limitations and issues with [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)]. For related information, see the following:

- [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md).
- [SQL Server on Linux Release notes](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes).

 **Try it out:**    
   -   [![Download from Evaluation Center](../analysis-services/media/download.png)](http://go.microsoft.com/fwlink/?LinkID=829477)  Download [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] from the **[Evaluation Center](http://go.microsoft.com/fwlink/?LinkID=829477)**

## SQL Server 2017 Release Candidate (RC2 - August 2017)
There are no release notes for this release.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 Release Candidate (RC1 - July 2017)
### SQL Server Integration Services (SSIS) (RC1 - July 2017)
- **Issue and customer impact:** The parameter *runincluster* of the stored procedure **[catalog].[create_execution]** is renamed to *runinscaleout* for consistency and readability.
- **Workaround:** If you have existing scripts to run packages in Scale Out, you have to change the parameter name from *runincluster* to *runinscaleout* to make the scripts work in RC1.

- **Issue and customer impact:** SQL Server Management Studio (SSMS) 17.1 and earlier versions can't trigger package execution in Scale Out in RC1. The error message is: "*@runincluster* is not a parameter for procedure **create_execution**." This issue is fixed in the next release of SSMS, version 17.2. Versions 17.2 and later of SSMS support the new parameter name and package execution in Scale Out. 
- **Workaround:** Until SSMS version 17.2 is available, you can use your existing version of SSMS to generate the package execution script, then change the name of the *runincluster* parameter to *runinscaleout* in the script, and run the script.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 CTP 2.1 (May  2017)
### Documentation (CTP 2.1)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### SQL Server Reporting Services (CTP 2.1)

- **Issue and customer impact:** If you have both SQL Server Reporting Services and Power BI Report Server on the same machine and uninstall one of them, you will no longer be able to connect to the remaining report server with Report Server Configuration Manager.
- **Workaround** To work around this issue, you must perform the following operations after uninstalling one of the servers.

    1. Launch a command prompt in Administrator mode.
    2. Go to the directory where the remaining report server is installed.

        *Default location for Power BI Report Server: C:\Program Files\Microsoft Power BI Report Server*

        *Default location for SQL Server Reporting Services: C:\Program Files\Microsoft SQL Server Reporting Services*

    3. Then go to the next folder. This will either be *SSRS* or *PBIRS* depending on what is remaining.
    4. Go to the WMI folder.
    5. Run the following command:

        ```
        regsvr32 /i ReportingServicesWMIProvider.dll
        ```

        You can ignore the following error, if you see it.

        ```
        The module "ReportingServicesWMIProvider.dll" was loaded but the entry-point DLLInstall was not found. Make sure that "ReportingServicesWMIProvider.dll" is a valid DLL or OCX file and then try again.
        ```

### TSqlLanguageService.msi (CTP 2.1)

- **Issue and customer impact:** After installing on a machine that has a 2016 version of *TSqlLanguageService.msi* installed (either through SQL Setup or as a standalone redistributable) the v13.* (SQL 2016) versions of *Microsoft.SqlServer.Management.SqlParser.dll* and *Microsoft.SqlServer.Management.SystemMetadataProvider.dll* are removed. Any applications that have a dependency on the 2016 versions of those assemblies will then cease to function, giving an error similar to: *error : Could not load file or assembly 'Microsoft.SqlServer.Management.SqlParser, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91' or one of its dependencies. The system cannot find the file specified.*

   In addition, attempts to reinstall a 2016 version of TSqlLanguageService.msi will fail with the message: *Installation of Microsoft SQL Server 2016 T-SQL Language Service failed because a higher version already exists on the machine*.

- **Workaround** To work around this issue and fix an application that depends on the v13 version of the assemblies follow these steps:

   1. Go to **Add/Remove Programs**
   1. Find *Microsoft SQL Server vNext T-SQL Language Service CTP2.1*, right-click it, and select **Uninstall**.
   1. After the component is removed, repair the application that is broken (or reinstall the appropriate version of *TSqlLanguageService.MSI*)

   This workaround will remove the v14 version of those assemblies, so any applications that depend on the v14 versions will no longer function. If those assemblies are needed, then a separate installation without any side-by-side 2016 installs is required.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 CTP 2.0 (April  2017)
### Documentation (CTP 2.0)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### Always On availability groups

- **Issue and customer impact:** A SQL Server instance hosting an availability group secondary replica crashes if the SQL Server major version is lower than the instance that hosts the primary replica. Affects upgrades from all supported versions of SQL Server that host availability groups to SQL Server [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] CTP 2.0. This happens under the following steps. 

> 1. User upgrades SQL Server instance hosting secondary replica in accordance with [best practices](../database-engine/availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md).
> 2. After upgrade, a failover occurs and newly upgraded secondary becomes primary before completing upgrade for all secondary replicas in the availability group. The old primary is now a secondary which is lower version than primary.
> 3. The availability group is in an unsupported configuration and any remaining secondary replicas might be vulnerable to crash. 

- **Workaround** Connect to the SQL Server instance hosting the new primary replica and remove the faulty secondary replica from the configuration.

   `ALTER AVAILABILITY GROUP agName REMOVE REPLICA ON NODE instanceName`

   The instance of SQL Server that hosted the secondary replica recovers.

##  ![info_tip](../sql-server/media/info-tip.png) Engage with the SQL Server engineering team 
- [Stack Overflow (tag sql-server) - ask technical questions](http://stackoverflow.com/questions/tagged/sql-server)
- [MSDN Forums - ask technical questions](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
- [Microsoft Connect - report bugs and request features](https://connect.microsoft.com/SQLServer/Feedback)
- [Reddit - general discussion about R](https://www.reddit.com/r/SQLServer/)

## More information
- [SQL Server Reporting Services release notes](../reporting-services/reporting-services-release-notes.md).
- [Known Issues for Machine Learning Services](../advanced-analytics/known-issues-for-sql-server-machine-learning-services.md)

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)