---
title: "Download SQL Server PowerShell Module | Microsoft Docs"
ms.custom: ""
ms.date: 01/23/2020
ms.prod: sql
ms.technology: scripting
ms.topic: conceptual
keywords: 
  - "install sql server powershell, download sql server powershell"
ms.assetid: 
author: markingmyname
ms.author: maghan
ms.reviewer: carlrab
---
# Install SQL Server PowerShell module

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

This article provides directions for installing the **SqlServer** PowerShell module.

## PowerShell modules for SQL Server

There are two SQL Server PowerShell modules:

- **SqlServer**: The SqlServer module includes new cmdlets to support the latest SQL features. The module also contains updated versions of the cmdlets in **SQLPS**. To download the SqlServer module, go to [SqlServer module in the PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver).
- **SQLPS**: The SQLPS module is included with the SQL Server installation (for backwards compatibility), but is no longer being updated. The most up-to-date PowerShell module is the **SqlServer** module.

The versions of the **SqlServer** module in the PowerShell Gallery support versioning and require PowerShell version 5.0 or greater.

For help topics, go to:

- For help topics for the SQL Server PowerShell cmdlets, go to [SqlServer](https://docs.microsoft.com/powershell/module/sqlserver) cmdlets.
- For help topics for the SQLPS cmdlets, go to [SQLPS](https://docs.microsoft.com/powershell/module/sqlps) cmdlets.

## SQL Server Management Studio

SQL Server Management Studio (SSMS), beginning with version 17.0, does not include either PowerShell module. To use PowerShell with SSMS, you must install the **SqlServer** module from the [PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver).

> [!NOTE]
> “With version 16.x of SSMS, an earlier version of the **SqlServer** module is included with SQL Server Management Studio (SSMS)

## Azure Data Studio

Azure Data Studio does not include either PowerShell module. To use PowerShell with Azure Data Studio, you must install the **SqlServer** module from the [PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver).

## Installing or updating the SqlServer module

To install the **SqlServer** module from the PowerShell Gallery, start a [PowerShell](https://docs.microsoft.com/powershell/scripting/powershell-scripting) session as an administrator.

> [!IMPORTANT]
> If you run into problems installing, see the [Install-Module documentation](https://www.powershellgallery.com/packages/PowerShellGet/2.2.1) and [Install-Module reference](https://docs.microsoft.com/powershell/module/powershellget/Install-Module).

### To install the SqlServer module

Run the following command in your administrator session to install the SqlServer module for all users.

```Install-Module -Name SqlServer```

### To overwrite a previous version of the SqlServer module

If there are previous versions of the **SqlServer** module on the computer, you may be able to use `Update-Module` (later in this article), or use the `-AllowClobber` parameter:  

```Install-Module -Name SqlServer -AllowClobber```

### Install for the current user rather than as an administrator

If you are not able to run the PowerShell session as administrator, install for the current user using the following command:

```Install-Module -Name SqlServer -Scope CurrentUser```

### Update the installed version of the SqlServer module

When updated versions of the **SqlServer** module are available, update the version using the following command:

```Update-Module -Name SqlServer```

### To view the versions of the SqlServer module installed

Execute the following command to see the versions of the SqlServer module that have been installed:

```Get-Module SqlServer -ListAvailable```

## Using a specific version of the SqlServer module

To use a specific version of the module, import it with a specific version number similar to the following command:

```Import-Module SqlServer -Version 21.1.18080```

## Using pre-release versions of the SqlServer module

Pre-release (or "preview") versions of the SqlServer module may be available on the PowerShell Gallery. They may be discovered and installed by using the updated *Find-Module* and *Install-Module* cmdlets that are part of the [PowerShellGet](https://www.powershellgallery.com/packages/PowerShellGet) module by passing the *-AllowPrerelease* switch.

### To discover pre-release versions of the SqlServer module

To discover the pre-release (preview) versions of the SqlServer module, run the following command:

```Find-Module SqlServer -AllowPrerelease```

### To install a specific pre-release version of the SqlServer module

To install a specific prerelease/preview version of the module, install it with a specific version number similar to the following:

```Install-Module SqlServer -RequiredVersion 21.1.18040-preview -AllowPrerelease```
