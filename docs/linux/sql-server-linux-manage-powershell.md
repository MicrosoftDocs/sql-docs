---
title: Manage SQL Server on Linux with PowerShell | Microsoft Docs
description: This article provides an overview of using PowerShell on Windows with SQL Server on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/02/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: a3492ce1-5d55-4505-983c-d6da8d1a94ad
---
# Use PowerShell on Windows to Manage SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article introduces [SQL Server PowerShell](https://msdn.microsoft.com/library/mt740629.aspx) and walks you through a couple of examples on how to use it with SQL Server on Linux. PowerShell support for SQL Server is currently available on Windows, so you can use it when you have a Windows machine that can connect to a remote SQL Server instance on Linux.

## Install the newest version of SQL PowerShell on Windows

[SQL PowerShell](https://msdn.microsoft.com/library/mt740629.aspx) on Windows is included with [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md). When working with SQL Server, you should always use the most recent version of SSMS and SQL PowerShell. The latest version of SSMS is continually updated and optimized and currently works with SQL Server on Linux. To download and install the latest version, see [Download SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md). To stay up-to-date, the latest version of SSMS prompts you when there is a new version available to download.

## Before you begin

Read the [Known Issues](sql-server-linux-release-notes.md) for SQL Server on Linux.

## Launch PowerShell and import the *sqlserver* module

Let's start by launching PowerShell on Windows. Open a *command prompt* on your Windows computer, and type **PowerShell** to launch a new Windows PowerShell session.

```
PowerShell
```

SQL Server provides a Windows PowerShell module named **SqlServer** that you can use to import the SQL Server components (SQL Server provider and cmdlets) into a PowerShell environment or script.

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
Script     0.0        SqlServer
Manifest   20.0       SqlServer     {Add-SqlAvailabilityDatabase, Add-SqlAvailabilityGroupList...
```

## Connect to SQL Server and get server information

Let's use PowerShell on Windows to connect to your SQL Server instance on Linux and display a couple of server properties.

Copy and paste the following commands at the PowerShell prompt. When you run these commands, PowerShell will:
- Display the *Windows PowerShell credential request* dialog that prompts you for the credentials (*SQL username* and *SQL password*) to connect to your SQL Server instance on Linux
- Load the SQL Server Management Objects (SMO) assembly
- Create an instance of the [Server](https://msdn.microsoft.com/library/microsoft.sqlserver.management.smo.server.aspx) object
- Connect to the **Server** and display a few properties

Remember to replace **\<your_server_instance\>** with the IP address or the hostname of your SQL Server instance on Linux.

```powershell
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

PowerShell should display information similar to the following output:

```
Edition          : Developer Edition (64-bit)
HostPlatform     : Linux
HostDistribution : Ubuntu
```
> [!NOTE]
> If nothing is displayed for these values, the connection to the target SQL Server instance most likely failed. Make sure that you can use the same connection information to connect from SQL Server Management Studio. Then review the [connection troubleshooting recommendations](sql-server-linux-troubleshooting-guide.md#connection).

## Examine SQL Server error logs

Let's use PowerShell on Windows to examine error logs connect on your SQL Server instance on Linux. We will also use the **Out-GridView** cmdlet to show information from the error logs in a grid view display.

Copy and paste the following commands at the PowerShell prompt. They might take a few minutes to run. These commands do the following:
- Display the *Windows PowerShell credential request* dialog that prompts you for the credentials (*SQL username* and *SQL password*) to connect to your SQL Server instance on Linux
- Use the **Get-SqlErrorLog** cmdlet to connect to the SQL Server instance on Linux and retrieve error logs since **Yesterday**
- Pipe the output to the **Out-GridView** cmdlet

Remember to replace **\<your_server_instance\>** with the IP address or the hostname of your SQL Server instance on Linux.

```powershell
# Prompt for credentials to login into SQL Server
$serverInstance = "<your_server_instance>"
$credential = Get-Credential

# Retrieve error logs since yesterday
Get-SqlErrorLog -ServerInstance $serverInstance -Credential $credential -Since Yesterday | Out-GridView
# done
```
## See also
- [SQL Server PowerShell](../relational-databases/scripting/sql-server-powershell.md)
