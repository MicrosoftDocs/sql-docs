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

This article introduces [SQL Server PowerShell](../powershell/sql-server-powershell.md) and walks you through a couple of examples on how to use it with SQL Server on Linux. PowerShell support for SQL Server is currently available on Windows, so you can use it when you have a Windows machine that can connect to a remote SQL Server instance on Linux.

## Install the newest version of SQL PowerShell on Windows

[SQL PowerShell](../powershell/download-sql-server-ps-module.md) on Windows is maintained in the PowerShell Gallery. When working with SQL Server, you should always use the most recent version of the SqlServer PowerShell module.

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
Script     21.1.18102 SqlServer     {Add-SqlAvailabilityDatabase, Add-SqlAvailabilityGroupList...
```

## Connect to SQL Server and get server information

Let's use PowerShell on Windows to connect to your SQL Server instance on Linux and display a couple of server properties.

Copy and paste the following commands at the PowerShell prompt. When you run these commands, PowerShell will:
- Display the *Windows PowerShell credential request* dialog that prompts you for the credentials (*SQL username* and *SQL password*) to connect to your SQL Server instance on Linux
- Create an instance of the [Server](https://msdn.microsoft.com/library/microsoft.sqlserver.management.smo.server.aspx) object
- Connect to the **Server** and display a few properties

Remember to replace **\<your_server_instance\>** with the IP address or the hostname of your SQL Server instance on Linux.

```powershell
# Prompt for credentials to login into SQL Server
$serverInstance = "<your_server_instance>"
$credential = Get-Credential

# Connect to the Server and get a few properties
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
