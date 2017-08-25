---
title: "SQL Tools and Utilities for SQL Server, Azure SQL Database, and SQL Data Warehouse | Microsoft Docs"
ms.custom: ""
ms.date: "08/25/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 
caps.latest.revision: 0
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Tools and utilities for Azure SQL Database, SQL Server, and SQL Data Warehouse

[!INCLUDE[tsql-appliesto-ss2008-all_md](../includes/tsql-appliesto-ss2008-all-md.md)]  

  ![](../includes/media/sql-database-tools.png) This article provides a list of available tools for working with SQL Server, Azure SQL Database, SQL Data Warehouse, and SQL Server-based applications. The first sections provide quick access to current tools for common tasks. Further down the page is a larger index of tools and command prompt utilities available for working with SQL.

If you want to jump right in and start creating tables, running queries, basically design and manage your database, then [**SQL Server Management Studio (SSMS)**](../ssms/download-sql-server-management-studio-ssms) is most likely your go-to tool. SSMS is free, and runs on Windows.

If you're not running Windows, [Visual Studio Code](https://code.visualstudio.com/) is free and works on Linux, macOS, (and Windows). Install the [**mssql for Visual Studio Code**](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) extension for developing Microsoft SQL Server, Azure SQL Database and SQL Data Warehouse everywhere with a rich set of functionalities. See [Use Visual Studio Code to create and run Transact-SQL scripts for SQL Server](../linux/sql-server-linux-develop-use-vscode.md).


## SQL tools 
 
| Tool | Description |
|:--|:--|
| [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md) | Use SQL Server Management Studio (SSMS) to query, design, and manage your SQL Server, Azure SQL Database, and Azure SQL Data Warehouse. |
| [SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md) | Turn Visual Studio into a powerful development environment for SQL Server, Azure SQL Database, and Azure SQL Data Warehouse. |
| [Visual Studio Code](https://code.visualstudio.com/)| Visual Studio Code works on Linux, macOS, and Windows. After installing Visual Studio Code, install the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) for developing Microsoft SQL Server, Azure SQL Database and SQL Data Warehouse. |
| [Configuration Manager](../tools/configuration-manager/sql-server-configuration-manager-help.md) | Use SQL Server Configuration Manager to configure SQL Server services and configure network connectivity.|
| [SQL Server Migration Assistant](../ssma/sql-server-migration-assistant.md) | Use SQL Server Migration Assistant to automate database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and Sybase.|
| [Distributed Replay](../tools/distributed-replay/install-distributed-replay-overview.md) | Use the Distributed Replay feature to help you assess the impact of future SQL Server upgrades. Also use Distributed Replay to help assess the impact of hardware and operating system upgrades, and SQL Server tuning. |
| [ssbdiagnose](../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md) | The ssbdiagnose utility reports issues in Service Broker conversations or the configuration of Service Broker services. |


## SQL Command Prompt utilities

  Command prompt utilities enable you to script [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] operations. The following table contains a list of command prompt utilities that ship with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
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


