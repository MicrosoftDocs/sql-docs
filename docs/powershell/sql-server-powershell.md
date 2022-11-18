---
title: SQL Server PowerShell
description: Learn about the two SQL Server PowerShell modules, SqlServer and SQLPS, which include PowerShell Providers and cmdlets.
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
ms.assetid: 89b70725-bbe7-4ffe-a27d-2a40005a97e7
author: markingmyname
ms.author: maghan
ms.reviewer: matteot
ms.custom: ""
ms.date: 06/11/2020
---

# SQL Server PowerShell

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

**[Install SQL Server PowerShell](download-sql-server-ps-module.md)**

There are two SQL Server PowerShell modules; **[SqlServer](/powershell/module/sqlserver)** and **[SQLPS](/powershell/module/sqlps)**.

The **SqlServer** module is the current PowerShell module to use.

The **SQLPS** module is included with the SQL Server installation (for backward compatibility) but is no longer updated.

The **SqlServer** module contains updated versions of the cmdlets in **SQLPS** and includes new cmdlets to support the latest SQL features.

Previous versions of the **SqlServer** module *were* included with [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md), but only with the 16.x versions of SSMS.

To use PowerShell with SSMS 17.0 and later, install the **SqlServer** module from the [PowerShell Gallery](https://www.powershellgallery.com/packages/SqlServer).

You can also use [PowerShell with Azure Data Studio](../azure-data-studio/extensions/powershell-extension.md).

**Why did the module change from SQLPS to SqlServer?**

To ship SQL PowerShell updates, we had to change the identity of the SQL PowerShell module, and the wrapper known as *SQLPS.exe*. Because of this change, there are now two SQL PowerShell modules, the **SqlServer** module, and the **SQLPS** module.  

**Update your PowerShell scripts if you import the SQLPS module.**

If you have any PowerShell scripts that run `Import-Module -Name SQLPS`, and you want to take advantage of the new provider functionality and new cmdlets, you must change them to `Import-Module -Name SqlServer`. The new module is installed to `%ProgramFiles%\WindowsPowerShell\Modules\SqlServer` folder. As such, you don't have to update the $env:PSModulePath variable. If you have scripts that use a third-party or community version of a module named **SqlServer**, use the Prefix parameter to avoid name collisions.

It is recommended to start your script with *Import-Module SQLServer* to avoid side-by-side issues if the SQLPS module is installed on the same machine.

This section applies to scripts executed from PowerShell and not the SQL Agent. The new module can be used with SQL Agent job steps using [#NOSQLPS](#sql-server-agent).

## SQL Server PowerShell Components

The **SqlServer** module comes with:

- [PowerShell Providers](/powershell/module/microsoft.powershell.core/about/about_providers), which enables a simple navigation mechanism similar to file system paths. You can build paths similar to file system paths, where the drive is associated with a SQL Server management object model, and the nodes are based on the object model classes. You can then use familiar commands such as **cd** (alias for Set-Location) and **dir** (alias for Get-ChildItem) to navigate the paths similar to the way you navigate folders in a command prompt window. You can use other commands, such as **ren** (alias for Rename-Item) or **del** (alias for Remove-Item), to perform actions on the nodes in the path.

- A set of cmdlets that support actions such as running a **sqlcmd** script containing Transact-SQL or XQuery statements.  

- The AS provider and cmdlets, which before they were installed separately.

## SQL Server versions

SQL PowerShell cmdlets can be used to manage instances of Azure SQL Database, Azure Synapse Analytics, and all [supported SQL Server products](https://support.microsoft.com/lifecycle/search/1044).

## SQL Server identifiers that contain characters not supported in PowerShell paths

The **Encode-Sqlname** (alias for ConvertTo-EncodedSqlName) and **Decode-Sqlname** (alias for ConvertFrom-EncodedSqlName) cmdlets help you specify SQL Server identifiers that contain characters not supported in PowerShell paths. For more information, see [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md).

Use the **Convert-UrnToPath** cmdlet to convert a Unique Resource Name for a Database Engine object to a path for the SQL Server PowerShell provider. For more information, see [Convert URNs to SQL Server Provider Paths](/powershell/module/sqlserver/Convert-UrnToPath).
  
## Query Expressions and Uniform Resource Names  

Query expressions are strings that use syntax similar to XPath to specify a set of criteria that enumerates one or more objects in an object model hierarchy. A Uniform Resource Name (URN) is a specific type of query expression string that uniquely identifies a single object. For more information, see [Query Expressions and Uniform Resource Names](query-expressions-and-uniform-resource-names.md).

## SQL Server Agent

There's no change to the module used by SQL Server Agent. As such, SQL Server Agent jobs, which have PowerShell type job steps use the SQLPS module. For more information, see [How to run PowerShell with SQL Server Agent](run-windows-powershell-steps-in-sql-server-agent.md). However, starting with SQL Server 2019, you can disable SQLPS. To do so, on the first line of a job step of the type PowerShell you can add `#NOSQLPS`, which stops the SQL Agent from auto-loading the SQLPS module. When you do this, your SQL Agent Job runs the version of PowerShell installed on the machine, and then you can use any other PowerShell module you like.

### Troubleshooting SQLPS
If you see agent job steps (PowerShell subsystem) failing with the following error, this section may be helpful in troubleshooting the issue. 

> A job step received an error at line 1 in a PowerShell script. The corresponding line is 'import-module SQLPS'. Correct the script and reschedule the job. The error information returned by PowerShell is: 'The specified module 'SQLPS' was not loaded because no valid module file was found in any module directory.  

The SQLPS module must be available at the environment variable PSModulePath.  Uninstalling SSMS 16.x may remove the SQLPS from PSModulePath.  To check the current values stored in PSModulePath, run the following PowerShell:

```powershell
 $env:PSModulePath -split ";"
```

If the path is set, you will see an entry similar to `C:\Program Files (x86)\Microsoft SQL Server\130\Tools\PowerShell\Modules`.  If the path is not set, locate the SQLPS folder on your server and add it to the environment variable value either through PowerShell or in *System Properties>Advanced>Environment Variables*.

### SQLServer module with SQL Agent

If you want to use the **SqlServer** module in your SQL Agent Job step, you can place this code on the first two lines of your script.

```powershell
#NOSQLPS
Import-Module -Name SqlServer
```



## Cmdlet reference

- [SqlServer cmdlets](/powershell/module/sqlserver)
- [SQLPS cmdlets](/powershell/module/sqlps)

## Next steps

- [Download SQL Server PowerShell Module](download-sql-server-ps-module.md)
- [SQL Server PowerShell cmdlets](/powershell/module/sqlserver)
- [Use PowerShell with Azure Data Studio](../azure-data-studio/extensions/powershell-extension.md)
