---
title: "Download SQL Server PowerShell Module | Microsoft Docs"
ms.custom: ""
ms.date: "01/05/2018"
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
> There are two SQL Server PowerShell modules; **SqlServer** and **SQLPS**. The **SQLPS** module is included with the SQL Server installation (for backwards compatibility), but is no longer being updated. The most up-to-date PowerShell module is the **SqlServer** module. The **SqlServer** module contains updated versions of the cmdlets in **SQLPS**, and also includes new cmdlets to support the latest SQL features. 
> Previous versions of the **SqlServer** module *were* included with SQL Server Management Studio (SSMS), but only with the 16.x versions of SSMS. To use PowerShell with SSMS 17.0 and later, the **SqlServer** module must be installed from the PowerShell Gallery.

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

```Import-Module SqlServer -Version 21.0.17178```


The versions of the **SqlServer** module in the PowerShell Gallery support versioning and require PowerShell version 5.0 or greater. 

* [SqlServer module in the PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver) 
* [SqlServer cmdlets](https://docs.microsoft.com/powershell/module/sqlserver)
* [SQLPS cmdlets](https://docs.microsoft.com/powershell/module/sqlps)
