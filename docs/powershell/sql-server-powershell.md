---
title: "SQL Server PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "08/04/2016"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-tools"
ms.service: ""
ms.component: "ssms-scripting"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 89b70725-bbe7-4ffe-a27d-2a40005a97e7
caps.latest.revision: 10
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
ms.workload: "Active"
---
# SQL Server PowerShell
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] supports Windows PowerShell.

> [!NOTE]
> There are two SQL Server PowerShell modules; **SqlServer** and **SQLPS**. The **SQLPS** module is included with the SQL Server installation (for backwards compatability), but is no longer being updated. The most up-to-date PowerShell module is the **SqlServer** module. The **SqlServer** module contains updated versions of the cmdlets in **SQLPS**, and also includes new cmdlets to support the latest SQL features.  
> To install the **SqlServer** module, see [Install SQL Server PowerShell](download-sql-server-ps-module.md).

**Why did the module change from SQLPS to SqlServer?**
To ship SQL PowerShell updates, we had to change the identity of the SQL PowerShell module, as well as the wrapper known as `SQLPS.exe`. Because of this change, there are now two SQL PowerShell modules, the **SQLPS** module, and the **SqlServer** module.  

**Update your PowerShell scripts if they import the SQLPS module.**
If you have any PowerShell scripts that run `Import-Module -Name SQLPS`, and you want to take advantage of the new provider functionality and new cmdlets, you must change them to `Import-Module -Name SqlServer`. The new module is installed to `%Program Files\WindowsPowerShell\Modules\SqlServer`. Therefore, you do not have to update the $env:PSModulePath variable. If you have scripts that use a third-party or community version of a module named **SqlServer**, use of the Prefix parameter to avoid name collisions. There is no change to the module used by SQL Server Agent. 

  
## SQL Server PowerShell Components  
The **SqlServer** module loads two Windows PowerShell snap-ins:  
  
-   A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider, which enables a simple navigation mechanism similar to file system paths. You can build paths similar to file system paths, where the drive is associated with a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] management object model, and the nodes are based on the object model classes. You can then use familiar commands such as **cd** and **dir** to navigate the paths similar to the way you navigate folders in a command prompt window. You can use other commands, such as **ren** or **del**, to perform actions on the nodes in the path.  
  
-   A set of cmdlets that support actions such as running a **sqlcmd** script containing [!INCLUDE[tsql](../includes/tsql-md.md)] or XQuery statements.  
  
  
## SQL Server Versions  
 The [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] PowerShell components can be used to manage instances of [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)] or later. Instances of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] must be running SP2 or later. Instances of [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)] must be running SP4 or later. When the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] PowerShell components are used with earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], they are limited to the functionality available in those versions.  

## SQL Server identifiers that contain characters not supported in PowerShell paths  
 
The **Encode-Sqlname** and **Decode-Sqlname** cmdlets help you specify SQL Server identifiers that contain characters not supported in PowerShell paths. For more information, see [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md).  
  
Use the **Convert-UrnToPath** cmdlet to convert a Unique Resource Name for a [!INCLUDE[ssDE](../includes/ssde-md.md)] object to a path for the SQL Server PowerShell provider. For more information, see [Convert URNs to SQL Server Provider Paths](https://docs.microsoft.com/powershell/module/sqlserver/Convert-UrnToPath).  
  
## Query Expressions and Unique Resource Names  

Query expressions are strings that use syntax similar to XPath to specify a set of criteria that enumerate one or more objects in an object model hierarchy. A Unique Resource Name (URN) is a specific type of query expression string that uniquely identifies a single object. For more information, see [Query Expressions and Uniform Resource Names](query-expressions-and-uniform-resource-names.md).       



## SQL Server PowerShell Tasks  
  
|Task Description|Topic|  
|----------------------|-----------| 
|Installing Microsoft® Windows PowerShell Extensions for Microsoft [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  The PowerShell modules are installed by default when installing [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  You can manually install the PowerShell Extensions for SQL Server 2016 by installing the following components from the Microsoft® SQL Server® 2016 Feature Pack:<br/>     Microsoft® System CLR Types for Microsoft SQL Server® 2016 (SQLSysClrTypes.msi)<br/>Microsoft® SQL Server® 2016 Shared Management Objects (SharedManagementObjects.msi)<br/> Microsoft® Windows PowerShell Extensions for Microsoft SQL Server® 2016 (PowerShellTools.msi)|[Microsoft® SQL Server® 2016 Feature Pack](https://www.microsoft.com/en-us/download/details.aspx?id=52676).   | 
|Describes the preferred mechanism for running the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] PowerShell components; to open a PowerShell session and load the **sqlps** module. The **sqlps** module loads in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] PowerShell provider and cmdlets, and the SQL Server Management Object (SMO) assemblies used by the provider and cmdlets.|[Import the SQLPS Module](import-the-sqlps-module.md)|  
|Describes how to load only the SMO assemblies without the provider or cmdlets.|[Load the SMO Assemblies in Windows PowerShell](load-the-smo-assemblies-in-windows-powershell.md)|  
|Describes how to run a Windows PowerShell session by right-clicking a node in **Object Explorer**. [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] launches a Windows PowerShell session, loads the **sqlps** module, and sets the SQL Server provider path to the object selected.|[Run Windows PowerShell from SQL Server Management Studio](run-windows-powershell-from-sql-server-management-studio.md)|  
|Describes how to create SQL Server Agent job steps that run a Windows PowerShell script. The jobs can then be scheduled to run at specific times or in response to events.|[Run Windows PowerShell Steps in SQL Server Agent](run-windows-powershell-steps-in-sql-server-agent.md)|  
|Describes how to use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider to navigate a hierarchy of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects.|[SQL Server PowerShell Provider](sql-server-powershell-provider.md)|  
|Describes how to specify [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] delimited identifiers that contain characters not supported by Windows PowerShell.|[SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md)|  
|Describes how to make SQL Server Authentication connections. By default, the SQL Server PowerShell components use Windows Authentication connections using the Windows credentials of the process running Windows PowerShell.|[Manage Authentication in Database Engine PowerShell](manage-authentication-in-database-engine-powershell.md)|  
|Describes how to use variables implemented by the SQL Server PowerShell provider to control how many objects are listed when using Windows PowerShell tab completion. This is particularly useful when working on databases that contain large numbers of objects.|[Manage Tab Completion &#40;SQL Server PowerShell&#41;](manage-tab-completion-sql-server-powershell.md)|
  
  
