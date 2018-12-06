---
title: "SQL Server PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "08/04/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
ms.assetid: 89b70725-bbe7-4ffe-a27d-2a40005a97e7
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server PowerShell
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

**[Install SQL Server PowerShell](download-sql-server-ps-module.md)**

> [!NOTE]
> There are two SQL Server PowerShell modules; **SqlServer** and **SQLPS**. The **SQLPS** module is included with the SQL Server installation (for backwards compatibility), but is no longer being updated. The most up-to-date PowerShell module is the **SqlServer** module. The **SqlServer** module contains updated versions of the cmdlets in **SQLPS**, and also includes new cmdlets to support the latest SQL features.  
> Previous versions of the **SqlServer** module *were* included with SQL Server Management Studio (SSMS), but only with the 16.x versions of SSMS. To use PowerShell with SSMS 17.0 and later, the **SqlServer** module must be installed from the PowerShell Gallery.
> To install the **SqlServer** module, see [Install SQL Server PowerShell](download-sql-server-ps-module.md).

**Why did the module change from SQLPS to SqlServer?**

To ship SQL PowerShell updates, we had to change the identity of the SQL PowerShell module, as well as the wrapper known as *SQLPS.exe*. Because of this change, there are now two SQL PowerShell modules, the **SqlServer** module, and the **SQLPS** module.  

**Update your PowerShell scripts if they import the SQLPS module.**

If you have any PowerShell scripts that run `Import-Module -Name SQLPS`, and you want to take advantage of the new provider functionality and new cmdlets, you must change them to `Import-Module -Name SqlServer`. The new module is installed to `%ProgramFiles%\WindowsPowerShell\Modules\SqlServer` folder. Therefore, you do not have to update the $env:PSModulePath variable. If you have scripts that use a third-party or community version of a module named **SqlServer**, use the Prefix parameter to avoid name collisions.

There is no change to the module used by SQL Server Agent. Therefore, job steps of the type PowerShell use the SQLPS module. For more information, see [How to run PowerShell with SQL Server Agent](run-windows-powershell-steps-in-sql-server-agent.md).


## SQL Server PowerShell Components  
The **SqlServer** module loads two Windows PowerShell snap-ins:  
  
-   A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider, which enables a simple navigation mechanism similar to file system paths. You can build paths similar to file system paths, where the drive is associated with a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] management object model, and the nodes are based on the object model classes. You can then use familiar commands such as **cd** and **dir** to navigate the paths similar to the way you navigate folders in a command prompt window. You can use other commands, such as **ren** or **del**, to perform actions on the nodes in the path.  
  
-   A set of cmdlets that support actions such as running a **sqlcmd** script containing [!INCLUDE[tsql](../includes/tsql-md.md)] or XQuery statements.  
  
  
## SQL Server Versions  
SQL PowerShell cmdlets can be used to manage instances of Azure SQL Database, Azure SQL Data Warehouse, and all [supported SQL Server products](https://support.microsoft.com/lifecycle/search/1044).  


## SQL Server identifiers that contain characters not supported in PowerShell paths  
 
The **Encode-Sqlname** and **Decode-Sqlname** cmdlets help you specify SQL Server identifiers that contain characters not supported in PowerShell paths. For more information, see [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md).  
  
Use the **Convert-UrnToPath** cmdlet to convert a Unique Resource Name for a [!INCLUDE[ssDE](../includes/ssde-md.md)] object to a path for the SQL Server PowerShell provider. For more information, see [Convert URNs to SQL Server Provider Paths](https://docs.microsoft.com/powershell/module/sqlserver/Convert-UrnToPath).  
  
## Query Expressions and Unique Resource Names  

Query expressions are strings that use syntax similar to XPath to specify a set of criteria that enumerate one or more objects in an object model hierarchy. A Unique Resource Name (URN) is a specific type of query expression string that uniquely identifies a single object. For more information, see [Query Expressions and Uniform Resource Names](query-expressions-and-uniform-resource-names.md).       


## Cmdlet reference
* [SqlServer cmdlets](https://docs.microsoft.com/powershell/module/sqlserver)
* [SQLPS cmdlets](https://docs.microsoft.com/powershell/module/sqlps)
