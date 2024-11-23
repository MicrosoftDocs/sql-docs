---
title: Enable or disable a server network protocol
description: Use SQL Server Configuration Manager or PowerShell to enable or disable a SQL Server server network protocol.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/11/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "network protocols [SQL Server], disabling"
  - "remote connections [SQL Server], enabling using Configuration Manager"
  - "protocols [SQL Server], enabling using Configuration Manager"
  - "protocols [SQL Server], disabling using Configuration Manager"
  - "disabling network protocols, Configuration Manager"
  - "network protocols [SQL Server], enabling"
  - "enabling network protocols, Configuration Manager"
  - "surface area configuration [SQL Server], connection protocols"
  - "connections [SQL Server], enabling remote using Configuration Manager"
---
# Enable or disable a server network protocol

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

All network protocols are installed during installation, by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, but may or may not be enabled. This article describes how to enable or disable a server network protocol in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager or PowerShell. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] must be stopped and restarted for the change to take effect.

## Remarks

- During setup of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] edition, a login is added for the BUILTIN\Users group. This login allows all authenticated users of the computer to access the instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] as a member of the public role. The BUILTIN\Users login can be safely removed to restrict [!INCLUDE[ssDE](../../includes/ssde-md.md)] access to computer users who have individual logins or are members of other Windows groups with logins.

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[msCoName](../../includes/msconame-md.md)] data providers for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] up to [!INCLUDE[sssql14](../../includes/sssql14-md.md)] only support TLS 1.0 and SSL 3.0 by default. If you enforce a different protocol (such as TLS 1.1 or TLS 1.2) by making changes in the operating system SChannel layer, your connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might fail, unless you install the appropriate update to add support for TLS 1.1 and 1.2 to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [KB 3135244](https://support.microsoft.com/help/3135244/). Starting from [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], all release versions of SQL Server include TLS 1.2 support without further updates required.

## <a id="SSMSProcedure"></a> Use SQL Server Configuration Manager

1. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, expand **SQL Server  Network Configuration**.

1. In the console pane, select **Protocols for _\<instance name>_**.

1. In the details pane, right-click the protocol you want to change, and then select **Enable** or **Disable**.

1. In the console pane, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server (_\<instance name>_)**, and then select **Restart**, to stop and restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.

> [!NOTE]  
> If you have a named instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], including [!INCLUDE [ssexpress-md](../../includes/ssexpress-md.md)] edition, you should also restart the SQL Server Browser service.

## <a id="PowerShellProcedure"></a> Use SQL Server PowerShell

#### Enable a server network protocol with PowerShell

1. Using administrator permissions, open a command prompt.

1. Start Windows PowerShell from the taskbar or Start menu.

1. Import the **SqlServer** module by entering `Import-Module SqlServer`.

1. Execute the following statements to enable both the TCP and named pipes protocols. Replace `<computer_name>` with the name of the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you are configuring a named instance (including [!INCLUDE [ssexpress-md](../../includes/ssexpress-md.md)] edition), replace `MSSQLSERVER` with the instance name.

   To disable protocols, set the `IsEnabled` properties to `$false`.

   You can run this script from any machine, with or without SQL Server installed. Make sure you have the **SqlServer** module installed.

   ```powershell
   #requires the SqlServer module
   Import-Module SQLServer

   $wmi = New-Object Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer <#computer_name#>

   # List the object properties, including the instance names.
   $Wmi

   # Enable the TCP protocol on the default instance.
   $uri = "ManagedComputer[@Name='<#computer_name#>']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']"
   $Tcp = $wmi.GetSmoObject($uri)
   $Tcp.IsEnabled = $true
   $Tcp.Alter()
   $Tcp
 
   # Enable the named pipes protocol for the default instance.
   $uri = "ManagedComputer[@Name='<#computer_name#>']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Np']"
   $Np = $wmi.GetSmoObject($uri)
   $Np.IsEnabled = $true
   $Np.Alter()
   $Np
   ```

#### Configure the protocols for the local computer

When the script is run locally and configures the local computer, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell can make the script more flexible by dynamically determining the local computer name. To retrieve the local computer name, replace the line setting the `$uri` variable with the following line.

```powershell
$uri = "ManagedComputer[@Name='" + (get-item env:\computername).Value + "']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']"
```

#### Restart the Database Engine with SQL Server PowerShell

After you enable or disable protocols, you must stop and restart the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for the change to take effect. Execute the following statements to stop and start the default instance by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell. To stop and start a named instance, replace `'MSSQLSERVER'` with `'MSSQL$<instance_name>'`.

```powershell
# Get a reference to the ManagedComputer class.
CD SQLSERVER:\SQL\<computer_name>
$Wmi = (get-item .).ManagedComputer
# Get a reference to the default instance of the Database Engine.
$DfltInstance = $Wmi.Services['MSSQLSERVER']
# Display the state of the service.
$DfltInstance
# Stop the service.
$DfltInstance.Stop();
# Wait until the service has time to stop.
# Refresh the cache.
$DfltInstance.Refresh();
# Display the state of the service.
$DfltInstance
# Start the service again.
$DfltInstance.Start();
# Wait until the service has time to start.
# Refresh the cache and display the state of the service.
$DfltInstance.Refresh();
$DfltInstance
```

> [!NOTE]  
> If you have a named instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], including [!INCLUDE [ssexpress-md](../../includes/ssexpress-md.md)] edition, you should also restart the SQL Server Browser service.

## Related content

- [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md)
- [SQL Server Browser (SQL Server Configuration Manager)](../../tools/configuration-manager/sql-server-browser-sql-server-configuration-manager.md)
