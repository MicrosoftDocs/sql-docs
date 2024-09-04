---
title: Manage SQL Server on Linux with PowerShell Core
description: Learn about SQL Server PowerShell by walking through a couple of examples on how to use SQL Server PowerShell with PowerShell Core on macOS and Linux.
author: SQLvariant
ms.author: aanelson
ms.reviewer: vanto, randolphwest
ms.date: 11/16/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Manage SQL Server on Linux with PowerShell Core

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article introduces [SQL Server PowerShell](/powershell/sql-server/sql-server-powershell) and walks you through a couple of examples on how to use it with PowerShell Core (PS Core) on macOS & Linux. PowerShell Core is now an Open Source project on [GitHub](https://github.com/powershell/powershell).

## Cross-platform editor options

All of the following steps for PowerShell Core work in a regular terminal, or you can run them from a terminal within Visual Studio Code or Azure Data Studio. Both VS Code and Azure Data Studio are available on macOS and Linux. For more information on Azure Data Studio, see [Quickstart: Use Azure Data Studio to connect and query SQL Server](/azure-data-studio/quickstart-sql-server). You may also want to consider using the [PowerShell Editor Support for Azure Data Studio](/azure-data-studio/extensions/powershell-extension).

## Install PowerShell Core

For more information on installing PowerShell Core on various supported and experimental platforms, see the following articles:

- [Installing PowerShell Core on Windows](/powershell/scripting/install/installing-powershell-core-on-windows)
- [Installing PowerShell Core on Linux](/powershell/scripting/install/installing-powershell-core-on-linux)
- [Installing PowerShell Core on macOS](/powershell/scripting/install/installing-powershell-core-on-macos)
- [Installing PowerShell Core on ARM](/powershell/scripting/install/powershell-core-on-arm)

## Install the SqlServer module

The `SqlServer` module is maintained in the [PowerShell Gallery](https://www.powershellgallery.com/packages/SqlServer/). When working with SQL Server, you should always use the most recent version of the SqlServer PowerShell module.

To install the SqlServer module, open a PowerShell Core session and run the following code:

```powerhsell
Install-Module -Name SqlServer
```

For more information on how to install the SqlServer module from the PowerShell Gallery, see [Install the SQL Server PowerShell module](/powershell/sql-server/download-sql-server-ps-module).

## Use the SqlServer module

Let's start by launching PowerShell Core. If you're on macOS or Linux, Open a *terminal session* on your computer, and type `pwsh` to launch a new PowerShell Core session. On Windows, use <kbd>Win</kbd>+<kbd>R</kbd>, and type `pwsh` to launch a new PowerShell Core session.

```console
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

The following steps use PowerShell Core to connect to your SQL Server instance on Linux and display a couple of server properties.

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

## Use the SQL Server PowerShell Provider

Another option for connecting to your SQL Server instance is to use the [SQL Server PowerShell Provider](/powershell/sql-server/sql-server-powershell-provider). Using the provider allows you to navigate SQL Server instance similar to as if you were navigating the tree structure in Object Explorer, but at the cmdline. By default this provider is presented as a PSDrive named `SQLSERVER:\`, which you can use to connect & navigate SQL Server instances that your domain account has access to. See [Configuration steps](./sql-server-linux-active-directory-auth-overview.md#configuration-steps) for information on how to set up Active Directory authentication for SQL Server on Linux.

You can also use SQL authentication with the SQL Server PowerShell Provider. To do this, use the `New-PSDrive` cmdlet to create a new PSDrive and supply the proper credentials to connect.

In the following example, you see an example of how to create a new PSDrive using SQL authentication.

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

Here is what the output might look like. You might notice this output is similar to what SSMS displays at the Databases node. It displays the user databases, but not the system databases.

```powershell
Name                 Status           Size     Space  Recovery Compat. Owner
                                            Available  Model     Level
----                 ------           ---- ---------- -------- ------- -----
AdventureWorks2022   Normal      209.63 MB    1.31 MB Simple       130 sa
AdventureWorksDW2022 Normal      167.00 MB   32.47 MB Simple       110 sa
AdventureWorksDW2022 Normal      188.00 MB   78.10 MB Simple       120 sa
AdventureWorksDW2022 Normal      172.00 MB   74.76 MB Simple       130 sa
AdventureWorksDW2022 Normal      208.00 MB   40.57 MB Simple       140 sa
```

If you need to see all databases on your instance, one option is to use the `Get-SqlDatabase` cmdlet.

## Get Databases

An important cmdlet to know is the `Get-SqlDatabase`. For many operations that involve a database, or objects within a database, the `Get-SqlDatabase` cmdlet can be used. If you supply values for both the `-ServerInstance` and `-Database` parameters, only that one database object is retrieved. However, if you specify only the `-ServerInstance` parameter, a full list of all databases on that instance are returned.

```powershell
# NOTE: We are reusing the values saved in the $credential variable from the above example.

# Connect to the Instance and retrieve all databases
Get-SqlDatabase -ServerInstance ServerB -Credential $credential
```

Here is a sample of what the Get-SqlDatabase command returns:

```powershell
Name                 Status           Size     Space  Recovery Compat. Owner
                                            Available  Model     Level
----                 ------           ---- ---------- -------- ------- -----
AdventureWorks2022   Normal      209.63 MB    1.31 MB Simple       130 sa
AdventureWorksDW2022 Normal      167.00 MB   32.47 MB Simple       110 sa
AdventureWorksDW2022 Normal      188.00 MB   78.10 MB Simple       120 sa
AdventureWorksDW2022 Normal      172.00 MB   74.88 MB Simple       130 sa
AdventureWorksDW2022 Normal      208.00 MB   40.63 MB Simple       140 sa
master               Normal        6.00 MB  600.00 KB Simple       140 sa
model                Normal       16.00 MB    5.70 MB Full         140 sa
msdb                 Normal       15.50 MB    1.14 MB Simple       140 sa
tempdb               Normal       16.00 MB    5.49 MB Simple       140 sa
```

## Examine SQL Server error logs

The following steps use PowerShell Core to examine error logs connect on your SQL Server instance on Linux.

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

## Explore cmdlets currently available in PowerShell Core

While the SqlServer module currently has 109 cmdlets available in Windows PowerShell, only 62 of the 109 are available in PSCore. Following is a full list of 62 cmdlets currently available. For in-depth documentation of all cmdlets in the SqlServer module, see the SqlServer [cmdlet reference](/powershell/module/sqlserver/).

The following command shows you all of the cmdlets available on the version of PowerShell you're using.

```powershell
Get-Command -Module SqlServer -CommandType Cmdlet |
Sort-Object -Property Noun |
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
- Invoke-SqlAssessment
- Get-SqlAssessmentItem
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
- Read-SqlXEvent
- Convert-UrnToPath

## Related content

- [SQL Server PowerShell](/powershell/sql-server/sql-server-powershell)
