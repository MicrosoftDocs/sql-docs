---
title: "SQL Server vNext Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "03/12/2017"
ms.prod: "sql-vnext"
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
# SQL Server vNext Release Notes
This topic describes limitations and issues with [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

- To read what's new in this release, see [What's New in SQL Server vNext](../sql-server/what-s-new-in-sql-server-vnext.md).
- [SQL Server on Linux Release notes](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes) 
are published on our new and growing documentation platform.
- [SQL Server Reporting Services release notes](../reporting-services/reporting-services-release-notes.md) are published within the Reporting Services section.

 **Try it out:**    
   -   [![Download from Evaluation Center](../analysis-services/media/download.png)](http://go.microsoft.com/fwlink/?LinkID=829477)  Download [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] from the **[Evaluation Center](http://go.microsoft.com/fwlink/?LinkID=829477)**


## SQL Server vNext CTP 1.4 (March  2017)
### Supported installation scenarios (CTP 1.4)

### Documentation (CTP 1.4)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

![horizontal_bar](../sql-server/media/horizontal-bar.png)

## SQL Server vNext CTP 1.3 (February  2017)
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

## SQL Server vNext CTP 1.2 (January  2017)
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

![horizontal_bar](../sql-server/media/horizontal-bar.png)

## SQL Server vNext CTP 1.1 (December  2016)
### Supported installation scenarios (CTP 1.1)
[!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is intended as a test version only.  Production deployments are not supported. It is recommneded you install and test [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] on a virtual machine.

Specifically, while the following scenarios may work for you, they have not been thoroughly tested and are **not** supported in [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)]:
- Uninstall of CTP 1.1.
- Side by side installation with any other versions of SQL Server.
- Upgrade from any previous versions of SQL Server.
- No SQL Server feature pack components are available as part of the [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] installation.

### Documentation (CTP 1.1)
- **Issue and customer impact:** Documentation for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is included with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in articles that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To:**. 
- **Issue and customer impact:** No offline content is available for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### SQL Server Master Data Services (CTP 1.1)
#### Transaction may not work when the entity transaction log type is set to attribute
**Issue and customer impact:** When the entity transaction log type is set to **Attribute** in [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] (default value is **Member**), the following scenarios fail:

* Transactions for entity changes are not shown in the website.
* Unable to open the **Transactions** page on the website and reverse a transaction.
* Unable to update an entity with a transaction annotation, in the website.

**Workaround**: There is no workaround.

#### Copy version may not work when **Copy only committed version** is set to false
-  **Issue and customer impact:** When the **Copy only committed version** setting is set to **No** (default value is **Yes**), the copy version operation may fail. There is no error message.
-  **Workaround**: There is no workaround.

### SQL Server Integration Services (SSIS) (CTP 1.1)
#### Deleting the SSIS Catalog may fail when SSIS Scale Out is installed
**Issue and customer impact**: When the SSIS Scale Out feature is installed on a computer, deleting the SSISDB catalog database may fail with following error: “Could not drop login *'login'* as the user is currently logged in”.
   
**Workaround**:
-   On a Scale Out Master computer, run the command “services.msc” to open the Services window. Stop the SQL Server Integration Services Cluster Master service.
-   On Scale Out Worker computers that connect to the master, run the command "services.msc" to open the Services window. Stop the SQL Server Integration Services Cluster Worker service.

You can now delete the SSISDB catalog database.

#### ODBC components are not supported in this CTP release
**Issue and customer impact**: The ODBC Connection Manager, Source, and Destination are not supported in this CTP release.

**Workaround**: There is no workaround.

![horizontal_bar](../sql-server/media/horizontal-bar.png)

## SQL Server vNext CTP 1.0 (November 2016)
### Supported installation scenarios (CTP1.0)
[!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is intended as a test version only.  Production deployments are not supported. It is recommneded you install and test [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] on a virtual machine.

Specifically, while the following scenarios may work for you, they have not been thoroughly tested and are **not** supported in [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)]:
- Uninstall of CTP 1.0.
- Side by side installation with any other versions of SQL Server.
- Upgrade from any previous versions of SQL Server.
- No SQL Server feature pack components are available as part of the [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] installation.

### Documentation (CTP 1.0)
- **Issue and customer impact:** Documentaion for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] is limited and content is inlcuded with the [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] documentation set.  Content in artilces that is specific to [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] will be noted with **Applies To:**. 
- **Issue and customer impact:** No offline content is avaiable for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### SQL Server Database Engine (CTP 1.0)
-  **Issue and customer impact:** The `STRING_AGG` function in CTP1 returns LOB type as a result (e.g. `NVARCHAR(MAX)`). In a future CTP release, this behavior might change and `STRING_AGG` might return `NVARCHAR(4000)` if input values are not LOB types. An overflow error might be thrown if the concatenated results cannot fit into `NVARCHAR(4000)`.

