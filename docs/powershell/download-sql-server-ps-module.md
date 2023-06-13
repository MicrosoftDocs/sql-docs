---
title: Download SQL Server PowerShell Module
description: Learn how to install the SqlServer PowerShell module, which provides cmdlets that support the latest SQL features, and also contains updated versions of the cmdlets in the SQLPS module.
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier, jopilov
ms.date: 05/24/2023
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
ms.custom: intro-installation
---
# Install the SQL Server PowerShell module

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article provides directions for installing the **SqlServer** PowerShell module.

## PowerShell modules for SQL Server

There are two SQL Server PowerShell modules:

- **SqlServer**: The SqlServer module includes new cmdlets to support the latest SQL features. The module also contains updated versions of the cmdlets in **SQLPS**. To download the SqlServer module, go to [SqlServer module in the PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver).

- **SQLPS**: The SQLPS is the module used by [SQL Agent](sql-server-powershell.md#sql-server-agent) to run agent jobs in agent job steps using the PowerShell subsystem.

> [!NOTE]  
> The versions of the **SqlServer** module in the PowerShell Gallery support versioning and require PowerShell version 5.1 or greater.

For help topics, go to:

- [SqlServer](/powershell/module/sqlserver) cmdlets.
- [SQLPS](/powershell/module/sqlps) cmdlets.

## SQL Server Management Studio

[SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md), doesn't install either PowerShell module. To use PowerShell with SSMS, install the **SqlServer** module from the [PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver).

> [!NOTE]  
> SQL Server Management Studio (SSMS) 16.x (and earlier versions) included the **SQLPS** module.

## Azure Data Studio

[Azure Data Studio](../azure-data-studio/download-azure-data-studio.md) doesn't install either PowerShell module. To use PowerShell with Azure Data Studio, install the **SqlServer** module from the [PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver).

You can use the [PowerShell extension](../azure-data-studio/extensions/powershell-extension.md), which provides rich PowerShell editor support in Azure Data Studio.

## Install or update the SqlServer module

To install the SqlServer module from the PowerShell Gallery, start a [PowerShell](/powershell/scripting/overview) session and run `Install-Module SQLServer`.

```powershell
Install-Module -Name SqlServer
```

If running on Windows PowerShell you can use `Install-Module SQLServer -Scope CurrentUser` to install the module for just the current user and avoid needing elevated permissions.

### Install the SqlServer module for all users

To install the SqlServer module for all users run the command below in an elevated PowerShell session; start a PowerShell session as administrator:

```powershell
Install-Module -Name SqlServer
```

### Install the SqlServer module to an offline computer

This section walks you through the steps of installing the SqlServer PowerShell module on a computer that has no access to the internet. You need two computers to accomplish this installation: a computer connected to the internet and the destination computer that is offline (not Internet connected).

1. On a computer connected to the internet download the SQLServer PowerShell module. This command will download and save all the files for the module in the sample `$env:TEMP\SQLServer\<SomeVersion>` folder, which is defined in the **-Path** parameter. You can choose a folder of your preference as a destination on your internet-connected computer.

   ```powershell
   Save-Module -Name SqlServer -Path $env:TEMP
   ```
  
1. Browse to the folder location in File Explorer to verify that a `$env:TEMP\SQLServer\<SomeVersion>` folder exists or run this command:

   ```powershell
   Get-ChildItem -Path $env:TEMP\SQLServer\
   ```

1. Copy the `$env:TEMP\SQLServer\<SomeVersion>` folder you found in step 2 to the destination offline computer in `%ProgramFiles%\WindowsPowerShell\Modules\SqlServer` folder (this folder would work for both PS5 and PS7). Be sure to replace `<SomeVersion>` with the value you found in the previous step.


1. Confirm that the SqlServer PowerShell module is available on the offline computer by running the following PowerShell cmdlet:

   ```powershell
   Get-Module SqlServer -ListAvailable
   ```

   The output may look like this (actual version may be different and must match `<SomeVersion>` listed previously):

   ```console
      PS C:\Users\user1> Get-Module SqlServer -ListAvailable

          Directory: C:\Program Files\WindowsPowerShell\Modules

      ModuleType Version    Name                     ExportedCommands
      ---------- -------    ----                     ----------------
      Script     22.1.1     SqlServer                {Add-RoleMember, Add-SqlAvail...
   ```

### View the versions of the SqlServer module installed

Execute the following command to see the versions of the SqlServer module that have been installed

```powershell
Get-Module SqlServer -ListAvailable
```

To view the version of the SqlServer module loaded in the current session

```powershell
(Get-Module SqlServer).Version
```

### Overwrite a previous version of the SqlServer module

You can also use the `Install-Module` command to overwrite a previous version.

```powershell
Install-Module -Name SqlServer -AllowClobber
```

> [!NOTE]  
> PowerShell always uses the latest module installed.

### Update the installed version of the SqlServer module

When updated versions of the **SqlServer** module are available, you can install the newer version using the following command:

```powershell
Update-Module -Name SqlServer -AllowClobber
```

You can use the `Update-Module` command to install the newest version of the SQLServer PowerShell module, but that doesn't remove older versions. It installs the newer version side by side to allow you the ability to experiment with the latest version, yet still have older modules installed.

However, if you don't want to keep older module versions, then you can use the `Uninstall-Module` command to remove previous versions.

You can use the following command to list if more than one version is installed:

```powershell
Get-Module SqlServer -ListAvailable
```

You can use the following command to remove older versions:

```powershell
Uninstall-module -Name SQLServer -RequiredVersion "<version number>"
```

### Troubleshoot

If you run into problems installing, see the [Install-Module documentation](https://www.powershellgallery.com/packages/PowerShellGet/2.2.1) and [Install-Module reference](/powershell/module/powershellget/Install-Module).

## Use a specific version of the SqlServer module

To use a specific version of the module, import it with a specific version number similar to the following command:

```powershell
Import-Module SqlServer -Version 21.1.18218
```

## Pre-release versions of the SqlServer module

Pre-release (or "preview") versions of the SqlServer module may be available in the PowerShell Gallery.

> [!IMPORTANT]  
> These versions may be discovered and installed by using the updated *Find-Module* and *Install-Module* cmdlets that are part of the [PowerShellGet](https://www.powershellgallery.com/packages/PowerShellGet) module by passing the *-AllowPrerelease* switch. To use these cmdlets, install the PowerShellGet module and then open a new session.

### Discover pre-release versions of the SqlServer module

To discover the pre-release (preview) versions of the SqlServer module, run the following command:

```powershell
Find-Module SqlServer -AllowPrerelease
```

### Install a specific pre-release version of the SqlServer module

To install a specific pre-release version of the module, install it with a specific version number.

You can try to use the following command:

```powershell
Install-Module SqlServer -RequiredVersion 21.1.18218-preview -AllowPrerelease
```

## SQL Server PowerShell on Linux

Visit [Manage SQL Server on Linux with PowerShell](../linux/sql-server-linux-manage-powershell-core.md) to see how to install SQL Server PowerShell on Linux.

## Other modules

- [Az.Sql](https://www.powershellgallery.com/packages/Az.Sql/) - SQL service cmdlets for Azure Resource Manager in Windows PowerShell and PowerShell.

- [SqlServerDsc](https://www.powershellgallery.com/packages/SqlServerDsc/) - Module with DSC resources for deployment and configuration of Microsoft SQL Server.

## Cmdlet reference

- [SqlServer cmdlets](/powershell/module/sqlserver)
- [SQLPS cmdlets](/powershell/module/sqlps)

## Next steps

- [SQL Server PowerShell](sql-server-powershell.md)
- [SQL Server PowerShell cmdlets](/powershell/module/sqlserver)
- [Use PowerShell with Azure Data Studio](../azure-data-studio/extensions/powershell-extension.md)
