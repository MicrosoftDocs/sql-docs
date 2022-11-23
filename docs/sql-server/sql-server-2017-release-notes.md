---
title: "SQL Server 2017 Release Notes | Microsoft Docs"
description: This article describes limitations and issues with SQL Server 2017 and provides links to related information.
ms.custom: ""
ms.date: "11/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: release-landing
ms.topic: conceptual
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-2017"
---
# SQL Server 2017 Release Notes
[!INCLUDE[SQL Server 2017](../includes/applies-to-version/sqlserver2017.md)]
This article describes limitations and issues with SQL Server 2017. For related information, see:
- [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)
- [SQL Server on Linux release notes](../linux/sql-server-linux-release-notes-2017.md)
- [SQL Server 2017 Cumulative updates](https://aka.ms/sql2017cu) for information about the latest cumulative update (CU) release

**Try SQL Server!**
- :::image type="icon" source="../includes/media/download.svg"::: [Download SQL Server 2017](https://go.microsoft.com/fwlink/?LinkID=829477)
- :::image type="icon" source="../includes/media/azure-vm.svg"::: [Spin up a Virtual Machine with SQL Server 2017](https://azure.microsoft.com/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm)

> [!NOTE]
> SQL Server 2019 preview is now available. For more information, see [What's New in SQL Server 2019](./what-s-new-in-sql-server-2019.md?preserve-view=true&view=sql-server-ver15).

## SQL Server 2017 - general availability release (October 2017)
### Database Engine

- **Issue and customer impact:** After upgrade, the existing FILESTREAM network share may be no longer available.

- **Workaround:** First, reboot the computer and check if the FILESTREAM network share is available. If the share is still not available, complete the following steps:

    1. In SQL Server Configuration Manager, right-click the SQL Server instance, and click **Properties**. 
    2. In the **FILESTREAM** tab clear **Enable FILESTREAM for file I/O streaming access**, then click **Apply**.
    3. Check **Enable FILESTREAM for file I/O streaming access** again with the original share name and click **Apply**.

### Master Data Services (MDS)
- **Issue and customer impact:** 
On the user permissions page, when granting permission to the root level in the entity tree view, you see the following error:
`"The model permission cannot be saved. The object guid is not valid"`

- **Workaround:** 
  - Grant permission on the sub nodes in the tree view instead of the root level.

### Analysis Services
- **Issue and customer impact:** Data connectors for the following sources are not yet available for tabular models at the 1400 compatibility level.
  - Amazon Redshift
  - IBM Netezza
  - Impala
- **Workaround:** None.   

- **Issue and customer impact:** Direct Query models at the 1400 compatibility level with perspectives can fail on querying or discovering metadata.
- **Workaround:** Remove perspectives and redeploy.

### Tools
- **Issue and customer impact:** Running *DReplay* fails with the following message: "Error DReplay Unexpected error occurred!".
- **Workaround:** None.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 Release Candidate (RC2 - August 2017)
There are no release notes for SQL Server on Windows related to this release. See [SQL Server on Linux Release notes](../linux/sql-server-linux-release-notes-2017.md).


![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 Release Candidate (RC1 - July 2017)
### SQL Server Integration Services (SSIS) (RC1 - July 2017)
- **Issue and customer impact:** The parameter *runincluster* of the stored procedure **[catalog].[create_execution]** is renamed to *runinscaleout* for consistency and readability.
- **Work around:** If you have existing scripts to run packages in Scale Out, you have to change the parameter name from *runincluster* to *runinscaleout* to make the scripts work in RC1.

- **Issue and customer impact:** SQL Server Management Studio (SSMS) 17.1 and earlier versions can't trigger package execution in Scale Out in RC1. The error message is: "*\@runincluster* is not a parameter for procedure **create_execution**." This issue is fixed in the next release of SSMS, version 17.2. Versions 17.2 and later of SSMS support the new parameter name and package execution in Scale Out. 
- **Work around:** Until SSMS version 17.2 is available:
  1. Use your existing version of SSMS to generate the package execution script.
  2. Change the name of the *runincluster* parameter to *runinscaleout* in the script.
  3. Run the script.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 CTP 2.1 (May  2017)
### Documentation (CTP 2.1)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssql17-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql16-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssql17-md.md)] is noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssql17-md.md)].

### SQL Server Reporting Services (CTP 2.1)

- **Issue and customer impact:** If you have both SQL Server Reporting Services and Power BI Report Server on the same machine and uninstall one of them, you cannot connect to the remaining report server with Report Server Configuration Manager.
- **Work around** To work around this issue, you must perform the following operations after uninstalling one of the servers.

    1. Launch a command prompt in Administrator mode.
    2. Go to the directory where the remaining report server is installed.

        *Default location for Power BI Report Server: C:\Program Files\Microsoft Power BI Report Server*

        *Default location for SQL Server Reporting Services: C:\Program Files\Microsoft SQL Server Reporting Services*

    3. Then go to the next folder, which is either *SSRS* or *PBIRS* depending on what is remaining.
    4. Go to the WMI folder.
    5. Run the following command:

        ```console
        regsvr32 /i ReportingServicesWMIProvider.dll
        ```

        If you see the following error, ignore it.

        ```
        The module "ReportingServicesWMIProvider.dll" was loaded but the entry-point DLLInstall was not found. Make sure that "ReportingServicesWMIProvider.dll" is a valid DLL or OCX file and then try again.
        ```

### TSqlLanguageService.msi (CTP 2.1)

- **Issue and customer impact:** After installing on a computer that has a 2016 version of *TSqlLanguageService.msi* installed (either through SQL Setup or as a standalone redistributable) the v13.* (SQL 2016) versions of *Microsoft.SqlServer.Management.SqlParser.dll* and *Microsoft.SqlServer.Management.SystemMetadataProvider.dll* are removed. Any application that has a dependency on the 2016 versions of those assemblies stops working and generate an error similar to: *error : Could not load file or assembly 'Microsoft.SqlServer.Management.SqlParser, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91' or one of its dependencies. The system cannot find the file specified.*

   In addition, attempts to reinstall a 2016 version of TSqlLanguageService.msi fail with the message: *Installation of Microsoft SQL Server 2016 T-SQL Language Service failed because a higher version already exists on the machine*.

- **Workaround** To work around this issue and fix an application that depends on the v13 version of the assemblies follow these steps:

   1. Go to **Add/Remove Programs**
   2. Find *Microsoft SQL Server 2019 T-SQL Language Service CTP2.1*, right-click it, and select **Uninstall**.
   3. After the component is removed, repair the application that is broken or reinstall the appropriate version of *TSqlLanguageService.MSI*.

   This workaround removes the v14 version of those assemblies, so any applications that depend on the v14 versions will no longer function. If those assemblies are needed, then a separate installation without any side-by-side 2016 installs is required.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 CTP 2.0 (April  2017)
### Documentation (CTP 2.0)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssql17-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql16-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssql17-md.md)] is noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssql17-md.md)].

### Always On availability groups

- **Issue and customer impact:** A SQL Server instance hosting an availability group secondary replica crashes if the SQL Server major version is lower than the instance that hosts the primary replica. Affects upgrades from all supported versions of SQL Server that host availability groups to SQL Server [!INCLUDE[ssSQLv14_md](../includes/sssql17-md.md)] CTP 2.0. The issue occurs under the following conditions. 

> 1. User upgrades SQL Server instance hosting secondary replica in accordance with [best practices](../database-engine/availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md).
> 2. After upgrade, a failover occurs and a newly upgraded secondary becomes primary before completing upgrade for all secondary replicas in the availability group. The old primary is now a secondary, which is lower version than primary.
> 3. The availability group is in an unsupported configuration and any remaining secondary replicas might be vulnerable to crash. 

- **Workaround** Connect to the SQL Server instance that hosts the new primary replica, and remove the faulty secondary replica from the configuration.

   ```sql
   ALTER AVAILABILITY GROUP agName REMOVE REPLICA ON NODE instanceName;
   ```

   The instance of SQL Server that hosted the secondary replica recovers.

## More information
- [SQL Server Reporting Services release notes](../reporting-services/release-notes-reporting-services.md).
- [Known Issues for Machine Learning Services](../machine-learning/troubleshooting/known-issues-for-sql-server-machine-learning-services.md)
- [SQL Server Update Center - links and information for all supported versions](../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)