-  **Issue and customer impact:** Avoid usages of non-reserved keywords such as `WITHIN` as an alias for `STRING_AGG` expressions. (For example `SELECT Company.Name, STRING_AGG(Product.Name, ',') AS WITHIN FROM TableA;`.) In a future CTP release, `WITHIN` token after `STRING_AGG` call might be used as a part of `STRING_AGG` function.

-  **Workaround:** Avoid using `WITHIN` alias or add `AS WITHIN` as an alias specification in order to avoid collisions with future changes that might be added in `STRING_AGG` function.


### SQL Server Integration Services (CTP 1.0)
#### Stop Operation in SSIS Catalog may fail
**Issue and customer impact:** Stopping an operation in [!INCLUDE[ssIS_md](../includes/ssis-md.md)] using [!INCLUDE[ssManStudio_md](../includes/ssmanstudio-md.md)] or the catalog.stop_operation stored procedure, may fail with following error: “A .NET Framework error occurred during execution of user-defined routine or aggregate 'stop_operation_internal'”.

-  **Workaround:** Open the Windows Task Manager. On the **Processes** Tab, find the process named **ISServerExec** and click **End Task** to end the process.

#### Create Dump of SSIS Pacakge Execution may fail
**Issue and customer impact:** Creating a dump for the execution of a package by using the catalog.create_execution_dump stored procedure, may fail with following error: “A .NET Framework error occurred during execution of user-defined routine or aggregate 'create_execution_dump_internal'”.

-  **Workaround:** Open the Windows Task Manager. On the **Processes** Tab, find the process named **ISServerExec**, right click the process, and then click **Create dump file**.

#### Get Perf Counter of SSIS Package Execution may fail
**Issue and customer impact:** Querying the [!INCLUDE[ssIS_md](../includes/ssis-md.md)] perf counters by using the table-valued function, catalog.dm_execution_performance_counters, may fail with following error: “A .NET Framework error occurred during execution of user-defined routine or aggregate 'get_execution_perf_counters'”.

-  **Workaround**: Use the Windows Performance Monitor to monitor the perf counters values directly. There is no workaround for perf counters that are for a specific package execution.

#### Deleting Catalog may fail when SSIS Scale Out Master is installed
**Issue and customer impact:** When the [!INCLUDE[ssIS_md](../includes/ssis-md.md)] Scale Out feature is installed on a machine, deleting the **SSISDB** catalog may fail with following error: “Could not drop login ‘…’ as the user is currently logged in”. 

**Workaround**: 
* On a Scale Out Master machine, run the command “services.msc” to open the **Services** window and stop the **SQL Server Integration Services Cluster Master** service. 

* On Scale Out Worker machines, which connect to the master, run the command "services.msc" to open the **Services** window. Stop the **SQL Server Integration Services Cluster Worker** service. 

You should now be able to delete the **SSISDB** catalog.

#### ODBC connector in SSIS is not supported
-  **Issue and customer impact:** ODCB connector in [!INCLUDE[ssIS_md](../includes/ssis-md.md)] is not supported.
-  **Workaround:** There is no workaround.

### SQL Server Reporting Services (CTP 1.0)

SQL Server Reporting Services isn't releasing any new features for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].

### SQL Server Master Data Services (CTP 1.0)
#### Transaction may not work when the entity transaction log type is set to attribute
**Issue and customer impact:** When the entity transaction log type is set to **Attribute** in [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] (default value is **Member**), the following scenarios fail:

* Transactions for entity changes are not shown in the website.
* Unable to open the **Transactions** page on the website and reverse a transaction.
* Unable to update an entity with a transaction annotation, in the website.

**Workaround**: There is no workaround.

#### Copy version may not work when **Copy only committed version** is set to false
**Issue and customer impact:** When the **Copy only committed version** setting is set to **No** (default value is **Yes**), the copy version operation may fail. There is no error message.

**Workaround**: There is no workaround.
 
### SQL Server Management Studio (SSMS) (CTP 1.0)
SQL Server Management Studio is a seperate downlaod.  For the latest information, see [SQL Server Management Studio - Release Notes](https://msdn.microsoft.com/library/mt238486.aspx).

##  ![info_tip](../sql-server/media/info-tip.png) Engage with the SQL Server engineering team 
- [Stack Overflow (tag sql-server) - ask technical questions](http://stackoverflow.com/questions/tagged/sql-server)
- [MSDN Forums - ask technical questions](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
- [Microsoft Connect - report bugs and request features](https://connect.microsoft.com/SQLServer/Feedback)
- [Reddit - general discussion about R](https://www.reddit.com/r/SQLServer/)


![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)

