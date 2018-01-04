---
title: "SQL Tools and Utilities for SQL Server, Azure SQL Database, and Azure SQL Data Warehouse | Microsoft Docs"
ms.custom: ""
ms.date: "11/15/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-tools"
ms.service: ""
ms.component: "misc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 
caps.latest.revision: 0
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
ms.workload: "On Demand"
---
# SQL Tools and Utilities for SQL Server, Azure SQL Database, and Azure SQL Data Warehouse
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]


## Tools to run queries and manage databases  

| Tool | Description |
|:--|:--|
| [[!INCLUDE[name-sos](../includes/name-sos.md)]](../sql-operations-studio/download.md) | [!INCLUDE[name-sos](../includes/name-sos-short.md)] is a free, light-weight tool, for managing databases wherever they're running. This preview release provides database management features, including an extended Transact-SQL editor and customizable insights into the operational state of your databases. **[!INCLUDE[name-sos](../includes/name-sos-short.md)] runs on Windows, macOS, and Linux**.|
| [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md) | Use SQL Server Management Studio (SSMS) to query, design, and manage your SQL Server, Azure SQL Database, and Azure SQL Data Warehouse. **SSMS runs on Windows**.|
| [SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md) | Turn Visual Studio into a powerful development environment for SQL Server, Azure SQL Database, and Azure SQL Data Warehouse. **SSDT runs on Windows**.|
| [Visual Studio Code](https://code.visualstudio.com/)| After installing Visual Studio Code, install the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) for developing Microsoft SQL Server, Azure SQL Database, and SQL Data Warehouse. **Visual Studio Code runs on Windows, macOS, and Linux**.|

## Which tool should I choose?

- Do you want to manage a SQL Server instance or database, in a light-weight editor on Windows, Linux or Mac? Choose [[!INCLUDE[name-sos](../includes/name-sos.md)]](../sql-operations-studio/download.md) | [!INCLUDE[name-sos](../includes/name-sos-short.md)] 
- Do you want to manage a SQL Server instance or database on Windows with full GUI support? Choose [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)
- Do you want to create or maintain database code, including compile time validation, refactoring and designer support on Windows? Choose [SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)
- Do you want to write T-SQL scripts in a light-weight editor on Windows, Linux or Mac? Choose [Visual Studio Code](https://code.visualstudio.com/) and the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)

## Additional tools

| Tool | Description |
|:--|:--|
| [Configuration Manager](../tools/configuration-manager/sql-server-configuration-manager-help.md) | Use SQL Server Configuration Manager to configure SQL Server services and configure network connectivity.|
| [SQL Server Migration Assistant](../ssma/sql-server-migration-assistant.md) | Use SQL Server Migration Assistant to automate database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and Sybase.|
| [Distributed Replay](../tools/distributed-replay/install-distributed-replay-overview.md) | Use the Distributed Replay feature to help you assess the impact of future SQL Server upgrades. Also use Distributed Replay to help assess the impact of hardware and operating system upgrades, and SQL Server tuning. |
| [ssbdiagnose](../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md) | The ssbdiagnose utility reports issues in Service Broker conversations or the configuration of Service Broker services. |


## Command line utilities

  Command line utilities enable you to script [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] operations. The following table contains a list of command prompt utilities that ship with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
|**Utility**|**Description**|**Installed in**|  
|-----------------|---------------------|----------------------|  
|[bcp Utility](../tools/bcp-utility.md)|Used to copy data between an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and a data file in a user-specified format.|\<*drive*:>\Program Files\\[!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]\Client SDK\ODBC\110\Tools\Binn|  
|[dta Utility](../tools/dta/dta-utility.md)|Used to analyze a workload and recommend physical design structures to optimize server performance for that workload.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[dtexec Utility](../integration-services/packages/dtexec-utility.md)|Used to configure and execute an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package. A user interface version of this command prompt utility is called **DTExecUI**, which brings up the Execute Package Utility.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]DTS\Binn|  
|[dtutil Utility](../integration-services/dtutil-utility.md)|Used to manage SSIS packages.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]DTS\Binn|  
|[Deploy Model Solutions with the Deployment Utility](../analysis-services/multidimensional-models/deploy-model-solutions-with-the-deployment-utility.md)|Used to deploy [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] projects to instances of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn\VShell\Common7\IDE|  
|[mssql-scripter (Public Preview)](https://blogs.technet.microsoft.com/dataplatforminsider/2017/05/17/try-new-sql-server-command-line-tools-to-generate-t-sql-scripts-and-monitor-dynamic-management-views/)|Used to generate CREATE and INSERT T-SQL scripts for database objects in SQL Server, Azure SQL Database, and Azure SQL Data Warehouse.|See our [GitHub repo](https://github.com/Microsoft/sql-xplat-cli) for download and usage information.| 
|[osql Utility](../tools/osql-utility.md)|Allows you to enter [!INCLUDE[tsql](../includes/tsql-md.md)] statements, system procedures, and script files at the command prompt.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[Profiler Utility](../tools/profiler-utility.md)|Used to start [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] from a command prompt.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[RS.exe Utility &#40;SSRS&#41;](../reporting-services/tools/rs-exe-utility-ssrs.md)|Used to run scripts designed for managing [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report servers.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[rsconfig Utility &#40;SSRS&#41;](../reporting-services/tools/rsconfig-utility-ssrs.md)|Used to configure a report server connection.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[rskeymgmt Utility &#40;SSRS&#41;](../reporting-services/tools/rskeymgmt-utility-ssrs.md)|Used to manage encryption keys on a report server.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[sqlagent90 Application](../tools/sqlagent90-application.md)|Used to start [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent from a command prompt.|\<drive>:\Program Files\Microsoft SQL Server\\<*instance_name*>\MSSQL\Binn|  
|[sqlcmd Utility](../tools/sqlcmd-utility.md)|Allows you to enter [!INCLUDE[tsql](../includes/tsql-md.md)] statements, system procedures, and script files at the command prompt.|\<*drive*:>\Program Files\\[!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]\Client SDK\ODBC\110\Tools\Binn|  
|[SQLdiag Utility](../tools/sqldiag-utility.md)|Used to collect diagnostic information for [!INCLUDE[msCoName](../includes/msconame-md.md)] Customer Service and Support.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[sqllogship Application](../tools/sqllogship-application.md)|Used by applications to perform backup, copy, and restore operations and associated clean-up tasks for a log shipping configuration without running the backup, copy, and restore jobs.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[SqlLocalDB Utility](../tools/sqllocaldb-utility.md)|An execution mode of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] targeted to program developers.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn\|  
|[sqlmaint Utility](../tools/sqlmaint-utility.md)|Used to execute database maintenance plans created in previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].|\<drive>:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn|  
|[sqlps Utility](../tools/sqlps-utility.md)|Used to run PowerShell commands and scripts. Loads and registers the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] PowerShell provider and cmdlets.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn|  
|[sqlservr Application](../tools/sqlservr-application.md)|Used to start and stop an instance of [!INCLUDE[ssDE](../includes/ssde-md.md)] from the command prompt for troubleshooting.|\<drive>:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn|  
|[Ssms Utility](../tools/sql-server-management-studio/ssms-utility.md)|Used to start [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from a command prompt.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]Tools\Binn\VSShell\Common7\IDE|  
|[tablediff Utility](../tools/tablediff-utility.md)|Used to compare the data in two tables for non-convergence, which is useful when troubleshooting a replication topology.|[!INCLUDE[ssInstallPathVar](../includes/ssinstallpathvar-md.md)]COM|  

## SQL Command Prompt utilities syntax conventions  
  
|**Convention**|**Used for**|  
|--------------------|------------------|  
|UPPERCASE|Statements and terms used at the operating system level.|  
|`monospace`|Sample commands and program code.|  
|*italic*|User-supplied parameters.|  
|**bold**|Commands, parameters, and other syntax that must be typed exactly as shown.|  


