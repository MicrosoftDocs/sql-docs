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
This topic describes limitations and issues with [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)]. For related informaiton, see the following:

- [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md).
- [SQL Server on Linux Release notes](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes).
- [SQL Server Reporting Services release notes](../reporting-services/reporting-services-release-notes.md).

 **Try it out:**    
   -   [![Download from Evaluation Center](../analysis-services/media/download.png)](http://go.microsoft.com/fwlink/?LinkID=829477)  Download [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] from the **[Evaluation Center](http://go.microsoft.com/fwlink/?LinkID=829477)**

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


![horizontal_bar](../sql-server/media/horizontal-bar.png)

## SQL Server 2017 CTP 1.4 (March  2017)

### Documentation (CTP 1.4)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

![horizontal_bar](../sql-server/media/horizontal-bar.png)

## SQL Server 2017 CTP 1.3 (February  2017)
### Supported installation scenarios (CTP 1.3)
[!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is intended as a test version only.  Production deployments are not supported. It is recommneded you install and test [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] on a virtual machine.

### Documentation (CTP 1.3)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### SQL Server Integration Services (SSIS) (CTP 1.3)
#### CDC components not supported in this CTP release
-   **Issue and customer impact**: The CDC Control Task, CDC Source, and CDC Splitter in are not supported in this CTP release.
-   **Workaround**: There is no workaround.


![horizontal_bar](../sql-server/media/horizontal-bar.png)

## SQL Server 2017 CTP 1.2 (January  2017)
### Supported installation scenarios (CTP 1.2)
[!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is intended as a test version only.  Production deployments are not supported. It is recommneded you install and test [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] on a virtual machine.

### SQL Server Database Engine (CTP 1.2)
- **Issue and customer impact:** In some cases, the MSSQLSERVER service will get stuck in the "Starting" state.
- **Workaround:** To work around this issue:
  -  Create a dependency between the `mssqlserver` service and the `keyiso` service. One way to do this is to run the following from an elevated Command Prompt: `sc config mssqlserver depend= keyiso`
  - Reboot the computer.

### Documentation (CTP 1.2)
- **Issue and customer impact:** Documentaion for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is inlcuded with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To:**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].
 
### SQL Server Integration Services (SSIS) (CTP 1.2)
#### Deleting the SSIS Catalog may fail when SSIS Scale Out is installed
**Issue and customer impact**: When the SSIS Scale Out feature is installed on a computer, deleting the SSISDB catalog database may fail with following error: “Could not drop login *'login'* as the user is currently logged in”.
   
**Workaround**:
-   On a Scale Out Master computer, run the command “services.msc” to open the Services window. Stop the SQL Server Integration Services Cluster Master service.
-   On Scale Out Worker computers that connect to the master, run the command "services.msc" to open the Services window. Stop the SQL Server Integration Services Cluster Worker service.

You can now delete the SSISDB catalog database.

### SQL Server Master Data Services (CTP 1.2)
#### Transaction may not work when the entity transaction log type is set to attribute
**Issue and customer impact:** When the entity transaction log type is set to **Attribute** in [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] (default value is **Member**), the following scenarios fail:

* Transactions for entity changes are not shown in the website.
* Unable to open the **Transactions** page on the website and reverse a transaction.
* Unable to update an entity with a transaction annotation, in the website.

**Workaround**: There is no workaround.

#### Copy version may not work when **Copy only committed version** is set to false
-  **Issue and customer impact:** When the **Copy only committed version** setting is set to **No** (default value is **Yes**), the copy version operation may fail. There is no error message.
-  **Workaround**: There is no workaround.

##  ![info_tip](../sql-server/media/info-tip.png) Engage with the SQL Server engineering team 
- [Stack Overflow (tag sql-server) - ask technical questions](http://stackoverflow.com/questions/tagged/sql-server)
- [MSDN Forums - ask technical questions](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
- [Microsoft Connect - report bugs and request features](https://connect.microsoft.com/SQLServer/Feedback)
- [Reddit - general discussion about R](https://www.reddit.com/r/SQLServer/)


![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)

