---
title: "Install SQL Server with PowerShell Desired State Configuration | Microsoft Docs"
description: "Learn how to install SQL Server by using PowerShell Desired State Configuration (DSC)."
ms.custom: ""
ms.date: "10/26/2018"
ms.devlang: PowerShell
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
author: randomnote1
ms.author: dareist
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---

# Install SQL Server with PowerShell Desired State Configuration

Have you ever gone through the SQL Server installation interface just by selecting the same buttons, entering the same information, and not giving it a second thought? The installation finished, but you forgot to specify the DBA group in the **sysadmin** role. Then you had to do these things:
* Drop into single-user mode.
* Add the appropriate users or groups.
* Bring SQL Server back up in multiuser mode.
* Test. 

What's worse is now the confidence of the entire installation is shaken. "What else did I forget?" you might ask yourself.

Read about [PowerShell Desired State Configuration (DSC)](https://docs.microsoft.com/powershell/dsc/overview). By using DSC, you build one configuration template that you can reuse over hundreds and thousands of servers. Depending on the build, you might have to tweak a few of the setup parameters. But that's not a significant issue because you can keep all of the standard settings in place. It eliminates the possibility that you'll forget to enter an important parameter.

This article explores the initial setup of a standalone instance of SQL Server 2017 on Windows Server 2016 by using the **SqlServerDsc** DSC resource. Some prior knowledge of DSC is helpful as we won't explore how DSC works.

The following items are required for this walkthrough:

- A machine that runs Windows Server 2016.
- SQL Server 2017 installation media.
- The **SqlServerDsc** DSC resource.

## Prerequisites

In most cases, DSC is used to handle the prerequisite requirements. But for the purposes of this demo, we handle the prerequisites manually.

## Install the SqlServerDsc DSC resource

Download the [SqlServerDsc](https://www.powershellgallery.com/packages/SqlServerDsc) DSC resource from the [PowerShell Gallery](https://www.powershellgallery.com/) by using the [Install-Module](https://docs.microsoft.com/powershell/module/powershellget/Install-Module?view=powershell-5.1) cmdlet. 

> [!NOTE]
> Make sure PowerShell is running **As Administrator** to install the module.

```PowerShell
Install-Module -Name SqlServerDsc
```

### Get the SQL Server 2017 installation media
Download the SQL Server 2017 installation media to the server. We downloaded SQL Server 2017 Enterprise from a Visual Studio subscription and copied the ISO to `C:\en_sql_server_2017_enterprise_x64_dvd_11293666.iso`.

Now the ISO must be extracted to a directory:

```PowerShell
New-Item -Path C:\SQL2017 -ItemType Directory
$mountResult = Mount-DiskImage -ImagePath 'C:\en_sql_server_2017_enterprise_x64_dvd_11293666.iso' -PassThru
$volumeInfo = $mountResult | Get-Volume
$driveInfo = Get-PSDrive -Name $volumeInfo.DriveLetter
Copy-Item -Path ( Join-Path -Path $driveInfo.Root -ChildPath '*' ) -Destination C:\SQL2017\ -Recurse
Dismount-DiskImage -ImagePath 'C:\en_sql_server_2017_enterprise_x64_dvd_11293666.iso'
```

## Create the configuration

### Configuration

Create the configuration function that will be called to generate the [Managed Object Format (MOF)](https://docs.microsoft.com/windows/desktop/WmiSdk/managed-object-format--mof-) documents:

```PowerShell
Configuration SQLInstall
{...}
```

### Modules

Import the modules into the current session. These modules tell the configuration document how to build the MOF documents. They also tell the DSC engine how to apply the MOF documents to the server:

```PowerShell
Import-DscResource -ModuleName SqlServerDsc
```

### Resources

#### .NET Framework

SQL Server relies on the .NET Framework. So we need to make sure it's installed before we install SQL Server. The **WindowsFeature** resource is used to install the **Net-Framework-45-Core** Windows feature:

```PowerShell
WindowsFeature 'NetFramework45'
{
     Name = 'Net-Framework-45-Core'
     Ensure = 'Present'
}
```

#### SqlSetup

The **SqlSetup** resource is used to tell DSC how to install SQL Server. The parameters required for a basic installation are as follows:

- **InstanceName**. The name of the instance. Use **MSSQLSERVER** for a default instance.
- **Features**. The features to install. In this example, we install only the **SQLEngine** feature.
- **SourcePath**. The path to the SQL installation media. In this example, we stored the SQL installation media in `C:\SQL2017`. A network share can minimize the space used on the server.
- **SQLSysAdminAccounts**. The users or groups who are to be a member of the **sysadmin** role. In this example, we grant the local Administrators group **sysadmin** access. 

> [!NOTE]
> We don't recommend this configuration in a high-security environment.

A full list and description of the parameters available on **SqlSetup** are available on the [SqlServerDsc GitHub repository](https://github.com/PowerShell/SqlServerDsc/tree/master#sqlsetup).

The **SqlSetup** resource installs only SQL Server and **doesn't** maintain the settings that are applied. An example is if the **SQLSysAdminAccounts** are specified at installation time. An admin might add or remove sign-ins to or from the **sysadmin** role. But the **SqlSetup** resource won't be affected. If you want DSC to enforce the membership of the **sysadmin** role, use the [SqlServerRole](https://github.com/PowerShell/SqlServerDsc/tree/master#sqlserverrole) resource.

#### Finish configuration

```PowerShell
Configuration SQLInstall
{
     Import-DscResource -ModuleName SqlServerDsc

     node localhost
     {
          WindowsFeature 'NetFramework45'
          {
               Name   = 'NET-Framework-45-Core'
               Ensure = 'Present'
          }

          SqlSetup 'InstallDefaultInstance'
          {
               InstanceName        = 'MSSQLSERVER'
               Features            = 'SQLENGINE'
               SourcePath          = 'C:\SQL2017'
               SQLSysAdminAccounts = @('Administrators')
               DependsOn           = '[WindowsFeature]NetFramework45'
          }
     }
}
```

## Build and deploy

### Compile the configuration

Dot source the configuration script:

```PowerShell
. .\SQLInstallConfiguration.ps1
```

Run the configuration function:

```PowerShell
SQLInstall
```

A directory called **SQLInstall** is created in the working directory. It contains a file called **localhost.mof**. Examine the contents of the MOF, which shows the compiled DSC configuration.

### Deploy the configuration

To start the DSC deployment of SQL Server, call the **Start-DscConfiguration** cmdlet. The following parameters are provided to the cmdlet:

- **Path**. The path to the folder that contains the MOF documents to deploy. An example is `C:\SQLInstall`.
- **Wait**. Wait for the configuration job to finish.
- **Force**. Override any existing DSC configurations.
- **Verbose**. Show the verbose output. It's useful when you push a configuration for the first time to aid in troubleshooting.

```PowerShell
Start-DscConfiguration -Path C:\SQLInstall -Wait -Force -Verbose
```

As the configuration applies, the verbose output shows you what's happening. As long as no errors (red text) are thrown, when **Operation 'Invoke CimMethod' complete** appears on the screen, SQL Server should be installed.

## Validate installation

### DSC

The [Test-DscConfiguration](https://docs.microsoft.com/powershell/module/psdesiredstateconfiguration/test-dscconfiguration) cmdlets can determine if the current state of the server meets the desired state. In this case, it's the SQL Server installation. The result of **Test-DscConfiguration** should be **True**:

```PowerShell
PS C:\> Test-DscConfiguration
True
```

### Services

The services list now returns SQL Server services:

```PowerShell
PS C:\> Get-Service -Name *SQL*
Status  Name           DisplayName
------  ----           -----------
Running MSSQLSERVER    SQL Server (MSSQLSERVER)
Stopped SQLBrowser     SQL Server Browser
Running SQLSERVERAGENT SQL Server Agent (MSSQLSERVER)
Running SQLTELEMETRY   SQL Server CEIP service (MSSQLSERVER)
Running SQLWriter      SQL Server VSS Writer
```

### SQL Server

```PowerShell
PS C:\> & sqlcmd -S $env:COMPUTERNAME
1> SELECT @@SERVERNAME
2> GO
1> quit
```

## See also

[Windows PowerShell Desired State Configuration Overview](https://docs.microsoft.com/powershell/dsc/overview)

[Install SQL Server from the command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)

[Install SQL Server by using a configuration file](../../database-engine/install-windows/install-sql-server-using-a-configuration-file.md)
