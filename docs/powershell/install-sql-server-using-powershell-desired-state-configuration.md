# Install SQL Server Using PowerShell Desired State Configuration

How many times have you clicked through the SQL Server installation interface just clicking the same old buttons, entering the same old information, not giving it much of a second thought? Then the installation finishes and the realization of "I forgot to specify the DBA group in the sysadmins role" hits. Now you have to spend precious time dropping into single-user mode, adding the appropriate users/groups, bringing SQL back up in multi-user mode, and testing. What's worse is now the confidence of the entire installation is shaken. "What else did I forget?" I for one have been in that situation more than once.

Enter PowerShell Desired State Configuration (DSC). Using DSC I can build one configuration template that can be reused over hundreds and thousands of servers. Depending on the build, I may have to tweak a few of the setup parameters, but that's not a big deal because I can still keep all of the standard settings in place. The beautiful thing is it eliminates the possibility that will forget to enter an important parameter after spending a sleepless night caring for my kids.

In this article I will explore the initial setup of a standalone instance of SQL Server 2017 on Windows Server 2016 using the SqlServerDsc DSC resource. Some prior knowledge of DSC will be helpful as I will not explore the hows and whys of how DSC works.

The following items are required for this walkthrough:

- A machine running Windows Server 2016
- SQL Server 2017 installation media
- The SqlServerDsc DSC resource (version 10.0.0.0 is the current release at the time of this writing)

## Prerequisites

In most cases DSC will be used to handle the prerequisite requirements. However, for the purposes of this demo, I will handle the prerequisites manually.

## Install the SqlServerDsc DSC Resource

The [SqlServerDsc](https://www.powershellgallery.com/packages/SqlServerDsc) DSC resource can be downloaded from the [PowerShell Gallery](https://www.powershellgallery.com/) using the [Install-Module](https://docs.microsoft.com/en-us/powershell/module/powershellget/Install-Module?view=powershell-5.1) cmdlet. _Note: Ensure PowerShell is running "As Administrator" to install the module._

```PowerShell
Install-Module -Name SqlServerDsc
```

Obtain the SQL Server 2017 Installation Media
Download the SQL Server 2017 installation media to the server. I downloaded SQL Server 2017 Enterprise from my Visual Studio subscription and copied the ISO to "C:\en_sql_server_2017_enterprise_x64_dvd_11293666.iso".

Now the ISO must be extracted to a directory.

```PowerShell
New-Item -Path C:\SQL2017 -ItemType Directory
$mountResult = Mount-DiskImage -ImagePath 'C:\en_sql_server_2017_enterprise_x64_dvd_11293666.iso' -PassThru
$volumeInfo = $mountResult | Get-Volume
$driveInfo = Get-PSDrive -Name $volumeInfo.DriveLetter
Copy-Item -Path ( Join-Path -Path $driveInfo.Root -ChildPath '*' ) -Destination C:\SQL2017\ -Recurse
Dismount-DiskImage -ImagePath 'C:\en_sql_server_2017_enterprise_x64_dvd_11293666.iso'
```

## Create the Configuration

### Configuration

Create the configuration function which will be called to generate the MOF document(s).

```PowerShell
Configuration SQLInstall
{...}
```

### Modules

Import the modules into the current session. These tell the configuration document how to build the MOF document(s) and tells the DSC engine how to apply the MOF document(s) to the server.

```PowerShell
Import-DscResource -ModuleName SqlServerDsc
```

### Resources

#### .NET Framework

SQL Server relies on the .NET framework, therefore we need to ensure it is installed prior to installing SQL Server. In order to accomplish this, the WindowsFeature resource is utilized to install the Net-Framework-45-Core Windows Feature.

```PowerShell
WindowsFeature 'NetFramework45'
{
     Name = 'Net-Framework-45-Core'
     Ensure = 'Present'
}
```

#### SqlSetup

The SqlSetup resource is used to tell DSC how to install SQL Server. The parameters required for a basic installation are:

- **InstanceName**: The name of the instance. Utilize MSSQLSERVER for a default instance.
- **Features** The features to install. In this example I am only installing the SQLEngine feature.
- **SourcePath**: The path to the SQL installation media. In this example I stored the SQL installation media in "C:\SQL2017". A network share can be utilized to minimize the space used on the server.
- **SQLSysAdminAccounts**: The users or groups who are to be a member of the sysadmin role. In this example I am granting the local Administrators group sysadmin access. _Note: This configuration is not recommended in a high security environment._

A full list and description of the parameters available on SqlSetup are available on the [SqlServerDsc GitHub respository](https://github.com/PowerShell/SqlServerDsc/tree/master#sqlsetup).

The SqlSetup resource is odd because it only installs SQL and DOES NOT maintain the settings that are applied. For example, if the SQLSysAdminAccounts are specified at installation time, an admin could add or remove logins from to/from the sysadmin role and the SqlSetup resource wouldn't care. If it is desired that DSC enforces the membership of the sysadmin role, the [SqlServerRole](https://github.com/PowerShell/SqlServerDsc/tree/master#sqlserverrole) resource should be utilized.

#### Complete Configuration

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

## Build and Deploy

### Compile the Configuration

Dot-source the configuration script:

```PowerShell
. .\SQLInstallConfiguration.ps1
```

Execute the configuration function:

```PowerShell
SQLInstall
```

A directory will be created in the working directory called "SQLInstall" and will contain a file call "localhost.mof". Examining the contents of the MOF will show the compiled DSC configuration.

### Deploy the Configuration

To start the DSC deployment of SQL Server, call the Start-DscConfiguration cmdlet. The parameters provided to the cmdlet are:

- **Path**: The path to the folder containing the MOF documents to deploy. (eg. "C:\SQLInstall")
- **Wait**: Wait for the configuration job to complete.
- **Force**: Override any existing DSC configurations.
- **Verbose**: Show the verbose output. This is handy when pushing a configuration for the first time to aid in troubleshooting.

```PowerShell
Start-DscConfiguration -Path C:\SQLInstall -Wait -Force -Verbose
```

As the configuration applies, the Verbose output will show you what is happening and give you a warm and fuzzy feeling that SOMETHING is happening. As long as no errors (red text) are thrown, when "Operation 'Invoke CimMethod' complete." is displayed on the screen, SQL should be installed.

## Validate Installation

### DSC

The [Test-DscConfiguration](https://docs.microsoft.com/en-us/powershell/module/psdesiredstateconfiguration/test-dscconfiguration) cmdlets can be utilized to determine if the current state of the server, in this case the SQL installation, meets the desired state. The result of Test-DscConfiguration should be "True".

```PowerShell
PS C:\> Test-DscConfiguration
True
```

### Services

The services listing should now return SQL Server services

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

### That's a Wrap

And there you have it. Installing SQL Server in a consistent manner has never been easier.
