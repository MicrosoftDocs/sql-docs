---
title: "Download SQL Server PowerShell Module | Microsoft Docs"
ms.custom: ""
ms.date: 01/31/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
keywords: 
  - "install sql server powershell, download sql server powershell"
ms.assetid: 
author: stevestein
ms.author: sstein
manager: craigg
---
# Install SQL Server PowerShell module
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

This article provides directions for installing the **SqlServer** PowerShell module.
> [!NOTE]
> There are two SQL Server PowerShell modules: 
> * **SQLPS**: This module is included with the SQL Server installation (for backwards compatibility), but is no longer being updated. The most up-to-date PowerShell module is the **SqlServer** module.
> * **SqlServer**: This module includes new cmdlets to support the latest SQL features. The module also contains updated versions of the cmdlets in **SQLPS**. 

Previous versions of the **SqlServer** module *were* included with SQL Server Management Studio (SSMS), but only with the 16.x versions of SSMS. To use PowerShell with SSMS 17.0 and later, the **SqlServer** module must be installed from the [PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver).
The current version of the **SqlServer** module is 21.1.18080. This is based on version v150 of Microsoft.SQLServer.SMO and supports the next version of SQL Server. The last version of the module based on version v140 of Microsoft.SQLServer.SMO) is 21.0.17279.

Pre-release versions of the module may become available more frequently: please refer to the section at the bottom of this page on how to get such versions of the module.

To install the **SqlServer** module from the PowerShell Gallery, start a [PowerShell](https://docs.microsoft.com/powershell/scripting/powershell-scripting) session and use the following commands. If you run into problems installing, see the [Install-Module documentation](https://docs.microsoft.com/powershell/gallery/psget/module/psget_install-module) and [Install-Module reference](https://docs.microsoft.com/powershell/module/powershellget/Install-Module).

To install the **SqlServer** module:

```Install-Module -Name SqlServer```

If there are previous versions of the **SqlServer** module on the computer, you may be able to use `Update-Module` (later in this article), or provide the `-AllowClobber` parameter:  

```Install-Module -Name SqlServer -AllowClobber```

If you are not able to run the PowerShell session as administrator, you can install for the current user:

```Install-Module -Name SqlServer -Scope CurrentUser```

When updated versions of the **SqlServer** module are available, you can update the version using `Update-Module`:

```Update-Module -Name SqlServer```

To view the versions of the module installed:

```Get-Module SqlServer -ListAvailable```

To use a specific version of the module, you can import it with a specific version number similar to the following:

```Import-Module SqlServer -Version 21.1.18080```

> [!NOTE]
> Prerelease (or "preview") versions of the module may be available on the PowerShell Gallery. They may be discovered and installed
> by using the updated *Find-Module* and *Install-Module* cmdlets that are part of the [PowerShellGet](https://www.powershellgallery.com/packages/PowerShellGet)
> module) by passing the *-AllowPrerelease* switch.
>
> To discover the prerelease/preview version of the module, you can run the following command:
>
> ```Find-Module SqlServer -AllowPrerelease```
>
> To install a specific prerelease/preview version of the module, you can install it with a specific version number similar to the following:
>
> ```Install-Module SqlServer -RequiredVersion 21.1.18040-preview -AllowPrerelease```
> 

The versions of the **SqlServer** module in the PowerShell Gallery support versioning and require PowerShell version 5.0 or greater. 

* [SqlServer module in the PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver) 
* [SqlServer cmdlets](https://docs.microsoft.com/powershell/module/sqlserver)
* [SQLPS cmdlets](https://docs.microsoft.com/powershell/module/sqlps)
