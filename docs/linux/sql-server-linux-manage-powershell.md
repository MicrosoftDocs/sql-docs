---
# required metadata

title: Use PowerShell on Windows to Manage SQL Server on Linux - SQL Server vNext | Microsoft Docs
description: This topic provides an overview of using PowerShell on Windows with SQL Server on Linux.
author: sanagama 
ms.author: sanagama 
manager: jhubbard
ms.date: 11/16/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: a3492ce1-5d55-4505-983c-d6da8d1a94ad

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Use PowerShell on Windows to Manage SQL Server on Linux

This topic introduces [SQL Server PowerShell](https://msdn.microsoft.com/en-us/library/mt740629.aspx) and walks you through a couple of examples on how to use it with SQL Server vNext CTP 1.1 on Linux. PowerShell support for SQL Server is currently available on Windows, so you can use it when you have a Windows machine that can connect to a remote SQL Server instance on Linux.

## Install the newest version of SQL PowerShell on Windows

[SQL PowerShell](https://msdn.microsoft.com/en-us/library/mt740629.aspx) on Windows is included with [SQL Server Management Studio (SSMS)](https://msdn.microsoft.com/en-us/library/hh213248.aspx). When working with SQL Server, you should always use the most recent version of SSMS and SQL PowerShell. The latest version of SSMS is continually updated and optimized and currently works with SQL Server vNext CTP 1.1 on Linux. To download and install the latest version, see [Download SQL Server Management Studio](https://msdn.microsoft.com/library/mt238290.aspx). To stay up-to-date, the latest version of SSMS prompts you when there is a new version available to download. 

## Before you begin
- Read the [Known Issues](sql-server-linux-release-notes.md) for SQL Server vNext CTP 1.1 on Linux.

## Launch PowerShell and import the *sqlserver* module

Let's start by launching PowerShell on Windows. Open a *command prompt* on your Windows computer and copy and paste the commands below to launch `powershell`.
```
powershell -ExecutionPolicy bypass
```

SQL Server provides a Windows PowerShell module named `sqlserver` that you can use to import the SQL Server components (SQL Server provider and cmdlets) into a PowerShell environment or script.
Copy and paste the command below at the PowerShell prompt to import the `sqlserver` module into your current PowerShell session:
```
Import-Module sqlserver -DisableNameChecking;
```

Type the command below at the PowerShell prompt to verify that the `sqlserver` module was imported correctly:
```
Get-Module -Name "sqlserver"
```

PowerShell should display information similar to what's below:
```
ModuleType Version    Name          ExportedCommands
---------- -------    ----          ----------------
Manifest   20.0       sqlserver     {Add-SqlAvailabilityDatabase, Add-SqlAvailabilityGroupList...
Script     0.0        SqlServer
```

## Connect to SQL Server and get server information

Let's use PowerShell on Windows to connect to your SQL Server vNext instance on Linux and display a couple of server properties.

Copy and paste the commands below at the PowerShell prompt. When you run these commands, PowerShell will:
- display the *Windows PowerShell credential request* dialog that prompts you for the credentials (*SQL username* and *SQL password*) to connect to your SQL Server vNext CTP 1.1 instance on Linux
- load the SQL Server Management Objects (SMO) assembly
- create an instance of the [Server](https://msdn.microsoft.com/en-us/library/microsoft.sqlserver.management.smo.server.aspx) object
- connect to the `Server` and display a few properties

Remember to replace `<your_server_instance>` with the IP address or the hostname of your SQL Server vNext CTP 1.1 instance on Linux.
```
# Prompt for credentials to login into SQL Server
$serverInstance = "<your_server_instance>"
$credential = Get-Credential

# Load the SMO assembly and create a Server object
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SMO') | out-null
$server = New-Object ('Microsoft.SqlServer.Management.Smo.Server') $serverInstance

# Set credentials
$server.ConnectionContext.LoginSecure=$false
$server.ConnectionContext.set_Login($credential.UserName)
$server.ConnectionContext.set_SecurePassword($credential.Password)

# Connect to the Server and get a few properties
$server.Information | Select-Object Edition, HostPlatform, HostDistribution | Format-List
# done

```

PowerShell should display information similar to what's shown below:
```
Edition          : Developer Edition (64-bit)
HostPlatform     : Linux
HostDistribution : Ubuntu
```

## Examine SQL Server error logs

Let's use PowerShell on Windows to examine error logs connect on your SQL Server vNext instance on Linux. We will also use the `Out-GridView` cmdlet to show information from the error logs in a grid view display.

Copy and paste the commands below at the PowerShell prompt. These commands may take a few minutes to run and do the following:
- display the *Windows PowerShell credential request* dialog that prompts you for the credentials (*SQL username* and *SQL password*) to connect to your SQL Server vNext CTP 1.1 instance on Linux
- use the `Get-SqlErrorLog` cmdlet to connect to the SQL Server vNext instance on Linux and retrieve error logs since `yesterday`
- pipe the output to the `Out-GridView` cmdlet

Remember to replace `<your_server_instance>` with the IP address or the hostname of your SQL Server vNext CTP 1.1 instance on Linux.

```
# Prompt for credentials to login into SQL Server
$serverInstance = "<your_server_instance>"
$credential = Get-Credential

# Retrieve error logs since yesterday
Get-SqlErrorLog -ServerInstance $serverInstance -Credential $credential -Since Yesterday | Out-GridView
# done

```

## See also
- [SQL Server PowerShell](https://msdn.microsoft.com/en-us/library/hh245198.aspx)
