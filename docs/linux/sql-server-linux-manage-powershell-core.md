---
title: Manage SQL Server on Linux with PowerShell Core | Microsoft Docs
description: This article provides an overview of using PowerShell Core with SQL Server on Linux
ms.date: "04/22/2019"
ms.reviewer: "rothja; jroth"
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.topic: conceptual
author: "SQLvariant"
ms.author: "aanelson"
manager: matthend
---
# Manage SQL Server on Linux with PowerShell Core

This article introduces [SQL Server PowerShell](../powershell/sql-server-powershell.md) and walks you through a couple of examples on how to use it with PowerShell Core (PS Core) on macOS & Linux. PowerShell Core is now an Open Source project on [GitHub](https://github.com/powershell/powershell).

## Cross-platform editor options

All of the steps PowerShell Core below will work in a regular terminal, or can run from a terminal within VS Code or Azure Data Studio.  Both VS Code and Azure Data Studio are available on macOS & Linux.  For more information on Azure Data Studio, see [this quickstart](https://docs.microsoft.com/sql/azure-data-studio/quickstart-sql-server).  You may also want to consider using the [PowerShell extension](https://docs.microsoft.com/sql/azure-data-studio/powershell-extension) for it.

## Installing PowerShell Core

For more information on installing PowerShell Core on various supported
and experimental platforms, see the following articles.

- [Installing PowerShell Core on Windows](https://docs.microsoft.com/powershell/scripting/install/installing-powershell-core-on-windows?view=powershell-6)
- [Installing PowerShell Core on Linux](https://docs.microsoft.com/powershell/scripting/install/installing-powershell-core-on-linux?view=powershell-6)
- [Installing PowerShell Core on macOS](https://docs.microsoft.com/powershell/scripting/install/installing-powershell-core-on-macos?view=powershell-6)
- [Installing PowerShell Core on ARM](https://docs.microsoft.com/powershell/scripting/install/powershell-core-on-arm?view=powershell-6)

## Install the SqlServer module

The `SqlServer` module is maintained in the [PowerShell Gallery](https://www.powershellgallery.com/packages/SqlServer/). When working with SQL Server, you should always use the most recent version of the SqlServer PowerShell module.

To install the SqlServer module, open a PowerShell Core session and run the following code:

```powerhsell
Install-Module -Name SqlServer
```

For more information on how to install the SqlServer module from the PowerShell Gallery, see this [page](../powershell/download-sql-server-ps-module.md).

## Using the SqlServer module

Let's start by launching PowerShell Core.  If you are on macOS or Linux, Open a *terminal session* on your computer, and type **pwsh** to launch a new PowerShell Core session.  On Windows, use <kbd>Win</kbd>+<kbd>R</kbd>, and type `pwsh` to launch a new PowerShell Core session.

```
pwsh
```

SQL Server provides a PowerShell module named **SqlServer**. You can use the **SqlServer** module to import the SQL Server components (SQL Server provider and cmdlets) into a PowerShell environment or script.

Copy and paste the following command at the PowerShell prompt to import the **SqlServer** module into your current PowerShell session:

```powershell
Import-Module SqlServer
```

Type the following command at the PowerShell prompt to verify that the **SqlServer** module was imported correctly:

```powershell
Get-Module -Name SqlServer
```

PowerShell should display information similar to the following output:

```
ModuleType Version    Name          ExportedCommands
---------- -------    ----          ----------------
Script     21.1.18102 SqlServer     {Add-SqlAvailabilityDatabase, Add-SqlAvailabilityGroupList...
```

## Connect to SQL Server and get server information

Let's use PowerShell Core to connect to your SQL Server instance on Linux and display a couple of server properties.

Copy and paste the following commands at the PowerShell prompt. When you run these commands, PowerShell will:
- Display a dialog that prompts you for the hostname or IP address of your instance
- Display the *PowerShell credential request* dialog, which prompts you for the credentials. You can use your *SQL username* and *SQL password* to connect to your SQL Server instance on Linux
- Use the **Get-SqlInstance** cmdlet to connect to the **Server** and display a few properties

Optionally, you can just replace the `$serverInstance` variable with the IP address or the hostname of your SQL Server instance.

```powershell
# Prompt for instance & credentials to login into SQL Server
$serverInstance = Read-Host "Enter the name of your instance"
$credential = Get-Credential

# Connect to the Server and return a few properties
Get-SqlInstance -ServerInstance $serverInstance -Credential $credential
# done
```

PowerShell should display information similar to the following output:

```
Instance Name                   Version    ProductLevel UpdateLevel  HostPlatform HostDistribution
-------------                   -------    ------------ -----------  ------------ ----------------
your_server_instance            14.0.3048  RTM          CU13         Linux        Ubuntu
```

> [!NOTE]
> If nothing is displayed for these values, the connection to the target SQL Server instance most likely failed. Make sure that you can use the same connection information to connect from SQL Server Management Studio. Then review the [connection troubleshooting recommendations](sql-server-linux-troubleshooting-guide.md#connection).

## Using the SQL Server PowerShell Provider

Another option for connecting to your SQL Server instance is to use the [SQL Server PowerShell Provider](https://docs.microsoft.com/sql/powershell/sql-server-powershell-provider).  Using the provider allows you to navigate SQL Server instance similar to as if you were navigating the tree structure in Object Explorer, but at the cmdline.  By default this provider is presented as a PSDrive named `SQLSERVER:\` which you can use to connect & navigate SQL Server instances that your domain account has access to.  See [Configuration steps](https://docs.microsoft.com/sql/linux/sql-server-linux-active-directory-auth-overview#configuration-steps) for information on how to setup Active Directory authentication for SQL Server on Linux.

You can also use SQL authentication with the SQL Server PowerShell Provider. To do this, use the `New-PSDrive` cmdlet to create a new PSDrive and supply the proper credentials to connect.

In this example below, you will see an example of how to create a new PSDrive using SQL authentication.

```powershell
# NOTE: We are reusing the values saved in the $credential variable from the above example.

New-PSDrive -Name SQLonDocker -PSProvider SqlServer -Root 'SQLSERVER:\SQL\localhost,10002\Default\' -Credential $credential
```

You can confirm that the drive was created by running the `Get-PSDrive` cmdlet.

```powershell
Get-PSDrive
```

Once you have created your new PSDrive, you can start navigating it.

```powershell
dir SQLonDocker:\Databases
```

Here is what the output might look like.  You might notice this output is similar to what SSMS will display at the Databases node.  It displays the user databases, but not the system databases.

```powershell
Name                 Status           Size     Space  Recovery Compat. Owner
                                            Available  Model     Level
----                 ------           ---- ---------- -------- ------- -----
AdventureWorks2016   Normal      209.63 MB    1.31 MB Simple       130 sa
AdventureWorksDW2012 Normal      167.00 MB   32.47 MB Simple       110 sa
AdventureWorksDW2014 Normal      188.00 MB   78.10 MB Simple       120 sa
AdventureWorksDW2016 Normal      172.00 MB   74.76 MB Simple       130 sa
AdventureWorksDW2017 Normal      208.00 MB   40.57 MB Simple       140 sa
```

If you need to see all databases on your instance, one option is to use the `Get-SqlDatabase` cmdlet.

## Get Databases

An important cmdlet to know is the Get-SqlDatabase.  For many operations that involve a database, or objects within a database, the `Get-SqlDatabase` cmdlet can be used.  If you supply values for both the `-ServerInstance` and `-Database` parameters, only that one database object will be retrieved.  However, if you specify only the `-ServerInstance` parameter, a full list of all databases on that instance will be returned.

```powershell
# NOTE: We are reusing the values saved in the $credential variable from the above example.

# Connect to the Instance and retrieve all databases
Get-SqlDatabase -ServerInstance ServerB -Credential $credential
```

Here is a sample of what might be returned by the Get-SqlDatabase command above:

```powershell
Name                 Status           Size     Space  Recovery Compat. Owner
                                            Available  Model     Level
----                 ------           ---- ---------- -------- ------- -----
AdventureWorks2016   Normal      209.63 MB    1.31 MB Simple       130 sa
AdventureWorksDW2012 Normal      167.00 MB   32.47 MB Simple       110 sa
AdventureWorksDW2014 Normal      188.00 MB   78.10 MB Simple       120 sa
AdventureWorksDW2016 Normal      172.00 MB   74.88 MB Simple       130 sa
AdventureWorksDW2017 Normal      208.00 MB   40.63 MB Simple       140 sa
master               Normal        6.00 MB  600.00 KB Simple       140 sa
model                Normal       16.00 MB    5.70 MB Full         140 sa
msdb                 Normal       15.50 MB    1.14 MB Simple       140 sa
tempdb               Normal       16.00 MB    5.49 MB Simple       140 sa

```

## Examine SQL Server error logs

Let's use PowerShell Core to examine error logs connect on your SQL Server instance on Linux.

Copy and paste the following commands at the PowerShell prompt. They might take a few minutes to run. These commands do the following steps:
- Display a dialog that prompts you for the hostname or IP address of your instance
- Display the *PowerShell credential request* dialog that prompts you for the credentials. You can use your *SQL username* and *SQL password* to connect to your SQL Server instance on Linux
- Use the **Get-SqlErrorLog** cmdlet to connect to the SQL Server instance on Linux and retrieve error logs since **Yesterday**

Optionally, you can replace the `$serverInstance` variable with the IP address or the hostname of your SQL Server instance.

```powershell
# Prompt for instance & credentials to login into SQL Server
$serverInstance = Read-Host "Enter the name of your instance"
$credential = Get-Credential

# Retrieve error logs since yesterday
Get-SqlErrorLog -ServerInstance $serverInstance -Credential $credential -Since Yesterday
# done
```

## Explore cmdlets currently available in PS Core
While the SqlServer module currently has 106 cmdlets available in Windows PowerShell, only 59 of the 106 are available in PSCore. A full list of 59 cmdlets currently available is included below.  For in-depth documentation of all cmdlets in the SqlServer module, see the SqlServer [cmdlet reference](https://docs.microsoft.com/powershell/module/sqlserver/).

```powershell
Get-Command -Module SqlServer -CommandType Cmdlet |
SORT -Property Noun |
SELECT Name
```

- ConvertFrom-EncodedSqlName
- ConvertTo-EncodedSqlName
- Get-SqlAgent
- Get-SqlAgentJob
- Get-SqlAgentJobHistory
- Get-SqlAgentJobSchedule
- Get-SqlAgentJobStep
- Get-SqlAgentSchedule
- Remove-SqlAvailabilityDatabase
- Resume-SqlAvailabilityDatabase
- Add-SqlAvailabilityDatabase
- Suspend-SqlAvailabilityDatabase
- New-SqlAvailabilityGroup
- Set-SqlAvailabilityGroup
- Remove-SqlAvailabilityGroup
- Switch-SqlAvailabilityGroup
- Join-SqlAvailabilityGroup
- Revoke-SqlAvailabilityGroupCreateAnyDatabase
- Grant-SqlAvailabilityGroupCreateAnyDatabase
- New-SqlAvailabilityGroupListener
- Set-SqlAvailabilityGroupListener
- Add-SqlAvailabilityGroupListenerStaticIp
- Set-SqlAvailabilityReplica
- Remove-SqlAvailabilityReplica
- New-SqlAvailabilityReplica
- Set-SqlAvailabilityReplicaRoleToSecondary
- New-SqlBackupEncryptionOption
- Get-SqlBackupHistory
- Invoke-Sqlcmd
- New-SqlCngColumnMasterKeySettings
- Remove-SqlColumnEncryptionKey
- Get-SqlColumnEncryptionKey
- Remove-SqlColumnEncryptionKeyValue
- Add-SqlColumnEncryptionKeyValue
- Get-SqlColumnMasterKey
- Remove-SqlColumnMasterKey
- New-SqlColumnMasterKey
- Get-SqlCredential
- Set-SqlCredential
- New-SqlCredential
- Remove-SqlCredential
- New-SqlCspColumnMasterKeySettings
- Get-SqlDatabase
- Restore-SqlDatabase
- Backup-SqlDatabase
- Set-SqlErrorLog
- Get-SqlErrorLog
- New-SqlHADREndpoint
- Set-SqlHADREndpoint
- Get-SqlInstance
- Add-SqlLogin
- Remove-SqlLogin
- Get-SqlLogin
- Set-SqlSmartAdmin
- Get-SqlSmartAdmin
- Read-SqlTableData
- Write-SqlTableData
- Read-SqlViewData
- Convert-UrnToPath

## See also
- [SQL Server PowerShell](../relational-databases/scripting/sql-server-powershell.md)